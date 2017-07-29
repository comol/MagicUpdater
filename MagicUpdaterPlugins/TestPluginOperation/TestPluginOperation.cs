using MagicUpdaterCommon.Abstract;
using System.ComponentModel;
using System.IO;

namespace TestPluginOperation
{
	public class TestPluginOperation : Operation
	{
		public TestPluginOperation(int? operationId) : base(operationId)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{

			//string a = Attributes.Name;

			//MuCore.ConnectionToSettings.SendAsyncMessage(new CommunicationObject
			//{
			//	ActionType = CommunicationActionType.ShowMessage,
			//	Data = "Plugin Test Operation"
			//});

			//AddCompleteMessage("Успешно");
			const string FILE_NAME = "test.txt";
			string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location);
			string fileFullPath = Path.Combine(appPath, FILE_NAME);

			if (File.Exists(fileFullPath))
			{
				File.Delete(fileFullPath);
			}

			var fs = File.Create(fileFullPath);
			fs.Close();
			fs.Dispose();
		}
	}

	//public class TextAttrib: IOperationAttributes
	//{
	//	public string Name
	//	{ get; set; }
	//}


	public class RegistrationParams : IRegistrationParams
	{
		public string Description => "";

		public int GroupId => 2;

		public string NameRus => "Пустая тестовая операция без атрибутов (плагин)";
	}
}
