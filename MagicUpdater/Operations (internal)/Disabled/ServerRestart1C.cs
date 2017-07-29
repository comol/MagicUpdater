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
			// TODO: Показываем предупреждение на кассовом компе
			//			По нажатию «ок»

			//new KillProcess1C(Id).ActRun(); //Грохаем все «1cv8c.exe» на всех компах в сети
			new StopServer1C(Id).ActRun(); //Останавливаем сервер 1С
			//new StopServerSQL(Id).ActRun(); //Останавливаем сервер SQL
			//new StartServerSQL(Id).ActRun(); //Стартуем сервер SQL
			new StartServer1C(Id).ActRun(); //Стартуем сервер 1C
		}
	}
}
