using MagicUpdaterMonitor.Base;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MagicUpdater.DL;
using MagicUpdater.DL.DB;
using MagicUpdater.DL.Helpers;
using MagicUpdaterMonitor.Helpers;
using MagicUpdater.DL.Models;

namespace MagicUpdaterMonitor.Forms
{
	public partial class NewEditShedulerTaskForm : BaseForm
	{
		private const int GRIDS_REFRESH_TIMEOUT = 5000;

		private int _taskId;
		private ShedulerTask _shedulerTask;
		private System.Threading.Timer _refreshComputersTimer;
		private System.Threading.Timer _refreshStepsTimer;
		private bool _dontCloseForm = false;

		public NewEditShedulerTaskForm(int taskId = 0)
		{
			_taskId = taskId;
			InitializeComponent();

			if (_taskId > 0)
			{
				_shedulerTask = MQueryCommand.SelectShedulerTasks().FirstOrDefault(f => f.Id == _taskId);
				if (_shedulerTask == null)
				{
					MessageBox.Show("Не найден [ShedulerTask]", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.Dispose();
					return;
				}
			}
			else
			{
				_shedulerTask = new ShedulerTask();
			}

			InitControls();
		}

		private void InitControls()
		{
			InitGrids();

			tabControl1.Appearance = TabAppearance.FlatButtons;
			tabControl1.ItemSize = new Size(0, 1);
			tabControl1.SizeMode = TabSizeMode.Fixed;

			cbOccurs.DisplayMember = "NameRus";
			cbOccurs.ValueMember = "Id";
			cbOccurs.DataSource = MQueryCommand.SelectShedulerModes().Where(w => w.Id != 0).ToList();

			var stepsGridList = rgvSteps.DataSource?.OfType<ViewShedulerStepModel>().ToList();

			cbStartStep.DisplayMember = "nameVisCount";
			cbStartStep.ValueMember = "Id";
			cbStartStep.DataSource = MQueryCommand.SelectShedulerStepsForGrid(_taskId);
			if (_shedulerTask.FirstStepId.HasValue)
			{
				cbStartStep.SelectedValue = _shedulerTask.FirstStepId.Value;
			}

			if (_taskId > 0)
			{
				tbTaskName.Text = _shedulerTask.Name;

				chbTaskEnabled.Checked = _shedulerTask.Enabled ?? false;
				rtbTaskDescription.Text = _shedulerTask.Description ?? "";

				cbSheduleType.SelectedIndex = _shedulerTask.Mode == 0 ? 0 : 1;

				if (_shedulerTask.FirstStepId.HasValue)
				{
					cbStartStep.SelectedValue = _shedulerTask.FirstStepId.Value;
				}

				if (_shedulerTask.Mode != 0)
				{
					cbOccurs.SelectedValue = _shedulerTask.Mode;
					dtbDateFrequency.Value = dtbTimeFrequency.Value = _shedulerTask.NextStartTime.ToLocalTime();
					nudRecurs.Value = Convert.ToDecimal(_shedulerTask.RepeatValue);
				}
				else
				{
					dtpDateOnce.Value = dtpTimeOnce.Value = _shedulerTask.NextStartTime.ToLocalTime();

				}
			}

			CreateDescription();
			this.TopMost = true;
			this.TopMost = false;
		}

		private async void InitGrids()
		{
			this.Opacity = 0;
			try
			{
				tabControl1.SelectedTab = tabPageComputersList;
				rgvComputersList.CbSelectAllVisible = true;
				int i = 3;
				while (i > 0)
				{
					try
					{
						i--;
						await InitComputersListGridAsync();
						break;
					}
					catch (Exception ex)
					{
						if (i == 0)
						{
							MLogger.Error(ex.ToString());
							MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
				_refreshComputersTimer = new System.Threading.Timer(RefreshComputersTimerCallback, null, GRIDS_REFRESH_TIMEOUT, System.Threading.Timeout.Infinite);

				tabControl1.SelectedTab = tabPageGeneral;
				await InitStepsGridAsync();
				//_refreshStepsTimer = new System.Threading.Timer(RefreshStepsTimerCallback, null, GRIDS_REFRESH_TIMEOUT, System.Threading.Timeout.Infinite);
			}
			finally
			{
				this.Opacity = 1;
			}
		}

		private async Task InitStepsGridAsync()
		{
			rgvSteps.KeyField = "Id";
			rgvSteps.MappingColumns = Mapping.StepsGridMapping;
			rgvSteps.DataSource = await MQueryCommand.SelectShedulerStepsForGridAsync(_taskId);
			rgvSteps.dataGridView.RowHeadersVisible = false;

			rgvSteps.dataGridView.Columns.OfType<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
			if (rgvSteps.dataGridView.Rows.Count > 0)
			{
				rgvSteps.dataGridView.Rows[0].Selected = true;
			}

			rgvSteps.dataGridView.CellDoubleClick += (sender, e) =>
			{
				btnEditStep.PerformClick();
			};
		}



		private async Task InitComputersListGridAsync()
		{

			rgvComputersList.KeyField = "ComputerId";

			rgvComputersList.PaintCells += (sender) =>
			{
				#region PaintCells
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
				#endregion
			};
			rgvComputersList.dataGridView.CellPainting += (sender, e) =>
			{
				#region CellPainting
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
				#endregion
			};


			rgvComputersList.MappingColumns = Mapping.ComputersGridColMap;
			rgvComputersList.DataSource = await MQueryCommand.SelectShopComputersServerViewGridAsync(); 
			rgvComputersList.BaseFilter = "IsClosed = false";
			rgvComputersList.dataGridView.RowHeadersVisible = false;
			//rgvComputersList.HideColumns("Is1CServer", "IsMainCashbox", "IsClosed");

			rgvComputersList.HideColumns("Is1CServer"
										, "IsMainCashbox"
										, "IsClosed"
										, "External_IP"
										, "LastSuccessfulUpload"
										, "LastSuccessfulReceive"
										, "OperationTypeRu"
										, "OperState"
										, "LastErrorString"
										, "MagicUpdaterVersion"
										, "OperationCreationDate");

			rgvComputersList.dataGridView.Columns["Selected"].Width = 50;
			rgvComputersList.dataGridView.Columns["IsOnBitmap"].Width = 50;

			#region rgvComputersListContextMenuAdd
			//Показать/скрыть дополнительные поля
			const string SHOW_ADDITIONAL_COLUMNS = "Показать дополнительные столбцы";
			const string HIDE_ADDITIONAL_COLUMNS = "Скрыть дополнительные столбцы";
			string[] additionalColumns = new string[]
			{
					"ComputerId",
					"LastError",
					"External_IP",
					"ComputerName"
			};
			rgvComputersList.HideColumns(additionalColumns);
			var miShowHideColumns = new ToolStripMenuItem();
			miShowHideColumns.Name = "miShowHideColumns";
			miShowHideColumns.Text = SHOW_ADDITIONAL_COLUMNS;
			miShowHideColumns.Image = Images.plus;
			miShowHideColumns.Click += (sender, e) =>
			{
				if (miShowHideColumns.Text == SHOW_ADDITIONAL_COLUMNS)
				{
					miShowHideColumns.Text = HIDE_ADDITIONAL_COLUMNS;
					rgvComputersList.ShowColumns(additionalColumns);
					miShowHideColumns.Image = Images.minus;
				}
				else
				{
					miShowHideColumns.Text = SHOW_ADDITIONAL_COLUMNS;
					rgvComputersList.HideColumns(additionalColumns);
					miShowHideColumns.Image = Images.plus;
				}
			};
			rgvComputersList.AddMenuItem(miShowHideColumns);
			#endregion rgvComputersListContextMenuAdd

			rgvComputersList.CbSelectAllVisible = true;

			if (_taskId > 0)
			{
				List<int> computersId = _shedulerTask.ShedulerTasksComputersLists.Select(s => s.ComputerId).ToList();

				if (rgvComputersList.dataGridView.Columns["Selected"].Visible)
				{
					foreach (DataGridViewRow row in rgvComputersList.dataGridView.Rows)
					{
						if (computersId.Contains(Convert.ToInt32(row.Cells[rgvComputersList.KeyField].Value)))
						{
							row.Cells["Selected"].Value = true;
						}
					}
				}
			}
		}


		/*
		Каждый день
		Каждую неделю
		Каждый месяц
		После кол-ва часов
		После кол-ва минут
		*/

		private void CreateDescription()
		{
			rtbSheduleDescription.Clear();

			if (cbSheduleType.SelectedIndex == 0)
			{
				rtbSheduleDescription.Text = $"Выполняется один раз {dtpDateOnce.Value.Date.ToShortDateString()}. В {dtpTimeOnce.Value.ToShortTimeString()}";
			}
			else
			{
				switch (cbOccurs.SelectedIndex)
				{
					case 0:
					case 1:
					case 2:
						rtbSheduleDescription.Text = $"Выполняется {cbOccurs.Text}. Начало выполнения {dtbDateFrequency.Value.Date.ToShortDateString()} в {dtbTimeFrequency.Value.ToShortTimeString()}";
						break;
					case 3:
						rtbSheduleDescription.Text = $"Выполняется каджые {nudRecurs.Value} часа(ов). Начало выполнения {dtbDateFrequency.Value.Date.ToShortDateString()} в {dtbTimeFrequency.Value.ToShortTimeString()}";
						break;
					case 4:
						rtbSheduleDescription.Text = $"Выполняется каджые {nudRecurs.Value} минут(ы). Начало выполнения {dtbDateFrequency.Value.Date.ToShortDateString()} в {dtbTimeFrequency.Value.ToShortTimeString()}";
						break;
				}
			}
		}

		private void lvPages_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selectedItem = lvPages.Items.OfType<ListViewItem>().FirstOrDefault(f => f.Selected);
			if (selectedItem == null)
			{
				return;
			}
			switch (selectedItem.Text)
			{
				case "Общие":
					tabControl1.SelectedTab = tabPageGeneral;
					break;
				case "Шаги":
					tabControl1.SelectedTab = tabPageSteps;
					break;
				case "Расписание":
					tabControl1.SelectedTab = tabPageSchedules;
					break;
				case "Список компьютеров":
					tabControl1.SelectedTab = tabPageComputersList;
					break;
			}
		}

		private void btnNewStep_Click(object sender, EventArgs e)
		{
			btnNewStep.Enabled = false;
			try
			{
				object cbStartStepSelectedValue = cbStartStep.SelectedValue;
				var stepsGridList = rgvSteps.DataSource?.OfType<ViewShedulerStepModel>().ToList();
				var newEditShedulerStepForm = new NewEditShedulerStepForm(stepsGridList);
				newEditShedulerStepForm.ShowDialog();

				if (newEditShedulerStepForm.OutViewShedulerStepModel == null)
				{
					return;
				}

				int newStepId = -1;
				long newStepStep = 1;

				if (stepsGridList == null)
				{
					stepsGridList = new List<ViewShedulerStepModel>();
				}

				if (stepsGridList.Count > 0)
				{
					var lastAddedStep = stepsGridList.OrderBy(o => o.Id).FirstOrDefault(f => f.Id < 0);
					newStepId = lastAddedStep == null ? -1 : lastAddedStep.Id - 1;

					var maxStepStep = stepsGridList.OrderByDescending(o => o.Step).FirstOrDefault();
					newStepStep = maxStepStep.Step.HasValue ? maxStepStep.Step.Value + 1 : 1;
				}

				var newViewShedulerStepModel = new ViewShedulerStepModel
				{
					Id = newStepId,
					Step = newStepStep,
					OnOperationCompleteStep = newEditShedulerStepForm.OutViewShedulerStepModel.OnOperationCompleteStep,
					OnOperationErrorStep = newEditShedulerStepForm.OutViewShedulerStepModel.OnOperationErrorStep,
					OperationType = newEditShedulerStepForm.OutViewShedulerStepModel.OperationType,
					OperationAttributes = newEditShedulerStepForm.OutViewShedulerStepModel.OperationAttributes,
					RepeatCount = newEditShedulerStepForm.OutViewShedulerStepModel.RepeatCount,
					RepeatTimeout = newEditShedulerStepForm.OutViewShedulerStepModel.RepeatTimeout,
					OrderId = newEditShedulerStepForm.OutViewShedulerStepModel.OrderId,
					TaskId = newEditShedulerStepForm.OutViewShedulerStepModel.TaskId,
					OperationCheckIntervalMs = newEditShedulerStepForm.OutViewShedulerStepModel.OperationCheckIntervalMs,
					nameVis = newEditShedulerStepForm.OutViewShedulerStepModel.nameVis,
					nameVisCountCompleteStep = newEditShedulerStepForm.OutViewShedulerStepModel.nameVisCountCompleteStep,
					nameVisCountErrorStep = newEditShedulerStepForm.OutViewShedulerStepModel.nameVisCountErrorStep,
					nameVisCount = $"[{newStepStep}] {newEditShedulerStepForm.OutViewShedulerStepModel.nameVis}"
				};

				stepsGridList.Add(newViewShedulerStepModel);

				cbStartStep.DataSource = stepsGridList.ToArray();
				rgvSteps.DataSource = stepsGridList.ToArray();
				if (cbStartStepSelectedValue != null)
				{
					cbStartStep.SelectedValue = cbStartStepSelectedValue;
				}
			}
			finally
			{
				btnNewStep.Enabled = true;
			}
		}

		private void btnEditStep_Click(object sender, EventArgs e)
		{
			btnEditStep.Enabled = false;
			try
			{
				DataGridViewRow selectedRow = rgvSteps.dataGridView.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
				if (selectedRow == null)
				{
					selectedRow = rgvSteps.dataGridView.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
					if (selectedRow == null)
					{
						return;
					}
				}

				int stepId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
				var viewShedulerStepModel = rgvSteps.DataSource.OfType<ViewShedulerStepModel>().First(f => f.Id == stepId);
				var stepsGridList = rgvSteps.DataSource.OfType<ViewShedulerStepModel>().ToList();
				var newEditShedulerStepForm = new NewEditShedulerStepForm(stepsGridList, viewShedulerStepModel);
				newEditShedulerStepForm.ShowDialog();
				if (newEditShedulerStepForm.OutViewShedulerStepModel == null)
				{
					return;
				}
				var editingStep = stepsGridList.First(f => f.Id == stepId);

				editingStep.OnOperationCompleteStep = newEditShedulerStepForm.OutViewShedulerStepModel.OnOperationCompleteStep;
				editingStep.OnOperationErrorStep = newEditShedulerStepForm.OutViewShedulerStepModel.OnOperationErrorStep;
				editingStep.nameVisCountCompleteStep = newEditShedulerStepForm.OutViewShedulerStepModel.nameVisCountCompleteStep;
				editingStep.nameVisCountErrorStep = newEditShedulerStepForm.OutViewShedulerStepModel.nameVisCountErrorStep;

				stepsGridList.Where(w => w.OnOperationCompleteStep == editingStep.Id)
					.ForEach(f =>
					{
						f.nameVisCountCompleteStep = $"[{editingStep.Step}] {editingStep.nameVis}";
					});

				stepsGridList.Where(w => w.OnOperationErrorStep == editingStep.Id)
					.ForEach(f =>
					{
						f.nameVisCountErrorStep = $"[{editingStep.Step}] {editingStep.nameVis}";
					});

				rgvSteps.DataSource = stepsGridList.ToArray();
			}
			finally
			{
				btnEditStep.Enabled = true;
			}
		}

		private void cbSheduleType_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (cbSheduleType.SelectedIndex)
			{
				case 0:
					label8.Enabled = tableLayoutPanel11.Enabled = true;
					tableLayoutPanel13.Enabled = label12.Enabled = false;
					break;
				case 1:
					label8.Enabled = tableLayoutPanel11.Enabled = false;
					tableLayoutPanel13.Enabled = label12.Enabled = true;
					break;
			}
			CreateDescription();
		}

		private void cbOccurs_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (Convert.ToInt32(cbOccurs.SelectedValue))
			{
				case 1:
					label15.Enabled = nudRecurs.Enabled = lbPeriod.Visible = false;
					break;
				case 2:
					label15.Enabled = nudRecurs.Enabled = lbPeriod.Visible = false;
					break;
				case 3:
					label15.Enabled = nudRecurs.Enabled = lbPeriod.Visible = false;
					break;
				case 4:
					label15.Enabled = nudRecurs.Enabled = lbPeriod.Visible = true;
					lbPeriod.Text = "Часа(ов)";
					break;
				case 5:
					label15.Enabled = nudRecurs.Enabled = lbPeriod.Visible = true;
					lbPeriod.Text = "Минут(ы)";
					break;
			}
			CreateDescription();
		}

		private async void RefreshComputersTimerCallback(object state)
		{
			try
			{
				await rgvComputersList.RefreshDataSourceAsync(await MQueryCommand.SelectShopComputersServerViewGridAsync());
			}
			catch (Exception ex)
			{
				MLogger.Error($"Ошибка обновления таблицы rgvComputersList в таймере RefreshComputersTimer. Original: {ex.ToString()}");
			}
			finally
			{
				_refreshComputersTimer.Change(GRIDS_REFRESH_TIMEOUT, System.Threading.Timeout.Infinite);
			}
		}

		private async void RefreshStepsTimerCallback(object state)
		{
			try
			{
				await rgvSteps.RefreshDataSourceAsync(await MQueryCommand.SelectShedulerStepsForGridAsync(_taskId));
			}
			catch (Exception ex)
			{
				MLogger.Error($"Ошибка обновления таблицы rgvSteps в таймере RefreshComputersTimer. Original: {ex.ToString()}");
			}
			finally
			{
				_refreshStepsTimer.Change(GRIDS_REFRESH_TIMEOUT, System.Threading.Timeout.Infinite);
			}
		}

		private void dtpDateOnce_ValueChanged(object sender, EventArgs e)
		{
			CreateDescription();
		}

		private void dtpTimeOnce_ValueChanged(object sender, EventArgs e)
		{
			CreateDescription();
		}

		private void nudRecurs_ValueChanged(object sender, EventArgs e)
		{
			CreateDescription();
		}

		private void dtbDateFrequency_ValueChanged(object sender, EventArgs e)
		{
			CreateDescription();
		}

		private void dtbTimeFrequency_ValueChanged(object sender, EventArgs e)
		{
			CreateDescription();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			btnOk.Enabled = false;
			try
			{
				DateTime nextStartTime = (cbSheduleType.SelectedIndex == 0 ?
					dtpDateOnce.Value.Date.Add(dtpTimeOnce.Value.TimeOfDay) :
					dtbDateFrequency.Value.Date.Add(dtbTimeFrequency.Value.TimeOfDay)).ToUniversalTime();

				#region InputChecks
				if (string.IsNullOrEmpty(tbTaskName.Text))
				{
					MessageBox.Show($"Отсутствует имя задания.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					_dontCloseForm = true;
					return;
				}

				if (MQueryCommand.CheckTaskExistsByName(tbTaskName.Text, new int[1] { _taskId }))
				{
					MessageBox.Show($"Задание с именем [{tbTaskName.Text}] уже существует, введите другое имя.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					_dontCloseForm = true;
					return;
				}

				if (rgvSteps.dataGridView.Rows.Count == 0)
				{
					MessageBox.Show($"Не создано ни одного шага.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					_dontCloseForm = true;
					return;
				}

				if (cbStartStep.SelectedItem == null)
				{
					MessageBox.Show($"Не выбран стартовый шаг.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					_dontCloseForm = true;
					return;
				}

				if (cbSheduleType.SelectedItem == null)
				{
					MessageBox.Show($"Не выбран тип расписания.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					_dontCloseForm = true;
					return;
				}

				if (rgvComputersList.SelectedValues.Count == 0)
				{
					MessageBox.Show($"Нет выбранных компьютеров в списке компьютеров.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					_dontCloseForm = true;
					return;
				}

				foreach (DataGridViewRow row in rgvSteps.dataGridView.Rows)
				{
					if (Convert.ToInt32(row.Cells["Step"].Value) == 1 &&
						Convert.ToInt32(row.Cells["Id"].Value) != Convert.ToInt32(cbStartStep.SelectedValue))
					{
						if (MessageBox.Show($"Не все шаги будут выполнены, продолжить?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
						{
							_dontCloseForm = true;
							return;
						}
						else
						{
							break;
						}
					}

					if (Convert.ToInt32(row.Cells["Step"].Value) > 1 &&
						!rgvSteps.dataGridView.Rows.OfType<DataGridViewRow>().Any(a =>
						Convert.ToInt32(a.Cells["OnOperationCompleteStep"].Value) == Convert.ToInt32(row.Cells["Id"].Value) ||
						Convert.ToInt32(a.Cells["OnOperationErrorStep"].Value) == Convert.ToInt32(row.Cells["Id"].Value)))
					{
						if (MessageBox.Show($"Не все шаги будут выполнены, продолжить?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
						{
							_dontCloseForm = true;
							return;
						}
						else
						{
							break;
						}
					}
				}

				if (nextStartTime <= DateTime.UtcNow)
				{
					MessageBox.Show($"Дата следующего выполнения должна быть больше текущей даты!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					_dontCloseForm = true;
					return;
				}

				if (nextStartTime > DateTime.UtcNow && nextStartTime.AddMinutes(-10) <= DateTime.UtcNow)
				{
					if (MessageBox.Show($"Задание будет выполнено менее чем через 10 минут. Продолжить?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					{
						_dontCloseForm = true;
						return;
					}
				}
				#endregion

				var computerIdList = rgvComputersList.SelectedValues.Select(s => Convert.ToInt32(s));

				var computerList = computerIdList.Select(s => new ShedulerTasksComputersList
				{
					ComputerId = s
				});

				computerList.ForEach(f =>
				{
					if (!_shedulerTask.ShedulerTasksComputersLists.Select(s => s.ComputerId).Contains(f.ComputerId))
					{
						_shedulerTask.ShedulerTasksComputersLists.Add(f);
					}
				});

				var taskComputersList = _shedulerTask.ShedulerTasksComputersLists.ToArray();

				taskComputersList.ForEach(f =>
				{
					if (!computerList.Select(s => s.ComputerId).Contains(f.ComputerId))
					{
						_shedulerTask.ShedulerTasksComputersLists.Remove(f);
					}
				});

				_shedulerTask.Enabled = chbTaskEnabled.Checked;
				_shedulerTask.Name = tbTaskName.Text;
				_shedulerTask.Description = rtbTaskDescription.Text;
				_shedulerTask.Mode = cbSheduleType.SelectedIndex == 0 ? 0 : Convert.ToInt32(cbOccurs.SelectedValue);

				_shedulerTask.StartTime = _shedulerTask.NextStartTime = nextStartTime;

				_shedulerTask.RepeatValue = Convert.ToInt32(nudRecurs.Value);
				_shedulerTask.FirstStepId = Convert.ToInt32(cbStartStep.SelectedValue);
				_shedulerTask.CreatedUserLogin = Environment.UserName;
				_shedulerTask.CreationDateUtc = DateTime.UtcNow;


				//TODO

				var stepsGridList = rgvSteps.DataSource.OfType<ViewShedulerStepModel>().ToList();

				var stepsForModify = stepsGridList.Select(s => new ShedulerStep
				{
					Id = s.Id,
					OnOperationCompleteStep = s.OnOperationCompleteStep,
					OnOperationErrorStep = s.OnOperationErrorStep,
					OperationAttributes = s.OperationAttributes,
					OperationCheckIntervalMs = s.OperationCheckIntervalMs,
					OperationType = s.OperationType,
					OrderId = s.OrderId,
					RepeatCount = s.RepeatCount,
					RepeatTimeout = s.RepeatTimeout,
					TaskId = _taskId
				});


				var tryDeleteInsertUpdateTaskFromFormRes = MQueryCommand.TryDeleteInsertUpdateTaskFromForm(_shedulerTask, stepsForModify);
				if (!tryDeleteInsertUpdateTaskFromFormRes.IsComplete)
				{
					MessageBox.Show(tryDeleteInsertUpdateTaskFromFormRes.Message);
				}
			}
			finally
			{
				btnOk.Enabled = true;
			}
		}

		private void btnDeleteStep_Click(object sender, EventArgs e)
		{
			object cbStartStepSelectedValue = cbStartStep.SelectedValue;
			DataGridViewRow selectedRow = rgvSteps.dataGridView.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
			if (selectedRow == null)
			{
				selectedRow = rgvSteps.dataGridView.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
				if (selectedRow == null)
				{
					return;
				}
			}

			int selectedKey = Convert.ToInt32(selectedRow.Cells["Id"].Value);

			int selectedIndex = 0;
			if (selectedRow != null)
			{
				selectedIndex = selectedRow.Index;
			}

			var stepsGridList = rgvSteps.DataSource.OfType<ViewShedulerStepModel>().ToList();

			stepsGridList.OfType<ViewShedulerStepModel>().Where(w => w.OnOperationCompleteStep == selectedKey).ForEach(f =>
			{
				f.OnOperationCompleteStep = 0;
				f.nameVisCountCompleteStep = "Завершить";
			});
			stepsGridList.OfType<ViewShedulerStepModel>().Where(w => w.OnOperationErrorStep == selectedKey).ForEach(f =>
			{
				f.OnOperationErrorStep = 0;
				f.nameVisCountErrorStep = "Завершить";
			});

			stepsGridList = stepsGridList.Where(w => w.Id != selectedKey).ToList();
			rgvSteps.DataSource = stepsGridList.ToArray();

			if (selectedIndex > 1 && rgvSteps.dataGridView.Rows.Count > 0)
			{
				rgvSteps.dataGridView.Rows[selectedIndex - 1].Selected = true;
			}

			cbStartStep.DataSource = stepsGridList.ToArray();
			if (cbStartStepSelectedValue != null)
			{
				cbStartStep.SelectedValue = cbStartStepSelectedValue;
			}
		}

		private void NewEditShedulerTaskForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = _dontCloseForm;
			_dontCloseForm = false;
		}
	}
}
