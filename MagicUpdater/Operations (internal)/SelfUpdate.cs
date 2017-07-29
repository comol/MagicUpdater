using Communications.Common;
using MagicUpdater.Actions;
using MagicUpdater.Core;
using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.Common;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using SmartAssembly.Attributes;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace MagicUpdater.Operations
{
	public class TrySelfUpdate : TryResult
	{
		public TrySelfUpdate(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}

#pragma warning disable CS0436 // Type conflicts with imported type
	[DoNotObfuscateType()]
#pragma warning restore CS0436 // Type conflicts with imported type
	public class SelfUpdate : Operation
	{
		private string _restartAppCurrentExePath;
		private string _restartAppUpdateExePath;
		private string _restartAppCurrenPath;
		private string _restartAppCurrenPathBackup;
		private string _restartAppUpdatePath;
		private Version _restartAppCurrentExeVersion;
		private Version _restartAppUpdateExeVersion;
		private string[] _searchPatterns;

		private string _logMessage = string.Empty;

		public SelfUpdate(int? operationId) : base(operationId) { }

		protected override void Execution(object sender, DoWorkEventArgs e)
		{
			if (!new Actions.FTPDownloadUpdate(Id).ActRun())
			{
				AddErrorMessage($"Ошибка закачки файлов по FTP для обновления MagicUpdater\r\n Action: FTPDownloadUpdate");
				return;
			}

			string appCurrentPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
			string appUpdatePath = Path.Combine(MainSettings.LocalSqlSettings.SelfUpdatePath, MainSettings.Constants.MAGIC_UPDATER_EXE);

			Version currentVersion = new Version(FileVersionInfo.GetVersionInfo(appCurrentPath).FileVersion);
			Version updateVersion = new Version(FileVersionInfo.GetVersionInfo(appUpdatePath).FileVersion);

			if (!CompareFileVersion(currentVersion, updateVersion))
			{
				#region UpdateAndExecute old disabled
				//if (UpdateAndExecute(appCurrentPath, appUpdatePath))
				//{
				//	//SendOperationReport("", true);
				//	//Extensions.ExitApplication();

				//	//Тормозим шедулер и ждем смерти
				//	//TaskerReporter.Stop();
				//}
				//Добавляем OperState тут, т.к. выходим из приложения
				//AddErrorMessage(_logMessage);
				#endregion UpdateAndExecute old disabled

				if (UpdateMyself())
				{
					//SendOperationReport("", true);
				}
				else
				{
					AddErrorMessage(_logMessage);
				}
			}
			else
			{
				AddErrorMessage("Версия локального файла приложения идентична версии файла приложения на сервере обновления");
			}
		}

		private bool UpdateMyself()
		{
			//Убиваем все процессы MagicUpdaterRestart
			Tools.KillAllProcessByname(MainSettings.Constants.MAGIC_UPDATER_RESTART, true);
			
			#region Initialization
			_restartAppCurrentExePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), MainSettings.Constants.RESTART_SERVICE_APPLICATION_FOLDER, MainSettings.Constants.RESTART_SERVICE_APPLICATION_EXE_NAME);
			_restartAppUpdateExePath = Path.Combine(MainSettings.LocalSqlSettings.SelfUpdatePath, MainSettings.Constants.RESTART_SERVICE_APPLICATION_FOLDER, MainSettings.Constants.RESTART_SERVICE_APPLICATION_EXE_NAME);
			_restartAppUpdatePath = Path.Combine(MainSettings.LocalSqlSettings.SelfUpdatePath, MainSettings.Constants.RESTART_SERVICE_APPLICATION_FOLDER);
			_restartAppCurrenPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), MainSettings.Constants.RESTART_SERVICE_APPLICATION_FOLDER);
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

			if (!File.Exists(_restartAppUpdateExePath))
			{
				AppendLogMessage($"Отсутствует файл по пути \"{_restartAppUpdateExePath}\"");
				return false;
			}

			_restartAppUpdateExeVersion = new Version(FileVersionInfo.GetVersionInfo(_restartAppUpdateExePath).FileVersion);
			#endregion Initialization

			#region RestartApplicationUpdate
			if (!CompareFileVersion(_restartAppCurrentExeVersion, _restartAppUpdateExeVersion))
			{
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
					AppendLogMessage($"Ошибка создания бекапа текущей версии {MainSettings.Constants.MAGIC_UPDATER_RESTART}. Original: {ex.Message}");
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
					AppendLogMessage($"Ошибка удаления текущей версии. Original: {ex.Message}. Выполняется восстановление из бекапа");
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
					AppendLogMessage($"Ошибка копирования новой версии в попку текущей. Original: {ex.Message}. Выполняется восстановление из бекапа");
					RestoreCurrentVersionFromBackup();
					return false;
				}
			}
			#endregion RestartApplicationUpdate

			//Посылаем команду запуска MagicUpdaterRestart для MagicUpdaterSettings
			//new StartSettingsAferUpdate(Id).ActRun();

			//if (Tools.GetProcessesCount(MainSettings.Constants.MAGIC_UPDATER_SETTINGS) > 0)
			//{
			//	var res = MuCore.ConnectionToSettings.SendAsyncMessageAndWaitForResponse(new CommunicationObject
			//	{
			//		ActionType = CommunicationActionType.StartMagicUpdaterRestartForSettings,
			//		Data = Id
			//	}, 10000);

			//	if (!res.IsComplete)
			//	{
			//		this.AddErrorMessage(res.Message);
			//	}

			//	Thread.Sleep(2000);

			//	//Останавливаем MagicUpdaterSettings
			//	Tools.KillAllProcessByname(MainSettings.Constants.MAGIC_UPDATER_SETTINGS, true);
			//}
			//else
			//{

			//}

			IsSendLogAndStatusAfterExecution = false;

			//Отрубаем пайпы
			//MuCore.ConnectionToSettings?.DisposeAsyncServer();

			//Запускаем MagicUpdaterRestart с ключем обновления
			Tools.SelfUpdateAndRestart(Id);

			return true;
		}

		private void RestoreCurrentVersionFromBackup()
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
							AppendLogMessage($"Не критическая ошибка копирования файла из бекапа. Original: {ex.Message}");
						}
					}
				}
				else
				{
					AppendLogMessage($"Ошибка восстановления текущей версии из бекапа. Бекапа не существует.");
				}
				AppendLogMessage($"Восстановление ткущей версии из бекапа выполнено.");
			}
			catch (Exception ex)
			{
				AppendLogMessage($"Ошибка восстановления текущей версии из бекапа. Original: {ex.Message}");
			}
		}

		private bool CompareFileVersion(Version firstFileVer, Version secondFileVer)
		{
			return firstFileVer.Equals(secondFileVer);
		}

		private void AppendLogMessage(string message)
		{
			if (string.IsNullOrEmpty(_logMessage))
			{
				_logMessage = message;
			}
			else
			{
				_logMessage += $"{Environment.NewLine}{message}";
			}
		}
	}
}
