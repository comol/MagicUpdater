using MagicUpdaterCommon.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MagicUpdater.Actions;

namespace CouchDbExecuteCommandPlugin
{
	public class CouchDbExecuteCommandPlugin : Operation
	{
		public CouchDbExecuteCommandPlugin(int? operationId) : base(operationId)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			new CouchDBExecuteCommandAction(Id).ActRun();
		}
	}

	public class RegistrationParams : IRegistrationParams
	{
		public string Description => "";

		public int GroupId => 2;

		public string NameRus => "CouchDb: Выполнение команды (плагин)";
	}
}
