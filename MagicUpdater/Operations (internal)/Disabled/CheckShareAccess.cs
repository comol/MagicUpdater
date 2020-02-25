using System;
using System.Collections.Generic;
using System.ComponentModel;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.Abstract;
using SmartAssembly.Attributes;

namespace MagicUpdater.Operations
{
	public class CheckShareAccess : Operation
	{

		private static readonly string REMOTE_PATH = @"c$\Windows";
		private static readonly string LOGIN = "Администратор";

		public CheckShareAccess(int? operationId) : base(operationId)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			var computersList = NetWork.GetNetworkComputerNamesScanShop();

			List<string> messages = new List<string>();

			foreach(var computer in computersList)
			{
				var remotePath = $"\\\\{computer}\\{REMOTE_PATH}";
			}

			AddCompleteMessage(string.Join(Environment.NewLine, messages));
		}
	}
}
