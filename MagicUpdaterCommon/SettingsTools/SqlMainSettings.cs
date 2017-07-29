using MagicUpdaterCommon.Common;
using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MagicUpdaterCommon.SettingsTools
{
	public class SqlMainSettings
	{
		public class TryLoadSettingsFromSql : TryResult
		{
			public TryLoadSettingsFromSql(bool isComplete = true, string message = "") : base(isComplete, message)
			{
			}
		}

		public void LoadSqlMainSettings()
		{
			var res = LoadSqlMainSettingsFromSQL();
			if (!res.IsComplete)
			{
				NLogger.LogErrorToHdd(res.Message);
			}
		}

		public TryLoadSettingsFromSql LoadSqlMainSettingsFromSQL()
		{
			DataSet ds = null;
			ds = SqlWorks.ExecProcExt(MainSettings.JsonSettings.ConnectionString, "GetComputerSettingsViaMac", HardwareInfo.GetMacAddress());
			if (ds != null)
			{
				ComputerId = ConvertSafe.ToInt32(ds.Tables[0].Rows[0]["ComputerId"]);
				ShopID = ConvertSafe.ToString(ds.Tables[0].Rows[0]["ShopId"]);
				Is1CServer = ConvertSafe.ToBoolean(ds.Tables[0].Rows[0]["Is1CServer"]);
				IsMainCashbox = ConvertSafe.ToBoolean(ds.Tables[0].Rows[0]["IsMainCashbox"]);
				return new TryLoadSettingsFromSql();
			}
			else
			{
				ComputerId = 0;
				ShopID = string.Empty;
				Is1CServer = false;
				IsMainCashbox = false;
				return new TryLoadSettingsFromSql(false, "Компьтер не зарегистрирован.");

				//if (RegisterComputerId().IsComplete)
				//{
				//	return new TryLoadSettingsFromSql(false, "Ошибка регистрации компьютера. Не получен ComputerId");
				//}

				//ds = SqlWorks.ExecProcExt(MainSettings.JsonSettings.ConnectionString, "GetComputerSettingsViaMac", HardwareInfo.GetMacAddress());
				//if (ds != null)
				//{
				//	ComputerId = ConvertSafe.ToInt32(ds.Tables[0].Rows[0]["ComputerId"]);
				//	ShopID = ConvertSafe.ToString(ds.Tables[0].Rows[0]["ShopId"]);
				//	Is1CServer = ConvertSafe.ToBoolean(ds.Tables[0].Rows[0]["Is1CServer"]);
				//	IsMainCashbox = ConvertSafe.ToBoolean(ds.Tables[0].Rows[0]["IsMainCashbox"]);
				//	return new TryLoadSettingsFromSql();
				//}
				//else
				//{
				//	ComputerId = 0;
				//	ShopID = string.Empty;
				//	Is1CServer = false;
				//	IsMainCashbox = false;
				//	return new TryLoadSettingsFromSql(false, "Компьтер не зарегистрирован.");
				//}
			}
		}

		public int? ComputerId { get; set; }
		public string ShopID { get; set; }
		public bool Is1CServer { get; set; }
		public bool IsMainCashbox { get; set; }
	}
}
