using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.IO;

namespace MagicUpdater.Actions
{
	public class FTPDownloadUpdate : OperAction
    {
		public FTPDownloadUpdate(int? _operationId) : base(_operationId) { }

		protected override void ActExecution()
        {

			string restartFolderPath = Path.Combine(MainSettings.LocalSqlSettings.SelfUpdatePath, MainSettings.Constants.MU_RESTART_FOLDER_NAME);
			FilesWorks.ClearDirectory(MainSettings.LocalSqlSettings.SelfUpdatePath);
			Directory.CreateDirectory(MainSettings.LocalSqlSettings.SelfUpdatePath);
			Directory.CreateDirectory(restartFolderPath);

			var resRestart = FtpWorks.DownloadFilesFromFtpFolder(MainSettings.LocalSqlSettings.SelfUpdateFtpServer
												, MainSettings.LocalSqlSettings.SelfUpdateFtpUser
												, MainSettings.LocalSqlSettings.SelfUpdateFtpPassword
												, Path.Combine(MainSettings.LocalSqlSettings.SelfUpdateFtpPath, MainSettings.Constants.MU_RESTART_FOLDER_NAME)
												, restartFolderPath
												, "*");


			if (!resRestart.IsComplete)
			{
				throw new Exception($"Restart part: {resRestart.Message}");
			}

			var resAgent = FtpWorks.DownloadFilesFromFtpFolder(MainSettings.LocalSqlSettings.SelfUpdateFtpServer
												, MainSettings.LocalSqlSettings.SelfUpdateFtpUser
												, MainSettings.LocalSqlSettings.SelfUpdateFtpPassword
												, MainSettings.LocalSqlSettings.SelfUpdateFtpPath
												, MainSettings.LocalSqlSettings.SelfUpdatePath
												, "*");


			if (!resAgent.IsComplete)
			{
				throw new Exception($"Agent part: {resAgent.Message}");
			}
		}
    }
}
