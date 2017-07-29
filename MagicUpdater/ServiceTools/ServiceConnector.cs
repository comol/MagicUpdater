using Communications.Common;
using MagicUpdaterCommon.Communication;
using MagicUpdaterCommon.Helpers;

namespace MagicUpdater.ServiceTools
{
	public class ServiceConnector : ServerConnector
	{
		protected override void ServerAsyncPipesActionExecute(CommunicationObject communicationObject)
		{
			switch (communicationObject.ActionType)
			{
				case CommunicationActionType.Reinitialization:
					break;
				case CommunicationActionType.Restart:
					break;
				case CommunicationActionType.ShowConfirmationMessage:
					break;
				case CommunicationActionType.ShowMessage:
					NLogger.LogDebugToHdd(communicationObject.Data.ToString());
					break;
				case CommunicationActionType.ShowProgressForm:
					break;
			}
		}
	}
}
