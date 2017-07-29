using System;
using System.Text;
using System.Diagnostics;
using System.IO;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.SettingsTools;

namespace MagicUpdater.Actions
{
	class CouchDBExecuteCommandAction : OperAction
    {
        public CouchDBExecuteCommandAction(int? _operationId) : base(_operationId)
        {
        }

        private StringBuilder _logText { get; set; }
        private void Log(string text)
        {
            this._logText.Append($"{text}{Environment.NewLine}");
            NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, text);
        }

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

        protected override void ActExecution()
        {
            const string BAT_FILE_NAME = @"commands.bat";
            const string CURL_EXE_FILE_NAME = @"curl.exe";
            const string CURL_CRT_EXE_FILE_NAME = @"ca-bundle.crt";
            
            const string DIRECTORY_NAME = "Curl";
            const string FTP_HOST = @"mskftp.sela.ru";
            const string LOGIN = "cis_obmen";
            const string PASSWORD = "cisobmen836";
            const string DESTINATION_DIRECTORY = @"C:\SystemUtils\Curl";

            this._logText = new StringBuilder();
            Log($"Download from FTP {CURL_EXE_FILE_NAME}");

            if (!Directory.Exists(DESTINATION_DIRECTORY))
            {
                Directory.CreateDirectory(DESTINATION_DIRECTORY);
                Log($"Directory {DESTINATION_DIRECTORY} was created");
            }

            var batFileFullPath = Path.Combine(DESTINATION_DIRECTORY, BAT_FILE_NAME);
            var curlFullPath = Path.Combine(DESTINATION_DIRECTORY, CURL_EXE_FILE_NAME);
            var curlCrtFullPath = Path.Combine(DESTINATION_DIRECTORY, CURL_CRT_EXE_FILE_NAME);
            

            try
            {
				FtpWorks.DownloadFileFromFtp(DESTINATION_DIRECTORY, FTP_HOST, LOGIN, PASSWORD, DIRECTORY_NAME, BAT_FILE_NAME);
				FtpWorks.DownloadFileFromFtp(DESTINATION_DIRECTORY, FTP_HOST, LOGIN, PASSWORD, DIRECTORY_NAME, CURL_EXE_FILE_NAME);
				FtpWorks.DownloadFileFromFtp(DESTINATION_DIRECTORY, FTP_HOST, LOGIN, PASSWORD, DIRECTORY_NAME, CURL_CRT_EXE_FILE_NAME);
            }
            catch (Exception ex)
            {
                SendReportToDB($"Ошибка загрузки файлов по FTP:{Environment.NewLine}{ex.ToString()}{Environment.NewLine}{"Лог: "}{this._logText.ToString()}");
                return;
            }

            Log($"Executing command...");

            try
            {
                var executionResult = ExecuteApplication(batFileFullPath, "");
                Log($"Executing completed, code {executionResult}");
            }
            catch (Exception ex)
            {
                SendReportToDB($"Ошибка выполнения команды:{Environment.NewLine}{ex.ToString()}{Environment.NewLine}{"Лог: "}{this._logText.ToString()}");
                return;
            }

            SendReportToDB($"{this._logText.ToString()}{Environment.NewLine}CouchDB COMMAND was successfully completed.", true);

        }
    }
}
