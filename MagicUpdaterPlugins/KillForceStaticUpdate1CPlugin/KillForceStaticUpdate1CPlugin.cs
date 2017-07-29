using System;
using System.ComponentModel;
using System.Threading;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.CommonActions;
using System.Reflection;
using System.ServiceProcess;

namespace KillForceStaticUpdate1CPlugin
{
	public class KillForceStaticUpdate1CPlugin : OperationWithAttr<PluginAttrib>
	{
		public KillForceStaticUpdate1CPlugin(int? operationId, string attrsJson) : base(operationId, attrsJson)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			new ExecProcessing1CAction(Id, "ОбменСЦентром.epf", 10).ActRun();

			//Грохаем все «1cv8c.exe» на всех компах в сети
			KillProcess1CCommon ActionKill = new KillProcess1CCommon(Id);
			ActionKill.ActRun(true, false, 7000);

			Thread.Sleep(3000);

			try
			{
				SetLockBackgroundJobsOnServer1C(true); //Блокировка фоновых заданий на сервере 1С
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, ex.Message.ToString());
			}

			try
			{
				KillUsers(); //Убийство всех сеансов на сервере 1С
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, ex.Message.ToString());
			}

			try
			{
				Restart1CServer();
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, ex.Message.ToString());
			}

			try
			{
				UpdateBase1C(); //Обновление базы
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, ex.Message.ToString());
			}

			try
			{
				SetLockBackgroundJobsOnServer1C(false); //Блокировка фоновых заданий на сервере 1С
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToBaseOrHdd(MainSettings.MainSqlSettings.ComputerId, ex.Message.ToString());
			}

			new SendLogsToCenter1CCommon(Id).ActRun(); //Сбор лога и отправка в центр

		}

		private void Restart1CServer()
		{
			ServiceController controller = new ServiceController();

			controller.MachineName = ".";
			controller.ServiceName = "1C:Enterprise 8.3 Server Agent";
			controller.Stop();

			Thread.Sleep(5000);

			controller.Start();
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

		private void UpdateBase1C()
		{
			StartWithParameter1C act = new StartWithParameter1C(Id, Parameters1C.CmdParams1C.StaticUpdateAndPrintLog); // параметр обновицца и записать лог
			act.ActRun();

			if (!act.NewProc.HasExited)
				act.NewProc.WaitForExit(60000 * 15);

			if (!act.NewProc.HasExited)
				throw new Exception("Процесс конфигуратора не завершился после 15 минут ожидания. Обновление не выполнено.");
		}

	}

	// Класс объявляет аттрибуты операции
	// аттрибуты задаются как параметры в пользовательском режиме
	// но можно указать параметры по умолчанию
	public class PluginAttrib : IOperationAttributes
	{
		//[OperationAttributeDisplayName("Attr1")] // название аттрибута, как оно будет отображаться пользователю
		//public string Attr1 { get; set; } = "Value1"; // значение аттрибута

		//[OperationAttributeDisplayName("Attr2")] //название аттрибута
		//public string Attr2 { get; set; } = "Value2"; //значение аттрибута

	}

	public class RegistrationParams : IRegistrationParams
	{
		public string Description => $"Обновление с завершением сеансов пользователей и перезапуском сервера 1С"; //Описание операции - будет отображаться в подсказке пользователей

		public int GroupId => 1;  // группа операции - чем меньше тем приоритетнее

		public string NameRus => "Обновление с перезапуском сервера 1С"; //Название операции как оно будет отображаться пользователю
	}

}
