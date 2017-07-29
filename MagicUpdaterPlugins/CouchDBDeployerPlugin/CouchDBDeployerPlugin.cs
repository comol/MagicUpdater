using MagicUpdaterCommon.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MagicUpdater.Actions;

namespace CouchDBDeployerPlugin
{
	public class CouchDBDeployerPlugin : Operation
	{
		public CouchDBDeployerPlugin(int? operationId) : base(operationId)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			new CouchDBDeployerAction(Id).ActRun();
		}
	}

	public class RegistrationParams : IRegistrationParams
	{
		public string Description => "";

		public int GroupId => 2;

		public string NameRus => "Установка коуч дб (плагин операция)";
	}
}
