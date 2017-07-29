using MagicUpdaterCommon.Abstract;
using System.ComponentModel;

namespace TestAttrPluginOperation
{
	public class TestAttrPluginOperation : OperationWithAttr<OperationAttributes>
	{
		public TestAttrPluginOperation(int? operationId, string attrsJson) : base(operationId, attrsJson)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			//IsSendLogAndStatusAfterExecution = false;
			//string a = Attributes.Name;

			SendOperationReport($"Name = {Attributes.Name}; Switch = {Attributes.Switch}", true);

			//MuCore.ConnectionToSettings.SendAsyncMessage(new CommunicationObject
			//{
			//	ActionType = CommunicationActionType.ShowMessage,
			//	Data = "Plugin Test Attr Operation"
			//});
		}
	}

	public class OperationAttributes : IOperationAttributes
	{
		//[OperationAttributeHidden]
		[OperationAttributeDisplayName("Имя")]
		public string Name { get; set; } = "TestName";

		//[OperationAttributeHidden]
		[OperationAttributeDisplayName("Переключатель")]
		public bool Switch { get; set; } = false;
	}

	public class RegistrationParams : IRegistrationParams
	{
		public string Description => "";

		public int GroupId => 3;

		public string NameRus => "Пустая тестовая операция с атрибутами (плагин)";
	}
}
