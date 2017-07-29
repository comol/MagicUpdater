using MagicUpdater.DL.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicUpdaterMonitor.Forms
{
	public partial class ApplicationLoadingForm : Form
	{
		public bool ShowProgress { get; set; } = true;
		public ApplicationLoadingForm()
		{
			InitializeComponent();
		}

		private void ApplicationLoadingForm_Load(object sender, EventArgs e)
		{
			//lbLoading.Left = (this.Width - lbLoading.Width) / 2;
			//lbLoading.Top = (this.Height - lbLoading.Height) / 2;
			//progressBar1.Left = (this.Width - progressBar1.Width) / 2;
			//progressBar1.Top = (this.Height - progressBar1.Height + lbLoading.Height + 30) / 2;
			//pictureBox1.Left = (this.Width - pictureBox1.Width) / 2;
			//pictureBox1.Top = (this.Height - pictureBox1.Height + progressBar1.Height + lbLoading.Height + 300) / 2;

			try
			{
				tableLayoutPanel1.Left = (this.Width - tableLayoutPanel1.Width) / 2;
				tableLayoutPanel1.Top = (this.Height - tableLayoutPanel1.Height) / 2;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				MLogger.Error(ex.ToString());
			}
		}

		public new void Close()
		{
			base.Close();
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = sender as BackgroundWorker;
			worker.ReportProgress((int)e.Argument);
		}

		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			try
			{
				if (this.InvokeRequired)
				{
					this.Invoke(new MethodInvoker(() => this.progressBar1.Value = e.ProgressPercentage));
				}
				else
				{
					this.progressBar1.Value = e.ProgressPercentage;
				}
			}
			catch (Exception ex)
			{
				
			}
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{

		}

		public async Task ReportProgress(int progressValue)
		{
			await Task.Factory.StartNew(() => backgroundWorker1.RunWorkerAsync(progressValue));
		}

		public new void Show()
		{
			if (ShowProgress)
			{
				//backgroundWorker1.RunWorkerAsync(0);
				progressBar1.Visible = ShowProgress;
			}
			base.Show();
		}
	}
}
