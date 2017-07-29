using Communications.Common;
using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using static Communications.Common.CommunicationDelegates;


namespace Communications.AsyncCore
{
	public class AsyncClient : IDisposable
	{
		private string _pipeName;
		private NamedPipeClientStream _client;
		private StreamReader _reader;
		private StreamWriter _writer;
		private Thread _clientListener;
		private Thread _connectThread;
		private bool IsDisposed = false;

		public event CommunicationEventHandler Connected;
		public event CommunicationEventHandler Disposed;
		public event CommunicationEventHandler MessageRecieved;
		public event CommunicationEventHandler MessagSended;

		public bool IsConnected => _client != null ? _client.IsConnected : false;

		public AsyncClient(string pipeName)
		{
			_pipeName = pipeName;
		}

		public void ConnectAsync()
		{
			_client = new NamedPipeClientStream(".", _pipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
			_connectThread = new Thread(Connect);
			_connectThread.Start();
		}

		private void Connect()
		{
			if (_client.IsConnected)
			{
				return;
			}

			_client.Connect();
			_reader = new StreamReader(_client);
			_writer = new StreamWriter(_client);
			_clientListener = new Thread(ListenServerMessages);
			_clientListener.Start();
			Connected?.Invoke(_client);
		}

		private void ListenServerMessages()
		{
			try
			{
				while (true)
				{
					BinaryFormatter bf = new BinaryFormatter();
					Stream stream = _reader.BaseStream;
					var res = bf.Deserialize(stream) as CommunicationObject;
					MessageRecieved?.Invoke(_client, res);
				}
			}
			catch
			{
				this.Dispose();
			}
		}

		public void SendMessage(CommunicationObject communicationObject)
		{
			try
			{
				if (!_client.IsConnected)
				{
					return;
				}

				BinaryFormatter bf = new BinaryFormatter();
				Stream stream = _writer.BaseStream;
				bf.Serialize(stream, communicationObject);

				_writer.Flush();
				MessagSended?.Invoke(_client, communicationObject);
			}
			catch
			{
				this.Dispose();
			}
		}

		public void DisconnectFromServer()
		{
			try
			{
				if (!_client.IsConnected)
				{
					return;
				}

				BinaryFormatter bf = new BinaryFormatter();
				Stream stream = _writer.BaseStream;
				bf.Serialize(stream, new CommunicationObject
				{
					ActionType = CommunicationActionType.DisposeServer
				});
				_writer.Flush();
			}
			catch
			{
				this.Dispose();
			}
		}

		public void Dispose()
		{
			try
			{
				if (IsDisposed)
				{
					return;
				}
				IsDisposed = true;

				DisconnectFromServer();
				_clientListener?.Interrupt();
				_connectThread?.Interrupt();
				_clientListener?.Abort();
				_connectThread?.Abort();
				_client?.Close();
				_client?.Dispose();
				Disposed?.Invoke(null);
			}
			catch { }
		}
	}
}
