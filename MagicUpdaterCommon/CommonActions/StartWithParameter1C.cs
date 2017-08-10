using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MagicUpdaterCommon.CommonActions
{
	public class StartWithParameter1C : OperAction
	{
		public Process NewProc { get; set; } = null;

		private string parameters = string.Empty;

		//public StartWithParameter1C(int? _operationId) : base(_operationId) { }

		public StartWithParameter1C(int? _operationId, string _parameters) : base(_operationId)
		{
			parameters = _parameters;
		}

		protected override void ActExecution()
		{
			try
			{
				NewProc = Process.Start(MainSettings.ExePath1C, parameters);
			}
			catch
			{
				string exePath1C = string.IsNullOrEmpty(MainSettings.ExePath1C) ? "ExePath1C: [Отсутствует путь к 1С]" : $"ExePath1C: {MainSettings.ExePath1C}";

				SendReportToDB($"{exePath1C}, parameters: {parameters}{Environment.NewLine}Возможная причина - не установлен флаг: \"Проверка установки 1С\"");
				throw;
			}
		}
	}
}
