using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterMonitor.Base;
using MagicUpdaterCommon.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MagicUpdater.DL;

namespace MagicUpdaterMonitor.Forms
{
	public partial class SettingsForm : BaseSettingsForm
	{
		public SettingsForm()
		{
			InitializeComponent();
			btnSave.Click += btnSave_Click;
			var tryLoadGlobalSettings = MainSettings.LoadMonitorCommonGlobalSettings();

			if (!tryLoadGlobalSettings.IsComplete)
			{
				MessageBox.Show(tryLoadGlobalSettings.Message);
				return;
			}
			
			tbServer1C.Text = MainForm.MonitorCommonGlobalSettings.Server1C;
			tbBase1C.Text = MainForm.MonitorCommonGlobalSettings.Base1C;
			tbUser1C.Text = MainForm.MonitorCommonGlobalSettings.User1C;
			tbPassword1C.Text = MainForm.MonitorCommonGlobalSettings.Password1C;
			tbPlatform.Text = MainForm.MonitorCommonGlobalSettings.Platform;
			tbServerSQL.Text = MainSettings.JsonSettings.ServerTask;
			tbBaseSql.Text = MainSettings.JsonSettings.BaseTask;
			tbUserSQL.Text = MainSettings.JsonSettings.UserTask;
			tbPasswordSQL.Text = MainSettings.JsonSettings.PasswordTask;
			tbAddressAst.Text = MainForm.MonitorCommonGlobalSettings.AddressAst;
			tbUserAst.Text = MainForm.MonitorCommonGlobalSettings.UserAst;
			tbPasswordAst.Text = MainForm.MonitorCommonGlobalSettings.PasswordAst;
			btnSave.Enabled = IsChangeExists();
		}

		private bool IsChangeExists()
		{
			return !(tbServer1C.Text == MainForm.MonitorCommonGlobalSettings.Server1C
			&&tbBase1C.Text == MainForm.MonitorCommonGlobalSettings.Base1C
			&&tbUser1C.Text == MainForm.MonitorCommonGlobalSettings.User1C
			&&tbPassword1C.Text == MainForm.MonitorCommonGlobalSettings.Password1C
			&&tbPlatform.Text == MainForm.MonitorCommonGlobalSettings.Platform
			&&tbServerSQL.Text == MainSettings.JsonSettings.ServerTask
			&&tbBaseSql.Text == MainSettings.JsonSettings.BaseTask
			&&tbUserSQL.Text == MainSettings.JsonSettings.UserTask
			&&tbPasswordSQL.Text == MainSettings.JsonSettings.PasswordTask
			&&tbAddressAst.Text == MainForm.MonitorCommonGlobalSettings.AddressAst
			&&tbUserAst.Text == MainForm.MonitorCommonGlobalSettings.UserAst
			&&tbPasswordAst.Text == MainForm.MonitorCommonGlobalSettings.PasswordAst);
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			var toJson = new JsonLocalSettings
			{
				ServerTask = tbServerSQL.Text,
				BaseTask = tbBaseSql.Text,
				UserTask = tbUserSQL.Text,
				PasswordTask = tbPasswordSQL.Text,
			};
			

			NewtonJson.WriteJsonFile(toJson, MainSettings.JsonSettingsFileFullPath);

			MainForm.MonitorCommonGlobalSettings.Server1C = tbServer1C.Text;
			MainForm.MonitorCommonGlobalSettings.Base1C = tbBase1C.Text;
			MainForm.MonitorCommonGlobalSettings.User1C = tbUser1C.Text;
			MainForm.MonitorCommonGlobalSettings.Password1C = tbPassword1C.Text;
			MainForm.MonitorCommonGlobalSettings.Platform = tbPlatform.Text;
			MainForm.MonitorCommonGlobalSettings.AddressAst = tbAddressAst.Text;
			MainForm.MonitorCommonGlobalSettings.UserAst = tbUserAst.Text;
			MainForm.MonitorCommonGlobalSettings.PasswordAst = tbPasswordAst.Text;

			SqlWorks.SaveCommonGlobalSettingsToBase(MainForm.MonitorCommonGlobalSettings);
		}

		private void tbAll_TextChanged(object sender, EventArgs e)
		{
			btnSave.Enabled = IsChangeExists();
		}
	}
}
