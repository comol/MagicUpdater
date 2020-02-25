using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.Common;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using MagicUpdaterRestart.RestartTools;
using System.Threading.Tasks;
using Communications.Common;
using MagicUpdaterRestart.Actions;
using System.Diagnostics;

namespace MagicUpdaterRestart
{
	class Program
	{
		private const int RESTART_TIMEOUT = 120000;
		private static string _currentVersionPath;
		private static string _newVersionPath;
		private static string _currentVersionBackupPath;
		private static string _currentVersionExeName;
		private static string _newVersionExeName;
		private static string[] _searchPatterns;
		private static int _operationId = 0;

		private static void RestoreCurrentVersionFromBackup()
		{
			try
			{
				if (Directory.Exists(_currentVersionBackupPath))
				{
					foreach (var file in FilesWorks.GetFiles(_currentVersionBackupPath, _searchPatterns, SearchOption.TopDirectoryOnly))
					{
						try
						{
							file.CopyTo(Path.Combine(_currentVersionPath, file.Name));
						}
						catch (Exception ex)
						{
							NLogger.LogErrorToHdd($"Не критическая ошибка копирования файла из бекапа. Original: {ex.Message}", MainSettings.Constants.MAGIC_UPDATER_RESTART);
						}
					}
				}
				else
				{
					NLogger.LogErrorToHdd($"Ошибка восстановления текущей версии из бекапа. Бекапа не существует.", MainSettings.Constants.MAGIC_UPDATER_RESTART);
				}
				NLogger.LogDebugToHdd($"Восстановление ткущей версии из бекапа выполнено.", MainSettings.Constants.MAGIC_UPDATER_RESTART);
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToHdd($"Ошибка восстановления текущей версии из бекапа. Original: {ex.Message}", MainSettings.Constants.MAGIC_UPDATER_RESTART);
			}
		}

		private static void CopyNewVersionFiles()
		{
			//Мутим Backup
			try
			{
				FilesWorks.DeleteDirectoryFull(_currentVersionBackupPath);
				Directory.CreateDirectory(_currentVersionBackupPath);
				if (Directory.Exists(_currentVersionBackupPath))
				{
					foreach (var file in FilesWorks.GetFiles(_currentVersionPath, _searchPatterns, SearchOption.TopDirectoryOnly))
					{
						file.CopyTo(Path.Combine(_currentVersionBackupPath, file.Name), true);
					}
				}
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToHdd($"Ошибка создания бекапа текущей версии {MainSettings.Constants.MAGIC_UPDATER}. Original: {ex.Message}", MainSettings.Constants.MAGIC_UPDATER_RESTART);
				return;
			}

			//Удаляем текущую версию
			try
			{
				foreach (var file in FilesWorks.GetFiles(_currentVersionPath, _searchPatterns, SearchOption.TopDirectoryOnly))
				{
					if (file.Name != "settings.json")
					{
						file.Delete();
					}
				}
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToHdd($"Ошибка удаления текущей версии. Original: {ex.Message}", MainSettings.Constants.MAGIC_UPDATER_RESTART);
				RestoreCurrentVersionFromBackup();
				return;
			}

			//Копируем новую версию
			try
			{
				foreach (var file in FilesWorks.GetFiles(_newVersionPath, _searchPatterns, SearchOption.TopDirectoryOnly))
				{
					file.CopyTo(Path.Combine(_currentVersionPath, file.Name));
				}
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToHdd($"Ошибка копирования новой версии в попку текущей. Original: {ex.Message}", MainSettings.Constants.MAGIC_UPDATER_RESTART);
				RestoreCurrentVersionFromBackup();
				return;
			}
		}

