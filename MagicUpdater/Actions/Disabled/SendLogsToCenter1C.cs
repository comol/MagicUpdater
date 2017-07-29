using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.SettingsTools;
using System.IO;
using System.Text;

namespace MagicUpdater.Actions
{
	public class SendLogsToCenter1C : OperAction
	{
		public SendLogsToCenter1C(int? _operationId) : base(_operationId) { }
		protected override void ActExecution()
		{
			if (File.Exists(MainSettings.LogPath1C))
			{
				string txt = File.ReadAllText(MainSettings.LogPath1C, Encoding.GetEncoding("windows-1251"));
				bool isError = txt.ToLower().Contains("ошибк");
				SqlWorks.ExecProc("SendLog1C", MainSettings.MainSqlSettings.ComputerId, isError, txt, operationId);
				File.Delete(MainSettings.LogPath1C);
			}
		}
	}
}
