using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.Helpers;
using System;

namespace OperatioinDebugger
{
    class Program
    {
        //Наследуемся от операции, которую хотим дебажить. (без атрибутов)

        public class OperationTest : CouchDbExecuteCommandPlugin.CouchDbExecuteCommandPlugin
        {
            public OperationTest(int? operationId, string attrsJson) : base(operationId, attrsJson)
            {
            }

            protected override bool isPluginTestingMode => true;
        }

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
            //    Process.Start(@"..\..\..\..\MagicUpdater\bin\Debug\MagicUpdaterSettings.exe");
            //    Thread.Sleep(3000);
            //}

            //Операция без атрибутов
            //Operation operation = (new OperationTest(null)) as Operation;

            //Операция с атрибутами
            Operation operation = (new OperationTest(null, NewtonJson.GetJsonFromModel(new CouchDbExecuteCommandPlugin.CouchDbExecuteCommandPluginAttr()))) as OperationWithAttr<CouchDbExecuteCommandPlugin.CouchDbExecuteCommandPluginAttr>;

            operation.Run();

            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();

            //if (WITH_SETTINGS)
            //{
            //    Tools.KillAllProcessByname(MainSettings.Constants.MAGIC_UPDATER_SETTINGS, true);
            //}
            //muCore.Dispose();
        }
    }
}
