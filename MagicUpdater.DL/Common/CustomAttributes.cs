using System;

namespace MagicUpdater.DL.Common
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class NotMapRefreshingGridView : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class SetCurrentTimeZome : Attribute
	{
	}
}
