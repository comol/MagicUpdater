using MagicUpdater.DL.Types;
using MagicUpdaterMonitor.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicUpdaterMonitor.Models
{
	public class ViewOperationsModel
	{
		public int ID { get; set; }
		public OperationsType OperationType { get; set; }
		public string CreatedUser { get; set; }
		public int ComputerId { get; set; }
		public string ComputerName { get; set; }
		public string ShopId { get; set; }
		//public string Attributes { get; set; }
		public Nullable<bool> IsRead { get; set; }
		public System.DateTime CreationDate { get; set; }
		public Nullable<System.DateTime> DateRead { get; set; }
		public string OperState { get; set; }
		public Nullable<bool> IsCompleted { get; set; }
		public Nullable<System.DateTime> DateCompleteOrError { get; set; }
		public string Result { get; set; }
		public Nullable<System.DateTime> LogDate1C { get; set; }
		public string LogMessage1C { get; set; }
		public int Is1CError { get; set; }
		public int IsActionError { get; set; }
	}
}
