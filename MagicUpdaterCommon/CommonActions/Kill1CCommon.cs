using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicUpdaterCommon.Abstract;
using System.Diagnostics;

namespace MagicUpdaterCommon.CommonActions
{
	public class KillProcess1CCommon : OperAction
	{
		public KillProcess1CCommon(int? _operationId) : base(_operationId) { }
		private readonly string processName = "1cv8";

		private void KillProcess(string processName)
		{

			foreach (Process proc in Process.GetProcesses())
			{
				if (strLeft(proc.ProcessName, 4) == processName)
				{
					try
					{
						proc.Kill();
					}
					catch
					{

					}
				}
			}
		}

		protected override void ActExecution()
		{
			KillProcess(processName);
		}

		private string strLeft(string value, int maxLength)
		{
			if (string.IsNullOrEmpty(value)) return value;
			maxLength = Math.Abs(maxLength);

			return (value.Length <= maxLength
				   ? value
				   : value.Substring(0, maxLength)
				   );
		}

	}
}
