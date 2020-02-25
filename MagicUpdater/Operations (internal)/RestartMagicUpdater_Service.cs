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

			IsSendLogAndStatusAfterExecution = false;

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
