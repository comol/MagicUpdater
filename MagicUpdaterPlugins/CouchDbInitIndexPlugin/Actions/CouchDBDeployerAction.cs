using System;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Net;
using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterCommon.Helpers;

namespace MagicUpdater.Actions
{
	class Requests
    {
		public string SendGet(string uri, string login, string password)
		{
			var request = WebRequest.Create(uri);
			string authInfo = login + ":" + password;
			authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));

			request.Headers["Authorization"] = "Basic " + authInfo;
			request.ContentType = "application/json; charset=utf-8";
			request.Timeout = 1000 * 60 * 60;

			var response = (HttpWebResponse)request.GetResponse();

			string text;
			using (var sr = new StreamReader(response.GetResponseStream()))
			{
				text = sr.ReadToEnd();
			}
			return text;
		}


		public string SendPost(string uri, string json, string login, string password)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            var encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(login + ":" + password));
            httpWebRequest.Headers.Add("Authorization", "Basic " + encoded);

            httpWebRequest.Credentials = new System.Net.NetworkCredential(login, password);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string result = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }

        public void SendPut(string address, byte[] data)
        {
            using (var client = new System.Net.WebClient())
            {
                client.UploadData(address, "PUT", data); // From string: Encoding.ASCII.GetBytes(putData)
            }
        }
    }

    class CouchDBDeployerAction : OperAction
    {
        static private int ExecuteApplication(string exeName, string arguments)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = arguments;
            start.FileName = exeName;
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();
                exitCode = proc.ExitCode;
            }
            return exitCode;
        }

        public void RestartService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                //ExecuteApplication("net", "stop \"Apache CouchDB\"");
                //Thread.Sleep(1000);
                //ExecuteApplication("net", "start \"Apache CouchDB\"");

                int millisec1 = Environment.TickCount;
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch (Exception ex)
            {
                SendReportToDB($"Ошибка при перезапуске службы:{Environment.NewLine}{ex.ToString()}{Environment.NewLine}{"Лог: "}{this._logText.ToString()}");
            }
        }

        private StringBuilder _logText { get; set; }
        private void Log(string text)
        {
            this._logText.Append($"{text}{Environment.NewLine}");
			MagicUpdaterCommon.Helpers.NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, text);
        }

        public CouchDBDeployerAction(int? _operationId) : base(_operationId) { }

        protected override void ActExecution()
        {
            this._logText = new StringBuilder();
            const string SETUP_FILE_NAME = @"apache-couchdb-2.0.0.1.msi";
            const string DEFAULT_INI_FILE_NAME = "default.ini";
            const string INI_DESTINATION_PATH_WITHOUT_DRIVE = @"CouchDB\etc";
            const string DIRECTORY_NAME = "CouchDB";
            const string FTP_HOST = @"mskftp.sela.ru";
            const string LOGIN = "cis_obmen";
            const string PASSWORD = "cisobmen836";
            const int SERVICE_RESTART_TIMEOUT_MILLISECONDS = 10000;
            const string COUCH_DB_SERVICE_NAME = "Apache CouchDB";

            const string ADMIN_LOGIN = "adm";
            const string ADMIN_PASSWORD = "123";
            const string MARK_FILENAME = @"C:\SystemUtils\CouchDB_Marker";

            if (File.Exists(MARK_FILENAME))
            {
                Log($"Marker file was: {MARK_FILENAME} was found.");
                SendReportToDB($"{this._logText.ToString()}{Environment.NewLine}CouchDB was already installed on this computer.", true);
                return;
            }

            Log($"Download from FTP {SETUP_FILE_NAME}");

            var setupFileFullPath = Path.Combine(Path.GetTempPath(), SETUP_FILE_NAME); //Очистить
            var defaultIniFullPath = Path.Combine(Path.GetTempPath(), DEFAULT_INI_FILE_NAME);

            try
            {

				FtpWorks.DownloadFileFromFtp(Path.GetTempPath(), FTP_HOST, LOGIN, PASSWORD, "Couch", SETUP_FILE_NAME);
				FtpWorks.DownloadFileFromFtp(Path.GetTempPath(), FTP_HOST, LOGIN, PASSWORD, "Couch", DEFAULT_INI_FILE_NAME);
				//using (Ftp client = new Ftp())
    //            {
    //                client.Connect(FTP_HOST);
    //                client.Login(LOGIN, PASSWORD);
    //                client.Download(Path.Combine("Couch", SETUP_FILE_NAME), setupFileFullPath);
    //                client.Download(Path.Combine("Couch", DEFAULT_INI_FILE_NAME), defaultIniFullPath);
    //                client.Close();
    //            }
            }
            catch (Exception ex)
            {
                SendReportToDB($"Ошибка загрузки файлов по FTP:{Environment.NewLine}{ex.ToString()}{Environment.NewLine}{"Лог: "}{this._logText.ToString()}");
                return;
            }

            Log($"Download complete");

            Log($"Lauching {SETUP_FILE_NAME}...");
            try
            {
                var setupResult = ExecuteApplication(setupFileFullPath, " /passive"); // (/quiet) /passive - для автоматической установки с показом полосы прогресса
                Log($"Setup was completed with code {setupResult}");
            }
            catch (Exception ex)
            {
                SendReportToDB($"Ошибка инсталлятора:{Environment.NewLine}{ex.ToString()}{Environment.NewLine}{"Лог: "}{this._logText.ToString()}");
                return;
            }

            Log("Check drive letter...");
            var allDrives = DriveInfo.GetDrives();
            string currentLetter = "";
            foreach (var drive in allDrives)
            {
                Log(drive.Name);
                if (Directory.Exists(Path.Combine(drive.Name, DIRECTORY_NAME)))
                {
                    Log($"{drive.Name} - current drive");
                    currentLetter = drive.Name;
                    break;
                }
            }
            var iniDestinationFullPath = Path.Combine(currentLetter, INI_DESTINATION_PATH_WITHOUT_DRIVE);

            if (!Directory.Exists(iniDestinationFullPath))
            {
                Log($"Path {iniDestinationFullPath} wasn't found.");
                SendReportToDB($"{"Лог: "}{this._logText.ToString()}");
                return;
            }

            try
            {
                Log("Backup current ini-file...");
                var iniFileFullName = Path.Combine(iniDestinationFullPath, DEFAULT_INI_FILE_NAME);
                var iniFileBackUp = $"{iniFileFullName}_backup";
                if (File.Exists(iniFileBackUp))
                {
                    System.IO.File.Delete(iniFileBackUp);
                }
                File.Move(iniFileFullName, iniFileBackUp);

                Log("Copy ini-file...");

                File.Copy(defaultIniFullPath, iniFileFullName, true);
                if (!File.Exists(iniFileFullName))
                {
                    Log($"Error! {iniFileFullName} nor found.");
                    SendReportToDB($"{"Лог: "}{this._logText.ToString()}");
                    return;
                }
            }
            catch (Exception ex)
            {
                SendReportToDB($"Ошибка при замене ini-файла:{Environment.NewLine}{ex.ToString()}{Environment.NewLine}{"Лог: "}{this._logText.ToString()}");
                return;
            }

            var request = new Requests();
            try
            {
                Log("Add new bases");

                request.SendPut(@"http://localhost:5984/_users", new byte[0]);
                request.SendPut(@"http://127.0.0.1:5984/_replicator", new byte[0]);
                request.SendPut(@"http://127.0.0.1:5984/_global_changes", new byte[0]);
            }
            catch (Exception ex)
            {
                SendReportToDB($"Ошибка при создании баз:{Environment.NewLine}{ex.ToString()}{Environment.NewLine}{"Лог: "}{this._logText.ToString()}");
                return;
            }

            Log("Restart service");
            this.RestartService(COUCH_DB_SERVICE_NAME, SERVICE_RESTART_TIMEOUT_MILLISECONDS);

            try
            {
                Log("Add Admin"); // adm 123
                var localIninFulFileName = Path.Combine(iniDestinationFullPath, "local.ini");
                var localIniFile = File.ReadAllLines(localIninFulFileName);
                for (int i = 0; i < localIniFile.Length; i++)
                {
                    if (localIniFile[i] == ";admin = mysecretpassword")
                    {
                        localIniFile[i] = "adm = -pbkdf2-4ba62a224fb986d7a08926f131eab71ce8b0ce22,7e5241170e58517b739181ce79ae82f0,10";
                    }
                }
                File.WriteAllLines(localIninFulFileName, localIniFile);
            }
            catch (Exception ex)
            {
                SendReportToDB($"Ошибка при добавлении администратора:{Environment.NewLine}{ex.ToString()}{Environment.NewLine}{"Лог: "}{this._logText.ToString()}");
                return;
            }

            Log("Restart service");
            RestartService(COUCH_DB_SERVICE_NAME, SERVICE_RESTART_TIMEOUT_MILLISECONDS);

            try
            {
                var json = "{\"_id\": \"replicationtocenter\",\"source\":  \"http://adm:123@retailcenter.sela.ru:5984/dk_0_remote\",\"target\":  \"http://adm:123@localhost:5984/dk_0_remote\", \"continuous\":true, \"create_target\": true}";
                var response = request.SendPost("http://localhost:5984/_replicator", json, ADMIN_LOGIN, ADMIN_PASSWORD);
                Log($"Результат POST-запроса: {Environment.NewLine}{response}");
            }
            catch (Exception ex)
            {
                SendReportToDB($"Ошибка при выполнении POST-запроса:{Environment.NewLine}{ex.ToString()}{Environment.NewLine}{"Лог: "}{this._logText.ToString()}");
                return;
            }

            try
            {
                // Чистим временные файлы
                File.Delete(setupFileFullPath);
                File.Delete(defaultIniFullPath);
            }
            catch (Exception ex)
            {
                SendReportToDB($"Ошибка при очистке временных файлов:{Environment.NewLine}{ex.ToString()}{Environment.NewLine}{"Лог: "}{this._logText.ToString()}");
                return;
            }

            // Добавляем создание маркера успешной установки
            try
            {
                if (!File.Exists(MARK_FILENAME))
                {
                    File.WriteAllLines(MARK_FILENAME, new string[0]);
                    Log("Success-marker file was added.");
                }
                else
                {
                    Log("Success-marker file already exists.");
                }
            }
            catch (Exception ex)
            {
                SendReportToDB($"Ошибка при создании маркера успешной установки:{Environment.NewLine}{ex.ToString()}{Environment.NewLine}{"Лог: "}{this._logText.ToString()}");
                return;
            }
           
            SendReportToDB($"{this._logText.ToString()}{Environment.NewLine}CouchDB setup was successfully completed.", true);
            
        }
    }
}
