using MagicUpdater.DL;
using MagicUpdater.DL.DB;
using MagicUpdater.DL.Helpers;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterMonitor.Base;
using MagicUpdaterMonitor.Forms;
using MagicUpdaterMonitor.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace MagicUpdaterMonitor
{
	static class Program
	{
		private static MainForm _mainForm;
		public static MainForm MainFormInst => _mainForm;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)    
		{
			try
			{
				using (EntityDb context = new EntityDb())
				{
					EntityConnection ec = new EntityConnection(context.ConnectionString);
					try
					{
						ec.Open();
						ec.Close();
					}
					catch (Exception ex)
					{
						MLogger.Error(ex.ToString());
						MessageBox.Show($"Ошибка подключения к базе заданий. Original: EntityException: {ex.Message}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
				}
			}
			catch (Exception ex)
			{
				MLogger.Error(ex.ToString());
				MessageBox.Show($"Ошибка подключения к базе заданий. Original: EntityException: {ex.Message}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			string currentExePath = Application.ExecutablePath;
			string exeName = Path.GetFileName(currentExePath);
			if (Path.Combine(MQueryCommand.GetSetting("MonitorSelfUpdatePath").Value, exeName).Trim().ToUpper() == currentExePath.Trim().ToUpper())
			{
				new DialogTimerForm($"Невозможно запустить MagicUpdaterMonitor из общей папки обновления!{Environment.NewLine}Скачайте папку с программой на локальный компьютер.").ShowDialog();
				return;
			}

			bool forceStart = false;
			forceStart = args.Length > 0 && args[0] == "force";
		
			if (forceStart)
			{
				Process process1 = Process.GetCurrentProcess();
				if (process1 != null)
				{
					foreach (Process pr in Process.GetProcessesByName(process1.ProcessName))
					{
						if (pr.Id != process1.Id)
							pr.Kill();
					}
				}
			}

			MainForm.HwId = HWID.Value();

			//Создаем пользователя в базе, если его нет
			MQueryCommand.CreateUser(Environment.UserName, MainForm.HwId);
			MainForm.UserId = MQueryCommand.GetUserId(Environment.UserName, MainForm.HwId);
			if (MainForm.UserId == 0)
			{
				MessageBox.Show($"Ошибка получения UserId по имени {Environment.UserName}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(0);
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

#if !DEBUG && !NO_UPDATE
			if (SelfUpdate.IsUpdateNeeded())
			{
				SelfUpdate.Update(new UpdatingForm());
			}
			else
			{
				StartApplication();
			}
#endif
#if DEBUG || NO_UPDATE
			StartApplication();
#endif
		}

		private static void StartApplication()
		{
			

			var tryLoadGlobalSettings = MainSettings.LoadMonitorCommonGlobalSettings();
			if (!tryLoadGlobalSettings.IsComplete)
			{
				MessageBox.Show(tryLoadGlobalSettings.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			_mainForm = new MainForm();
			_mainForm.LoadForm();
			Application.Run();
		}
	}
}
