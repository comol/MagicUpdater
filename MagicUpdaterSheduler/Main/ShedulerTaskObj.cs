using MagicUpdater.DL;
using MagicUpdater.DL.DB;
using MagicUpdater.DL.Helpers;
using MagicUpdater.DL.Tools;
using MagicUpdaterCommon.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MagicUpdaterSheduler.Main
{
	public class ShedulerTaskObj : ShedulerTask
	{
		private Guid stepGuid = Guid.NewGuid();

		private ShedulerStepObj GetStep(int? Id)
		{
			if (Id == null)
				return null;

			var step = MQueryCommand.SelectShedulerStep(Id);

			if (step != null)
			{
				return new ShedulerStepObj()
				{
					Id = step.Id,
					OnOperationCompleteStep = step.OnOperationCompleteStep,
					OnOperationErrorStep = step.OnOperationErrorStep,
					OperationCheckIntervalMs = step.OperationCheckIntervalMs,
					OperationType = step.OperationType,
					OrderId = step.OrderId,
					ShedulerTask = step.ShedulerTask,
					ShedulerTasks = step.ShedulerTasks,
					TaskId = step.TaskId,
					RepeatCount = step.RepeatCount,
					RepeatTimeout = step.RepeatTimeout,
					ShedulerTaskHistories = step.ShedulerTaskHistories,
					OperationAttributes = step.OperationAttributes
				};
			}
			else
				return null;
		}

		private ShedulerStepObj CheckStepResultNew(ShedulerStepObj step)
		{
			MLogger.Debug($"\"CheckStepResultNew\", StepId: {step.Id}, ThreadId: {Thread.CurrentThread.ManagedThreadId}, OperationId: {step.OperationID}");
			var operation = MQueryCommand.SelectOperationById(step.OperationID);

			if (operation != null)
			{
				//операция не прочитана
				if (!(operation.IsRead ?? false))
				{
					MLogger.Debug($"\"CheckStepResultNew\" - operation not readed, StepId: {step.Id}, ThreadId: {Thread.CurrentThread.ManagedThreadId}, OperationId: {step.OperationID}");
					var typeName = OperationTools.GetOperationNameEnById(operation.OperationType);
					MQueryCommand.TryInsertShedulerHistory(Id, step.Id, step.ComputerId, false, $"Операция \"{typeName}\" для компьютера \"{operation.ComputerName}\" не прочитана. ID операции = {operation.ID}");
					return null;
				}

				//операция прочитана, но не выполена
				if ((operation.IsRead ?? false) && (!(operation.IsCompleted ?? false) || (operation.Is1CError == 1)))
				{
					MLogger.Debug($"\"CheckStepResultNew\" - operation readed, bun not completed, StepId: {step.Id}, ThreadId: {Thread.CurrentThread.ManagedThreadId}, OperationId: {step.OperationID}");
					if (step.RepeatCount > 0)
					{
						MLogger.Debug($"\"CheckStepResultNew\" - step.RepeatCount > 0) operation readed, bun not completed, StepId: {step.Id}, ThreadId: {Thread.CurrentThread.ManagedThreadId}, OperationId: {step.OperationID}");
						MLogger.Debug($"\"CheckStepResultNew\" - step.RepeatCount ((до step.RepeatCount -= 1) = {step.RepeatCount}) operation readed, bun not completed, StepId: {step.Id}, ThreadId: {Thread.CurrentThread.ManagedThreadId}, OperationId: {step.OperationID}");
						step.RepeatCount -= 1;
						MLogger.Debug($"\"CheckStepResultNew\" - step.RepeatCount ((после step.RepeatCount -= 1) = {step.RepeatCount}) operation readed, bun not completed, StepId: {step.Id}, ThreadId: {Thread.CurrentThread.ManagedThreadId}, OperationId: {step.OperationID}");
						Thread.Sleep(step.RepeatTimeout ?? 0);
						MLogger.Debug($"\"CheckStepResultNew\" - step.RepeatCount (после step.RepeatCount -= 1), operation readed, bun not completed, return StepId: {step.Id}, ThreadId: {Thread.CurrentThread.ManagedThreadId}, OperationId: {step.OperationID}");
						return step;
					}
					else
					{
						var newStep = GetStep(step.OnOperationErrorStep);
						if (newStep != null)
							MLogger.Debug($"\"CheckStepResultNew\" - step.RepeatCount <= 0, operation readed, bun not completed, return StepId: {newStep.Id}, ThreadId: {Thread.CurrentThread.ManagedThreadId}, OperationId: {step.OperationID}");
						else
							MLogger.Debug($"\"CheckStepResultNew\" - step.RepeatCount <= 0, operation readed, bun not completed, return StepId: null, ThreadId: {Thread.CurrentThread.ManagedThreadId}");
						return newStep;
					}
				}

				//операция прочитана и выполнена
				if ((operation.IsRead ?? false) && (operation.IsCompleted ?? false) && (operation.Is1CError == 0))
				{
					var newStep = GetStep(step.OnOperationCompleteStep);
					if (newStep != null)
						MLogger.Debug($"\"CheckStepResultNew\" - operation readed, bun not completed, return StepId: {newStep.Id}, ThreadId: {Thread.CurrentThread.ManagedThreadId}, OperationId: {step.OperationID}");
					else
						MLogger.Debug($"\"CheckStepResultNew\" - operation readed, bun not completed, return StepId: null, ThreadId: {Thread.CurrentThread.ManagedThreadId}");
					return newStep;
				}
			}
			return null;
		}

		public void Run()
		{
			var res = MQueryCommand.TryUpdateLastStartTime(Id, DateTime.UtcNow);
			if (!res.IsComplete)
			{
				MLogger.Error($"3 - {res.Message}");
			}

			List<Task> taskList = new List<Task>();

			foreach (var compId in ShedulerTasksComputersLists.Select(x => x.ComputerId))
			{
				taskList.Add(Task.Factory.StartNew(() =>
				{
					var threadStep = GetStep(FirstStepId);
					if (threadStep == null)
					{
						MLogger.Error($"Error while getting first step of task (TaskId: {Id}, FirstStepId: {FirstStepId}). Operation won't be send.");
					}

					while (threadStep != null)
					{
						if (threadStep.OperationCheckIntervalMs > 0)
						{
							if (threadStep.Run(compId))
							{
								Thread.Sleep(threadStep.OperationCheckIntervalMs);
								threadStep = CheckStepResultNew(threadStep);
							}
							else
								return;
						}
						else
						{
							if (threadStep.Run(compId))
								threadStep = GetStep(threadStep.OnOperationCompleteStep);
							else
								return;
						}
					}
				}));
				MLogger.Debug($"Task for compId: {compId} added");
			}

			MLogger.Debug($"Start multithread [taskList.ToArray()]");

			try
			{
				Task.WaitAll(taskList.ToArray());
			}
			catch (Exception ex)
			{
				MLogger.Error($"4 - {ex.ToString()}");
			}

			MLogger.Debug($"Before \"TryUpdateLastEndTime\"");
			res = MQueryCommand.TryUpdateLastEndTime(Id, DateTime.UtcNow);
			if (!res.IsComplete)
			{
				MLogger.Error($"5 - {res.Message}");
			}

			DateTime period = new DateTime(1889, 1, 1, 0, 0, 0);
			switch ((ShedulerTaskModes)Mode)
			{
				case ShedulerTaskModes.Once:
					res = MQueryCommand.TrySetTaskEnabled(Id, false);
					if (!res.IsComplete)
					{
						MLogger.Error($"6 - {res.Message}");
					}

					period = new DateTime(1889, 1, 1, 0, 0, 0);
					break;
				case ShedulerTaskModes.Daily:
					period = DateTime.UtcNow.AddDays(1);
					break;
				case ShedulerTaskModes.Weekly:
					period = DateTime.UtcNow.AddDays(7);
					break;
				case ShedulerTaskModes.Monthly:
					period = DateTime.UtcNow.AddMonths(1);
					break;
				case ShedulerTaskModes.Hours:
					period = DateTime.UtcNow.AddHours(RepeatValue.HasValue ? RepeatValue.Value : 0);
					break;
				case ShedulerTaskModes.Minutes:
					period = DateTime.UtcNow.AddMinutes(RepeatValue.HasValue ? RepeatValue.Value : 0);
					break;
			}

			res = MQueryCommand.TryUpdateNextStartTime(Id, period);
			if (!res.IsComplete)
			{
				MLogger.Error($"7 - {res.Message}");
			}
		}

		public static List<ShedulerTaskObj> GetShedulerTasks()
		{
			var tasks = MQueryCommand.SelectShedulerTasks();

			return tasks.Select(c => new ShedulerTaskObj
			{
				Id = c.Id,
				FirstStepId = c.FirstStepId,
				LastEndTime = c.LastEndTime,
				LastStartTime = c.LastStartTime,
				Mode = c.Mode,
				Name = c.Name,
				NextStartTime = c.NextStartTime,
				ShedulerStep = c.ShedulerStep,
				ShedulerSteps = c.ShedulerSteps,
				ShedulerTasksComputersLists = c.ShedulerTasksComputersLists,
				StartTime = c.StartTime,
				Status = c.Status,
				Enabled = c.Enabled,
				RepeatValue = c.RepeatValue
			}).ToList();
		}

		private class ShedulerStepCheckResult
		{
			private readonly List<int> successIds;
			private readonly List<int> failerIds;
			private readonly List<int> notRead;
			public List<int> SuccessIds => successIds;
			public List<int> FailerIds => failerIds;
			public List<int> NotRead => notRead;

			public ShedulerStepCheckResult(List<int> _successIds, List<int> _failerIds, List<int> _notRead)
			{
				if (_successIds == null || _failerIds == null || _notRead == null)
				{
					throw new NullReferenceException();
				}

				successIds = _successIds;
				failerIds = _failerIds;
				notRead = _notRead;
			}
		}

		public ShedulerStatuses ShedulerStatus => (ShedulerStatuses)Status;
		public bool IsTimeToRun => (Enabled ?? false) && (DateTime.UtcNow >= NextStartTime);
	}
}
