using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System;
using MagicUpdater.DL.Models;
using System.Collections.Generic;
using MagicUpdater.DL.Tools;
using MagicUpdater.DL.Helpers;
using MagicUpdaterCommon.Helpers;
using MagicUpdater.DL.Common;
using MagicUpdaterMonitor.Base;
using MagicUpdaterCommon.Common;
using MagicUpdaterCommon.SettingsTools;

namespace MagicUpdater.DL.DB
{
	public enum ShedulerUserHistoryAction
	{
		Insert = 0,
		Update = 1,
		Delete = 2
	}

	public class TrySQLCommand
	{
		public TrySQLCommand(bool isComplete, string message, int returnedId = 0)
		{
			IsComplete = isComplete;
			Message = message;
			ReturnedId = returnedId;
		}

		public bool IsComplete { get; private set; }
		public string Message { get; private set; }
		public int ReturnedId { get; private set; }
	}

	public class TrySQLQuery : TryResult
	{
		public TrySQLQuery(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}

	public static class MQueryCommand
	{
		private static object lockTryInsertShedulerHistory = new object();
		private static object lockTryUpdateLastStartTime = new object();
		private static object lockTryUpdateLastEndTime = new object();
		private static object lockTryUpdateNextStartTime = new object();
		private static object lockTryInsertNewTask = new object();
		private static object lockTryDeleteTask = new object();
		private static object lockTryDeletePluginTask = new object();
		private static object lockGetAllStepsParentChild = new object();
		private static object lockGetStepById = new object();
		private static object lockGetAllSteps = new object();
		private static object lockTryInsertNewStep = new object();
		private static object lockTryInsertNewOperationFromStep = new object();
		private static object lockSelectOperationById = new object();
		private static object lockSelectShedulerTasks = new object();
		private static object lockSelectShedulerPluginTasks = new object();
		private static object lockSelectShedulerStep = new object();
		private static object lockTryUpdateStep = new object();
		private static object lockTryInsertNewOperationByShedulerStep = new object();
		private static object lockGetMonitorUsersRealCount = new object();
		private static object lockGetAgentUsersRealCount = new object();

		private static CommonGlobalSettings _commonGlobalSettings = new CommonGlobalSettings();

		static MQueryCommand()
		{
			_commonGlobalSettings.LoadCommonGlobalSettings();
		}

		#region Commands
		public static TrySQLCommand TryUpdatePcCounts(int monitorCount, int agentCount)
		{
			try
			{
				using (EntityDb context = new EntityDb())
				{
					CommonGlobalSetting commonGlobalSettingMonitorCount = context.CommonGlobalSettings.FirstOrDefault(f => f.Name == CommonGlobalSettings.Lic_Monitor_Count);
					CommonGlobalSetting commonGlobalSettingAgentCount = context.CommonGlobalSettings.FirstOrDefault(f => f.Name == CommonGlobalSettings.Lic_Agents_Count);

					if (commonGlobalSettingMonitorCount == null)
					{
						return new TrySQLCommand(false, "Ошибка счетчика мониторов");
					}

					if (commonGlobalSettingAgentCount == null)
					{
						return new TrySQLCommand(false, "Ошибка счетчика агентов");
					}

					commonGlobalSettingMonitorCount.Value = monitorCount.ToString();
					commonGlobalSettingAgentCount.Value = agentCount.ToString();
					context.SaveChanges();
					return new TrySQLCommand(true, "");
				}
			}
			catch (Exception ex)
			{
				return new TrySQLCommand(false, ex.ToString());
			}
		}

		public static TrySQLCommand TryUpdateMonitorLic(int userId, LicResponce licResponce)
		{
			try
			{
				using (EntityDb context = new EntityDb())
				{
					User user = context.Users.FirstOrDefault(f => f.Id == userId);
					if (user == null)
					{
						return new TrySQLCommand(false, $"Пользователь с userId = {userId} отсутствует");
					}

					user.LicId = licResponce.Val;
					context.SaveChanges();
					return new TrySQLCommand(true, "");
				}
			}
			catch (Exception ex)
			{
				return new TrySQLCommand(false, ex.ToString());
			}
		}

		public static TrySQLCommand TryUpdateAgentLic(int computerId, LicResponce licResponce)
		{
			try
			{
				using (EntityDb context = new EntityDb())
				{
					ShopComputer shopComputer = context.ShopComputers.FirstOrDefault(f => f.ComputerId == computerId);
					if (shopComputer == null)
					{
						return new TrySQLCommand(false, $"Агент с computerId = {computerId} отсутствует");
					}

					LicAgent licAgent = context.LicAgents.FirstOrDefault(f => f.ComputerId == computerId);
					if (licAgent == null)
					{
						return new TrySQLCommand(false, $"Агент с computerId = {computerId} отсутствует в таблице лицензий");
					}

					licAgent.LicId = licResponce.Val;
					context.SaveChanges();
					return new TrySQLCommand(true, "");
				}
			}
			catch (Exception ex)
			{
				return new TrySQLCommand(false, ex.ToString());
			}
		}

