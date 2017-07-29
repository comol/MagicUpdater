using Communications.Common;
using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Communications.SyncCore
{
	public class SyncClient : IDisposable
	{
		private string _pipeName;
		private NamedPipeClientStream _client;
		private StreamReader _reader;
		private StreamWriter _writer;
		private bool IsDisposed = false;

		public bool IsConnected => _client != null ? _client.IsConnected : false;

		public SyncClient(string pipeName = "MuSyncPipe")
		{
			_pipeName = pipeName;
		}

		public void Connect()
		{
			_client = new NamedPipeClientStream(".", _pipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
			_client.Connect();
			_reader = new StreamReader(_client);
			_writer = new StreamWriter(_client);
		}

		public CommunicationObject WaitForRecieveMessage()
		{
			try
			{
				if (!_client.IsConnected)
				{
					return null;
				}

				BinaryFormatter bf = new BinaryFormatter();
				Stream stream = _reader.BaseStream;
				return bf.Deserialize(stream) as CommunicationObject;
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
				if (!_client.IsConnected)
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

		public void Dispose()
		{
			if (IsDisposed)
			{
				return;
			}
			IsDisposed = true;

			_client?.Close();
			_client?.Dispose();
		}
	}
}
