using System;
using System.Text;
using System.IO;
using System.ServiceProcess;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.SettingsTools;

namespace MagicUpdater.Actions
{
	class CouchDBConfiguratorAction : OperAction
	{
		private StringBuilder _logText { get; set; }
		public CouchDBConfiguratorAction(int? _operationId) : base(_operationId) { }

		public bool IsComplete { get; private set; } = true;
		public string MsgForoperation { get; private set; } = "";

		private void Log(string text)
		{
			this._logText.Append($"{text}{Environment.NewLine}");
			NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, text);
		}

		public void RestartService(string serviceName, int timeoutMilliseconds)
		{
			ServiceController service = new ServiceController(serviceName);
			try
			{
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
				return;
			}
		}

		protected override void ActExecution()
		{
			const string ADMIN_LOGIN = "adm";
			const string ADMIN_PASSWORD = "123";

			const string INI_DESTINATION_PATH_WITHOUT_DRIVE = @"CouchDB\etc";
			const string DIRECTORY_NAME = "CouchDB";

			const int SERVICE_RESTART_TIMEOUT_MILLISECONDS = 10000;
			const string COUCH_DB_SERVICE_NAME = "Apache CouchDB";

			const string MARK_FILENAME = @"C:\SystemUtils\CouchDB_Marker_Replication_Retry_Count_INDEX ";

			this._logText = new StringBuilder();

			if (File.Exists(MARK_FILENAME))
			{
				Log($"Marker file was: {MARK_FILENAME} was found.");
				SendReportToDB($"{this._logText.ToString()}{Environment.NewLine}CouchDB Retry Count was already CONFIGURED on this computer.", true);
				MsgForoperation = $"{this._logText.ToString()}{Environment.NewLine}CouchDB Retry Count was already CONFIGURED on this computer.";
				return;
			}
			
			//var request = new Requests();

			#region Change document example
			/*
            try
            {
                var response = request.SendGet("http://localhost:5984/_replicator/replicationtocenter?revs_info=true", ADMIN_LOGIN, ADMIN_PASSWORD);
                var responseObj = NewtonJson.ReadJsonString<dynamic>(response);
                var revZero = (string)(responseObj._revs_info[0].rev); 
                
                var json1 =
                "{" +
                  "\"_id\": \"replicationtocenter\"," +
                  "\"_rev\": \"" + revZero + "\"," +
                  "\"source\": \"http://adm:123@retailcenter.sela.ru:5984/dk_0_remote\"," +
                  "\"target\": \"http://adm:123@localhost:5984/dk_0_remote\"," +
                  "\"continuous\": true," +
                  "\"create_target\": true," +
                  "\"owner\": \"adm\"," +
                  "\"_replication_state\": \"completed\"," +
                  "\"_replication_state_time\": \"2017-03-11T13:00:45+03:00\"," +
                  "\"_replication_state_reason\": \"\"," +
                  "\"_replication_id\": \"7d12141a3205e5f3cfc3b88964190cfc\"," +
                  "\"cancel\": true" +
                "}";
                var response1 = request.SendPost("http://localhost:5984/_replicator", json1, ADMIN_LOGIN, ADMIN_PASSWORD);
                Log($"Результат POST-запроса 1: {Environment.NewLine}{response1}");

                // Создание репликации с именем replicationtocenter2
                var json2 = "{\"_id\": \"replicationtocenter2\",\"source\":  \"http://adm:123@retailcenter.sela.ru:5984/dk_0_remote\",\"target\":  \"http://adm:123@localhost:5984/dk_0_remote\", \"continuous\":true, \"create_target\":  true}";

                var response2 = request.SendPost("http://localhost:5984/_replicator", json2, ADMIN_LOGIN, ADMIN_PASSWORD);
                Log($"Результат POST-запроса 2: {Environment.NewLine}{response2}");
            }
            catch (Exception ex)
            {
                SendReportToDB($"Ошибка при выполнении POST-запроса:{Environment.NewLine}{ex.ToString()}{Environment.NewLine}{"Лог: "}{this._logText.ToString()}");
                return;
            }
            */
			#endregion Change document example


			/*
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
                Log("Max_replication_retry_count"); // adm 123
                var localIninFulFileName = Path.Combine(iniDestinationFullPath, "local.ini");
                var ini = new Ini(localIninFulFileName);

                ini.Load();
                ini.WriteValue("max_replication_retry_count", "replicator", "100");
                ini.Save();

            }
            catch (Exception ex)
            {
                SendReportToDB($"Ошибка при добавлении параметра max_replication_retry_count:{Environment.NewLine}{ex.ToString()}{Environment.NewLine}{"Лог: "}{this._logText.ToString()}");
                return;
            }
          
            Log("Restart service");
            RestartService(COUCH_DB_SERVICE_NAME, SERVICE_RESTART_TIMEOUT_MILLISECONDS);

            */

			var request = new Requests();
			try
			{
				Log("Start making indexes");

				Log(@"by_code...");
				request.SendGetNoReturn(@"http://localhost:5984/dk_0_remote/_design/doc/_view/by_code", ADMIN_LOGIN, ADMIN_PASSWORD);
				Log(@"by_phone...");
				request.SendGetNoReturn(@"http://localhost:5984/dk_0_remote/_design/doc/_view/by_phone", ADMIN_LOGIN, ADMIN_PASSWORD);
				Log(@"by_owner...");
				request.SendGetNoReturn(@"http://localhost:5984/dk_0_remote/_design/doc/_view/by_owner", ADMIN_LOGIN, ADMIN_PASSWORD);
				Log("Finished");

			}
			catch (Exception ex)
			{
				SendReportToDB($"Ошибка при создании индексов:{Environment.NewLine}{ex.ToString()}{Environment.NewLine}{"Лог: "}{this._logText.ToString()}");
				Operation.SendOperationReport(operationId, ex.Message, false);
				Operation.AddOperState(operationId, OperStates.End);
				IsComplete = false;
				MsgForoperation = $"Ошибка при создании индексов:{Environment.NewLine}{ex.ToString()}{Environment.NewLine}{"Лог: "}{this._logText.ToString()}";
				return;
			}

			Operation.AddOperState(operationId, OperStates.End);




			// Добавляем создание маркера успешной установки
			try
			{
				if (!File.Exists(MARK_FILENAME))
				{
					File.WriteAllLines(MARK_FILENAME, new string[0]);
					Log("Success-configure-marker-file was added.");
				}
				else
				{
					Log("Success-configure-marker-file already exists.");
				}
			}
			catch (Exception ex)
			{
				SendReportToDB($"Ошибка при создании маркера успешной настройкии репликации:{Environment.NewLine}{ex.ToString()}{Environment.NewLine}{"Лог: "}{this._logText.ToString()}");
				IsComplete = false;
				MsgForoperation = $"Ошибка при создании маркера успешной настройкии репликации:{Environment.NewLine}{ex.ToString()}{Environment.NewLine}{"Лог: "}{this._logText.ToString()}";
				return;
			}


			SendReportToDB($"{this._logText.ToString()}{Environment.NewLine}CouchDB INDEXES were successfully CONFIGURED.", true);
			MsgForoperation = $"{this._logText.ToString()}{Environment.NewLine}CouchDB INDEXES were successfully CONFIGURED.";

		}
	}
}
