using MagicUpdaterMonitor.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicUpdaterMonitor.Forms
{
	public partial class AboutForm : BaseForm
	{
		public AboutForm()
		{
			InitializeComponent();
			FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
			lbVersion.Text = fvi.FileVersion;
		}
	}
}
