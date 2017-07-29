using MagicUpdater.DL;
using MagicUpdater.DL.DB;
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
	public partial class NewEditShedulerPluginTaskForm : BaseForm
	{
		private int _pluginTaskId;
		private ShedulerPluginTask _shedulerPluginTask;
		private bool _dontCloseForm = false;

		public NewEditShedulerPluginTaskForm(int pluginTaskId = 0)
		{
			_pluginTaskId = pluginTaskId;
			InitializeComponent();

			if (_pluginTaskId > 0)
			{
				_shedulerPluginTask = MQueryCommand.SelectShedulerPluginTasks().FirstOrDefault(f => f.Id == _pluginTaskId);
				if (_shedulerPluginTask == null)
				{
					MessageBox.Show("Не найден [ShedulerPluginTask]", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.Dispose();
					return;
				}
			}
			else
			{
				_shedulerPluginTask = new ShedulerPluginTask();
			}

			InitControls();
		}

		private void InitControls()
		{
			tabControl1.Appearance = TabAppearance.FlatButtons;
			tabControl1.ItemSize = new Size(0, 1);
			tabControl1.SizeMode = TabSizeMode.Fixed;

			cbOccurs.DisplayMember = "NameRus";
			cbOccurs.ValueMember = "Id";
			cbOccurs.DataSource = MQueryCommand.SelectShedulerModes().Where(w => w.Id != 0).ToList();

			if (_pluginTaskId > 0)
			{
				tbTaskName.Text = _shedulerPluginTask.Name;

				chbTaskEnabled.Checked = _shedulerPluginTask.Enabled ?? false;
				rtbTaskDescription.Text = _shedulerPluginTask.Description ?? "";

				cbSheduleType.SelectedIndex = _shedulerPluginTask.Mode == 0 ? 0 : 1;

				tbDllFileName.Text = _shedulerPluginTask.PluginFileName;

				if (_shedulerPluginTask.Mode != 0)
				{
					cbOccurs.SelectedValue = _shedulerPluginTask.Mode;
					dtbDateFrequency.Value = dtbTimeFrequency.Value = _shedulerPluginTask.NextStartTime.ToLocalTime();
					nudRecurs.Value = Convert.ToDecimal(_shedulerPluginTask.RepeatValue);
				}
				else
				{
					dtpDateOnce.Value = dtpTimeOnce.Value = _shedulerPluginTask.NextStartTime.ToLocalTime();
				}
			}

			CreateDescription();
			this.TopMost = true;
			this.TopMost = false;
		}

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
				case "Расписание":
					tabControl1.SelectedTab = tabPageSchedules;
					break;
				case "Плагин-операция":
					tabControl1.SelectedTab = tabPagePluginOperation;
					break;
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

		private void NewEditShedulerPluginTaskForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = _dontCloseForm;
			_dontCloseForm = false;
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

				if (MQueryCommand.CheckTaskExistsByName(tbTaskName.Text, new int[1] { _pluginTaskId }))
				{
					MessageBox.Show($"Задание с именем [{tbTaskName.Text}] уже существует, введите другое имя.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					_dontCloseForm = true;
					return;
				}

				if (cbSheduleType.SelectedItem == null)
				{
					MessageBox.Show($"Не выбран тип расписания.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					_dontCloseForm = true;
					return;
				}

				if (string.IsNullOrEmpty(tbDllFileName.Text))
				{
					MessageBox.Show($"Не указано имя файла плагин-операции [Имя dll - файла с расширением]", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					_dontCloseForm = true;
					return;
				}
				else if(!tbDllFileName.Text.Contains(".dll"))
				{
					MessageBox.Show($"Не указано расширение в имени файла для плагин-операции [.dll]", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					_dontCloseForm = true;
					return;
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

				_shedulerPluginTask.Enabled = chbTaskEnabled.Checked;
				_shedulerPluginTask.Name = tbTaskName.Text;
				_shedulerPluginTask.PluginFileName = tbDllFileName.Text;
				_shedulerPluginTask.Description = rtbTaskDescription.Text;
				_shedulerPluginTask.Mode = cbSheduleType.SelectedIndex == 0 ? 0 : Convert.ToInt32(cbOccurs.SelectedValue);

				_shedulerPluginTask.NextStartTime = nextStartTime;

				_shedulerPluginTask.RepeatValue = Convert.ToInt32(nudRecurs.Value);
				_shedulerPluginTask.CreatedUserLogin = Environment.UserName;
				_shedulerPluginTask.CreationDateUtc = DateTime.UtcNow;

				//TODO
				var tryDeleteInsertUpdateTaskFromFormRes = MQueryCommand.TryInsertUpdatePluginTaskFromForm(_shedulerPluginTask);
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
	}
}
