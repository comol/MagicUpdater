using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicUpdaterMonitor.Models
{
	public class ShopComputersModel
	{
		public string ShopId { get; set; }
		public int ComputerId { get; set; }
		public string ComputerName { get; set; }
		public string Local_IP { get; set; }
		public string External_IP { get; set; }
		public bool Is1CServer { get; set; }
		public bool IsMainCashbox { get; set; }
		public bool? IsTaskerAlive { get; set; }
		public string MagicUpdaterVersion { get; set; }
		public int IsOn { get; set; }
	}
}
