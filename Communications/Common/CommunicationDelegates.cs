using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communications.Common
{
	public class CommunicationDelegates
	{
		public delegate void CommunicationEventHandler(object sender, CommunicationObject communicationObject = null);
	}
}
