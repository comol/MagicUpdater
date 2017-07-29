using System;
using System.ComponentModel;
using System.Data;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.Abstract;

namespace MagicUpdater.Operations
{
	public class RegisterViaMac_Service : Operation
	{
		public RegisterViaMac_Service(int? operationId) : base(operationId) { }

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			if (!RegisterViaMac(MainSettings.MainSqlSettings))
				AddErrorMessage("Не удалось получить ID компьютера");
		}

		private static bool RegisterViaMac(SqlMainSettings newSettings)
		{
			bool result = true;
			DataSet ds = SqlWorks.ExecProc("RegisterComputer_Service",
				newSettings.ShopID,
				Environment.MachineName,
				NetWork.GetLocalIPAddress(),
				newSettings.Is1CServer,
				newSettings.IsMainCashbox,
				HardwareInfo.GetMacAddress());
			if (ds != null)
			{
				int? id = Convert.ToInt32(ds.Tables[0].Rows[0]["ComputerId"]);
				bool isNew = Convert.ToBoolean(ds.Tables[0].Rows[0]["isNew"]);

				if (id == 0)
					id = null;

				newSettings.ComputerId = id;

				if (!isNew && id != null)
				{
					ChangeComputerParams(newSettings);
				}
			}
			else
			{
				result = false;

				//MessageBox.Show("Не удалось получить ID компьютера", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return result;
		}

		private static bool ChangeComputerParams(SqlMainSettings newSettings)
		{
			bool result = true;
			DataSet ds = SqlWorks.ExecProc("ChangeComputerParams_Servece",
				newSettings.ComputerId,
				newSettings.ShopID,
				Environment.MachineName,
				NetWork.GetLocalIPAddress(),
				newSettings.Is1CServer,
				newSettings.IsMainCashbox,
				Extensions.GetApplicationVersion());
			if (ds == null)
				result = false;
			return result;
		}
	}
}
