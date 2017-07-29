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
			//TODO расхардкодить пути обновления
			//         using (Ftp ftp = new Ftp())
			//         {
			//	//            ftp.Connect("mskftp.sela.ru");  // or ConnectSSL for SSL
			//	//            ftp.Login("cis_obmen", "cisobmen836");
			//	//try
			//	//{
			//	//	Directory.CreateDirectory(MainSettings.SqlSettings.SelfUpdatePath);
			//	//	ftp.DownloadFiles("MagicUpdater", MainSettings.SqlSettings.SelfUpdatePath,
			//	//		new RemoteSearchOptions("*", true));
			//	//}
			//	//finally
			//	//{
			//	//	if(ftp.Connected)
			//	//		ftp.Close();
			//	//}

			//	//ftp.Connect("mskftp.sela.ru");  // or ConnectSSL for SSL
			//	//ftp.Login("cis_obmen", "cisobmen836");
			//	//try
			//	//{
			//	//	Directory.CreateDirectory(MainSettings.LocalSqlSettings.SelfUpdatePath);
			//	//	ftp.DownloadFiles("MagicUpdaterTest", MainSettings.LocalSqlSettings.SelfUpdatePath,
			//	//		new RemoteSearchOptions("*", true));
			//	//}
			//	//finally
			//	//{
			//	//	if (ftp.Connected)
			//	//		ftp.Close();
			//	//}



			//	//ftp.Connect(MainSettings.LocalSqlSettings.SelfUpdateFtpServer);  // or ConnectSSL for SSL
			//	//ftp.Login(MainSettings.LocalSqlSettings.SelfUpdateFtpUser, MainSettings.LocalSqlSettings.SelfUpdateFtpPassword);
			//	//try
			//	//{
			//	//	Directory.CreateDirectory(MainSettings.LocalSqlSettings.SelfUpdatePath);
			//	//	ftp.DownloadFiles(MainSettings.LocalSqlSettings.SelfUpdateFtpPath, MainSettings.LocalSqlSettings.SelfUpdatePath,
			//	//		new RemoteSearchOptions("*", true));
			//	//}
			//	//finally
			//	//{
			//	//	if (ftp.Connected)
			//	//		ftp.Close();
			//	//}
			//}

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

			//var resRestart = FtpWorks.UpdateFilesInFolder(MainSettings.LocalSqlSettings.SelfUpdateFtpServer
			//									, MainSettings.LocalSqlSettings.SelfUpdateFtpUser
			//									, MainSettings.LocalSqlSettings.SelfUpdateFtpPassword
			//									, Path.Combine(MainSettings.LocalSqlSettings.SelfUpdateFtpPath, MainSettings.Constants.MU_RESTART_FOLDER_NAME)
			//									, restartFolderPath
			//									, "*");

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

			//var resAgent = FtpWorks.UpdateFilesInFolder(MainSettings.LocalSqlSettings.SelfUpdateFtpServer
			//									, MainSettings.LocalSqlSettings.SelfUpdateFtpUser
			//									, MainSettings.LocalSqlSettings.SelfUpdateFtpPassword
			//									, MainSettings.LocalSqlSettings.SelfUpdateFtpPath
			//									, MainSettings.LocalSqlSettings.SelfUpdatePath
			//									, "*");

			if (!resAgent.IsComplete)
			{
				throw new Exception($"Agent part: {resAgent.Message}");
			}
		}
    }
}
