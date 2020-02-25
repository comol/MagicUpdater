using MagicUpdater.DL;
using MagicUpdater.DL.DB;
using MagicUpdater.DL.Helpers;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MagicUpdaterSheduler.Main
{
	public class ShedulerStepObj : ShedulerStep
	{
		public Guid GroupGuid => Guid.NewGuid();

		public int OperationID { get; private set; }

		public int ComputerId { get; private set; }

		public bool Run(int computerId)
		{
			ComputerId = computerId;
			try
			{
				var res = MQueryCommand.TryInsertNewOperationByShedulerStep(computerId, OperationType, OperationAttributes);
				if (!res.IsComplete)
				{
					MLogger.Error($"1 - {res.Message}");
					MQueryCommand.TryInsertShedulerHistory(TaskId, Id, computerId, false, res.Message);
					return false;
				}

				OperationID = res.ReturnedId;
				MLogger.Debug($"\"Step Run\", ThreadId: {Thread.CurrentThread.ManagedThreadId}, AddedOperationId: {OperationID}");
				MQueryCommand.TryInsertShedulerHistory(TaskId, Id, computerId, true, $"Выполнено. Операция {OperationID} добавлена.");
				return true;

			}
			catch (Exception ex)
			{
				MLogger.Error($"2 - {ex.ToString()}");
				MQueryCommand.TryInsertShedulerHistory(TaskId, Id, computerId, true, ex.Message);
				return false;
			}
		}

	}

	public class ShedulerStepInfo
	{
		private int _nextStep;
		private List<int> _computerIds;

		public ShedulerStepInfo(int nextStep, List<int> computerIds)
		{
			if (computerIds == null)
				throw new NullReferenceException("computerIds can't be null");
			_nextStep = nextStep;
			_computerIds = computerIds;
		}
	}
}
