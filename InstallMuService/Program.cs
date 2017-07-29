using InstallService.Abstract;
using InstallService.Helpers;
using Limilabs.FTP.Client;
using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace InstallService
{
	class ServiceInstaller
	{
		#region Private Variables
		private string _servicePath;
		private string _serviceName;
		private string _serviceDisplayName;

		private static string _message = "";
		private static int _operationId = 0;
		private static string _action = "";

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
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		#region Main method + testing code 
		[STAThread]
		static void Main(string[] args)
		{
			if (args != null && args.Length > 0)
			{
				try
				{
					_operationId = Convert.ToInt32(args[0]);
				}
				catch
				{
					_operationId = 0;
				}

				if (args.Length == 2)
				{
					try
					{
						_action = Convert.ToString(args[1]);
					}
					catch
					{
						_action = "";
					}
				}
			}



			// TODO: Add code to start application here
			// Testing --------------
			//string svcPath;
			//string svcName;
			//string svcDispName;
			//path to the service that you want to install
			//const string SERVICE_PATH = @"D:\GitProjects\MagicUpdater\MagicUpdater\bin\Release\MagicUpdater.exe";
			const string SERVICE_PATH = @"C:\SystemUtils\MagicUpdater\MagicUpdater.exe";
			const string SERVICE_DISPLAY_NAME = "MagicUpdater";
			const string SERVICE_NAME = "MagicUpdater";
			const string MAGIC_UPDATER_PATH = @"C:\SystemUtils\MagicUpdater";
			const string MAGIC_UPDATER_NEW_VER_PATH = @"C:\SystemUtils\MagicUpdaterNewVer";
			const string SETTINGS_FILE_NAME = "settings.json";

			ServiceInstaller serviceInstaller = new ServiceInstaller();
			ServiceController service = new ServiceController(SERVICE_NAME);
			try
			{
				if (string.IsNullOrEmpty(_action))
				{
					TimeSpan timeout = TimeSpan.FromMilliseconds(30000);

					Console.WriteLine("Тормозим старую службу...");
					//Тормозим старую службу
					try
					{
						service.Stop();
						service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
						AddMessage($"Старая служба {SERVICE_NAME} - остановлена");
						NLogger.LogDebugToHdd($"Старая служба {SERVICE_NAME} - остановлена");
						Console.WriteLine("Старая служба остановлена");
					}
					catch (Exception ex)
					{
						AddMessage(ex.Message);
						NLogger.LogDebugToHdd(ex.Message);
						Console.WriteLine(ex.Message);
					}

					Console.WriteLine("Удаляем старую службу...");
					//Удаляем старую службу
					try
					{
						serviceInstaller.UnInstallService(SERVICE_NAME);
						Thread.Sleep(3000);
						NLogger.LogDebugToHdd($"Старая служба {SERVICE_NAME} - удалена");
						AddMessage($"Старая служба {SERVICE_NAME} - удалена");

						Console.WriteLine($"Старая служба {SERVICE_NAME} - удалена");
					}
					catch (Exception ex)
					{
						AddMessage(ex.Message);
						NLogger.LogDebugToHdd(ex.Message);
						Console.WriteLine(ex.Message);
					}

					Console.WriteLine("Убиваем все процессы MagicUpdater...");
					//Убиваем все процессы MagicUpdater
					Process[] procs = Process.GetProcessesByName(SERVICE_NAME);
					foreach (var proc in procs)
					{
						proc.Kill();
					}
					Thread.Sleep(3000);
					NLogger.LogDebugToHdd($"Все процессы {SERVICE_NAME} - убиты");
					AddMessage($"Все процессы {SERVICE_NAME} - убиты");
					Console.WriteLine("Все процессы MagicUpdater завершены");

					Console.WriteLine("Чистим реестр (автозагрузка MU как приложения в корне run)...");
					//Чистим реестр (автозагрузка MU как приложения в корне run)
					#region Чистим реестр
					string keyName = @"Software\Microsoft\Windows\CurrentVersion\Run";
					using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName, true))
					{
						if (key == null)
						{
							Console.WriteLine($"CurrentUser: Отсутствует путь в реестре {keyName}");
							NLogger.LogErrorToHdd($"CurrentUser: Отсутствует путь в реестре {keyName}");
							AddMessage($"CurrentUser: Отсутствует путь в реестре {keyName}");
						}
						else
						{
							try
							{
								key.DeleteValue(SERVICE_NAME);
								Console.WriteLine($"CurrentUser: Значение реестра - {SERVICE_NAME} удалено");
								NLogger.LogDebugToHdd($"CurrentUser: Значение реестра - {SERVICE_NAME} удалено");
								AddMessage($"CurrentUser: Значение реестра - {SERVICE_NAME} удалено");
							}
							catch (Exception ex)
							{
								NLogger.LogDebugToHdd($"CurrentUser: {ex.Message}");
								Console.WriteLine($"CurrentUser: {ex.Message}");
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
								Console.WriteLine($"LocalMachine: Значение реестра - {SERVICE_NAME} удалено");
								NLogger.LogDebugToHdd($"LocalMachine: Значение реестра - {SERVICE_NAME} удалено");
								AddMessage($"LocalMachine: Значение реестра - {SERVICE_NAME} удалено");
							}
							catch (Exception ex)
							{
								Console.WriteLine($"LocalMachine: {ex.Message}");
								NLogger.LogDebugToHdd($"LocalMachine: {ex.Message}");
								AddMessage($"LocalMachine: {ex.Message}");
							}
						}
					}
					#endregion Чистим реестр

					Console.WriteLine("Удаляем все из папок MagicUpdater и MagicUpdaterNewVer...");
					//Удаляем все из папок MagicUpdater и MagicUpdaterNewVer
					#region Удаляем все из папок MagicUpdater и MagicUpdaterNewVer
					try
					{
						DirectoryInfo di = new DirectoryInfo(MAGIC_UPDATER_PATH);

						foreach (FileInfo file in di.GetFiles())
						{
							if (file.Name.ToUpper() != SETTINGS_FILE_NAME.ToUpper())
							{
								file.Delete();
							}
						}
						foreach (DirectoryInfo dir in di.GetDirectories())
						{
							dir.Delete(true);
						}
						Console.WriteLine($"Путь {MAGIC_UPDATER_PATH} - очищен");
						NLogger.LogDebugToHdd($"Путь {MAGIC_UPDATER_PATH} - очищен");
						AddMessage($"Путь {MAGIC_UPDATER_PATH} - очищен");
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
						NLogger.LogErrorToHdd(ex.ToString());
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
						Console.WriteLine($"Путь {MAGIC_UPDATER_NEW_VER_PATH} - очищен");
						NLogger.LogDebugToHdd($"Путь {MAGIC_UPDATER_NEW_VER_PATH} - очищен");
						AddMessage($"Путь {MAGIC_UPDATER_NEW_VER_PATH} - очищен");
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
						NLogger.LogErrorToHdd(ex.ToString());
						AddMessage(ex.Message);
					}
					#endregion Удаляем все из папок MagicUpdater и MagicUpdaterNewVer

					Thread.Sleep(1000);

					Console.WriteLine("Скачиваем новую версию...");
					//Копируем новый MagicUpdater целиком!
					#region Копируем новый MagicUpdater целиком!
					using (Ftp ftp = new Ftp())
					{
						ftp.Connect("mskftp.sela.ru");  // or ConnectSSL for SSL
						ftp.Login("cis_obmen", "cisobmen836");
						try
						{
							Directory.CreateDirectory(MAGIC_UPDATER_PATH);
							ftp.DownloadFiles("MagicUpdaterTest", MAGIC_UPDATER_PATH, new RemoteSearchOptions("*", true));
						}
						finally
						{
							if (ftp.Connected)
								ftp.Close();
						}
					}
					Console.WriteLine($"Закачка нового {SERVICE_NAME} - завешена");
					NLogger.LogDebugToHdd($"Закачка нового {SERVICE_NAME} - завешена");
					AddMessage($"Закачка нового {SERVICE_NAME} - завешена");
					#endregion Копируем новый MagicUpdater целиком!

					//Устанавливаем службу с режимом автозапуска
					//Запускаем службу
					Thread.Sleep(3000);

					Console.WriteLine("Создаем задачу MuInstallService в планировщике для установки службы...");
					NLogger.LogDebugToHdd("Создаем задачу MuInstallService в планировщике для установки службы...");
					try
					{
						CreateMuInstallServiceSchedule("MuInstallService");
						Console.WriteLine("Задача MuInstallService создана");
						NLogger.LogDebugToHdd("Задача MuInstallService создана");
					}
					catch(Exception ex)
					{
						Console.WriteLine($"Задача MuInstallService: {ex.Message}");
						NLogger.LogErrorToHdd($"Задача MuInstallService: {ex.ToString()}");
					}
					
					//string path = System.Reflection.Assembly.GetEntryAssembly().Location;
					//Process.Start(path, $"{_operationId.ToString()} restart");
				}
				else if (_action == "restart")
				{
					serviceInstaller.InstallService(SERVICE_PATH, SERVICE_NAME, SERVICE_DISPLAY_NAME);
					Console.WriteLine($"Новая служба {SERVICE_NAME} - установлена и запущена");
					NLogger.LogDebugToHdd($"Новая служба {SERVICE_NAME} - установлена и запущена");
					AddMessage($"Новая служба {SERVICE_NAME} - установлена и запущена");

					//serviceInstaller.UnInstallService(SERVICE_NAME);

					SendMessagesToOperation(true);
					DeleteSchedule("MuInstallService");
				}
				else if (_action == "schedule")
				{
					CreateMuInstallServiceSchedule("MuInstallService");
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				NLogger.LogErrorToHdd(ex.ToString());
				AddMessage(ex.Message);
				SendMessagesToOperation(false);
			}

			//Console.Read();
		}

		private static void CreateMuInstallServiceSchedule(string taskName)
		{
			using (TaskService ts = new TaskService())
			{
				// Create a new task definition and assign properties
				TaskDefinition td = ts.NewTask();
				td.RegistrationInfo.Description = "MuInstallService";

				// Create a trigger that will fire the task at this time every other day
				td.Triggers.Add(new TimeTrigger
				{
					Enabled = true,
					StartBoundary = DateTime.Now + TimeSpan.FromSeconds(30),
					ExecutionTimeLimit = TimeSpan.FromMinutes(10),
					Id = "MuInstallServiceTrigger"
				});

				// Create an action that will launch Notepad whenever the trigger fires
				td.Actions.Add(new ExecAction(System.Reflection.Assembly.GetEntryAssembly().Location, $"{_operationId.ToString()} restart", null));

				// Register the task in the root folder
				ts.RootFolder.RegisterTaskDefinition(taskName, td);
			}
		}

		private static void DeleteSchedule(string taskName)
		{
			using (TaskService ts = new TaskService())
			{
				// Remove the task we just created
				ts.RootFolder.DeleteTask(taskName);
			}
		}

		private static void AddMessage(string message)
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

		private static void SendMessagesToOperation(bool isCompleted)
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
				//string mess = "sdasdasd";
				Operation.SendOperationReport(_operationId, mess, isCompleted);
				Operation.AddOperState(_operationId, OperStates.End);
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToHdd(ex.ToString());
			}
		}

		#endregion Main method + testing code - Commented

		/// <summary>
		/// This method installs and runs the service in the service control manager.
		/// </summary>
		/// <param name="svcPath">The complete path of the service.</param>
		/// <param name="svcName">Name of the service.</param>
		/// <param name="svcDispName">Display name of the service.</param>
		/// <returns>True if the process went thro successfully. False if there was any error.</returns>
		public bool InstallService(string svcPath, string svcName, string svcDispName)
		{
			#region Constants declaration.
			int SC_MANAGER_CREATE_SERVICE = 0x0002;
			int SERVICE_WIN32_OWN_PROCESS = 0x00000010;
			//int SERVICE_DEMAND_START = 0x00000003;
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
							//Console.WriteLine("Couldnt start service");
							return false;
						}
						//Console.WriteLine("Success");
						CloseServiceHandle(sc_handle);
						return true;
					}
				}
				else
					//Console.WriteLine("SCM not opened successfully");
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
				//Console.WriteLine(svc_hndl.ToInt32());
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
