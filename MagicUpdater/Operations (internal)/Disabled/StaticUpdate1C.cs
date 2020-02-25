using MagicUpdater.Actions;
using System;
using System.ComponentModel;
using System.Threading;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.Abstract;

namespace MagicUpdater.Operations
{
	public class StaticUpdate1C : Operation
	{
		public StaticUpdate1C(int? operationId) : base(operationId) { }

		public override bool IsOnlyMainCashbox => true;
		protected override ProgressBarProps ProgressBarProps_p { get; set; } =
			new ProgressBarProps()
			{
				ProgressHeader = "Статическое обновление",
				Maximum = 5,
				Step = 1
			};

		protected override bool BeforeExecution()
		{
			if (!MainSettings.MainSqlSettings.IsMainCashbox)
			{
				return true;
			}

			AddOperState(OperStates.WaitingForUserConfirmation);
			AddOperState(OperStates.ConfirmedByUser);
			return base.BeforeExecution();
		}


		protected override void Execution(object sender, DoWorkEventArgs e)
		{

			// Если касса, то сначала дожидаемся подтверждения
			if (MainSettings.MainSqlSettings.IsMainCashbox)
			{
				// Пример использования операции с преогресс баром
				BackgroundWorker worker = sender as BackgroundWorker;
				if (worker != null)
				{
					using (worker)
					{
						worker.ReportProgress(0, "Закрытие 1С на всех компьютерах");
						//Грохаем все «1cv8c.exe» на всех компах в сети
						KillProcess1C ActionKill = new KillProcess1C(Id);
						ActionKill.ActRun(true, false, 7000);
						Thread.Sleep(3000);

						worker.ReportProgress(1, "Блокировка фоновых заданий на сервере 1С");
						try
						{
							new LockBackgroundJobsOnServer1C(Id).ActRun(); //Блокировка фоновых заданий на сервере 1С
						}
						catch(Exception ex)
						{
							NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, ex.Message.ToString());
						}

						worker.ReportProgress(2, "Завершение сеансов на сервере 1С");
						try
						{
							new KillAll1CUsers(Id).ActRun(); //Убийство всех сеансов на сервере 1С
						}
						catch (Exception ex)
						{
							NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, ex.Message.ToString());
						}

						worker.ReportProgress(3, "Обновление информационной базы...");
						new UpdateBase1C(Id).ActRun(); //Обновление базы

						worker.ReportProgress(4, "Разрешение фоновых заданий на сервере 1С");
						try
						{
							new UnLockBackgroundJobsOnServer1C(Id).ActRun(); //Блокировка фоновых заданий на сервере 1С
						}
						catch (Exception ex)
						{
							NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, ex.Message.ToString());
						}

						worker.ReportProgress(5, "Сбор логов");
						new SendLogsToCenter1C(Id).ActRun(); //Сбор лога и отправка в центр

						Thread.Sleep(1000);
					}
				}
			}
		}

		protected override void AfterExecution()
		{
			OpenVikiAndShowDialogForStaticUpdate act = new OpenVikiAndShowDialogForStaticUpdate(Id);
			act.ShowMessage = false;
			act.ActRun(true, true);
		}
	}
}
