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
		[SetCurrentTimeZome]
		public DateTime? AvgPerformanceCounterValuesDateTimeUtc { get; set; }
		[NotMapRefreshingGridView]
		public double? AvgCpuTime { get; set; }
		public string AvgCpuTimeVis => AvgCpuTime == null ? string.Empty : $"{Math.Round(AvgCpuTime.Value).ToString()}%";
		[NotMapRefreshingGridView]
		public double? AvgRamAvailableMBytes { get; set; }
		public string AvgRamAvailableMBytesVis => AvgRamAvailableMBytes == null ? string.Empty : $"{Math.Round(AvgRamAvailableMBytes.Value).ToString()} MB";
		[NotMapRefreshingGridView]
		public double? AvgDiskQueueLength { get; set; }
		public string AvgDiskQueueLengthVis => AvgDiskQueueLength == null ? string.Empty : $"{Math.Round(AvgDiskQueueLength.Value).ToString()}";
		[NotMapRefreshingGridView]
		public DateTime? LastErrorDate { get; set; }
		[NotMapRefreshingGridView]
		public string LastError { get; set; }

		public string LastErrorString => LastErrorDate.HasValue ?
			$"[{LastErrorDate.Value.Add(TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now))}]: {LastError ?? ""}" :
			"";

		public string MagicUpdaterVersion { get; set; }

		[NotMapRefreshingGridView]
		public string HwId { get; set; }

		[NotMapRefreshingGridView]
		public string LicId { get; set; }

		[NotMapRefreshingGridView]
		public int? LicStatus { get; set; }


		[NotMapRefreshingGridView]

		public Bitmap LicStatusBitmap
		{
			get
			{
				if ((LicStatus ?? -1) == 0)
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
		public string AddressToConnect { get; set; }

		[NotMapRefreshingGridView]
		public string Phone { get; set; }

		[NotMapRefreshingGridView]
		public bool IsExchangeError { get; set; }		
	}
}
