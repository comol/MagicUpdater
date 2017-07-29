using MagicUpdaterCommon.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MagicUpdater.Actions;

namespace CouchDbInitIndexPlugin
{
	public class CouchDbInitIndexPlugin : Operation
	{
		public CouchDbInitIndexPlugin(int? operationId) : base(operationId)
		{
			IsSendLogAndStatusAfterExecution = false;
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			new CouchDBConfiguratorAction(Id).ActRun();
		}
	}

	public class RegistrationParams : IRegistrationParams
	{
		public string Description => "";

		public int GroupId => 2;

		public string NameRus => "CouchDb: Реинициализация индексов (плагин)";
	}
}
