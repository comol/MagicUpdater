using System;
using System.ComponentModel;
using MagicUpdater.Actions;
using System.Threading;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.CommonActions;

namespace MagicUpdater.Operations
{
	public class KillForceStaticUpdate1C : Operation
	{
		public KillForceStaticUpdate1C(int? operationId) : base(operationId)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{

			
			new ExecProcessing1CAction(Id, "ОбменСЦентром.epf" , 10).ActRun();

			//Грохаем все «1cv8c.exe» на всех компах в сети
			KillProcess1C ActionKill = new KillProcess1C(Id);
			ActionKill.ActRun(true, false, 7000);

			Thread.Sleep(3000);

			try
			{
				new LockBackgroundJobsOnServer1C(Id).ActRun(); //Блокировка фоновых заданий на сервере 1С
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, ex.Message.ToString());
			}

			try
			{
				new StopServer1C(Id).ActRun(); //Убиваем сервер 1С
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, ex.Message.ToString());
			}

			Thread.Sleep(10000);

			try
			{
				new StartServer1C(Id).ActRun(); //Запускаем сервер 1С
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, ex.Message.ToString());
			}

			Thread.Sleep(10000);

			new UpdateBase1C(Id).ActRun(); //Обновление базы

			try
			{
				new UnLockBackgroundJobsOnServer1C(Id).ActRun(); //Разблокировка фоновых заданий на сервере 1С
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, ex.Message.ToString());
			}

			new SendLogsToCenter1C(Id).ActRun(); //Сбор лога и отправка в центр
		}
	}
}
