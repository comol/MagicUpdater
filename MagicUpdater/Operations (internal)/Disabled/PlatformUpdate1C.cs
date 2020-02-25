using System;
using System.Linq;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using MagicUpdater.Actions;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Threading;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.Abstract;

namespace MagicUpdater.Operations
{
	public class PlatformUpdate1C : Operation
	{
		private static readonly string FTP_SERVER = "";
		private static readonly string LOGIN = "";
		private static readonly string PASSWORD = "";
		private static readonly string ZIP_FILE_PATH = @"C:\Distrib\AdminInstall1C.zip";
		private static readonly string NEW_PLATFORM_LOCAL_PATH = @"C:\SystemUtils\AdminInstall1C";
		private static readonly string CLIENT_FOLDER = "Client";
		private static readonly string START_FILE_NAME = "1cestart.cfg";
		private static readonly string LOCAL_BIN_FOLDER_PATH = @"C:\Program Files (x86)\1cv8\8.3.9.1850\bin";
		private static readonly string BACKBAS_DLL = "backbas.dll";

		private static readonly string REMOTE_PC_LOGIN = "";
		private static readonly string DOMAIN = "";
		private static readonly string REMOTE_PC_PASSWORD = "";
		private static readonly string REMOTE_PC_PATH_X86 = @"c$\Program Files (x86)\1cv8\";
		private static readonly string REMOTE_PC_PATH = @"c$\Program Files\1cv8\";

		private static readonly string FILE_MD5 = "";
		private static readonly string FILE_PATH = @"C:\Distrib\AdminInstall1C.zip";

		private static readonly string OLD_PLATFORM = "8.3.7.1970"; //Как пример
		private static readonly string NEW_PLATFORM = "8.3.9.1850"; //Как пример

		string WindowsStartFolderPath => Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData), @"Application Data\1C\1CEStart");

		string NewPlatformSetupFilePath => Path.Combine(NEW_PLATFORM_LOCAL_PATH, NEW_PLATFORM, "setup.exe");

		public PlatformUpdate1C(int? operationId) : base(operationId)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{


			using (var md5 = MD5.Create())
			{
				using (var stream = File.OpenRead(FILE_PATH))
				{
					if (md5.ComputeHash(stream).ToHex(true) != FILE_MD5)
					{
						throw new Exception("Архив платформы отсутствует");
					}
				}
			}

			FilesWorks.DecompressZip(ZIP_FILE_PATH, NEW_PLATFORM_LOCAL_PATH);

			if (!Directory.Exists(NEW_PLATFORM_LOCAL_PATH))
			{
				throw new Exception($"Путь не существует {NEW_PLATFORM_LOCAL_PATH}");
			}
			if (Directory.GetFiles(NEW_PLATFORM_LOCAL_PATH).Count() == 0)
			{
				throw new Exception($"Папка пустая {NEW_PLATFORM_LOCAL_PATH}");
			}

			File.Delete(Path.Combine(WindowsStartFolderPath, START_FILE_NAME));
			File.Copy(Path.Combine(NEW_PLATFORM_LOCAL_PATH, START_FILE_NAME), Path.Combine(WindowsStartFolderPath, START_FILE_NAME), true);

			Process process = Process.Start($"{NewPlatformSetupFilePath}", "/S");

			if (!process.HasExited)
				process.WaitForExit(60000 * 15);

			if (!process.HasExited)
				throw new Exception("Процесс установки платформы не завершился после 15 минут ожидания. Обновление платформы не выполнено.");

			Thread.Sleep(30000);

			if (new KillProcess1C(this.Id).ActRun())
			{
			}

			RegistryKey myKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\ControlSet001\\services\\1C:Enterprise 8.3 Server Agent", true);
			if (myKey != null)
			{
				string path = Convert.ToString(myKey.GetValue("ImagePath"));
				path = path.Replace(OLD_PLATFORM, NEW_PLATFORM);
				myKey.SetValue("ImagePath", path, RegistryValueKind.String);
			}
			myKey.Close();

			File.Copy(Path.Combine(NEW_PLATFORM_LOCAL_PATH, BACKBAS_DLL), Path.Combine(LOCAL_BIN_FOLDER_PATH, BACKBAS_DLL), true);

			var computersList = NetWork.GetNetworkComputerNames();
			foreach (var computer in computersList)
			{
				var tryCheckExists86 = FilesWorks.IsRemoteFolderExists(REMOTE_PC_LOGIN, computer, REMOTE_PC_PASSWORD, Path.Combine($"\\\\{computer}", REMOTE_PC_PATH_X86));
				var tryCheckExists = FilesWorks.IsRemoteFolderExists(REMOTE_PC_LOGIN, computer, REMOTE_PC_PASSWORD, Path.Combine($"\\\\{computer}", REMOTE_PC_PATH));
				if (tryCheckExists86.IsComplete)
				{
					var res = FilesWorks.CopyFolderToRemotePc(REMOTE_PC_LOGIN, computer, REMOTE_PC_PASSWORD, Path.Combine(NEW_PLATFORM_LOCAL_PATH, CLIENT_FOLDER), Path.Combine($"\\\\{computer}", REMOTE_PC_PATH_X86));
					if (!res.IsComplete)
					{
						AddErrorMessage($"Ошибка копирования папки Client на компьютер {computer}");
					}
				}
				else if (tryCheckExists.IsComplete)
				{
					var res = FilesWorks.CopyFolderToRemotePc(REMOTE_PC_LOGIN, computer, REMOTE_PC_PASSWORD, Path.Combine(NEW_PLATFORM_LOCAL_PATH, CLIENT_FOLDER), Path.Combine($"\\\\{computer}", REMOTE_PC_PATH));
					if (!res.IsComplete)
					{
						AddErrorMessage($"Ошибка копирования папки Client на компьютер {computer}");
					}
				}
				else
				{
					if (!tryCheckExists86.IsComplete)
					{
						AddErrorMessage(tryCheckExists86.Message);
					}

					if (!tryCheckExists.IsComplete)
					{
						AddErrorMessage(tryCheckExists.Message);
					}
				}
			}
		}
	}
}
