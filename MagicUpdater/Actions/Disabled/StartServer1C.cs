using MagicUpdaterCommon.Abstract;
using System.ServiceProcess;

namespace MagicUpdater.Actions
{
	public class StartServer1C : OperAction
	{
		public StartServer1C(int? _operationId) : base(_operationId) { }
		protected override void ActExecution()
		{
			ServiceController controller = new ServiceController();

			controller.MachineName = ".";
			controller.ServiceName = "1C:Enterprise 8.3 Server Agent";
			controller.Start();
		}
	}
}
