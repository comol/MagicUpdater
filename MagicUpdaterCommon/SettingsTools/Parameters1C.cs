using MagicUpdaterCommon.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MagicUpdaterCommon.SettingsTools
{
	public static class Parameters1C
	{
		static Parameters1C()
		{
			CmdParams1C = new CmdParams();
		}

		public static CmdParams CmdParams1C { get; private set; }
	}

	public class CmdParams : ICmdParams1C
	{
		public string Base1C => GetConnectionstring1C();
		public string UpdateAndPrintLog =>
			$"DESIGNER {Base1C} /DisableStartupMessages /DisableStartupDialogs /UpdateDBCfg -WarningsAsErrors -Dynamic+ /Out\"{MainSettings.LogPath1C}\"";

		public string StaticUpdateAndPrintLog => $"DESIGNER {Base1C} /DisableStartupMessages /DisableStartupDialogs /UpdateDBCfg -Dynamic-  /Out\"{MainSettings.LogPath1C}\"";

		public string HiddenEnterpriseStart => $"-Embedding ENTERPRISE {Base1C} /DisableStartupMessages /DisableStartupDialogs  ";

		public string Update => $"DESIGNER {Base1C} /DisableStartupMessages /DisableStartupDialogs /UpdateDBCfg -Dynamic+";

		public string StaticUpdate => $"DESIGNER {Base1C} /DisableStartupMessages /DisableStartupDialogs /UpdateDBCfg -Dynamic-";

		public string ExecProcessing(string processingName)
		{
			return $"ENTERPRISE {Base1C} /DisableStartupMessages /DisableStartupDialogs /Execute \"{processingName}\"";
		}

		public string GetConnectionstring1C()
		{
			string server1C = MainSettings.LocalSqlSettings.Server1C;
			string base1C = MainSettings.LocalSqlSettings.Base1C;
			string user1C = MainSettings.LocalSqlSettings.User1C;
			string pass1C = MainSettings.LocalSqlSettings.Pass1C;
			string fileBasePath = MainSettings.LocalSqlSettings.InformationBaseDirectory;
			bool is1CBaseOnServer = MainSettings.LocalSqlSettings.Is1CBaseOnServer;
			if (is1CBaseOnServer)
			{
				return $"/S\"{Path.Combine(server1C, base1C)}\" /N\"{user1C}\" /P\"{pass1C}\"";
			}
			else
			{
				return $"/F\"{fileBasePath}\" /N\"{user1C}\" /P\"{pass1C}\"";
			}
		}

		public string GetConnectionstring1CServerBase(string server1C, string base1C, string user1C, string pass1C)
		{
			return $"/S\"{Path.Combine(server1C, base1C)}\" /N\"{user1C}\" /P\"{pass1C}\"";
		}

		public string GetConnectionstring1CFileBase(string fileBasePath, string user1C, string pass1C)
		{
			return $"/F\"{fileBasePath}\" /N\"{user1C}\" /P\"{pass1C}\"";
		}

		//public string GetConnectionstring1CFile(string server1C, string base1C, string user1C, string pass1C)
		//{
		//	return $"/F\"{FileBasePath}\" /N\"{user1C}\" /P\"{pass1C}\"";
		//}
	}

	class CmdParamsTest : ICmdParams1C
	{
		public string Base1C => GetConnectionstring1C();
		public string UpdateAndPrintLog => $"DESIGNER {Base1C} /DisableStartupMessages /DisableStartupDialogs /UpdateDBCfg -Dynamic+ /Out\"{MainSettings.LogPath1C}\"";
		public string StaticUpdateAndPrintLog => $"DESIGNER {Base1C} /DisableStartupMessages /DisableStartupDialogs /UpdateDBCfg /Out\"{MainSettings.LogPath1C}\"";
		public string HiddenEnterpriseStart => $"-Embedding ENTERPRISE {Base1C} /DisableStartupMessages /DisableStartupDialogs";

		public string Update => $"DESIGNER {Base1C} /DisableStartupMessages /DisableStartupDialogs /UpdateDBCfg -Dynamic+";

		public string StaticUpdate => $"DESIGNER {Base1C} /DisableStartupMessages /DisableStartupDialogs /UpdateDBCfg";

		public string GetConnectionstring1C()
		{

			return "";
		}
	}
}
