using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicUpdater.DL.Models
{
	public class ShopSettingsModel
	{
		public string ShopName { get; set; }
		public string ShopRegion { get; set; }
		public string AddressToConnect { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public bool IsClosed { get; set; }
	}
}
