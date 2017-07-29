using MagicUpdater.DL.DB;
using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterMonitor.Abstract;
using MagicUpdaterMonitor.Base;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MagicUpdaterMonitor.Forms
{
	public partial class AgentSettingsForm : BaseForm
	{
		private SqlLocalSettings _sqlLocalSettings;
		private int _computerId;
		private System.Threading.Timer _agentSettingsIsReadCheckTimer;
		private const int AGENT_SETTINGS_ISREADCHECK_TIMER_INTERVAL = 3000;
		private bool _isLoadingErrorExists = false;
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

		public AgentSettingsForm(int computerId)
		{
			InitializeComponent();
			lbShopId.Text = MQueryCommand.GetShopId(computerId) ?? "";
			_computerId = computerId;
			_sqlLocalSettings = new SqlLocalSettings();
			var res = _sqlLocalSettings.LoadSqlLocalSettings(_computerId);

			if (!res.IsComplete)
			{
				MessageBox.Show(res.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			_isLoadingErrorExists = !res.IsComplete || _computerId <= 0;

			if (!_isLoadingErrorExists)
			{
				_agentSettingsIsReadCheckTimer = new System.Threading.Timer(AgentSettingsIsReadCheckTimerCallback, null, AGENT_SETTINGS_ISREADCHECK_TIMER_INTERVAL, Timeout.Infinite);
				ChangeAgentSettingsReadedLabel(); 
			}
		}

		public new DialogResult ShowDialog()
		{
			if (_isLoadingErrorExists)
			{
				return DialogResult.Abort;
			}
			else
			{
				return base.ShowDialog();
			}
		}

		private void ChangeAgentSettingsReadedLabel()
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() =>
				{
					toolTip1.RemoveAll();
				}));
			}
			else
			{
				toolTip1.RemoveAll();
			}

			bool flag = MQueryCommand.GetIsAgentSettingsReadedValue(_computerId);
			if (flag)
			{
				if (this.InvokeRequired)
				{
					this.Invoke(new MethodInvoker(() =>
					{
						lbSettingsReadingCheck.Text = "Настройки прочитаны агентом";
						lbSettingsReadingCheck.BackColor = Color.PaleGreen;

					}));
				}
				else
				{
					lbSettingsReadingCheck.Text = "Настройки прочитаны агентом";
					lbSettingsReadingCheck.BackColor = Color.PaleGreen;
				}
			}
			else
			{
				if (this.InvokeRequired)
				{
					this.Invoke(new MethodInvoker(() =>
					{
						lbSettingsReadingCheck.Text = "Настройки не прочитаны агентом";
						lbSettingsReadingCheck.BackColor = Color.Salmon;
						toolTip1.SetToolTip(lbSettingsReadingCheck, "Для того чтобы агент прочитал настройки требуется его перезапуск. (Отправить операцию \"Перезапустить агента\")");
					}));
				}
				else
				{
					lbSettingsReadingCheck.Text = "Настройки не прочитаны агентом";
					lbSettingsReadingCheck.BackColor = Color.Salmon;
					toolTip1.SetToolTip(lbSettingsReadingCheck, "Для того чтобы агент прочитал настройки требуется его перезапуск. (Отправить операцию \"Перезапустить агента\")");
				}
			}
		}

		private void AgentSettingsIsReadCheckTimerCallback(object state)
		{
			try
			{
				try
				{
					ChangeAgentSettingsReadedLabel();
				}
				finally
				{
					_agentSettingsIsReadCheckTimer.Change(AGENT_SETTINGS_ISREADCHECK_TIMER_INTERVAL, Timeout.Infinite);
				}
			}
			catch { }
		}

		private bool IsChangeExists()
		{
			return !(tbUser1C.Text == (_sqlLocalSettings.User1C ?? "")
			&& tbPass1C.Text == (_sqlLocalSettings.Pass1C ?? "")
			&& tbOperationsListCheckTimeout.Text == (_sqlLocalSettings.OperationsListCheckTimeout.ToString() ?? "")
			&& tbSelfUpdatePath.Text == (_sqlLocalSettings.SelfUpdatePath ?? "")
			&& tbVersion1C.Text == (_sqlLocalSettings.Version1C ?? "")
			&& chbIsCheck1C.Checked == _sqlLocalSettings.IsCheck1C
			&& tbSelfUpdateFtpServer.Text == (_sqlLocalSettings.SelfUpdateFtpServer ?? "")
			&& tbSelfUpdateFtpUser.Text == (_sqlLocalSettings.SelfUpdateFtpUser ?? "")
			&& tbSelfUpdateFtpPassword.Text == (_sqlLocalSettings.SelfUpdateFtpPassword ?? "")
			&& tbSelfUpdateFtpPath.Text == (_sqlLocalSettings.SelfUpdateFtpPath ?? "")
			&& _is1CBaseOnServer == _sqlLocalSettings.Is1CBaseOnServer
			&& tbServerOrPath1C.Text == (_sqlLocalSettings.Is1CBaseOnServer ? (_sqlLocalSettings.Server1C ?? "") : (_sqlLocalSettings.InformationBaseDirectory ?? ""))
			&& tbBase1C.Text==(_sqlLocalSettings.Base1C ?? ""));
		}

		private void AgentSettingsForm_Load(object sender, EventArgs e)
		{
			tbUser1C.Text = _sqlLocalSettings.User1C;
			tbPass1C.Text = _sqlLocalSettings.Pass1C;
			tbOperationsListCheckTimeout.Text = _sqlLocalSettings.OperationsListCheckTimeout.ToString();
			tbSelfUpdatePath.Text = _sqlLocalSettings.SelfUpdatePath;
			tbVersion1C.Text = _sqlLocalSettings.Version1C;
			chbIsCheck1C.Checked = _sqlLocalSettings.IsCheck1C;
			tbSelfUpdateFtpServer.Text = _sqlLocalSettings.SelfUpdateFtpServer;
			tbSelfUpdateFtpUser.Text = _sqlLocalSettings.SelfUpdateFtpUser;
			tbSelfUpdateFtpPassword.Text = _sqlLocalSettings.SelfUpdateFtpPassword;
			tbSelfUpdateFtpPath.Text = _sqlLocalSettings.SelfUpdateFtpPath;
			_is1CBaseOnServer = _sqlLocalSettings.Is1CBaseOnServer;
			tbServerOrPath1C.Text = (_sqlLocalSettings.Is1CBaseOnServer ? _sqlLocalSettings.Server1C : _sqlLocalSettings.InformationBaseDirectory);
			tbBase1C.Text = _sqlLocalSettings.Base1C;

			btnOk.Enabled = IsChangeExists();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			_agentSettingsIsReadCheckTimer.Change(Timeout.Infinite, Timeout.Infinite);
			_agentSettingsIsReadCheckTimer.Dispose();

			Thread.Sleep(100);

			DialogResult dr = MessageBox.Show($"Для обновления настроек агента, необходим его перезапуск.{Environment.NewLine}Отправить операцию перезапуска?{Environment.NewLine}[Ок] - сохранение настроек и перезапуск агента{Environment.NewLine}[Отмена] - сохранение настроек без перезапуска"
				, "Подтверждение"
				, MessageBoxButtons.OKCancel
				, MessageBoxIcon.Question);

			_sqlLocalSettings.User1C = tbUser1C.Text;
			_sqlLocalSettings.Pass1C = tbPass1C.Text;
			_sqlLocalSettings.OperationsListCheckTimeout = Convert.ToInt32(tbOperationsListCheckTimeout.Text);
			_sqlLocalSettings.SelfUpdatePath = tbSelfUpdatePath.Text;
			_sqlLocalSettings.Version1C = tbVersion1C.Text;
			_sqlLocalSettings.IsCheck1C = chbIsCheck1C.Checked;
			_sqlLocalSettings.SelfUpdateFtpServer = tbSelfUpdateFtpServer.Text;
			_sqlLocalSettings.SelfUpdateFtpUser = tbSelfUpdateFtpUser.Text;
			_sqlLocalSettings.SelfUpdateFtpPassword = tbSelfUpdateFtpPassword.Text;
			_sqlLocalSettings.SelfUpdateFtpPath = tbSelfUpdateFtpPath.Text;
			_sqlLocalSettings.Is1CBaseOnServer = _is1CBaseOnServer;

			if (_sqlLocalSettings.Is1CBaseOnServer)
			{
				_sqlLocalSettings.Server1C = tbServerOrPath1C.Text;
				_sqlLocalSettings.Base1C = tbBase1C.Text;
			}
			else
			{
				_sqlLocalSettings.InformationBaseDirectory = tbServerOrPath1C.Text;
			}
			
			_sqlLocalSettings.SaveSettingsToDb(_computerId);

			//Проставляем флаг в базу о том, что настройки были изменены
			SqlWorks.ExecProc("SetIsAgentSettingsReaded", _computerId, false);

			if (dr == DialogResult.OK)
			{
				new OperationBase(1003).SendForComputer(_computerId);
			}
		}

		private void tbOperationsListCheckTimeout_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}

		private void tbAll_TextChanged(object sender, EventArgs e)
		{
			btnOk.Enabled = IsChangeExists();
		}

		private void chbIsCheck1C_CheckedChanged(object sender, EventArgs e)
		{
			btnOk.Enabled = IsChangeExists();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			_agentSettingsIsReadCheckTimer.Change(Timeout.Infinite, Timeout.Infinite);
			_agentSettingsIsReadCheckTimer.Dispose();
			Thread.Sleep(100);
		}

		private void rbServerBase_CheckedChanged(object sender, EventArgs e)
		{
			if (rbServerBase.Checked)
			{
				lbServerOrPath1C.Text = "Сервер 1С";
				lbBase1C.Visible = tbBase1C.Visible = true;
				tbServerOrPath1C.Text = _sqlLocalSettings.Server1C;
				btnOk.Enabled = IsChangeExists();
			}
		}

		private void rbFileBase_CheckedChanged(object sender, EventArgs e)
		{
			if (rbFileBase.Checked)
			{
				lbServerOrPath1C.Text = "Каталог информационной базы";
				lbBase1C.Visible = tbBase1C.Visible = false;
				tbServerOrPath1C.Text = _sqlLocalSettings.InformationBaseDirectory;
				btnOk.Enabled = IsChangeExists();
			}
		}
	}
}
