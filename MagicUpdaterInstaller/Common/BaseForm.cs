using MagicUpdaterCommon.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MagicUpdaterInstaller.Common
{
	public partial class BaseForm : Form
	{
		protected RichTextBox _rtbInstallServiceLog;
		protected ProgressBar _progressBar1;

		public BaseForm()
		{
			InitializeComponent();
		}
		public void LogInstallServiceString(string logStr)
		{
			NLogger.LogDebugToHdd(logStr);
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() =>
				{
					_rtbInstallServiceLog.AppendText($"{logStr}{Environment.NewLine}");
					if (_progressBar1.Value < _progressBar1.Maximum)
					{
						_progressBar1.Value++;
					}
				}));
			}
			else
			{
				_rtbInstallServiceLog.AppendText($"{logStr}{Environment.NewLine}");
				if (_progressBar1.Value < _progressBar1.Maximum)
				{
					_progressBar1.Value++;
				}
			}
		}

		public void LogInstallServiceString(Exception ex)
		{
			NLogger.LogDebugToHdd(ex.ToString());
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() =>
				{
					_rtbInstallServiceLog.AppendText($"{ex.Message}{Environment.NewLine}");
					if (_progressBar1.Value < _progressBar1.Maximum)
					{
						_progressBar1.Value++;
					}
				}));
			}
			else
			{
				_rtbInstallServiceLog.AppendText($"{ex.Message}{Environment.NewLine}");
				if (_progressBar1.Value < _progressBar1.Maximum)
				{
					_progressBar1.Value++;
				}
			}

		}
	}
}
