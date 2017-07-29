using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicUpdater.DL.Helpers
{
	public static class MLogger
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public static void Debug(string message)
		{
			logger.Debug(message);
		}

		public static void Error(string message)
		{
			logger.Error(message);
		}
	}
}
