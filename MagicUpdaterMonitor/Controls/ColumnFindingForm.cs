using MagicUpdaterMonitor.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MagicUpdaterMonitor.Controls
{
	public partial class ColumnFindingForm : BaseForm
	{
		private List<string> operatorsString = new List<string>
		{
			"like",
			"=",
			"<>"
		};

		private List<string> operatorsInt = new List<string>
		{
			"=",
			">",
			"<",
			">=",
			"<=",
			"<>"
		};

		private List<string> operatorsDateTime = new List<string>
		{
			"=",
			">",
			"<",
			">=",
			"<=",
			"<>"
		};

		private List<string> operatorsBool = new List<string>
		{
			"=",
			"<>"
		};

		private DataGridViewColumn column = null;
		private DataGridView dataGridView = null;
		public string FilterString { get; private set; } = "";

		public ColumnFindingForm(DataGridView dgv, DataGridViewColumn _column, Type type)
		{
			InitializeComponent();
			column = _column;

			if (column.Name == "Selected")
			{
				return;
			}

			lbColumnName.Text = column.Name;

			if (type == typeof(bool))
			{
				cbOperator.DataSource = operatorsBool;
			}
			else if (type == typeof(int))
			{
				cbOperator.DataSource = operatorsInt;
			}
			else if (type == typeof(string))
			{
				cbOperator.DataSource = operatorsString;
			}
			else if (type == typeof(DateTime))
			{
				cbOperator.DataSource = operatorsDateTime;
			}

			cbOperator.SelectedIndex = 0;
			dataGridView = dgv;
			FillFilterCombo();
			SetWindowStartPosition();
		}

		public new DialogResult ShowDialog()
		{
			if (column.Name == "Selected")
			{
				this.Dispose();
				return DialogResult.Abort;
			}

			return base.ShowDialog();
		}

		private void SetWindowStartPosition()
		{
			int x;
			if (Cursor.Position.X + this.Width > SystemInformation.VirtualScreen.Width)
				x = Cursor.Position.X - (Cursor.Position.X + this.Width - SystemInformation.VirtualScreen.Width);
			else
				x = Cursor.Position.X;
			Location = new Point(x, Cursor.Position.Y);
		}

		private void FillFilterCombo()
		{
			DataView dv = dataGridView.DataSource as DataView;
			if (dv != null)
			{
				DataTable dt = dv.ToTable(true, column.Name);
				cbFilter.DataSource = dt;
				cbFilter.DisplayMember = column.Name;
				cbFilter.ValueMember = column.Name;
			}
		}

		private string ConvertStringByColumn(string input, string _operator)
		{
			Type type = column.ValueType;
			if (type == typeof(string) || type == typeof(DateTime))
			{
				return $"{column.Name} {_operator} '{input}'";
			}
			return $"{column.Name} {_operator} {input}";
		}

		private const int CP_NOCLOSE_BUTTON = 0x200;
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams myCp = base.CreateParams;
				myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
				return myCp;
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			FilterString = ConvertStringByColumn(cbFilter.Text, cbOperator.Text);
		}
	}
}
