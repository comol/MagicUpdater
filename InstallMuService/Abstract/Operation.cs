using InstallService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstallService.Abstract
{
	public enum OperStates
	{
		Begin = 1, //Запущена
		End = 2, //Завершена
		WaitingForUserConfirmation = 3, //Ожидание подтверждения от пользователя
		Interrupted = 4, //Прервана
		Runs = 5, //Выполняется
		ConfirmedByUser = 6, //Подтверждена пользователем
		CancelledByUser = 7, //Отклонена пользователем
		CancelledByTimer = 8 //Отклонена по таймеру
	}

	public static class Operation
	{
		public static void AddOperState(int? operationId, OperStates state)
		{

			if (operationId != null && operationId > 0)
				SqlWorks.ExecProc("AddOperState", operationId, (int)state);

		}

		public static void SendOperationReport(int? operationId, string message, bool isCompleted)
		{
			if (operationId != null)
				SqlWorks.ExecProc("SendOperationReport", operationId, isCompleted, message);
			else
				NLogger.LogErrorToHdd($"Ошибка отправки отчета для операции");
		}
	}
}
