using MagicUpdater.Actions;
using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.CommonActions;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.ComponentModel;
using System.Threading;

namespace MagicUpdater.Operations
{
	class DynamicUpdate1C : Operation
	{
		public DynamicUpdate1C(int? operationId) : base(operationId) { }
		public override bool IsOnlyMainCashbox => false;
		protected override void Execution(object sender, DoWorkEventArgs e)
		{
			//Грохаем все «1cv8c.exe» на серваке
			if (MainSettings.MainSqlSettings.Is1CServer)
			{
				KillProcess1C ActionKill = new KillProcess1C(Id);
				ActionKill.ActRun();
			}

			Thread.Sleep(3000);

			MagicUpdaterCommon.CommonActions.StartWithParameter1C act = new MagicUpdaterCommon.CommonActions.StartWithParameter1C(Id, MagicUpdaterCommon.SettingsTools.Parameters1C.CmdParams1C.UpdateAndPrintLog); // параметр обновицца и записать лог
			act.ActRun();

			if (!act.NewProc.HasExited)
				act.NewProc.WaitForExit(60000 * 7);

			if (!act.NewProc.HasExited)
				throw new Exception("Процесс конфигуратора не завершился после 7 минут ожидания. Обновление не выполнено.");

			new SendLogsToCenter1C(Id).ActRun(); // Собираем и отправляем в центр логи

			if (!MainSettings.MainSqlSettings.Is1CServer)
			{
				new OpenVikiAndShowNotifyForDynamicUpdate(Id).ActRun(true, false);
			}
		}
	}
}
