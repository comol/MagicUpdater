using MagicUpdaterCommon.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MagicUpdaterCommon.SettingsTools;
using System.Diagnostics;

namespace SetAutomaticDelayedStartPlugin
{
	public class SetAutomaticDelayedStartPlugin : Operation
	{
		public SetAutomaticDelayedStartPlugin(int? operationId) : base(operationId)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			Process process = Process.Start("sc.exe", $"config {MainSettings.Constants.MAGIC_UPDATER} start= delayed-auto");
			process.WaitForExit(10000);
		}
	}

	public class RegistrationParams : IRegistrationParams
	{
		public string Description => "";

		public int GroupId => 4;

		public string NameRus => "Установить отложенный запуск агента (плагин)";
	}
}
