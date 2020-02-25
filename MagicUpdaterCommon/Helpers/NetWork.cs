using MagicUpdaterCommon.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Text;

namespace MagicUpdaterCommon.Helpers
{
	public static class NetWork
	{
		private static DataSet netWorkDS;
		public static DataSet NetWorkTasks => netWorkDS;
		private static readonly string _wikiUrl = "https://ru.wikipedia.org/";
		static NetWork()
		{
			netWorkDS = new DataSet();
			DataTable dt = new DataTable();
			dt.Columns.Add("ID", typeof(Int32));
			dt.Columns.Add("Attributes", typeof(string));
			dt.Columns.Add("OperationType", typeof(byte));
			dt.Columns.Add("IsRead", typeof(bool));
			netWorkDS.Tables.Add(dt);
		}

		[DllImport("Netapi32", CharSet = CharSet.Auto,
		SetLastError = true),
		SuppressUnmanagedCodeSecurityAttribute]

		// The NetServerEnum API function lists all servers of the 
		// specified type that are visible in a domain.
		private static extern int NetServerEnum(
			string ServerNane, // must be null
			int dwLevel,
			ref IntPtr pBuf,
			int dwPrefMaxLen,
			out int dwEntriesRead,
			out int dwTotalEntries,
			int dwServerType,
			string domain, // null for login domain
			out int dwResumeHandle
			);

		//declare the Netapi32 : NetApiBufferFree method import
		[DllImport("Netapi32", SetLastError = true),
		SuppressUnmanagedCodeSecurityAttribute]

		// Netapi32.dll : The NetApiBufferFree function frees 
		// the memory that the NetApiBufferAllocate function allocates.         
		private static extern int NetApiBufferFree(
			IntPtr pBuf);

		//create a _SERVER_INFO_100 STRUCTURE
		[StructLayout(LayoutKind.Sequential)]
		private struct _SERVER_INFO_100
		{
			internal int sv100_platform_id;
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string sv100_name;
		}

		public static List<string> GetNetworkComputerNames()
		{
			const string ip = "192.168.0.";
			var list = new List<string>();
			for (int i = 1; i <= 20; i++)
			{
				Ping ping = new Ping();
				PingReply pr = ping.Send($"{ip}{i}", 10);

				if (pr.Status == IPStatus.Success)
				{
					try
					{
						IPHostEntry hostEntry = Dns.GetHostEntry($"{ip}{i}");
						if (Environment.MachineName.Trim().ToUpper() != hostEntry.HostName.Trim().ToUpper())
						{
							list.Add(hostEntry.HostName);
						}
					}
					catch
					{
						//list.Add($"{ip}{i}");
					}

				}
			}

			return list;
		}

