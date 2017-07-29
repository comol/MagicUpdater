using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.ComponentModel;

namespace MagicUpdaterCommon.Abstract
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class OperationAttributeDisplayName : Attribute
	{
		public OperationAttributeDisplayName(string name)
		{
			Name = name;
		}

		public string Name { get; private set; }
	}

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class OperationAttributeHidden : Attribute
	{
	}

	public abstract class Operation : IOperation
	{
		/// <summary>
		/// Если мы хотим дернуть операцию руками из кода, то передаем Id = null,
		/// в этом случае логи не попадут в SQL базу
		/// </summary>
		protected int? Id { get; private set; }
		public string OperationType { get; private set; }
		/// <summary>
		/// Если IsSingle == true, то все прочитанные операции после этой, возвращаются в базу
		/// Например самообновление: Приложение перезапускается и теряет очереть операций, зачитанную из базы
		/// </summary>
		protected bool IsCompleted { get; private set; } = true;

		/// <summary>
		/// Операция будет выполняться только на главной кассе
		/// </summary>
		public virtual bool IsOnlyMainCashbox { get; private set; } = false;

		/// <summary>
		/// Режим тестирования в проекте плагинов
		/// </summary>
		protected virtual bool isPluginTestingMode { get; private set; } = false;

		protected string Message { get; private set; } = string.Empty;

		protected bool IsSendLogAndStatusAfterExecution { get; set; } = true;

		protected class ProgressBarProps
		{
			public string ProgressHeader { get; set; } = string.Empty;
			public int Maximum { get; set; } = 100;
			public int Step { get; set; } = 10;
			public int Minimum { get; set; } = 0;
		}

		protected virtual ProgressBarProps ProgressBarProps_p { get; set; } = null;

		public Operation(int? operationId, string attrsJson = null)
		{
			Id = operationId;
			OperationType = this.GetType().Name;
			AddOperState(OperStates.Begin);
		}

		//public Operation(int? operationId)
		//{
		//	Id = operationId;
		//	OperationType = this.GetType().Name;
		//	AddOperState(OperStates.Begin);
		//	//OperationType = (OperationsType)Enum.Parse(typeof(OperationsType), this.GetType().Name);
		//}

		protected abstract void Execution(object sender = null, DoWorkEventArgs e = null);

		/// <summary>
		/// если возвращает false, то операция завершается
		/// </summary>
		/// <returns></returns>
		protected virtual bool BeforeExecution() { return true; }

		protected virtual void AfterExecution() { }
		
		protected void AddOperState(OperStates state)
		{
			if (Id != null && Id.Value > 0)
			{
				if (!isPluginTestingMode)
				{
					SqlWorks.ExecProc("AddOperState", Id, (int)state);
				}
			}
		}

		public static void AddOperState(int? operationId, OperStates state)
		{

			if (operationId != null && operationId > 0)
				SqlWorks.ExecProc("AddOperState", operationId, (int)state);

		}

		public void Run()
		{
			try
			{
				try
				{
					if (BeforeExecution())
					{
						if (ProgressBarProps_p == null)
						{
							AddOperState(OperStates.Runs);
							Execution();
							if (IsSendLogAndStatusAfterExecution)
							{
								AddOperState(OperStates.End);
							}
						}
						else
						{
							//TODO: Делаем связь с приложением и выкидываем прогресс форму
							//ProgressForm form = new ProgressForm(ProgressBarProps_p.ProgressHeader, Execution, ProgressBarProps_p.Maximum, ProgressBarProps_p.Step, ProgressBarProps_p.Minimum);
							//form.ShowDialog();
						}
						AfterExecution();
					}
				}
				catch (Exception ex)
				{
					AddOperState(OperStates.EndWithError);
					AddErrorMessage(ex.ToString());
				}
			}
			finally
			{
				if (IsSendLogAndStatusAfterExecution)
				{
					SendOperationReport();
				}
			}
		}

		private void SendOperationReport()
		{
			if (Id != null && Id.Value > 0)
			{
				if (!isPluginTestingMode)
				{
					//if (Id != null && Id.Value > 0)
					SqlWorks.ExecProc("SendOperationReport", Id, IsCompleted, Message);
					//else
					//	NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, $"Ошибка отправки отчета для операции: {this.GetType().ToString()}");
				}
			}
		}

		public void SendOperationReport(string message, bool isCompleted)
		{
			Message = message;
			IsCompleted = isCompleted;
			SendOperationReport();
		}

		public static void SendOperationReport(int? operationId, string message, bool isCompleted)
		{
			if (operationId != null && operationId.Value > 0)
				SqlWorks.ExecProc("SendOperationReport", operationId, isCompleted, message);
			else
				NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, $"Ошибка отправки отчета для операции");
		}

		protected void AddErrorMessage(string message)
		{
			if (string.IsNullOrEmpty(message))
				return;

			if (IsCompleted)
				Message = "";
			IsCompleted = false;
			if (Message == "")
				Message = message;
			else
				Message += $";\r\n{message}";
		}

		protected void AddCompleteMessage(string message)
		{
			if (string.IsNullOrEmpty(message))
				return;

			if (IsCompleted)
			{
				if (Message == "")
					Message = message;
				else
					Message += $";\r\n{message}";
			}
		}
	}

	public abstract class OperationWithAttr<T> : Operation
	{
		public T Attributes { get; }

		public OperationWithAttr(int? operationId, string attrsJson) : base(operationId)
		{
			if (!string.IsNullOrEmpty(attrsJson))
			{
				Attributes = NewtonJson.ReadJsonString<T>(attrsJson); 
			}
		}
	}

	//public enum OperationsType
	//{
	//	SetOperationsListCheckTimeout = 0,
	//	SelfUpdate = 1,
	//	DynamicUpdate1C = 2,
	//	StaticUpdate1C = 3,
	//	CacheClear1C = 4,
	//	ServerRestart1C = 5,
	//	ForceStaticUpdate1C = 6,
	//	KillForceStaticUpdate1C = 7,
	//	BackupBase = 8,

	//	//Сервисные операции
	//	SetLanMacToDB_Service = 1000,
	//	RegisterViaMac_Service = 1001,
	//	SetExternalIp_Service = 1002,
	//	RestartMagicUpdater_Service = 1003,
	//	ExecProcessing1C = 1004, //Хардкод для конкретного случая
	//	DownloadPaltform1C = 1005, //Хардкод для конкретного случая
	//	CheckFileExistsViaMd5 = 1006, //Хардкод для конкретного случая
	//	CheckShareAccess = 1007,//Хардкод для конкретного случая
	//	PlatformUpdate1C = 1008 //Хардкод для конкретного случая
	//}

	public enum OperStates
	{
		Begin = 1, //Запущена
		End = 2, //Завершена успешно
		WaitingForUserConfirmation = 3, //Ожидание подтверждения от пользователя
		Interrupted = 4, //Прервана
		Runs = 5, //Выполняется
		ConfirmedByUser = 6, //Подтверждена пользователем
		CancelledByUser = 7, //Отклонена пользователем
		CancelledByTimer = 8, //Отклонена по таймеру
		EndWithError = 9 //Завершена с ошибкой
	}
}
