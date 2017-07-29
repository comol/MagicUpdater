using MagicUpdaterInstaller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MagicUpdaterInstaller
{
	public partial class StartForm : Form
	{
		public StartForm()
		{
			InitializeComponent();
			btnUninstall.Enabled = MuServiceInstaller.IsServiceInstalled();
		}

		private void btnInstall_Click(object sender, EventArgs e)
		{
			Program.MainForm = new InstallerForm();
			this.Hide();
			this.ShowInTaskbar = false;
			Program.MainForm.Show();
		}

		private void btnUninstall_Click(object sender, EventArgs e)
		{
			Program.MainForm = new UninstallerForm();
			this.Hide();
			this.ShowInTaskbar = false;
			Program.MainForm.Show();
		}
	}
}
