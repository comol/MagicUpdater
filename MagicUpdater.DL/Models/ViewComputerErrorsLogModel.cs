using MagicUpdater.DL.Common;

namespace MagicUpdater.DL.Models
{
	public class ViewComputerErrorsLogModel
	{
		public int ErrorId { get; set; }
		public int ComputerId { get; set; }
		public string ComputerName { get; set; }
		public string External_IP { get; set; }
		public bool Is1CServer { get; set; }
		public bool IsMainCashbox { get; set; }
		public bool? IsTaskerAlive { get; set; }
		public int IsON { get; set; }
		public string ErrorMessage { get; set; }

		[SetCurrentTimeZome]
		public System.DateTime ErrorDate { get; set; }
	}
}
