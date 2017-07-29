using MagicUpdaterCommon.Abstract;
using System;
using MagicUpdater.Core;
using MagicUpdaterCommon.Common;
using MagicUpdaterCommon.SettingsTools;
using System.Diagnostics;
using System.Threading;
using CouchDBDeployerPlugin;
using MagicUpdaterCommon.Helpers;

namespace OperatioinDebugger
{
	class Program
	{
		//Наследуемся от операции, которую хотим дебажить. (без атрибутов)
		//public class OperationTest : TestAttrPluginOperation.TestAttrPluginOperation
		//{
		//	public OperationTest(int? operationId, string attrsJson) : base(operationId, attrsJson)
		//	{
		//	}

		//	protected override bool isPluginTestingMode => true;
		//}

		//Наследуемся от операции, которую хотим дебажить. (с атрибутами)
		//public class OperationTest : TestAttrPluginOperation.TestAttrPluginOperation
		//{			
		//	public OperationTest(int? operationId, string attrsJson) : base(operationId, attrsJson)
		//	{
		//	}

		//	protected override bool isPluginTestingMode => true;
		//}

		//С приложением настроек
		private const bool WITH_SETTINGS = true;

		static void Main(string[] args)
		{
			//var muCore = new MuCore(true);

			//if (WITH_SETTINGS)
			//{
			//	Process.Start(@"..\..\..\..\MagicUpdater\bin\Debug\MagicUpdaterSettings.exe");
			//	Thread.Sleep(3000);
			//}

			//Операция без атрибутов
			//Operation operation = (new OperationTest(null)) as Operation;

			//Операция с атрибутами
			//Operation operation = (new OperationTest(null, NewtonJson.GetJsonFromModel(new TestAttrPluginOperation.OperationAttributes()))) as OperationWithAttr<TestAttrPluginOperation.OperationAttributes>;

			//operation.Run();

			//Console.WriteLine("Нажмите любую клавишу...");
			//Console.ReadKey();

			//if (WITH_SETTINGS)
			//{
			//	Tools.KillAllProcessByname(MainSettings.Constants.MAGIC_UPDATER_SETTINGS, true);
			//}
			//muCore.Dispose();
		}
	}
}
