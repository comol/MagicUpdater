using System.ComponentModel;
using System.Security.Cryptography;
using System.IO;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.Abstract;

namespace MagicUpdater.Operations
{
	class CheckFileExistsViaMd5 : Operation
	{
		private const string FILE_MD5 = "25A2E9DFD2228D8630012AB649AEEFA8";

		private const string FILE_PATH = @"C:\Distrib\AdminInstall1C.zip";

		public CheckFileExistsViaMd5(int? operationId) : base(operationId)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			using (var md5 = MD5.Create())
			{
				using (var stream = File.OpenRead(FILE_PATH))
				{
					if (md5.ComputeHash(stream).ToHex(true) == FILE_MD5)
					{
						AddCompleteMessage("Архив платформы присутствует");
					}
					else
					{
						AddErrorMessage("Архив платформы отсутствует");
					}
				}
			}
		}
	}
}
