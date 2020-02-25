using MagicUpdaterCommon.Abstract;
using System.ComponentModel;

namespace MagicUpdater.Operations
{
	class SetOperationsListCheckTimeout : OperationWithAttr<SetOperationsListCheckTimeoutAttr>
	{
		public SetOperationsListCheckTimeout(int operationID, string attrsJson) : base(operationID, attrsJson) { }

		protected override void Execution(object sender, DoWorkEventArgs e)
		{

		}
	}

	public class SetOperationsListCheckTimeoutAttr : IOperationAttributes
	{
		public int Timeout { get; set; }
	}
}
