namespace MagicUpdaterMonitor.Controls
{
	partial class ColumnFindingForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColumnFindingForm));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lbColumnName = new System.Windows.Forms.Label();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.cbOperator = new System.Windows.Forms.ComboBox();
			this.cbFilter = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.lbColumnName, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(391, 104);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.tableLayoutPanel2.Controls.Add(this.btnOk, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.btnCancel, 2, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 64);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(6);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(379, 34);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnOk.Location = new System.Drawing.Point(73, 3);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 28);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "Ок";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnCancel.Location = new System.Drawing.Point(229, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 28);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// lbColumnName
			// 
			this.lbColumnName.AutoSize = true;
			this.lbColumnName.BackColor = System.Drawing.Color.Gold;
			this.lbColumnName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbColumnName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lbColumnName.Location = new System.Drawing.Point(0, 0);
			this.lbColumnName.Margin = new System.Windows.Forms.Padding(0);
			this.lbColumnName.Name = "lbColumnName";
			this.lbColumnName.Size = new System.Drawing.Size(391, 20);
			this.lbColumnName.TabIndex = 2;
			this.lbColumnName.Text = "ColumnName";
			this.lbColumnName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.cbOperator, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.cbFilter, 1, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 23);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(385, 32);
			this.tableLayoutPanel3.TabIndex = 3;
			// 
			// cbOperator
			// 
			this.cbOperator.Dock = System.Windows.Forms.DockStyle.Top;
			this.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbOperator.FormattingEnabled = true;
			this.cbOperator.Location = new System.Drawing.Point(6, 5);
			this.cbOperator.Margin = new System.Windows.Forms.Padding(6, 5, 6, 6);
			this.cbOperator.Name = "cbOperator";
			this.cbOperator.Size = new System.Drawing.Size(39, 21);
			this.cbOperator.TabIndex = 2;
			// 
			// cbFilter
			// 
			this.cbFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cbFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbFilter.Dock = System.Windows.Forms.DockStyle.Top;
			this.cbFilter.FormattingEnabled = true;
			this.cbFilter.Location = new System.Drawing.Point(57, 6);
			this.cbFilter.Margin = new System.Windows.Forms.Padding(6);
			this.cbFilter.Name = "cbFilter";
			this.cbFilter.Size = new System.Drawing.Size(322, 21);
			this.cbFilter.TabIndex = 1;
			// 
			// ColumnFindingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(391, 104);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ColumnFindingForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Поиск в столбце";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lbColumnName;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.ComboBox cbOperator;
		private System.Windows.Forms.ComboBox cbFilter;
	}
}