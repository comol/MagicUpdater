using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StepTreeControl
{
    public partial class StepPropertiesForm : Form
    {
        public Dictionary<int, string> Operations = new Dictionary<int, string>();
        public OperationItem SelectedOperationItem { get; set; }
        public StepsProperties StepsProperties { get; set; }
        private bool _isPositive { get; set; }
        public StepPropertiesForm(StepFormMode mode, StepsProperties properties, Dictionary<int, string> operations)
        {
            InitializeComponent();

            this.StepsProperties = properties;
            this.Operations = operations;

            switch (mode)
            {
                case StepFormMode.AddPositive:
                    this._isPositive = true;
                    this.Text = "Действие при положительном результате";
                    SetDefaultValues();
                    break;
                case StepFormMode.AddNegative:
                    this._isPositive = false;
                    this.Text = "Действие при отрицательном результате";
                    SetDefaultValues();
                    break;
                case StepFormMode.Edit:
                    if (this.StepsProperties == null)
                    {
                        SetDefaultValues();
                        break;
                    }

                    chbIs1cErrorEqualRuntimeError.Checked = this.StepsProperties.Is1cErrorEqualRuntimeError;
                    chbIsOperErrorEqualRuntimeError.Checked = this.StepsProperties.IsOperErrorEqualRuntimeError;

                    if (this.StepsProperties.WaitingForSuccessInterval < 0)
                    {
                        MessageBox.Show($"Неверное значение: {this.StepsProperties.WaitingForSuccessInterval}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var waitingForSuccessInterval = TimeSpan.FromSeconds(this.StepsProperties.WaitingForSuccessInterval);
                    this.pckrWaitingForSuccessInterval.Value = new DateTime(
                            DateTime.Now.Year,
                            DateTime.Now.Month,
                            DateTime.Now.Day,
                            waitingForSuccessInterval.Hours,
                            waitingForSuccessInterval.Minutes,
                            waitingForSuccessInterval.Seconds);

                    if (this.StepsProperties.RepeatingIntervalOn1cWaiting < 0)
                    {
                        MessageBox.Show($"Неверное значение: {this.StepsProperties.RepeatingIntervalOn1cWaiting}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var repeatingIntervalOn1cWaiting = TimeSpan.FromSeconds(this.StepsProperties.RepeatingIntervalOn1cWaiting);
                    this.pckrRepeatingIntervalOn1cWaiting.Value = new DateTime(
                            DateTime.Now.Year,
                            DateTime.Now.Month,
                            DateTime.Now.Day,
                            repeatingIntervalOn1cWaiting.Hours,
                            repeatingIntervalOn1cWaiting.Minutes,
                            repeatingIntervalOn1cWaiting.Seconds);

                    edRepeatTimesOnLackOf1cResponse.Text = this.StepsProperties.RepeatTimesOnLackOf1cResponse.ToString();

                    break;
                default:
                    this._isPositive = false;
                    break;
            }
        }

        private void SetDefaultValues()
        {
            pckrWaitingForSuccessInterval.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 5, 0);
            pckrRepeatingIntervalOn1cWaiting.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 5, 0);
            edRepeatTimesOnLackOf1cResponse.Text = "5";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.SelectedOperationItem = (OperationItem)this.cmbOperType.SelectedItem;

            this.StepsProperties = new StepsProperties
            {
                OperationId = (int)this.SelectedOperationItem?.Value,
                OperationName = (string)this.SelectedOperationItem?.Text,
                IsPositive = this._isPositive,
                WaitingForSuccessInterval = (int)this.pckrWaitingForSuccessInterval.Value.TimeOfDay.TotalSeconds,
                IsOperErrorEqualRuntimeError = chbIsOperErrorEqualRuntimeError.Checked,
                Is1cErrorEqualRuntimeError = chbIs1cErrorEqualRuntimeError.Checked,
                RepeatTimesOnLackOf1cResponse = Int32.Parse(edRepeatTimesOnLackOf1cResponse.Text),
                RepeatingIntervalOn1cWaiting = (int)this.pckrRepeatingIntervalOn1cWaiting.Value.TimeOfDay.TotalSeconds
            };

            Close();
        }

        private void StepPropertiesForm_Load(object sender, EventArgs e)
        {
            int selectedIndex = 0;
            foreach (var operation in Operations)
            {
                var item = new OperationItem();
                item.Text = operation.Value;
                item.Value = operation.Key;
                var currentIndex = this.cmbOperType.Items.Add(item);
                if (item?.Value == this?.StepsProperties?.OperationId)
                {
                    selectedIndex = currentIndex;
                }
            };
            this.cmbOperType.SelectedIndex = selectedIndex;
        }

        private void edRepeatTimesOnLackOf1cResponse_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }

    public class OperationItem
    {
        public string Text { get; set; }
        public int Value { get; set; }
        public override string ToString()
        {
            return Text;
        }
    }
}
