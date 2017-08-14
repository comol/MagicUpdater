using MagicUpdaterCommon.Common;
using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.Helpers;
using System;
using System.Data;
using System.Reflection;

namespace MagicUpdaterCommon.SettingsTools
{
	public class SqlLocalSettings
	{
		public class TryLoadSettingsFromSql : TryResult
		{
			public TryLoadSettingsFromSql(bool isComplete = true, string message = "") : base(isComplete, message)
			{
			}
		}

		public void LoadSqlLocalSettings()
		{
			var res = LoadSqlLocalSettingsFromSQL(MainSettings.MainSqlSettings.ComputerId);
			if (!res.IsComplete)
			{
				NLogger.LogErrorToHdd(res.Message);
			}
		}

		public TryLoadSettingsFromSql LoadSqlLocalSettings(int? computerId)
		{
			var res = LoadSqlLocalSettingsFromSQL(computerId);
			if (!res.IsComplete)
			{
				NLogger.LogErrorToHdd(res.Message);
				return new TryLoadSettingsFromSql(false, res.Message);
			}
			return new TryLoadSettingsFromSql();
		}

		public void SaveSettingsToDb(int? computerId)
		{
			foreach (PropertyInfo pi in this.GetType().GetProperties())
			{
				SqlWorks.ExecProc("SetLocalSettingsForComputer", computerId, pi.Name, pi.GetValue(this, null));
			}
		}

		private TryLoadSettingsFromSql LoadSqlLocalSettingsFromSQL(int? computerId)
		{
			if (computerId == null || computerId == 0)
			{
				return new TryLoadSettingsFromSql(false, "Ошибка загрузки параметров для компьютера из таблицы LocalSettings, параметр ComputerId ");
			}

			DataSet ds = null;
			ds = SqlWorks.ExecProcExt(MainSettings.JsonSettings.ConnectionString, "GetLocalSettingsForComputer", computerId);
			if (ds != null)
			{
				foreach (DataRowView drv in ds.Tables[0].DefaultView)
				{
					switch (Convert.ToString(drv["Name"]))
					{
						case "Server1C":
							Server1C = Convert.ToString(drv["Value"]);
							break;
						case "Base1C":
							Base1C = Convert.ToString(drv["Value"]);
							break;
						case "BaseSQL":
							BaseSQL = Convert.ToString(drv["Value"]);
							break;
						case "BackupPath":
							BackupPath = Convert.ToString(drv["Value"]);
							break;
						case "User1C":
							User1C = Convert.ToString(drv["Value"]);
							break;
						case "Pass1C":
							Pass1C = Convert.ToString(drv["Value"]);
							break;
						case "OperationsListCheckTimeout":
							OperationsListCheckTimeout = Convert.ToInt32(drv["Value"]);
							break;
						case "SelfUpdatePath":
							SelfUpdatePath = Convert.ToString(drv["Value"]);
							break;
						case "Version1C":
							Version1C = Convert.ToString(drv["Value"]);
							break;
						case "IsCheck1C":
							IsCheck1C = Convert.ToBoolean(drv["Value"]);
							break;
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
						case "InformationBaseDirectory":
							InformationBaseDirectory = Convert.ToString(drv["Value"]);
							break;
						case "Is1CBaseOnServer":
							Is1CBaseOnServer = Convert.ToBoolean(drv["Value"]);
							break;
						case "PerformanceCounterMode":
							PerformanceCounterMode = Convert.ToInt32(drv["Value"]);
							break;
					}
				}
			}

			return new TryLoadSettingsFromSql();
		}


		//Менять имена свойств только хорошо подумав!
		//Имена свойств должны совпадать с именами в базе SQL!

		public string Server1C { get; set; } = string.Empty;
		public string Base1C { get; set; } = string.Empty;
		public string BaseSQL { get; set; } = string.Empty;
		public string BackupPath { get; set; } = string.Empty;
		public string User1C { get; set; }
		public string Pass1C { get; set; }
		public int OperationsListCheckTimeout { get; set; }
		public string SelfUpdatePath { get; set; }
		public string Version1C { get; set; }
		public bool IsCheck1C { get; set; } = true;
		public string SelfUpdateFtpServer { get; set; }
		public string SelfUpdateFtpUser { get; set; }
		public string SelfUpdateFtpPassword { get; set; }
		public string SelfUpdateFtpPath { get; set; }
		public string InformationBaseDirectory { get; set; }
		public bool Is1CBaseOnServer { get; set; } = true;
		//Режимсчетчика производительности (0 - выключен, 1 - последние средние значения, 2 - последние средние значения и история)
		public int PerformanceCounterMode { get; set; } = 0;
	}
}
