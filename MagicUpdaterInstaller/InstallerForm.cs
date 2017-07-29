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
using MagicUpdaterInstaller;
using System.Threading.Tasks;
using System.IO;
using MagicUpdaterInstaller.Common;
using System.Linq;
using System.Management;
using System.Data.SqlClient;

namespace MagicUpdaterInstaller
{
	public partial class InstallerForm : BaseForm
	{
		private const int _serviceCheckTimeout = 4000;
		private System.Threading.Timer _serviceCheckTimer;
		private bool _isServiceInstallComplete = false;
		private bool _isServiceInstalling = false;
		private bool _isShowVersion1cToolTip = false;

		private static readonly string _serverTaskDefault = "mu.sela.ru";
		private static readonly string _baseTaskDefault = "MagicUpdater";
		private static readonly string _userTaskDefault = "Sela";
		private static readonly string _passwordTaskDefault = "Kizombo1313";
		private static readonly string _user1CDefault = "Программист";
		private static readonly string _pass1CDefault = "Sela700";
		private static readonly string _selfUpdateFtpServerDefault = "mskftp.sela.ru";
		private static readonly string _selfUpdateFtpUserDefault = "cis_obmen";
		private static readonly string _selfUpdateFtpPasswordDefault = "cisobmen836";

		private static readonly string _checkQuery = $"select count(*) as cnt from [dbo].[ShopComputers]";
		private static readonly string _checkMessage = "В пробной версии разрешается не более 5 агентов!";

		private bool _is1CBaseOnServer
		{
			get
			{
				return rbServerBase.Checked;
			}
			set
			{
				if (value)
				{
					rbServerBase.Checked = true;
				}
				else
				{
					rbFileBase.Checked = true;
				}
			}
		}


		private enum Pages
		{
			JsonSettings = 0,
			MainAndLocalSqlSettings = 1,
			InstallService = 2,
			ControlTest = 3,
			End = 4
		}

		public InstallerForm()
		{
			InitializeComponent();
			_rtbInstallServiceLog = rtbInstallServiceLog;
			_progressBar1 = progressBar1;

			if (tabControlSettings.SelectedTab == tabPageConnectionString)
			{
				btnPrevousStep.Visible = false;
				//btnNextStep.Left = btnPrevousStep.Left;
			}

			lbServiceStatus.Text = "";
			btnRestartService.Text = "";

			//ConnectionToService = new ApplicationConnector();
			//ConnectionToService.AsyncConnect(MainSettings.Constants.MAGIC_UPDATER_PIPE_NAME);
		}

		private bool CheckServiceIsRunning()
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
					return true;
				}

