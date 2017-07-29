using System;
using System.Drawing;
using System.Windows.Forms;
using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterCommon.Helpers;
using System.Threading;
using System.ServiceProcess;
using MagicUpdaterCommon.Common;
using MagicUpdaterInstaller.ApplicationTools;

namespace MagicUpdaterInstaller
{
	public partial class SettingsForm : Form
	{
		private const int _serviceCheckTimeout = 4000;
		private System.Threading.Timer _serviceCheckTimer;
		public SettingsForm()
		{
			InitializeComponent();
			this.ShowInTaskbar = false;

			if (tabControlSettings.SelectedTab == tabPageConnectionString)
			{
				btnPrevousStep.Visible = false;
				btnNextStep.Left = btnPrevousStep.Left;
			}

			lbServiceStatus.Text = "";
			btnRestartService.Text = "";

			//ConnectionToService = new ApplicationConnector();
			//ConnectionToService.AsyncConnect(MainSettings.Constants.MAGIC_UPDATER_PIPE_NAME);
		}

		private void CheckService()
		{
			try
			{
				ServiceController service = new ServiceController(MainSettings.Constants.SERVICE_NAME);
				if (service.Status == ServiceControllerStatus.Running)
				{
					this.Invoke((MethodInvoker)delegate
					{
						lbServiceStatus.BackColor = Color.LightGreen;
						lbServiceStatus.Text = "Служба работает";
						btnRestartService.Text = "Перезапустить службу";
					});
				}

				if (service.Status == ServiceControllerStatus.Stopped)
				{
					this.Invoke((MethodInvoker)delegate
					{
						lbServiceStatus.BackColor = Color.Pink;
						lbServiceStatus.Text = "Служба остановлена";
						btnRestartService.Text = "Запустить службу";
					});
				}
			}
			catch (Exception ex)
			{
				this.Invoke((MethodInvoker)delegate
				{
					lbServiceStatus.BackColor = Color.Pink;
					lbServiceStatus.Text = "Служба не установлена";
					btnRestartService.Text = "Запустить службу";
					btnRestartService.Enabled = false;
				});
			}
		}

		private void ServiceCheckTimerTick(object state)
		{
			CheckService();
			_serviceCheckTimer.Change(_serviceCheckTimeout, Timeout.Infinite);
		}

		private void SettingsForm_Shown(object sender, EventArgs e)
		{
			CheckService();
			tabControlSettings.Appearance = TabAppearance.FlatButtons;
			tabControlSettings.ItemSize = new Size(0, 1);
			tabControlSettings.SizeMode = TabSizeMode.Fixed;

			if (SqlWorks.CheckSQL_Connection(MainSettings.JsonSettings.ConnectionString))
			{
				ActivateSettingsPage(tabPageSettings);
			}
			else
			{
				ActivateSettingsPage(tabPageConnectionString);
			}
			_serviceCheckTimer = new System.Threading.Timer(ServiceCheckTimerTick, null, _serviceCheckTimeout, Timeout.Infinite);
		}

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			try
			{
				var loadFromJsonResult = MainSettings.LoadFromJson();

				if (!loadFromJsonResult.IsComplete)
				{
					//NLogger.LogErrorToHdd(res.Message, MainSettings.Constants.MAGIC_UPDATER_SETTINGS);
					//MessageBox.Show(res.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				//JsonSettings
				txtServerTask.Text = MainSettings.JsonSettings.ServerTask;
				txtBaseTask.Text = MainSettings.JsonSettings.BaseTask;
				txtUserTask.Text = MainSettings.JsonSettings.UserTask;
				txtPasswordTask.Text = MainSettings.JsonSettings.PasswordTask;

				var loadSettingsResult = MainSettings.LoadSettings();

				if (!loadSettingsResult.IsComplete)
				{
					return;
				}

				cbShopID.DisplayMember = "ShopId";
				cbShopID.ValueMember = "ShopId";
				cbShopID.DataSource = SqlWorks.ExecProc("SelectShopsList").Tables[0];

				//SqlSettings
				cbShopID.Text = MainSettings.MainSqlSettings.ShopID;
				txtServer1C.Text = MainSettings.LocalSqlSettings.Server1C;
				txtBase1C.Text = MainSettings.LocalSqlSettings.Base1C;
				txtUser1C.Text = MainSettings.LocalSqlSettings.User1C;
				txtPass1C.Text = MainSettings.LocalSqlSettings.Pass1C;
				txtVersion1C.Text = MainSettings.LocalSqlSettings.Version1C;
				txtTimeOut.Text = MainSettings.LocalSqlSettings.OperationsListCheckTimeout.ToString();
				txtSelfUpdatePath.Text = MainSettings.LocalSqlSettings.SelfUpdatePath;
				cbIsMainCashbox.Checked = MainSettings.MainSqlSettings.IsMainCashbox;
				cbIsServerLocated.Checked = MainSettings.MainSqlSettings.Is1CServer;
				cbIsCheck1C.Checked = MainSettings.LocalSqlSettings.IsCheck1C;
			}
			catch (Exception ex)
			{
				ConnectionToService?.DisposeAsyncClient();
				MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				NLogger.LogErrorToHdd(ex.Message.ToString(), MainSettings.Constants.MAGIC_UPDATER_SETTINGS);
				Application.Exit();
			}
		}