		public static List<string> GetNetworkComputerNames4()
		{
			List<string> networkComputerNames = new List<string>();
			const int MAX_PREFERRED_LENGTH = -1;
			int SV_TYPE_WORKSTATION = 1;
			int SV_TYPE_SERVER = 2;
			IntPtr buffer = IntPtr.Zero;
			IntPtr tmpBuffer = IntPtr.Zero;
			int entriesRead = 0;
			int totalEntries = 0;
			int resHandle = 0;
			int sizeofINFO = Marshal.SizeOf(typeof(_SERVER_INFO_100));

			try
			{
				int ret = NetServerEnum(null, 100, ref buffer,
					MAX_PREFERRED_LENGTH,
					out entriesRead,
					out totalEntries, SV_TYPE_WORKSTATION |
					SV_TYPE_SERVER, null, out
					resHandle);
				//if the returned with a NERR_Success 
				//(C++ term), =0 for C#
				if (ret == 0)
				{
					//loop through all SV_TYPE_WORKSTATION and SV_TYPE_SERVER PC's
					for (int i = 0; i < totalEntries; i++)
					{
						tmpBuffer = new IntPtr((int)buffer +
								   (i * sizeofINFO));

						//Have now got a pointer to the list of SV_TYPE_WORKSTATION and SV_TYPE_SERVER PC's
						_SERVER_INFO_100 svrInfo = (_SERVER_INFO_100)
							Marshal.PtrToStructure(tmpBuffer,
									typeof(_SERVER_INFO_100));

						//add the Computer name to the List
						networkComputerNames.Add(svrInfo.sv100_name);
					}
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				//The NetApiBufferFree function frees the allocated memory
				NetApiBufferFree(buffer);
			}

			return networkComputerNames;
		}

		public static string GetExternalIpAddress()
		{
			string ip = "";
			try
			{
				using (var client = new WebClient())
				{
					var uri = new Uri("http://yandex.ru/internet/");

					client.Encoding = Encoding.UTF8;
					client.Proxy = null;

					var downloadString = client.DownloadString(uri);
					var first = downloadString.IndexOf("<strong>IP-адрес</strong>: ", StringComparison.Ordinal) + 27;
					var last = downloadString.IndexOf("<strong>Регион по IP-адресу</strong>", StringComparison.Ordinal);

					downloadString = downloadString.Substring(first, last - first);

					downloadString = downloadString.Remove(0, downloadString.IndexOf("info__value info__value_type_ipv4"));
					downloadString = downloadString.Remove(downloadString.IndexOf("<strong>IPv6-адрес</strong>") - 1, downloadString.Length - downloadString.IndexOf("<strong>IPv6-адрес</strong>") - 1);
					downloadString = downloadString.Replace("</span>", "");
					string[] downloadStringSp = downloadString.Split('>');
					ip = downloadStringSp[1];
				}
			}
			catch (Exception msg)
			{

			}
			return ip;
			//string serviceURL = "http://2ip.ru/";
			//string result = "";
			//string serviceTag = "BIG";
			//try
			//{

			//	WebClient wc = new WebClient();

			//	string requestResult = Encoding.UTF8.GetString(wc.DownloadData(serviceURL));

			//	if (!string.IsNullOrEmpty(requestResult))

			//	{
			//		string[] split1 = requestResult.ToUpper().Split(new string[] { serviceTag }, new StringSplitOptions());
			//		string[] split2 = split1[1].Split('<', '>');
			//		result = split2[1];
			//	}
			//}
			//catch (Exception ex)
			//{
			//	Log.LogErrorToBaseOrHdd(ApplicationSettings.JsonSettings.ComputerId, ex.Message.ToString());
			//}
			//return result;
		}

		public static string GetLocalIPAddress()
		{
			var host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (var ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					return ip.ToString();
				}
			}
			throw new Exception("Local IP Address Not Found!");
		}

		

		public static void OpenLinkInBrowser(string link)
		{
			System.Diagnostics.Process.Start(link);
		}

		public static void OpenViki()
		{
			OpenLinkInBrowser(_wikiUrl);
		}

		

		public class TryCheckAccess : TryResult
		{
			public TryCheckAccess(bool isComplete = true, string message = "") : base(isComplete, message)
			{
			}
		}

		[DllImport("advapi32.DLL", SetLastError = true)]
		public static extern int LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

		public static TryCheckAccess CheckAccess(string remotePath, string login, string domain, string password, string machineName)
		{
			IntPtr admin_token = default(IntPtr);
			WindowsIdentity wid_current = WindowsIdentity.GetCurrent();
			WindowsIdentity wid_admin = null;
			WindowsImpersonationContext wic = null;
			try
			{
				if (LogonUser(login, domain, password, 9, 0, ref admin_token) != 0)
				{
					wid_admin = new WindowsIdentity(admin_token);
					wic = wid_admin.Impersonate();
					System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(remotePath);
					return new TryCheckAccess(true, $"Проверка доступа выполнена. {machineName} {password}");
				}
				else
				{
					return new TryCheckAccess(false, $"Ошибка доступа в {remotePath.Replace("Windows", "")}. {machineName} {password}");
				}
			}
			catch (Exception ex)
			{
				int ret = Marshal.GetLastWin32Error();
				return new TryCheckAccess(false, $"Ошибка доступа в {remotePath.Replace("Windows", "")}. {machineName} {password}" +
												 $"{Environment.NewLine} Exception: {ex.Message.ToString()}");
			}
			finally
			{
				if (wic != null)
				{
					wic.Undo();
				}
			}
		}

		public static List<string> GetNetworkComputerNamesScanShop()
		{
			const string ip = "192.168.0.";
			var list = new List<string>();
			for (int i = 2; i <= 20; i++)
			{
				Ping ping = new Ping();
				PingReply pr = ping.Send($"{ip}{i}", 10);

				if (pr.Status == IPStatus.Success)
				{
					try
					{
						IPHostEntry hostEntry = Dns.GetHostEntry($"{ip}{i}");
						if (Environment.MachineName.Trim().ToUpper() != hostEntry.HostName.Trim().ToUpper())
						{
							list.Add(hostEntry.HostName);
						}
					}
					catch
					{
						//list.Add($"{ip}{i}");
					}

				}
			}

			return list;
		}
	}
}
