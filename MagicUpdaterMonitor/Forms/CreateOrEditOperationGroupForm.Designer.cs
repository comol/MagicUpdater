namespace MagicUpdaterMonitor.Forms
{
	partial class CreateOrEditOperationGroupForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateOrEditOperationGroupForm));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tbDescription = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tbOperationGroupName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.31185F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.68815F));
			this.tableLayoutPanel1.Controls.Add(this.tbDescription, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tbOperationGroupName, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(409, 55);
			this.tableLayoutPanel1.TabIndex = 4;
			// 
			// tbDescription
			// 
			this.tbDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbDescription.Location = new System.Drawing.Point(209, 31);
			this.tbDescription.Name = "tbDescription";
			this.tbDescription.Size = new System.Drawing.Size(196, 20);
			this.tbDescription.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(4, 1);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(198, 26);
			this.label1.TabIndex = 3;
			this.label1.Text = "Имя группы:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tbOperationGroupName
			// 
			this.tbOperationGroupName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbOperationGroupName.Location = new System.Drawing.Point(209, 4);
			this.tbOperationGroupName.Name = "tbOperationGroupName";
			this.tbOperationGroupName.Size = new System.Drawing.Size(196, 20);
			this.tbOperationGroupName.TabIndex = 4;
			this.tbOperationGroupName.TextChanged += new System.EventHandler(this.tbNewOperationGroupName_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(4, 28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(198, 26);
			this.label2.TabIndex = 5;
			this.label2.Text = "Описание:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CreateOrEditOperationGroupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(409, 99);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CreateOrEditOperationGroupForm";
			this.Text = "Создание группы для операций";
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbOperationGroupName;
		private System.Windows.Forms.TextBox tbDescription;
		private System.Windows.Forms.Label label2;
	}
}