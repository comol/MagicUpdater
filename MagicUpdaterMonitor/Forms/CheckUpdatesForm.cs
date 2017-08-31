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
			var localVersionRes = FtpWorks.GetFtpFileVersion("mskftp.sela.ru", "cis_obmen", "cisobmen836", "MagicUpdaterTest", "MagicUpdater.exe");
			if (!localVersionRes.IsComplete)
			{

			}

			var remoteVersionRes = FtpWorks.GetFtpFileVersion("sartre.timeweb.ru", "dublerin_mulic", "8CFeyXB3", "Agent", "MagicUpdater.exe");
			if (!remoteVersionRes.IsComplete)
			{

			}

			switch(remoteVersionRes.Ver.CompareTo(localVersionRes.Ver))
			{
				case 0:
					//обновлений нет
					break;
				case 1:
					//обновления есть
					break;
				case -1:
					break;
			}
			//FtpWorks.UploadFileToFtp(@"D:\", "mskftp.sela.ru", "cis_obmen", "cisobmen836", "MagicUpdaterTest", "Services-50.png");

			///FtpWorks.UploadFilesFromFolderToFtp("mskftp.sela.ru", "cis_obmen", "cisobmen836", "MagicUpdaterTest", @"D:\111");
		}
	}
}
