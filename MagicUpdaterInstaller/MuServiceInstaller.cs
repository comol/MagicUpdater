using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MagicUpdaterInstaller
{
	public class MuServiceInstaller
	{
		#region Constants
		// Каталоги сервиса по умолчанию
		const string SERVICE_PATH = @"C:\SystemUtils\MagicUpdater\MagicUpdater.exe";
		const string SERVICE_DISPLAY_NAME = "MagicUpdater";
		const string SERVICE_EXE_NAME = "MagicUpdater.exe";
		const string SERVICE_NAME = "MagicUpdater";
		const string MAGIC_UPDATER_PATH = @"C:\SystemUtils\MagicUpdater";
		const string MAGIC_UPDATER_NEW_VER_PATH = @"C:\SystemUtils\MagicUpdaterNewVer";
		#endregion

		#region Private Variables
		private string _message = "";
		private int _operationId = 0;

		private string _servicePath;
		private string _magicUpdaterPath;
		private string _restartApplicationPath;

		private ServiceController service = new ServiceController(SERVICE_NAME);
		#endregion Private Variables

		#region DLLImport
		[DllImport("advapi32.dll")]
		public static extern IntPtr OpenSCManager(string lpMachineName, string lpSCDB, int scParameter);
		[DllImport("Advapi32.dll")]
		public static extern IntPtr CreateService(IntPtr SC_HANDLE, string lpSvcName, string lpDisplayName,
		int dwDesiredAccess, int dwServiceType, int dwStartType, int dwErrorControl, string lpPathName,
		string lpLoadOrderGroup, int lpdwTagId, string lpDependencies, string lpServiceStartName, string lpPassword);
		[DllImport("advapi32.dll")]
		public static extern void CloseServiceHandle(IntPtr SCHANDLE);
		[DllImport("advapi32.dll")]
		public static extern int StartService(IntPtr SVHANDLE, int dwNumServiceArgs, string lpServiceArgVectors);
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern IntPtr OpenService(IntPtr SCHANDLE, string lpSvcName, int dwNumServiceArgs);
		[DllImport("advapi32.dll")]
		public static extern int DeleteService(IntPtr SVHANDLE);
		[DllImport("kernel32.dll")]
		public static extern int GetLastError();
		#endregion DLLImport

		public MuServiceInstaller(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new Exception($"Ошибочный путь установки MU - {path}");
			}
			_magicUpdaterPath = path;
			_servicePath = Path.Combine(path, SERVICE_EXE_NAME);
		}

		public static bool IsServiceInstalled()
		{
			ServiceController srv = ServiceController.GetServices().FirstOrDefault(s => s.ServiceName == SERVICE_NAME);
			return srv != null;
		}

		private void UninstallBase()
		{
			TimeSpan timeout = TimeSpan.FromMilliseconds(30000);

			Program.MainForm?.LogInstallServiceString("Тормозим старую службу...");
			//Тормозим старую службу
			try
			{
				service.Stop();
				service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
				AddMessage($"Старая служба {SERVICE_NAME} - остановлена");
				Program.MainForm?.LogInstallServiceString($"Старая служба {SERVICE_NAME} - остановлена");
				Program.MainForm?.LogInstallServiceString("Старая служба остановлена");
			}
			catch (Exception ex)
			{
				AddMessage(ex.Message);
				Program.MainForm?.LogInstallServiceString(ex);
			}

			Console.WriteLine("Удаляем старую службу...");
			//Удаляем старую службу
			try
			{
				using (ServiceInstaller ServiceInstallerObj = new ServiceInstaller())
				{
					InstallContext Context = new InstallContext(null, null);
					ServiceInstallerObj.Context = Context;
					ServiceInstallerObj.ServiceName = SERVICE_NAME;
					ServiceInstallerObj.Uninstall(null);
				}

				Thread.Sleep(3000);

				AddMessage($"Старая служба {SERVICE_NAME} - удалена");
				Program.MainForm?.LogInstallServiceString($"Старая служба {SERVICE_NAME} - удалена");
			}
			catch (Exception ex)
			{
				AddMessage(ex.Message);
				Program.MainForm?.LogInstallServiceString(ex);
			}

			Program.MainForm?.LogInstallServiceString("Убиваем все процессы MagicUpdater...");
			//Убиваем все процессы MagicUpdater
			Process[] procs = Process.GetProcessesByName(SERVICE_NAME);
			foreach (var proc in procs)
			{
				proc.Kill();
			}
			Thread.Sleep(3000);
			AddMessage($"Все процессы {SERVICE_NAME} - убиты");
			Program.MainForm?.LogInstallServiceString("Все процессы MagicUpdater завершены");

			Program.MainForm?.LogInstallServiceString("Чистим реестр (автозагрузка MU как приложения в корне run)...");
			//Чистим реестр (автозагрузка MU как приложения в корне run)
			#region Чистим реестр
			string keyName = @"Software\Microsoft\Windows\CurrentVersion\Run";
			using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName, true))
			{
				if (key == null)
				{
					Program.MainForm?.LogInstallServiceString($"CurrentUser: Отсутствует путь в реестре {keyName}");
					AddMessage($"CurrentUser: Отсутствует путь в реестре {keyName}");
				}
				else
				{
					try
					{
						key.DeleteValue(SERVICE_NAME);
						Program.MainForm?.LogInstallServiceString($"CurrentUser: Значение реестра - {SERVICE_NAME} удалено");
						AddMessage($"CurrentUser: Значение реестра - {SERVICE_NAME} удалено");
					}
					catch (Exception ex)
					{
						NLogger.LogDebugToHdd($"CurrentUser: {ex.Message}");
						AddMessage($"CurrentUser: {ex.Message}");
					}

				}
			}

			using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyName, true))
			{
				if (key == null)
				{
					NLogger.LogErrorToHdd($"LocalMachine: Отсутствует путь в реестре {keyName}");
					AddMessage($"LocalMachine: Отсутствует путь в реестре {keyName}");
				}
				else
				{
					try
					{
						key.DeleteValue(SERVICE_NAME);
						Program.MainForm?.LogInstallServiceString($"LocalMachine: Значение реестра - {SERVICE_NAME} удалено");
						AddMessage($"LocalMachine: Значение реестра - {SERVICE_NAME} удалено");
					}
					catch (Exception ex)
					{
						Program.MainForm?.LogInstallServiceString($"LocalMachine: {ex.Message}");
						AddMessage($"LocalMachine: {ex.Message}");
					}
				}
			}
			#endregion Чистим реестр

			Program.MainForm?.LogInstallServiceString("Удаляем все из папок MagicUpdater и MagicUpdaterNewVer...");
			//Удаляем все из папок MagicUpdater и MagicUpdaterNewVer
			#region Удаляем все из папок MagicUpdater и MagicUpdaterNewVer
			try
			{
				DirectoryInfo di = new DirectoryInfo(_magicUpdaterPath);

				foreach (FileInfo file in di.GetFiles())
				{
					if (file.Name.ToUpper() != MainSettings.Constants.SETTINGS_JSON_FILE_NAME.ToUpper())
					{
						file.Delete();
					}
				}
				foreach (DirectoryInfo dir in di.GetDirectories())
				{
					dir.Delete(true);
				}
				Program.MainForm?.LogInstallServiceString($"Путь {_magicUpdaterPath} - очищен");
				AddMessage($"Путь {_magicUpdaterPath} - очищен");
			}
			catch (Exception ex)
			{
				Program.MainForm?.LogInstallServiceString(ex.Message);
				AddMessage(ex.Message);
			}

			try
			{
				DirectoryInfo di = new DirectoryInfo(MAGIC_UPDATER_NEW_VER_PATH);

				foreach (FileInfo file in di.GetFiles())
				{
					file.Delete();
				}
				foreach (DirectoryInfo dir in di.GetDirectories())
				{
					dir.Delete(true);
				}
				Program.MainForm?.LogInstallServiceString($"Путь {MAGIC_UPDATER_NEW_VER_PATH} - очищен");
				AddMessage($"Путь {MAGIC_UPDATER_NEW_VER_PATH} - очищен");
			}
			catch (Exception ex)
			{
				Program.MainForm?.LogInstallServiceString(ex.Message);
				AddMessage(ex.Message);
			}
			#endregion Удаляем все из папок MagicUpdater и MagicUpdaterNewVer
		}

		public void Uninstall()
		{
			try
			{
				UninstallBase();

				if (Directory.Exists(MAGIC_UPDATER_PATH))
				{
					FilesWorks.DeleteDirectoryFull(MAGIC_UPDATER_PATH);
				}

				if (Directory.Exists(MAGIC_UPDATER_NEW_VER_PATH))
				{
					FilesWorks.DeleteDirectoryFull(MAGIC_UPDATER_NEW_VER_PATH);
				}
			}
			catch (Exception ex)
			{
				Program.MainForm?.LogInstallServiceString(ex);
				AddMessage(ex.Message);
				NLogger.LogErrorToHdd(ex.ToString());
			}
		}

		private void InstallBase()
		{
			Directory.CreateDirectory(_magicUpdaterPath);

			Program.MainForm?.LogInstallServiceString($"Копирование файла {MainSettings.Constants.SETTINGS_JSON_FILE_NAME}.");
			if (File.Exists(Path.Combine(_magicUpdaterPath, MainSettings.Constants.SETTINGS_JSON_FILE_NAME)))
			{
				File.Delete(Path.Combine(_magicUpdaterPath, MainSettings.Constants.SETTINGS_JSON_FILE_NAME));
			}
			File.Copy(MainSettings.JsonSettingsFileFullPath, Path.Combine(_magicUpdaterPath, MainSettings.Constants.SETTINGS_JSON_FILE_NAME));
			Program.MainForm?.LogInstallServiceString($"Копирование файла {MainSettings.Constants.SETTINGS_JSON_FILE_NAME} успешно завершено.");


			Thread.Sleep(1000);

			Program.MainForm?.LogInstallServiceString("Скачиваем новую версию приложения для перезапуска для агента");
			//Копируем новый MagicUpdater целиком!
			#region Копируем новый MagicUpdater целиком!
			Directory.CreateDirectory(_magicUpdaterPath);
			
			//Restart applivation
			var resReatartApp = FtpWorks.DownloadFilesFromFtpFolder(MainSettings.LocalSqlSettings.SelfUpdateFtpServer
				, MainSettings.LocalSqlSettings.SelfUpdateFtpUser
				, MainSettings.LocalSqlSettings.SelfUpdateFtpPassword
				, Path.Combine(MainSettings.LocalSqlSettings.SelfUpdateFtpPath, MainSettings.Constants.MU_RESTART_FOLDER_NAME)
				, Path.Combine(_magicUpdaterPath, MainSettings.Constants.MU_RESTART_FOLDER_NAME)
				, "*");

			if (!resReatartApp.IsComplete)
			{
				throw new Exception(resReatartApp.Message);
			}
			else
			{
				Program.MainForm?.LogInstallServiceString($"Закачка нового {MainSettings.Constants.MAGIC_UPDATER_RESTART} - завешена");
				AddMessage($"Закачка нового {MainSettings.Constants.MAGIC_UPDATER_RESTART} - завешена");
			}

			//Agent
			Program.MainForm?.LogInstallServiceString("Скачиваем новую версию агента");
			var resAgent = FtpWorks.DownloadFilesFromFtpFolder(MainSettings.LocalSqlSettings.SelfUpdateFtpServer
				, MainSettings.LocalSqlSettings.SelfUpdateFtpUser
				, MainSettings.LocalSqlSettings.SelfUpdateFtpPassword
				, MainSettings.LocalSqlSettings.SelfUpdateFtpPath
				, _magicUpdaterPath
				, "*");

			if (!resAgent.IsComplete)
			{
				throw new Exception(resAgent.Message);
			}
			else
			{
				Program.MainForm?.LogInstallServiceString($"Закачка нового {SERVICE_NAME} - завешена");
				AddMessage($"Закачка нового {SERVICE_NAME} - завешена");
			}
			#endregion Копируем новый MagicUpdater целиком!

			//Устанавливаем службу с режимом автозапуска
			//Запускаем службу
			Thread.Sleep(3000);

			try
			{
				Program.MainForm?.LogInstallServiceString($"Установка новой службы {SERVICE_NAME}");

				InstallService(_servicePath, SERVICE_NAME, SERVICE_DISPLAY_NAME);

				Program.MainForm?.LogInstallServiceString($"Новая служба {SERVICE_NAME} - установлена и запущена");
				AddMessage($"Новая служба {SERVICE_NAME} - установлена и запущена");

				//для отложенного запуска
				Thread.Sleep(3000);
				//Меняем режим запуска на отложенный
				Process changeProcess = Process.Start("sc.exe", $"config {SERVICE_NAME} start= delayed-auto");
				changeProcess.WaitForExit(10000);
				Program.MainForm?.LogInstallServiceString($"Для службы {SERVICE_NAME} - установлен отложенный запуск.");
				//sc.exe config MagicUpdater start= delayed-auto
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public bool Install()
		{
			try
			{
				UninstallBase();
				InstallBase();
				return true;
			}
			catch (Exception ex)
			{
				Program.MainForm?.LogInstallServiceString(ex);
				NLogger.LogErrorToHdd(ex.ToString());
				AddMessage(ex.Message);
				return false;
			}

		}

		public void RestartService()
		{
			const int RESTART_TIMEOUT = 60000;

			try
			{
				ServiceController service = new ServiceController(MainSettings.Constants.SERVICE_NAME);
				TimeSpan timeout = TimeSpan.FromMilliseconds(RESTART_TIMEOUT);
				if (service.Status == ServiceControllerStatus.Running)
				{
					service.Stop();
					service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

					service.Start();
					service.WaitForStatus(ServiceControllerStatus.Running, timeout);
				}

				if (service.Status == ServiceControllerStatus.Stopped)
				{
					service.Start();
					service.WaitForStatus(ServiceControllerStatus.Running, timeout);
				}
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToHdd(ex.Message.ToString(), MainSettings.Constants.MAGIC_UPDATER_SETTINGS);
				MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void Restart()
		{
			try
			{
				NLogger.LogDebugToHdd($"Установка новой службы {SERVICE_NAME}");
				InstallService(_servicePath, SERVICE_NAME, SERVICE_DISPLAY_NAME);
				NLogger.LogDebugToHdd($"Новая служба {SERVICE_NAME} - установлена и запущена");
				AddMessage($"Новая служба {SERVICE_NAME} - установлена и запущена");

				Thread.Sleep(3000);
				//Меняем режим запуска на отложенный
				Process.Start("sc.exe", $"config {SERVICE_NAME} start= delayed-auto");

				//sc.exe config MagicUpdater start= delayed-auto
			}
			catch (Exception ex)
			{
				NLogger.LogDebugToHdd(ex.ToString());
			}
		}

		private void CreateMuInstallServiceSchedule(string taskName)
		{
			using (TaskService ts = new TaskService())
			{
				// Create a new task definition and assign properties
				TaskDefinition td = ts.NewTask();
				td.RegistrationInfo.Description = SERVICE_NAME;

				// Create a trigger that will fire the task at this time every other day
				td.Triggers.Add(new TimeTrigger
				{
					Enabled = true,
					StartBoundary = DateTime.Now + TimeSpan.FromSeconds(30),
					ExecutionTimeLimit = TimeSpan.FromMinutes(10),
					Id = "MuInstallServiceTrigger"
				});

				// Create an action that will launch Notepad whenever the trigger fires
				td.Actions.Add(new ExecAction(System.Reflection.Assembly.GetEntryAssembly().Location, $"restart {_servicePath}", null));

				// Register the task in the root folder
				ts.RootFolder.RegisterTaskDefinition(taskName, td);
			}
		}

		private void DeleteSchedule(string taskName)
		{
			using (TaskService ts = new TaskService())
			{
				foreach (var task in ts.RootFolder.AllTasks)
				{
					if(task.Name == taskName)
					{
						ts.RootFolder.DeleteTask(taskName);
					}
				}
			}
		}

		private void AddMessage(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				return;
			}

			if (string.IsNullOrEmpty(_message))
			{
				_message = message;
			}
			else
			{
				_message = $"{_message}{Environment.NewLine}{message}";
			}
		}

		private void SendMessagesToOperation(bool isCompleted)
		{
			try
			{
				if (_operationId == 0)
				{
					return;
				}


				string logErrorPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "installServiceLogs/errors");
				string logDebugPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "installServiceLogs/debug");
				string errorLogText = null;
				string debugLogText = null;

				try
				{
					DirectoryInfo errorDirectoryInfo = new DirectoryInfo(logErrorPath);
					foreach (FileInfo errFile in errorDirectoryInfo.GetFiles())
					{
						if (string.IsNullOrEmpty(errorLogText))
						{
							errorLogText = File.ReadAllText(errFile.FullName, Encoding.GetEncoding("windows-1251"));
						}
						else
						{
							errorLogText += $"{Environment.NewLine}{File.ReadAllText(errFile.FullName, Encoding.GetEncoding("windows-1251"))}";
						}
					}
				}
				catch { }

				try
				{
					DirectoryInfo debugDirectoryInfo = new DirectoryInfo(logDebugPath);
					foreach (FileInfo debugFile in debugDirectoryInfo.GetFiles())
					{
						if (string.IsNullOrEmpty(errorLogText))
						{
							debugLogText = File.ReadAllText(debugFile.FullName, Encoding.GetEncoding("windows-1251"));
						}
						else
						{
							debugLogText += $"{Environment.NewLine}{File.ReadAllText(debugFile.FullName, Encoding.GetEncoding("windows-1251"))}";
						}
					}
				}
				catch { }

				string mess = $"Debug:{Environment.NewLine}{debugLogText}{Environment.NewLine}Errors:{Environment.NewLine}{errorLogText}";
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToHdd(ex.ToString());
			}
		}


		/// <summary>
		/// This method installs and runs the service in the service control manager.
		/// </summary>
		/// <param name="svcPath">The complete path of the service.</param>
		/// <param name="svcName">Name of the service.</param>
		/// <param name="svcDispName">Display name of the service.</param>
		/// <returns>True if the process went thro successfully. False if there was any error.</returns>
		private bool InstallService(string svcPath, string svcName, string svcDispName)
		{
			#region Constants declaration.
			int SC_MANAGER_CREATE_SERVICE = 0x0002;
			int SERVICE_WIN32_OWN_PROCESS = 0x00000010;
			int SERVICE_ERROR_NORMAL = 0x00000001;
			int STANDARD_RIGHTS_REQUIRED = 0xF0000;
			int SERVICE_QUERY_CONFIG = 0x0001;
			int SERVICE_CHANGE_CONFIG = 0x0002;
			int SERVICE_QUERY_STATUS = 0x0004;
			int SERVICE_ENUMERATE_DEPENDENTS = 0x0008;
			int SERVICE_START = 0x0010;
			int SERVICE_STOP = 0x0020;
			int SERVICE_PAUSE_CONTINUE = 0x0040;
			int SERVICE_INTERROGATE = 0x0080;
			int SERVICE_USER_DEFINED_CONTROL = 0x0100;
			int SERVICE_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED |
			SERVICE_QUERY_CONFIG |
			SERVICE_CHANGE_CONFIG |
			SERVICE_QUERY_STATUS |
			SERVICE_ENUMERATE_DEPENDENTS |
			SERVICE_START |
			SERVICE_STOP |
			SERVICE_PAUSE_CONTINUE |
			SERVICE_INTERROGATE |
			SERVICE_USER_DEFINED_CONTROL);
			int SERVICE_AUTO_START = 0x00000002;
			#endregion Constants declaration.
			try
			{
				IntPtr sc_handle = OpenSCManager(null, null, SC_MANAGER_CREATE_SERVICE);
				if (sc_handle.ToInt32() != 0)
				{
					IntPtr sv_handle = CreateService(sc_handle, svcName, svcDispName, SERVICE_ALL_ACCESS, SERVICE_WIN32_OWN_PROCESS, SERVICE_AUTO_START, SERVICE_ERROR_NORMAL, svcPath, null, 0, null, null, null);
					if (sv_handle.ToInt32() == 0)
					{
						CloseServiceHandle(sc_handle);
						return false;
					}
					else
					{
						//now trying to start the service
						int i = StartService(sv_handle, 0, null);
						// If the value i is zero, then there was an error starting the service.
						// note: error may arise if the service is already running or some other problem.
						if (i == 0)
						{
							return false;
						}
						CloseServiceHandle(sc_handle);
						return true;
					}
				}
				else
					return false;
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// This method uninstalls the service from the service conrol manager.
		/// </summary>
		/// <param name="svcName">Name of the service to uninstall.</param>
		public bool UnInstallService(string svcName)
		{
			int GENERIC_WRITE = 0x40000000;
			IntPtr sc_hndl = OpenSCManager(null, null, GENERIC_WRITE);
			if (sc_hndl.ToInt32() != 0)
			{
				int DELETE = 0x10000;
				IntPtr svc_hndl = OpenService(sc_hndl, svcName, DELETE);
				if (svc_hndl.ToInt32() != 0)
				{
					int i = DeleteService(svc_hndl);
					if (i != 0)
					{
						CloseServiceHandle(sc_hndl);
						return true;
					}
					else
					{
						CloseServiceHandle(sc_hndl);
						return false;
					}
				}
				else
					return false;
			}
			else
				return false;
		}

	}
}
