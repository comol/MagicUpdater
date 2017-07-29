using Communications.AsyncCore;
using Communications.Common;
using Communications.SyncCore;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.ServiceProcess;
using System.Threading;

namespace MagicUpdaterCommon.Communication
{
	public abstract class ClientConnector
	{
		private AsyncClient _asyncClient;
		private bool _isManualDispose = false;
		private bool _isMessageRecieved = false;
		private SyncClient _syncClient;
		private string _pipeName;

		public void AsyncConnect(string pipeName)
		{
			_pipeName = pipeName;
			//StartService();
			AsyncConnectBase();
			NLogger.LogDebugToHdd($"Client: Async client created. Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
		}

		private void AsyncConnectBase()
		{
			if (_asyncClient != null)
			{
				return;
			}

			_asyncClient = new AsyncClient(_pipeName);
			_asyncClient.Connected += _asyncClient_Connected;
			_asyncClient.MessageRecieved += _asyncClient_MessageRecieved;
			_asyncClient.Disposed += _asyncClient_Disposed;
			_asyncClient.MessagSended += _asyncClient_MessagSended;
			_asyncClient.ConnectAsync();
		}

		private void _asyncClient_MessagSended(object sender, CommunicationObject communicationObject = null)
		{
			NLogger.LogDebugToHdd($"Client: Message sended: \"{communicationObject.ActionType.ToString()}\". Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
		}

		private void _asyncClient_Disposed(object sender, CommunicationObject communicationObject = null)
		{
			if (!_isManualDispose)
			{
				NLogger.LogDebugToHdd($"Client: Disposed. Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
				AsyncConnectBase();
			}
			else
			{
				NLogger.LogDebugToHdd($"Client: Manual dispose. Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
			}
		}

		private void _asyncClient_MessageRecieved(object sender, CommunicationObject communicationObject = null)
		{
			if (communicationObject.ActionType == CommunicationActionType.MessageRecieved)
			{
				NLogger.LogDebugToHdd($"Client: Answer received. Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
				_isMessageRecieved = true;
			}
			else
			{
				if (communicationObject.Tag != null && (bool)(communicationObject.Tag))
				{
					NLogger.LogDebugToHdd($"Client: Message recieved with sending answer: \"{communicationObject.ActionType.ToString()}\". Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
					SendAsyncMessage(new CommunicationObject
					{
						ActionType = CommunicationActionType.MessageRecieved
					});
					NLogger.LogDebugToHdd($"Client: Answer sended. Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
				}
				else
				{
					NLogger.LogDebugToHdd($"Client: Message recieved: \"{communicationObject.ActionType.ToString()}\". Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
				}

				var res = AsyncPipesAction(communicationObject);
				if (!res.IsComplete)
				{
					NLogger.LogErrorToBaseAndHdd(MainSettings.MainSqlSettings.ComputerId, res.Message);
				}
			}
		}

		protected virtual void _asyncClient_Connected(object sender, Communications.Common.CommunicationObject communicationObject = null)
		{
			NLogger.LogDebugToHdd($"Client: Async client connected. Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
		}

		public void SendAsyncMessage(CommunicationObject communicationObject)
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
		public TrySendMessageAndWaitForResponse SendAsyncMessageAndWaitForResponse(CommunicationObject communicationObject, int timeout = 60000)
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
					return new TrySendMessageAndWaitForResponse(false, $"Сообщение не получено. Выход по таймауту.");
				}
			}

			_isMessageRecieved = false;
			return new TrySendMessageAndWaitForResponse();
		}

		/// <summary>
		/// После выполнения клиент не будет пытаться подключиться
		/// </summary>
		public void DisposeAsyncClient()
		{
			_isManualDispose = true;
			_asyncClient.Dispose();
			_asyncClient = null;
			_isManualDispose = false;
		}

		public void SendSyncMessage(CommunicationObject communicationObject)
		{
			using (_syncClient = new SyncClient())
			{
				_syncClient.Connect();
				_syncClient.SendMessage(communicationObject);
			}
		}

		public CommunicationObject RecieveSyncMessage()
		{
			CommunicationObject result;
			using (_syncClient = new SyncClient())
			{
				_syncClient.Connect();
				result = _syncClient.WaitForRecieveMessage();
			}

			return result;
		}

		private TryToStartService StartService()
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

		private TryAsyncPipesAction AsyncPipesAction(CommunicationObject communicationObject)
		{
			try
			{
				NLogger.LogDebugToHdd($"Client: Start action excecute...:\"{communicationObject.ActionType.ToString()}\". Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
				ClientAsyncPipesActionExecute(communicationObject);
				NLogger.LogDebugToHdd($"Client: End action execute: \"{communicationObject.ActionType.ToString()}\". Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
				return new TryAsyncPipesAction();
			}
			catch (Exception ex)
			{
				return new TryAsyncPipesAction(false, ex.Message.ToString());
			}
		}

		public abstract void ClientAsyncPipesActionExecute(CommunicationObject communicationObject);
	}
}
