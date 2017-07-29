using MagicUpdater.DL.Common;

namespace MagicUpdater.DL.Models
{
	public class OperationTypeModel
	{
		[NotMapRefreshingGridView]
		public int Id { get; set; }
		[NotMapRefreshingGridView]
		public string Name { get; set; }
		[NotMapRefreshingGridView]
		public string NameRus { get; set; }
		public string DisplayGridName { get; set; }
		[NotMapRefreshingGridView]
		public int GroupId { get; set; }
		[NotMapRefreshingGridView]
		public string FileName { get; set; }
		[NotMapRefreshingGridView]
		public string FileMd5 { get; set; }
		[NotMapRefreshingGridView]
		public string Description { get; set; }
		[NotMapRefreshingGridView]
		public bool? Visible { get; set; }
	}
}
