using MagicUpdaterCommon.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communications.Common;
using System.Windows.Forms;
using MagicUpdaterCommon.Common;

namespace MagicUpdaterInstaller.ApplicationTools
{
	public class ApplicationConnector : ClientConnector
	{
		public override void ClientAsyncPipesActionExecute(CommunicationObject communicationObject)
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
					MessageBox.Show(communicationObject.Data.ToString());
					break;
				case CommunicationActionType.ShowProgressForm:
					break;
				case CommunicationActionType.StartMagicUpdaterRestartForSettings:
					Tools.StartMagicUpdaterRestartForSettings(Convert.ToInt32(communicationObject.Data));
					break;
			}
		}
	}
}
