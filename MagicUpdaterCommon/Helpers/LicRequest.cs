using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MagicUpdaterCommon.Helpers
{
	public static class LicRequest
	{
		private static readonly string _rs8u2938yh = "HellWasMadeInHeaven";
		private static readonly string _kjsdlfuhho459078phg4 = "jkdfkgmoiwrj45poij98wer";
		public static string GetRequest(string HWID, int agentsCount)
		{
			int hwidc = (int)(HWID.Length / 2);
			int hwidcc = HWID.Length - (int)(HWID.Length / 2);
			string rr = HWID.Substring(0, hwidc - 1) + _rs8u2938yh + HWID.Replace(HWID.Substring(0, hwidc - 1), "");
			int hwidccc = (int)(_rs8u2938yh.Length / 2);
			int hwidcccc = _rs8u2938yh.Length - (int)(_rs8u2938yh.Length / 2);
			string rrr = _rs8u2938yh.Substring(0, hwidccc - 1) + rr + _rs8u2938yh.Replace(_rs8u2938yh.Substring(0, hwidccc - 1), "") + agentsCount.ToString();
			MD5 sec = new MD5CryptoServiceProvider();
			byte[] bt = Encoding.ASCII.GetBytes(rrr);
			byte[] hash = sec.ComputeHash(bt);
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
				sb.Append(hash[i].ToString("X2"));
			}
			string rrrr = _rs8u2938yh.Replace(_rs8u2938yh.Substring(0, hwidccc - 1), "") + sb.ToString() + _rs8u2938yh.Substring(0, hwidccc - 1) + agentsCount.ToString();
			byte[] btt = Encoding.ASCII.GetBytes(rrrr);
			byte[] hashh = sec.ComputeHash(btt);
			StringBuilder sbb = new StringBuilder();
			for (int i = 0; i < hashh.Length; i++)
			{
				sbb.Append(hashh[i].ToString("X2"));
			}
			string ss = HWID.Substring(0, hwidc - 1) + _kjsdlfuhho459078phg4 + HWID.Replace(HWID.Substring(0, hwidc - 1), "");
			int ssc = (int)(_kjsdlfuhho459078phg4.Length / 2);
			string sss = _kjsdlfuhho459078phg4.Substring(0, ssc) + ss + _kjsdlfuhho459078phg4.Replace(_kjsdlfuhho459078phg4.Substring(0, ssc), "") + agentsCount.ToString();
			byte[] bts = Encoding.ASCII.GetBytes(sss);
			byte[] hashs = sec.ComputeHash(bts);
			StringBuilder sbs = new StringBuilder();
			for (int i = 0; i < hashs.Length; i++)
			{
				sbs.Append(hashs[i].ToString("X2"));
			}

			return sbb.ToString() + sbs.ToString();
		}
	}
}
