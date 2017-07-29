using MagicUpdaterCommon.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MagicUpdaterCommon.SettingsTools;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace BackupBaseSqlPlugin
{
	public static class Const
	{
		//Auto consts 
		public const string SQL_BASE = "|*sql_base*|";
		public const string LOCAL_BACKUP_PATH = "|*local_backup_path*|";

		//Attributes consts
		public const string SQL_SERVER = "|*sql_server*|";
		public const string SQL_LOGIN = "|*sql_login*|";
		public const string SQL_PASSWORD = "|*sql_password*|";

		//Hard consts
		public const string SQLCMD = "sqlcmd";
	}

	public class BackupBaseSqlPlugin : OperationWithAttr<SqlAttrib>
	{
		private readonly string _pattern = $"-S {Const.SQL_SERVER} -U {Const.SQL_LOGIN} -P {Const.SQL_PASSWORD} -Q \"BACKUP DATABASE [{Const.SQL_BASE}] TO DISK = '{Const.LOCAL_BACKUP_PATH}'\"";

		public BackupBaseSqlPlugin(int? operationId, string attrsJson) : base(operationId, attrsJson)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			string backupPath = Attributes.LocalBackupPath;

			if (!Directory.Exists(Path.GetDirectoryName(backupPath)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(backupPath));
			}

			if (File.Exists(backupPath))
			{
				File.SetAttributes(backupPath, FileAttributes.Normal);
				File.Delete(backupPath);
			}

			Thread.Sleep(1000);

			string sqlBaseName = MainSettings.LocalSqlSettings.Base1C;

			if (!string.IsNullOrEmpty(MainSettings.LocalSqlSettings.BaseSQL))
			{
				sqlBaseName = MainSettings.LocalSqlSettings.BaseSQL;
			}

			string args = _pattern.Replace(Const.SQL_SERVER, Attributes.Server)
									.Replace(Const.SQL_LOGIN, Attributes.Login)
									.Replace(Const.SQL_PASSWORD, Attributes.Password)
									.Replace(Const.SQL_BASE, sqlBaseName)
									.Replace(Const.LOCAL_BACKUP_PATH, backupPath);

			LaunchCommandLineApp(Const.SQLCMD, args);

			// Проверка
			if (!File.Exists(backupPath))
			{
				AddErrorMessage($"Файл {backupPath} не был создан");
			}
		}

		public void LaunchCommandLineApp(string fileName, string arguments)
		{
			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.CreateNoWindow = false;
			startInfo.UseShellExecute = true;
			startInfo.FileName = fileName;
			startInfo.WindowStyle = ProcessWindowStyle.Normal;
			startInfo.Arguments = arguments;

			using (Process exeProcess = Process.Start(startInfo))
			{
				exeProcess.WaitForExit();
			}
		}
	}

	public class SqlAttrib : IOperationAttributes
	{
		[OperationAttributeDisplayName("Server")]
		public string Server { get; set; } = "localhost";
		[OperationAttributeDisplayName("Login")]
		public string Login { get; set; } = "sa";
		[OperationAttributeDisplayName("Password")]
		public string Password { get; set; } = "Sela111111";
		[OperationAttributeDisplayName("Локальный путь бекапа")]
		public string LocalBackupPath { get; set; } = "C:\\Backup\\baza.bak";
	}

	public class RegistrationParams : IRegistrationParams
	{
		public string Description => $"Бекап sql-базы на агенте.";

		public int GroupId => 1;

		public string NameRus => "Бекап Sql-базы (плагин)";
	}
}
