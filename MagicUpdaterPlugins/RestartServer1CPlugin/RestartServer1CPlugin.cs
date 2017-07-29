using MagicUpdaterCommon.Abstract;
using System.ComponentModel;
using MagicUpdaterCommon.CommonActions;
using System.ServiceProcess;
using System.Threading;

namespace RestartServer1CPlugin
{
	public class RestartServer1CPlugin : OperationWithAttr<PluginAttrib>
	{
		public RestartServer1CPlugin(int? operationId, string attrsJson) : base(operationId, attrsJson)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			Restart1CServer();
		}

		private void Restart1CServer()
		{
			ServiceController controller = new ServiceController();

			controller.MachineName = ".";
			controller.ServiceName = "1C:Enterprise 8.3 Server Agent";
			controller.Stop();

			Thread.Sleep(5000);

			controller.Start();
		}
	}

	// Класс объявляет аттрибуты операции
	// аттрибуты задаются как параметры в пользовательском режиме
	// но можно указать параметры по умолчанию
	public class PluginAttrib : IOperationAttributes
	{
		//[OperationAttributeDisplayName("Attr1")] // название аттрибута, как оно будет отображаться пользователю
		//public string Attr1 { get; set; } = "Value1"; // значение аттрибута

		//[OperationAttributeDisplayName("Attr2")] //название аттрибута
		//public string Attr2 { get; set; } = "Value2"; //значение аттрибута

	}

	public class RegistrationParams : IRegistrationParams
	{
		public string Description => $"Перезапуск сервера 1С"; //Описание операции - будет отображаться в подсказке пользователей

		public int GroupId => 1;  // группа операции - чем меньше тем приоритетнее

		public string NameRus => "Перезапуск сервера 1С"; //Название операции как оно будет отображаться пользователю
	}

}
