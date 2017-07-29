using MagicUpdaterCommon.Abstract;
using System;
using System.Diagnostics;

namespace MagicUpdater.Actions
{
	public class KillProcess1C : OperAction
	{
		public KillProcess1C(int? _operationId) : base(_operationId) { }
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
