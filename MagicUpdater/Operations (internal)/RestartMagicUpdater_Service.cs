using MagicUpdater.Actions;
using MagicUpdater.Core;
using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.Common;
using SmartAssembly.Attributes;
using System;
using System.ComponentModel;

namespace MagicUpdater.Operations
{
#pragma warning disable CS0436 // Type conflicts with imported type
	[DoNotObfuscateType()]
#pragma warning restore CS0436 // Type conflicts with imported type
	public class RestartMagicUpdater_Service : Operation
	{
		public RestartMagicUpdater_Service(int? operationId) : base(operationId)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			//string curExeName = System.Reflection.Assembly.GetExecutingAssembly().Location;
			//bool isProcessCreated = false;
			//for (int i = 3; i >= 0; i--)
			//{
			//	Process process = null;
			//	if (!isProcessCreated)
			//		process = Process.Start(curExeName, $"force {Id}");
			//	Thread.Sleep(2000);
			//	isProcessCreated = process != null && !process.HasExited;
			//	if (isProcessCreated)
			//		break;
			//}

			//if (!isProcessCreated)
			//	AddErrorMessage("не удалось перезапустить MagicUpdater");

			//var pass = new SecureString();
			//pass.AppendChar('n');
			//pass.AppendChar('i');
			//pass.AppendChar('k');
			//pass.AppendChar('i');
			//pass.AppendChar('t');
			//pass.AppendChar('o');
			//pass.AppendChar('s');
			//pass.AppendChar('s');
			//pass.AppendChar('&');
			//pass.AppendChar('1');
			//pass.AppendChar('9');
			//pass.AppendChar('4');
			//pass.AppendChar('1');
			//pass.AppendChar('3');

			//var psi = new ProcessStartInfo
			//{
			//	FileName = restartServiceApplication,
			//	UserName = "Администратор",
			//	Domain = "",
			//	Password = pass,
			//	UseShellExecute = false,
			//	RedirectStandardOutput = true,
			//	RedirectStandardError = true,
			//	Arguments = $"{PARAMETER_CODE_NAME} {Id}"
			//};

			//Process.Start(psi);

			IsSendLogAndStatusAfterExecution = false;

			//Посылаем команду запуска MagicUpdaterRestart для MagicUpdaterSettings
			//new StartSettingsAferUpdate(Id).ActRun();

			//Отрубаем пайпы
			//MuCore.ConnectionToSettings?.DisposeAsyncServer();

			//Запускаем MagicUpdaterRestart с ключем перезапуска
			try
			{
				Tools.SelfRestart(Id);
			}
			catch (System.Exception ex)
			{
				IsSendLogAndStatusAfterExecution = true;
				throw new System.Exception($"Скорее всего отсутствует приложение MagicUpdaterRestart.{Environment.NewLine}{Environment.NewLine}Original: {ex.ToString()}");
			}
		}
	}
}
