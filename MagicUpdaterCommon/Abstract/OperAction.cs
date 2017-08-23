using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Messengers;
using Hik.Communication.Scs.Server;
using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MagicUpdaterCommon.Abstract
{
	public abstract class OperAction
	{
		protected int? operationId = 0;
		public OperAction(int? _operationId)
		{
			operationId = _operationId;
		}

		public void SendReportToDB(string message, bool isComplete = false)
		{
			string actionName = this.GetType().ToString();
			if (operationId > 0 && !string.IsNullOrEmpty(actionName))
			{
				SqlWorks.ExecProc("SetActionReport"
					, operationId
					, MainSettings.MainSqlSettings.ComputerId
					, actionName
					, IsFromLan
					, isComplete
					, message);
			}
			else
				NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, $"Невозможно отправить отчет действия: {actionName}. Сообщение: {message}");
		}

		public static void SendReportToDB(int _operationId, string actionName, bool IsFromLan, string message, bool isComplete = false)
		{
			if (_operationId > 0 && !string.IsNullOrEmpty(actionName))
			{
				SqlWorks.ExecProc("SetActionReport"
					, _operationId
					, MainSettings.MainSqlSettings.ComputerId
					, actionName
					, IsFromLan
					, isComplete
					, message);
			}
			else
				NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, $"Невозможно отправить отчет действия: {actionName}. Сообщение: {message}");
		}

		protected abstract void ActExecution();

		public bool IsFromLan { get; set; } = false;

		/// <summary>
		/// запускаем действие
		/// </summary>
		/// <param name="viaNetwork">Сначала выполняется через локальную сеть, потом уже тут</param>
		/// <param name="isWaitForResponse">Ждем ответа по сети</param>
		public bool ActRun(bool viaNetwork = false, bool isWaitForResponse = false, int connectionTimeout = 5000, int responseTimeOut = 5000)
		{
			bool result = true;
			try
			{
				if (viaNetwork && NetworkForActions.IsServerStarted)
				{
					var tasks = new List<Task>();
					if (isWaitForResponse)
					{
						foreach (MainSettings.CompInfo Comp in MainSettings.ComputersList)
						{
							tasks.Add(Task.Factory.StartNew(() =>
							{
								try
								{
									string response = NetworkForActions.SendActionAndWaitForResponse(Comp.IP, 10085, operationId, this.GetType().ToString(), connectionTimeout, responseTimeOut);
									if (!string.IsNullOrEmpty(response))
									{
										if (!response.Equals("ok"))
										{
											SendReportToDB($"Ответ по сети: {response}");
										}
									}
								}
								catch (Exception ex)
								{
									if (ex is TimeoutException || ex is CommunicationException)
									{
										this.SendReportToDB($"ShopId: {Comp.ShopId}, ComputerId: {Comp.ComputerId}, ComputerName: {Comp.ComputerName}, IP: {Comp.IP}. Original: {ex.Message.ToString()}", false);
									}
									else
										this.SendReportToDB(ex.Message.ToString(), false);
								}
							}));
						}

						Task.WaitAll(tasks.ToArray());
					}
					else
					{
						foreach (MainSettings.CompInfo Comp in MainSettings.ComputersList)
						{
							tasks.Add(Task.Factory.StartNew(() =>
							{
								try
								{
									NetworkForActions.SendAction(Comp.IP, 10085, operationId, this.GetType().ToString());
								}
								catch (Exception ex)
								{
									if (ex is TimeoutException || ex is CommunicationException)
									{
										this.SendReportToDB($"ShopId: {Comp.ShopId}, ComputerId: {Comp.ComputerId}, ComputerName: {Comp.ComputerName}, IP: {Comp.IP}. Original: {ex.Message.ToString()}", false);
									}
									else
										this.SendReportToDB(ex.Message.ToString(), false);
								}
							}));

							Task.WaitAll(tasks.ToArray());
						}
					}
				}

				ActExecution();
			}
			catch (Exception ex)
			{
				SendReportToDB(ex.ToString());
				result = false;
			}

			return result;
		}
	}

	public static class NetworkForActions
	{
		private static IScsServer server;
		public static bool IsServerStarted { get; private set; } = false;

		public static void StartServer(int listeningPort)
		{
			if (!IsServerStarted)
			{
				// Создаем сервер, слушаем порт
				server = ScsServerFactory.CreateServer(new ScsTcpEndPoint(listeningPort));
				//Register events of the server to be informed about clients
				server.ClientConnected += Server_ClientConnected;
				server.ClientDisconnected += Server_ClientDisconnected;
				server?.Start(); //Start the server
				IsServerStarted = true;
			}
		}

		private static void Server_ClientDisconnected(object sender, ServerClientEventArgs e)
		{
			//TODO: клиент отключился
		}

		private static void Server_ClientConnected(object sender, ServerClientEventArgs e)
		{
			//TODO: клиент подключился
			e.Client.MessageReceived += Client_MessageReceived;
		}

		private static void Client_MessageReceived(object sender, MessageEventArgs e)
		{
			var message = e.Message as ScsTextMessage; //Server only accepts text messages

			//Get a reference to the client

			string[] splitMsg = message.Text.Split(',');
			int operationId = 0;
			string actionName = null;
			IScsServerClient client = null;
			string actionNameSingle = null;
			if (splitMsg.Length == 3 && Int32.TryParse(splitMsg[0], out operationId))
			{
				try
				{
					string[] actionNameSplit = splitMsg[1].Split('.');
					if (actionNameSplit.Length > 0)
						actionNameSingle = actionNameSplit[actionNameSplit.Length - 1];
					bool isWaitForResponse = splitMsg[2] == "1";

					if (isWaitForResponse)
						client = (IScsServerClient)sender;

					if (message == null || string.IsNullOrEmpty(message.Text))
					{
						if (client != null)
							client?.SendMessage(new ScsTextMessage("Неправильный формат сетевого сообщения"));
						else
							OperAction.SendReportToDB(operationId, actionNameSingle, true, "Неправильный формат сетевого сообщения");
						return;
					}

					actionName = splitMsg[1];
					Type t = Type.GetType(actionName);
					if (t != null)
					{
						OperAction oa = (OperAction)Activator.CreateInstance(t, operationId);
						oa.IsFromLan = true;
						oa.ActRun();
					}
					if (client != null)
						client?.SendMessage(new ScsTextMessage("ok"));
				}
				catch (Exception ex)
				{
					if (client != null)
						client?.SendMessage(new ScsTextMessage(ex.Message.ToString()));
					else
						OperAction.SendReportToDB(operationId, actionNameSingle, true, ex.Message.ToString());
				}
			}
			else
			{
				if (client != null)
					client?.SendMessage(new ScsTextMessage("Неправильный формат сетевого сообщения"));
				else
					OperAction.SendReportToDB(operationId, actionNameSingle, true, "Неправильный формат сетевого сообщения");
			}
		}

		public static void StopServer()
		{
			if (IsServerStarted)
			{
				server?.Stop(); //Stop the server
				IsServerStarted = false;
			}
		}
		/// <summary>
		/// Отправка сообщения по сети и ожидание ответа
		/// </summary>
		public static string SendActionAndWaitForResponse(string ip, int port, int? operationId, string actionName, int connectionTimeout = 3000, int responseTimeOut = 5000)
		{
			string result = null;
			string message = $"{operationId},{actionName},1";
			//Create a client object to connect a server 
			using (var client = ScsClientFactory.CreateClient(new ScsTcpEndPoint(ip, port)))
			{
				//Create a RequestReplyMessenger that uses the client as internal messenger.
				using (var requestReplyMessenger = new RequestReplyMessenger<IScsClient>(client))
				{
					requestReplyMessenger.Start(); //Start request/reply messenger
					client.ConnectTimeout = connectionTimeout;
					client.Connect(); //Connect to the server
					Thread.Sleep(700);
					//Send user message to the server and get response
					var response = requestReplyMessenger.SendMessageAndWaitForResponse(new ScsTextMessage(message), responseTimeOut);

					result = ((ScsTextMessage)response).Text;
				}
			}
			return result;
		}

		public static void SendAction(string ip, int port, int? operationId, string actionName, int connectionTimeout = 3000)
		{
			string message = $"{operationId},{actionName},0";
			//Create a client object to connect a server 
			using (var client = ScsClientFactory.CreateClient(new ScsTcpEndPoint(ip, port)))
			{
				//Create a RequestReplyMessenger that uses the client as internal messenger.
				using (var requestReplyMessenger = new RequestReplyMessenger<IScsClient>(client))
				{
					requestReplyMessenger.Start(); //Start request/reply messenger
					client.ConnectTimeout = connectionTimeout;
					client?.Connect(); //Connect to the server
					Thread.Sleep(700);
					//Send user message to the server
					requestReplyMessenger.SendMessage(new ScsTextMessage(message));
				}
			}
		}
	}
}
