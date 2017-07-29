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
	public partial class CreateOrEditOperationGroupForm : BaseSettingsForm
	{
		private OperationGroupsModel _editGroup;
		public CreateOrEditOperationGroupForm(OperationGroupsModel editGroup = null)
		{
			_editGroup = editGroup;
			InitializeComponent();
			btnSave.Click += BtnSave_Click;
			btnSave.Enabled = IsChangeExists();

			if (_editGroup != null)
			{
				tbOperationGroupName.Text = _editGroup.Name;
				tbDescription.Text = _editGroup.Description;
				this.Text = "Редактирование группы для операций";
			}
		}

		private void BtnSave_Click(object sender, EventArgs e)
		{
			GroupName = tbOperationGroupName.Text;
			Description = tbDescription.Text;
		}

		private bool IsChangeExists()
		{
			return !(tbOperationGroupName.Text == "");
		}

		private void tbNewOperationGroupName_TextChanged(object sender, EventArgs e)
		{
			btnSave.Enabled = IsChangeExists();
		}

		public string GroupName { get; private set; } = "";
		public string Description { get; private set; } = "";
	}
}
