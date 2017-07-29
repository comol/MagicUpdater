using MagicUpdaterCommon.Common;
using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using static MagicUpdaterCommon.SettingsTools.CommonGlobalSettings;

namespace MagicUpdaterCommon.SettingsTools
{
	public class TryRegisterComputerId : TryResult
	{
		public int Id { get; private set; }
		public TryRegisterComputerId(int id, bool isComplete = true, string message = "") : base(isComplete, message)
		{
			Id = id;
		}
	}

	public static class MainSettings
	{
		public static class Constants
		{
			public const string HELP_FILES_FOLDER = "HelpFiles";
			public const string SETTINGS_JSON_FILE_NAME = "settings.json";
			public const string RESTART_SETTINGS_PIPE_NAME = "RestartSettingsAsyncPipe";
			public const string MAGIC_UPDATER_PIPE_NAME = "MuAsyncPipe";
			public const string RESTART_PARAMETER = "restartMagicUpdaterService";
			public const string UPDATE_RESTART_PARAMETER = "updateRestartMagicUpdaterService";
			public const string WAIT_FOR_START_SETTINGS_PARAMETER = "waitForStartMagicUpdaterSettings";
			public const string RESTART_SERVICE_APPLICATION_FOLDER = "restart";
			public const string RESTART_SERVICE_APPLICATION_EXE_NAME = "MagicUpdaterRestart.exe";
			public const string SETTINGS_APPLICATION_EXE_NAME = "MagicUpdaterSettings.exe";
			public const string MAGIC_UPDATER = "MagicUpdater";
			public const string MAGIC_UPDATER_EXE = "MagicUpdater.exe";
			public const string MAGIC_UPDATER_SETTINGS = "MagicUpdaterSettings";
			public const string SERVICE_NAME = "MagicUpdater";
			public const string MAGIC_UPDATER_RESTART = "MagicUpdaterRestart";
			public const string PIPES_LOGGER_NAME = "MagicUpdaterPipes";
			public const string OPERATION_PLUGIN_DIRECTORY_NAME = "OperationPlugin";
			public const string MU_RESTART_FOLDER_NAME = "restart";
			//public const string MU_SHEDULER_USER_LOGIN_GUID = "D19A247DADD04DF3A9077B6DC9C9110C";
			public const string MU_SHEDULER_USER_NAME = "Sheduler";

			public static string PathToSettingsApplication => GetPathToSettingsApplication();
			public static string PluginOperationDllDirectoryPath => Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), OPERATION_PLUGIN_DIRECTORY_NAME);

