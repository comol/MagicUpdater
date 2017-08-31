using MagicUpdaterCommon.Helpers;
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
	public partial class CheckUpdatesForm : BaseForm
	{
		public CheckUpdatesForm()
		{
			InitializeComponent();
		}

		private void btnCheckUpdates_Click(object sender, EventArgs e)
		{


			//FtpWorks.GetFtpFileVersion("mskftp.sela.ru", "cis_obmen", "cisobmen836", "MagicUpdaterTest", "MagicUpdater.exe");

			FtpWorks.GetFtpFileVersion("sartre.timeweb.ru", "dublerin_mulic", "8CFeyXB3", "Agent", "MagicUpdater.exe");

			//FtpWorks.UploadFileToFtp(@"D:\", "mskftp.sela.ru", "cis_obmen", "cisobmen836", "MagicUpdaterTest", "Services-50.png");

			///FtpWorks.UploadFilesFromFolderToFtp("mskftp.sela.ru", "cis_obmen", "cisobmen836", "MagicUpdaterTest", @"D:\111");
		}
	}
}