		static void Main(string[] args)
		{
			#region Initialization
			//Загружаем все настройки
			var loadSettingsResult = MainSettings.LoadSettings();
			if (!loadSettingsResult.IsComplete)
			{
				NLogger.LogErrorToHdd(loadSettingsResult.NamedMessage, MainSettings.Constants.MAGIC_UPDATER);
				return;
			}

			//Инициализируем переменные
			_currentVersionPath = Path.GetDirectoryName(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
			_newVersionPath = MainSettings.LocalSqlSettings.SelfUpdatePath;
			_currentVersionBackupPath = Path.Combine(_currentVersionPath, "backup");
			_currentVersionExeName = Path.Combine(_currentVersionPath, MainSettings.Constants.MAGIC_UPDATER_EXE);
			_newVersionExeName = Path.Combine(_newVersionPath, MainSettings.Constants.MAGIC_UPDATER_EXE);
			_searchPatterns = new string[]
						{
						"*.exe",
						"*.dll",
						"*.xml",
						"*.config",
						"*.pdb",
						"*.manifest",
						};

			_operationId = 0;
			#endregion Initialization

			if (args != null && args.Length == 2)
			{
				#region setOperationId
				try
				{
					_operationId = Convert.ToInt32(args[1]);
				}
				catch (Exception ex)
				{
					_operationId = 0;
					NLogger.LogErrorToHdd($"{ex.Message}{Environment.NewLine}{ex.ToString()}", MainSettings.Constants.MAGIC_UPDATER_RESTART);
				}
				#endregion setOperationId

				#region restart
				if (args[0] == MainSettings.Constants.RESTART_PARAMETER)
				{
					ServiceController service = new ServiceController(MainSettings.Constants.SERVICE_NAME);
					TimeSpan timeout = TimeSpan.FromMilliseconds(RESTART_TIMEOUT);
					try
					{
						if (service.Status == ServiceControllerStatus.Running)
						{
							service.Stop();
							service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
							Tools.WaitAllProcessByname(MainSettings.Constants.MAGIC_UPDATER);

							Thread.Sleep(3000);

							//Если эта зараза повисла то кильнем, иначе крышка
							foreach (var proc in Process.GetProcessesByName(MainSettings.Constants.MAGIC_UPDATER))
							{
								proc.Kill();
							}

							service.Start();
							service.WaitForStatus(ServiceControllerStatus.Running, timeout);
						}
						else if (service.Status == ServiceControllerStatus.Stopped)
						{
							service.Start();
							service.WaitForStatus(ServiceControllerStatus.Running, timeout);
						}

						//Отправляем сообщение еще одному приложению MagicUpdaterRestart для того чтобы оно запустило MagicUpdaterSettings с UI
						//new StartSettingsViaPipe(_operationId).ActRun();

						//Меняем состояние операции
						if (_operationId > 0)
						{
							Operation.AddOperState(_operationId, OperStates.End);
							Operation.SendOperationReport(_operationId, "", true);
						}
					}
					catch (Exception ex)
					{
						//Если службу не удалось нормлаьно остановитиь, то киляем ее процесс и пробуем запустить.
						try
						{
							foreach (var proc in Process.GetProcessesByName(MainSettings.Constants.MAGIC_UPDATER))
							{
								proc.Kill();
							}

							Thread.Sleep(3000);

							service.Start();
							service.WaitForStatus(ServiceControllerStatus.Running, timeout);

							if (_operationId > 0)
							{
								Operation.AddOperState(_operationId, OperStates.End);
								Operation.SendOperationReport(_operationId, $"Не удалось штатно остановить службу, служба была остановлена принудительно и перезапущена. Ошибка штатной остановки: {ex.Message}", true);
							}
							NLogger.LogErrorToHdd($"{ex.Message}{Environment.NewLine}{ex.ToString()}", MainSettings.Constants.MAGIC_UPDATER_RESTART);
						}
						catch (Exception exx)
						{
							if (_operationId > 0)
							{
								Operation.AddOperState(_operationId, OperStates.End);
								Operation.SendOperationReport(_operationId, $"Не удалось перезапустить службу MagicUpdater. Ошибка: {exx.Message}", false);
							}
							NLogger.LogErrorToHdd($"{exx.Message}{Environment.NewLine}{exx.ToString()}", MainSettings.Constants.MAGIC_UPDATER_RESTART);
						}
					}
				}
				#endregion restart

				#region updateRestart
				if (args[0] == MainSettings.Constants.UPDATE_RESTART_PARAMETER)
				{
					try
					{
						ServiceController service = new ServiceController(MainSettings.Constants.SERVICE_NAME);
						TimeSpan timeout = TimeSpan.FromMilliseconds(RESTART_TIMEOUT);
						if (service.Status == ServiceControllerStatus.Running)
						{
							service.Stop();
							service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
							Tools.WaitAllProcessByname(MainSettings.Constants.MAGIC_UPDATER);

							Thread.Sleep(3000);

							//Если эта зараза повисла то кильнем, иначе крышка
							foreach (var proc in Process.GetProcessesByName(MainSettings.Constants.MAGIC_UPDATER))
							{
								proc.Kill();
							}

							CopyNewVersionFiles();
							Thread.Sleep(1000);
							service.Start();
							service.WaitForStatus(ServiceControllerStatus.Running, timeout);
						}
						else if (service.Status == ServiceControllerStatus.Stopped)
						{
							CopyNewVersionFiles();
							Thread.Sleep(3000);
							service.Start();
							service.WaitForStatus(ServiceControllerStatus.Running, timeout);
						}

						//Отправляем сообщение еще одному приложению MagicUpdaterRestart для того чтобы оно запустило MagicUpdaterSettings с UI
						//new StartSettingsViaPipe(_operationId).ActRun();

						//Меняем состояние операции
						if (_operationId > 0)
						{
							Operation.AddOperState(_operationId, OperStates.End);
							Operation.SendOperationReport(_operationId, "", true);
						}
					}
					catch (Exception ex)
					{
						if (_operationId > 0)
						{
							Operation.SendOperationReport(_operationId, $"Не удалось перезапустить обновленную версию службы службу MagicUpdater. Original: {ex.ToString()}", false);
						}
						NLogger.LogErrorToHdd($"Не удалось перезапустить обновленную версию службы службу MagicUpdater. Original: {ex.ToString()}", MainSettings.Constants.MAGIC_UPDATER_RESTART);
					}
				}
				#endregion updateRestart

				#region waitForStartMagicUpdaterSettingsViaPipe
				if (args[0] == MainSettings.Constants.WAIT_FOR_START_SETTINGS_PARAMETER)
				{
					try
					{
						bool startSettingsMessageRecieved = false;
						Task.Factory.StartNew(() =>
						{
							PipeServer pipeServer = new PipeServer();
							var res = pipeServer.RecieveSyncMessage();

							if (res.ActionType == CommunicationActionType.StartMagicUpdaterSettings)
							{
								Tools.StartMagicUpdaterSettings();
							}

							startSettingsMessageRecieved = true;
						});

						while (!startSettingsMessageRecieved) { }
					}
					catch (Exception ex)
					{
						if (_operationId > 0)
						{
							Operation.SendOperationReport(_operationId, $"Не удалось запустить MagicUpdaterRestart для MagicUpdaterSettings. Original: {ex.ToString()}", false);
						}
						NLogger.LogErrorToHdd($"Не удалось запустить MagicUpdaterRestart для MagicUpdaterSettings. Original: {ex.ToString()}", MainSettings.Constants.MAGIC_UPDATER_RESTART);
					}
				}
				#endregion waitForStartMagicUpdaterSettingsViaPipe
			}
			else
			{
				NLogger.LogErrorToHdd($"Обшибочные параметры запуска.", MainSettings.Constants.MAGIC_UPDATER_RESTART);
			}
		}

	}
}
