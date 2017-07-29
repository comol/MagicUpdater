using MagicUpdaterCommon.Abstract;
using System.ComponentModel;
using MagicUpdaterCommon.CommonActions;
using System.ServiceProcess;
using System.Threading;
using System.Diagnostics;

namespace RestartComputerPlugin
{
	public class RestartComputerPlugin : OperationWithAttr<PluginAttrib>
	{
		public RestartComputerPlugin(int? operationId, string attrsJson) : base(operationId, attrsJson)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			RestartComp();
		}

		private void RestartComp()
		{
			Process.Start("shutdown -r -f");
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
		public string Description => $"Перезапуск компьютера"; //Описание операции - будет отображаться в подсказке пользователей

		public int GroupId => 1;  // группа операции - чем меньше тем приоритетнее

		public string NameRus => "Перезапуск компьютера"; //Название операции как оно будет отображаться пользователю
	}

}
