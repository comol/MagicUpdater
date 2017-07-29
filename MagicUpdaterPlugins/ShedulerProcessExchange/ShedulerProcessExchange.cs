using MagicUpdaterCommon.Abstract;
using System.ComponentModel;
using System.Reflection;
using System;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterCommon.CommonActions;

namespace OperationExchangeToCenterPlugin
{
	public class ShedulerProcessExchange : Operation
	{
		public ShedulerProcessExchange(int? operationId, string attrsJson) : base(operationId, attrsJson)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			new ExecProcessing1CAction(Id, "Upload1C.epf", 120).ActRun();
		}
	}

	public class RegistrationParams : IRegistrationParams
	{
		public string Description => $"Обновить данные из 1С"; //Описание операции - будет отображаться в подсказке пользователей

		public int GroupId => 1;  // группа операции - чем меньше тем приоритетнее

		public string NameRus => "Обновить данные из 1С"; //Название операции как оно будет отображаться пользователю
	}

}
