using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.Reflection;

namespace MagicUpdater.Actions
{
	public class UnLockBackgroundJobsOnServer1C : OperAction
	{
		public UnLockBackgroundJobsOnServer1C(int? _operationId) : base(_operationId) { }
		protected override void ActExecution()
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
				dynamic WorkingProcess = Agent.GetWorkingProcesses(CurrentCluster);
				dynamic currentwp = WorkingProcess[0];

				String Port = (String)currentwp.MainPort.ToString();
				String HostName = (String)currentwp.HostName;
				String ConnStr = HostName + ":" + Port;

				dynamic wpConnection = V8.ConnectWorkingProcess(ConnStr);
				wpConnection.AddAuthentication(MainSettings.LocalSqlSettings.User1C, MainSettings.LocalSqlSettings.Pass1C);
				dynamic IBArray = wpConnection.GetInfoBases();

				int Count = IBArray.GetLength(0);
				for (int j = 0; j < Count; j++)
				{
					dynamic currentIB = IBArray[j];
					if (string.Compare(currentIB.Name, MainSettings.LocalSqlSettings.Base1C, true) == 0)
					{
						currentIB.ScheduledJobsDenied = false;
						wpConnection.UpdateInfoBase(currentIB);
					}
				}

			}
#endif
		}
	}
}
