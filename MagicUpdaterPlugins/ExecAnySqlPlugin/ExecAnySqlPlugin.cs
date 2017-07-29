using MagicUpdaterCommon.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using MagicUpdaterCommon.SettingsTools;

namespace ExecAnySqlPlugin
{
	public static class Const
	{
		//Auto consts
		public const string SQL_BASE = "|*sql_base*|";

		//Attributes consts
		public const string SQL_SERVER = "|*sql_server*|";
		public const string SQL_LOGIN = "|*sql_login*|";
		public const string SQL_PASSWORD = "|*sql_password*|";
		public const string SQL_QUERY = "|*sql_query*|";

		//Hard consts
		public const string SQLCMD = "sqlcmd";
	}

	public class ExecAnySqlPlugin : OperationWithAttr<SqlAttrib>
	{
		
		private readonly string _pattern = $"-S {Const.SQL_SERVER} -U {Const.SQL_LOGIN} -P {Const.SQL_PASSWORD} -Q \"{Const.SQL_QUERY}\"";

		public ExecAnySqlPlugin(int? operationId, string attrsJson) : base(operationId, attrsJson)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			//Filling pattern
			string args = _pattern.Replace(Const.SQL_SERVER, Attributes.Server)
								.Replace(Const.SQL_LOGIN, Attributes.Login)
								.Replace(Const.SQL_PASSWORD, Attributes.Password)
								.Replace(Const.SQL_QUERY, Attributes.Query);

			string sqlBaseName = MainSettings.LocalSqlSettings.Base1C;

			if (!string.IsNullOrEmpty(MainSettings.LocalSqlSettings.BaseSQL))
			{
				sqlBaseName = MainSettings.LocalSqlSettings.BaseSQL;
			}

			//Replacing auto constants
			args = args.Replace(Const.SQL_BASE, sqlBaseName);

			LaunchCommandLineApp(Const.SQLCMD, args);
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

				if (exeProcess.ExitCode == 0)
				{
					AddCompleteMessage($"Cmd line: {fileName} {arguments}{Environment.NewLine}Process exit code: {exeProcess.ExitCode}");
				}
				else
				{
					AddErrorMessage($"Cmd line: {fileName} {arguments}{Environment.NewLine}Process exit code: {exeProcess.ExitCode}");
				}
			}
		}
	}

	public class SqlAttrib : IOperationAttributes
	{
		[OperationAttributeDisplayName("Server")]
		public string Server { get; set; }
		[OperationAttributeDisplayName("Login")]
		public string Login { get; set; }
		[OperationAttributeDisplayName("Password")]
		public string Password { get; set; }
		[OperationAttributeDisplayName("Sql-запрос")]
		public string Query { get; set; } = "";
		
	}

	public class RegistrationParams : IRegistrationParams
	{
		public string Description => $"Выполнение sql-запроса на агенте. \"{Const.SQL_BASE}\" - Base1C или BaseSQL(если не пустая), указанная в настройках агента.";

		public int GroupId => 1;

		public string NameRus => "Выполнить Sql-запрос";
	}
}
