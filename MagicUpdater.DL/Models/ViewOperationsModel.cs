using MagicUpdater.DL.Common;
using MagicUpdater.DL.Helpers;
using System;

namespace MagicUpdater.DL.Models
{
	public class ViewOperationsModel
	{
		[NotMapRefreshingGridView]
		public int ID { get; set; }

		[NotMapRefreshingGridView]
		public int OperationTypeId { get; set; }
		[NotMapRefreshingGridView]
		public string OperationTypeEn { get; set; }
		public string OperationTypeRu { get; set; }
		public string CreatedUser { get; set; }
		public bool IsFromSheduler { get; set; }
		public int ComputerId { get; set; }
		public string ComputerName { get; set; }
		public string ShopId { get; set; }
		[NotMapRefreshingGridView]
		public Nullable<bool> IsRead { get; set; }

		[SetCurrentTimeZome]
		public DateTime CreationDate { get; set; }

		[SetCurrentTimeZome]
		public Nullable<System.DateTime> DateRead { get; set; }
		public string OperState { get; set; }
		[NotMapRefreshingGridView]
		public Nullable<bool> IsCompleted { get; set; }

		[SetCurrentTimeZome]
		public Nullable<System.DateTime> DateCompleteOrError { get; set; }
		public string Result { get; set; }

		[SetCurrentTimeZome]
		public Nullable<System.DateTime> LogDate1C { get; set; }
		public string LogMessage1C { get; set; }
		[NotMapRefreshingGridView]
		public int Is1CError { get; set; }
		[NotMapRefreshingGridView]
		public int IsActionError { get; set; }
		[NotMapRefreshingGridView]
		public Guid? GroupGUID { get; set; }

		[NotMapRefreshingGridView]
		[SetCurrentTimeZome]
		public DateTime? PoolDate { get; set; }
	}
}
