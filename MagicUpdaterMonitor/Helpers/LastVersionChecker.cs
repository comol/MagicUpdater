using MagicUpdater.DL.Helpers;
using System;
using System.Diagnostics;
using System.Timers;

namespace MagicUpdaterMonitor.Helpers
{
	public enum VersionCompareResult
	{
		Before = -1,
		Equal = 0,
		Subsequent = 1,
		BaseVersionNotFound = -2
	}
	public class LastVersionChecker
	{
		private Timer timerCheck;
		private static bool isAutoRefresh = false;
		private static Version version = null;
		public event EventHandler<VersionRefreshEventArgs> VersionRefresh;
		public LastVersionChecker(bool _isAutoRefresh)
		{
			timerCheck = new Timer();
			timerCheck.Interval = 60000;
			timerCheck.Elapsed += TimerCheck_Elapsed;
			IsAutoRefresh = _isAutoRefresh;
		}

		private void RefreshVersion()
		{
			try
			{
				if (!Version.TryParse(FileVersionInfo.GetVersionInfo(MainForm.MonitorCommonGlobalSettings.AgentLastVersionPathForMonitor).FileVersion, out version))
					version = null;
			}
			catch (Exception ex)
			{
				MLogger.Error(ex.ToString());
				version = null;
			}
		}

		public static Version GetLatestVersion()
		{
			if (isAutoRefresh && version != null)
				return version;

			try
			{
				Version.TryParse(FileVersionInfo.GetVersionInfo(MainForm.MonitorCommonGlobalSettings.AgentLastVersionPathForMonitor).FileVersion, out version);
			}
			catch(Exception ex)
			{
				MLogger.Error(ex.ToString());
				version = null;
			}
			return version;
		}

		public static VersionCompareResult CompareVersions(string ver)
		{
			Version vver;
			if (Version.TryParse(ver, out vver) && version != null)
			{
				return (VersionCompareResult)vver.CompareTo(version);
			}
			else
				return (VersionCompareResult)(-2);
		}

		private void TimerCheck_Elapsed(object sender, ElapsedEventArgs e)
		{
			RefreshVersion();
			VersionRefresh?.Invoke(sender, new VersionRefreshEventArgs { ver = version });
		}

		public bool IsAutoRefresh
		{
			get { return isAutoRefresh; }
			set
			{
				isAutoRefresh = value;
				timerCheck.Enabled = isAutoRefresh;
			}
		}
	}
	public class VersionRefreshEventArgs : EventArgs
	{
		public Version ver { get; set; }
	}
}
