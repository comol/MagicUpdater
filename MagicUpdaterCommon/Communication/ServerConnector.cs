using Communications.AsyncCore;
using Communications.Common;
using Communications.SyncCore;
using MagicUpdaterCommon.Common;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace MagicUpdaterCommon.Communication
{
	public abstract class ServerConnector
	{
		private AsyncServer _pipesAsyncServer;
		private bool _isManualDispose = false;
		private bool _isMessageRecieved = false;
		private SyncServer _syncServer;
		private Process _settingsApplicationProcess = null;
		private string _pipeName;
		private bool _isDisposed = false;

		public void CreateAsyncServer(string pipeName)
		{
			_pipeName = pipeName;
			StartSettingsApplication();
			CreateAsyncServerBase();
			NLogger.LogDebugToHdd($"Server: Async server created. Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
		}

		private void CreateAsyncServerBase()
		{
			if (_pipesAsyncServer != null)
			{
				return;
			}

			_pipesAsyncServer = new AsyncServer(_pipeName);
			_pipesAsyncServer.Connected += _pipesAsyncServer_Connected;
			_pipesAsyncServer.MessagSended += _pipesAsyncServer_MessagSended;
			_pipesAsyncServer.MessageRecieved += _pipesAsyncServer_MessageRecieved;
			_pipesAsyncServer.Disposed += _pipesAsyncServer_Disposed;
			_pipesAsyncServer.WaitForConnectionAsync();
		}

		private void _pipesAsyncServer_MessagSended(object sender, CommunicationObject communicationObject = null)
		{
			NLogger.LogDebugToHdd($"Server: Message sended: \"{communicationObject.ActionType.ToString()}\". Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
		}

		private void _pipesAsyncServer_Disposed(object sender, Communications.Common.CommunicationObject communicationObject = null)
		{
			if (!_isManualDispose)
			{
				NLogger.LogDebugToHdd($"Server: Disposed. Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
				CreateAsyncServerBase();
			}
			else
			{
				NLogger.LogDebugToHdd($"Server: Manual dispose. Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
			}
		}

		private void _pipesAsyncServer_MessageRecieved(object sender, CommunicationObject communicationObject = null)
		{
			if (communicationObject.ActionType == CommunicationActionType.MessageRecieved)
			{
				NLogger.LogDebugToHdd($"Server: Answer received. Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
				_isMessageRecieved = true;
			}
			else
			{
				if (communicationObject.Tag != null && (bool)(communicationObject.Tag))
				{
					NLogger.LogDebugToHdd($"Server: Message recieved with sending answer: \"{communicationObject.ActionType.ToString()}\". Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
					SendAsyncMessage(new CommunicationObject
					{
						ActionType = CommunicationActionType.MessageRecieved
					});
					NLogger.LogDebugToHdd($"Server: Answer sended. Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
				}
				else
				{
					NLogger.LogDebugToHdd($"Server: Message recieved: \"{communicationObject.ActionType.ToString()}\". Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
				}

				var res = AsyncPipesAction(communicationObject);
				if (!res.IsComplete)
				{
					NLogger.LogErrorToBaseAndHdd(MainSettings.MainSqlSettings.ComputerId, res.Message);
				}
			}
		}

		private void _pipesAsyncServer_Connected(object sender, Communications.Common.CommunicationObject communicationObject = null)
		{
			NLogger.LogDebugToHdd($"Server: Client connected. Pipe name: \"{_pipeName}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
		}

		public void SendAsyncMessage(CommunicationObject communicationObject)
		{
			//var res = StartSettingsApplication();
			//if (!res.IsComplete)
			//{
			//	NLogger.LogErrorToHdd(res.Message, MainSettings.Constants.MAGIC_UPDATER);
			//}
			CreateAsyncServerBase();
			_pipesAsyncServer.SendMessage(communicationObject);
		}

		/// <summary>
		/// Асинхронная отправка сообщения и ожидание подтверждения
		/// </summary>
		/// <param name="communicationObject"></param>
		/// <param name="timeout">Таймаут ожидания в миллисекундах</param>
		/// <returns></returns>
		public TrySendMessageAndWaitForResponse SendAsyncMessageAndWaitForResponse(CommunicationObject communicationObject, int timeout = 60000)
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
					return new TrySendMessageAndWaitForResponse(false, $"Сообщение не получено. Выход по таймауту.");
				}
			}

			_isMessageRecieved = false;
			return new TrySendMessageAndWaitForResponse();
		}

		/// <summary>
		/// После выполнения этого метода сервер не пересоздается
		/// </summary>
		public void DisposeAsyncServer()
		{
			if(_isDisposed)
			{
				return;
			}
			_isDisposed = true;
			_isManualDispose = true;
			_pipesAsyncServer?.Dispose();
			_pipesAsyncServer = null;
			_isManualDispose = false;
			GC.Collect();
		}

		public void SendSyncMessage(CommunicationObject communicationObject)
		{
			using (_syncServer = new SyncServer())
			{
				_syncServer.WaitForConnection();
				_syncServer.SendMessage(communicationObject);
			}
		}

		public CommunicationObject RecieveSyncMessage()
		{
			CommunicationObject result;
			using (_syncServer = new SyncServer())
			{
				_syncServer.WaitForConnection();
				result = _syncServer.WaitForRecieveMessage();
			}

			return result;
		}

		private TryToStartSettingsApplication StartSettingsApplication()
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

		private TryAsyncPipesAction AsyncPipesAction(CommunicationObject communicationObject)
		{
			try
			{
				NLogger.LogDebugToHdd($"Server: Start action excecute...:\"{communicationObject.ActionType.ToString()}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
				ServerAsyncPipesActionExecute(communicationObject);
				NLogger.LogDebugToHdd($"Server: End action execute: \"{communicationObject.ActionType.ToString()}\"", MainSettings.Constants.PIPES_LOGGER_NAME);
				return new TryAsyncPipesAction();
			}
			catch (Exception ex)
			{
				return new TryAsyncPipesAction(false, ex.Message.ToString());
			}
		}

		protected abstract void ServerAsyncPipesActionExecute(CommunicationObject communicationObject);
	}
}
