using MagicUpdater.DL.Common;

namespace MagicUpdater.DL.Models
{
	public class ActionsReportsModel
	{
		public int ReportId { get; set; }
		public int OperationId { get; set; }
		public int ComputerId { get; set; }
		public string ActionName { get; set; }
		public bool IsFromLan { get; set; }
		public bool IsCompleted { get; set; }
		[SetCurrentTimeZome]
		public System.DateTime DateCompleteOrError { get; set; }
		public string Result { get; set; }

		public virtual Operation Operation { get; set; }
		public virtual ShopComputer ShopComputer { get; set; }
	}
}
