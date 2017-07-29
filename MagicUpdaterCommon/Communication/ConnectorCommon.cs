using MagicUpdaterCommon.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicUpdaterCommon.Communication
{
	public class TrySendMessageAndWaitForResponse : TryResult
	{
		public TrySendMessageAndWaitForResponse(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}

	public class TryToStartSettingsApplication : TryResult
	{
		public TryToStartSettingsApplication(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}

	public class TryAsyncPipesAction : TryResult
	{
		public TryAsyncPipesAction(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}

	public class TryToStartService : TryResult
	{
		public TryToStartService(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}
}
