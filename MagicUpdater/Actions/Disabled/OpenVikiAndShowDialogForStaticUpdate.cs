using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;

namespace MagicUpdater.Actions
{
	public class OpenVikiAndShowDialogForStaticUpdate : OperAction
	{
		public OpenVikiAndShowDialogForStaticUpdate(int? _operationId) : base(_operationId) { }
		public bool ShowMessage { get; set; } = true;
		protected override void ActExecution()
		{
			if (!MainSettings.MainSqlSettings.Is1CServer || MainSettings.MainSqlSettings.IsMainCashbox)
			{
				NetWork.OpenViki();
				//TODO: Сделать связь с приложением
				//if (ShowMessage)
				//	MessageForm.ShowDialogOk("Можно продолжать работу", "Уведомление");
			}
		}
	}
}
