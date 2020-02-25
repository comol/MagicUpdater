using MagicUpdater.Actions;
using MagicUpdaterCommon.Abstract;
using System.ComponentModel;

namespace MagicUpdater.Operations
{
	public class ServerRestart1C : Operation
	{
		public ServerRestart1C(int? operationId) : base(operationId) { }
		public new bool IsOnlyMainCashbox => true;
		protected override void Execution(object sender, DoWorkEventArgs e)
		{
			new StopServer1C(Id).ActRun(); //Останавливаем сервер 1С
			new StartServer1C(Id).ActRun(); //Стартуем сервер 1C
		}
	}
}
