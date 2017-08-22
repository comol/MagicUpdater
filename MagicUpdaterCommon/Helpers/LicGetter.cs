using MagicUpdaterCommon.Common;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace MagicUpdaterCommon.Helpers
{
	public class TryGetLicFromWeb : TryResult
	{
		public LicResponce Result { get; private set; }
		public Exception @Exception { get; private set; }
		public TryGetLicFromWeb(bool isComplete = true, string message = "", LicResponce result = null, Exception exception = null) : base(isComplete, message)
		{
			Result = result;
			@Exception = exception;
		}
	}

	public static class LicGetter
	{
		public static TryGetLicFromWeb GetLicFromWeb(string link)
		{
			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);

				request.Method = "GET";
				request.Accept = "application/json";

				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());
				StringBuilder output = new StringBuilder();
				output.Append(reader.ReadToEnd());

				response.Close();

				var licResponce = NewtonJson.ReadJsonString<LicResponce>(output.ToString());

				return new TryGetLicFromWeb(true, "", licResponce);
			}
			catch (Exception ex)
			{
				return new TryGetLicFromWeb(false, ex.Message, null, ex);
			}
		}
	}
}
