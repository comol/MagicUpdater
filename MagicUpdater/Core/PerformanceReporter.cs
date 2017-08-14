using MagicUpdater.ServiceTools;
using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.SettingsTools;
using System;

namespace MagicUpdater.Core
{
	public class PerformanceReporter : IDisposable
	{
		private WorkloadAnalyzer _workloadAnalyzer;

		public void StartOnlyAvg()
		{
			_workloadAnalyzer = new WorkloadAnalyzer();
			_workloadAnalyzer.OnWorkloadAllSave += _workloadAnalyzer_OnWorkloadAvgSave;
			_workloadAnalyzer.Start();
		}

		private void _workloadAnalyzer_OnWorkloadAvgSave(WorkloadInfo workloadInfo)
		{
			SqlWorks.ExecProc("AddAvgPerformanceCounter"
				, MainSettings.MainSqlSettings.ComputerId
				, DateTime.UtcNow
				, workloadInfo.AvgWorkloadInfoCpu
				, workloadInfo.AvgWorkloadInfoRam
				, workloadInfo.AvgWorkloadInfoDisk);
		}

		public void StartAll()
		{
			_workloadAnalyzer = new WorkloadAnalyzer();
			_workloadAnalyzer.OnWorkloadAllSave += _workloadAnalyzer_OnWorkloadAllSave;
			_workloadAnalyzer.Start();
		}

		private void _workloadAnalyzer_OnWorkloadAllSave(WorkloadInfo workloadInfo)
		{
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

		public void Dispose()
		{
			_workloadAnalyzer?.Stop();
		}
	}
}
