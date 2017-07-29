using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicUpdaterCommon.Abstract
{
	public interface ICmdParams1C
	{
		string Base1C { get; }
		string UpdateAndPrintLog { get; }
		string StaticUpdateAndPrintLog { get; }
		string Update { get; }
		string StaticUpdate { get; }
		string HiddenEnterpriseStart { get; }
		string GetConnectionstring1C();
	}
}
