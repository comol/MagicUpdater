using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.Reflection;

namespace MagicUpdater.Actions
{
	public class KillAll1CUsers : OperAction
	{
		public KillAll1CUsers(int? _operationId) : base(_operationId) { }
		private void KillUsers()
		{
#if !NO1CCOM
			Type type = TypeDelegator.GetTypeFromProgID("V83.ComConnector");
			dynamic V8 = Activator.CreateInstance(type);
			dynamic Agent = V8.ConnectAgent(MainSettings.LocalSqlSettings.Server1C);      // Соединились с 1С Агентом
			dynamic Clusters = Agent.GetClusters();
			int ClustersCount = Clusters.GetLength(0);
			for (int i = 0; i < ClustersCount; i++)
			{
				dynamic CurrentCluster = Clusters[i];
				Agent.Authenticate(CurrentCluster, "", "");
				dynamic IBSessions = Agent.GetSessions(CurrentCluster);
				int SessionsCount = IBSessions.GetLength(0);
				for (int k = 0; k < SessionsCount; k++)
				{
					Agent.TerminateSession(CurrentCluster, IBSessions[k]);
				}
			}
#endif
		}

		protected override void ActExecution()
		{
			KillUsers();
		}
	}
}
