using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.SettingsTools;
using System.IO;
using System.Text;

namespace MagicUpdaterCommon.CommonActions
{
	public class SendLogsToCenter1CCommon : OperAction
	{
		public SendLogsToCenter1CCommon(int? _operationId) : base(_operationId) { }
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
