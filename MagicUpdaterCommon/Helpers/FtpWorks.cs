using MagicUpdaterCommon.Common;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace MagicUpdaterCommon.Helpers
{
	public class TryDownloadFtp : TryResult
	{
		public TryDownloadFtp(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}

	public class TryGetMd5ViaFtp : TryResult
	{
		public TryGetMd5ViaFtp(string value = null, bool isComplete = true, string message = "") : base(isComplete, message)
		{
			Value = value;
		}

		public string Value { get; private set; }
	}

	public class TryDownloadFilesFromFtpFolder : TryResult
	{
		public TryDownloadFilesFromFtpFolder(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}

	public class TryUpdateFilesInFolder : TryResult
	{
		public TryUpdateFilesInFolder(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}

	public class TryGetFtpFileMd5 : TryResult
	{
		public TryGetFtpFileMd5(bool isComplete, string value, string message = "") : base(isComplete, message)
		{
			Value = value;
		}

		public string Value { get; private set; }
	}

	public class TryGetFtpFileVersion : TryResult
	{
		public TryGetFtpFileVersion(bool isComplete = true, string message = "", Version ver = null) : base(isComplete, message)
		{
			Ver = ver;
		}

		public Version Ver { get; private set; }
	}

	public class TryUploadFilesFromFolderToFtp : TryResult
	{
		public TryUploadFilesFromFolderToFtp(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}

	public class TryUploadFileToFtp : TryResult
	{
		public TryUploadFileToFtp(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}

	public static class FtpWorks
	{
		#region fileExtensionsList
		private static string[] fileExtensionsList =
		{
			".a",
			".asm",
			".asp",
			".awk",
			".bat",
			".bmp",
			".btm",
			".BTM",
			".c",
			".class",
			".cmd",
			".CPP",
			".csv",
			".cur",
			".cxx",
			".CXX",
			".db",
			".def",
			".DES",
			".dlg",
			".dll",
			".don",
			".dpc",
			".dpj",
			".dtd",
			".dump",
			".dxp",
			".eng",
			".exe",
			".flt",
			".fmt",
			".font",
			".fp",
			".ft",
			".gif",
			".h",
			".H",
			".hdb",
			".hdl",
			".hid",
			".hpp",
			".hrc",
			".HRC",
			".html",
			".hxx",
			".Hxx",
			".HXX",
			".ico",
			".idl",
			".IDL",
			".ih",
			".ilb",
			".inc",
			".inf",
			".ini",
			".inl",
			".ins",
			".java",
			".jar",
			".jnl",
			".jpg",
			".js",
			".jsp",
			".kdelnk",
			".l",
			".lgt",
			".lib",
			".lin",
			".ll",
			".LN3",
			".lng",
			".lnk",
			".lnx",
			".LOG",
			".lst",
			".lst",
			".mac",
			".MacOS",
			".map",
			".mk",
			".MK",
			".mod",
			".NT2",
			".o",
			".obj",
			".par",
			".pfa",
			".pfb",
			".pl",
			".PL",
			".plc",
			".pld",
			".PLD",
			".plf",
			".pm",
			".pmk",
			".pre",
			".PRJ",
			".prt",
			".PS",
			".ptr",
			".r",
			".rc",
			".rdb",
			".res",
			".s",
			".S",
			".sbl",
			".scp",
			".scr",
			".sda",
			".sdb",
			".sdc",
			".sdd",
			".sdg",
			".sdm",
			".sds",
			".sdv",
			".sdw",
			".sdi",
			".seg",
			".SEG",
			".Set",
			".sgl",
			".sh",
			".sid",
			".smf",
			".sms",
			".so ",
			".sob",
			".soh",
			".sob",
			".soc",
			".sod",
			".soe",
			".sog",
			".soh",
			".src",
			".srs",
			".SSLeay",
			".Static",
			".tab",
			".TFM",
			".th",
			".tpt",
			".tsc",
			".ttf",
			".TTF",
			".txt",
			".TXT",
			".unx",
			".UNX",
			".urd",
			".url",
			".VMS",
			".vor",
			".W32",
			".wav",
			".wmf",
			".xml",
			".xpm",
			".xrb",
			".y",
			".yxx",
			".zip",
		};
		#endregion fileExtensionsList

		private const string FTP_FOLDER_SIGN = "d";
		private const string FTP_FILE_SIGN = "-";

		//private const string FTP_FOLDER_SIGN = "drwxrwxrwx";
		//private const string FTP_FILE_SIGN = "-rw-rw-rw-";
		private const string FTP_PREFIX = "ftp://";

		public static TryUploadFilesFromFolderToFtp UploadFilesFromFolderToFtp(string server,
														string login,
														string password,
														string ftpFolder,
														string localFolder,
														params string[] searchPattern)
		{
			try
			{
				if (!server.Contains(FTP_PREFIX))
				{
					server = $"{FTP_PREFIX}{server}";
				}

				if (searchPattern.Length == 0 || searchPattern.Contains("*"))
				{
					try
					{
						using (WebClient request = new WebClient())
						{
							foreach (string filePath in Directory.GetFiles(localFolder))
							{
								string ftpFilePath = Path.Combine(server, ftpFolder, Path.GetFileName(filePath));
								ftpFilePath = ftpFilePath.Replace("\\", "/");
								request.Credentials = new NetworkCredential(login, password);

								request.UploadFile(ftpFilePath, Path.Combine(localFolder, filePath));
							}
						}
					}
					catch (Exception ex)
					{
						return new TryUploadFilesFromFolderToFtp(false, ex.ToString());
					}
				}
				else
				{
					try
					{
						using (WebClient request = new WebClient())
						{
							foreach (string filePath in Directory.GetFiles(localFolder))
							{
								if (searchPattern.Contains(Path.GetExtension(filePath)))
								{
									string ftpFilePath = Path.Combine(server, ftpFolder, Path.GetFileName(filePath));
									ftpFilePath = ftpFilePath.Replace("\\", "/");
									request.Credentials = new NetworkCredential(login, password);

									request.UploadFile(ftpFilePath, Path.Combine(localFolder, filePath));
								}
							}
						}
					}
					catch (Exception ex)
					{
						return new TryUploadFilesFromFolderToFtp(false, ex.ToString());
					}
				}
			}
			catch (Exception ex)
			{
				return new TryUploadFilesFromFolderToFtp(false, ex.ToString());
			}

			return new TryUploadFilesFromFolderToFtp();
		}

		public static TryUploadFileToFtp UploadFileToFtp(string localDirectory,
											string server,
											string login,
											string password,
											string ftpFolder,
											string fileName)
		{
			try
			{
				if (!server.Contains(FTP_PREFIX))
				{
					server = $"{FTP_PREFIX}{server}";
				}
				string ftpFilePath = Path.Combine(server, ftpFolder, fileName);
				ftpFilePath = ftpFilePath.Replace("\\", "/");

				using (WebClient request = new WebClient())
				{
					request.Credentials = new NetworkCredential(login, password);

					request.UploadFile(ftpFilePath, Path.Combine(localDirectory, fileName));
				}
				return new TryUploadFileToFtp();
			}
			catch (Exception ex)
			{
				return new TryUploadFileToFtp(false, ex.Message.ToString());
			}
		}

		public static TryDownloadFilesFromFtpFolder DownloadFilesFromFtpFolder(string server,
																				string login,
																				string password,
																				string ftpFolder,
																				string localFolder,
																				params string[] searchPattern)
		{
			try
			{
				if (!Directory.Exists(localFolder))
				{
					Directory.CreateDirectory(localFolder);
				}

				if (!server.Contains(FTP_PREFIX))
				{
					server = $"{FTP_PREFIX}{server}";
				}

				if (searchPattern.Length == 0 || searchPattern.Contains("*"))
				{
					try
					{
						using (WebClient request = new WebClient())
						{
							foreach (string fileName in GetFilesList(server, login, password, ftpFolder))
							{

								string ftpFilePath = Path.Combine(server, ftpFolder, fileName);
								ftpFilePath = ftpFilePath.Replace("\\", "/");
								request.Credentials = new NetworkCredential(login, password);

								byte[] fileData = request.DownloadData(ftpFilePath);

								using (FileStream file = File.Create(Path.Combine(localFolder, fileName)))
								{
									file.Write(fileData, 0, fileData.Length);
									file.Close();
								}
							}
						}
					}
					catch (Exception ex)
					{
						return new TryDownloadFilesFromFtpFolder(false, ex.ToString());
					}
				}
				else
				{
					try
					{
						using (WebClient request = new WebClient())
						{
							foreach (string fileName in GetFilesList(server, login, password, ftpFolder))
							{
								if (searchPattern.Contains(Path.GetExtension(fileName)))
								{
									string ftpFilePath = Path.Combine(server, ftpFolder, fileName);
									ftpFilePath = ftpFilePath.Replace("\\", "/");
									request.Credentials = new NetworkCredential(login, password);

									byte[] fileData = request.DownloadData(ftpFilePath);

									using (FileStream file = File.Create(Path.Combine(localFolder, fileName)))
									{
										file.Write(fileData, 0, fileData.Length);
										file.Close();
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						return new TryDownloadFilesFromFtpFolder(false, ex.ToString());
					}
				}
			}
			catch (Exception ex)
			{
				return new TryDownloadFilesFromFtpFolder(false, ex.ToString());
			}

			return new TryDownloadFilesFromFtpFolder();
		}

		public static TryDownloadFilesFromFtpFolder DownloadFilesFromFtpFolder(string ftpFolder,
																				string localFolder,
																				params string[] searchPattern)
		{
			try
			{
				return DownloadFilesFromFtpFolder(MainSettings.LocalSqlSettings.SelfUpdateFtpServer
												, MainSettings.LocalSqlSettings.SelfUpdateFtpUser
												, MainSettings.LocalSqlSettings.SelfUpdateFtpPassword
												, ftpFolder
												, localFolder
												, searchPattern);
			}
			catch (Exception ex)
			{
				return new TryDownloadFilesFromFtpFolder(false, ex.ToString());
			}
		}


		//public static TryDownloadFtp DownloadFilesFromFtpFolderOld(string server,
		//														string login,
		//														string password,
		//														string ftpFolder,
		//														string localFolder)
		//{
		//	using (Ftp ftp = new Ftp())
		//	{
		//		try
		//		{
		//			ftp.Connect(server);
		//			ftp.Login(login, password);
		//			try
		//			{
		//				Directory.CreateDirectory(localFolder);
		//				ftp.DownloadFiles(ftpFolder, localFolder,
		//					new RemoteSearchOptions("*", true));
		//			}
		//			finally
		//			{
		//				if (ftp.Connected)
		//					ftp.Close();
		//			}

		//			return new TryDownloadFtp();
		//		}
		//		catch (Exception ex)
		//		{
		//			return new TryDownloadFtp(false, ex.Message.ToString());
		//		}
		//	}
		//}

		public static TryDownloadFtp DownloadFileFromFtp(string localDirectory,
															string server,
															string login,
															string password,
															string ftpFolder,
															string fileName)
		{
			try
			{
				if (!server.Contains(FTP_PREFIX))
				{
					server = $"{FTP_PREFIX}{server}";
				}
				string ftpFilePath = Path.Combine(server, ftpFolder, fileName);
				ftpFilePath = ftpFilePath.Replace("\\", "/");

				using (WebClient request = new WebClient())
				{
					request.Credentials = new NetworkCredential(login, password);

					byte[] fileData = request.DownloadData(ftpFilePath);

					using (FileStream file = File.Create(Path.Combine(localDirectory, fileName)))
					{
						file.Write(fileData, 0, fileData.Length);
						file.Close();
					}
				}
				return new TryDownloadFtp();
			}
			catch (Exception ex)
			{
				return new TryDownloadFtp(false, ex.Message.ToString());
			}
		}

		public static TryDownloadFtp DownloadFileFromFtp(string localDirectory,
															string ftpFolder,
															string fileName)
		{
			try
			{
				return DownloadFileFromFtp(localDirectory
											, MainSettings.LocalSqlSettings.SelfUpdateFtpServer
											, MainSettings.LocalSqlSettings.SelfUpdateFtpUser
											, MainSettings.LocalSqlSettings.SelfUpdateFtpPassword
											, ftpFolder
											, fileName);
			}
			catch (Exception ex)
			{
				return new Helpers.TryDownloadFtp(false, ex.ToString());
			}
		}

		public static TryGetFtpFileMd5 GetFtpFileMd5(string server,
													string login,
													string password,
													string ftpFolder,
													string fileName)
		{
			try
			{
				if (!server.Contains(FTP_PREFIX))
				{
					server = $"{FTP_PREFIX}{server}";
				}
				string ftpFilePath = Path.Combine(server, ftpFolder, fileName);
				ftpFilePath = ftpFilePath.Replace("\\", "/");
				string md5 = "";
				using (WebClient request = new WebClient())
				{
					request.Credentials = new NetworkCredential(login, password);

					byte[] fileData = request.DownloadData(ftpFilePath);

					string filePath = Path.Combine(Path.GetTempPath(), fileName);

					using (FileStream file = File.Create(filePath))
					{
						file.Write(fileData, 0, fileData.Length);
						file.Close();
					}

					md5 = MD5Works.GetFileHash(filePath);

					File.Delete(filePath);
				}

				return new TryGetFtpFileMd5(true, md5);
			}
			catch (Exception ex)
			{
				return new TryGetFtpFileMd5(false, "", ex.Message.ToString());
			}
		}

		public static TryGetFtpFileVersion GetFtpFileVersion(string server,
															string login,
															string password,
															string ftpFolder,
															string fileName)
		{
			try
			{
				if (!server.Contains(FTP_PREFIX))
				{
					server = $"{FTP_PREFIX}{server}";
				}
				string ftpFilePath = Path.Combine(server, ftpFolder, fileName);
				ftpFilePath = ftpFilePath.Replace("\\", "/");
				Version ver;
				using (WebClient request = new WebClient())
				{
					request.Credentials = new NetworkCredential(login, password);

					byte[] fileData = request.DownloadData(ftpFilePath);

					string filePath = Path.Combine(Path.GetTempPath(), fileName);

					using (FileStream file = File.Create(filePath))
					{
						file.Write(fileData, 0, fileData.Length);
						file.Close();
					}

					if (!Version.TryParse(FileVersionInfo.GetVersionInfo(filePath).FileVersion, out ver))
						ver = null;

					File.Delete(filePath);
				}

				return new TryGetFtpFileVersion(true, "", ver);
			}
			catch (Exception ex)
			{
				return new TryGetFtpFileVersion(false, ex.Message.ToString());
			}
		}

		[System.Obsolete("Этот метод устарел, используйте DownloadFileFromFtp")]
		public static TryDownloadFtp DownloadFileFromFtpOld(string localFilePath, string ftpFilePath, string userName, string password)
		{
			try
			{
				ftpFilePath = ftpFilePath.Replace("\\", "/");
				if (!ftpFilePath.Contains(FTP_PREFIX))
				{
					ftpFilePath = Path.Combine(FTP_PREFIX, ftpFilePath);
				}

				using (WebClient request = new WebClient())
				{
					request.Credentials = new NetworkCredential(userName, password);

					byte[] fileData = request.DownloadData(ftpFilePath);

					using (FileStream file = File.Create(localFilePath))
					{
						file.Write(fileData, 0, fileData.Length);
						file.Close();
					}
				}
				return new TryDownloadFtp();
			}
			catch (Exception ex)
			{
				return new TryDownloadFtp(false, ex.Message.ToString());
			}
		}

		public static List<string> GetFolderList(string server,
												string login,
												string password,
												string ftpFolder)
		{
			server = server.Replace("ftp://", "");
			FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create($"ftp://{server}//{ftpFolder}");
			ftpRequest.Credentials = new NetworkCredential(login, password);
			ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
			FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
			StreamReader streamReader = new StreamReader(response.GetResponseStream());

			FtpWebRequest ftpRequestFolder = (FtpWebRequest)WebRequest.Create($"ftp://{server}//{ftpFolder}");
			ftpRequestFolder.Credentials = new NetworkCredential(login, password);
			ftpRequestFolder.Method = WebRequestMethods.Ftp.ListDirectory;
			FtpWebResponse responseFolder = (FtpWebResponse)ftpRequestFolder.GetResponse();
			StreamReader streamReaderFolder = new StreamReader(responseFolder.GetResponseStream());

			List<string> folders = new List<string>();

			string line = streamReader.ReadLine();
			string lineFolder = streamReaderFolder.ReadLine();
			while (!string.IsNullOrEmpty(line))
			{
				List<string> split = line.Split(' ').ToList();

				for (int i = split.Count - 1; i >= 0; i--)
				{
					if (split[i] == "" || split[i] == " ")
					{
						split.Remove(split[i]);
					}
				}

				if (split[0] == FTP_FOLDER_SIGN && split.Last() != "." && split.Last() != "..")
				{
					folders.Add(lineFolder);
				}

				line = streamReader.ReadLine();
				if (!(split[0] == FTP_FOLDER_SIGN && (split.Last() == "." || split.Last() == "..")))
				{
					lineFolder = streamReaderFolder.ReadLine();
				}
			}

			return folders;
		}

		public static TryUpdateFilesInFolder UpdateFilesInFolder(string server,
												string login,
												string password,
												string ftpFolder,
												string localFolder,
												params string[] searchPattern)
		{
			try
			{
				if (!Directory.Exists(localFolder))
				{
					Directory.CreateDirectory(localFolder);
				}

				if (searchPattern.Length == 0 || searchPattern.Contains("*"))
				{
					foreach (string file in GetFilesList(server, login, password, ftpFolder))
					{
						if (!IsFilesLastModifiedDateIdentical(server, login, password, ftpFolder, file, Path.Combine(localFolder, file)))
						{
							var res = DownloadFileFromFtp(localFolder, server, login, password, ftpFolder, file);
							if (!res.IsComplete)
							{
								return new TryUpdateFilesInFolder(false, res.Message);
							}
						}
					}
				}
				else
				{
					foreach (string file in GetFilesList(server, login, password, ftpFolder))
					{
						if (searchPattern.Contains(Path.GetExtension(file)))
						{
							if (!IsFilesLastModifiedDateIdentical(server, login, password, ftpFolder, file, Path.Combine(localFolder, file)))
							{
								var res = DownloadFileFromFtp(localFolder, server, login, password, ftpFolder, file);
								if (!res.IsComplete)
								{
									return new TryUpdateFilesInFolder(false, res.Message);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				return new TryUpdateFilesInFolder(false, ex.ToString());
			}

			return new TryUpdateFilesInFolder();
		}

		public static bool IsFilesLastModifiedDateIdentical(string server,
											string login,
											string password,
											string ftpFolder,
											string ftpFileName,
											string localFileFullPath)
		{
			if (!File.Exists(localFileFullPath))
			{
				return false;
			}

			DateTime localDateTime = File.GetLastWriteTime(localFileFullPath);
			DateTime? ftpDateTime = GetFileLastModifiedDate(server, login, password, ftpFolder, ftpFileName);
			return Extensions.IsNullableDateTimeIdentical(localDateTime, ftpDateTime);
		}

		public static DateTime? GetFileLastModifiedDate(string server,
												string login,
												string password,
												string ftpFolder,
												string fileName)
		{
			try
			{
				server = server.Replace("ftp://", "");
				FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create($"ftp://{server}//{ftpFolder}//{fileName}");
				ftpRequest.Credentials = new NetworkCredential(login, password);
				ftpRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;
				FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
				return response.LastModified;
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToHdd(ex.ToString());
				return null;
			}
		}

		public static List<string> GetFilesList(string server,
												string login,
												string password,
												string ftpFolder,
												params string[] searchPattern)
		{
			server = server.Replace("ftp://", "");
			FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create($"ftp://{server}//{ftpFolder}");
			ftpRequest.Credentials = new NetworkCredential(login, password);
			ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
			FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
			StreamReader streamReader = new StreamReader(response.GetResponseStream());

			FtpWebRequest ftpRequestFile = (FtpWebRequest)WebRequest.Create($"ftp://{server}//{ftpFolder}");
			ftpRequestFile.Credentials = new NetworkCredential(login, password);
			ftpRequestFile.Method = WebRequestMethods.Ftp.ListDirectory;
			FtpWebResponse responseFile = (FtpWebResponse)ftpRequestFile.GetResponse();
			StreamReader streamReaderFile = new StreamReader(responseFile.GetResponseStream());

			List<string> files = new List<string>();

			string line = streamReader.ReadLine();
			List<string> listDirectoryDetails = new List<string>();
			while (!string.IsNullOrEmpty(line))
			{
				List<string> split = line.Split(' ').ToList();

				for (int i = split.Count - 1; i >= 0; i--)
				{
					if (split[i] == "" || split[i] == " ")
					{
						split.Remove(split[i]);
					}
				}

				if (!(split[0].Substring(0, 1) == FTP_FOLDER_SIGN && (split.Last() == "." || split.Last() == "..")))
				{
					listDirectoryDetails.Add(line);
				}
				
				line = streamReader.ReadLine();
			}

			string lineFile = Path.GetFileName(streamReaderFile.ReadLine());
			List<string> listDirectory = new List<string>();
			while (!string.IsNullOrEmpty(lineFile))
			{
				listDirectory.Add(lineFile);
				lineFile = Path.GetFileName(streamReaderFile.ReadLine());
			}

			for (int i = 0; i < listDirectoryDetails.Count; i++)
			{
				if (listDirectoryDetails[i].Substring(0, 1) != FTP_FOLDER_SIGN)
				{

					if (searchPattern.Length == 0 || searchPattern.Contains("*"))
					{
						files.Add(listDirectory[i]);
					}
					else
					{
						if (searchPattern.Contains(Path.GetExtension(listDirectory[i])))
						{
							files.Add(listDirectory[i]);
						}
					}
				}
			}

			return files;
		}

		public static List<string> GetFilesListOld(string server,
												string login,
												string password,
												string ftpFolder,
												params string[] searchPattern)
		{
			server = server.Replace("ftp://", "");
			FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create($"ftp://{server}//{ftpFolder}");
			ftpRequest.Credentials = new NetworkCredential(login, password);
			ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
			FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
			StreamReader streamReader = new StreamReader(response.GetResponseStream());

			FtpWebRequest ftpRequestFile = (FtpWebRequest)WebRequest.Create($"ftp://{server}//{ftpFolder}");
			ftpRequestFile.Credentials = new NetworkCredential(login, password);
			ftpRequestFile.Method = WebRequestMethods.Ftp.ListDirectory;
			FtpWebResponse responseFile = (FtpWebResponse)ftpRequestFile.GetResponse();
			StreamReader streamReaderFile = new StreamReader(responseFile.GetResponseStream());

			List<string> files = new List<string>();

			string line = streamReader.ReadLine();
			string lineFile = Path.GetFileName(streamReaderFile.ReadLine());
			while (!string.IsNullOrEmpty(line))
			{
				List<string> split = line.Split(' ').ToList();

				for (int i = split.Count - 1; i >= 0; i--)
				{
					if (split[i] == "" || split[i] == " ")
					{
						split.Remove(split[i]);
					}
				}

				if (split[0] != FTP_FOLDER_SIGN)
				{

					if (searchPattern.Length == 0 || searchPattern.Contains("*"))
					{
						files.Add(lineFile);
					}
					else
					{
						if (searchPattern.Contains(Path.GetExtension(lineFile)))
						{
							files.Add(lineFile);
						}
					}
				}


				line = Path.GetFileName(streamReader.ReadLine());
				if (!(split[0] == FTP_FOLDER_SIGN && (split.Last() == "." || split.Last() == "..")))
				{
					lineFile = Path.GetFileName(streamReaderFile.ReadLine());
				}
			}

			return files;
		}

		public static List<string> GetFilesListOld(string server,
												string login,
												string password,
												string ftpFolder)
		{
			server = server.Replace("ftp://", "");
			FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create($"ftp://{server}//{ftpFolder}");
			ftpRequest.Credentials = new NetworkCredential(login, password);
			ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
			FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
			StreamReader streamReader = new StreamReader(response.GetResponseStream());

			List<string> files = new List<string>();

			string line = streamReader.ReadLine();
			while (!string.IsNullOrEmpty(line))
			{
				files.Add(line);
				line = streamReader.ReadLine();
			}

			return files;


			//var res = new List<string>();
			//using (Ftp ftp = new Ftp())
			//{
			//	ftp.Connect(server);
			//	ftp.Login(login, password);
			//	try
			//	{
			//		var filesList = ftp.GetList(ftpFolder);
			//		foreach (var item in filesList)
			//		{
			//			if (item.IsFile)
			//			{
			//				res.Add(Path.GetFileName(item.Name));
			//			}
			//		}
			//	}
			//	catch (Exception ex)
			//	{
			//		return res;
			//	}
			//	finally
			//	{
			//		if (ftp.Connected)
			//			ftp.Close();

			//		ftp.Dispose();
			//	}
			//}
			//return res;
		}

		//public static TryGetMd5ViaFtp GetFileMD5(string server,
		//								string login,
		//								string password,
		//								string ftpFilePath)
		//{
		//	string result = null;
		//	using (Ftp ftp = new Ftp())
		//	{
		//		try
		//		{
		//			ftp.Connect(server);
		//			ftp.Login(login, password);
		//			try
		//			{
		//				result = ftp.GetFileHash(ftpFilePath, FtpHashType.MD5).ToHex(true);
		//			}
		//			finally
		//			{
		//				if (ftp.Connected)
		//					ftp.Close();
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			return new TryGetMd5ViaFtp(null, false, ex.Message.ToString());
		//		}
		//		finally
		//		{
		//			if (ftp.Connected)
		//				ftp.Close();

		//			ftp.Dispose();
		//		}
		//	}

		//	return new TryGetMd5ViaFtp(result);
		//}
	}
}
