using MagicUpdater.Actions;
using MagicUpdaterCommon.Abstract;
using System.ComponentModel;

namespace MagicUpdater.Operations
{
	class CacheClear1C : Operation
	{
		public CacheClear1C(int? operationId) : base(operationId) { }

		public new bool IsOnlyMainCashbox => true;

		protected override bool BeforeExecution()
		{
			//TODO: Сделать связь с приложением
			//По нажатию «ок» продолжаем месить! И не забыть пульнуть другим компам через сокеты о предстоящем замесе.
			//if (MessageForm.ShowDialogOk("Необходимо выполнить очистку кэша", "Предупреждение", "Выполнить") == DialogResult.OK)
			//	return base.BeforeExecution();
			//else
			//	return false;
			return base.BeforeExecution();
		}

		protected override void Execution(object sender, DoWorkEventArgs e)
		{
			new KillProcess1C(this.Id).ActRun(); //Грохаем все «1сv8 *.exe»
			new DelCacheFolders1C(this.Id).ActRun(); //Удаляем папки с кэшем
		}

		protected override void AfterExecution()
		{
			//TODO: Сделать связь с приложением
			//MessageForm.ShowDialogOk("Можно продолжать работу", "Уведомление");
		}
	}
}
