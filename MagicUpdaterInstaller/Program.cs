using MagicUpdaterInstaller;
using MagicUpdaterInstaller.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicUpdaterInstaller
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			if (args != null
					&& args.Length == 2
					&& args[0] == "restart"
					&& !string.IsNullOrEmpty(args[1])
					&& File.Exists(args[1]))
			{
				MuServiceInstaller muServiceInstaller = new MuServiceInstaller(args[1]);
				muServiceInstaller.Restart();
				return;
			}

			if (args != null
					&& args.Length == 2
					&& args[0] == "uninstallService"
					&& !string.IsNullOrEmpty(args[1])
					&& File.Exists(args[1]))
			{
				MuServiceInstaller muServiceInstaller = new MuServiceInstaller(args[1]);
				muServiceInstaller.UnInstallService(args[1]);
				return;
			}

			bool createdNew = true;

			using (Mutex mutex = new Mutex(true, Application.ProductName, out createdNew))
			{
				if (createdNew)
				{
					AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;

					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new StartForm());
				}
				else
				{
					Process current = Process.GetCurrentProcess();
					foreach (Process process in Process.GetProcessesByName(current.ProcessName))
					{
						if (process.Id != current.Id)
						{
							break;
						}
					}
				}
			}
		}

		private static void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
		{
		}

		public static BaseForm MainForm { get; set; }
	}
}
