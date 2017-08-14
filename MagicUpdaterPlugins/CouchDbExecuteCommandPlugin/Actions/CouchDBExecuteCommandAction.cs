using System;
using System.Text;
using System.Diagnostics;
using System.IO;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.SettingsTools;
using System.Collections.Generic;

namespace MagicUpdater.Actions
{
    public class CouchDBExecuteCommandAction : OperAction
    {
        private string _command;
        private string _parameterName;
        public string Result { get; private set; }
        public CouchDBExecuteCommandAction(int? _operationId, string command, string parameterName) : base(_operationId)
        {
            this._command = command;
            this._parameterName = parameterName;
        }

        private StringBuilder _logText { get; set; }
        private void Log(string text)
        {
            this._logText.Append($"{text}{Environment.NewLine}");
            //NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, text);
        }

        static private List<string> ExecuteApplicationWithResult(string exeName, string arguments)
        {
            var result = new List<string>();
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = exeName,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                result.Add(line);
            }
            return result;
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
            var executionResult = new List<string>();
            try
            {
                //this._command = @"http://localhost:5984/dk_0_remote";
                executionResult = ExecuteApplicationWithResult(curlFullPath, this._command);
                //var executionResult = ExecuteApplication(batFileFullPath, "");
                Log($"Executing completed, code {executionResult}");
            }
            catch (Exception ex)
            {
                SendReportToDB($"Ошибка выполнения команды:{Environment.NewLine}{ex.ToString()}{Environment.NewLine}{"Лог: "}{this._logText.ToString()}{Environment.NewLine}Console text: {string.Join(Environment.NewLine, executionResult.ToArray())}");
                return;
            }

            //this._parameterName = "doc_count";
            var resultText = string.Empty;
            if (string.IsNullOrEmpty(this._parameterName))
            {
                resultText = $"{this._logText.ToString()}{Environment.NewLine}CouchDB COMMAND was successfully completed.{Environment.NewLine}{string.Join(Environment.NewLine, executionResult.ToArray())}";
            }
            else
            {
                dynamic model = (dynamic)(NewtonJson.GetModelFromJson(string.Join(Environment.NewLine, executionResult.ToArray())));
                resultText = (string)((Newtonsoft.Json.Linq.JObject)model).GetValue(this._parameterName);
            }

            Result = resultText;

        }
    }
}
