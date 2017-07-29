using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.Helpers;

namespace MagicUpdater.Actions
{
	class OpenVikiAndShowNotifyForDynamicUpdate : OperAction
	{
		public OpenVikiAndShowNotifyForDynamicUpdate(int? _operationId) : base(_operationId)
		{
		}

		protected override void ActExecution()
		{
			NetWork.OpenViki();
		}
	}
}
