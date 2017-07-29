using MagicUpdater.DL.Common;
using System;
using System.Drawing;

namespace MagicUpdater.DL.Models
{
	public class ShopComputersModel
	{
		public Bitmap IsOnBitmap
		{
			get
			{
				if (IsOn)
				{
					return Images.accept16;
				}
				else
				{
					return Images.delete16;
				}
			}
		}

		[NotMapRefreshingGridView]
		public bool IsOn{ get; set; }

		[NotMapRefreshingGridView]
		public string ShopId { get; set; }

		public string ShopName { get; set; }

		[NotMapRefreshingGridView]
		public bool ExchangeError { get; set; }

		public bool IsClosed { get; set; }
		
		public int ComputerId { get; set; }

		public string ComputerName { get; set; }

		[NotMapRefreshingGridView]
		public string Local_IP { get; set; }

		public string External_IP { get; set; }

		[NotMapRefreshingGridView]
		public string Email { get; set; }
		
		public bool Is1CServer { get; set; }

		public bool IsMainCashbox { get; set; }

		[NotMapRefreshingGridView]
		public bool? IsTaskerAlive { get; set; }

		public DateTime? LastSuccessfulUpload { get; set; }

		public DateTime? LastSuccessfulReceive { get; set; }

		public string OperationTypeRu { get; set; }

		[SetCurrentTimeZome]
		public DateTime? OperationCreationDate { get; set; }
		public string OperState { get; set; }
		[NotMapRefreshingGridView]
		public DateTime? LastErrorDate { get; set; }
		[NotMapRefreshingGridView]
		public string LastError { get; set; }

		public string LastErrorString => LastErrorDate.HasValue ?
			$"[{LastErrorDate.Value.Add(TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now))}]: {LastError ?? ""}" :
			"";

		public string MagicUpdaterVersion { get; set; }

		[NotMapRefreshingGridView]
		public string AddressToConnect { get; set; }

		[NotMapRefreshingGridView]
		public string Phone { get; set; }

		[NotMapRefreshingGridView]
		public bool IsExchangeError { get; set; }		
	}
}
