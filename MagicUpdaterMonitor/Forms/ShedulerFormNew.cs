using MagicUpdater.DL.DB;
using MagicUpdater.DL.Helpers;
using MagicUpdaterMonitor.Base;
using MagicUpdaterMonitor.Helpers;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace MagicUpdaterMonitor.Forms
{
	public partial class ShedulerFormNew : BaseForm
	{
		protected bool _designMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);

		public ShedulerFormNew()
		{
			InitializeComponent();

			if (!_designMode)
			{
				InitControls(); 
			}
		}

		protected virtual void InitControls()
		{
			rgvTasks.KeyField = "Id";
			rgvTasks.IsMultiselect = false;
			rgvTasks.dataGridView.RowHeadersVisible = false;
			rgvTasks.IsShowCellExtensionFormByDoubleClick = false;
			rgvTasks.MappingColumns = Mapping.TasksGridColMap;
			rgvTasks.DataSource = MQueryCommand.SelectShedulerTasksGrid();
			//rgvTasks.DataView.Sort = "CreationDate DESC";
			rgvTasks.dataGridView.CellDoubleClick += (sender, e) =>
			{
				btnEditTask.PerformClick();
			};

			#region ContextMenuAdd
			//var miCreateNew = new ToolStripMenuItem();
			//miCreateNew.Name = "miCreateNew";
			//miCreateNew.Text = "Создать новую задачу";
			//miCreateNew.Click += (sender, e) =>
			//{
			//	new NewEditShedulerTaskForm().ShowDialog();
			//};
			//rgvTasks.AddMenuItem(miCreateNew);

			//string miEditText = "Изменить задачу";
			//var miEdit = new ToolStripMenuItem();
			//miEdit.Name = "miEdit";
			//miEdit.Text = miEditText;
			//miEdit.Click += (sender, e) =>
			//{
			//	int? selectedKey = rgvTasks.SelectedValue as int?;
			//	if (selectedKey.HasValue)
			//	{
			//		string taskName = Convert.ToString(rgvTasks.SelectedRow.Cells["Name"].Value);
			//		int taskId = Convert.ToInt32(rgvTasks.SelectedRow.Cells["Id"].Value);
			//		miEdit.Text = $"{miEditText} [{taskName}]";

			//		new NewEditShedulerTaskForm(taskId).ShowDialog();
			//	}
			//};
			//rgvTasks.AddMenuItem(miEdit);
			#endregion
		}

		protected void RefreshGrid<T>(T[] array)
		{
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
			rgvTasks.RefreshDataSourceAsync(array);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

		}

		protected virtual void btnCreateTask_Click(object sender, EventArgs e)
		{
			btnCreateTask.Enabled = false;
			try
			{
				new NewEditShedulerTaskForm().ShowDialog();
				RefreshGrid(MQueryCommand.SelectShedulerTasksGrid());
			}
			finally
			{
				btnCreateTask.Enabled = true;
			}
		}


		protected virtual void btnEditTask_Click(object sender, EventArgs e)
		{
			btnEditTask.Enabled = false;
			try
			{
				int? selectedKey = rgvTasks.SelectedValue as int?;
				if (selectedKey.HasValue)
				{
					int taskId = Convert.ToInt32(rgvTasks.SelectedRow.Cells["Id"].Value);
					new NewEditShedulerTaskForm(taskId).ShowDialog();
					RefreshGrid(MQueryCommand.SelectShedulerTasksGrid());
				}
			}
			finally
			{
				btnEditTask.Enabled = true;
			}
		}

		protected virtual void btnDeleteTask_Click(object sender, EventArgs e)
		{
			btnDeleteTask.Enabled = false;
			try
			{
				int? selectedKey = rgvTasks.SelectedValue as int?;
				if (selectedKey.HasValue)
				{
					int taskId = Convert.ToInt32(rgvTasks.SelectedRow.Cells["Id"].Value);
					string taskName = Convert.ToString(rgvTasks.SelectedRow.Cells["Name"].Value);
					if (MessageBox.Show($"Удалить задание: [{taskName}] ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						var res = MQueryCommand.TryDeleteTask(taskId);
						if (!res.IsComplete)
						{
							MessageBox.Show(res.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						else
						{
							int oldIndex = rgvTasks.SelectedRow.Index < 0 ? 0 : rgvTasks.SelectedRow.Index;
							int index = rgvTasks.SelectedRow.Index - 1 < 0 ? 0 : rgvTasks.SelectedRow.Index - 1;
							rgvTasks.dataGridView.Rows[index].Selected = true;
							Thread.Sleep(300);
							RefreshGrid(MQueryCommand.SelectShedulerTasksGrid());
							rgvTasks.dataGridView.Invalidate();
						}
					}
				}
			}
			finally
			{
				btnDeleteTask.Enabled = true;
			}
		}
	}
}