		public static TrySQLCommand TryDeleteInsertUpdateTaskFromForm(ShedulerTask task, IEnumerable<ShedulerStep> steps)
		{
			try
			{
				using (EntityDb context = new EntityDb())
				{
					using (var dbContextTransaction = context.Database.BeginTransaction())
					{
						try
						{
							ShedulerUserHistoryAction taskHistoryAction;
							ShedulerTask editTask = null;
							int? firstStepReplace = task.FirstStepId;
							if (task.Id == 0)
							{
								task.FirstStepId = null;
								context.ShedulerTasks.Add(task);
								context.SaveChanges();
								taskHistoryAction = ShedulerUserHistoryAction.Insert;
							}
							else
							{
								editTask = context.ShedulerTasks.First(f => f.Id == task.Id);
								editTask.LastEndTime = task.LastEndTime;
								editTask.LastStartTime = task.LastEndTime;
								editTask.Mode = task.Mode;
								editTask.Name = task.Name;
								editTask.Enabled = task.Enabled;
								editTask.NextStartTime = task.NextStartTime;
								editTask.RepeatValue = task.RepeatValue;
								editTask.FirstStepId = null;
								//editTask.ShedulerTasksComputersLists = task.ShedulerTasksComputersLists;
								editTask.StartTime = task.StartTime;
								editTask.Status = task.Status;
								editTask.Description = task.Description;
								context.SaveChanges();
								taskHistoryAction = ShedulerUserHistoryAction.Update;
							}

							var computersListSql = context.ShedulerTasksComputersLists.Where(w => w.TaskId == task.Id);

							List<ShedulerTasksComputersList> computersToRemove = new List<ShedulerTasksComputersList>();
							List<ShedulerTasksComputersList> computersToAdd = new List<ShedulerTasksComputersList>();

							foreach (var item in computersListSql)
							{
								if (!task.ShedulerTasksComputersLists.Select(s => s.ComputerId).Contains(item.ComputerId))
								{
									computersToRemove.Add(item);
								}
							}

							foreach (var item in task.ShedulerTasksComputersLists)
							{
								if (!computersListSql.Select(s => s.ComputerId).Contains(item.ComputerId))
								{
									item.TaskId = task.Id;
									computersToAdd.Add(item);
								}
							}

							//var computersToRemove = computersListSql.Where(w => !task.ShedulerTasksComputersLists.Select(s => s.ComputerId).Contains(w.ComputerId));
							//var computersToAdd = task.ShedulerTasksComputersLists.Where(w => !computersListSql.Select(s => s.ComputerId).Contains(w.ComputerId));

							context.ShedulerTasksComputersLists.RemoveRange(computersToRemove);
							context.ShedulerTasksComputersLists.AddRange(computersToAdd);
							context.SaveChanges();

							var stepsSql = context.ShedulerSteps
											.Include(x => x.ShedulerTasks)
											.Include(x => x.ShedulerTaskHistories)
											.Where(x => x.TaskId == task.Id).ToList();

							var stepsForDelete = stepsSql.Where(w => !steps.Select(s => s.Id).Contains(w.Id));
							context.ShedulerSteps.RemoveRange(stepsForDelete);
							context.SaveChanges();
							AddShedulerStepUserHistory(stepsForDelete.ToArray(), ShedulerUserHistoryAction.Delete, context);

							var stepsForInsert = steps.Where(w => w.Id < 0).OrderByDescending(o => o.Id);
							Dictionary<int, int> replaceStepsId = new Dictionary<int, int>();

							foreach (var stepForInsert in stepsForInsert)
							{
								int negativeId = stepForInsert.Id;
								stepForInsert.TaskId = task.Id;
								context.ShedulerSteps.Add(stepForInsert);
								context.SaveChanges();
								AddShedulerStepUserHistory(stepForInsert, ShedulerUserHistoryAction.Insert, context);
								replaceStepsId.Add(negativeId, stepForInsert.Id);
							}

							var stepsForUpdate = steps.ToList();

							stepsForUpdate.ForEach(e =>
							{
								if (e.Id < 0)
								{
									e.Id = replaceStepsId[e.Id];
								}

								e.OnOperationCompleteStep = replaceStepsId.ContainsKey(e.OnOperationCompleteStep) ?
												   replaceStepsId[e.OnOperationCompleteStep] :
												   e.OnOperationCompleteStep;
								e.OnOperationErrorStep = replaceStepsId.ContainsKey(e.OnOperationErrorStep) ?
												   replaceStepsId[e.OnOperationErrorStep] :
												   e.OnOperationErrorStep;
							});

							foreach (var stepFromUpdate in stepsForUpdate)
							{
								var stepForUpdate = context.ShedulerSteps
											.Include(x => x.ShedulerTasks)
											.Include(x => x.ShedulerTaskHistories)
											.First(f => f.Id == stepFromUpdate.Id);

								stepForUpdate.OnOperationCompleteStep = stepFromUpdate.OnOperationCompleteStep;
								stepForUpdate.OnOperationErrorStep = stepFromUpdate.OnOperationErrorStep;
								stepForUpdate.OperationAttributes = stepFromUpdate.OperationAttributes;
								stepForUpdate.OperationCheckIntervalMs = stepFromUpdate.OperationCheckIntervalMs;
								stepForUpdate.OperationType = stepFromUpdate.OperationType;
								stepForUpdate.OrderId = stepFromUpdate.OrderId;
								stepForUpdate.RepeatCount = stepFromUpdate.RepeatCount;
								stepForUpdate.RepeatTimeout = stepFromUpdate.RepeatTimeout;
							}

							if (firstStepReplace.HasValue)
							{
								switch (taskHistoryAction)
								{
									case ShedulerUserHistoryAction.Insert:
										task.FirstStepId = replaceStepsId.ContainsKey(firstStepReplace.Value) ? (int?)replaceStepsId[firstStepReplace.Value] : null;
										break;
									case ShedulerUserHistoryAction.Update:
										editTask.FirstStepId = replaceStepsId.ContainsKey(firstStepReplace.Value) ? replaceStepsId[firstStepReplace.Value] : task.FirstStepId;
										break;
								}
							}

							context.SaveChanges();

							AddShedulerStepUserHistory(stepsForUpdate.ToArray(), ShedulerUserHistoryAction.Update, context);

							switch (taskHistoryAction)
							{
								case ShedulerUserHistoryAction.Insert:
									AddShedulerTaskUserHistory(task, taskHistoryAction, context);
									break;
								case ShedulerUserHistoryAction.Update:
									AddShedulerTaskUserHistory(editTask, taskHistoryAction, context);
									break;
							}

							dbContextTransaction.Commit();
							return new TrySQLCommand(true, "");
						}
						catch (Exception ex)
						{
							dbContextTransaction.Rollback();
							return new TrySQLCommand(false, ex.ToString());
						}
					}



					//var stepsGridList = rgvSteps.DataSource.OfType<ViewShedulerStepModel>().ToList();
					//var stepsForDelete = stepsSql.Where(w => !stepsGridList.Select(s => s.Id).Contains(w.Id));

					//var tryDeleteStepsRes = MQueryCommand.TryDeleteSteps(stepsForDelete.Select(s => s.Id));
					//if (!tryDeleteStepsRes.IsComplete)
					//{
					//	MessageBox.Show(tryDeleteStepsRes.Message);
					//}
				}
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public static TrySQLCommand TryInsertUpdatePluginTaskFromForm(ShedulerPluginTask pluginTask)
		{
			try
			{
				using (EntityDb context = new EntityDb())
				{
					using (var dbContextTransaction = context.Database.BeginTransaction())
					{
						try
						{
							ShedulerUserHistoryAction taskHistoryAction;
							ShedulerPluginTask editPluginTask = null;
							if (pluginTask.Id == 0)
							{
								context.ShedulerPluginTasks.Add(pluginTask);
								context.SaveChanges();
								taskHistoryAction = ShedulerUserHistoryAction.Insert;
							}
							else
							{
								editPluginTask = context.ShedulerPluginTasks.First(f => f.Id == pluginTask.Id);
								editPluginTask.LastEndTime = pluginTask.LastEndTime;
								editPluginTask.LastStartTime = pluginTask.LastEndTime;
								editPluginTask.Mode = pluginTask.Mode;
								editPluginTask.Name = pluginTask.Name;
								editPluginTask.Enabled = pluginTask.Enabled;
								editPluginTask.NextStartTime = pluginTask.NextStartTime;
								editPluginTask.RepeatValue = pluginTask.RepeatValue;
								editPluginTask.Status = pluginTask.Status;
								editPluginTask.Description = pluginTask.Description;
								editPluginTask.PluginFileName = pluginTask.PluginFileName;
								context.SaveChanges();
								taskHistoryAction = ShedulerUserHistoryAction.Update;
							}

							switch (taskHistoryAction)
							{
								case ShedulerUserHistoryAction.Insert:
									AddShedulerPluginTaskUserHistory(pluginTask, taskHistoryAction, context);
									break;
								case ShedulerUserHistoryAction.Update:
									AddShedulerPluginTaskUserHistory(editPluginTask, taskHistoryAction, context);
									break;
							}

							dbContextTransaction.Commit();
							return new TrySQLCommand(true, "");
						}
						catch (Exception ex)
						{
							dbContextTransaction.Rollback();
							return new TrySQLCommand(false, ex.ToString());
						}
					}



					//var stepsGridList = rgvSteps.DataSource.OfType<ViewShedulerStepModel>().ToList();
					//var stepsForDelete = stepsSql.Where(w => !stepsGridList.Select(s => s.Id).Contains(w.Id));

					//var tryDeleteStepsRes = MQueryCommand.TryDeleteSteps(stepsForDelete.Select(s => s.Id));
					//if (!tryDeleteStepsRes.IsComplete)
					//{
					//	MessageBox.Show(tryDeleteStepsRes.Message);
					//}
				}
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public static TrySQLCommand AddShedulerTaskUserHistory(ShedulerTask shedulerTask, ShedulerUserHistoryAction shedulerUserHistoryAction, EntityDb db = null)
		{
			try
			{
				string computersList = string.Join(",", shedulerTask.ShedulerTasksComputersLists.Select(s => s.ComputerId));
				//string computersList = string.Join(",", computersListId);

				if (db == null)
				{
					using (EntityDb context = new EntityDb())
					{
						context.ShedulerTasksUserHistories.Add(new ShedulerTasksUserHistory
						{
							Id = shedulerTask.Id,
							Name = shedulerTask.Name,
							Enabled = shedulerTask.Enabled,
							FirstStepId = shedulerTask.FirstStepId,
							Mode = shedulerTask.Mode,
							StartTime = shedulerTask.StartTime,
							NextStartTime = shedulerTask.NextStartTime,
							LastStartTime = shedulerTask.LastStartTime,
							LastEndTime = shedulerTask.LastEndTime,
							Status = shedulerTask.Status,
							ChangeUserDateUtc = DateTime.UtcNow,
							ComputersList = computersList,
							UserLogin = Environment.UserName,
							ActionType = shedulerUserHistoryAction.ToString(),
						});

						context.SaveChanges();
					}
				}
				else
				{
					db.ShedulerTasksUserHistories.Add(new ShedulerTasksUserHistory
					{
						Id = shedulerTask.Id,
						Name = shedulerTask.Name,
						Enabled = shedulerTask.Enabled,
						FirstStepId = shedulerTask.FirstStepId,
						Mode = shedulerTask.Mode,
						StartTime = shedulerTask.StartTime,
						NextStartTime = shedulerTask.NextStartTime,
						LastStartTime = shedulerTask.LastStartTime,
						LastEndTime = shedulerTask.LastEndTime,
						Status = shedulerTask.Status,
						ChangeUserDateUtc = DateTime.UtcNow,
						ComputersList = computersList,
						UserLogin = Environment.UserName,
						ActionType = shedulerUserHistoryAction.ToString(),
					});

					db.SaveChanges();
				}
				return new TrySQLCommand(true, "");
			}
			catch (Exception ex)
			{
				return new TrySQLCommand(false, ex.ToString());
			}
		}

		public static TrySQLCommand AddShedulerPluginTaskUserHistory(ShedulerPluginTask shedulerPluginTask, ShedulerUserHistoryAction shedulerPluginUserHistoryAction, EntityDb db = null)
		{
			try
			{
				if (db == null)
				{
					using (EntityDb context = new EntityDb())
					{
						context.ShedulerPluginTasksUserHistories.Add(new ShedulerPluginTasksUserHistory
						{
							Id = shedulerPluginTask.Id,
							Name = shedulerPluginTask.Name,
							Enabled = shedulerPluginTask.Enabled,
							Mode = shedulerPluginTask.Mode,
							NextStartTime = shedulerPluginTask.NextStartTime,
							LastStartTime = shedulerPluginTask.LastStartTime,
							LastEndTime = shedulerPluginTask.LastEndTime,
							Status = shedulerPluginTask.Status,
							ChangeUserDateUtc = DateTime.UtcNow,
							UserLogin = Environment.UserName,
							ActionType = shedulerPluginUserHistoryAction.ToString(),
						});

						context.SaveChanges();
					}
				}
				else
				{
					db.ShedulerPluginTasksUserHistories.Add(new ShedulerPluginTasksUserHistory
					{
						Id = shedulerPluginTask.Id,
						Name = shedulerPluginTask.Name,
						Enabled = shedulerPluginTask.Enabled,
						Mode = shedulerPluginTask.Mode,
						NextStartTime = shedulerPluginTask.NextStartTime,
						LastStartTime = shedulerPluginTask.LastStartTime,
						LastEndTime = shedulerPluginTask.LastEndTime,
						Status = shedulerPluginTask.Status,
						ChangeUserDateUtc = DateTime.UtcNow,
						UserLogin = Environment.UserName,
						ActionType = shedulerPluginUserHistoryAction.ToString(),
					});

					db.SaveChanges();
				}
				return new TrySQLCommand(true, "");
			}
			catch (Exception ex)
			{
				return new TrySQLCommand(false, ex.ToString());
			}
		}

		public static TrySQLCommand AddShedulerTaskUserHistory(ShedulerTask[] shedulerTasks, ShedulerUserHistoryAction shedulerUserHistoryAction, EntityDb db = null)
		{
			try
			{
				if (db == null)
				{
					using (EntityDb context = new EntityDb())
					{
						foreach (ShedulerTask shedulerTask in shedulerTasks)
						{
							string computersList = string.Join(",", shedulerTask.ShedulerTasksComputersLists.Select(s => s.ComputerId));
							context.ShedulerTasksUserHistories.Add(new ShedulerTasksUserHistory
							{
								Id = shedulerTask.Id,
								Name = shedulerTask.Name,
								Enabled = shedulerTask.Enabled,
								FirstStepId = shedulerTask.FirstStepId,
								Mode = shedulerTask.Mode,
								StartTime = shedulerTask.StartTime,
								NextStartTime = shedulerTask.NextStartTime,
								LastStartTime = shedulerTask.LastStartTime,
								LastEndTime = shedulerTask.LastEndTime,
								Status = shedulerTask.Status,
								ChangeUserDateUtc = DateTime.UtcNow,
								ComputersList = computersList,
								UserLogin = Environment.UserName,
								ActionType = shedulerUserHistoryAction.ToString(),
							});
						}

						context.SaveChanges();
					}
				}
				else
				{
					foreach (ShedulerTask shedulerTask in shedulerTasks)
					{
						string computersList = string.Join(",", shedulerTask.ShedulerTasksComputersLists.Select(s => s.ComputerId));
						db.ShedulerTasksUserHistories.Add(new ShedulerTasksUserHistory
						{
							Id = shedulerTask.Id,
							Name = shedulerTask.Name,
							Enabled = shedulerTask.Enabled,
							FirstStepId = shedulerTask.FirstStepId,
							Mode = shedulerTask.Mode,
							StartTime = shedulerTask.StartTime,
							NextStartTime = shedulerTask.NextStartTime,
							LastStartTime = shedulerTask.LastStartTime,
							LastEndTime = shedulerTask.LastEndTime,
							Status = shedulerTask.Status,
							ChangeUserDateUtc = DateTime.UtcNow,
							ComputersList = computersList,
							UserLogin = Environment.UserName,
							ActionType = shedulerUserHistoryAction.ToString(),
						});
					}

					db.SaveChanges();
				}
				return new TrySQLCommand(true, "");
			}
			catch (Exception ex)
			{
				return new TrySQLCommand(false, ex.ToString());
			}
		}

		public static TrySQLCommand AddShedulerStepUserHistory(ShedulerStep shedulerStep, ShedulerUserHistoryAction shedulerUserHistoryAction, EntityDb db = null)
		{
			try
			{
				if (db == null)
				{
					using (EntityDb context = new EntityDb())
					{
						context.ShedulerStepsUserHistories.Add(new ShedulerStepsUserHistory
						{
							Id = shedulerStep.Id,
							TaskId = shedulerStep.TaskId,
							OperationType = shedulerStep.OperationType,
							RepeatCount = shedulerStep.RepeatCount,
							RepeatTimeout = shedulerStep.RepeatTimeout,
							OperationCheckIntervalMs = shedulerStep.OperationCheckIntervalMs,
							OnOperationCompleteStep = shedulerStep.OnOperationCompleteStep,
							OnOperationErrorStep = shedulerStep.OnOperationErrorStep,
							OrderId = shedulerStep.OrderId,
							ChangeUserDateUtc = DateTime.UtcNow,
							UserLogin = Environment.UserName,
							ActionType = shedulerUserHistoryAction.ToString(),
						});

						context.SaveChanges();
					}
				}
				else
				{
					db.ShedulerStepsUserHistories.Add(new ShedulerStepsUserHistory
					{
						Id = shedulerStep.Id,
						TaskId = shedulerStep.TaskId,
						OperationType = shedulerStep.OperationType,
						RepeatCount = shedulerStep.RepeatCount,
						RepeatTimeout = shedulerStep.RepeatTimeout,
						OperationCheckIntervalMs = shedulerStep.OperationCheckIntervalMs,
						OnOperationCompleteStep = shedulerStep.OnOperationCompleteStep,
						OnOperationErrorStep = shedulerStep.OnOperationErrorStep,
						OrderId = shedulerStep.OrderId,
						ChangeUserDateUtc = DateTime.UtcNow,
						UserLogin = Environment.UserName,
						ActionType = shedulerUserHistoryAction.ToString(),
					});

					db.SaveChanges();
				}
				return new TrySQLCommand(true, "");
			}
			catch (Exception ex)
			{
				return new TrySQLCommand(false, ex.ToString());
			}
		}

		public static TrySQLCommand AddShedulerStepUserHistory(ShedulerStep[] shedulerSteps, ShedulerUserHistoryAction shedulerUserHistoryAction, EntityDb db = null)
		{
			try
			{
				if (db == null)
				{
					using (EntityDb context = new EntityDb())
					{
						foreach (ShedulerStep shedulerStep in shedulerSteps)
						{
							context.ShedulerStepsUserHistories.Add(new ShedulerStepsUserHistory
							{
								Id = shedulerStep.Id,
								TaskId = shedulerStep.TaskId,
								OperationType = shedulerStep.OperationType,
								RepeatCount = shedulerStep.RepeatCount,
								RepeatTimeout = shedulerStep.RepeatTimeout,
								OperationCheckIntervalMs = shedulerStep.OperationCheckIntervalMs,
								OnOperationCompleteStep = shedulerStep.OnOperationCompleteStep,
								OnOperationErrorStep = shedulerStep.OnOperationErrorStep,
								OrderId = shedulerStep.OrderId,
								ChangeUserDateUtc = DateTime.UtcNow,
								UserLogin = Environment.UserName,
								ActionType = shedulerUserHistoryAction.ToString(),
							});
						}

						context.SaveChanges();
					}
				}
				else
				{
					foreach (ShedulerStep shedulerStep in shedulerSteps)
					{
						db.ShedulerStepsUserHistories.Add(new ShedulerStepsUserHistory
						{
							Id = shedulerStep.Id,
							TaskId = shedulerStep.TaskId,
							OperationType = shedulerStep.OperationType,
							RepeatCount = shedulerStep.RepeatCount,
							RepeatTimeout = shedulerStep.RepeatTimeout,
							OperationCheckIntervalMs = shedulerStep.OperationCheckIntervalMs,
							OnOperationCompleteStep = shedulerStep.OnOperationCompleteStep,
							OnOperationErrorStep = shedulerStep.OnOperationErrorStep,
							OrderId = shedulerStep.OrderId,
							ChangeUserDateUtc = DateTime.UtcNow,
							UserLogin = Environment.UserName,
							ActionType = shedulerUserHistoryAction.ToString(),
						});
					}

					db.SaveChanges();
				}
				return new TrySQLCommand(true, "");
			}
			catch (Exception ex)
			{
				return new TrySQLCommand(false, ex.ToString());
			}
		}

		public static TrySQLCommand EditOperationGroup(int groupId, string editName, string editDescription)
		{
			using (EntityDb context = new EntityDb())
			{
				try
				{
					OperationGroup operationGroup = context.OperationGroups.FirstOrDefault(f => f.Id == groupId);
					operationGroup.Name = editName;
					operationGroup.Description = editDescription;
					context.SaveChanges();
				}
				catch (Exception ex)
				{
					return new TrySQLCommand(false, ex.Message);
				}
			}

			return new TrySQLCommand(true, "");
		}
		public static TrySQLCommand RemoveOperationGroup(int groupId)
		{
			using (EntityDb context = new EntityDb())
			{
				try
				{
					OperationGroup operationGroup = context.OperationGroups.FirstOrDefault(f => f.Id == groupId);
					if (operationGroup.OperationTypes == null || operationGroup.OperationTypes.Count == 0)
					{
						context.OperationGroups.Remove(operationGroup);
						context.SaveChanges();
					}
					else
					{
						return new TrySQLCommand(false, "Ошибка удаления группы. Группа не пустая.");
					}
				}
				catch (Exception ex)
				{
					return new TrySQLCommand(false, ex.Message);
				}
			}

			return new TrySQLCommand(true, "");
		}

		public static TrySQLCommand CreateUser(string userLogin, string hwId, string userName = null)
		{
			try
			{
				using (EntityDb context = new EntityDb())
				{
					//Заплатка для проставления HwId
					if (context.Users.Any(a => a.UserLogin == userLogin && string.IsNullOrEmpty(a.HwId)))
					{
						User exUser = context.Users.First(f => f.UserLogin == userLogin);
						exUser.HwId = hwId;
						context.SaveChanges();
						return new TrySQLCommand(true, "");
					}

					if (context.Users.Any(a => a.UserLogin == userLogin && a.HwId == hwId))
					{
						return new TrySQLCommand(true, "");
					}

					User user = new User
					{
						UserLogin = userLogin,
						UserName = userName,
						HwId = hwId
					};

					context.Users.Add(user);
					context.SaveChanges();
				}

				return new TrySQLCommand(true, "");
			}
			catch (Exception ex)
			{
				return new TrySQLCommand(false, ex.ToString());
			}
		}

		public static TrySQLCommand CreateOperationGroup(string groupName, string description)
		{
			using (EntityDb context = new EntityDb())
			{
				try
				{
					OperationGroup operationGroup = new OperationGroup
					{
						Name = groupName,
						Description = description
					};

					context.OperationGroups.Add(operationGroup);
					context.SaveChanges();
				}
				catch (Exception ex)
				{
					return new TrySQLCommand(false, ex.Message);
				}
			}

			return new TrySQLCommand(true, "");
		}

		public static TrySQLCommand MoveOperationToGroup(int operTypeId, int groupId)
		{
			using (EntityDb context = new EntityDb())
			{
				var operationType = context.OperationTypes.FirstOrDefault(f => f.Id == operTypeId);
				var operationGroup = context.OperationGroups.FirstOrDefault(f => f.Id == groupId);
				if (operationType != null && operationGroup != null)
				{
					try
					{
						operationType.OperationGroup = operationGroup;
						context.SaveChanges();
					}
					catch (Exception ex)
					{
						return new TrySQLCommand(false, ex.Message);
					}
				}
			}

			return new TrySQLCommand(true, "");
		}
		public static TrySQLCommand SetShopSettings(int computerId, ShopSettingsModel shopSettingsModel)
		{
			using (EntityDb context = new EntityDb())
			{
				var shop = context.ShopComputers.Include(x => x.Shop).FirstOrDefault(w => w.ComputerId == computerId).Shop;
				try
				{
					shop.ShopName = shopSettingsModel.ShopName;
					shop.ShopRegion = shopSettingsModel.ShopRegion;
					shop.AddressToConnect = shopSettingsModel.AddressToConnect;
					shop.Email = shopSettingsModel.Email;
					shop.Phone = shopSettingsModel.Phone;
					shop.IsClosed = shopSettingsModel.IsClosed;
					context.SaveChanges();
				}
				catch (Exception ex)
				{
					return new TrySQLCommand(false, ex.Message);
				}
			}

			return new TrySQLCommand(true, "");
		}
		public static TrySQLCommand TryFillChildStepsId(int stepId, out int positiveStepId, out int negativeStepId)
		{
			using (EntityDb context = new EntityDb())
			{
				try
				{
					var step = context.ShedulerSteps.FirstOrDefault(x => x.Id == stepId);
					positiveStepId = step.OnOperationCompleteStep;
					negativeStepId = step.OnOperationErrorStep;

					return new TrySQLCommand(true, null);
				}
				catch (Exception ex)
				{
					positiveStepId = 0;
					negativeStepId = 0;
					return new TrySQLCommand(false, $"{ex.Message}{Environment.NewLine}{ex.InnerException.ToString()}");
				}
			}
		}

		public static TrySQLCommand TrySaveOperationAttributes(int operTypeId, object model)
		{
			string jsonModel = NewtonJson.GetJsonFromModel(model);

			if (string.IsNullOrEmpty(jsonModel))
			{
				return new TrySQLCommand(false, "Ошибка формирования json из модели");
			}

			using (EntityDb context = new EntityDb())
			{
				try
				{
					OperationType operationType = context.OperationTypes.FirstOrDefault(w => w.Id == operTypeId);
					if (operationType == null)
					{
						return new TrySQLCommand(false, "Невозможно найти тип операции в таблице \"OperationTypes\"");
					}

					operationType.SavedAttributes = jsonModel;

					context.SaveChanges();

					return new TrySQLCommand(true, null);
				}
				catch (Exception ex)
				{
					return new TrySQLCommand(false, ex.Message);
				}
			}
		}

		public static TrySQLCommand TrySaveOperationAttributesByUser(int operTypeId, int userId, object model)
		{
			string jsonModel = NewtonJson.GetJsonFromModel(model);

			if (string.IsNullOrEmpty(jsonModel))
			{
				return new TrySQLCommand(false, "Ошибка формирования json из модели");
			}

			using (EntityDb context = new EntityDb())
			{
				try
				{
					//OperationType operationType = context.OperationTypes.FirstOrDefault(w => w.Id == operTypeId);
					OperationTypeAttribute operationTypeAttribute = context.OperationTypeAttributes.FirstOrDefault(w => w.OperationTypeId == operTypeId && w.UserId == userId);
					if (operationTypeAttribute == null)
					{
						operationTypeAttribute = new OperationTypeAttribute
						{
							OperationTypeId = operTypeId,
							UserId = userId,
							Attributes = jsonModel
						};

						context.OperationTypeAttributes.Add(operationTypeAttribute);
						//return new TrySQLCommand(false, "Невозможно атрибут для операции в таблице \"OperationTypeAttributes\"");
					}
					else
					{
						operationTypeAttribute.Attributes = jsonModel;
					}

					context.SaveChanges();

					return new TrySQLCommand(true, null);
				}
				catch (Exception ex)
				{
					return new TrySQLCommand(false, ex.Message);
				}
			}
		}

		public static TrySQLCommand TryInsertShedulerHistory(int taskId, int stepId, int computerId, bool isComplete, string message)
		{
			lock (lockTryInsertShedulerHistory)
			{
				using (EntityDb context = new EntityDb())
				{
					try
					{
						var task = SelectShedulerTasks().First(x => x.Id == taskId);
						var step = SelectShedulerStep(stepId);
						var his = new ShedulerTaskHistory
						{
							Date = DateTime.UtcNow,
							IsComplete = isComplete,
							Message = message,
							ShedulerStep = null,
							ShedulerTask = null,
							TaskId = taskId,
							StepId = stepId,
							ComputerId = computerId
						};

						context.ShedulerTaskHistories.Add(his);

						context.SaveChanges();

						return new TrySQLCommand(true, null);
					}
					catch (Exception ex)
					{
						return new TrySQLCommand(false, ex.Message);
					}
				}
			}
		}

		public static TrySQLCommand TryUpdateLastStartTime(int taskId, DateTime dateTime)
		{
			lock (lockTryUpdateLastStartTime)
			{
				using (EntityDb context = new EntityDb())
				{

					try
					{
						ShedulerTask sqlTask = context.ShedulerTasks.First(x => x.Id == taskId);
						sqlTask.LastStartTime = dateTime;
						context.SaveChanges();

						return new TrySQLCommand(true, null);
					}
					catch (Exception ex)
					{
						return new TrySQLCommand(false, ex.Message);
					}
				}
			}
		}

		public static TrySQLCommand TryUpdatePluginTaskLastStartTime(int taskId, DateTime dateTime)
		{
			lock (lockTryUpdateLastStartTime)
			{
				using (EntityDb context = new EntityDb())
				{

					try
					{
						ShedulerPluginTask sqlTask = context.ShedulerPluginTasks.First(x => x.Id == taskId);
						sqlTask.LastStartTime = dateTime;
						context.SaveChanges();

						return new TrySQLCommand(true, null);
					}
					catch (Exception ex)
					{
						return new TrySQLCommand(false, ex.Message);
					}
				}
			}
		}

		public static TrySQLCommand TryUpdateLastEndTime(int taskId, DateTime dateTime)
		{
			lock (lockTryUpdateLastEndTime)
			{
				using (EntityDb context = new EntityDb())
				{
					try
					{
						ShedulerTask sqlTask = context.ShedulerTasks.First(x => x.Id == taskId);
						sqlTask.LastEndTime = dateTime;
						context.SaveChanges();

						return new TrySQLCommand(true, null);
					}
					catch (Exception ex)
					{
						return new TrySQLCommand(false, ex.Message);
					}
				}
			}
		}

		public static TrySQLCommand TryUpdatePluginTaskLastEndTime(int taskId, DateTime dateTime)
		{
			lock (lockTryUpdateLastEndTime)
			{
				using (EntityDb context = new EntityDb())
				{
					try
					{
						ShedulerPluginTask sqlTask = context.ShedulerPluginTasks.First(x => x.Id == taskId);
						sqlTask.LastEndTime = dateTime;
						context.SaveChanges();

						return new TrySQLCommand(true, null);
					}
					catch (Exception ex)
					{
						return new TrySQLCommand(false, ex.Message);
					}
				}
			}
		}

		public static TrySQLCommand TryUpdateNextStartTime(int taskId, DateTime dateTime)
		{
			lock (lockTryUpdateNextStartTime)
			{
				using (EntityDb context = new EntityDb())
				{
					try
					{
						ShedulerTask sqlTask = context.ShedulerTasks.First(x => x.Id == taskId);
						sqlTask.NextStartTime = dateTime;
						context.SaveChanges();

						return new TrySQLCommand(true, null);
					}
					catch (Exception ex)
					{
						return new TrySQLCommand(false, ex.Message);
					}
				}
			}
		}
		public static TrySQLCommand TryUpdatePluginTaskNextStartTime(int taskId, DateTime dateTime)
		{
			lock (lockTryUpdateNextStartTime)
			{
				using (EntityDb context = new EntityDb())
				{
					try
					{
						ShedulerPluginTask sqlTask = context.ShedulerPluginTasks.First(x => x.Id == taskId);
						sqlTask.NextStartTime = dateTime;
						context.SaveChanges();

						return new TrySQLCommand(true, null);
					}
					catch (Exception ex)
					{
						return new TrySQLCommand(false, ex.Message);
					}
				}
			}
		}


		public static TrySQLCommand TrySetTaskEnabled(int taskId, bool enabled)
		{
			using (EntityDb context = new EntityDb())
			{
				try
				{
					ShedulerTask sqlTask = context.ShedulerTasks.First(x => x.Id == taskId);
					sqlTask.Enabled = enabled;
					context.SaveChanges();

					return new TrySQLCommand(true, null);
				}
				catch (Exception ex)
				{
					return new TrySQLCommand(false, ex.Message);
				}
			}
		}

		public static TrySQLCommand TrySetPluginTaskEnabled(int taskId, bool enabled)
		{
			using (EntityDb context = new EntityDb())
			{
				try
				{
					ShedulerPluginTask sqlTask = context.ShedulerPluginTasks.First(x => x.Id == taskId);
					sqlTask.Enabled = enabled;
					context.SaveChanges();

					return new TrySQLCommand(true, null);
				}
				catch (Exception ex)
				{
					return new TrySQLCommand(false, ex.Message);
				}
			}
		}

		public static TrySQLCommand TryClearSteps(int taskId)
		{
			using (EntityDb context = new EntityDb())
			{
				try
				{
					ShedulerTask sqlTask = context.ShedulerTasks.First(x => x.Id == taskId);
					sqlTask.FirstStepId = null;
					context.SaveChanges();

					AddShedulerTaskUserHistory(sqlTask, ShedulerUserHistoryAction.Update);

					var steps = context.ShedulerSteps.Where(x => x.TaskId == taskId);
					context.ShedulerSteps.RemoveRange(steps);
					context.SaveChanges();

					AddShedulerStepUserHistory(steps.ToArray(), ShedulerUserHistoryAction.Delete);

					return new TrySQLCommand(true, null);
				}
				catch (Exception ex)
				{
					return new TrySQLCommand(false, ex.Message);
				}
			}
		}

		public static TrySQLCommand TryDeleteSteps(IEnumerable<int> stepsId)
		{
			try
			{
				if (stepsId.Count() == 0)
				{
					return new TrySQLCommand(true, "");
				}

				using (EntityDb context = new EntityDb())
				{
					ShedulerStep[] shedulerSteps = context.ShedulerSteps.Where(w => stepsId.Contains(w.Id)).ToArray();

					context.ShedulerSteps.RemoveRange(shedulerSteps);

					foreach (var deletedStep in shedulerSteps)
					{
						ShedulerStep[] otherSteps = context.ShedulerSteps.Where(w => w.TaskId == deletedStep.TaskId).ToArray();

						foreach (var otherStep in otherSteps)
						{
							if (otherStep.OnOperationCompleteStep == deletedStep.Id)
							{
								otherStep.OnOperationCompleteStep = 0;
							}

							if (otherStep.OnOperationErrorStep == deletedStep.Id)
							{
								otherStep.OnOperationErrorStep = 0;
							}
						}
					}

					context.SaveChanges();

					AddShedulerStepUserHistory(shedulerSteps, ShedulerUserHistoryAction.Update);

					return new TrySQLCommand(true, null);
				}
			}
			catch (Exception ex)
			{
				return new TrySQLCommand(false, ex.Message);
			}
		}

		public static TrySQLCommand TryUpdateStepPositiveOperationId(int stepId, int positiveStepId)
		{
			using (EntityDb context = new EntityDb())
			{
				try
				{
					ShedulerStep sqlStep = context.ShedulerSteps.First(x => x.Id == stepId);

					sqlStep.OnOperationCompleteStep = positiveStepId;

					context.SaveChanges();

					AddShedulerStepUserHistory(sqlStep, ShedulerUserHistoryAction.Update);

					return new TrySQLCommand(true, null);
				}
				catch (Exception ex)
				{
					return new TrySQLCommand(false, ex.Message);
				}
			}
		}

		public static TrySQLCommand TryUpdateStepNegativeOperationId(int stepId, int negativeStepId)
		{
			using (EntityDb context = new EntityDb())
			{
				try
				{
					ShedulerStep sqlStep = context.ShedulerSteps.First(x => x.Id == stepId);

					sqlStep.OnOperationErrorStep = negativeStepId;

					context.SaveChanges();

					AddShedulerStepUserHistory(sqlStep, ShedulerUserHistoryAction.Update);

					return new TrySQLCommand(true, null);
				}
				catch (Exception ex)
				{
					return new TrySQLCommand(false, ex.Message);
				}
			}
		}

		public static TrySQLCommand TryUpdateStepFirstStepId(int taskId, int firstStepId)
		{
			using (EntityDb context = new EntityDb())
			{
				try
				{
					ShedulerTask sqlTask = context.ShedulerTasks.First(x => x.Id == taskId);

					//sqlTask.ShedulerTasksComputersLists.Clear();//311311
					sqlTask.FirstStepId = firstStepId;
					context.SaveChanges();

					AddShedulerTaskUserHistory(sqlTask, ShedulerUserHistoryAction.Update);

					return new TrySQLCommand(true, null);
				}
				catch (Exception ex)
				{
					return new TrySQLCommand(false, ex.Message);
				}
			}
		}

		public static TrySQLCommand TryUpdateTask(int taskId,
										   bool isEnabled,
										   string name,
										   int? firstStepId,
										   int mode,
										   DateTime startTime,
										   ICollection<ShedulerTasksComputersList> computersList)
		{
			using (EntityDb context = new EntityDb())
			{
				try
				{
					ShedulerTask sqlTask = context.ShedulerTasks.First(x => x.Id == taskId);

					sqlTask.ShedulerTasksComputersLists.Clear();
					context.SaveChanges();

					sqlTask.Enabled = isEnabled;
					sqlTask.Name = name;
					sqlTask.FirstStepId = firstStepId;
					sqlTask.Mode = mode;
					sqlTask.StartTime = startTime;
					sqlTask.NextStartTime = startTime;
					sqlTask.ShedulerTasksComputersLists = computersList;
					context.SaveChanges();

					AddShedulerTaskUserHistory(sqlTask, ShedulerUserHistoryAction.Update);

					return new TrySQLCommand(true, null);
				}
				catch (Exception ex)
				{
					return new TrySQLCommand(false, ex.Message);
				}
			}
		}

		public static TrySQLCommand TryInsertNewTask(string name,
											  int mode,
											  DateTime startTime,
											  ICollection<ShedulerTasksComputersList> computersList,
											  bool enabled)
		{
			lock (lockTryInsertNewTask)
			{
				using (EntityDb context = new EntityDb())
				{
					try
					{
						var schedulerTaskToAdd =
							new ShedulerTask
							{
								Name = name,
								StartTime = startTime,
								NextStartTime = startTime,
								LastStartTime = Constants.DEFAULT_DATE_TIME,
								LastEndTime = Constants.DEFAULT_DATE_TIME,
								ShedulerTasksComputersLists = computersList,
								Enabled = enabled
							};

						context.ShedulerTasks.Add(schedulerTaskToAdd);

						context.SaveChanges();

						AddShedulerTaskUserHistory(schedulerTaskToAdd, ShedulerUserHistoryAction.Insert);

						return new TrySQLCommand(true, null, schedulerTaskToAdd.Id);
					}
					catch (Exception ex)
					{
						return new TrySQLCommand(false, ex.Message);
					}
				}
			}
		}

		public static TrySQLCommand TryDeleteTask(int taskId)
		{
			lock (lockTryDeleteTask)
			{
				using (EntityDb context = new EntityDb())
				{
					using (var dbContextTransaction = context.Database.BeginTransaction())
					{
						try
						{
							var taskHistory = context.ShedulerTaskHistories.Where(x => x.TaskId == taskId);
							context.ShedulerTaskHistories.RemoveRange(taskHistory);
							context.SaveChanges();

							var task = context.ShedulerTasks.First(x => x.Id == taskId);
							context.ShedulerTasks.Remove(task);
							context.SaveChanges();

							AddShedulerTaskUserHistory(task, ShedulerUserHistoryAction.Delete, context);

							var steps = context.ShedulerSteps.Where(x => x.TaskId == taskId);
							context.ShedulerSteps.RemoveRange(steps);
							context.SaveChanges();

							foreach (var item in steps)
							{
								AddShedulerStepUserHistory(item, ShedulerUserHistoryAction.Delete, context);
							}

							var computers = context.ShedulerTasksComputersLists.Where(x => x.TaskId == taskId);
							context.ShedulerTasksComputersLists.RemoveRange(computers);
							context.SaveChanges();
							dbContextTransaction.Commit();
							return new TrySQLCommand(true, null);
						}
						catch (Exception ex)
						{
							return new TrySQLCommand(false, $"{ex.Message}{Environment.NewLine}{ex.InnerException.ToString()}");
						}
					}
				}
			}
		}

		public static TrySQLCommand TryDeletePluginTask(int pluginTaskId)
		{
			lock (lockTryDeletePluginTask)
			{
				using (EntityDb context = new EntityDb())
				{
					using (var dbContextTransaction = context.Database.BeginTransaction())
					{
						try
						{
							var pluginTask = context.ShedulerPluginTasks.First(x => x.Id == pluginTaskId);
							context.ShedulerPluginTasks.Remove(pluginTask);
							context.SaveChanges();

							AddShedulerPluginTaskUserHistory(pluginTask, ShedulerUserHistoryAction.Delete, context);

							dbContextTransaction.Commit();
							return new TrySQLCommand(true, null);
						}
						catch (Exception ex)
						{
							return new TrySQLCommand(false, $"{ex.Message}{Environment.NewLine}{ex.InnerException.ToString()}");
						}
					}
				}
			}
		}

		public static List<ViewShedulerStepsModelParentChild> GetAllStepsParentChild(int taskId)
		{
			lock (lockGetAllStepsParentChild)
			{
				var allSteps = GetAllSteps(taskId);
				var stepsResult = new List<ViewShedulerStepsModelParentChild>();
				using (EntityDb context = new EntityDb())
				{
					var steps = context.ViewShedulerSteps.Where(x => x.taskId == taskId);
					foreach (var step in steps)
					{
						stepsResult.Add(new ViewShedulerStepsModelParentChild
						{
							ChildId = step.ChildId,
							ParentId = step.ParentId,
							StepData = allSteps.FirstOrDefault(x => x.Id == step.ChildId),
							IsChildPositiveBranch = (step.IsChildPositiveBranch == 1) ? true : false
						});
					}
				}
				return stepsResult;
			}
		}

		private static ShedulerStep GetStepById(int stepId)
		{
			lock (lockGetStepById)
			{
				using (EntityDb context = new EntityDb())
				{
					return context.ShedulerSteps.FirstOrDefault(x => x.Id == stepId);
				}
			}
		}


		/*
        public List<ShedulerStep> GetAllStepsParentChild(int taskId)
        {
            using (EntityDb context = new EntityDb())
            {
                context.ViewShedulerSteps.Join(x)
                var steps = context.ViewShedulerSteps.Where(x => x.taskId == taskId);
                //return steps.ToDictionary(mc => mc.ParentId,
                //                 mc => mc.ChildId);
            }
        }
        */

		public static List<ShedulerStep> GetAllSteps(int taskId)
		{
			lock (lockGetAllSteps)
			{
				var result = new List<ShedulerStep>();

				using (EntityDb context = new EntityDb())
				{
					var steps = context.ShedulerSteps.Where(x => x.TaskId == taskId);
					return steps.ToList<ShedulerStep>();
				}
			}
		}

		public static TrySQLCommand TryInsertNewStep(int taskId,
											  int operationType,
											  int repeatCount,
											  int repeatTimeout,
											  int operationCheckIntervalMs)
		{
			lock (lockTryInsertNewStep)
			{
				using (EntityDb context = new EntityDb())
				{
					try
					{
						var step = new ShedulerStep
						{
							TaskId = taskId,
							OperationType = operationType,
							RepeatCount = repeatCount,
							RepeatTimeout = repeatTimeout,
							OperationCheckIntervalMs = operationCheckIntervalMs
						};

						context.ShedulerSteps.Add(step);

						context.SaveChanges();

						AddShedulerStepUserHistory(step, ShedulerUserHistoryAction.Insert);

						return new TrySQLCommand(true, null, step.Id);
					}
					catch (Exception ex)
					{
						return new TrySQLCommand(false, ex.Message);
					}
				}
			}
		}

		public static TrySQLCommand TryUpdateStep(int stepId,
											  int operationType,
											  int repeatCount,
											  int repeatTimeout,
											  int operationCheckIntervalMs)
		{
			lock (lockTryUpdateStep)
			{
				using (EntityDb context = new EntityDb())
				{
					try
					{
						var step = context.ShedulerSteps.First(w => w.Id == stepId);
						step.OperationType = operationType;
						step.RepeatCount = repeatCount;
						step.RepeatTimeout = repeatTimeout;
						step.OperationCheckIntervalMs = operationCheckIntervalMs;

						context.SaveChanges();

						AddShedulerStepUserHistory(step, ShedulerUserHistoryAction.Update);

						return new TrySQLCommand(true, null, step.Id);
					}
					catch (Exception ex)
					{
						return new TrySQLCommand(false, ex.Message);
					}
				}
			}
		}

		public static TrySQLCommand TryUpdateStep(ShedulerStep shedulerStep)
		{
			lock (lockTryUpdateStep)
			{
				if (shedulerStep.Id <= 0)
				{
					return new TrySQLCommand(true, null, shedulerStep.Id);
				}

				using (EntityDb context = new EntityDb())
				{
					try
					{
						var step = context.ShedulerSteps.First(w => w.Id == shedulerStep.Id);

						step.TaskId = shedulerStep.TaskId;
						step.OperationType = shedulerStep.OperationType;
						step.RepeatCount = shedulerStep.RepeatCount;
						step.RepeatTimeout = shedulerStep.RepeatTimeout;
						step.OperationCheckIntervalMs = shedulerStep.OperationCheckIntervalMs;
						step.OnOperationCompleteStep = shedulerStep.OnOperationCompleteStep;
						step.OnOperationErrorStep = shedulerStep.OnOperationErrorStep;
						step.OrderId = shedulerStep.OrderId;
						step.OperationAttributes = shedulerStep.OperationAttributes;

						context.SaveChanges();

						AddShedulerStepUserHistory(step, ShedulerUserHistoryAction.Update);

						return new TrySQLCommand(true, null, step.Id);
					}
					catch (Exception ex)
					{
						return new TrySQLCommand(false, ex.Message);
					}
				}
			}
		}

		public static TrySQLCommand TryInsertNewOperationFromStep(ShedulerStep step, int computerId, int operationType)
		{
			lock (lockTryInsertNewOperationFromStep)
			{
				using (EntityDb context = new EntityDb())
				{
					try
					{
						var operation = new Operation
						{
							ComputerId = computerId,
							OperationType = operationType,
							CreationDate = DateTime.UtcNow,
							CreatedUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
							IsFromSheduler = true
						};

						context.Operations.Add(operation);

						context.SaveChanges();

						return new TrySQLCommand(true, null, operation.ID);
					}
					catch (Exception ex)
					{
						return new TrySQLCommand(false, ex.Message);
					}
				}
			}
		}

		public static async Task<TrySQLCommand> TryInsertNewOperationAsync(int computerId, int operationType, DateTime? poolDate)
		{
			using (EntityDb context = new EntityDb())
			{
				try
				{
					Operation operation;
					OperationType operationTypeDb = context.OperationTypes.FirstOrDefault(f => f.Id == operationType);
					if (operationTypeDb != null && !string.IsNullOrEmpty(operationTypeDb.SavedAttributes))
					{
						operation = new Operation
						{
							ComputerId = computerId,
							OperationType = operationType,
							CreationDate = DateTime.UtcNow,
							PoolDate = poolDate,
							CreatedUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
							Attributes = operationTypeDb.SavedAttributes
						};
						context.Operations.Add(operation);
					}
					else
					{
						operation = new Operation
						{
							ComputerId = computerId,
							OperationType = operationType,
							CreationDate = DateTime.UtcNow,
							PoolDate = poolDate,
							CreatedUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name
						};
						context.Operations.Add(operation);
					}

					await context.SaveChangesAsync();

					return new TrySQLCommand(true, null);
				}
				catch (Exception ex)
				{
					return new TrySQLCommand(false, ex.Message);
				}
			}
		}

		public static async Task<TrySQLCommand> TryInsertNewOperationByUserAsync(int computerId, int operationType, int userId, DateTime? poolDate)
		{
			using (EntityDb context = new EntityDb())
			{
				try
				{
					OperationTypeAttribute operationTypeAttribute = context.OperationTypeAttributes.FirstOrDefault(f => f.OperationTypeId == operationType && f.UserId == userId);
					OperationType operationTypeDb = context.OperationTypes.FirstOrDefault(f => f.Id == operationType);
					if (operationTypeAttribute != null && !string.IsNullOrEmpty(operationTypeAttribute.Attributes))
					{
						context.Operations.Add(new Operation
						{
							ComputerId = computerId,
							OperationType = operationType,
							CreationDate = DateTime.UtcNow,
							PoolDate = poolDate,
							CreatedUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
							Attributes = operationTypeAttribute.Attributes
						});
					}
					else
					if (operationTypeDb != null && !string.IsNullOrEmpty(operationTypeDb.SavedAttributes))
					{
						context.Operations.Add(new Operation
						{
							ComputerId = computerId,
							OperationType = operationType,
							CreationDate = DateTime.UtcNow,
							PoolDate = poolDate,
							CreatedUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
							Attributes = operationTypeDb.SavedAttributes
						});
					}
					else
					{
						context.Operations.Add(new Operation
						{
							ComputerId = computerId,
							OperationType = operationType,
							CreationDate = DateTime.UtcNow,
							PoolDate = poolDate,
							CreatedUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name
						});
					}

					await context.SaveChangesAsync();

					return new TrySQLCommand(true, null);
				}
				catch (Exception ex)
				{
					return new TrySQLCommand(false, ex.Message);
				}
			}
		}

		public static TrySQLCommand TryInsertNewOperationByUser(int computerId, int operationType, int userId, DateTime? poolDate)
		{
			using (EntityDb context = new EntityDb())
			{
				try
				{
					OperationTypeAttribute operationTypeAttribute = context.OperationTypeAttributes.FirstOrDefault(f => f.OperationTypeId == operationType && f.UserId == userId);
					OperationType operationTypeDb = context.OperationTypes.FirstOrDefault(f => f.Id == operationType);
					if (operationTypeAttribute != null && !string.IsNullOrEmpty(operationTypeAttribute.Attributes))
					{
						context.Operations.Add(new Operation
						{
							ComputerId = computerId,
							OperationType = operationType,
							CreationDate = DateTime.UtcNow,
							PoolDate = poolDate,
							CreatedUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
							Attributes = operationTypeAttribute.Attributes
						});
					}
					else
					if (operationTypeDb != null && !string.IsNullOrEmpty(operationTypeDb.SavedAttributes))
					{
						context.Operations.Add(new Operation
						{
							ComputerId = computerId,
							OperationType = operationType,
							CreationDate = DateTime.UtcNow,
							PoolDate = poolDate,
							CreatedUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
							Attributes = operationTypeDb.SavedAttributes
						});
					}
					else
					{
						context.Operations.Add(new Operation
						{
							ComputerId = computerId,
							OperationType = operationType,
							CreationDate = DateTime.UtcNow,
							PoolDate = poolDate,
							CreatedUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name
						});
					}

					context.SaveChanges();

					return new TrySQLCommand(true, null);
				}
				catch (Exception ex)
				{
					return new TrySQLCommand(false, ex.Message);
				}
			}
		}

		public static TrySQLCommand TryInsertNewOperationByShedulerStep(int computerId, int operationType, string attributes)
		{
			lock (lockTryInsertNewOperationByShedulerStep)
			{
				using (EntityDb context = new EntityDb())
				{
					try
					{
						Operation operation;
						OperationType operationTypeDb = context.OperationTypes.FirstOrDefault(f => f.Id == operationType);
						if (!string.IsNullOrEmpty(attributes))
						{
							operation = new Operation
							{
								ComputerId = computerId,
								OperationType = operationType,
								CreationDate = DateTime.UtcNow,
								CreatedUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
								Attributes = attributes,
								IsFromSheduler = true
							};
							context.Operations.Add(operation);
						}
						else
						if (operationTypeDb != null && !string.IsNullOrEmpty(operationTypeDb.SavedAttributes))
						{
							operation = new Operation
							{
								ComputerId = computerId,
								OperationType = operationType,
								CreationDate = DateTime.UtcNow,
								CreatedUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
								Attributes = operationTypeDb.SavedAttributes,
								IsFromSheduler = true
							};
							context.Operations.Add(operation);
						}
						else
						{
							operation = new Operation
							{
								ComputerId = computerId,
								OperationType = operationType,
								CreationDate = DateTime.UtcNow,
								CreatedUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
								IsFromSheduler = true
							};
							context.Operations.Add(operation);
						}

						context.SaveChanges();

						return new TrySQLCommand(true, null, operation.ID);
					}
					catch (Exception ex)
					{
						return new TrySQLCommand(false, ex.Message);
					}
				}
			}
		}

		public static async Task<TrySQLCommand> TryInsertNewShopAsync(string shopId, string shopRegion)
		{
			using (EntityDb context = new EntityDb())
			{
				if (context.Shops.Any(o => string.Compare(o.ShopId.Trim(), shopId.Trim(), true) == 0))
				{
					return new TrySQLCommand(false, $"Магазин с ShopId = {shopId.Trim()} уже существует");
				}

				try
				{
					context.Shops.Add(new Shop
					{
						ShopId = shopId.Trim(),
						ShopName = shopId.Trim(),
						ShopRegion = shopRegion.Trim()
					});

					await context.SaveChangesAsync();

					return new TrySQLCommand(true, null);
				}
				catch (Exception ex)
				{
					return new TrySQLCommand(false, ex.Message);
				}
			}
		}
		#endregion Commands

		#region Queries


		public static int GetMonitorUsersRealCount()
		{
			lock (lockGetMonitorUsersRealCount)
			{
				using (EntityDb context = new EntityDb())
				{
					return context.Users.Count();
				}
			}
		}

		public static int GetAgentUsersRealCount()
		{
			lock (lockGetAgentUsersRealCount)
			{
				using (EntityDb context = new EntityDb())
				{
					return context.LicAgents.Count();
				}
			}
		}

		public static TrySQLQuery CheckMonitorLic(int userId, string hwId)
		{
			CommonGlobalSettings commonGlobalSettings = new CommonGlobalSettings();
			commonGlobalSettings.LoadCommonGlobalSettings();
			int licMonitorCount;
			if (!int.TryParse(commonGlobalSettings.LicMonitorCount, out licMonitorCount))
			{
				return new TrySQLQuery(false, "Ошибка опции [LicMonitorCount]");
			}

			if (GetMonitorUsersRealCount() > licMonitorCount)
			{
				return new TrySQLQuery(false, "Количество пользователей монитора больше чем допустимо по лицензии");
			}

			using (EntityDb context = new EntityDb())
			{
				string licId = LicRequest.GetRequest(hwId, licMonitorCount);
				var licResult = context.Users.FirstOrDefault(f => f.Id == userId && f.HwId == hwId && f.LicId == licId);
				if (licResult != null)
				{
					return new TrySQLQuery();
				}
				else
				{
					return new TrySQLQuery(false, "Ошибка проверки лицензии");
				}
			}
		}

		public static bool CheckTaskExistsByName(string name, int[] excludedTasks = null)
		{
			using (EntityDb context = new EntityDb())
			{
				ShedulerTask task;
				if (excludedTasks == null)
				{
					task = context.ShedulerTasks.FirstOrDefault(f => f.Name.Trim().ToUpper() == name.Trim().ToUpper());
				}
				else
				{
					task = context.ShedulerTasks.Where(w => !excludedTasks.Contains(w.Id)).FirstOrDefault(f => f.Name.Trim().ToUpper() == name.Trim().ToUpper());
				}
				return task != null;
			}
		}

		public static string GetAgentLastCriticalError(int computerId)
		{
			using (EntityDb context = new EntityDb())
			{
				var comp = context.ShopComputers.First(f => f.ComputerId == computerId);
				return $"{comp.LastErrorDate}: {comp.LastError}";
			}
		}

		public static int GetUserId(string userLogin, string hwId)
		{
			User user = null;
			using (EntityDb context = new EntityDb())
			{
				user = context.Users.FirstOrDefault(f => f.UserLogin == userLogin && f.HwId == hwId);
			}

			return user == null ? 0 : user.Id;
		}

		public static ShopSettingsModel GetShopSettings(int computerId)
		{
			ShopSettingsModel result = null;
			using (EntityDb context = new EntityDb())
			{
				var shop = context.ShopComputers.Include(x => x.Shop).FirstOrDefault(w => w.ComputerId == computerId).Shop;
				result = new ShopSettingsModel
				{
					ShopName = shop.ShopName,
					ShopRegion = shop.ShopRegion,
					AddressToConnect = shop.AddressToConnect,
					Email = shop.Email,
					Phone = shop.Phone,
					IsClosed = shop.IsClosed ?? false
				};
			}

			return result;
		}

		public static bool GetIsAgentSettingsReadedValue(int computerId)
		{
			using (EntityDb context = new EntityDb())
			{
				return context.ShopComputers.FirstOrDefault(w => w.ComputerId == computerId).IsAgentSettingsReaded ?? false;
			}
		}

		public static string GetShopId(int computerId)
		{
			using (EntityDb context = new EntityDb())
			{
				try
				{
					return context.ShopComputers.FirstOrDefault(f => f.ComputerId == computerId).ShopId;
				}
				catch (Exception ex)
				{
					return null;
				}
			}
		}

		public static async Task<List<DateTime>> GetLast_10_OperationPoolDates()
		{
			var list = new List<DateTime>();
			using (EntityDb context = new EntityDb())
			{
				try
				{
					DateTime?[] dt = await context.Operations
						.GroupBy(g => g.PoolDate, s => s.PoolDate)
						.OrderByDescending(o => o.Key)
						.Select(s => s.Key)
						.Take(10)
						.ToArrayAsync();
					foreach (var item in dt)
					{
						if (item.HasValue)
						{
							list.Add(item.Value.Add(TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now)));
						}
					}
				}
				catch (Exception ex)
				{
					return list;
				}

				return list;
			}
		}

		public static object GetSavedOperationAttributes(int operTypeId)
		{
			using (EntityDb context = new EntityDb())
			{
				try
				{
					OperationType operationType = context.OperationTypes.FirstOrDefault(w => w.Id == operTypeId);
					if (operationType == null)
					{
						//return new TrySQLCommand(false, "Невозможно найти тип операции в таблице \"OperationTypes\"");
					}

					return NewtonJson.GetModelFromJson(operationType.SavedAttributes);
				}
				catch (Exception ex)
				{
					return null;
				}
			}
		}

		public static object GetSavedOperationAttributesByUser(int operTypeId, int userId)
		{
			using (EntityDb context = new EntityDb())
			{
				try
				{
					//OperationType operationType = context.OperationTypes.FirstOrDefault(w => w.Id == operTypeId);;
					OperationTypeAttribute operationTypeAttribute = context.OperationTypeAttributes.FirstOrDefault(w => w.OperationTypeId == operTypeId && w.UserId == userId);
					string attributes = null;
					if (operationTypeAttribute != null && !string.IsNullOrEmpty(operationTypeAttribute.Attributes))
					{
						attributes = operationTypeAttribute.Attributes;
					}
					else
					{
						OperationType operationType = context.OperationTypes.FirstOrDefault(w => w.Id == operTypeId);
						if (operationType != null && !string.IsNullOrEmpty(operationType.SavedAttributes))
						{
							attributes = operationType.SavedAttributes;
						}
						else
						{
							return null;
						}
					}

					return NewtonJson.GetModelFromJson(attributes);
				}
				catch (Exception ex)
				{
					return null;
				}
			}
		}

		public static string GetOperationNameRusById(int id)
		{
			using (EntityDb context = new EntityDb())
			{
				var operType = context.OperationTypes.FirstOrDefault(x => x.Id == id);
				if (operType == null)
				{
					return "";
				}
				return operType.NameRus;
			}
		}

		public static OperationTypeModel[] GetOperationTypes()
		{
			using (EntityDb context = new EntityDb())
			{
				var query = from c in context.OperationTypes
							select new OperationTypeModel
							{
								Id = c.Id,
								Name = c.Name,
								DisplayGridName = string.IsNullOrEmpty(c.NameRus) ? c.Name : c.NameRus,
								NameRus = c.NameRus,
								FileName = c.FileName,
								FileMd5 = c.FileMD5,
								GroupId = c.GroupId,
								Description = c.Description,
								Visible = c.Visible
							};

				return query.ToArray();
			}
		}

		public static async Task<OperationTypeModel[]> GetOperationTypesAsync()
		{
			using (EntityDb context = new EntityDb())
			{
				var query = from c in context.OperationTypes
							where c.Visible ?? true
							select new OperationTypeModel
							{
								Id = c.Id,
								Name = c.Name,
								DisplayGridName = string.IsNullOrEmpty(c.NameRus) ? c.Name : c.NameRus,
								NameRus = c.NameRus,
								FileName = c.FileName,
								FileMd5 = c.FileMD5,
								GroupId = c.GroupId,
								Description = c.Description
							};

				return await query.ToArrayAsync();
			}
		}

		public static CommonGlobalSetting GetSetting(string name)
		{
			using (EntityDb context = new EntityDb())
			{
				return context.CommonGlobalSettings.First(x => x.Name == name);
			}
		}

		public static List<ViewOperation> SelectOperationsByGuid(Guid guid)
		{
			using (EntityDb context = new EntityDb())
			{
				var query = from c in context.ViewOperations
							where c.GroupGUID == guid
							select new ViewOperation();

				return query.ToList();
			}
		}

		public static List<OperationType> SelectAllOperationsTypes()
		{
			using (EntityDb context = new EntityDb())
			{
				return context.OperationTypes.ToList<OperationType>();
			}
		}

		public static ViewOperation SelectOperationById(int id)
		{
			lock (lockSelectOperationById)
			{
				using (EntityDb context = new EntityDb())
				{
					return context.ViewOperations.First(x => x.ID == id);
				}
			}
		}

		public static List<ShedulerTask> SelectShedulerTasks()
		{
			lock (lockSelectShedulerTasks)
			{
				using (EntityDb context = new EntityDb())
				{
					return context.ShedulerTasks.Include(x => x.ShedulerSteps)
								.Include(x => x.ShedulerTaskHistories)
								.Include(x => x.ShedulerTasksComputersLists).ToList();
				}
			}
		}

		public static List<ShedulerTaskMode> SelectShedulerModes()
		{
			using (EntityDb context = new EntityDb())
			{
				return context.ShedulerTaskModes.ToList();
			}
		}

		public static List<ShedulerStatus> SelectShedulerStatuses()
		{
			using (EntityDb context = new EntityDb())
			{
				return context.ShedulerStatuses.ToList();
			}
		}

		public static List<ShedulerPluginTask> SelectShedulerPluginTasks()
		{
			lock (lockSelectShedulerPluginTasks)
			{
				using (EntityDb context = new EntityDb())
				{
					return context.ShedulerPluginTasks.ToList();
				}
			}
		}

		public static List<ShedulerStep> SelectShedulerSteps(int taskId)
		{
			using (EntityDb context = new EntityDb())
			{
				return context.ShedulerSteps
					.Include(x => x.ShedulerTasks)
					.Include(x => x.ShedulerTaskHistories)
					.Where(x => x.TaskId == taskId).ToList();
			}
		}

		public static async Task<ViewShedulerStepModel[]> SelectShedulerStepsForGridAsync(int taskId)
		{
			using (EntityDb context = new EntityDb())
			{
				return await context.ViewShedulerStepsVis.Where(w => w.TaskId == taskId).Select(s => new ViewShedulerStepModel
				{
					Id = s.Id,
					Step = s.Step,
					TaskId = s.TaskId,
					OperationType = s.OperationType,
					Name = s.Name,
					NameRus = s.NameRus,
					nameVis = s.nameVis,
					nameVisCount = s.nameVisCount,
					RepeatCount = s.RepeatCount,
					RepeatTimeout = s.RepeatTimeout,
					OperationCheckIntervalMs = s.OperationCheckIntervalMs,
					OnOperationCompleteStep = s.OnOperationCompleteStep,
					NameCompleteStep = s.NameCompleteStep,
					NameRusCompleteStep = s.NameRusCompleteStep,
					nameVisCompleteStep = s.nameVisCompleteStep,
					nameVisCountCompleteStep = s.nameVisCountCompleteStep,
					OnOperationErrorStep = s.OnOperationErrorStep,
					NameErrorStep = s.NameErrorStep,
					NameRusErrorStep = s.NameRusErrorStep,
					nameVisErrorStep = s.nameVisErrorStep,
					nameVisCountErrorStep = s.nameVisCountErrorStep,
					OrderId = s.OrderId,
					OperationAttributes = s.OperationAttributes
				}).ToArrayAsync();
			}
		}

		public static ViewShedulerStepModel[] SelectShedulerStepsForGrid(int taskId)
		{
			using (EntityDb context = new EntityDb())
			{
				return context.ViewShedulerStepsVis.Where(w => w.TaskId == taskId).Select(s => new ViewShedulerStepModel
				{
					Id = s.Id,
					Step = s.Step,
					TaskId = s.TaskId,
					OperationType = s.OperationType,
					Name = s.Name,
					NameRus = s.NameRus,
					nameVis = s.nameVis,
					nameVisCount = s.nameVisCount,
					RepeatCount = s.RepeatCount,
					RepeatTimeout = s.RepeatTimeout,
					OperationCheckIntervalMs = s.OperationCheckIntervalMs,
					OnOperationCompleteStep = s.OnOperationCompleteStep,
					NameCompleteStep = s.NameCompleteStep,
					NameRusCompleteStep = s.NameRusCompleteStep,
					nameVisCompleteStep = s.nameVisCompleteStep,
					nameVisCountCompleteStep = s.nameVisCountCompleteStep,
					OnOperationErrorStep = s.OnOperationErrorStep,
					NameErrorStep = s.NameErrorStep,
					NameRusErrorStep = s.NameRusErrorStep,
					nameVisErrorStep = s.nameVisErrorStep,
					nameVisCountErrorStep = s.nameVisCountErrorStep,
					OrderId = s.OrderId,
				}).ToArray();
			}
		}

		public static ShedulerStep SelectShedulerStep(int? stepId)
		{
			lock (lockSelectShedulerStep)
			{
				if (stepId == null || stepId == 0)
					return null;

				using (EntityDb context = new EntityDb())
				{
					var query = context.ShedulerSteps
						.Include(x => x.ShedulerTasks)
						.Include(x => x.ShedulerTaskHistories);


					query = query.ToArray().AsQueryable();
					return query.FirstOrDefault(x => x.Id == stepId);
				}
			}
		}

		public static List<ShopComputer> SelectShopComputers()
		{
			using (EntityDb context = new EntityDb())
			{
				var query = from c in context.ShopComputers
							select new ShopComputer();
				return query.ToList();
			}
		}
		public static async Task<ActionsReportsModel[]> SelectActionsReportsGridAsync(int key)
		{
			using (EntityDb context = new EntityDb())
			{
				IQueryable<ActionsReportsModel> queryActionsReportsGrid;
				if (key != 0)
				{
					queryActionsReportsGrid = from c in context.ActionsReports
											  where c.OperationId == key
											  select new ActionsReportsModel
											  {
												  ReportId = c.ReportId,
												  OperationId = c.OperationId,
												  ComputerId = c.ComputerId,
												  ActionName = c.ActionName,
												  IsFromLan = c.IsFromLan,
												  IsCompleted = c.IsCompleted,
												  DateCompleteOrError = c.DateCompleteOrError,
												  Result = c.Result
											  };
				}
				else
				{
					queryActionsReportsGrid = from c in context.ActionsReports
											  select new ActionsReportsModel
											  {
												  ReportId = c.ReportId,
												  OperationId = c.OperationId,
												  ComputerId = c.ComputerId,
												  ActionName = c.ActionName,
												  IsFromLan = c.IsFromLan,
												  IsCompleted = c.IsCompleted,
												  DateCompleteOrError = c.DateCompleteOrError,
												  Result = c.Result
											  };
				}

				return await queryActionsReportsGrid.ToArrayAsync();
			}
		}

		public static async Task<ShopComputersModel[]> SelectComputersGridAsync()
		{
			using (EntityDb context = new EntityDb())
			{
				var queryComputersGrid = from Computers in context.ShopComputers
										 join Shops in context.Shops on Computers.ShopId equals Shops.ShopId
										 select new ShopComputersModel
										 {
											 ShopId = Shops.ShopName,
											 ShopName = Shops.ShopName,
											 AddressToConnect = Shops.AddressToConnect,
											 Phone = Shops.Phone,
											 IsExchangeError = Shops.ExchangeError ?? false,
											 LastSuccessfulReceive = Shops.LastSuccessfulReceive ?? System.DateTime.MinValue,
											 LastSuccessfulUpload = Shops.LastSuccessfulUpload ?? System.DateTime.MinValue,
											 ComputerId = Computers.ComputerId,
											 ComputerName = Computers.ComputerName,
											 Local_IP = Computers.ComputerAddress,
											 External_IP = Computers.External_IP,
											 Is1CServer = Computers.Is1CServer,
											 IsMainCashbox = Computers.IsMainCashbox,
											 IsTaskerAlive = Computers.IsTaskerAlive,
											 MagicUpdaterVersion = Computers.MagicUpdaterVersion,
											 IsOn = (Computers.IsON == 1) ? true : false
										 };

				return await queryComputersGrid.ToArrayAsync();
			}
		}

		public static async Task<ShopComputersModel[]> SelectShopComputersServerViewGridFullAsync()
		{
			using (EntityDb context = new EntityDb())
			{
				var queryComputersGrid = from Computers in context.ViewShopComputersServerFulls
										 select new ShopComputersModel
										 {
											 ShopId = Computers.ShopName,
											 ShopName = Computers.ShopName,
											 ExchangeError = Computers.ExchangeError ?? false,
											 AddressToConnect = Computers.AddressToConnect,
											 Phone = Computers.Phone,
											 IsExchangeError = Computers.ExchangeError ?? false,
											 LastSuccessfulReceive = Computers.LastSuccessfulReceive ?? System.DateTime.MinValue,
											 LastSuccessfulUpload = Computers.LastSuccessfulUpload ?? System.DateTime.MinValue,
											 ComputerId = Computers.ComputerId ?? 0,
											 IsClosed = Computers.IsClosed ?? false,
											 ComputerName = Computers.ComputerName,
											 Local_IP = Computers.ComputerAddress,
											 External_IP = Computers.External_IP,
											 Is1CServer = Computers.Is1CServer,
											 IsMainCashbox = Computers.IsMainCashbox,
											 IsTaskerAlive = Computers.IsTaskerAlive,
											 OperationTypeRu = Computers.OperationTypeRu,
											 OperationCreationDate = Computers.OperationCreationDate,
											 OperState = Computers.OperState,
											 MagicUpdaterVersion = Computers.MagicUpdaterVersion,
											 IsOn = (Computers.IsON == 1) ? true : false,
											 LastError = Computers.LastError,
											 Email = Computers.Email
										 };

				return await queryComputersGrid.ToArrayAsync();
			}
		}

		public static async Task<ShopComputersModel[]> SelectShopComputersServerViewGridAsync()
		{
			using (EntityDb context = new EntityDb())
			{
				var queryComputersGrid = (from Computers in context.ViewShopComputersServers
										  select new ShopComputersModel
										  {
											  ShopId = Computers.ShopName,
											  ShopName = Computers.ShopName,
											  ExchangeError = Computers.ExchangeError ?? false,
											  AddressToConnect = Computers.AddressToConnect,
											  Phone = Computers.Phone,
											  IsExchangeError = Computers.ExchangeError ?? false,
											  LastSuccessfulReceive = Computers.LastSuccessfulReceive,
											  LastSuccessfulUpload = Computers.LastSuccessfulUpload,
											  ComputerId = Computers.ComputerId ?? 0,
											  IsClosed = Computers.IsClosed ?? false,
											  ComputerName = Computers.ComputerName,
											  Local_IP = Computers.ComputerAddress,
											  External_IP = Computers.External_IP,
											  Is1CServer = Computers.Is1CServer,
											  IsMainCashbox = Computers.IsMainCashbox,
											  IsTaskerAlive = Computers.IsTaskerAlive,
											  OperationTypeRu = Computers.OperationTypeRu,
											  OperationCreationDate = Computers.OperationCreationDate,
											  OperState = Computers.OperState,
											  MagicUpdaterVersion = Computers.MagicUpdaterVersion,
											  IsOn = (Computers.IsON == 1) ? true : false,
											  Email = Computers.Email,
											  LastErrorDate = Computers.LastErrorDate,
											  LastError = Computers.LastError,
											  AvgPerformanceCounterValuesDateTimeUtc = Computers.AvgPerformanceCounterValuesDateTimeUtc,
											  AvgCpuTime = Computers.AvgCpuTime,
											  AvgRamAvailableMBytes = Computers.AvgRamAvailableMBytes,
											  AvgDiskQueueLength = Computers.AvgDiskQueueLength,
											  HwId = Computers.HwId,
											  LicId = Computers.LicId,
											  LicStatus = Computers.LicStatus
										  });

#if DEMO
				return await queryComputersGrid.Take(10).ToArrayAsync(); 
#endif
#if LIC
				//int licAgentsCount;
				//if (!int.TryParse(_commonGlobalSettings.LicAgentsCount, out licAgentsCount))
				//{
				//	licAgentsCount = 0;
				//}
				//return await queryComputersGrid.Take(licAgentsCount).ToArrayAsync();
				return await queryComputersGrid.ToArrayAsync();
#endif
#if !DEMO && !LIC
				return await queryComputersGrid.ToArrayAsync();
#endif
			}
		}

		public static ShopComputersModel[] SelectShopComputersServerViewGrid()
		{
			using (EntityDb context = new EntityDb())
			{
				var queryComputersGrid = from Computers in context.ViewShopComputersServers
										 select new ShopComputersModel
										 {
											 ShopId = Computers.ShopName,
											 ShopName = Computers.ShopName,
											 ExchangeError = Computers.ExchangeError ?? false,
											 AddressToConnect = Computers.AddressToConnect,
											 Phone = Computers.Phone,
											 IsExchangeError = Computers.ExchangeError ?? false,
											 LastSuccessfulReceive = Computers.LastSuccessfulReceive,
											 LastSuccessfulUpload = Computers.LastSuccessfulUpload,
											 ComputerId = Computers.ComputerId ?? 0,
											 IsClosed = Computers.IsClosed ?? false,
											 ComputerName = Computers.ComputerName,
											 Local_IP = Computers.ComputerAddress,
											 External_IP = Computers.External_IP,
											 Is1CServer = Computers.Is1CServer,
											 IsMainCashbox = Computers.IsMainCashbox,
											 IsTaskerAlive = Computers.IsTaskerAlive,
											 OperationTypeRu = Computers.OperationTypeRu,
											 OperationCreationDate = Computers.OperationCreationDate,
											 OperState = Computers.OperState,
											 MagicUpdaterVersion = Computers.MagicUpdaterVersion,
											 IsOn = (Computers.IsON == 1) ? true : false,
											 Email = Computers.Email,
											 LastErrorDate = Computers.LastErrorDate,
											 LastError = Computers.LastError,
											 AvgPerformanceCounterValuesDateTimeUtc = Computers.AvgPerformanceCounterValuesDateTimeUtc,
											 AvgCpuTime = Computers.AvgCpuTime,
											 AvgRamAvailableMBytes = Computers.AvgRamAvailableMBytes,
											 AvgDiskQueueLength = Computers.AvgDiskQueueLength,
											 HwId = Computers.HwId,
											 LicId = Computers.LicId,
											 LicStatus = Computers.LicStatus
										 };


				return queryComputersGrid.ToArray();
			}
		}

		public static OperationGroupsModel[] SelectOperationGroups()
		{
			using (EntityDb context = new EntityDb())
			{
				var queryOperationGroups = from c in context.OperationGroups
										   select new OperationGroupsModel
										   {
											   Id = c.Id,
											   Name = c.Name,
											   Description = c.Description
										   };
				return queryOperationGroups.ToArray();
			}
		}

		public static async Task<OperationGroupsModel[]> SelectOperationGroupsAsync()
		{
			using (EntityDb context = new EntityDb())
			{
				var queryOperationGroups = from c in context.OperationGroups
										   select new OperationGroupsModel
										   {
											   Id = c.Id,
											   Name = c.Name,
											   Description = c.Description
										   };
				return await queryOperationGroups.ToArrayAsync();
			}
		}

		public static async Task<ViewComputerErrorsLogModel[]> SelectComputerErrorsLogsAsync()
		{
			using (EntityDb context = new EntityDb())
			{
				DateTime time = DateTime.UtcNow.AddDays(-7);
				var queryComputerErrorsLogGrid = from c in context.ViewComputerErrorsLogs
												 where c.ErrorDate > time
												 select new ViewComputerErrorsLogModel
												 {
													 ComputerId = c.ComputerId,
													 ComputerName = c.ComputerName,
													 ErrorDate = c.ErrorDate,
													 ErrorId = c.ErrorId,
													 ErrorMessage = c.ErrorMessage,
													 External_IP = c.External_IP,
													 Is1CServer = c.Is1CServer,
													 IsMainCashbox = c.IsMainCashbox,
													 IsON = c.IsON,
													 IsTaskerAlive = c.IsTaskerAlive
												 };
				return await queryComputerErrorsLogGrid.ToArrayAsync();
			}
		}

		public static ViewTaskModel[] SelectShedulerTasksGrid()
		{
			return SelectShedulerTasks().Select(s => new ViewTaskModel
			{
				Id = s.Id,
				Name = s.Name,
				Enabled = s.Enabled ?? false,
				FirstStepId = s.FirstStepId ?? 0,
				Mode = (ShedulerTaskModes)s.Mode,
				StartTime = s.StartTime,
				NextStartTime = s.NextStartTime,
				LastStartTime = s.LastStartTime,
				LastEndTime = s.LastEndTime,
				Status = (ShedulerStatuses)s.Status,
				RepeatValue = s.RepeatValue
			}).ToArray();
		}

		public static ViewTaskModel[] SelectShedulerPluginTasksGrid()
		{
			return SelectShedulerPluginTasks().Select(s => new ViewTaskModel
			{
				Id = s.Id,
				Name = s.Name,
				Enabled = s.Enabled ?? false,
				Mode = (ShedulerTaskModes)s.Mode,
				NextStartTime = s.NextStartTime,
				LastStartTime = s.LastStartTime,
				LastEndTime = s.LastEndTime,
				Status = (ShedulerStatuses)s.Status,
				RepeatValue = s.RepeatValue
			}).ToArray();
		}

		public static async Task<ViewOperationsModel[]> SelectOperationsGridAsync()
		{
			using (EntityDb context = new EntityDb())
			{
				var queryOperationsGrid = from o in context.ViewOperationsTop5000
										  select new ViewOperationsModel
										  {
											  ID = o.ID,
											  OperationTypeId = o.OperationType,
											  OperationTypeEn = o.OperationTypeEn,
											  OperationTypeRu = !string.IsNullOrEmpty(o.OperationTypeRu) ? o.OperationTypeRu : o.OperationTypeEn,
											  CreatedUser = o.CreatedUser,
											  IsFromSheduler = o.IsFromSheduler,
											  ComputerId = o.ComputerId,
											  ComputerName = o.ComputerName,
											  ShopId = o.ShopId,
											  CreationDate = o.CreationDate,
											  DateRead = o.DateRead,
											  DateCompleteOrError = o.DateCompleteOrError,
											  IsRead = o.IsRead,
											  OperState = o.OperState,
											  IsCompleted = o.IsCompleted,
											  Result = o.Result,
											  LogDate1C = o.LogDate1C,
											  LogMessage1C = o.LogMessage1C,
											  Is1CError = o.Is1CError,
											  IsActionError = o.IsActionError,
											  PoolDate = o.PoolDate
										  };
				return await queryOperationsGrid.ToArrayAsync();
			}
		}

		public static async Task<ShopsModel[]> SelectShopsGridAsync()
		{
			using (EntityDb context = new EntityDb())
			{
				var queryShopsGrid = from s in context.Shops
									 select new ShopsModel
									 {
										 ShopId = s.ShopId,
										 ShopRegion = s.ShopRegion
									 };
				return await queryShopsGrid.ToArrayAsync();
			}
		}
		#endregion Queries
	}
}
