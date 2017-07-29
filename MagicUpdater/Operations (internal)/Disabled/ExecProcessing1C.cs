using System;
using System.ComponentModel;
using System.IO;
using MagicUpdater.Actions;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.CommonActions;
using SmartAssembly.Attributes;

namespace MagicUpdater.Operations
{
	//Хардкод для конкретного случая
	public class ExecProcessing1C : Operation
	{
		private static readonly string FILE_NAME = "NodesRegistrationRulesUpdate.epf";
		private static readonly string FTP_FILE_PATH = @"ftp://mskftp.sela.ru/MagicUpdater";
		private static readonly string FTP_LOGIN = "cis_obmen";
		private static readonly string FTP_PASSWORD = "cisobmen836";
		private static readonly string LOCAL_FILE_PATH = @"C:\SystemUtils\MagicUpdaterNewVer";

		public ExecProcessing1C(int? operationId) : base(operationId)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			string ftpFilePath = Path.Combine(FTP_FILE_PATH, FILE_NAME);
			string localFilePath = Path.Combine(LOCAL_FILE_PATH, FILE_NAME);

			var res = FtpWorks.DownloadFileFromFtpOld(localFilePath, ftpFilePath, FTP_LOGIN, FTP_PASSWORD);

			if(!res.IsComplete)
				throw new Exception(res.Message);

			if (!File.Exists(localFilePath))
				throw new Exception($"Файл не существует:{Environment.NewLine}{localFilePath}");

			var act = new MagicUpdaterCommon.CommonActions.StartWithParameter1C(Id, Parameters1C.CmdParams1C.ExecProcessing(localFilePath));
			act.ActRun();

			if (!act.NewProc.HasExited)
				act.NewProc.WaitForExit(60000 * 5);

			if (!act.NewProc.HasExited)
				act.NewProc.Kill();
		}
	}
}
