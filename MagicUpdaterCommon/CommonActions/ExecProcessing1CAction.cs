using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MagicUpdaterCommon.CommonActions
{
	public class ExecProcessing1CAction : OperAction
	{
		private string _name;
		private int _timeoutMin;
		public ExecProcessing1CAction(int? _operationId, string name, int timeoutMin = 5) : base(_operationId)
		{
			_name = name;
			_timeoutMin = timeoutMin;
		}

		protected override void ActExecution()
		{
			const string EXTENSION_1C_PROCESSING = ".epf";

			//Чтобы можно было вводить имена файлов с расширением и без
			_name = $"{_name.Replace(EXTENSION_1C_PROCESSING, "")}{EXTENSION_1C_PROCESSING}";

			var res = FtpWorks.DownloadFileFromFtp(MainSettings.Constants.PluginOperationDllDirectoryPath, MainSettings.GlobalSettings.TempFilesFtpPath, _name);

			if (!res.IsComplete)
				throw new Exception(res.Message);

			string fileFullPath = Path.Combine(MainSettings.Constants.PluginOperationDllDirectoryPath, _name);

			if (!File.Exists(fileFullPath))
				throw new Exception($"Файл не существует:{Environment.NewLine}{fileFullPath}");

			var act = new StartWithParameter1C(operationId, Parameters1C.CmdParams1C.ExecProcessing(fileFullPath));
			act.ActRun();

			if (!act.NewProc.HasExited)
				act.NewProc.WaitForExit(60000 * _timeoutMin);

			if (!act.NewProc.HasExited)
				act.NewProc.Kill();
		}
	}
}
