using MagicUpdater.DL.DB;
using MagicUpdaterMonitor.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicUpdaterMonitor.Forms
{
	public partial class ShedulerPluginForm : ShedulerFormNew
	{
		public ShedulerPluginForm() : base()
		{
			//InitializeComponent();
		}

		protected override void InitControls()
		{
			rgvTasks.KeyField = "Id";
			rgvTasks.IsMultiselect = false;
			rgvTasks.dataGridView.RowHeadersVisible = false;
			rgvTasks.IsShowCellExtensionFormByDoubleClick = false;
			rgvTasks.MappingColumns = Mapping.TasksGridColMap;
			rgvTasks.DataSource = MQueryCommand.SelectShedulerPluginTasksGrid();
			rgvTasks.HideColumns("FirstStepId", "StartTime");
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

		protected override void btnCreateTask_Click(object sender, EventArgs e)
		{
			btnCreateTask.Enabled = false;
			try
			{
				new NewEditShedulerPluginTaskForm().ShowDialog();
				RefreshGrid(MQueryCommand.SelectShedulerPluginTasksGrid());
			}
			finally
			{
				btnCreateTask.Enabled = true;
			}
		}


		protected override void btnEditTask_Click(object sender, EventArgs e)
		{
			btnEditTask.Enabled = false;
			try
			{
				int? selectedKey = rgvTasks.SelectedValue as int?;
				if (selectedKey.HasValue)
				{
					int taskId = Convert.ToInt32(rgvTasks.SelectedRow.Cells["Id"].Value);
					new NewEditShedulerPluginTaskForm(taskId).ShowDialog();
					RefreshGrid(MQueryCommand.SelectShedulerPluginTasksGrid());
				}
			}
			finally
			{
				btnEditTask.Enabled = true;
			}
		}

		protected override void btnDeleteTask_Click(object sender, EventArgs e)
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
						var res = MQueryCommand.TryDeletePluginTask(taskId);
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
							RefreshGrid(MQueryCommand.SelectShedulerPluginTasksGrid());
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
