using Communications.Common;
using MagicUpdater.Core;
using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.Common;
using MagicUpdaterCommon.SettingsTools;
using System.Threading;

namespace MagicUpdater.Actions
{
	public class StartSettingsAferUpdate : OperAction
	{
		public StartSettingsAferUpdate(int? _operationId) : base(_operationId)
		{
		}

		protected override void ActExecution()
		{
			if (Tools.GetProcessesCount(MainSettings.Constants.MAGIC_UPDATER_SETTINGS, true) > 0)
			{
				var res = MuCore.ConnectionToSettings.SendAsyncMessageAndWaitForResponse(new CommunicationObject
				{
					ActionType = CommunicationActionType.StartMagicUpdaterRestartForSettings,
					Data = operationId
				}, 20000);

				if (!res.IsComplete)
				{
					this.SendReportToDB(res.Message);
				}

				Thread.Sleep(2000);

				//Останавливаем MagicUpdaterSettings
				Tools.KillAllProcessByname(MainSettings.Constants.MAGIC_UPDATER_SETTINGS, true);
			}
			else
			{
				this.SendReportToDB($"Процесс \"{MainSettings.Constants.MAGIC_UPDATER_SETTINGS}\" не запущен.");
			}
		}
	}
}
