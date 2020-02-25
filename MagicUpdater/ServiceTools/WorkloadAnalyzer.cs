using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MagicUpdater.ServiceTools
{
	public class WorkloadInfo
	{
		public Dictionary<DateTime, double> WorkloadInfoCpuList { get; private set; }
		public Dictionary<DateTime, double> WorkloadInfoRamList { get; private set; }
		public Dictionary<DateTime, double> WorkloadInfoDiskList { get; private set; }
		public double AvgWorkloadInfoCpu { get; private set; }
		public double AvgWorkloadInfoRam { get; private set; }
		public double AvgWorkloadInfoDisk { get; private set; }

		public double MaxWorkloadInfoCpu { get; private set; }
		public double MaxWorkloadInfoRam { get; private set; }
		public double MaxWorkloadInfoDisk { get; private set; }

		public WorkloadInfo(Dictionary<DateTime, double> workloadInfoCpuList
			, Dictionary<DateTime, double> workloadInfoRamList
			, Dictionary<DateTime, double> workloadInfoDiskList
			, double avgWorkloadInfoCpuList
			, double avgWorkloadInfoRamList
			, double avgWorkloadInfoDiskList
			, double maxWorkloadInfoCpuList
			, double maxWorkloadInfoRamList
			, double maxWorkloadInfoDiskList)
		{
			WorkloadInfoCpuList = workloadInfoCpuList;
			WorkloadInfoRamList = workloadInfoRamList;
			WorkloadInfoDiskList = workloadInfoDiskList;
			AvgWorkloadInfoCpu = avgWorkloadInfoCpuList;
			AvgWorkloadInfoRam = avgWorkloadInfoRamList;
			AvgWorkloadInfoDisk = avgWorkloadInfoDiskList;
			MaxWorkloadInfoCpu = maxWorkloadInfoCpuList;
			MaxWorkloadInfoRam = maxWorkloadInfoRamList;
			MaxWorkloadInfoDisk = maxWorkloadInfoDiskList;
		}
	}

	public delegate void WorkloadCpuSaveDelegate(Dictionary<DateTime, double> workloadInfoCpuList);
	public delegate void WorkloadRamSaveDelegate(Dictionary<DateTime, double> workloadInfoRamList);
	public delegate void WorkloadDiskSaveDelegate(Dictionary<DateTime, double> workloadInfoDiskList);
	public delegate void WorkloadAllSaveDelegate(WorkloadInfo workloadInfo);

	public class WorkloadAnalyzer
	{
		private const int SCAN_INTERVAL = 1000;

		private PerformanceCounter _cpuCounter;
		private PerformanceCounter _ramCounter;
		private PerformanceCounter _diskCounter;
		private System.Threading.Timer _timerScan;
		private System.Threading.Timer _timerSaveToDb;
		private bool _isTimerScanRunning = false;
		private bool _isTimerSaveToDbRunning = false;
		private bool _stopTimerScanRunning = false;
		private bool _stopTimerSaveToDbRunning = false;

		private bool _isDiagnosticRunning = false;

		private double _cpuMinValue;
		private double _cpuMaxValue;
		private double _ramMinValue;
		private double _ramMaxValue;
		private double _diskMinValue;
		private double _diskMaxValue;

		private double _cpuDelta;
		private double _ramDelta;
		private double _diskDelta;

		private int _savingToDbIntervalMs;

		private Dictionary<DateTime, double> _workloadInfoCpuList;
		private Dictionary<DateTime, double> _workloadInfoRamList;
		private Dictionary<DateTime, double> _workloadInfoDiskList;

		public event WorkloadAllSaveDelegate OnWorkloadAllSave;

		public WorkloadAnalyzer(double cpuDelta = 30, double ramDelta = 500, double diskDelta = 4, int savingToDbIntervalMs = 300000)
		{
			_cpuDelta = cpuDelta;
			_ramDelta = ramDelta;
			_diskDelta = diskDelta;
			_savingToDbIntervalMs = savingToDbIntervalMs;
		}

		public void Start()
		{
			if (_isDiagnosticRunning)
			{
				return;
			}
			_workloadInfoCpuList = new Dictionary<DateTime, double>();
			_workloadInfoRamList = new Dictionary<DateTime, double>();
			_workloadInfoDiskList = new Dictionary<DateTime, double>();
			_isDiagnosticRunning = true;
			_stopTimerScanRunning = false;
			_stopTimerSaveToDbRunning = false;
			_cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
			_ramCounter = new PerformanceCounter("Memory", "Available MBytes");
			_diskCounter = new PerformanceCounter("PhysicalDisk", "Current Disk Queue Length", "_Total");
			_timerScan = new System.Threading.Timer(TimerAllCallback, null, SCAN_INTERVAL, System.Threading.Timeout.Infinite);
			_timerSaveToDb = new System.Threading.Timer(TimerSaveToDbCallback, null, _savingToDbIntervalMs, System.Threading.Timeout.Infinite);
		}

		private void TimerSaveToDbCallbackThread()
		{
			while (_isTimerScanRunning)
			{
				System.Threading.Thread.Sleep(100);
			}
			TimerSaveToDbCallbackContinuation();
		}

		private void TimerSaveToDbCallback(object state)
		{
			_isTimerSaveToDbRunning = true;

			_stopTimerScanRunning = true;

			System.Threading.Thread thread = new System.Threading.Thread(TimerSaveToDbCallbackThread);
			thread.Start();
		}

		private void TimerSaveToDbCallbackContinuation()
		{
			try
			{
				//TODO: Сюда сохранение в базу
				double avgWorkloadInfoCpuList = _workloadInfoCpuList.Count > 0 ? _workloadInfoCpuList.Sum(s => s.Value) / _workloadInfoCpuList.Count : 0d;
				double avgWorkloadInfoRamList = _workloadInfoRamList.Count > 0 ? _workloadInfoRamList.Sum(s => s.Value) / _workloadInfoRamList.Count : 0d;
				double avgWorkloadInfoDiskList = _workloadInfoDiskList.Count > 0 ? _workloadInfoDiskList.Sum(s => s.Value) / _workloadInfoDiskList.Count : 0d;

				double maxWorkloadInfoCpuList = _workloadInfoCpuList.Max(m => m.Value);
				double maxWorkloadInfoRamList = _workloadInfoRamList.Max(m => m.Value);
				double maxWorkloadInfoDiskList = _workloadInfoDiskList.Max(m => m.Value);

				WorkloadInfo wi = new WorkloadInfo(
					_workloadInfoCpuList
					, _workloadInfoRamList
					, _workloadInfoDiskList
					, avgWorkloadInfoCpuList
					, avgWorkloadInfoRamList
					, avgWorkloadInfoDiskList
					, maxWorkloadInfoCpuList
					, maxWorkloadInfoRamList
					, maxWorkloadInfoDiskList);

				OnWorkloadAllSave?.Invoke(wi);
				_workloadInfoCpuList.Clear();
				_workloadInfoRamList.Clear();
				_workloadInfoDiskList.Clear();
			}
			finally
			{
				if (_stopTimerSaveToDbRunning)
				{
					_timerSaveToDb.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
					_isTimerSaveToDbRunning = false;
				}
				else
				{
					_timerSaveToDb.Change(_savingToDbIntervalMs, System.Threading.Timeout.Infinite);
				}

				_stopTimerScanRunning = false;
				_timerScan.Change(0, System.Threading.Timeout.Infinite);
			}
		}

		private void TimerAllCallback(object state)
		{
			_isTimerScanRunning = true;
			try
			{
				DateTime utc = DateTime.UtcNow;

				double cpu = _cpuCounter.NextValue();
				double ram = _ramCounter.NextValue();
				double disk = _diskCounter.NextValue();
				//cpu
				if (_cpuDelta > 0)
				{
					if (_workloadInfoCpuList.Count == 0)
					{
						_workloadInfoCpuList.Add(utc, cpu);
					}
					else
					{
						_cpuMinValue = _workloadInfoCpuList.Last().Value - (_cpuDelta / 2);
						_cpuMaxValue = _workloadInfoCpuList.Last().Value + (_cpuDelta / 2);

						if (cpu > _cpuMaxValue || cpu < _cpuMinValue)
						{
							_workloadInfoCpuList.Add(utc, cpu);
						}
					}
				}
				else
				{
					_workloadInfoCpuList.Add(utc, cpu);
				}
				//ram
				if (_ramDelta > 0)
				{
					if (_workloadInfoRamList.Count == 0)
					{
						_workloadInfoRamList.Add(utc, ram);
					}
					else
					{
						_ramMinValue = _workloadInfoRamList.Last().Value - (_ramDelta / 2);
						_ramMaxValue = _workloadInfoRamList.Last().Value + (_ramDelta / 2);

						if (ram > _ramMaxValue || ram < _ramMinValue)
						{
							_workloadInfoRamList.Add(utc, ram);
						}
					}
				}
				else
				{
					_workloadInfoRamList.Add(utc, ram);
				}
				//disk
				if (_diskDelta > 0)
				{
					if (_workloadInfoDiskList.Count == 0)
					{
						_workloadInfoDiskList.Add(utc, disk);
					}
					else
					{
						_diskMinValue = _workloadInfoDiskList.Last().Value - (_diskDelta / 2);
						_diskMaxValue = _workloadInfoDiskList.Last().Value + (_diskDelta / 2);

						if (disk > _diskMaxValue || disk < _diskMinValue)
						{
							_workloadInfoDiskList.Add(utc, disk);
						}
					}
				}
				else
				{
					_workloadInfoDiskList.Add(utc, disk);
				}
			}
			finally
			{
				if (_stopTimerScanRunning)
				{
					_timerScan.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
					_isTimerScanRunning = false;
				}
				else
				{
					_timerScan.Change(SCAN_INTERVAL, System.Threading.Timeout.Infinite);
				}
			}
		}

		public void Pause()
		{
			_stopTimerSaveToDbRunning =
			_stopTimerScanRunning = true;
		}

		public void Resume()
		{
			_isDiagnosticRunning = true;
			_stopTimerSaveToDbRunning =
			_stopTimerScanRunning = false;
			_timerScan.Change(SCAN_INTERVAL, System.Threading.Timeout.Infinite);
			_timerSaveToDb.Change(_savingToDbIntervalMs, System.Threading.Timeout.Infinite);
		}

		private void StopThread()
		{
			while (_isTimerScanRunning || _isTimerSaveToDbRunning)
			{
				System.Threading.Thread.Sleep(100);
			}

			StopContinuation();
		}

		public void Stop()
		{
			if (_timerScan == null
				&& _cpuCounter == null
				&& _ramCounter == null
				&& _diskCounter == null)
			{
				return;
			}

			_stopTimerSaveToDbRunning =
			_stopTimerScanRunning = true;

			System.Threading.Thread thread = new System.Threading.Thread(StopThread);
			thread.Start();
		}

		private void StopContinuation()
		{
			_timerScan?.Dispose();
			_timerSaveToDb?.Dispose();
			_cpuCounter?.Dispose();
			_ramCounter?.Dispose();

			_diskCounter?.Dispose();
			_workloadInfoCpuList?.Clear();
			_workloadInfoRamList?.Clear();
			_workloadInfoDiskList?.Clear();

			_workloadInfoCpuList = null;
			_workloadInfoRamList = null;
			_workloadInfoDiskList = null;
			_timerScan = null;
			_timerSaveToDb = null;
			_cpuCounter = null;
			_ramCounter = null;
			_diskCounter = null;

			_isDiagnosticRunning = false;
		}
	}
}
