using MagicUpdater.DL.DB;
using MagicUpdater.DL.Models;
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
	public partial class ShopSettingsForm : BaseSettingsForm
	{
		private int _computerId;
		private ShopSettingsModel _shopSettingsModel;
		public ShopSettingsForm(int computerId)
		{
			_computerId = computerId;
			InitializeComponent();
			btnSave.Click += btnSave_Click;
			lbShopId.Text = MQueryCommand.GetShopId(computerId) ?? "";
			_shopSettingsModel = MQueryCommand.GetShopSettings(computerId);
		}

		private void tbPhone_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}

		private bool IsChangeExists()
		{
			return !(tbEmail.Text == (_shopSettingsModel.Email ?? "")
			&& tbPhone.Text == (_shopSettingsModel.Phone ?? "")
			&& tbName.Text == (_shopSettingsModel.ShopName ?? "")
			&& tbRegion.Text == (_shopSettingsModel.ShopRegion ?? "")
			&& tbAddressToConnect.Text == (_shopSettingsModel.AddressToConnect ?? "")
			&& chbIsClosed.Checked == _shopSettingsModel.IsClosed);
		}

		private void tbAll_TextChanged(object sender, EventArgs e)
		{
			btnSave.Enabled = IsChangeExists();
		}

		private void ShopSettingsForm_Load(object sender, EventArgs e)
		{
			tbName.Text = _shopSettingsModel.ShopName;
			tbRegion.Text = _shopSettingsModel.ShopRegion;
			tbAddressToConnect.Text = _shopSettingsModel.AddressToConnect;
			tbEmail.Text = _shopSettingsModel.Email;
			tbPhone.Text = _shopSettingsModel.Phone;
			chbIsClosed.Checked = _shopSettingsModel.IsClosed;
			btnSave.Enabled = IsChangeExists();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			_shopSettingsModel.ShopName = tbName.Text;
			_shopSettingsModel.ShopRegion = tbRegion.Text;
			_shopSettingsModel.AddressToConnect = tbAddressToConnect.Text;
			_shopSettingsModel.Email = tbEmail.Text;
			_shopSettingsModel.Phone = tbPhone.Text;
			_shopSettingsModel.IsClosed = chbIsClosed.Checked;
			MQueryCommand.SetShopSettings(_computerId, _shopSettingsModel);
		}

		private void chbIsClosed_CheckedChanged(object sender, EventArgs e)
		{
			btnSave.Enabled = IsChangeExists();
		}
	}
}
