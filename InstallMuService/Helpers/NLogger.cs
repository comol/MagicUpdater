using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstallService.Helpers
{
	public static class NLogger
	{
		public static void LogErrorToHdd(string errorMessage)
		{
			LogManager.GetCurrentClassLogger().Error(errorMessage);
		}

		public static void LogDebugToHdd(string debugMessage)
		{
			LogManager.GetCurrentClassLogger().Debug(debugMessage);
		}
	}
}
