using MagicUpdater.ServiceTools;
using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using System;

namespace MagicUpdater.Core
{
	public static class PerformanceReporter
	{
		private static WorkloadAnalyzer _workloadAnalyzer;

		public static void StartOnlyAvg()
		{
			if (_workloadAnalyzer != null)
			{
				return;
			}

			try
			{
				_workloadAnalyzer = new WorkloadAnalyzer();
				_workloadAnalyzer.OnWorkloadAllSave += _workloadAnalyzer_OnWorkloadAvgSave;
				_workloadAnalyzer.Start();
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToBaseAndHdd(MainSettings.MainSqlSettings.ComputerId, ex.ToString(), MainSettings.Constants.MAGIC_UPDATER);
			}
		}

		private static void _workloadAnalyzer_OnWorkloadAvgSave(WorkloadInfo workloadInfo)
		{
			SqlWorks.ExecProc("AddAvgPerformanceCounter"
				, MainSettings.MainSqlSettings.ComputerId
				, DateTime.UtcNow
				, workloadInfo.AvgWorkloadInfoCpu
				, workloadInfo.AvgWorkloadInfoRam
				, workloadInfo.AvgWorkloadInfoDisk);
		}

		public static void StartAll()
		{
			if (_workloadAnalyzer != null)
			{
				return;
			}

			try
			{
				_workloadAnalyzer = new WorkloadAnalyzer();
				_workloadAnalyzer.OnWorkloadAllSave += _workloadAnalyzer_OnWorkloadAllSave;
				_workloadAnalyzer.Start();
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToBaseAndHdd(MainSettings.MainSqlSettings.ComputerId, ex.ToString(), MainSettings.Constants.MAGIC_UPDATER);
			}
		}

		private static void _workloadAnalyzer_OnWorkloadAllSave(WorkloadInfo workloadInfo)
		{
			SqlWorks.ExecProc("AddAvgPerformanceCounter"
				, MainSettings.MainSqlSettings.ComputerId
				, DateTime.UtcNow
				, workloadInfo.AvgWorkloadInfoCpu
				, workloadInfo.AvgWorkloadInfoRam
				, workloadInfo.AvgWorkloadInfoDisk);

			//ProcessorTime
			foreach (var item in workloadInfo.WorkloadInfoCpuList)
			{
				SqlWorks.ExecProc("AddPerformanceCounter"
						, MainSettings.MainSqlSettings.ComputerId
						, item.Key
						, 1
						, item.Value); 
			}

			//RamAvailableMBytes
			foreach (var item in workloadInfo.WorkloadInfoRamList)
			{
				SqlWorks.ExecProc("AddPerformanceCounter"
						, MainSettings.MainSqlSettings.ComputerId
						, item.Key
						, 2
						, item.Value);
			}

			//CurrentDiskQueueLength
			foreach (var item in workloadInfo.WorkloadInfoDiskList)
			{
				SqlWorks.ExecProc("AddPerformanceCounter"
						, MainSettings.MainSqlSettings.ComputerId
						, item.Key
						, 3
						, item.Value);
			}
		}

		public static void Stop()
		{
			_workloadAnalyzer?.Stop();
			_workloadAnalyzer = null;
		}
	}
}
