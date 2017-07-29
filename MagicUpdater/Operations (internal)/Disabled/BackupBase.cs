using MagicUpdater.Actions;
using MagicUpdaterCommon.Abstract;
using System.ComponentModel;

namespace MagicUpdater.Operations
{
	public class BackupBase : Operation
	{
		public BackupBase(int? operationId) : base(operationId) { }

		protected override void Execution(object sender, DoWorkEventArgs e)
		{
			new BackupBaseAction(Id).ActRun(); // Бэкапим базу
		}
	}
}
