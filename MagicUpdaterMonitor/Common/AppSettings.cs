using MagicUpdater.DL;
using MagicUpdater.DL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicUpdaterMonitor.Common
{
	public static class AppSettings
	{
		public static List<ShopComputer> ShopComputers
		{
			get
			{
				return MQueryCommand.SelectShopComputers();
			}
		}

		public static bool IsServiceMode => Environment.GetCommandLineArgs().Contains("-service");
		public static bool IsShowFilters => Environment.GetCommandLineArgs().Contains("-filters");
	}
}
