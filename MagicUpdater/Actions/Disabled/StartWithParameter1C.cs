using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MagicUpdater.Actions
{
	[Obsolete("Метод заглушка! Перенесен в [MagicUpdaterCommon.CommonActions]! Используйте [MagicUpdaterCommon.CommonActions.StartWithParameter1C]!")]
	public class StartWithParameter1C : OperAction
	{
		public Process NewProc { get; set; } = null;
		private string parameters = string.Empty;
		MagicUpdaterCommon.CommonActions.StartWithParameter1C _startWithParameter1C;

		//public StartWithParameter1C(int? _operationId) : base(_operationId) { }

		public StartWithParameter1C(int? _operationId, string _parameters) : base(_operationId)
		{
			_startWithParameter1C = new MagicUpdaterCommon.CommonActions.StartWithParameter1C(_operationId, _parameters);
			parameters = _parameters;
		}

		protected override void ActExecution()
		{
			try
			{
				_startWithParameter1C.ActRun();
				NewProc = _startWithParameter1C.NewProc;
			}
			catch
			{
				SendReportToDB($"ExePath1C: {MainSettings.ExePath1C}, parameters: {parameters}");
				throw;
			}
		}
	}
}
