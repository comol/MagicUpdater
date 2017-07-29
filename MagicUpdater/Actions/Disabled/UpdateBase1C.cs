using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.CommonActions;
using MagicUpdaterCommon.SettingsTools;
using System;

namespace MagicUpdater.Actions
{
	public class UpdateBase1C : OperAction
	{
		public UpdateBase1C(int? _operationId) : base(_operationId) { }
		protected override void ActExecution()
		{
			StartWithParameter1C act = new StartWithParameter1C(operationId, Parameters1C.CmdParams1C.StaticUpdateAndPrintLog); // параметр обновицца и записать лог
			act.ActRun();

			if (!act.NewProc.HasExited)
				act.NewProc.WaitForExit(60000 * 15);

			if (!act.NewProc.HasExited)
				throw new Exception("Процесс конфигуратора не завершился после 15 минут ожидания. Обновление не выполнено.");

		}
	}
}
