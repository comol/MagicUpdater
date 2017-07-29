using MagicUpdater.DL.Types;
using MagicUpdaterMonitor.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicUpdaterMonitor.Models
{
	public class OperationsModel
	{
		public int ComputerId { get; set; }
		public OperationsType OperationType { get; set; }
	}
}