		private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
			}
			this.WindowState = FormWindowState.Minimized;
		}

		private void btnConnectTaskBase_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void btnCheckConnectionTasks_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void btnCheckConnection1C_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void mnuItemExit_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void mnuItemSettings_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void testsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Hide();
			notifyIcon1.Visible = true;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			// Tools.AddUpdateAppSettings("Host", "Host1");
			// Tools.AddUpdateAppSettings("Port", "Port1");
		}

		private void txtTimeOut_KeyPress(object sender, KeyPressEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void txtLastVersion1C_KeyPress(object sender, KeyPressEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void SettingsForm_Resize(object sender, EventArgs e)
		{
			if (FormWindowState.Minimized == this.WindowState)
			{
				notifyIcon1.Visible = true;
				notifyIcon1.BalloonTipTitle = "MagicUpdater";
				notifyIcon1.BalloonTipText = "Приложение было свёрнуто в трей";
				notifyIcon1.ShowBalloonTip(5000);
				this.Hide();
			}
			else if (FormWindowState.Normal == this.WindowState)
			{
				notifyIcon1.Visible = false;
			}
		}

		private void ShowSettingsForm()
		{
			Show();
			this.WindowState = FormWindowState.Normal;
			notifyIcon1.Visible = false;
		}

		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			ShowSettingsForm();
		}

		private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowSettingsForm();
		}

		private void btnNextStep_Click(object sender, EventArgs e)
		{
			Button button = sender as Button;
			int tag = (int)button.Tag;

			switch (tag)
			{
				case 1:
					//Сохраняем Json
					var formJson = new JsonLocalSettings
					{
						BaseTask = txtBaseTask.Text,
						ServerTask = txtServerTask.Text,
						PasswordTask = txtPasswordTask.Text,
						UserTask = txtUserTask.Text
					};
					if (!CheckSQLConnection(formJson, false))
					{
						return;
					}
					NewtonJson.WriteJsonFile(formJson, MainSettings.JsonSettingsFileFullPath);
					break;
				case 2:
					//Сохраняем главные настройки компьютера
					var formSqlMainSettings = new SqlMainSettings
					{
						ComputerId = MainSettings.MainSqlSettings.ComputerId,
						Is1CServer = cbIsServerLocated.Checked,
						IsMainCashbox = cbIsMainCashbox.Checked,
						ShopID = cbShopID.Text
					};
					SqlWorks.SaveMainSqlSettingsToBase(formSqlMainSettings);

					//Сохраняем дополнительные настройки компьютера
					SqlWorks.SaveLocalSqlSettingsToBase(new SqlLocalSettings
					{
						Server1C = txtServer1C.Text,
						Base1C = txtBase1C.Text,
						User1C = txtUser1C.Text,
						Pass1C = txtPass1C.Text,
						Version1C = txtVersion1C.Text,
						IsCheck1C = cbIsCheck1C.Checked,
						SelfUpdatePath = txtSelfUpdatePath.Text,
						OperationsListCheckTimeout = Convert.ToInt32(txtTimeOut.Text)
					});

					var loadSettingsresult = MainSettings.LoadSettings();
					if (!loadSettingsresult.IsComplete)
					{
						MessageBox.Show(loadSettingsresult.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					else
					{
						Tools.SelfRestart(0);
						//TODO: тут нужно как-то дать знать на форму что служба перезапущена и все заебись!
						Hide();
						notifyIcon1.Visible = true;
					}
					break;
			}
			ActivateSettingsPage(tabPageSettings);
		}

		private void btnPrevousStep_Click(object sender, EventArgs e)
		{

			ActivateSettingsPage(tabPageConnectionString);
		}

		private void ActivateSettingsPage(TabPage currentPage)
		{
			if (currentPage == tabPageConnectionString)
			{
				tabControlSettings.SelectedTab = tabPageConnectionString;
				btnPrevousStep.Visible = false;
				btnNextStep.Left = btnPrevousStep.Left;
				btnNextStep.Text = "Сохранить и перейти к настройкам 1С >";
				btnNextStep.Tag = 1;
			}
			else
			if (currentPage == tabPageSettings)
			{
				tabControlSettings.SelectedTab = tabPageSettings;
				btnPrevousStep.Visible = true;
				btnNextStep.Left = 318;
				btnNextStep.Text = "Сохранить и закрыть";
				btnNextStep.Tag = 2;
			}
		}

		private void btnTestSqlConnection_Click(object sender, EventArgs e)
		{
			CheckSQLConnection(new JsonLocalSettings
			{
				BaseTask = txtBaseTask.Text,
				PasswordTask = txtPasswordTask.Text,
				ServerTask = txtServerTask.Text,
				UserTask = txtUserTask.Text
			});
		}

		private bool CheckSQLConnection(JsonLocalSettings jsonLocalSettings, bool isShowSuccessMessage = true)
		{
			if (SqlWorks.CheckSQL_Connection(jsonLocalSettings.ConnectionString))
			{
				if (isShowSuccessMessage)
				{
					MessageBox.Show("Тест соединения успешно пройден.", "Тест подключения", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				return true;
			}
			else
			{
				MessageBox.Show("Ошибка при попытке установить соединение", "Тест подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}

		private void btnRestartService_Click(object sender, EventArgs e)
		{
			const int RESTART_TIMEOUT = 60000;
			Button button = sender as Button;

			button.Enabled = false;
			string buttonText = button.Text;
			button.Text = ".......";

			try
			{
				ServiceController service = new ServiceController(MainSettings.Constants.SERVICE_NAME);
				TimeSpan timeout = TimeSpan.FromMilliseconds(RESTART_TIMEOUT);
				if (service.Status == ServiceControllerStatus.Running)
				{
					service.Stop();
					service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

					service.Start();
					service.WaitForStatus(ServiceControllerStatus.Running, timeout);
				}

				if (service.Status == ServiceControllerStatus.Stopped)
				{
					service.Start();
					service.WaitForStatus(ServiceControllerStatus.Running, timeout);
				}
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToHdd(ex.Message.ToString(), MainSettings.Constants.MAGIC_UPDATER_SETTINGS);
				MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				button.Text = buttonText;
				button.Enabled = true;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			//ServiceConnector.SendSyncMessage(new Communications.Common.CommunicationObject
			//{
			//	ActionType = Communications.Common.CommunicationActionType.ShowMessage,
			//	Data = "kjhkjih"
			//});

			ConnectionToService.SendAsyncMessage(new Communications.Common.CommunicationObject
			{
				ActionType = Communications.Common.CommunicationActionType.ShowMessage,
				Data = "message"
			});
		}

		public static ApplicationConnector ConnectionToService { get; private set; }
	}
}
