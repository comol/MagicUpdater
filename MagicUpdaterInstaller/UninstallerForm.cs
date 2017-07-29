using MagicUpdaterCommon.Helpers;
using MagicUpdaterInstaller.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicUpdaterInstaller
{
	public partial class UninstallerForm : BaseForm
	{
		private bool _isServiceUninstallComplete = false;
		private bool _IsServiceUninstalling = false;

		public UninstallerForm()
		{
			InitializeComponent();
			_rtbInstallServiceLog = rtbInstallServiceLog;
			_progressBar1 = progressBar1;
		}

		private async void btnUnInstall_Click(object sender, EventArgs e)
		{
			_IsServiceUninstalling = true;
			LogInstallServiceString("Удаление службы.");
			btnUnInstall.Enabled = false;
			try
			{
				await Task.Factory.StartNew(() =>
				{
					MuServiceInstaller muServiceInstaller = new MuServiceInstaller(this.tbInstallationpath.Text);
					muServiceInstaller.Uninstall();
				});
				_isServiceUninstallComplete = true;
				LogInstallServiceString("Успешное удаление службы.");

				MessageBox.Show("Удаление MagicUpdater завершено.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
				Application.Exit();
			}
			catch (Exception ex)
			{
				LogInstallServiceString(ex);
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				_IsServiceUninstalling = false;
				btnUnInstall.Enabled = true;
			}
		}

		private void UninstallerForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				if (_IsServiceUninstalling)
				{
					MessageBox.Show("Выход из мастера установки в процессе удаления службы запрещен.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
					e.Cancel = true;
					return;
				}

				if (!_isServiceUninstallComplete && MessageBox.Show($"Удаление не завершено!{Environment.NewLine}Вы уверены, что хотите выйти?"
				, "Подтверждение"
				, MessageBoxButtons.YesNo
				, MessageBoxIcon.Question) == DialogResult.No)
				{
					e.Cancel = true;
				}
				else
				{
					NLogger.LogDebugToHdd($"Удаление не завершено!{Environment.NewLine}Удаление прервано пользователем.");
				}
			}
		}

		private void UninstallerForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}
	}
}
