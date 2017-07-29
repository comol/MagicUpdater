using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicUpdaterMonitor.Helpers
{
	public static class Extensions
	{
		public static void ForEach<T>(this IEnumerable<T> source,Action<T> action)
		{
			foreach (T element in source)
				action(element);
		}
	}
}
