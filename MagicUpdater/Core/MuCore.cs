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

		}

		private bool Initialize()
		{

			if (!File.Exists(MainSettings.JsonSettingsFileFullPath))
			{
				NLogger.LogErrorToHdd("Json - файл настроек не найден", MainSettings.Constants.MAGIC_UPDATER);
				return false;
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
			}

			//Отчитываемся о том, что настройки прочитаны
			SqlWorks.ExecProc("SetIsAgentSettingsReaded", MainSettings.MainSqlSettings.ComputerId, true);
			
			//Если не режим отладки плагинов
			if (!_isPluginDebugMode)
			{

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
			TaskerReporter.DisposeTasker();
			RestartTaskerReporter.DisposeTasker();
			NetworkForActions.StopServer();
			PerformanceReporter.Stop();
		}

		public static ServerConnector ConnectionToSettings { get; private set; }
	}
}
