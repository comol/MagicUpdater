using MagicUpdaterCommon.Abstract;
using System;
using System.ComponentModel;
using System.IO;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterCommon.Helpers;
using MagicUpdater.Actions;
using MagicUpdaterCommon.CommonActions;

namespace ExecProcessing1CPlugin
{
	public class ExecProcessing1CPlugin : OperationWithAttr<Processing1CAttr>
	{
		public ExecProcessing1CPlugin(int? operationId, string attrsJson) : base(operationId, attrsJson)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			new ExecProcessing1CAction(Id, Attributes.FileName, Attributes.TimeoutMin).ActRun();

			}
	}
	public class Processing1CAttr : IOperationAttributes
	{
		[OperationAttributeDisplayName("Имя файла обработки")]
		public string FileName { get; set; } = "";
		[OperationAttributeDisplayName("Ожидать завершение выполнения, минут")]
		public int TimeoutMin { get; set; } = 5;
	}

	public class RegistrationParams : IRegistrationParams
	{
		public string Description => "";

		public int GroupId => 2;

		public string NameRus => "Выполнение произвольной обработки";
	}
}
