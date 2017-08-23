using MagicUpdater.ServiceTools;
using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.Communication;
using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using static MagicUpdaterCommon.SettingsTools.MainSettings;

namespace MagicUpdater.Core
{
	public class MuCore : IDisposable
	{
		private bool _isPluginDebugMode;
		private static readonly string _checkQuery = $"select count(*) as cnt from [dbo].[ShopComputers]";
		public static string HwId { get; private set; }

		public MuCore(bool isPluginDebugMode = false)
		{
			_isPluginDebugMode = isPluginDebugMode;

			//ConnectionToSettings = new ServiceConnector();
			//ConnectionToSettings.CreateAsyncServer(MainSettings.Constants.MAGIC_UPDATER_PIPE_NAME);

			if (!Initialize())
			{
				//TODO: отправить в приложение
			}

			SendSelfUpdateOperationReport();

#if DEMO
			try
			{
				string query = _checkQuery;
				using (SqlConnection conn = new SqlConnection(MainSettings.JsonSettings.ConnectionString))
				{
					conn.Open();
					using (SqlCommand command = new SqlCommand(query, conn))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							reader.Read();
							int count = reader.GetInt32(0);

							if (count > 10)
							{
								Environment.Exit(0);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToHdd(ex.ToString(), MainSettings.Constants.MAGIC_UPDATER);
				throw;
			} 
#endif
		}

		private bool Initialize()
		{
			//ApplicationSettings.LoadFromJson();
			//LoadSettingsFromSQL(GetTaskConnectionString());

			#region AutoGetLatestVersion1CPath(disabled)
			//if (LatestVersion1C.Refresh())
			//{
			//	string dir = Path.GetDirectoryName(LatestVersion1C.ExePath1C);
			//	string logs = Path.Combine(dir, "Logs");
			//	DirectoryInfo df;
			//	if (!Directory.Exists(logs))
			//	{
			//		df = Directory.CreateDirectory(logs);
			//	}
			//ApplicationSettings.JsonSettings.LogPath1C = Path.Combine(logs, "OutLog.log");

			//	txtVersion1C.Text = LatestVersion1C.Version1C.ToString();
			//}
			//else
			//	txtVersion1C.Text = string.Empty;
			#endregion AutoGetLatestVersion1CPath(disabled)

			#region Check1CAutoVersion(disabled)
			//TODO: для автополучения версии, которая закомменчена
			//if(string.IsNullOrEmpty(txtLastVersion1C.Text))
			//{
			//	MessageBox.Show("Не найдена установленная версия 1С\r\nПрограмма завершает работу", "MagicUpdater - Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//  NetWork.StopServer();
			//  Program.MainForm.Dispose();
			//	Application.Exit();
			//}
			#endregion Check1CAutoVersion(disabled)

			if (!File.Exists(MainSettings.JsonSettingsFileFullPath))
			{
				NLogger.LogErrorToHdd("Json - файл настроек не найден", MainSettings.Constants.MAGIC_UPDATER);
				return false;
				//TODO: Выйти и пинать извне
			}

			//Загружаем все настройки
			TryLoadMainSettings loadSettingsResult = MainSettings.LoadSettings();
			while (!loadSettingsResult.IsComplete)
			{
				NLogger.LogErrorToHdd(loadSettingsResult.NamedMessage, MainSettings.Constants.MAGIC_UPDATER);
				Thread.Sleep(60000);
				loadSettingsResult = MainSettings.LoadSettings();
			}

			if (MainSettings.MainSqlSettings.ComputerId == null || MainSettings.MainSqlSettings.ComputerId == 0)
			{
				NLogger.LogErrorToHdd("Ошибка регистрации компьютера. Не получен ComputerId.", MainSettings.Constants.MAGIC_UPDATER);
				return false;
				//TODO: Выйти и пинать извне
			}

			//Отчитываемся о том, что настройки прочитаны
			SqlWorks.ExecProc("SetIsAgentSettingsReaded", MainSettings.MainSqlSettings.ComputerId, true);

			//Запуск TaskerReporter
			if (!MainSettings.MainSqlSettings.IsMainCashbox)
			{
				//Отрубил отправку действий по локалке
				//NetworkForActions.StartServer(10085);
			}
			
			//Если не режим отладки плагинов
			if (!_isPluginDebugMode)
			{
#if LIC
				HwId = HWID.Value();
				SqlWorks.ExecProc("InsertUpdateAgetHwid", MainSettings.MainSqlSettings.ComputerId, HwId);
#endif
				SqlWorks.ExecProc("UpdateVersion", MainSettings.MainSqlSettings.ComputerId, Extensions.GetApplicationVersion());
				TaskerReporter.Start();
				RestartTaskerReporter.Start();
				switch (MainSettings.LocalSqlSettings.PerformanceCounterMode)
				{
					case 0:
						break;
					case 1:
						PerformanceReporter.StartOnlyAvg();
						break;
					case 2:
						PerformanceReporter.StartAll();
						break;
				}
			}

			return true;
		}

		private void SendSelfUpdateOperationReport()
		{
			if (MainSettings.StartOperationId > 0)
			{
				Operation.AddOperState(MainSettings.StartOperationId, OperStates.End);
				Operation.SendOperationReport(MainSettings.StartOperationId, "", true);
			}
		}

		public void Dispose()
		{
			MuCore.ConnectionToSettings?.DisposeAsyncServer();

			//TODO
			//Убивать ли настройки? Вот в чем вопрос.

			//Process[] processes = Process.GetProcessesByName(MainSettings.Constants.MAGIC_UPDATER_SETTINGS);
			//foreach (var proc in processes)
			//{
			//	proc.Kill();
			//}
			TaskerReporter.DisposeTasker();
			RestartTaskerReporter.DisposeTasker();
			NetworkForActions.StopServer();
			PerformanceReporter.Stop();
		}

		public static ServerConnector ConnectionToSettings { get; private set; }
	}
}
