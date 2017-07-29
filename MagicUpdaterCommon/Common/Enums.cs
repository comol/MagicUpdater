using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicUpdaterCommon.Common
{
	public enum ShedulerTaskModes
	{
		Once = 0,
		Daily = 1,
		Weekly = 2,
		Monthly = 3,
		Hours = 4,
		Minutes = 5
	}

	public enum ShedulerStatuses
	{
		New = 0,
		Running = 1,
		Competed = 2,
		CompletedWithErrors = 3,
		Stoped = 4
	}
}
