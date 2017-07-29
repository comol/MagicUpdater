using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;

namespace MagicUpdaterCommon.Helpers
{
	public static class Extensions
	{
		public static string GetCommandLine(this Process process)
		{
			var commandLine = new StringBuilder(process.MainModule.FileName);

			commandLine.Append(" ");
			using (var searcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
			{
				foreach (var @object in searcher.Get())
				{
					commandLine.Append(@object["CommandLine"]);
					commandLine.Append(" ");
				}
			}

			return commandLine.ToString();
		}

		public static string ToHex(this byte[] bytes, bool upperCase)
		{
			StringBuilder result = new StringBuilder(bytes.Length * 2);

			for (int i = 0; i < bytes.Length; i++)
				result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

			return result.ToString();
		}

		public static void Each<T>(this IEnumerable<T> enumeration, Action<T> action)
		{
			foreach (T item in enumeration)
			{
				action(item);
			}
		}

		public static void ExitApplication()
		{
			//NetWork.StopServer();
			//TaskerReporter.Stop();
			//Program.MainForm.Invoke(new MethodInvoker(() => Program.MainForm.Dispose()));
			//Application.Exit();
		}

		public static string GetServiceInstallPath(string serviceName)
		{
			RegistryKey regkey;
			regkey = Registry.LocalMachine.OpenSubKey(string.Format(@"SYSTEM\CurrentControlSet\services\{0}", serviceName));

			if (regkey == null)
			{
				regkey = Registry.LocalMachine.OpenSubKey(string.Format(@"SYSTEM\CurrentControlSet\services\{0}", $"{serviceName}.exe"));
			}

			if (regkey == null)
			{
				return "";
			}

			if (regkey.GetValue("ImagePath") == null)
				return "";
			else
				return regkey.GetValue("ImagePath").ToString();
		}

		public static string GetPathByProcessName(string processName)
		{
			Process[] processes = Process.GetProcessesByName(processName);
			if (processes.Length > 0)
			{
				return processes[0].MainModule.FileName;
			}

			processes = Process.GetProcessesByName($"{processName}.vshost");
			if (processes.Length > 0)
			{
				return processes[0].MainModule.FileName;
			}

			return "";
		}

		public static string GetApplicationVersion()
		{
			Version version = Assembly.GetCallingAssembly().GetName().Version;
			return version.ToString();
		}

		public static void AddDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
		{
			// Create a new DirectoryInfo object.
			DirectoryInfo dInfo = new DirectoryInfo(FileName);

			// Get a DirectorySecurity object that represents the 
			// current security settings.
			DirectorySecurity dSecurity = dInfo.GetAccessControl();

			// Add the FileSystemAccessRule to the security settings. 
			dSecurity.AddAccessRule(new FileSystemAccessRule(Account,
															Rights,
															ControlType));

			// Set the new access settings.
			dInfo.SetAccessControl(dSecurity);

		}

		// Removes an ACL entry on the specified directory for the specified account.
		public static void RemoveDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
		{
			// Create a new DirectoryInfo object.
			DirectoryInfo dInfo = new DirectoryInfo(FileName);

			// Get a DirectorySecurity object that represents the 
			// current security settings.
			DirectorySecurity dSecurity = dInfo.GetAccessControl();

			// Add the FileSystemAccessRule to the security settings. 
			dSecurity.RemoveAccessRule(new FileSystemAccessRule(Account,
															Rights,
															ControlType));

			// Set the new access settings.
			dInfo.SetAccessControl(dSecurity);

		}

		public static bool IsNullableDateTimeIdentical(DateTime? dateTime1, DateTime? dateTime2)
		{
			if(dateTime1.HasValue && dateTime2.HasValue)
			{
				return dateTime1.Value == dateTime2.Value;
			}
			else
			{
				return false;
			}
		}
	}
}
