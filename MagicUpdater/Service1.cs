using MagicUpdater.Core;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;

namespace MagicUpdater
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
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

		private readonly Thread workerThread;

		private MuCore _muCore;

		public Service1()
		{
			InitializeComponent();

#if !DEBUG

#endif
#if DEBUG
			workerThread = new Thread(DoWork);
			workerThread.SetApartmentState(ApartmentState.STA);
#endif
		}

		#region CodeForDebug
		public void Start()
		{

#if DEBUG
			//Точка входа для режима Debug
			//System.Diagnostics.Process settingsApplicationProcess = System.Diagnostics.Process.Start(MainSettings.Constants.PathToSettingsApplication);
			//if (settingsApplicationProcess == null || settingsApplicationProcess.HasExited || !settingsApplicationProcess.Responding)
			//{
			//	NLogger.LogErrorToHdd("Не удалось запустить приложение \"MagicUpdaterSettings\"", MainSettings.Constants.MAGIC_UPDATER);
			//}
			workerThread.Start();
#endif
		}

		private void DoWork()
		{
#if DEBUG
			StartEntryPoint();
#endif
		}
		#endregion CodeForDebug

		protected override void OnStart(string[] args)
		{

#if !DEBUG
			//Точка входа для режима Release
			StartEntryPoint();
#else
			workerThread.Start();
#endif
			NLogger.LogDebugToHdd("Служба запущена", MainSettings.Constants.MAGIC_UPDATER);
		}

		protected override void OnStop()
		{

#if DEBUG
				workerThread.Abort();
#endif
			StopEntryPoint();

			NLogger.LogDebugToHdd("Служба остановлена", MainSettings.Constants.MAGIC_UPDATER);

		}

		private void StartEntryPoint()
		{
			try
			{
				_muCore = new MuCore();
			}
			catch (Exception ex)
			{
				NLogger.LogDebugToHdd(ex.ToString(), MainSettings.Constants.MAGIC_UPDATER);
			}
		}

		private void StopEntryPoint()
		{
			try
			{
				//_muCore?.Dispose();
			}
			catch (Exception ex)
			{
				NLogger.LogDebugToHdd(ex.ToString(), MainSettings.Constants.MAGIC_UPDATER);
			}
		}
	}
}
