using MagicUpdater.DL.DB;
using MagicUpdaterCommon.Common;
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
	public partial class MoveToOperationGroupForm : BaseSettingsForm
	{
		private int _currentGroupId;
		public MoveToOperationGroupForm(string operationTypeName, int currentGroupId)
		{
			_currentGroupId = currentGroupId;
			InitializeComponent();
			LoadOperationGroups();
			btnSave.Click += btnSave_Click;
		}

		private void LoadOperationGroups()
		{
			cbOperationGroup.ValueMember = "Id";
			cbOperationGroup.DisplayMember = "Name";
			cbOperationGroup.DataSource = MQueryCommand.SelectOperationGroups();
			cbOperationGroup.SelectedValue = _currentGroupId;
			btnSave.Enabled = IsChangeExists();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			OperationGroupId = ConvertSafe.ToInt32(cbOperationGroup.SelectedValue);
		}

		private bool IsChangeExists()
		{
			return !(Convert.ToInt32(cbOperationGroup.SelectedValue) == _currentGroupId);
		}

		public int OperationGroupId { get; private set; } = 0;

		private void cbOperationGroup_SelectedValueChanged(object sender, EventArgs e)
		{
			btnSave.Enabled = IsChangeExists();
		}
	}
}
