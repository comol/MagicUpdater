using System;
using System.Text;
using System.ServiceProcess;
using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;

namespace MagicUpdater.Actions
{
	class CouchDBRestartServiceAction : OperAction
    {
        const int SERVICE_RESTART_TIMEOUT_MILLISECONDS = 10000;
        const string COUCH_DB_SERVICE_NAME = "Apache CouchDB";

        private StringBuilder _logText { get; set; }
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
            }
        }

        public CouchDBRestartServiceAction(int? _operationId) : base(_operationId)
        {
        }

        protected override void ActExecution()
        {
            this._logText = new StringBuilder();
            Log("Restart service");
            RestartService(COUCH_DB_SERVICE_NAME, SERVICE_RESTART_TIMEOUT_MILLISECONDS);
            SendReportToDB($"{this._logText.ToString()}{Environment.NewLine}CouchDB service was successfully RESTARTED.", true);
        }
    }
}
