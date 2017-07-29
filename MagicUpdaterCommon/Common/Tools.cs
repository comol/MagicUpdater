using MagicUpdaterCommon.SettingsTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MagicUpdaterCommon.Common
{
	public static class Tools
	{
		private static string _restartServiceApplication = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), MainSettings.Constants.RESTART_SERVICE_APPLICATION_FOLDER, MainSettings.Constants.RESTART_SERVICE_APPLICATION_EXE_NAME);
		public static void SelfRestart(int? operationId)
		{
			Process.Start(_restartServiceApplication, $"{MainSettings.Constants.RESTART_PARAMETER} {operationId}");
		}

		public static void SelfUpdateAndRestart(int? operationId)
		{
			Process.Start(_restartServiceApplication, $"{MainSettings.Constants.UPDATE_RESTART_PARAMETER} {operationId}");
		}

		public static void StartMagicUpdaterRestartForSettings(int? operationId)
		{
			Process.Start(_restartServiceApplication, $"{MainSettings.Constants.WAIT_FOR_START_SETTINGS_PARAMETER} {operationId}");
		}

		public static void StartMagicUpdaterSettings()
		{
			Process.Start(MainSettings.Constants.PathToSettingsApplication);
		}

		public static void WaitAllProcessByname(string processName, int timeout = 300000)
		{
			Process[] procs = Process.GetProcessesByName(processName);
			foreach(var proc in procs)
			{
				proc.WaitForExit(timeout);
			}
		}

		public static int GetProcessesCount(string processName, bool isCountSelf = false)
		{
			Process[] procs = Process.GetProcessesByName(processName);
			int procId = Process.GetCurrentProcess().Id;
			int i = 0;
			foreach(var proc in procs)
			{
				if (proc.Id != procId || isCountSelf )
				{
					i++;
				}
			}
			return i;
		}

		public static void KillAllProcessByname(string processName, bool killSelfProcess = false)
		{
			Process[] processes = Process.GetProcessesByName(processName);
			int currentProcessId = Process.GetCurrentProcess().Id;
			if (killSelfProcess)
			{
				foreach (var proc in processes)
				{
					proc.Kill();
				}
			}
			else
			{
				foreach (var proc in processes)
				{
					if (proc.Id != currentProcessId)
					{
						proc.Kill();
					}
				}
			}
		}
	}
}
