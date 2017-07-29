using MagicUpdaterCommon.Abstract;
using System.ComponentModel;
using MagicUpdaterCommon.CommonActions;
using System;
using System.Threading;

namespace FileDynamicUpdate1CPlugin
{
	public class FileDynamicUpdate1CPlugin : OperationWithAttr<PluginAttrib>
	{
		public FileDynamicUpdate1CPlugin(int? operationId, string attrsJson) : base(operationId, attrsJson)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{

			MagicUpdaterCommon.CommonActions.StartWithParameter1C act = new MagicUpdaterCommon.CommonActions.StartWithParameter1C(Id, MagicUpdaterCommon.SettingsTools.Parameters1C.CmdParams1C.UpdateAndPrintLog); // параметр обновицца и записать лог
			act.ActRun();

			if (!act.NewProc.HasExited)
				act.NewProc.WaitForExit(60000 * 7);

			if (!act.NewProc.HasExited)
				throw new Exception("Процесс конфигуратора не завершился после 7 минут ожидания. Обновление не выполнено.");

			new SendLogsToCenter1CCommon(Id).ActRun(); // Собираем и отправляем в центр логи
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
		public string Description => $"Динамическое обновление (файловая база)"; //Описание операции - будет отображаться в подсказке пользователей

		public int GroupId => 1;  // группа операции - чем меньше тем приоритетнее

		public string NameRus => "Динамическое обновление (файловая база)"; //Название операции как оно будет отображаться пользователю
	}

}
