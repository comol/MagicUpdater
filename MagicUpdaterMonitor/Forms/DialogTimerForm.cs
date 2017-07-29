using MagicUpdaterMonitor.Base;
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
	public partial class DialogTimerForm : BaseForm
	{
		private int _timeoutSeconds;
		private string _buttonCloseText;

		public DialogTimerForm(string messageText, int timeoutSeconds = 5)
		{
			_timeoutSeconds = timeoutSeconds;
			InitializeComponent();
			_buttonCloseText = btnClose.Text;
			lbMessageText.Text = messageText;
			tmFormClose.Interval = 1000;
			tmFormClose.Enabled = true;
		}

		private void DialogTimerForm_Load(object sender, EventArgs e)
		{

		}

		private void tmFormClose_Tick(object sender, EventArgs e)
		{
			btnClose.Text = $"{_buttonCloseText} ({_timeoutSeconds})";

			if (_timeoutSeconds == 0)
			{
				this.Close();
			}

			_timeoutSeconds--;
		}
	}
}
