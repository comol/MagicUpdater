using MagicUpdaterCommon.Abstract;
using System.ServiceProcess;

namespace MagicUpdater.Actions
{
	public class StopServer1C : OperAction
	{
		public StopServer1C(int? _operationId) : base(_operationId) { }
		protected override void ActExecution()
		{
			ServiceController controller = new ServiceController();

			controller.MachineName = ".";
			controller.ServiceName = "1C:Enterprise 8.3 Server Agent";
			controller.Stop();
			
			//Process.Start("net stop \"1C:Enterprise 8.3 Server Agent\"");
		}
	}
}
