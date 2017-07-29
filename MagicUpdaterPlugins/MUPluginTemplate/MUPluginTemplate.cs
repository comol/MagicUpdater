using MagicUpdaterCommon.Abstract;
using System.ComponentModel;
using MagicUpdaterCommon.CommonActions;

namespace MUPluginTemplate
{
	public class MUPluginTemplate : OperationWithAttr<PluginAttrib>
	{
		public MUPluginTemplate(int? operationId, string attrsJson) : base(operationId, attrsJson)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			//***************************************************************************
			// TO DO
			// Сюда писать код операции
			//
			//***************************************************************************

			// Пример работы с API:
			// new ExecProcessing1CAction(Id, "ВнешняяОбработка.epf", 10).ActRun();


		}
	}

	// Класс объявляет аттрибуты операции
	// аттрибуты задаются как параметры в пользовательском режиме
	// но можно указать параметры по умолчанию
	public class PluginAttrib : IOperationAttributes
	{
		[OperationAttributeDisplayName("Attr1")] // название аттрибута, как оно будет отображаться пользователю
		public string Attr1 { get; set; } = "Value1"; // значение аттрибута

		[OperationAttributeDisplayName("Attr2")] //название аттрибута
		public string Attr2 { get; set; } = "Value2"; //значение аттрибута

	}

	public class RegistrationParams : IRegistrationParams
	{
		public string Description => $"Описание операции"; //Описание операции - будет отображаться в подсказке пользователей

		public int GroupId => 1;  // группа операции - чем меньше тем приоритетнее

		public string NameRus => "Название операции"; //Название операции как оно будет отображаться пользователю
	}

}
