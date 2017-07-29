using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace MagicUpdaterCommon.Common
{
	public static class Checks1C
	{
		public static bool Check1C_Connection(string _connection, out string reasonMsg)
		{
			reasonMsg = null;
			bool result = false;
			Process NewProc = new Process();
			NewProc.StartInfo.FileName = MainSettings.ExePath1C;
			NewProc.StartInfo.Arguments = $"DESIGNER {_connection} / DisableStartupMessages /IBCheckAndRepair - TestOnly /Out\"{MainSettings.LogPath1C}\"";
			NewProc.StartInfo.UseShellExecute = true;
			NewProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			NewProc.EnableRaisingEvents = true;
			try
			{
				NewProc.Start();

				// Если за 15 секунд процесс не завершился, значит база недоступна, такй вот костыль(, но лучше чем ничего)))
				for (int i = 0; i < 15; i++)
				{
					Thread.Sleep(1000);
					if (NewProc.HasExited)
						break;
				}

				if (!NewProc.HasExited)
				{
					//Считаем что база недоступна
					//Лог 1С будет пустым
					NewProc.Kill();
				}
				else
				{
					//Считаем что база доступна
					string txt = File.ReadAllText(MainSettings.LogPath1C, Encoding.GetEncoding("windows-1251"));
					bool isError = !txt.ToLower().Contains("тестирование закончено");
					if (isError)
						reasonMsg = txt;
					else
						result = true;
				}
			}
			catch (Exception ex)
			{
				reasonMsg = ex.Message.ToString();
				NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, ex.Message.ToString());
			}

			return result;
		}

		public static bool Check1C_Connection(string _connection)
		{
			string msg;
			return Check1C_Connection(_connection, out msg);
		}
	}
}
