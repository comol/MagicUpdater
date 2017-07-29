using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using SmartAssembly.Attributes;
using System.ComponentModel;

namespace MagicUpdater.Operations
{
#pragma warning disable CS0436 // Type conflicts with imported type
	[DoNotObfuscateType()]
#pragma warning restore CS0436 // Type conflicts with imported type
	class SetExternalIp_Service : Operation
	{
		public SetExternalIp_Service(int? operationId) : base(operationId) { }

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			if (MainSettings.MainSqlSettings.ComputerId != null)
			{
				SqlWorks.ExecProc("SetExternalIp_Service"
					, MainSettings.MainSqlSettings.ComputerId
					, NetWork.GetExternalIpAddress());
			}
		}
	}
}
