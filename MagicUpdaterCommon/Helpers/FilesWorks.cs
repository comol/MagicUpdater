using Ionic.Zip;
using MagicUpdaterCommon.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

namespace MagicUpdaterCommon.Helpers
{
	#region TryResult
	public class TryCopy : TryResult
	{
		public TryCopy(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}

	public class TryCheckExists : TryResult
	{
		public TryCheckExists(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}

	public class TryDeleteDirectoryFull : TryResult
	{
		public TryDeleteDirectoryFull(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}

	public class TryClearDirectory : TryResult
	{
		public TryClearDirectory(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}
	#endregion TryResult

	public static class FilesWorks
	{
		public static string[] GetFilesPaths(string path, string[] searchPatterns, SearchOption searchOption)
		{
			List<string> files = new List<string>();
			foreach (string sp in searchPatterns)
			{
				files.AddRange(Directory.GetFiles(path, sp, searchOption));
			}
			return files.ToArray();
		}

		public static FileInfo[] GetFiles(string path, string[] searchPatterns, SearchOption searchOption)
		{
			List<FileInfo> files = new List<FileInfo>();
			DirectoryInfo di = new DirectoryInfo(path);
			foreach (string sp in searchPatterns)
			{
				files.AddRange(di.GetFiles(sp, searchOption));
			}
			return files.ToArray();
		}

		public static TryDeleteDirectoryFull DeleteDirectoryFull(string path)
		{
			if(!Directory.Exists(path))
			{
				return new TryDeleteDirectoryFull();
			}

			try
			{
				DirectoryInfo di = new DirectoryInfo(path);
				foreach (FileInfo file in di.GetFiles())
				{
					file.Delete();
				}
				foreach (DirectoryInfo dir in di.GetDirectories())
				{
					dir.Delete(true);
				}

				di.Delete(true);
				return new TryDeleteDirectoryFull();
			}
			catch(Exception ex)
			{
				return new TryDeleteDirectoryFull(false, ex.Message.ToString());
			}
		}

		public static TryClearDirectory ClearDirectory(string path)
		{
			if (!Directory.Exists(path))
			{
				return new TryClearDirectory();
			}

			try
			{
				DirectoryInfo di = new DirectoryInfo(path);
				foreach (FileInfo file in di.GetFiles())
				{
					file.Delete();
				}
				foreach (DirectoryInfo dir in di.GetDirectories())
				{
					dir.Delete(true);
				}

				return new TryClearDirectory();
			}
			catch (Exception ex)
			{
				return new TryClearDirectory(false, ex.Message.ToString());
			}
		}

		public static void DecompressZip(string zipPath, string extractPath)
		{
			ZipFile zip = ZipFile.Read(zipPath);
			Directory.CreateDirectory(extractPath);
			foreach (ZipEntry e in zip)
			{
				e.Extract(extractPath, ExtractExistingFileAction.OverwriteSilently);
			}
		}

		[DllImport("advapi32.DLL", SetLastError = true)]
		public static extern int LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

		public static TryCheckExists IsRemoteFolderExists(string login, string domain, string password, string destinationPath)
		{
			IntPtr admin_token = default(IntPtr);
			WindowsIdentity wid_current = WindowsIdentity.GetCurrent();
			WindowsIdentity wid_admin = null;
			WindowsImpersonationContext wic = null;
			try
			{
				if (LogonUser(login, domain, password, 9, 0, ref admin_token) != 0)
				{
					wid_admin = new WindowsIdentity(admin_token);
					wic = wid_admin.Impersonate();

					if (Directory.Exists(destinationPath))
					{
						return new TryCheckExists();
					}
					else
					{
						return new TryCheckExists(false, "false");
					}

				}
				else
				{
					return new TryCheckExists(false, "Неизвестная ошибка. Копирование не удалось.");
				}
			}
			catch (System.Exception se)
			{
				int ret = Marshal.GetLastWin32Error();
				return new TryCheckExists(false, $"LastWin32Error: {ret}. Text: {se.Message}");
			}
			finally
			{
				if (wic != null)
				{
					wic.Undo();
				}
			}
		}

		public static TryCopy CopyFolderToRemotePc(string login, string domain, string password, string sourcePath, string destinationPath)
		{
			IntPtr admin_token = default(IntPtr);
			WindowsIdentity wid_current = WindowsIdentity.GetCurrent();
			WindowsIdentity wid_admin = null;
			WindowsImpersonationContext wic = null;
			try
			{
				if (LogonUser(login, domain, password, 9, 0, ref admin_token) != 0)
				{
					wid_admin = new WindowsIdentity(admin_token);
					wic = wid_admin.Impersonate();

					//Now Create all of the directories
					foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
						Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));

					//Copy all the files & Replaces any files with the same name
					foreach (string newPath in Directory.GetFiles(sourcePath, "*.*",
						SearchOption.AllDirectories))
						File.Copy(newPath, newPath.Replace(sourcePath, destinationPath), true);

					return new TryCopy();
				}
				else
				{
					return new TryCopy(false, "Неизвестная ошибка. Копирование не удалось.");
				}
			}
			catch (System.Exception se)
			{
				int ret = Marshal.GetLastWin32Error();
				return new TryCopy(false, $"LastWin32Error: {ret}. Text: {se.Message}");
			}
			finally
			{
				if (wic != null)
				{
					wic.Undo();
				}
			}
		}
	}
}
