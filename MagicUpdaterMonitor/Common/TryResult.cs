using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicUpdaterMonitor.Common
{
	public abstract class TryResult
	{
		protected bool _isComplete;
		protected string _message;
		protected virtual string Name { get; set; } = "";

		public bool IsComplete => _isComplete;
		public string Message => _message;

		public string NamedMessage => !string.IsNullOrEmpty(Name) ? $"{Name}: {_message}" : _message;

		public TryResult(bool isComplete = true, string message = "")
		{
			_isComplete = isComplete;
			_message = message;
		}
	}
}
