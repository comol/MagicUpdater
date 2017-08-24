using System;
using System.Collections.Generic;
using System.Data;
using System.Timers;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.Abstract;

namespace MagicUpdater.Core
{

	public class TaskerReporter : IDisposable
	{
		private class OperationSqlDto
		{
			public OperationSqlDto(string name = "", string fileName = "", string fileMD5 = "")
			{
				Name = name;
				FileName = fileName;
				FileMD5 = fileMD5;
			}
			public string Name { get; private set; }
			public string FileName { get; private set; }
			public string FileMD5 { get; private set; }
		}

		protected bool _isLicOk = false;

		private static TaskerReporter self = null;

		private readonly List<Operation> OperationList = new List<Operation>();

		protected System.Timers.Timer taskTimer;

		protected System.Timers.Timer selfCheckTimer;

		protected System.Threading.Timer _taskThreadTimer;

		protected System.Threading.Timer _selfCheckThreadTimer;

		protected bool isStarted = false;

		private int loop = 20;
		private bool isTaskerAlive_Internal = true;
		private bool isTaskerAlive = true;

		protected virtual bool EnableSelfCheck => true;
		protected virtual bool EnableOnlyRestartOperationMode => false;

		public TaskerReporter()
		{
			_taskThreadTimer = new System.Threading.Timer(TaskThreadTimerTick, null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
			if (EnableSelfCheck)
			{
				_selfCheckThreadTimer = new System.Threading.Timer(SelfCheckThreadTimerTick, null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
			}

			//taskTimer = new System.Timers.Timer(MainSettings.SqlSettings.OperationsListCheckTimeout);
			//taskTimer.Elapsed += Timer_Elapsed;
			//if (EnableSelfCheck)
			//{
			//	selfCheckTimer = new System.Timers.Timer(MainSettings.SqlSettings.OperationsListCheckTimeout);
			//	selfCheckTimer.Elapsed += SelfCheckTimer_Elapsed;
			//}
		}

		protected void StartTaskThreadTimer()
		{

			_taskThreadTimer?.Change(MainSettings.LocalSqlSettings.OperationsListCheckTimeout, System.Threading.Timeout.Infinite);
		}

		protected void StopTaskThreadTimer()
		{
			_taskThreadTimer?.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
		}

		protected void StartSelfCheckThreadTimer()
		{
			_selfCheckThreadTimer?.Change(MainSettings.LocalSqlSettings.OperationsListCheckTimeout, System.Threading.Timeout.Infinite);
		}

		protected void StopSelfCheckThreadTimer()
		{
			_selfCheckThreadTimer?.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
		}

		private void SelfCheckThreadTimerTick(object state)
		{
			try
			{
				try
				{
					ComputerResponse();

					if (loop == 0)
					{
						loop = 20;
						if (isTaskerAlive_Internal)
						{
							isTaskerAlive_Internal = false;
							isTaskerAlive = true;
						}
						else
							isTaskerAlive = false;
					}
				}
				catch (Exception ex)
				{
					NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, ex.Message.ToString());
				}
			}
			finally
			{
				StartSelfCheckThreadTimer();
			}
		}

		private void TaskThreadTimerTick(object state)
		{
			try
			{
#if LIC
				//if (!_isLicOk)
				//{
					int licAgentCount;
					if (!int.TryParse(MainSettings.GlobalSettings.LicAgentsCount, out licAgentCount))
					{
						licAgentCount = 0;
					}

					_isLicOk = SqlWorks.CheckAgentLic(MuCore.HwId, licAgentCount);
				//}

				if (_isLicOk)
				{
#endif
					try
					{

						isTaskerAlive_Internal = true;
						GetOperations();
						foreach (Operation op in OperationList)
						{
#if DEBUG
							//MessageBox.Show(string.Format("Будет выполнена операция {0}", op.OperationType.ToString()));
#endif
							if (op.IsOnlyMainCashbox)
								if (!MainSettings.MainSqlSettings.IsMainCashbox)
								{
									op.SendOperationReport("Данная операция предназначена для выполнения только на главной кассе", false);
									continue;
								}

							op.Run();
						}
						OperationList.Clear();
					}
					catch (Exception ex)
					{
						NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, ex.ToString());
					}
#if LIC
				}
#endif
			}
			finally
			{
				StartTaskThreadTimer();
			}
		}

