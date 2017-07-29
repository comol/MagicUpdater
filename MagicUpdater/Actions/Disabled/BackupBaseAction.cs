using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.SettingsTools;

namespace MagicUpdater.Actions
{
	public class BackupBaseAction : OperAction
	{
		public void LaunchCommandLineApp(string fileName, string arguments)
		{
			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.CreateNoWindow = false;
			startInfo.UseShellExecute = true;
			startInfo.FileName = fileName;
			startInfo.WindowStyle = ProcessWindowStyle.Normal;
			startInfo.Arguments = arguments;
			try
			{
				using (Process exeProcess = Process.Start(startInfo))
				{
					exeProcess.WaitForExit();
				}
			}
			catch(Exception ex)
			{
				this.SendReportToDB($"LaunchCommandLineApp {Environment.NewLine}{ex.ToString()}");
			}
		}

		public BackupBaseAction(int? _operationId) : base(_operationId) { }
		protected override void ActExecution()
		{
			// По умолчанию
			const string backupName = "baza.bak";
			string sqlBaseName = MainSettings.LocalSqlSettings.Base1C;
			string backupPath = "C:\\Backup";

			if (!string.IsNullOrEmpty(MainSettings.LocalSqlSettings.BaseSQL))
			{
				sqlBaseName = MainSettings.LocalSqlSettings.BaseSQL;
			}

			if (!string.IsNullOrEmpty(MainSettings.LocalSqlSettings.BackupPath))
			{
				backupPath = MainSettings.LocalSqlSettings.BackupPath;
			}

			backupPath = Path.Combine(backupPath, backupName);
			try
			{
				if (!Directory.Exists(Path.GetDirectoryName(backupPath)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(backupPath));
				}
				if (File.Exists(backupPath)){
					File.SetAttributes(backupPath, FileAttributes.Normal);
					File.Delete(backupPath);
				}
			}
			catch (Exception ex)
			{
				SendReportToDB(ex.ToString());
			}
			Thread.Sleep(1000);
			LaunchCommandLineApp("sqlcmd", $" -E -S localhost -Q \"BACKUP DATABASE [{sqlBaseName}] TO DISK = '{backupPath}'\"");

            // Проверка
            if (!File.Exists(backupPath))
            {
                SendReportToDB($"Файл {backupPath} не был создан");
            }
        }
	}
}
