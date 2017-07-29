namespace MagicUpdaterMonitor.Forms
{
	partial class MoveToOperationGroupForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveToOperationGroupForm));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.lbOperationTypeName = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cbOperationGroup = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.31185F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.68815F));
			this.tableLayoutPanel1.Controls.Add(this.lbOperationTypeName, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.cbOperationGroup, 1, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(448, 58);
			this.tableLayoutPanel1.TabIndex = 3;
			// 
			// lbOperationTypeName
			// 
			this.lbOperationTypeName.AutoSize = true;
			this.lbOperationTypeName.BackColor = System.Drawing.Color.Gold;
			this.tableLayoutPanel1.SetColumnSpan(this.lbOperationTypeName, 2);
			this.lbOperationTypeName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbOperationTypeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lbOperationTypeName.Location = new System.Drawing.Point(4, 1);
			this.lbOperationTypeName.Name = "lbOperationTypeName";
			this.lbOperationTypeName.Size = new System.Drawing.Size(440, 28);
			this.lbOperationTypeName.TabIndex = 2;
			this.lbOperationTypeName.Text = "lbOperationTypeName";
			this.lbOperationTypeName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(4, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(217, 27);
			this.label1.TabIndex = 3;
			this.label1.Text = "Группа";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// cbOperationGroup
			// 
			this.cbOperationGroup.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbOperationGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbOperationGroup.FormattingEnabled = true;
			this.cbOperationGroup.Location = new System.Drawing.Point(228, 33);
			this.cbOperationGroup.Name = "cbOperationGroup";
			this.cbOperationGroup.Size = new System.Drawing.Size(216, 21);
			this.cbOperationGroup.TabIndex = 6;
			this.cbOperationGroup.SelectedValueChanged += new System.EventHandler(this.cbOperationGroup_SelectedValueChanged);
			// 
			// MoveToOperationGroupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(448, 105);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MoveToOperationGroupForm";
			this.Text = "Перемещение операции в группу";
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label lbOperationTypeName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbOperationGroup;
	}
}