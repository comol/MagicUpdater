using Communications.Common;
using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.Common;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterRestart.RestartTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MagicUpdaterRestart.Actions
{
	public class StartSettingsViaPipe : OperAction
	{
		private int _timeOut;
		public StartSettingsViaPipe(int? _operationId, int timeOut = 20000) : base(_operationId)
		{
			_timeOut = timeOut;
		}

		protected override void ActExecution()
		{
			try
			{
				if (Tools.GetProcessesCount(MainSettings.Constants.MAGIC_UPDATER_RESTART) == 1)
				{
					bool restartSettingsConnected = false;
					Task.Factory.StartNew(() =>
					{
						PipeClient pipeClient = new PipeClient();
						pipeClient.SendSyncMessage(new CommunicationObject
						{
							ActionType = CommunicationActionType.StartMagicUpdaterSettings
						});
						restartSettingsConnected = true;
					});

					while (!restartSettingsConnected)
					{
						Thread.Sleep(1);
						_timeOut--;
						if (_timeOut <= 0)
						{
							this.SendReportToDB($"Выход по тайм-ауту. Не удалось запустить {MainSettings.Constants.MAGIC_UPDATER_SETTINGS}");
						}
					}
				}
				else
				{
					this.SendReportToDB($"Отсутствует процесс \"{MainSettings.Constants.MAGIC_UPDATER_RESTART}\" для \"{MainSettings.Constants.MAGIC_UPDATER_SETTINGS}\".{Environment.NewLine}Приложение \"{MainSettings.Constants.MAGIC_UPDATER_SETTINGS}\" не запущено.");
				}
			}
			catch (Exception ex)
			{
				if (operationId > 0)
				{
					Operation.SendOperationReport(operationId, $"Не удалось перезапустить MagicUpdaterSettings. Original: {ex.ToString()}", false);
				}
				NLogger.LogErrorToHdd($"Не удалось перезапустить MagicUpdaterSettings. Original: {ex.ToString()}", MainSettings.Constants.MAGIC_UPDATER_RESTART);
			}
		}
	}
}
