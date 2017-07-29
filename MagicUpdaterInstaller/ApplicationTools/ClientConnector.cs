using Communications.AsyncCore;
using Communications.Common;
using Communications.SyncCore;
using MagicUpdaterCommon.Common;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;

namespace MagicUpdaterSettings.ServiceTools
{
	#region TryResult
	public class TrySendMessageAndWaitForResponse : TryResult
	{
		public TrySendMessageAndWaitForResponse(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}

	public class TryToStartService : TryResult
	{
		public TryToStartService(bool isComplete = true, string message = "") : base(isComplete, message)
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

	public static class ClientConnector
	{
		private static AsyncClient _asyncClient;
		private static bool _isManualDispose = false;
		private static bool _isMessageRecieved = false;
		private static SyncClient _syncClient;

		public static void AsyncConnect()
		{
			//StartService();
			AsyncConnectBase();
		}

		private static void AsyncConnectBase()
		{
			if (_asyncClient != null)
			{
				return;
			}

			_asyncClient = new AsyncClient();
			_asyncClient.Connected += _asyncClient_Connected;
			_asyncClient.MessageRecieved += _asyncClient_MessageRecieved;
			_asyncClient.Disposed += _asyncClient_Disposed;
			_asyncClient.MessagSended += _asyncClient_MessagSended;
			_asyncClient.ConnectAsync();
		}

		private static void _asyncClient_MessagSended(object sender, CommunicationObject communicationObject = null)
		{
		}

		private static void _asyncClient_Disposed(object sender, Communications.Common.CommunicationObject communicationObject = null)
		{
			if (!_isManualDispose)
			{
				AsyncConnectBase();
			}
		}

		private static void _asyncClient_MessageRecieved(object sender, Communications.Common.CommunicationObject communicationObject = null)
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

		private static void _asyncClient_Connected(object sender, Communications.Common.CommunicationObject communicationObject = null)
		{
			NLogger.LogDebugToHdd("server connected");
		}

		public static void SendAsyncMessage(CommunicationObject communicationObject)
		{
			//var res = StartService();
			//if (!res.IsComplete)
			//{
			//	NLogger.LogErrorToHdd(res.Message, MainSettings.Constants.MAGIC_UPDATER_SETTINGS);
			//}
			AsyncConnectBase();
			_asyncClient.SendMessage(communicationObject);
		}

		/// <summary>
		/// Асинхронная отправка сообщения и ожидание подтверждения
		/// </summary>
		/// <param name="communicationObject"></param>
		/// <param name="timeout">Таймаут ожидания в миллисекундах</param>
		/// <returns></returns>
		public static TrySendMessageAndWaitForResponse SendAsyncMessageAndWaitForResponse(CommunicationObject communicationObject, int timeout = 60000)
		{
			//var res = StartService();
			//if (!res.IsComplete)
			//{
			//	NLogger.LogErrorToHdd(res.Message, MainSettings.Constants.MAGIC_UPDATER_SETTINGS);
			//}

			AsyncConnectBase();
			communicationObject.Tag = true;
			_asyncClient.SendMessage(communicationObject);
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
		/// После выполнения клиент не будет пытаться подключиться
		/// </summary>
		public static void DisposeAsyncClient()
		{
			_isManualDispose = true;
			_asyncClient.Dispose();
			_asyncClient = null;
			_isManualDispose = false;
		}

		public static void SendSyncMessage(CommunicationObject communicationObject)
		{
			using (_syncClient = new SyncClient())
			{
				_syncClient.Connect();
				_syncClient.SendMessage(communicationObject);
			}
		}

		public static CommunicationObject RecieveSyncMessage()
		{
			CommunicationObject result;
			using (_syncClient = new SyncClient())
			{
				_syncClient.Connect();
				result = _syncClient.WaitForRecieveMessage();
			}

			return result;
		}

		private static TryToStartService StartService()
		{
			const int TIMEOUT = 60000;
			TimeSpan timeout = TimeSpan.FromMilliseconds(TIMEOUT);
			ServiceController service = new ServiceController(MainSettings.Constants.SERVICE_NAME);
			try
			{
				if (service == null)
				{
					return new TryToStartService(false, "Служба \"MagicUpdater\" не установлена");
				}

				if (service.Status == ServiceControllerStatus.Stopped)
				{
					service.Start();
					service.WaitForStatus(ServiceControllerStatus.Running, timeout);
					return new TryToStartService();
				}
			}
			catch (Exception ex)
			{
				return new TryToStartService(false, ex.Message.ToString());
			}

			return new TryToStartService();
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
						MessageBox.Show(communicationObject.Data.ToString());
						break;
					case CommunicationActionType.ShowProgressForm:
						break;
					case CommunicationActionType.StartMagicUpdaterRestartForSettings:
						Tools.StartMagicUpdaterRestartForSettings(Convert.ToInt32(communicationObject.Data));
						break;
				}
				return new TryAsyncPipesAction();
			}
			catch (Exception ex)
			{
				return new TryAsyncPipesAction(false, ex.Message.ToString());
			}
		}
	}
}
