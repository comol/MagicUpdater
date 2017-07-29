using System;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.Helpers;

namespace MagicUpdater.Operations
{
	public class DownloadPaltform1C : Operation
	{
		private static readonly string WGET = @"wget";
		private static readonly string WGET_EXE = @"wget.exe";
		private static readonly string DISTRIB_PATH = @"C:\Distrib";
		private static readonly string LOGIN = "cis_obmen";
		private static readonly string PASSWORD = "cisobmen836";
		private static readonly string FTP_PATH = @"ftp://mskftp.sela.ru/AdminInstallZip1C/";
		private static readonly string FTP_PATH_PART = @"mskftp.sela.ru/AdminInstallZip1C/AdminInstall1C.zip";
		private static readonly string ZIP_FILE_NAME = "AdminInstall1C.zip";

		public DownloadPaltform1C(int? operationId) : base(operationId)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			if (!Directory.Exists(DISTRIB_PATH))
			{
				Directory.CreateDirectory(DISTRIB_PATH);
			}

			if (!Directory.Exists(Path.Combine(DISTRIB_PATH, WGET)))
			{
				Directory.CreateDirectory(Path.Combine(DISTRIB_PATH, WGET));
			}

			if (!File.Exists(Path.Combine(DISTRIB_PATH, WGET, WGET_EXE)))
			{
				var res = FtpWorks.DownloadFileFromFtpOld(Path.Combine(DISTRIB_PATH, WGET, WGET_EXE), Path.Combine(FTP_PATH, WGET_EXE), LOGIN, PASSWORD);
				if(!res.IsComplete)
				{
					throw new Exception(res.Message);
				}
			}

			var process = Process.Start($"{Path.Combine(DISTRIB_PATH, WGET, WGET_EXE)}", $" -c -nc ftp://{LOGIN}:{PASSWORD}@{FTP_PATH_PART} - P \"{DISTRIB_PATH}\"");
		}
	}
}
