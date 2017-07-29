using MagicUpdater.DL.Common;
using MagicUpdater.DL.DB;
using MagicUpdater.DL.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace MuMonitorRestart.Core
{
	public static class MuMonitorUpdate
	{
		private static string[] GetFiles(string path, string[] searchPatterns, SearchOption searchOption)
		{
			List<string> files = new List<string>();
			foreach (string sp in searchPatterns)
			{
				files.AddRange(System.IO.Directory.GetFiles(path, sp, searchOption));
			}
			files.Sort();
			return files.ToArray();
		}

		private static bool CompareFileVersion(Version firstFileVer, Version secondFileVer)
		{
			return firstFileVer.Equals(secondFileVer);
		}

		private static bool IsFileLocked(string filePath)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			FileStream stream = null;

			try
			{
				stream = fileInfo.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
			}
			catch (IOException)
			{
				//the file is unavailable because it is:
				//still being written to
				//or being processed by another thread
				//or does not exist (has already been processed)
				return true;
			}
			finally
			{
				if (stream != null)
					stream.Close();
			}

			//file is not locked
			return false;
		}

		public static bool IsUpdateNeeded()
		{
			string currentExePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
			string exeName = Path.GetFileName(currentExePath);
			string updateExePath = Path.Combine(new MQueryCommand().GetSetting("MonitorSelfUpdatePath").Value, exeName);
			FileVersionInfo currentFileVer = FileVersionInfo.GetVersionInfo(currentExePath);
			Version currentVersion = new Version(currentFileVer.FileVersion);
			FileVersionInfo updateFileVer = FileVersionInfo.GetVersionInfo(updateExePath);
			Version updateVersion = new Version(updateFileVer.FileVersion);

			return !CompareFileVersion(currentVersion, updateVersion);
		}

		private static void WaitFileUnlocking(string currentFile, int tryCount, int timeout)
		{
			if (File.Exists(currentFile))
			{
				while (IsFileLocked(currentFile) && tryCount > 0)
				{
					Thread.Sleep(timeout);
					tryCount -= 1;
				}
			}
		}

		public static void Update()
		{
			string currentExePath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)), Constants.MAGIC_UPDATER_MONITOR_EXE_NAME);
			string exeName = Constants.MAGIC_UPDATER_MONITOR_EXE_NAME;
			string updateExePath = Path.Combine(new MQueryCommand().GetSetting("MonitorSelfUpdatePath").Value, exeName);
			FileVersionInfo currentFileVer = FileVersionInfo.GetVersionInfo(currentExePath);
			Version currentVersion = new Version(currentFileVer.FileVersion);
			FileVersionInfo updateFileVer = FileVersionInfo.GetVersionInfo(updateExePath);
			Version updateVersion = new Version(updateFileVer.FileVersion);

			try
			{
				if (File.Exists(updateExePath))
				{
					//rename existed file
					//File.Delete(curExeName + "_");
					string currentPath = Path.GetDirectoryName(currentExePath);
					string updatingPath = Path.GetDirectoryName(updateExePath);

					string[] allCurrentOtherFiles = GetFiles(currentPath, new string[] { "*.*_" }, SearchOption.TopDirectoryOnly);

					try
					{
						foreach (var currentFile in allCurrentOtherFiles)
						{
							WaitFileUnlocking(currentFile, 10, 2000);
							File.Delete(currentFile);
						}
					}
					catch (Exception ex)
					{
						MLogger.Debug(ex.Message);
					}

					File.Move(currentExePath, currentExePath + "_");

					//copy standard file
					if (File.Exists(updateExePath) && !File.Exists(currentExePath))
					{
						File.Copy(updateExePath, currentExePath, true);
					}
					else
					{
						MLogger.Debug("невозможно скопировать .exe - файл MagicUpdaterMonitor'а, либо не переименовался текущий файл, либо не существует файла для обновления.");
					}

					// Подменяем библиотеки и прочие файлы
					string[] searchPatterns = new string[]
					{
						"*.dll",
						"*.xml",
						"*.config",
						"*.pdb",
						"*.manifest",
					};

					string[] currentOtherFiles = GetFiles(currentPath, searchPatterns, SearchOption.TopDirectoryOnly);
					string[] updatingOtherFiles = GetFiles(updatingPath, searchPatterns, SearchOption.TopDirectoryOnly);

					try
					{
						foreach (var currentFile in currentOtherFiles)
						{
							File.Move(currentFile, $"{currentFile}_");
						}
					}
					catch (Exception ex)
					{
						MLogger.Debug(ex.Message);
					}

					try
					{
						foreach (var updatingFile in updatingOtherFiles)
						{
							File.Copy(updatingFile, updatingFile.Replace(updatingPath, currentPath), true);
						}
					}
					catch (Exception ex)
					{
						MLogger.Debug(ex.Message);
					}

					bool isProcessCreated = false;
					for (int i = 3; i >= 0; i--)
					{
						//execute copied (standard) file
						Process process = null;
						if (!isProcessCreated)
						{
							//process = Process.Start(currentExePath, $"force");
							process = Process.Start(currentExePath);
						}
						Thread.Sleep(2000);
						isProcessCreated = process != null && !process.HasExited;
						if (isProcessCreated)
							break;
					}

					if (!isProcessCreated)
					{
						MLogger.Debug("не удалось запустить обновленную версию MagicUpdaterMonitor'а");
					}
				}
				else
				{
					MLogger.Debug(".exe - файл MagicUpdaterMonitor'а не найден.");
				}
			}
			catch (Exception ex)
			{
				MLogger.Debug(ex.Message);
			}
		}
	}
}
