using MuMonitorRestart.Core;
using System.Windows.Forms;

namespace MuMonitorRestart
{
	public partial class UpdatingForm : Form
	{
		public UpdatingForm()
		{
			InitializeComponent();
		}

		private void UpdatingForm_Shown(object sender, System.EventArgs e)
		{
			//Tools.KillAllProcessByname(MainSettings.Constants.MAGIC_UPDATER_MONITOR, true);
			MuMonitorUpdate.Update();
		}
	}
}
