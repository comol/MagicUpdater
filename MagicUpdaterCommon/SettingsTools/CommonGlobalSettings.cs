using MagicUpdaterCommon.Common;
using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.Helpers;
using System;
using System.Data;

namespace MagicUpdaterCommon.SettingsTools
{
	public class CommonGlobalSettings
	{
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

		public TryLoadCommonGlobalSettings LoadCommonGlobalSettingsFromSQL()
		{
			DataSet ds = null;
			if (MainSettings.JsonSettings != null && !string.IsNullOrEmpty(MainSettings.JsonSettings.ConnectionString))
			{
				ds = SqlWorks.ExecProcExt(MainSettings.JsonSettings.ConnectionString, "GetCommonGlobalSettings");
			}
			else
			{
				//ds = SqlWorks.ExecProcExt(Settings.Default.ConnectionString, "GetCommonGlobalSettings");
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
						case "PasswordAst":
							PasswordAst = Convert.ToString(drv["Value"]);
							break;
						case "UserAst":
							UserAst = Convert.ToString(drv["Value"]);
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


	}
}
