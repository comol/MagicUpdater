using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicUpdaterCommon.Helpers
{
	public enum LicResponceStatus
	{
		LicIdOk = 0,
		LicDeletingOk = 1,
		LicLimitOver = 2,
		ErrorAuth = -1,
		ErrorLicDeleting = -2,
		ErrorLicIdCreation = -3,
		ErrorLicParameters = -4,
		ErrorHwId = -5,
		ErrorPcCount = -6,
		ErrorLicType = -7
	}
	public class LicResponce
	{
		public LicResponceStatus Status { get; set; }
		public string StatusString => Status.ToString();
		public string Val { get; set; } = null;
		public int PcCount { get; set; } = -1;
	}
}
