using MagicUpdater.DL.DB;
using MagicUpdaterMonitor.Base;
using MagicUpdaterMonitor.Helpers;
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
	public partial class CreateShopForm : BaseForm
	{
		public CreateShopForm()
		{
			InitializeComponent();
		}

		public string ShopId => tbShopId.Text;
		public string ShopRegion => tbShopRegion.Text;

		private const int CP_NOCLOSE_BUTTON = 0x200;
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams myCp = base.CreateParams;
				myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
				return myCp;
			}
		}

		private async void btnOk_Click(object sender, EventArgs e)
		{
			if(string.IsNullOrEmpty(tbShopId.Text))
			{
				lbError.Text = "Не заполнено ShopId";
				return;
			}
			if (string.IsNullOrEmpty(tbShopRegion.Text))
			{
				lbError.Text = "Не заполнено ShopRegion";
				return;
			}

			var result = await MQueryCommand.TryInsertNewShopAsync(tbShopId.Text, tbShopRegion.Text);
			if (result.IsComplete)
			{
				DialogResult = DialogResult.OK;
			}
			else
			{
				lbError.Text = result.Message;
			}
		}

		private void tbShopId_Enter(object sender, EventArgs e)
		{
			lbError.Text = "";
		}
	}
}
