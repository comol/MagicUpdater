using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MagicUpdaterCommon.Helpers
{
	public static class WinApi
	{
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool FreeLibrary(IntPtr hModule);

		public static void UnloadModule(string moduleName)
		{
			if (moduleName == "*")
			{
				foreach (ProcessModule mod in Process.GetCurrentProcess().Modules)
				{
					FreeLibrary(mod.BaseAddress);
				}
			}
			else
			{
				foreach (ProcessModule mod in Process.GetCurrentProcess().Modules)
				{
					if (mod.ModuleName == moduleName)
					{
						FreeLibrary(mod.BaseAddress);
					}
				}
			}
		}

	}
}
