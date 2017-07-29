using MagicUpdaterCommon.Abstract;
using System.ComponentModel;
using System.Reflection;
using System;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterCommon.CommonActions;

namespace OperationExchangeToCenterPlugin
{
	public class OperationExchangeToCenterPlugin : OperationWithAttr<PluginAttrib>
	{
		public OperationExchangeToCenterPlugin(int? operationId, string attrsJson) : base(operationId, attrsJson)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			new ExecProcessing1CAction(Id, "ОбменСЦентром.epf", 10).ActRun();
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
		public string Description => $"Выполнить обмен с центральной базой"; //Описание операции - будет отображаться в подсказке пользователей

		public int GroupId => 1;  // группа операции - чем меньше тем приоритетнее

		public string NameRus => "Выполнить обмен с центром"; //Название операции как оно будет отображаться пользователю
	}

}
