using System.ComponentModel;
using MagicUpdaterCommon.Abstract;
using MagicUpdater.Core;

namespace MagicUpdater.Operations
{
	public class SetLanMacToDB_Service : Operation
	{
		public SetLanMacToDB_Service(int? operationId) : base(operationId) { }

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			//SqlWorks.ExecProc("SetMacAddress_Service"
			//	, MainSettings.MainSqlSettings.ComputerId
			//	, MainSettings.MainSqlSettings.ShopID
			//	, Environment.MachineName
			//	, NetWork.GetLocalIPAddress()
			//	, MainSettings.MainSqlSettings.Is1CServer
			//	, MainSettings.MainSqlSettings.IsMainCashbox
			//	, HardwareInfo.GetMacAddress());

			//ApplicationConnector.RecieveSyncMessage();

			//MuCore.ConnectionToSettings.SendAsyncMessage(new Communications.Common.CommunicationObject
			//{
			//	ActionType = Communications.Common.CommunicationActionType.ShowMessage,
			//	Data = "ебланство"
			//});
		}
	}
}