			private static string GetPathToSettingsApplication()
			{
				if (Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).Split('\\').Last() != MainSettings.Constants.RESTART_SERVICE_APPLICATION_FOLDER)
				{
					return Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), SETTINGS_APPLICATION_EXE_NAME);
				}
				else
				{
					return Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)), SETTINGS_APPLICATION_EXE_NAME);
				}
			}
		}

		private static CommonGlobalSettings _globalSettings = null;

		public static TryRegisterComputerId RegisterComputerId(string shopId, bool is1CServer = true, bool isMainCashbox = false)
		{
			if (string.IsNullOrEmpty(shopId))
			{
				return new TryRegisterComputerId(0, false, "Пустой Id магазина");
			}

			DataSet dsShop = SqlWorks.ExecProc("IsShopExists", shopId);
			if (dsShop == null || ConvertSafe.ToInt32(dsShop.Tables[0].Rows[0]["result"]) == 0)
			{
				return new TryRegisterComputerId(0, false, $"Магазин с id = {shopId} отсутствует");
			}

			DataSet ds = SqlWorks.ExecProc("RegisterComputer",
				shopId,
				Environment.MachineName,
				NetWork.GetLocalIPAddress(),
				is1CServer,
				isMainCashbox,
				HardwareInfo.GetMacAddress());
			if (ds != null)
			{
				int? id = Convert.ToInt32(ds.Tables[0].Rows[0]["ComputerId"]);
				if (id.HasValue)
				{
					return new TryRegisterComputerId(id.Value);
				}
				else
				{
					NLogger.LogErrorToHdd("Ошибка регистрации. Не удалось получить ID компьютера.", MainSettings.Constants.MAGIC_UPDATER);
					return new TryRegisterComputerId(0, false, "Ошибка регистрации. Не удалось получить ID компьютера.");
				}
			}
			else
			{
				NLogger.LogErrorToHdd("Ошибка регистрации. Не удалось получить ID компьютера.", MainSettings.Constants.MAGIC_UPDATER);
				return new TryRegisterComputerId(0, false, "Ошибка регистрации. Не удалось получить ID компьютера.");
			}
		}

		public static TryLoadMainSettings LoadSettings()
		{
			var loadFromJsonResult = LoadFromJson();
			if (!loadFromJsonResult.IsComplete)
			{
				return new TryLoadMainSettings(false, loadFromJsonResult.NamedMessage);
			}

			var LoadMainSettingsFromSQLResult = LoadMainSettingsFromSQL();
			if (!LoadMainSettingsFromSQLResult.IsComplete)
			{
				return new TryLoadMainSettings(false, LoadMainSettingsFromSQLResult.NamedMessage);
			}

			var loadLocalSettingsFromSQLResult = LoadLocalSettingsFromSQL();
			if (!loadLocalSettingsFromSQLResult.IsComplete)
			{
				return new TryLoadMainSettings(false, loadLocalSettingsFromSQLResult.NamedMessage);
			}

			var loadAgentCommonGlobalSettingsResult = LoadAgentCommonGlobalSettings();
			if (!loadAgentCommonGlobalSettingsResult.IsComplete)
			{
				return new TryLoadMainSettings(false, loadAgentCommonGlobalSettingsResult.NamedMessage);
			}

#if !DONTCHECK
			if (LocalSqlSettings.IsCheck1C)
			{
				var getExePath1CResult = GetExePath1C();
				if (!getExePath1CResult.IsComplete)
				{
					return new TryLoadMainSettings(false, getExePath1CResult.NamedMessage);
				}

				var getLogPath1CResult = GetLogPath1C();
				if (!getLogPath1CResult.IsComplete)
				{
					return new TryLoadMainSettings(false, getLogPath1CResult.NamedMessage);
				}
			}
#endif

			return new TryLoadMainSettings();
		}

		#region TryClasses(by SOLID))
		public class TryLoadSettingsFromSql : TryResult
		{
			protected override string Name { get; set; } = "Загрузка настроек из SQL";
			public TryLoadSettingsFromSql(bool isComplete = true, string message = "") : base(isComplete, message)
			{
			}
		}

		public class TryLoadMainSettings : TryResult
		{
			protected override string Name { get; set; } = "Загрузка главных настроек";
			public TryLoadMainSettings(bool isComplete = true, string message = "") : base(isComplete, message)
			{
			}
		}

		public class TryGetExePath1C : TryResult
		{
			protected override string Name { get; set; } = "Получение полного пути до exe - файла 1С";
			public TryGetExePath1C(bool isComplete = true, string message = "") : base(isComplete, message)
			{
			}
		}

		public class TryGetLogPath1C : TryResult
		{
			protected override string Name { get; set; } = "Получение полного пути до лог - файла 1С";
			public TryGetLogPath1C(bool isComplete = true, string message = "") : base(isComplete, message)
			{
			}
		}

		public class TryLoadSettingsFromJson : TryResult
		{
			protected override string Name { get; set; } = "Получение настрорек из json - файла";
			public TryLoadSettingsFromJson(bool isComplete = true, string message = "") : base(isComplete, message)
			{
			}
		}

		#endregion TryClasses(by SOLID))

		public class CompInfo
		{
			public int ComputerId { get; set; }
			public string ShopId { get; set; }
			public string ComputerName { get; set; }
			public string IP { get; set; }
			public bool IsServer { get; set; }
			public bool IsMainCashBox { get; set; }
		}

		public static TryGetExePath1C GetExePath1C(string version = null)
		{
			Version ver = null;
			if (!string.IsNullOrEmpty(version))
			{
				Version.TryParse(version, out ver);
			}
			else
			{
				Version.TryParse(LocalSqlSettings.Version1C, out ver);
			}

			//Определение пути по службе 1С работает, но закоменчено, чтобы не было путаницы
			//if (!Get1CPathByService())
			//{
			Load1CPathByVersion(ver);
			//}

			if (string.IsNullOrEmpty(ExePath1C))
			{
				if (ver == null)
				{
					string msg = $"Версия 1С не указана в настройках";
					NLogger.LogErrorToHdd(msg);
					return new TryGetExePath1C(false, msg);
				}
				else
				{
					string msg = $"Версия 1С ({ver}) не установлена на данном компьютере";
					NLogger.LogErrorToHdd(msg);
					return new TryGetExePath1C(false, msg);
				}
			}
			return new TryGetExePath1C();
		}

		//Автоматическое определение пути 1С по службе (только для серверов, где установлена служба 1С)
		public static bool Get1CPathByService()
		{
			ExePath1C = null;
			try
			{
				//Пробуем узнать версию 1С
				const string CONST_PART_1C_AGENT_PATH = "\\bin\\ragent.exe";
				const string RAGENT_EXE = "ragent.exe";
				const string EXE_1C = "1cv8.exe";
				foreach (ServiceController sc in ServiceController.GetServices())
				{
					using (ManagementObject wmiService = new ManagementObject("Win32_Service.Name='" + sc.ServiceName + "'"))
					{
						wmiService.Get();
						string currentserviceExePath = wmiService["PathName"].ToString();
						if (currentserviceExePath.Contains(CONST_PART_1C_AGENT_PATH))
						{
							currentserviceExePath = currentserviceExePath.Replace(RAGENT_EXE, "|");
							string pathCropped = currentserviceExePath.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).First();
							pathCropped = pathCropped.Replace("\"", "");
							pathCropped = Path.Combine(pathCropped, EXE_1C);
							ExePath1C = pathCropped;
							return true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				return false;
			}
			return false;
		}

		public static bool Load1CPathByVersion(Version ver)
		{
			ExePath1C = null;
			if (ver == null)
				return false;

			DriveInfo[] drives = DriveInfo.GetDrives();

			string targetDrive = null;
			string programFiles = null;
			string exeName = null;

			foreach (DriveInfo drive in drives)
			{
				if (Directory.Exists(Path.Combine(drive.Name, "Program Files (x86)")))
					programFiles = "Program Files (x86)";
				else if (Directory.Exists(Path.Combine(drive.Name, "Program Files")))
					programFiles = "Program Files";

				if (!string.IsNullOrEmpty(programFiles))
				{
					targetDrive = drive.Name;
					string vers;

					for (int i = 80; i <= 99; i++)
					{
						if (i % 10 == 0)
							vers = ((int)(i / 10)).ToString();
						else
							vers = i.ToString();

						if (Directory.Exists(Path.Combine(targetDrive, programFiles, $"1cv{vers}")))
						{
							exeName = $"1cv{vers[0]}";

							foreach (string directory in Directory.GetDirectories(Path.Combine(targetDrive, programFiles, $"1cv{vers}")))
							{
								Version version;
								if (Version.TryParse(Path.GetFileName(directory), out version))
								{
									if (version.ToString().Trim() == ver.ToString().Trim())
									{
										ExePath1C = Path.Combine(directory, "bin", $"{exeName}.exe");
										return true;
									}
								}
							}
						}
					}
				}
			}
			return false;
		}

		public static TryGetLogPath1C GetLogPath1C()
		{
			if (string.IsNullOrEmpty(ExePath1C))
			{
				string msg = "Отсутствует путь к исполняемуом файлу 1С";
				NLogger.LogErrorToHdd(msg);
				return new TryGetLogPath1C(false, msg);
			}

			string logs = Path.Combine(Path.GetTempPath(), "1CLogsForMagicUpdater");
			if (!Directory.Exists(logs))
			{
				try
				{
					Directory.CreateDirectory(logs);
				}
				catch (Exception ex)
				{
					NLogger.LogErrorToHdd(ex.Message.ToString());
					return new TryGetLogPath1C(false, ex.Message.ToString());
				}
			}
			try
			{
				Extensions.AddDirectorySecurity(logs, System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow);
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToHdd(ex.Message.ToString());
				return new TryGetLogPath1C(false, ex.Message.ToString());
			}

			LogPath1C = Path.Combine(logs, "OutLog.log");

			return new TryGetLogPath1C();
		}

		public static TryLoadSettingsFromSql LoadMainSettingsFromSQL()
		{
			string exceptionMsg;
			if (!SqlWorks.CheckSQL_Connection(MainSettings.JsonSettings.ConnectionString, out exceptionMsg))
			{
				return new TryLoadSettingsFromSql(false, $"Ошибка подключения к SQL-базе заданий.\r\nПричина: {exceptionMsg}");
			}

			MainSqlSettings = new SqlMainSettings();
			MainSqlSettings.LoadSqlMainSettings();
			if (MainSqlSettings.ComputerId == null || MainSqlSettings.ComputerId == 0)
			{
				return new TryLoadSettingsFromSql(false, "Ошибка регистрации компьютера, не получен ComputerId. Проверьте настройки и перезапустите службу.");
			}
			else
			{
				return new TryLoadSettingsFromSql();
			}
		}

		private static TryLoadSettingsFromSql LoadLocalSettingsFromSQL()
		{
			string exceptionMsg;
			if (!SqlWorks.CheckSQL_Connection(MainSettings.JsonSettings.ConnectionString, out exceptionMsg))
			{
				return new TryLoadSettingsFromSql(false, $"Ошибка подключения к SQL-базе заданий.\r\nПричина: {exceptionMsg}");
			}

			if (MainSqlSettings.ComputerId == null || MainSqlSettings.ComputerId == 0)
			{
				return new TryLoadSettingsFromSql(false, "Ошибка регистрации компьютера, не получен ComputerId. Проверьте настройки и перезапустите службу.");
			}
			else
			{
				try
				{
					LocalSqlSettings = new SqlLocalSettings();
					LocalSqlSettings.LoadSqlLocalSettings();
				}
				catch (Exception ex)
				{
					return new TryLoadSettingsFromSql(false, ex.Message);
				}
				return new TryLoadSettingsFromSql();
			}
		}

		public static TryLoadSettingsFromJson LoadFromJson()
		{
			try
			{
				if (!File.Exists(JsonSettingsFileFullPath))
				{
					return new TryLoadSettingsFromJson(false, "Отсутствует файл настроек \"settings.json\"");
				}
				JsonSettings = NewtonJson.ReadJsonFile<JsonLocalSettings>(JsonSettingsFileFullPath);
				return new TryLoadSettingsFromJson();
			}
			catch (Exception ex)
			{
				return new TryLoadSettingsFromJson(false, ex.Message.ToString());
			}
		}

		public static TryLoadCommonGlobalSettings LoadMonitorCommonGlobalSettings()
		{
			var loadFromJsonResult = LoadFromJson();
			if (!loadFromJsonResult.IsComplete)
			{
				return new TryLoadCommonGlobalSettings(false, loadFromJsonResult.Message);
			}

			string exceptionMsg;
			if (!SqlWorks.CheckSQL_Connection(MainSettings.JsonSettings.ConnectionString, out exceptionMsg))
			{
				return new TryLoadCommonGlobalSettings(false, $"Ошибка подключения к SQL-базе заданий.\r\nПричина: {exceptionMsg}");
			}

			_globalSettings = new CommonGlobalSettings();
			return _globalSettings.LoadCommonGlobalSettings();
		}

		public static TryLoadCommonGlobalSettings LoadAgentCommonGlobalSettings()
		{
			_globalSettings = new CommonGlobalSettings();
			return _globalSettings.LoadCommonGlobalSettings();
		}

		public static void RefreshSettings(SqlLocalSettings newSettings)
		{
			LocalSqlSettings = newSettings;
		}

		private static string GetPathOfMagicUpdaterService()
		{
			return Extensions.GetServiceInstallPath("MagicUpdater");
		}

		private static string GetPathOfMagicUpdaterProcess()
		{
			return Extensions.GetPathByProcessName("MagicUpdater");
		}

		private static string GetJsonSettingsFileFullPath()
		{
			if (Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).Split('\\').Last() != MainSettings.Constants.RESTART_SERVICE_APPLICATION_FOLDER)
			{
				return Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), MainSettings.Constants.SETTINGS_JSON_FILE_NAME);
			}
			else
			{
				return Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)), MainSettings.Constants.SETTINGS_JSON_FILE_NAME);
			}
		}

		public static int StartOperationId { get; set; } = 0;

		//public static string JsonSettingsFileFullPath => Path.Combine(Path.GetDirectoryName(GetPathOfMagicUpdaterProcess()), Settings.Default.SettingsFileName);

		public static string JsonSettingsFileFullPath => GetJsonSettingsFileFullPath();
		public static List<CompInfo> ComputersList { get; private set; } = new List<CompInfo>();
		public static SqlMainSettings MainSqlSettings { get; private set; }
		public static SqlLocalSettings LocalSqlSettings { get; private set; }
		public static JsonLocalSettings JsonSettings { get; private set; }
		public static CommonGlobalSettings GlobalSettings { get { return _globalSettings; } }
		public static string ExePath1C { get; private set; } = null;
		public static string LogPath1C { get; set; } = null;

	}
}
