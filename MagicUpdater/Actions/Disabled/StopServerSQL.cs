using MagicUpdaterCommon.Abstract;
using System;

namespace MagicUpdater.Actions
{
	class StopServerSQL : OperAction
	{
		public StopServerSQL(int? _operationId) : base(_operationId) { }
		protected override void ActExecution()
		{
			throw new NotImplementedException();
		}
	}
}
