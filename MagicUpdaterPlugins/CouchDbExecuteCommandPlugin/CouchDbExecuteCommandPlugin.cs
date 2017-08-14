using MagicUpdaterCommon.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MagicUpdater.Actions;

namespace CouchDbExecuteCommandPlugin
{
    public class CouchDbExecuteCommandPlugin : OperationWithAttr<CouchDbExecuteCommandPluginAttr>
    {
        public CouchDbExecuteCommandPlugin(int? operationId, string attrsJson) : base(operationId, attrsJson)
        {
        }

        protected override void Execution(object sender = null, DoWorkEventArgs e = null)
        {
            var act = new CouchDBExecuteCommandAction(Id, Attributes.Command, Attributes.ParameterName);
            act.ActRun();
            SendOperationReport(act.Result, true);
        }
    }

    public class CouchDbExecuteCommandPluginAttr : IOperationAttributes
    {
        [OperationAttributeDisplayName("Текст команды")]
        public string Command { get; set; } = "";

        [OperationAttributeDisplayName("Имя параметра")]
        public string ParameterName { get; set; } = "";
    }


    public class RegistrationParams : IRegistrationParams
    {
        public string Description => "";

        public int GroupId => 2;

        public string NameRus => "CouchDb: Выполнение команды (плагин)";
    }
}