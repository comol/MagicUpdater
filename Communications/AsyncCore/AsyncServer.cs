using Communications.Common;
using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using static Communications.Common.CommunicationDelegates;

namespace Communications.AsyncCore
{
	public class AsyncServer : IDisposable
	{
		private string _pipeName;
		private NamedPipeServerStream _server;
		private StreamReader _reader;
		private StreamWriter _writer;
		private Thread _serverListener;
		private bool _stopWaiting = false;
		private bool IsDisposed = false;

		public event CommunicationEventHandler Connected;
		public event CommunicationEventHandler Disconnected;
		public event CommunicationEventHandler Disposed;
		public event CommunicationEventHandler MessageRecieved;
		public event CommunicationEventHandler MessagSended;

		public bool IsConnected => _server != null ? _server.IsConnected : false;

		public AsyncServer(string pipeName)
		{
			_pipeName = pipeName;
		}

		public void WaitForConnectionAsync()
		{
			_server = new NamedPipeServerStream(_pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
			_server.BeginWaitForConnection(BeginConnectionCallback, _server);
		}

		private void BeginConnectionCallback(IAsyncResult ar)
		{
			if (_stopWaiting)
			{
				_stopWaiting = false;
				return;
			}
			_reader = new StreamReader(_server);
			_writer = new StreamWriter(_server);
			NamedPipeServerStream pipe = (NamedPipeServerStream)ar.AsyncState;

			_serverListener = new Thread(ListenClientMessages);
			_serverListener.Start();

			pipe.EndWaitForConnection(ar);

			Connected?.Invoke(_server);
		}

		public void SendMessage(CommunicationObject communicationObject)
		{
			try
			{
				if (!_server.IsConnected)
				{
					return;
				}

				BinaryFormatter bf = new BinaryFormatter();
				Stream stream = _writer.BaseStream;
				bf.Serialize(stream, communicationObject);

				_writer.Flush();
				MessagSended?.Invoke(_server, communicationObject);
			}
			catch
			{
				this.Dispose();
			}
		}

		private void BeginWriteCallback(IAsyncResult ar)
		{
			NamedPipeServerStream server = ar.AsyncState as NamedPipeServerStream;
			server.EndWrite(ar);
		}

		private void ListenClientMessages()
		{
			try
			{
				while (true)
				{
					BinaryFormatter bf = new BinaryFormatter();
					Stream stream = _reader.BaseStream;
					var res = bf.Deserialize(stream) as CommunicationObject;
					if(res.ActionType == CommunicationActionType.DisposeServer)
					{
						this.Disconnect();
						this.Dispose();
						return;
					}
					MessageRecieved?.Invoke(_server, res);
				}
			}
			catch
			{
				this.Dispose();
			}
		}

		private byte[] ObjectToByteArray(object obj)
		{
			if (obj == null)
				return null;
			BinaryFormatter bf = new BinaryFormatter();
			using (MemoryStream ms = new MemoryStream())
			{
				bf.Serialize(ms, obj);
				return ms.ToArray();
			}
		}

		private void Disconnect()
		{
			if (_server != null && _server.IsConnected)
			{
				_server.Disconnect();
				Disconnected?.Invoke(_server);
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

				_stopWaiting = true;
				Disconnect();
				_serverListener?.Interrupt();
				_serverListener?.Abort();
				_server?.Close();
				_server?.Dispose();
				Disposed?.Invoke(null);
			}
			catch { }
		}
	}
}