				if (service.Status == ServiceControllerStatus.Stopped)
				{
					this.Invoke((MethodInvoker)delegate
					{
						lbServiceStatus.BackColor = Color.Pink;
						lbServiceStatus.Text = "Служба остановлена";
						btnRestartService.Text = "Запустить службу";
					});
					return false;
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
				return false;
			}
			return false;
		}

		private bool CheckAgentIsOn()
		{
			try
			{
				if (MainSettings.MainSqlSettings.ComputerId.HasValue)
				{
					var ds = SqlWorks.ExecProc("GetComputerById", MainSettings.MainSqlSettings.ComputerId.Value);
					if (ds != null
						&& ds.Tables.Count > 0
						&& ds.Tables[0].Rows.Count > 0)
					{
						bool isOn = ConvertSafe.ToInt32(ds.Tables[0].Rows[0]["IsON"]) == 1 ? true : false;
						if (isOn)
						{
							if (this.InvokeRequired)
							{
								this.Invoke(new MethodInvoker(() =>
								{
									lbAgentStatus.Text = "Агент отчитывается в базу";
									lbAgentStatus.BackColor = Color.LightGreen;
								}));
							}
							else
							{
								lbAgentStatus.Text = "Агент отчитывается в базу";
								lbAgentStatus.BackColor = Color.LightGreen;
							}
							return true;
						}
						else
						{
							if (this.InvokeRequired)
							{
								this.Invoke(new MethodInvoker(() =>
								{
									lbAgentStatus.Text = "Агент не отчитывается в базу";
									lbAgentStatus.BackColor = Color.Pink;
								}));
							}
							else
							{
								lbAgentStatus.Text = "Агент не отчитывается в базу";
								lbAgentStatus.BackColor = Color.Pink;
							}
							return false;
						}
					}
					else
					{
						if (this.InvokeRequired)
						{
							this.Invoke(new MethodInvoker(() =>
							{
								lbAgentStatus.Text = "Агент не отчитывается в базу";
								lbAgentStatus.BackColor = Color.Pink;
							}));
						}
						else
						{
							lbAgentStatus.Text = "Агент не отчитывается в базу";
							lbAgentStatus.BackColor = Color.Pink;
						}
						return false;
					}
				}
			}
			catch (Exception ex)
			{
				if (this.InvokeRequired)
				{
					this.Invoke(new MethodInvoker(() =>
					{
						lbAgentStatus.Text = "Агент не отчитывается в базу";
						lbAgentStatus.BackColor = Color.Pink;
					}));
				}
				else
				{
					lbAgentStatus.Text = "Агент не отчитывается в базу";
					lbAgentStatus.BackColor = Color.Pink;
				}
				return false;
			}
			return false;
		}

		private void ServiceCheckTimerTick(object state)
		{
			bool enabled = CheckServiceIsRunning() && CheckAgentIsOn();
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() =>
				{
					btnNextStep.Enabled = enabled;
				}));
			}
			else
			{
				btnNextStep.Enabled = enabled;
			}

			_serviceCheckTimer.Change(_serviceCheckTimeout, Timeout.Infinite);
		}

		private void SettingsForm_Shown(object sender, EventArgs e)
		{
			tabControlSettings.Appearance = TabAppearance.FlatButtons;
			tabControlSettings.ItemSize = new Size(0, 1);
			tabControlSettings.SizeMode = TabSizeMode.Fixed;

			ActivateSettingsPage(tabPageConnectionString);

			//if (MainSettings.JsonSettings != null
			//	&& !string.IsNullOrEmpty(MainSettings.JsonSettings.ConnectionString)
			//	&& SqlWorks.CheckSQL_Connection(MainSettings.JsonSettings.ConnectionString))
			//{
			//	ActivateSettingsPage(tabPageSettings);
			//}
			//else
			//{
			//	ActivateSettingsPage(tabPageConnectionString);
			//}
		}

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			try
			{
				//Чистим контролы, чтоб чисто было
				//txtServerTask.Clear();
				//txtBaseTask.Clear();
				//txtUserTask.Clear();
				//txtPasswordTask.Clear();
				//txtUser1C.Clear();
				//txtPass1C.Clear();
				//tbSelfUpdateFtpServer.Clear();
				//tbSelfUpdateFtpUser.Clear();
				//tbSelfUpdateFtpPassword.Clear();

#if !DEMO
				txtServerTask.Text = _serverTaskDefault;
				txtBaseTask.Text = _baseTaskDefault;
				txtUserTask.Text = _userTaskDefault;
				txtPasswordTask.Text = _passwordTaskDefault;
				txtUser1C.Text = _user1CDefault;
				txtPass1C.Text = _pass1CDefault;
				tbSelfUpdateFtpServer.Text = _selfUpdateFtpServerDefault;
				tbSelfUpdateFtpUser.Text = _selfUpdateFtpUserDefault;
				tbSelfUpdateFtpPassword.Text = _selfUpdateFtpPasswordDefault;
#endif

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

			}
			catch (Exception ex)
			{
				ConnectionToService?.DisposeAsyncClient();
				MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				NLogger.LogDebugToHdd(ex.Message.ToString());
				Application.Exit();
			}
		}

		private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				if (_isServiceInstalling)
				{
					MessageBox.Show("Выход из мастера установки в процессе установки службы запрещен.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
					e.Cancel = true;
					return;
				}

				if (MessageBox.Show($"Установка не завершена!{Environment.NewLine}Вы уверены, что хотите прервать установку?"
				, "Подтверждение"
				, MessageBoxButtons.YesNo
				, MessageBoxIcon.Question) == DialogResult.No)
				{
					e.Cancel = true;
				}
				else
				{
					NLogger.LogDebugToHdd($"Установка не завершена!{Environment.NewLine}Установка прервана пользователем.");
				}
			}
		}

		private bool Check1CInstallationWithMessage()
		{
			if (MainSettings.LocalSqlSettings.IsCheck1C)
			{
				Version ver = null;
				Version.TryParse(txtVersion1C.Text.Trim(), out ver);

				//Определение пути по службе 1С работает, но закоменчено, чтобы не было путаницы
				//if (!MainSettings.Get1CPathByService())
				//{
				MainSettings.Load1CPathByVersion(ver);
				//}

				if (string.IsNullOrEmpty(MainSettings.ExePath1C))
				{
					string msg;
					if (string.IsNullOrEmpty(txtVersion1C.Text))
						msg = "версия не введена";
					else
						msg = txtVersion1C.Text.Trim();
					MessageBox.Show($"Версия 1С ({msg}) не установлена на данном компьютере", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
			}
			return true;
		}

		private void btnCheckConnection1C_Click(object sender, EventArgs e)
		{
			var res = MainSettings.GetExePath1C(txtVersion1C.Text);
			if (!res.IsComplete)
			{
				MessageBox.Show(this, res.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var resLogPath = MainSettings.GetLogPath1C();
			if (!resLogPath.IsComplete)
			{
				MessageBox.Show(this, resLogPath.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!Check1CInstallationWithMessage())
				return;
			string exceptionMsg;

			CmdParams cmdParams = new CmdParams();


			//TODO: Добавить файловую базу

			bool check1C = false;
			if (_is1CBaseOnServer)
			{
				check1C = Checks1C.Check1C_Connection(cmdParams.GetConnectionstring1CServerBase(tbServerOrPath1C.Text, tbBase1C.Text, txtUser1C.Text, txtPass1C.Text), out exceptionMsg);
			}
			else
			{
				check1C = Checks1C.Check1C_Connection(cmdParams.GetConnectionstring1CFileBase(tbServerOrPath1C.Text, txtUser1C.Text, txtPass1C.Text), out exceptionMsg);
			}

			if (check1C)
				MessageBox.Show(this, "Успешное подключение к базе 1С", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
			{
				string msg = "Ошибка подключения к базе 1С";
				if (!string.IsNullOrEmpty(exceptionMsg))
					msg = $"{msg}\r\nПричина: {exceptionMsg}";

				MessageBox.Show(this, msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void txtTimeOut_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
			}
		}

		private void txtLastVersion1C_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
			{
				e.Handled = true;
			}
		}

		private void SettingsForm_Resize(object sender, EventArgs e)
		{
		}

		private void btnNextStep_Click(object sender, EventArgs e)
		{
			Button button = sender as Button;
			Pages page = (Pages)button.Tag;
			int tag = (int)button.Tag;

			switch (page)
			{
				case Pages.JsonSettings:
					#region JsonSettings
					//Сохраняем Json
					try
					{
						btnNextStep.Enabled = false;
						var formJson = new JsonLocalSettings
						{
							BaseTask = txtBaseTask.Text,
							ServerTask = txtServerTask.Text,
							PasswordTask = txtPasswordTask.Text,
							UserTask = txtUserTask.Text
						};
						if (!CheckSQLConnection(formJson, false))
						{
							LogString("Ошибка при попытке установить соединение с Sql базой заданий");
							return;
						}
						LogString("Тест соединения с Sql базой заданий успешно пройден.");
						try
						{
							LogString("Создание Json - файла с настроками.");
							NewtonJson.WriteJsonFile(formJson, MainSettings.JsonSettingsFileFullPath);
						}
						catch (Exception ex)
						{
							LogString(ex);
							MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
						LogString("Json с настройками подключения к базе заданий создан успешно.");

						LogString("Проверка Json - файла.");
						var loadFromJsonResult = MainSettings.LoadFromJson();

						if (!loadFromJsonResult.IsComplete)
						{
							LogString(loadFromJsonResult.Message);
							MessageBox.Show(loadFromJsonResult.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
						LogString("Проверка Json - файла завершена успешно.");

						cbShopID.DisplayMember = "ShopId";
						cbShopID.ValueMember = "ShopId";
						cbShopID.DataSource = SqlWorks.ExecProc("SelectShopsList").Tables[0];

						LogString("Поиск настроек агента в Sql базе заданий.");
						var loadSettingsResult = MainSettings.LoadSettings();
						if (!loadSettingsResult.IsComplete)
						{
							LogString($"{loadSettingsResult.Message} - настройки не найдены.");

							try
							{
								//Пробуем узнать версию 1С
								LogString("Пробуем узнать версию 1С");
								const string CONST_PART_1C_AGENT_PATH = "\\bin\\ragent.exe";
								foreach (ServiceController sc in ServiceController.GetServices())
								{
									using (ManagementObject wmiService = new ManagementObject("Win32_Service.Name='" + sc.ServiceName + "'"))
									{
										wmiService.Get();
										string currentserviceExePath = wmiService["PathName"].ToString();
										if (currentserviceExePath.Contains(CONST_PART_1C_AGENT_PATH))
										{
											currentserviceExePath = currentserviceExePath.Replace(CONST_PART_1C_AGENT_PATH, "|");
											string pathCropped = currentserviceExePath.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).First();
											txtVersion1C.Text = pathCropped.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries).Last();
											//Показываем baloon tool tip о том что версия была определена автоматически и может быть не корректной
											_isShowVersion1cToolTip = true;
											break;
										}
									}
								}
							}
							catch (Exception ex)
							{
								LogString(ex);
							}
						}
						else
						{
							LogString($"Настройки найдены.");

							//SqlSettings
							cbShopID.SelectedValue = MainSettings.MainSqlSettings.ShopID;
							tbBase1C.Text = MainSettings.LocalSqlSettings.Base1C;
							_is1CBaseOnServer = MainSettings.LocalSqlSettings.Is1CBaseOnServer;
							tbServerOrPath1C.Text = (MainSettings.LocalSqlSettings.Is1CBaseOnServer ? MainSettings.LocalSqlSettings.Server1C : MainSettings.LocalSqlSettings.InformationBaseDirectory);
							tbBase1C.Text = MainSettings.LocalSqlSettings.Base1C;
							txtUser1C.Text = MainSettings.LocalSqlSettings.User1C;
							txtPass1C.Text = MainSettings.LocalSqlSettings.Pass1C;
							txtVersion1C.Text = MainSettings.LocalSqlSettings.Version1C;
							txtTimeOut.Text = MainSettings.LocalSqlSettings.OperationsListCheckTimeout.ToString();
							txtSelfUpdatePath.Text = MainSettings.LocalSqlSettings.SelfUpdatePath;
							cbIsMainCashbox.Checked = MainSettings.MainSqlSettings.IsMainCashbox;
							cbIsServerLocated.Checked = MainSettings.MainSqlSettings.Is1CServer;
							cbIsCheck1C.Checked = MainSettings.LocalSqlSettings.IsCheck1C;
							tbSelfUpdateFtpServer.Text = MainSettings.LocalSqlSettings.SelfUpdateFtpServer;
							tbSelfUpdateFtpUser.Text = MainSettings.LocalSqlSettings.SelfUpdateFtpUser;
							tbSelfUpdateFtpPassword.Text = MainSettings.LocalSqlSettings.SelfUpdateFtpPassword;
							tbSelfUpdateFtpPath.Text = MainSettings.LocalSqlSettings.SelfUpdateFtpPath;
						}


					}
					finally
					{
						btnNextStep.Enabled = true;
					}
					ActivateSettingsPage(tabPageSettings);

					#endregion
					break;
				case Pages.MainAndLocalSqlSettings:
					#region MainAndLocalSqlSettings

					//Если компьютер не зарегистрирован, то регистрируем его
					#region RegisterAgent
					if (MainSettings.MainSqlSettings == null
											|| !MainSettings.MainSqlSettings.ComputerId.HasValue
											|| MainSettings.MainSqlSettings.ComputerId.Value == 0)
					{
#if DEMO
						try
						{
							string query = _checkQuery;
							using (SqlConnection conn = new SqlConnection(MainSettings.JsonSettings.ConnectionString))
							{
								conn.Open();
								using (SqlCommand command = new SqlCommand(query, conn))
								{
									using (SqlDataReader reader = command.ExecuteReader())
									{
										reader.Read();
										int count = reader.GetInt32(0);

										if (count > 5)
										{
											MessageBox.Show(_checkMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
											Thread thread = new Thread(Terminate);
											thread.Start();
											return;
										}
									}
								}
							}
						}
						catch (Exception ex)
						{
							NLogger.LogErrorToHdd(ex.ToString());
							throw;
						} 
#endif

						LogString("Компьютер не зарегистрирован... Производится регистрация компьютера.");
						var res = MainSettings.RegisterComputerId(ConvertSafe.ToString(cbShopID.SelectedValue));
						if (!res.IsComplete)
						{
							LogString(res.Message);
							MessageBox.Show(res.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
						LogString("Регистрация компьютера произведена успешно.");

						LogString("Проверка регистрации компьютера.");

						var loadSettingsRes = MainSettings.LoadMainSettingsFromSQL();
						if (!loadSettingsRes.IsComplete)
						{
							LogString($"{loadSettingsRes.Message} - настройки не найдены.");
							MessageBox.Show($"{loadSettingsRes.Message} - настройки не найдены.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}

						if (MainSettings.MainSqlSettings == null
											|| !MainSettings.MainSqlSettings.ComputerId.HasValue
											|| MainSettings.MainSqlSettings.ComputerId.Value == 0)
						{
							LogString("Ошибка проверки регистрации компьютера. Не получен ComputerId.");
							MessageBox.Show("Ошибка проверки регистрации компьютера. Не получен ComputerId.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
						LogString("Проверка регистрации компьютера успешно завершена.");
					}
					#endregion

					btnNextStep.Enabled = _isServiceInstallComplete;

					try
					{
						//Сохраняем главные настройки компьютера
						LogString("Сохранение главных настроек компьютера.");
						var formSqlMainSettings = new SqlMainSettings
						{
							ComputerId = MainSettings.MainSqlSettings.ComputerId,
							Is1CServer = cbIsServerLocated.Checked,
							IsMainCashbox = cbIsMainCashbox.Checked,
							ShopID = Convert.ToString(cbShopID.SelectedValue)
						};
						SqlWorks.SaveMainSqlSettingsToBase(formSqlMainSettings);
					}
					catch (Exception ex)
					{
						LogString(ex);
						MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					LogString("Настройки компьютера успешно сохранены.");

					try
					{
						//Сохраняем дополнительные настройки компьютера
						LogString("Сохранение дополнительных настроек компьютера.");
						var sqlLocalSettings = new SqlLocalSettings
						{
							Base1C = tbBase1C.Text,
							User1C = txtUser1C.Text,
							Pass1C = txtPass1C.Text,
							Version1C = txtVersion1C.Text,
							IsCheck1C = cbIsCheck1C.Checked,
							SelfUpdatePath = txtSelfUpdatePath.Text,
							OperationsListCheckTimeout = Convert.ToInt32(txtTimeOut.Text),
							SelfUpdateFtpServer = tbSelfUpdateFtpServer.Text,
							SelfUpdateFtpUser = tbSelfUpdateFtpUser.Text,
							SelfUpdateFtpPassword = tbSelfUpdateFtpPassword.Text,
							SelfUpdateFtpPath = tbSelfUpdateFtpPath.Text,
							Is1CBaseOnServer = _is1CBaseOnServer,
							Server1C = MainSettings.LocalSqlSettings?.Server1C,
							InformationBaseDirectory = MainSettings.LocalSqlSettings?.InformationBaseDirectory
						};

						if (_is1CBaseOnServer)
						{
							sqlLocalSettings.Server1C = tbServerOrPath1C.Text;
						}
						else
						{
							sqlLocalSettings.InformationBaseDirectory = tbServerOrPath1C.Text;
						}

						var res = SqlWorks.SaveLocalSqlSettingsToBase(sqlLocalSettings);

						if (!res.IsComplete)
						{
							LogString(res.Message);
							MessageBox.Show(res.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
					catch (Exception ex)
					{
						LogString(ex);
						MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					LogString("Дополнительные настройки компьютера успешно сохранены.");

					LogString("Проверка настроек.");
					var loadSettingsresult = MainSettings.LoadSettings();
					if (!loadSettingsresult.IsComplete)
					{
						LogString(loadSettingsresult.Message);
						MessageBox.Show(loadSettingsresult.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					LogString("Проверка настроек успешно завершена.");
					ActivateSettingsPage(tabInstallService);
					#endregion

					LogString("Помечаем магазин как открытый.");
					SqlWorks.ExecProc("SetShopToOpen", ConvertSafe.ToString(cbShopID.SelectedValue));

					#region Check1C (disabled)
					//if (cbIsCheck1C.Checked)
					//{
					//	LogString("Проверка установки 1С.");

					//	var resExePath = MainSettings.GetExePath1C();
					//	if (!resExePath.IsComplete)
					//	{
					//		LogString(resExePath.Message);
					//		return;
					//	}

					//	var resLogPath = MainSettings.GetLogPath1C();
					//	if (!resLogPath.IsComplete)
					//	{
					//		LogString(resLogPath.Message);
					//		return;
					//	}

					//	if (!Check1CInstallationWithMessage())
					//	{
					//		LogString("Ошибка проверки установки 1С.");
					//		return;
					//	}
					//	string exceptionMsg;

					//	CmdParams cmdParams = new CmdParams();

					//	if (Checks1C.Check1C_Connection(cmdParams.GetConnectionstring1C(MainSettings.LocalSqlSettings.Server1C, MainSettings.LocalSqlSettings.Base1C, MainSettings.LocalSqlSettings.User1C, MainSettings.LocalSqlSettings.Pass1C), out exceptionMsg))
					//		MessageBox.Show(this, "Успешное подключение к базе 1С", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					//	else
					//	{
					//		string msg = "Ошибка подключения к базе 1С";
					//		if (!string.IsNullOrEmpty(exceptionMsg))
					//			msg = $"{msg}\r\nПричина: {exceptionMsg}";

					//		LogString(msg);
					//		MessageBox.Show(this, msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					//		return;
					//	}
					//	LogString("Успешная проверка установки 1С.");
					//}
					#endregion
					break;
				case Pages.InstallService:
					ActivateSettingsPage(tabControlTest);
					break;
				case Pages.ControlTest:
					this.Hide();
					NLogger.LogDebugToHdd("MagicUpdater успешно установлен.");
					MessageBox.Show("MagicUpdater успешно установлен.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
					Application.Exit();
					break;
				case Pages.End:

					break;
			}
		}

		private void Terminate()
		{
			Application.Exit();
		}

		public void LogString(string logStr)
		{
			NLogger.LogDebugToHdd(logStr);
			rtbLog.AppendText($"{logStr}{Environment.NewLine}");
		}

		public void LogString(Exception ex)
		{
			NLogger.LogDebugToHdd(ex.ToString());
			rtbLog.AppendText($"{ex.Message}{Environment.NewLine}");
		}

		private void btnPrevousStep_Click(object sender, EventArgs e)
		{
			int tag = (int)btnNextStep.Tag;
			if (tag > 0)
			{
				tag--;
			}

			Pages page = (Pages)tag;

			switch (page)
			{
				case Pages.JsonSettings:
					ActivateSettingsPage(tabPageConnectionString);
					break;
				case Pages.MainAndLocalSqlSettings:
					ActivateSettingsPage(tabPageSettings);
					break;
				case Pages.InstallService:
					ActivateSettingsPage(tabInstallService);
					break;
				case Pages.ControlTest:
					ActivateSettingsPage(tabControlTest);
					break;
				case Pages.End:
					break;
				default:
					break;
			}

		}

		private void ActivateSettingsPage(TabPage currentPage)
		{
			if (currentPage == tabPageConnectionString)
			{
				tabControlSettings.SelectedTab = tabPageConnectionString;
				btnPrevousStep.Visible = false;
				//btnNextStep.Left = btnPrevousStep.Left;
				btnNextStep.Text = "Сохранить и перейти к настройкам 1С >";
				btnNextStep.Tag = Pages.JsonSettings;
			}
			else
			if (currentPage == tabPageSettings)
			{
				tabControlSettings.SelectedTab = tabPageSettings;
				btnPrevousStep.Visible = true;
				//btnNextStep.Left = 318;
				btnNextStep.Text = "Установка службы >";
				btnNextStep.Tag = Pages.MainAndLocalSqlSettings;
				if (_isShowVersion1cToolTip)
				{
					toolTip1.Show("Версия 1С определена автоматически! Желательно проверить, так ли это на самом деле!"
						, this
						, txtVersion1C.Location.X + 300
						, txtVersion1C.Location.Y + 10
						, 4000);
					_isShowVersion1cToolTip = false;
				}
			}
			else if (currentPage == tabInstallService)
			{
				tabControlSettings.SelectedTab = tabInstallService;
				btnPrevousStep.Visible = true;
				btnNextStep.Text = "Контрольный тест >";
				btnNextStep.Tag = Pages.InstallService;
			}
			else if (currentPage == tabControlTest)
			{
				btnNextStep.Enabled = false;
				_serviceCheckTimer = new System.Threading.Timer(ServiceCheckTimerTick, null, 0, Timeout.Infinite);
				tabControlSettings.SelectedTab = tabControlTest;
				btnPrevousStep.Visible = true;
				btnNextStep.Text = "Завершить";
				btnNextStep.Tag = Pages.ControlTest;
			}

			CheckMainSqlSettingsValidOnForm();
			CheckJsonSettingsValidOnForm();
		}

		private void btnTestSqlConnection_Click(object sender, EventArgs e)
		{
			Button button = sender as Button;
			button.Enabled = false;
			string buttonText = button.Text;
			button.Text = "......";
			try
			{
				CheckSQLConnection(new JsonLocalSettings
				{
					BaseTask = txtBaseTask.Text,
					PasswordTask = txtPasswordTask.Text,
					ServerTask = txtServerTask.Text,
					UserTask = txtUserTask.Text
				});
			}
			finally
			{
				button.Text = buttonText;
				button.Enabled = true;
			}
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
				NLogger.LogDebugToHdd(ex.Message.ToString());
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

		private void CheckMainSqlSettingsValidOnForm()
		{
			if (tabControlSettings.SelectedTab == tabPageSettings)
			{
				bool primary = !(string.IsNullOrEmpty(txtSelfUpdatePath.Text)
					|| string.IsNullOrEmpty(txtTimeOut.Text)
					|| string.IsNullOrEmpty(tbSelfUpdateFtpServer.Text)
					|| string.IsNullOrEmpty(tbSelfUpdateFtpUser.Text)
					|| string.IsNullOrEmpty(tbSelfUpdateFtpPassword.Text)
					|| string.IsNullOrEmpty(tbSelfUpdateFtpPath.Text));

				bool for1C = !(string.IsNullOrEmpty(txtUser1C.Text)
					|| string.IsNullOrEmpty(txtPass1C.Text)
					|| string.IsNullOrEmpty(txtVersion1C.Text)
					|| string.IsNullOrEmpty(tbServerOrPath1C.Text)
					|| (string.IsNullOrEmpty(tbBase1C.Text) && tbBase1C.Visible));

				if (cbIsCheck1C.Checked)
				{
					btnNextStep.Enabled = primary && for1C;
				}
				else
				{
					btnNextStep.Enabled = primary;
				}
			}
		}

		private void CheckJsonSettingsValidOnForm()
		{
			if (tabControlSettings.SelectedTab == tabPageConnectionString)
			{
				btnNextStep.Enabled = !(string.IsNullOrEmpty(txtServerTask.Text)
					|| string.IsNullOrEmpty(txtBaseTask.Text)
					|| string.IsNullOrEmpty(txtUserTask.Text)
					|| string.IsNullOrEmpty(txtPasswordTask.Text));
			}
		}

		private void ChangeFor1cLabels()
		{
			if (cbIsCheck1C.Checked)
			{
				lbServerOrPath1C.Text = $"*{lbServerOrPath1C.Text}";
				lbBase1C.Text = $"*{lbBase1C.Text}";
				lbUser1C.Text = $"*{lbUser1C.Text}";
				lbPassword1C.Text = $"*{lbPassword1C.Text}";
				lbLastVersion1C.Text = $"*{lbLastVersion1C.Text}";
			}
			else
			{
				lbServerOrPath1C.Text = lbServerOrPath1C.Text.Replace("*", "");
				lbBase1C.Text = lbBase1C.Text.Replace("*", "");
				lbUser1C.Text = lbUser1C.Text.Replace("*", "");
				lbPassword1C.Text = lbPassword1C.Text.Replace("*", "");
				lbLastVersion1C.Text = lbLastVersion1C.Text.Replace("*", "");
			}
		}

		private void TbAll_TextChanged(object sender, EventArgs e)
		{
			CheckMainSqlSettingsValidOnForm();
		}

		private void ChbAll_CheckedChanged(object sender, EventArgs e)
		{
			ChangeFor1cLabels();
			CheckMainSqlSettingsValidOnForm();
		}

		private void TbAllJsonSettings_TextChanged(object sender, EventArgs e)
		{
			CheckJsonSettingsValidOnForm();
		}

		private async void btnInstall_Click(object sender, EventArgs e)
		{
			btnInstall.Enabled = false;
			_isServiceInstalling = true;
			btnInstall.Enabled = false;
			btnPrevousStep.Enabled = false;
			LogString("Установка службы.");

			try
			{
				bool res = false;
				await Task.Factory.StartNew(() =>
				{
					MuServiceInstaller muServiceInstaller = new MuServiceInstaller(tbInstallationpath.Text);
					res = muServiceInstaller.Install();
				});

				if (res)
				{
					btnNextStep.Enabled = true;
					_isServiceInstallComplete = true;
					LogString("Успешная установка службы."); 
				}
				else
				{
					LogString("Ошибка установки службы.");
				}
			}
			catch (Exception ex)
			{
				btnInstall.Enabled = true;
				LogString(ex);
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				btnInstall.Enabled = true;
			}
			finally
			{
				btnPrevousStep.Enabled = true;
				_isServiceInstalling = false;
			}
		}

		private void btnRestartService_Click_1(object sender, EventArgs e)
		{

		}

		private void InstallerForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

		private void rbServerBase_CheckedChanged(object sender, EventArgs e)
		{
			if (rbServerBase.Checked)
			{
				if (cbIsCheck1C.Checked)
				{
					lbServerOrPath1C.Text = "*Сервер 1С предприятия";
				}
				else
				{
					lbServerOrPath1C.Text = "Сервер 1С предприятия";
				}

				lbBase1C.Visible = tbBase1C.Visible = true;
				tbServerOrPath1C.Text = MainSettings.LocalSqlSettings?.Server1C;
				CheckMainSqlSettingsValidOnForm();
			}
		}

		private void rbFileBase_CheckedChanged(object sender, EventArgs e)
		{
			if (rbFileBase.Checked)
			{
				if (rbServerBase.Checked)
				{
					lbServerOrPath1C.Text = "*Каталог информационной базы";
				}
				else
				{
					lbServerOrPath1C.Text = "Каталог информационной базы";
				}

				lbBase1C.Visible = tbBase1C.Visible = false;
				tbServerOrPath1C.Text = MainSettings.LocalSqlSettings?.InformationBaseDirectory;
				CheckMainSqlSettingsValidOnForm();
			}
		}
	}
}
