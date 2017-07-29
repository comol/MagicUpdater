using MagicUpdater.DL.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicUpdater.DL.Helpers
{
	public static class Extensions
	{
		public static string GetOperationName (this int value)
		{
			return OperationTools.GetOperationNameEnById(value);
		}
	}
}
