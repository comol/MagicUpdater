using MagicUpdaterCommon.Abstract;
using System.ComponentModel;

namespace MagicUpdater.Operations
{
	class SetOperationsListCheckTimeout : OperationWithAttr<SetOperationsListCheckTimeoutAttr>
	{
		public SetOperationsListCheckTimeout(int operationID, string attrsJson) : base(operationID, attrsJson) { }

		protected override void Execution(object sender, DoWorkEventArgs e)
		{

			//Демонстрация передачи атрибутов из базы
			//MessageBox.Show("Execution!!!");
			//MessageBox.Show(Attributes.Timeout.ToString());
		}
	}

	public class SetOperationsListCheckTimeoutAttr : IOperationAttributes
	{
		public int Timeout { get; set; }
	}
}
