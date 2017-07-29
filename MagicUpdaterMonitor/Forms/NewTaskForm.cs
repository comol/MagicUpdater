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
    public partial class NewTaskForm : BaseForm
    {
        public string NewTaskName { get; set; }
        public NewTaskForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.NewTaskName = this.edNewTaskName.Text;
        }

        private void NewTaskForm_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = this.edNewTaskName;
        }

        private void edNewTaskName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk_Click(this, new EventArgs());
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
