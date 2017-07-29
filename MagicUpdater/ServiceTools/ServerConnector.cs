using Communications.AsyncCore;
using Communications.Common;
using MagicUpdaterCommon.Common;
using System.Threading;
using Communications.SyncCore;
using System.Diagnostics;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterCommon.Helpers;
using System;

namespace MagicUpdater.ApplicationTools
{
	#region TryResult
	public class TrySendMessageAndWaitForResponse : TryResult
	{
		public TrySendMessageAndWaitForResponse(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}

	public class TryToStartSettingsApplication : TryResult
	{
		public TryToStartSettingsApplication(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}

	public class TryAsyncPipesAction : TryResult
	{
		public TryAsyncPipesAction(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}
	#endregion TryResult

	public static class ServerConnector
	{
		private static AsyncServer _pipesAsyncServer;
		private static bool _isManualDispose = false;
		private static bool _isMessageRecieved = false;
		private static SyncServer _syncServer;
		private static Process _settingsApplicationProcess = null;

		public static void CreateAsyncServer()
		{
			StartSettingsApplication();
			CreateAsyncServerBase();
		}

		private static void CreateAsyncServerBase()
		{
			if (_pipesAsyncServer != null)
			{
				return;
			}

			_pipesAsyncServer = new AsyncServer();
			_pipesAsyncServer.Connected += _pipesAsyncServer_Connected;
			_pipesAsyncServer.MessageRecieved += _pipesAsyncServer_MessageRecieved;
			_pipesAsyncServer.Disposed += _pipesAsyncServer_Disposed;
			_pipesAsyncServer.WaitForConnectionAsync();
		}

		private static void _pipesAsyncServer_Disposed(object sender, Communications.Common.CommunicationObject communicationObject = null)
		{
			if (!_isManualDispose)
			{
				CreateAsyncServerBase();
			}
		}

		private static void _pipesAsyncServer_MessageRecieved(object sender, CommunicationObject communicationObject = null)
		{
			if (communicationObject.ActionType == CommunicationActionType.MessageRecieved)
			{
				_isMessageRecieved = true;
			}
			else
			{
				if (communicationObject.Tag != null && (bool)(communicationObject.Tag))
				{
					SendAsyncMessage(new CommunicationObject
					{
						ActionType = CommunicationActionType.MessageRecieved
					});
				}
				AsyncPipesAction(communicationObject);
			}
		}

		private static void _pipesAsyncServer_Connected(object sender, Communications.Common.CommunicationObject communicationObject = null)
		{
			NLogger.LogDebugToHdd("client connected");
		}

		public static void SendAsyncMessage(CommunicationObject communicationObject)
		{
			var res = StartSettingsApplication();
			if (!res.IsComplete)
			{
				NLogger.LogErrorToHdd(res.Message, MainSettings.Constants.MAGIC_UPDATER);
			}
			CreateAsyncServerBase();
			_pipesAsyncServer.SendMessage(communicationObject);
		}

		/// <summary>
		/// Асинхронная отправка сообщения и ожидание подтверждения
		/// </summary>
		/// <param name="communicationObject"></param>
		/// <param name="timeout">Таймаут ожидания в миллисекундах</param>
		/// <returns></returns>
		public static TrySendMessageAndWaitForResponse SendAsyncMessageAndWaitForResponse(CommunicationObject communicationObject, int timeout = 60000)
		{
			var res = StartSettingsApplication();
			if (!res.IsComplete)
			{
				NLogger.LogErrorToHdd(res.Message, MainSettings.Constants.MAGIC_UPDATER);
				return new TrySendMessageAndWaitForResponse(false, res.Message);
			}
			CreateAsyncServerBase();
			communicationObject.Tag = true;
			_pipesAsyncServer.SendMessage(communicationObject);
			while (!_isMessageRecieved)
			{
				Thread.Sleep(1);
				--timeout;
				if (timeout <= 0)
				{
					return new TrySendMessageAndWaitForResponse(false, $"Сообщение не получено. Выход по таймауту ({timeout}).");
				}
			}

			_isMessageRecieved = false;
			return new TrySendMessageAndWaitForResponse();
		}

		/// <summary>
		/// После выполнения этого метода сервер не пересоздается
		/// </summary>
		public static void DisposeAsyncServer()
		{
			_isManualDispose = true;
			_pipesAsyncServer.Dispose();
			_pipesAsyncServer = null;
			_isManualDispose = false;
		}

		public static void SendSyncMessage(CommunicationObject communicationObject)
		{
			using (_syncServer = new SyncServer())
			{
				_syncServer.WaitForConnection();
				_syncServer.SendMessage(communicationObject);
			}
		}

		public static CommunicationObject RecieveSyncMessage()
		{
			CommunicationObject result;
			using (_syncServer = new SyncServer())
			{
				_syncServer.WaitForConnection();
				result = _syncServer.WaitForRecieveMessage();
			}

			return result;
		}

		private static TryToStartSettingsApplication StartSettingsApplication()
		{
			return new TryToStartSettingsApplication();
			//if (_settingsApplicationProcess != null && !_settingsApplicationProcess.HasExited)
			//{
			//	if (_settingsApplicationProcess.Responding)
			//	{
			//		return new TryToStartSettingsApplication();
			//	}
			//	else
			//	{
			//		_settingsApplicationProcess.Kill();
			//	}
			//}

			//Process[] processes = Process.GetProcessesByName(MainSettings.Constants.MAGIC_UPDATER_SETTINGS);
			//foreach(var proc in processes)
			//{
			//	proc.Kill();
			//}

			//_settingsApplicationProcess = Process.Start(MainSettings.Constants.PathToSettingsApplication);
			//if (_settingsApplicationProcess != null && !_settingsApplicationProcess.HasExited && _settingsApplicationProcess.Responding)
			//{
			//	return new TryToStartSettingsApplication();
			//}
			//else
			//{
			//	return new TryToStartSettingsApplication(false, "Не удалось запустить приложение \"MagicUpdaterSettings\"");
			//}
		}

		private static TryAsyncPipesAction AsyncPipesAction(CommunicationObject communicationObject)
		{
			try
			{
				switch (communicationObject.ActionType)
				{
					case CommunicationActionType.Reinitialization:
						break;
					case CommunicationActionType.Restart:
						break;
					case CommunicationActionType.ShowConfirmationMessage:
						break;
					case CommunicationActionType.ShowMessage:
						NLogger.LogDebugToHdd(communicationObject.Data.ToString());
						break;
					case CommunicationActionType.ShowProgressForm:
						break;
				}
				return new TryAsyncPipesAction();
			}
			catch(Exception ex)
			{
				return new TryAsyncPipesAction(false, ex.Message.ToString());
			}
		}
	}
}
