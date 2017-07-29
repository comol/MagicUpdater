using Communications.Common;
using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using static Communications.Common.CommunicationDelegates;

namespace Communications.SyncCore
{
	public class SyncServer : IDisposable
	{
		private string _pipeName;
		private NamedPipeServerStream _server;
		private StreamReader _reader;
		private StreamWriter _writer;
		private bool IsDisposed = false;

		public bool IsConnected => _server != null ? _server.IsConnected : false;

		public SyncServer(string pipeName = "MuSyncPipe")
		{
			_pipeName = pipeName;
		}

		public void WaitForConnection()
		{
			_server = new NamedPipeServerStream(_pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte);
			_server.WaitForConnection();
			_reader = new StreamReader(_server);
			_writer = new StreamWriter(_server);
		}

		public CommunicationObject WaitForRecieveMessage()
		{
			try
			{
				if (!_server.IsConnected)
				{
					return null;
				}

				BinaryFormatter bf = new BinaryFormatter();
				Stream stream = _reader.BaseStream;
				CommunicationObject res = bf.Deserialize(stream) as CommunicationObject;

				return res;
			}
			catch
			{
				this.Dispose();
			}

			return null;
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
			}
			catch
			{
				this.Dispose();
			}
		}

		private void Disconnect()
		{
			if (_server != null && _server.IsConnected)
			{
				_server.Disconnect();
			}
		}

		public void Dispose()
		{
			if (IsDisposed)
			{
				return;
			}
			IsDisposed = true;

			Disconnect();
			_server?.Close();
			_server?.Dispose();
		}
	}
}
