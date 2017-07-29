using MagicUpdater.DL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicUpdater.DL.Models
{
	public class ViewShedulerStepModel
	{
		[NotMapRefreshingGridView]
		public int Id { get; set; }
		public Nullable<long> Step { get; set; }
		[NotMapRefreshingGridView]
		public int TaskId { get; set; }
		[NotMapRefreshingGridView]
		public int OperationType { get; set; }
		[NotMapRefreshingGridView]
		public string Name { get; set; }
		[NotMapRefreshingGridView]
		public string NameRus { get; set; }
		public string nameVis { get; set; }
		[NotMapRefreshingGridView]
		public string nameVisCount { get; set; }
		[NotMapRefreshingGridView]
		public Nullable<int> RepeatCount { get; set; }
		[NotMapRefreshingGridView]
		public Nullable<int> RepeatTimeout { get; set; }
		[NotMapRefreshingGridView]
		public int OperationCheckIntervalMs { get; set; }
		[NotMapRefreshingGridView]
		public int OnOperationCompleteStep { get; set; }
		[NotMapRefreshingGridView]
		public string NameCompleteStep { get; set; }
		[NotMapRefreshingGridView]
		public string NameRusCompleteStep { get; set; }
		[NotMapRefreshingGridView]
		public string nameVisCompleteStep { get; set; }
		public string nameVisCountCompleteStep { get; set; }
		[NotMapRefreshingGridView]
		public int OnOperationErrorStep { get; set; }
		[NotMapRefreshingGridView]
		public string NameErrorStep { get; set; }
		[NotMapRefreshingGridView]
		public string NameRusErrorStep { get; set; }
		[NotMapRefreshingGridView]
		public string nameVisErrorStep { get; set; }
		public string nameVisCountErrorStep { get; set; }
		[NotMapRefreshingGridView]
		public int OrderId { get; set; }

		[NotMapRefreshingGridView]
		public ViewShedulerStepsVi ViewShedulerStepEntity { get; set; } = null;
		[NotMapRefreshingGridView]
		public string OperationAttributes { get; set; }
	}
}
