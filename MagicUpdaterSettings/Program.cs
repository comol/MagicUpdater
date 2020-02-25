using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		static void Main()
		{
			bool createdNew = true;

			using (Mutex mutex = new Mutex(true, Application.ProductName, out createdNew))
			{

				if (createdNew)
				{
					AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;

					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					MainForm = new SettingsForm();
					Application.Run();
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

		public static SettingsForm MainForm { get; private set; }
	}
}
