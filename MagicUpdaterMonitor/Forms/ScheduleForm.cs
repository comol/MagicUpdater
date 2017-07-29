using MagicUpdater.DL;
using MagicUpdater.DL.Common;
using MagicUpdater.DL.DB;
using MagicUpdater.DL.Helpers;
using MagicUpdaterMonitor.Base;
using MagicUpdaterMonitor.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicUpdaterMonitor.Forms
{
	public partial class ScheduleForm : BaseForm
	{
		private string _newTaskName;
		private bool _isAddTaskMode;
		private TimeSpan _utcOffset;
		private bool _isFormLoading;

		public ScheduleForm()
		{
			InitializeComponent();
			_utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
		}

		private void PrepareToAddTask()
		{
			this._isAddTaskMode = true;
			var newTaskName = "";
			var newTaskForm = new NewTaskForm();
			if (newTaskForm.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			newTaskName = newTaskForm.NewTaskName;

			if (string.IsNullOrEmpty(newTaskName))
			{
				MessageBox.Show("Название задачи на может быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Очистка полей
			this.SetDefaultControlValues();
			this.edTaskName.Text = newTaskName;
			this._newTaskName = newTaskName;
			this.stepsTree1.ResetTree();
			stepsTree1.stepTree.Visible = true;

			// Блокируем выбор тасков, пока не завершили редактирование

			this.toolStripButtonAddTask.Enabled =
			this.toolStripButtonRemoveTask.Enabled =
			this.lbTasks.Enabled = false;

			this.labelEditing.Visible = true;
			this.btnCancel.Visible = true;
		}

		private void addTaskToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.PrepareToAddTask();
		}

		private void PaintComputersGrid(DataGridView sender)
		{
			DataGridViewRow[] rows = sender.Rows.OfType<DataGridViewRow>().ToArray();
			foreach (DataGridViewRow row in rows)
			{
				if (Convert.ToInt32(row.Cells["IsOn"].Value) == 1)
				{
					row.Cells["IsOn"].Style.BackColor = Color.Green;
				}
				else
				{
					row.Cells["IsOn"].Style.BackColor = Color.Red;
				}
			}
		}

		private async Task InitComputersShedulerGridAsync()
		{
			await rgvComputersForSheduler.ShowLoading(true);

			try
			{
				rgvComputersForSheduler.KeyField = "ComputerId";
				rgvComputersForSheduler.PaintCells += PaintComputersForShedulerGrid;
				rgvComputersForSheduler.dataGridView.CellPainting += DataGridView_CellPainting;

				rgvComputersForSheduler.MappingColumns = Mapping.ComputersGridColMap;
				rgvComputersForSheduler.DataSource = await MQueryCommand.SelectShopComputersServerViewGridAsync();

				rgvComputersForSheduler.HideColumns("Phone",
										  "AddressToConnect",
										  "LastSuccessfulReceive",
										  "LastSuccessfulUpload",
										  "ExchangeError",
										  "IsMainCashbox",
										  "OperationTypeRu",
										  "OperationCreationDate",
										  "OperState");

				rgvComputersForSheduler.HideColumns("Is1CServer", "IsMainCashbox", "IsClosed");

				const string SHOW_ADDITIONAL_COLUMNS = "Показать дополнительные столбцы";
				const string HIDE_ADDITIONAL_COLUMNS = "Скрыть дополнительные столбцы";
				string[] additionalColumns = new string[]
				{
				"LastError",
				"External_IP"
				};
				rgvComputersForSheduler.HideColumns(additionalColumns);
				var miShowHideColumns = new ToolStripMenuItem();
				miShowHideColumns.Name = "miShowHideColumns";
				miShowHideColumns.Text = SHOW_ADDITIONAL_COLUMNS;
				miShowHideColumns.Click += (sender, e) =>
				{
					if (miShowHideColumns.Text == SHOW_ADDITIONAL_COLUMNS)
					{
						miShowHideColumns.Text = HIDE_ADDITIONAL_COLUMNS;
						rgvComputersForSheduler.ShowColumns(additionalColumns);
					}
					else
					{
						miShowHideColumns.Text = SHOW_ADDITIONAL_COLUMNS;
						rgvComputersForSheduler.HideColumns(additionalColumns);
					}
				};
				rgvComputersForSheduler.AddMenuItem(miShowHideColumns);

				this.rgvComputersForSheduler.CbSelectAllVisible = true;

				rgvComputersForSheduler.dataGridView.Columns["ShopName"].Width = 150;
				rgvComputersForSheduler.dataGridView.Columns["Selected"].Width = 70;
				rgvComputersForSheduler.dataGridView.Columns["IsOn"].Width = 70;

				btnRefreshGrid.Enabled = true;
			}
			finally
			{
				await rgvComputersForSheduler.ShowLoading(false);
			}
		}

		private void PaintComputersForShedulerGrid(DataGridView sender)
		{
			DataGridViewRow[] rows = sender.Rows.OfType<DataGridViewRow>().ToArray();
			foreach (DataGridViewRow dr in rows)
			{
				if (LastVersionChecker.CompareVersions(Convert.ToString(dr.Cells["MagicUpdaterVersion"].Value)) == VersionCompareResult.Before)
				{
					dr.Cells["MagicUpdaterVersion"].Style.BackColor = Color.Pink;
				}

				if (LastVersionChecker.CompareVersions(Convert.ToString(dr.Cells["MagicUpdaterVersion"].Value)) == VersionCompareResult.Equal)
				{
					dr.Cells["MagicUpdaterVersion"].Style.BackColor = Color.LightGreen;
				}

				if (LastVersionChecker.CompareVersions(Convert.ToString(dr.Cells["MagicUpdaterVersion"].Value)) == VersionCompareResult.Subsequent)
				{
					dr.Cells["MagicUpdaterVersion"].Style.BackColor = Color.DeepSkyBlue;
				}

				if (Convert.ToInt32(dr.Cells["ComputerId"].Value) < 0)
				{
					dr.Cells["Selected"].ReadOnly = true;
					foreach (DataGridViewCell cell in dr.Cells)
					{
						cell.Style.BackColor = Color.LightGray;
					}
				}

				//Включен или нет
				if (Convert.ToInt32(dr.Cells["IsOn"].Value) == 1)
				{
					dr.Cells["IsOn"].Style.BackColor = Color.Green;
				}
				else
				{
					dr.Cells["IsOn"].Style.BackColor = Color.Red;
				}

				//Ошибка обмена
				if (Convert.ToBoolean(dr.Cells["ExchangeError"].Value)
					&& string.IsNullOrEmpty(Convert.ToString(dr.Cells["OperState"].Value))
					&& Convert.ToInt32(dr.Cells["ComputerId"].Value) > 0)
				{
					dr.Cells["ShopName"].Style.BackColor =
					dr.Cells["LastSuccessfulReceive"].Style.BackColor =
					dr.Cells["LastSuccessfulUpload"].Style.BackColor =
					dr.Cells["OperationTypeRu"].Style.BackColor =
					dr.Cells["OperationCreationDate"].Style.BackColor =
					dr.Cells["OperState"].Style.BackColor = Color.PaleVioletRed;
				}

				//Статусы операций
				#region OperStates
				if (Convert.ToString(dr.Cells["OperState"].Value) == "Завершена с ошибкой" && Convert.ToInt32(dr.Cells["ComputerId"].Value) > 0)
				{
					dr.Cells["ShopName"].Style.BackColor =
					dr.Cells["LastSuccessfulReceive"].Style.BackColor =
					dr.Cells["LastSuccessfulUpload"].Style.BackColor =
					dr.Cells["OperationTypeRu"].Style.BackColor =
					dr.Cells["OperationCreationDate"].Style.BackColor =
					dr.Cells["OperState"].Style.BackColor = Color.PaleVioletRed;
				}
				else if (Convert.ToString(dr.Cells["OperState"].Value) == "Выполняется" && Convert.ToInt32(dr.Cells["ComputerId"].Value) > 0)
				{
					dr.Cells["ShopName"].Style.BackColor =
					dr.Cells["LastSuccessfulReceive"].Style.BackColor =
					dr.Cells["LastSuccessfulUpload"].Style.BackColor =
					dr.Cells["OperationTypeRu"].Style.BackColor =
					dr.Cells["OperationCreationDate"].Style.BackColor =
					dr.Cells["OperState"].Style.BackColor = Color.LightYellow;
				}
				else if (Convert.ToString(dr.Cells["OperState"].Value) == "Завершена успешно" && Convert.ToInt32(dr.Cells["ComputerId"].Value) > 0)
				{
					dr.Cells["ShopName"].Style.BackColor =
					dr.Cells["LastSuccessfulReceive"].Style.BackColor =
					dr.Cells["LastSuccessfulUpload"].Style.BackColor =
					dr.Cells["OperationTypeRu"].Style.BackColor =
					dr.Cells["OperationCreationDate"].Style.BackColor =
					dr.Cells["OperState"].Style.BackColor = Color.FromArgb(215, 255, 220);
				}
				#endregion OperStates

				//Заливаем белым
				if (!Convert.ToBoolean(dr.Cells["ExchangeError"].Value)
					&& string.IsNullOrEmpty(Convert.ToString(dr.Cells["OperState"].Value))
					&& Convert.ToInt32(dr.Cells["ComputerId"].Value) > 0)
				{
					dr.Cells["ShopName"].Style.BackColor =
					dr.Cells["LastSuccessfulReceive"].Style.BackColor =
					dr.Cells["LastSuccessfulUpload"].Style.BackColor =
					dr.Cells["OperationTypeRu"].Style.BackColor =
					dr.Cells["OperationCreationDate"].Style.BackColor =
					dr.Cells["OperState"].Style.BackColor = Color.White;
				}
			}
		}

		private void DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			DataGridView dgv = sender as DataGridView;
			if (dgv == null)
				return;

			if (e.ColumnIndex == 0 && e.RowIndex > -1)
			{
				if (Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == 1)
				{
					foreach (DataGridViewCell cell in dgv.Rows[e.RowIndex].Cells)
					{
						cell.Style.Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold);
					}
				}
				else
				{
					foreach (DataGridViewCell cell in dgv.Rows[e.RowIndex].Cells)
					{
						cell.Style.Font = new Font(SystemFonts.DefaultFont, FontStyle.Regular);
					}
				}
			}
		}

		private void UpdateTasksList()
		{
			this.lbTasks.Items.Clear();
			var tasks = MQueryCommand.SelectShedulerTasks();
			foreach (var task in tasks)
			{
				this.lbTasks.Items.Add(task);
			}

			if (this.lbTasks.Items.Count > 0)
			{
				this.lbTasks.SelectedIndex = 0;
			}
		}

		private void SetDefaultControlValues()
		{
			this.edTaskName.Text = string.Empty;
			this.cmbTaskModes.SelectedIndex = 0;
			this.pckrStartDateTime.Value = DateTime.Now.AddHours(1);
			this.chbEnable.Checked = false;

			// Очистка списка компьютеров
			DataGridViewRow[] rows = rgvComputersForSheduler.dataGridView.Rows.OfType<DataGridViewRow>().ToArray();
			foreach (DataGridViewRow row in rows)
			{
				row.Cells["Selected"].Value = false;
			}
		}

		private async void ScheduleForm_Load(object sender, EventArgs e)
		{
			_isFormLoading = true;
			try
			{
				pckrStartDateTime.Format = DateTimePickerFormat.Custom;
				pckrStartDateTime.CustomFormat = "Дата: dd/MM/yyyy   время: HH:mm:ss";

				this.lbTasks.DisplayMember = "Name";
				this.lbTasks.ValueMember = "Id";

				var allOperationsTypes = MQueryCommand.SelectAllOperationsTypes();
				foreach (var operation in allOperationsTypes)
				{
					this.stepsTree1.Operations.Add(operation.Id, operation.NameRus);
				}

				await InitComputersShedulerGridAsync();
				
				this.UpdateTasksList();
			}
			finally
			{
				_isFormLoading = false;
			}
		}

		private void lbTasks_SelectedIndexChanged(object sender, EventArgs e)
		{
			stepsTree1.stepTree.Visible = false;

			var selectedTaskItem = (ShedulerTask)lbTasks.SelectedItem;
			if (selectedTaskItem == null)
			{
				this.stepsTree1.ClearTree();
				return;
			}

			DateTime actualDateTime = Constants.DEFAULT_DATE_TIME;
			if (selectedTaskItem.NextStartTime > Constants.DEFAULT_DATE_TIME)
			{
				actualDateTime = selectedTaskItem.NextStartTime;
			}
			else if (selectedTaskItem.LastStartTime.HasValue && selectedTaskItem.LastStartTime.Value > Constants.DEFAULT_DATE_TIME)
			{
				actualDateTime = selectedTaskItem.LastStartTime.Value;
			}
			else if (selectedTaskItem.StartTime > Constants.DEFAULT_DATE_TIME)
			{
				actualDateTime = selectedTaskItem.StartTime;
			}

			this.edTaskName.Text = selectedTaskItem.Name;
			this.cmbTaskModes.SelectedIndex = selectedTaskItem.Mode;
			this.pckrStartDateTime.Value = actualDateTime.Add(_utcOffset);
			this.chbEnable.Checked = selectedTaskItem.Enabled ?? false;

			// Заполнение списка компьютеров
			DataGridViewRow[] rows = rgvComputersForSheduler.dataGridView.Rows.OfType<DataGridViewRow>().ToArray();
			foreach (DataGridViewRow row in rows)
			{
				row.Cells["Selected"].Value = false;
			}
			foreach (DataGridViewRow row in rows)
			{
				foreach (var currentComputer in selectedTaskItem.ShedulerTasksComputersLists)
				{
					if (currentComputer.ComputerId == Convert.ToInt32(row.Cells["ComputerId"].Value))
					{
						row.Cells["Selected"].Value = true;
						continue;
					}
				}
			}

			this.stepsTree1.ClearTree();

			var currentTaskSteps = MQueryCommand.GetAllStepsParentChild(selectedTaskItem.Id).OrderBy(x => x.ParentId).ToList();

			stepsTree1.PopulateA(currentTaskSteps);
			this.stepsTree1.ExpandAll();

			stepsTree1.stepTree.Visible = true;
		}

		private void DeleteTask()
		{
			this.toolStripButtonAddTask.Enabled =
			this.toolStripButtonRemoveTask.Enabled =
			lbTasks.Enabled = false;
			var selectedTaskItem = (ShedulerTask)lbTasks.SelectedItem;
			if (selectedTaskItem == null)
			{
				MessageBox.Show("Не выбрано ни одного пункта", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (MessageBox.Show($"Удалить задачу '{selectedTaskItem.Name}'?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return;
			}

			// Удаление задачи
			var result = MQueryCommand.TryDeleteTask(selectedTaskItem.Id);
			if (result.IsComplete)
			{
				this.UpdateTasksList();
				if (this.lbTasks.Items.Count > 0)
				{
					this.lbTasks.SelectedIndex = this.lbTasks.Items.Count - 1;
				}
				MessageBox.Show($"Задача успешно удалена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show($"Ошибка удаления задачи{Environment.NewLine}{result.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			this.toolStripButtonAddTask.Enabled =
			this.toolStripButtonRemoveTask.Enabled =
			lbTasks.Enabled = true;
		}

		private void deleteTaskToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.DeleteTask();
		}

		private void lbTasks_MouseDown(object sender, MouseEventArgs e)
		{
			this.lbTasks.SelectedIndex = this.lbTasks.IndexFromPoint(e.X, e.Y);
		}

		private void ClearSteps(int taskId)
		{
			MQueryCommand.TryClearSteps(taskId);
		}

		private void AddEditTask()
		{
			if (string.IsNullOrEmpty(this.edTaskName.Text))
			{
				MessageBox.Show("Значение поля 'Название задачи' не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.ActiveControl = this.edTaskName;
				return;
			}
			List<int> selectedComputers = rgvComputersForSheduler.SelectedValues.Select(s => (int)s).ToList();

			// Добавление таска
			this.lbTasks.DisplayMember = "Name";
			this.lbTasks.ValueMember = "Id";

			var schedulerTask = new ShedulerTask
			{
				Name = this._newTaskName,
				//StartTime = DateTime.Now.AddHours(1)
			};

			// Заполнение списка компьютеров
			var computersList = selectedComputers.Select(x => new ShedulerTasksComputersList
			{
				ComputerId = x
			}).ToList();

			if (this._isAddTaskMode)
			{
				var addedIndex = this.lbTasks.Items.Add(schedulerTask);
				// Добавление новой задачи
				var addTaskResult = MQueryCommand.TryInsertNewTask(this.edTaskName.Text,
																	 cmbTaskModes.SelectedIndex,
																	 this.pckrStartDateTime.Value.Add(-_utcOffset),
																	 computersList,
																	 this.chbEnable.Checked);
				if (!addTaskResult.IsComplete)
				{
					MessageBox.Show($"Ошибка добавления новой задачи{Environment.NewLine}{addTaskResult.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// Добавление шагов
				var stepsWereAdded = stepsTree1.AddSteps(addTaskResult.ReturnedId);
				if (!stepsWereAdded)
				{
					using (EntityDb context = new EntityDb())
					{
						try
						{
							var task = context.ShedulerTasks.First(x => x.Id == addTaskResult.ReturnedId);
							context.ShedulerTasks.Remove(task);
							context.SaveChanges();
						}
						catch (Exception ex)
						{
							MLogger.Error(ex.ToString());
							MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
					}

					this.lbTasks.Items.Remove(schedulerTask);

					MessageBox.Show("Невозможно добавление новой задачи без указания операций. Необходимо указать как минимум одну операцию.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

					return;
				}

				// Перезагрузка списка задач
				this.UpdateTasksList();
				this.lbTasks.SelectedIndex = addedIndex;
				this._isAddTaskMode = false;
				MessageBox.Show($"Задача '{this.edTaskName.Text}' была успешно добавлена", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				// Редактирование выбранной задачи
				var selectedTaskItem = (ShedulerTask)lbTasks.SelectedItem;
				if (selectedTaskItem == null)
				{
					return;
				}

				var updateResult = MQueryCommand.TryUpdateTask(selectedTaskItem.Id,
																	 this.chbEnable.Checked,
																	 this.edTaskName.Text,
																	 null,
																	 cmbTaskModes.SelectedIndex,
																	 this.pckrStartDateTime.Value.Add(-_utcOffset),
																	 computersList);
				if (!updateResult.IsComplete)
				{
					MessageBox.Show($"Ошибка при обновлении данных{Environment.NewLine}{updateResult.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// Очистка списка шагов
				//if (!this.stepsTree1.IsEmpty())
				//{
				//	this.ClearSteps(selectedTaskItem.Id);
				//}

				//Удаление шагов
				var stepsWereDeleted = MQueryCommand.TryDeleteSteps(stepsTree1.StepsIdListToDelete);
				if (!stepsWereDeleted.IsComplete)
				{
					MessageBox.Show(stepsWereDeleted.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				// Добавление шагов
				var stepsWereEdited = stepsTree1.AddSteps(selectedTaskItem.Id);
				if (!stepsWereEdited)
				{
					MessageBox.Show("Невозможно сохраниение изменений для дерева операций. Требуется добавить хотя бы одну операцию.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				var currentSelectedIndex = this.lbTasks.SelectedIndex;
				this.UpdateTasksList();
				this.lbTasks.SelectedIndex = currentSelectedIndex;

				MessageBox.Show($"Изменения по задаче '{this.edTaskName.Text}' были успешно сохранены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			this.toolStripButtonAddTask.Enabled =
			this.toolStripButtonRemoveTask.Enabled =
			this.lbTasks.Enabled = true;
			this.labelEditing.Visible = false;
			this.btnCancel.Visible = false;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				btnSave.Enabled = false;
				this.AddEditTask();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				btnSave.Enabled = true;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.toolStripButtonAddTask.Enabled =
			this.toolStripButtonRemoveTask.Enabled =
			this.lbTasks.Enabled = true;
			this.labelEditing.Visible = false;
			this.SetDefaultControlValues();
			this.stepsTree1.ClearTree();
			stepsTree1.stepTree.Visible = false;
		}

		private void toolStripButtonAddTask_Click(object sender, EventArgs e)
		{
			this.PrepareToAddTask();
		}

		private void toolStripButtonRemoveTask_Click(object sender, EventArgs e)
		{
			this.DeleteTask();
		}

		private async void btnRefreshGrid_Click(object sender, EventArgs e)
		{
			var button = sender as Button;

			button.Enabled = false;
			rgvComputersForSheduler.ShowLoading(true);
			try
			{
				await rgvComputersForSheduler.RefreshDataSourceAsync(await MQueryCommand.SelectShopComputersServerViewGridAsync());
			}
			finally
			{
				button.Enabled = true;
				rgvComputersForSheduler.ShowLoading(false);
			}
		}
	}

	// Для справки
	//public enum ShedulerTaskModes
	//{
	//    Once = 0,
	//    Daily = 1,
	//    Weekly = 2,
	//    Monthly = 3
	//}
}
