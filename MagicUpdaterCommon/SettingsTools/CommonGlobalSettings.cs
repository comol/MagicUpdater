using MagicUpdaterCommon.Common;
using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.Helpers;
using System;
using System.Data;

namespace MagicUpdaterCommon.SettingsTools
{
	public class CommonGlobalSettings
	{
		private static readonly string LIC_AGENTS_COUNT = "LicAgentsCount";
		private static readonly string LIC_MONITOR_COUNT = "LicMonitorCount";

		public static string Lic_Agents_Count => LIC_AGENTS_COUNT;
		public static string Lic_Monitor_Count => LIC_MONITOR_COUNT;

		public class TryLoadCommonGlobalSettings : TryResult
		{
			public TryLoadCommonGlobalSettings(bool isComplete = true, string message = "") : base(isComplete, message)
			{
			}
		}

		public TryLoadCommonGlobalSettings LoadCommonGlobalSettings()
		{
			var res = LoadCommonGlobalSettingsFromSQL();
			if (!res.IsComplete)
			{
				NLogger.LogErrorToHdd(res.Message);
				return new TryLoadCommonGlobalSettings(false, res.Message);
			}
			else
			{
				return new TryLoadCommonGlobalSettings();
			}

		}

		private TryLoadCommonGlobalSettings LoadCommonGlobalSettingsFromSQL()
		{
			DataSet ds = null;
			if (MainSettings.JsonSettings != null && !string.IsNullOrEmpty(MainSettings.JsonSettings.ConnectionString))
			{
				ds = SqlWorks.ExecProcExt(MainSettings.JsonSettings.ConnectionString, "GetCommonGlobalSettings");
			}
			else
			{
				return new TryLoadCommonGlobalSettings(false, "Ошибка загрузки общих параметров из таблицы CommonGlobalSettings");
			}
			if (ds != null)
			{
				foreach (DataRowView drv in ds.Tables[0].DefaultView)
				{
					switch (Convert.ToString(drv["Name"]))
					{
						case "SelfUpdateFtpServer":
							SelfUpdateFtpServer = Convert.ToString(drv["Value"]);
							break;
						case "SelfUpdateFtpUser":
							SelfUpdateFtpUser = Convert.ToString(drv["Value"]);
							break;
						case "SelfUpdateFtpPassword":
							SelfUpdateFtpPassword = Convert.ToString(drv["Value"]);
							break;
						case "SelfUpdateFtpPath":
							SelfUpdateFtpPath = Convert.ToString(drv["Value"]);
							break;
						case "MonitorSelfUpdatePath":
							MonitorSelfUpdatePath = Convert.ToString(drv["Value"]);
							break;
						case "AgentLastVersionPathForMonitor":
							AgentLastVersionPathForMonitor = Convert.ToString(drv["Value"]);
							break;
						case "TempFilesFtpPath":
							TempFilesFtpPath = Convert.ToString(drv["Value"]);
							break;
						case "Server1C":
							Server1C = Convert.ToString(drv["Value"]);
							break;
						case "Base1C":
							Base1C = Convert.ToString(drv["Value"]);
							break;
						case "User1C":
							User1C = Convert.ToString(drv["Value"]);
							break;
						case "Password1C":
							Password1C = Convert.ToString(drv["Value"]);
							break;
						case "Platform":
							Platform = Convert.ToString(drv["Value"]);
							break;
						case "AddressAst":
							AddressAst = Convert.ToString(drv["Value"]);
							break;
						case "UserAst":
							UserAst = Convert.ToString(drv["Value"]);
							break;
						case "PasswordAst":
							PasswordAst = Convert.ToString(drv["Value"]);
							break;
						case "LicAgentsCount":
							LicAgentsCount = Convert.ToString(drv["Value"]);
							break;
						case "LicMonitorCount":
							LicMonitorCount = Convert.ToString(drv["Value"]);
							break;
						case "LicLink":
							LicLink = Convert.ToString(drv["Value"]);
							break;
						case "LicLogin":
							LicLogin = Convert.ToString(drv["Value"]);
							break;
						case "LicPassword":
							LicPassword = Convert.ToString(drv["Value"]);
							break;
						case "UpdateVersionRemoteServer":
							UpdateVersionRemoteServer = Convert.ToString(drv["Value"]);
							break;
						case "UpdateVersionRemoteLogin":
							UpdateVersionRemoteLogin = Convert.ToString(drv["Value"]);
							break;
						case "UpdateVersionRemotePassword":
							UpdateVersionRemotePassword = Convert.ToString(drv["Value"]);
							break;
					}
				}

				return new TryLoadCommonGlobalSettings();
			}
			else
			{
				return new TryLoadCommonGlobalSettings(false, "Ошибка загрузки общих параметров из таблицы CommonGlobalSettings");
			}
		}


		//Менять имена свойств только хорошо подумав!
		//Имена свойств должны совпадать с именами в базе SQL!

		public string MonitorSelfUpdatePath { get; set; }
		public string SelfUpdateFtpServer { get; set; }
		public string SelfUpdateFtpUser { get; set; }
		public string SelfUpdateFtpPassword { get; set; }
		public string SelfUpdateFtpPath { get; set; }
		public string AgentLastVersionPathForMonitor { get; set; }
		public string TempFilesFtpPath { get; set; }
		public string Server1C { get; set; }
		public string Base1C { get; set; }
		public string User1C { get; set; }
		public string Password1C { get; set; }
		public string Platform { get; set; }
		public string AddressAst { get; set; }
		public string UserAst { get; set; }
		public string PasswordAst { get; set; }
		public string LicAgentsCount { get; set; }
		public string LicMonitorCount { get; set; }
		public string LicLink { get; set; }
		public string LicLogin { get; set; }
		public string LicPassword { get; set; }

		public string UpdateVersionRemoteServer { get; set; }
		public string UpdateVersionRemoteLogin { get; set; }
		public string UpdateVersionRemotePassword { get; set; }
	}
}
