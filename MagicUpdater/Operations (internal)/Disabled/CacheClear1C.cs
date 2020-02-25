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
			return base.BeforeExecution();
		}

		protected override void Execution(object sender, DoWorkEventArgs e)
		{
			new KillProcess1C(this.Id).ActRun(); //Грохаем все «1сv8 *.exe»
			new DelCacheFolders1C(this.Id).ActRun(); //Удаляем папки с кэшем
		}

		protected override void AfterExecution()
		{

		}
	}
}
