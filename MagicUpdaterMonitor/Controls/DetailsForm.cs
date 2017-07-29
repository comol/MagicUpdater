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

namespace MagicUpdaterMonitor.Controls
{
	public partial class DetailsForm : BaseForm
	{
		public bool IsShowing { get; set; } = false;
		public DetailsForm()
		{
			InitializeComponent();
		}

		public new void Show()
		{
			IsShowing = true;
			base.Show();
		}

		public new void Hide()
		{
			IsShowing = false;
			base.Hide();
		}

		public new void Close()
		{
			IsShowing = false;
			base.Close();
		}

		private void DetailsForm_Shown(object sender, EventArgs e)
		{
			rgvDetails.Repaint();
		}
	}
}
