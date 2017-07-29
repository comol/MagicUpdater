using System;
using System.Collections.Generic;
using System.Text;

namespace Communications.Common
{
	public enum CommunicationActionType
	{
		//Служебные
		DisposeServer = -1,
		MessageRecieved = -2,

		//Стандартные
		ShowMessage = 0,
		ShowConfirmationMessage = 1,
		ShowProgressForm = 2,
		Reinitialization = 3,
		Restart = 4,
		StartMagicUpdaterRestartForSettings = 5,
		StartMagicUpdaterSettings = 6
	}

	[Serializable]
	public class CommunicationObject
	{
		public CommunicationActionType ActionType { get; set; }
		public object Data { get; set; }
		public object Tag { get; set; }
	}

}
