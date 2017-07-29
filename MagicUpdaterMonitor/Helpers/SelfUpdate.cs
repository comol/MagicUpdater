using MagicUpdater.DL.Common;
using MagicUpdater.DL.DB;
using MagicUpdater.DL.Helpers;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterMonitor.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MagicUpdaterMonitor.Helpers
{
	public static class SelfUpdate
	{
		private static string _restartAppCurrentExePath;
		private static string _restartAppUpdateExePath;
		private static string _restartAppCurrenPath;
		private static string _restartAppCurrenPathBackup;
		private static string _restartAppUpdatePath;
		private static Version _restartAppCurrentExeVersion;
		private static Version _restartAppUpdateExeVersion;
		private static string[] _searchPatterns;

		private class AsyncMessageForm
		{
			private volatile bool _shouldStop = false;
			private Form mf;
			public AsyncMessageForm()
			{
				mf = new UpdatingForm();
			}
			public void ShowDialog()
			{
				mf.ShowDialog();
			}
		}

		private static Thread asyncThread = null;
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
			try
			{
				string currentExePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
				string exeName = Path.GetFileName(currentExePath);
				string updateExePath = Path.Combine(MQueryCommand.GetSetting("MonitorSelfUpdatePath").Value, exeName);
				FileVersionInfo currentFileVer = FileVersionInfo.GetVersionInfo(currentExePath);
				Version currentVersion = new Version(currentFileVer.FileVersion);
				FileVersionInfo updateFileVer = FileVersionInfo.GetVersionInfo(updateExePath);
				Version updateVersion = new Version(updateFileVer.FileVersion);

				return !CompareFileVersion(currentVersion, updateVersion);
			}
			catch(Exception ex)
			{
				MLogger.Error(ex.ToString());
			}
			return false;
		}

		public static void ShowDialogAsync()
		{
			AsyncMessageForm asyncMessageForm = new AsyncMessageForm();
			asyncThread = new Thread(asyncMessageForm.ShowDialog);
			asyncThread.Start();
			while (!asyncThread.IsAlive) ;
			Thread.Sleep(100);
		}

		public static void CloseAsync()
		{
			if (asyncThread != null)
			{
				Thread tempThread = asyncThread;
				asyncThread = null;
				tempThread.Abort();
			}
		}

		private static void Harakiri()
		{
			var process = Process.GetCurrentProcess();
			if (!process.HasExited)
			{
				process.Kill();
			}
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

		private static void RestoreCurrentVersionFromBackup()
		{
			try
			{
				if (Directory.Exists(_restartAppCurrenPathBackup))
				{
					foreach (var file in FilesWorks.GetFiles(_restartAppCurrenPathBackup, _searchPatterns, SearchOption.TopDirectoryOnly))
					{
						try
						{
							file.CopyTo(Path.Combine(_restartAppCurrenPath, file.Name));
						}
						catch (Exception ex)
						{
							MLogger.Error($"Не критическая ошибка копирования файла из бекапа. Original: {ex.Message}");
						}
					}
				}
				else
				{
					MLogger.Error($"Ошибка восстановления текущей версии из бекапа. Бекапа не существует.");
				}
				MLogger.Error($"Восстановление ткущей версии из бекапа выполнено.");
			}
			catch (Exception ex)
			{
				MLogger.Error($"Ошибка восстановления текущей версии из бекапа. Original: {ex.Message}");
			}
		}

		public static bool UpdateNew()
		{
			#region Initialization
			_restartAppCurrentExePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), Constants.RESTART_MONITOR_APPLICATION_FOLDER, Constants.RESTART_MONITOR_APPLICATION_EXE_NAME);
			_restartAppUpdateExePath = Path.Combine(MQueryCommand.GetSetting("MonitorSelfUpdatePath").Value, Constants.RESTART_MONITOR_APPLICATION_FOLDER, Constants.RESTART_MONITOR_APPLICATION_EXE_NAME);
			_restartAppUpdatePath = Path.Combine(MQueryCommand.GetSetting("MonitorSelfUpdatePath").Value, Constants.RESTART_MONITOR_APPLICATION_FOLDER);
			_restartAppCurrenPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), Constants.RESTART_MONITOR_APPLICATION_FOLDER);
			_restartAppCurrenPathBackup = Path.Combine(_restartAppCurrenPath, "backup");
			_searchPatterns = new string[]
						{
						"*.exe",
						"*.dll",
						"*.xml",
						"*.config",
						"*.pdb",
						"*.manifest",
						};

			if (!File.Exists(_restartAppCurrentExePath))
			{
				_restartAppCurrentExeVersion = new Version(0, 0, 0, 0);
			}
			else
			{
				_restartAppCurrentExeVersion = new Version(FileVersionInfo.GetVersionInfo(_restartAppCurrentExePath).FileVersion);
			}

			_restartAppUpdateExeVersion = new Version(FileVersionInfo.GetVersionInfo(_restartAppUpdateExePath).FileVersion);
			#endregion Initialization

			#region RestartApplicationUpdate
			//Мутим Backup
			try
			{
				FilesWorks.DeleteDirectoryFull(_restartAppCurrenPathBackup);
				Directory.CreateDirectory(_restartAppCurrenPathBackup);
				if (Directory.Exists(_restartAppCurrenPathBackup))
				{
					foreach (var file in FilesWorks.GetFiles(_restartAppCurrenPath, _searchPatterns, SearchOption.TopDirectoryOnly))
					{
						file.CopyTo(Path.Combine(_restartAppCurrenPathBackup, file.Name));
					}
				}
			}
			catch (Exception ex)
			{
				MLogger.Error($"Ошибка создания бекапа текущей версии {Constants.MAGIC_UPDATER_RESTART}. Original: {ex.Message}");
				return false;
			}

			//Удаляем текущую версию
			try
			{
				foreach (var file in FilesWorks.GetFiles(_restartAppCurrenPath, _searchPatterns, SearchOption.TopDirectoryOnly))
				{
					file.Delete();
				}
			}
			catch (Exception ex)
			{
				MLogger.Error($"Ошибка удаления текущей версии. Original: {ex.Message}. Выполняется восстановление из бекапа");
				RestoreCurrentVersionFromBackup();
				return false;
			}

			//Копируем новую версию
			try
			{
				Directory.CreateDirectory(_restartAppCurrenPath);
				foreach (var file in FilesWorks.GetFiles(_restartAppUpdatePath, _searchPatterns, SearchOption.TopDirectoryOnly))
				{
					file.CopyTo(Path.Combine(_restartAppCurrenPath, file.Name));
				}
			}
			catch (Exception ex)
			{
				MLogger.Error($"Ошибка копирования новой версии в попку текущей. Original: {ex.Message}. Выполняется восстановление из бекапа");
				RestoreCurrentVersionFromBackup();
				return false;
			}
			#endregion RestartApplicationUpdate

			return true;
		}

		public static void Update(Form updatingForm = null)
		{
			ShowDialogAsync();

			const string SETTINGS_JSON = "settings.json";

			string currentExePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
			string exeName = Path.GetFileName(currentExePath);
			string updateExePath = Path.Combine(MQueryCommand.GetSetting("MonitorSelfUpdatePath").Value, exeName);
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
						File.Copy(Path.Combine(updatingPath, SETTINGS_JSON), Path.Combine(updatingPath, SETTINGS_JSON).Replace(updatingPath, currentPath), true);
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
					else
					{
						CloseAsync();
						Harakiri();
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
				CloseAsync();
				Harakiri();
			}
		}
	}
}
