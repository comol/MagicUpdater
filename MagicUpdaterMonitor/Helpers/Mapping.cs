using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicUpdaterMonitor.Helpers
{
	public static class Mapping
	{
		public static Dictionary<string, string> TasksGridColMap = new Dictionary<string, string>
		{
			{"Name","Имя"},
			{"Enabled","Включен"},
			{"ModeVis","Расписание"},
			{"RepeatValueVis","Интервал"},
			{"StatusVis","Статус"},
			{"NextStartTimeVis","Следующее выполнение"},
			{"LastStartTime","Последнее выполнение"},
			{"LastEndTime","Последнее завершение выполнения"},
		};

		public static Dictionary<string, string> ComputersGridColMap = new Dictionary<string, string>
		{
			{"Selected","Выбранные"},
			{"ShopId","ID магазина"},
			{"IsClosed", "Закрыт" },
			{"OperationTypeRu","Имя операции" },
			{"OperationCreationDate","Дата создания операции" },
			{"OperState","Состояние операции"},
			{"ShopName","Имя магазина"},
			{"ComputerId","ID компьютера"},
			{"ComputerName"," Имя компьютера"},
			{"Local_IP","Локальный IP"},
			{"External_IP","Внешний IP"},
			{"Is1CServer","Сервер 1С"},
			{"IsMainCashbox","Главная касса"},
			{"ExchangeError","Есть ошибка"},
			{"LastSuccessfulUpload","Выгружено"},
			{"LastSuccessfulReceive","Загружено"},
			{"AddressToConnect","Ярлык удаленного подключения"},
			{"MagicUpdaterVersion","Версия MU"},
			{"Phone","Телефон"},
			{"IsOn","Включен"},
			{"IsOnBitmap","Включен"},
			{"LastErrorString", "Последняя критическая ошибка"}
		};

		public static Dictionary<string, string> StepsGridMapping = new Dictionary<string, string>
		{
			{"Step","N"},
			{ "nameVis", "Имя"},
			{ "nameVisCountCompleteStep", "В случае выполнения"},
			{ "nameVisCountErrorStep", "В случае ошибки"},
		};

		public static Dictionary<string, string> SendOpersGridColMap = new Dictionary<string, string>
		{
			{"DisplayGridName","Операция"},
			{ "Description", "Описание"}
		};

		public static Dictionary<string, string> ShopsGridColMap = new Dictionary<string, string>
		{
			{"Selected","Выбранные"},
			{"ShopId","ID магазина"},
			{"ShopRegion","Регион"}
		};

		public static Dictionary<string, string> OperationsGridColMap = new Dictionary<string, string>
		{
			{"Details","Детали"},
			{"OperationTypeRu","Операция"},
			{"CreatedUser","Пользователь"},
			{"IsFromSheduler","По расписанию"},
			{"ComputerId","ID компьютера"},
			{"ComputerName","Имя компьютера"},
			{"ShopId","ID магазина"},
			{"CreationDate","Дата создания"},
			{"DateRead","Дата считывания"},
			{"OperState","Состояние"},
			{"DateCompleteOrError","Дата завершения"},
			{"Result","Лог"},
			{"LogDate1C","Дата лога 1С"},
			{"LogMessage1C","Лог 1С"}
		};

		public static Dictionary<string, string> ComputerErrorsLogGridColMap = new Dictionary<string, string>
		{
			{"Selected","Выбранные"},
			{"ErrorId","ID ошибки"},
			{"ComputerId","ID компьютера"},
			{"ComputerName","Имя компьютера"},
			{"External_IP","Внешний IP"},
			{"Is1CServer","Сервер 1С"},
			{"IsMainCashbox","Главная касса"},
			{"IsTaskerAlive","Чтение задач"},
			{"IsON","Включен"},
			{"ErrorMessage","Текст ошибки"},
			{"ErrorDate","Дата ошибки"}
		};
	}
}
