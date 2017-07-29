using MagicUpdaterSheduler.Main;
using System;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using System.Timers;

namespace MagicUpdaterSheduler
{
	public enum ServiceState
	{
		SERVICE_STOPPED = 0x00000001,
		SERVICE_START_PENDING = 0x00000002,
		SERVICE_STOP_PENDING = 0x00000003,
		SERVICE_RUNNING = 0x00000004,
		SERVICE_CONTINUE_PENDING = 0x00000005,
		SERVICE_PAUSE_PENDING = 0x00000006,
		SERVICE_PAUSED = 0x00000007,
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct ServiceStatus
	{
		public long dwServiceType;
		public ServiceState dwCurrentState;
		public long dwControlsAccepted;
		public long dwWin32ExitCode;
		public long dwServiceSpecificExitCode;
		public long dwCheckPoint;
		public long dwWaitHint;
	};

	public partial class Service1 : ServiceBase
	{
		private System.Threading.Timer _agenOperationTimer;
		private System.Threading.Timer _pluginOperationTimer;
		private const int AGENT_OPERATION_TIMER_INTERVAL = 60000;
		private const int PLUGIN_OPERATION_TIMER_INTERVAL = 60000;

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

		private readonly Thread workerThread;
		public Service1()
		{
			InitializeComponent();

#if !DEBUG
			//eventLog1 = new System.Diagnostics.EventLog();
			//if (!System.Diagnostics.EventLog.SourceExists("MySource"))
			//{
			//	System.Diagnostics.EventLog.CreateEventSource(
			//		"MySource", "MyNewLog");
			//}
			//eventLog1.Source = "MySource";
			//eventLog1.Log = "MyNewLog";

			
#endif
#if DEBUG
			workerThread = new Thread(DoWork);
			workerThread.SetApartmentState(ApartmentState.STA);
#endif
		}

		public void Start()
		{

#if DEBUG
			workerThread.Start();
#endif
		}
		private void DoWork()
		{
#if DEBUG
			var tasks = ShedulerTaskObj.GetShedulerTasks();
			foreach (var task in tasks)
			{
				if (task.IsTimeToRun)
					task.Run();
			}

			var pluginTasks = ShedulerPluginTaskObj.GetShedulerTasks();
			foreach (var task in pluginTasks)
			{
				if (task.IsTimeToRun)
					task.Run();
			}

			Thread.Sleep(60000);
#endif
		}

		protected override void OnStart(string[] args)
		{
#if !DEBUG
			ServiceStatus serviceStatus = new ServiceStatus();
			serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
			serviceStatus.dwWaitHint = 100000;
			SetServiceStatus(this.ServiceHandle, ref serviceStatus);

			serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
			SetServiceStatus(this.ServiceHandle, ref serviceStatus);

			_agenOperationTimer = new System.Threading.Timer(AgenOperationTimerCallback, null, AGENT_OPERATION_TIMER_INTERVAL, System.Threading.Timeout.Infinite);
			_pluginOperationTimer= new System.Threading.Timer(PluginOperationTimerCallback, null, PLUGIN_OPERATION_TIMER_INTERVAL, System.Threading.Timeout.Infinite);

#else
			workerThread.Start();
#endif
		}
		private void AgenOperationTimerCallback(object state)
		{
			var tasks = ShedulerTaskObj.GetShedulerTasks();
			foreach (var task in tasks)
			{
				if (task.IsTimeToRun)
					task.Run();
			}
			_agenOperationTimer.Change(AGENT_OPERATION_TIMER_INTERVAL, System.Threading.Timeout.Infinite);
		}

		private void PluginOperationTimerCallback(object state)
		{
			//TODO
			var tasks = ShedulerPluginTaskObj.GetShedulerTasks();
			foreach (var task in tasks)
			{
				if (task.IsTimeToRun)
					task.Run();
			}
			_pluginOperationTimer.Change(PLUGIN_OPERATION_TIMER_INTERVAL, System.Threading.Timeout.Infinite);
		}

		protected override void OnStop()
		{
#if DEBUG
			workerThread.Abort();
#endif
		}
	}
}