		private void SelfCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			selfCheckTimer.Stop();
			try
			{
				try
				{
					selfCheckTimer.Stop();

					ComputerResponse();

					if (loop == 0)
					{
						loop = 20;
						if (isTaskerAlive_Internal)
						{
							isTaskerAlive_Internal = false;
							isTaskerAlive = true;
						}
						else
							isTaskerAlive = false;
					}
				}
				catch (Exception ex)
				{
					NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, ex.Message.ToString());
				}
			}
			finally
			{
				selfCheckTimer.Start();
			}
		}

		public static void Start()
		{
			if (self == null)
				self = new TaskerReporter();
			if (self.isStarted)
				return;
			self.isStarted = true;
			self.StartTaskThreadTimer();
			self.StartSelfCheckThreadTimer();
			//self.taskTimer.Start();
			//self.selfCheckTimer?.Start();
		}

		public static void Stop()
		{
			if (self == null)
				return;

			if (!self.isStarted)
				return;

			self.isStarted = false;
			self.StopTaskThreadTimer();
			self.StopSelfCheckThreadTimer();
			//self.taskTimer.Stop();
			//self.selfCheckTimer?.Stop();
		}

		private void ComputerResponse()
		{
			SqlWorks.ExecProc("ComputerResponse", MainSettings.MainSqlSettings.ComputerId, Environment.MachineName, NetWork.GetLocalIPAddress(), isTaskerAlive);
		}

		private bool SetOperationReaded(int operationId)
		{
			DataSet ds = SqlWorks.ExecProc("SetOperationIsReaded", operationId);
			if (ds == null)
				return false;
			return Convert.ToBoolean(ds.Tables[0].Rows[0]["Result"]);
		}

		private OperationSqlDto GetOperationName(int operationType)
		{
			DataSet ds = null;
			try
			{
				ds = SqlWorks.ExecProc("GetOperationTypesList");
				if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
				{
					foreach (DataRow row in ds.Tables[0].Rows)
					{
						if (Convert.ToInt32(row["Id"]) == operationType)
						{
							return new OperationSqlDto(
								Convert.ToString(row["Name"]),
								row["FileName"] == DBNull.Value ? "" : Convert.ToString(row["FileName"]),
								row["FileMD5"] == DBNull.Value ? "" : Convert.ToString(row["FileMD5"]));
						}
					}
				}

				return new OperationSqlDto();
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, ex.ToString(), MainSettings.Constants.MAGIC_UPDATER);
				return new OperationSqlDto();
			}
		}

		private void GetOperations()
		{
			OperationList.Clear();
			DataSet ds = null;

			if (!EnableOnlyRestartOperationMode)
				// Берем все оперции за исключением RestartMagicUpdater_Service (1003) т.к. она нужна для
				// перезапуска MagicUpdater'a и должна выполняться в отдельном потоке.
				ds = SqlWorks.ExecProc("SelectOperationsList", MainSettings.MainSqlSettings.ComputerId);
			else
				// Берем только операцию RestartMagicUpdater_Service (1003)
				ds = SqlWorks.ExecProc("SelectRestartOperation", MainSettings.MainSqlSettings.ComputerId);

			if (ds != null)
			{
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					int id = Convert.ToInt32(dr["ID"]);
					if (!SetOperationReaded(id))
					{
						NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, $"Ошибка чтения операции. ID: {id}");
						continue;
					}
					string attrsJson = Convert.ToString(dr["Attributes"]);

					var operationSqlDto = GetOperationName(Convert.ToInt32(dr["OperationType"]));

					string ot = operationSqlDto.Name;
					string fileName = operationSqlDto.FileName;
					string fileMD5 = operationSqlDto.FileMD5;

					//OperationsType ot = (OperationsType)Convert.ToInt32(dr["OperationType"]);

					Type t = null;

					//Если имя файла не пустое - это плагин операция (dll)
					if (string.IsNullOrEmpty(fileName))
					{
						t = Type.GetType($"MagicUpdater.Operations.{ot}");
					}
					else
					{
						t = PluginOperationAdapter.GetPluginOperationType(ot, fileName, fileMD5);
					}

					if (t != null)
					{
						try
						{
							Operation operation;

							//if (!string.IsNullOrEmpty(attrsJson))
							//{
							//	operation = (Operation)Activator.CreateInstance(t, id, attrsJson);
							//}
							//else
							//{
							//	operation = (Operation)Activator.CreateInstance(t, id);
							//}

							if (t.GetConstructors()[0].GetParameters().Length == 2)
							{
								operation = (Operation)Activator.CreateInstance(t, id, attrsJson);
							}
							else
							{
								operation = (Operation)Activator.CreateInstance(t, id);
							}

							OperationList.Add(operation);
						}
						catch (Exception ex)
						{
							NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, $"Возможная причина. Несовпадение сигнатур операций БД и Шарпа. Original: {ex.Message.ToString()}");
						}
					}
					else
					{
						NLogger.SetReportForOperation(id, false, "Не удалось найти тип операции");
					}
				}
			}
		}

		public virtual void Dispose()
		{
			StopSelfCheckThreadTimer();
			StopTaskThreadTimer();
			_selfCheckThreadTimer?.Dispose();
			_taskThreadTimer?.Dispose();
		}

		public static void DisposeTasker()
		{
			self?.Dispose();
		}
	}

	public class RestartTaskerReporter : TaskerReporter
	{
		private static RestartTaskerReporter self = null;
		protected override bool EnableSelfCheck => false;
		protected override bool EnableOnlyRestartOperationMode => true;

		public static new void Start()
		{
			if (self == null)
				self = new RestartTaskerReporter();
			if (self.isStarted)
				return;
			self.isStarted = true;
			self.StartTaskThreadTimer();
			//self.taskTimer.Start();
		}

		public static new void Stop()
		{
			if (self == null)
				return;

			if (!self.isStarted)
				return;

			self.isStarted = false;
			self.StopTaskThreadTimer();
			//self.taskTimer.Stop();
		}

		public static new void DisposeTasker()
		{
			self?.Dispose();
		}
	}
}
