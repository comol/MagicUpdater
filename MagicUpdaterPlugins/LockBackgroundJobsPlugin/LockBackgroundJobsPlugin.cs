using MagicUpdaterCommon.Abstract;
using System.ComponentModel;
using MagicUpdaterCommon.CommonActions;
using System;
using System.Reflection;
using MagicUpdaterCommon.SettingsTools;

namespace LockBackgroundJobsPlugin
{
	public class LockBackgroundJobsPlugin : OperationWithAttr<PluginAttrib>
	{
		public LockBackgroundJobsPlugin(int? operationId, string attrsJson) : base(operationId, attrsJson)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			if (this.Attributes.Block == "1")
			{
				SetLockBackgroundJobsOnServer1C(true);
			}
			else
			{
				SetLockBackgroundJobsOnServer1C(false);
			}
			
		}

		private void SetLockBackgroundJobsOnServer1C(bool flag)
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
						currentIB.ScheduledJobsDenied = flag;
						wpConnection.UpdateInfoBase(currentIB);
					}
				}

			}
#endif
		}
	}

	// Класс объявляет аттрибуты операции
	// аттрибуты задаются как параметры в пользовательском режиме
	// но можно указать параметры по умолчанию
	public class PluginAttrib : IOperationAttributes
	{
		[OperationAttributeDisplayName("Заблокировать")] // название аттрибута, как оно будет отображаться пользователю
		public string Block { get; set; } = "1"; // значение аттрибута
	}

	public class RegistrationParams : IRegistrationParams
	{
		public string Description => $"Блокировка фоновых заданий"; //Описание операции - будет отображаться в подсказке пользователей

		public int GroupId => 1;  // группа операции - чем меньше тем приоритетнее

		public string NameRus => "Блокировка фоновых заданий"; //Название операции как оно будет отображаться пользователю
	}

}
