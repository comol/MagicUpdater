namespace MagicUpdaterMonitor.Controls
{
	partial class CellExtensionForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CellExtensionForm));
			this.rtbInfo = new System.Windows.Forms.RichTextBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.btnClose = new System.Windows.Forms.Button();
			this.lbInfo = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// rtbInfo
			// 
			this.rtbInfo.BackColor = System.Drawing.SystemColors.Control;
			this.rtbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbInfo.Location = new System.Drawing.Point(5, 34);
			this.rtbInfo.Name = "rtbInfo";
			this.rtbInfo.ReadOnly = true;
			this.rtbInfo.Size = new System.Drawing.Size(276, 198);
			this.rtbInfo.TabIndex = 0;
			this.rtbInfo.Text = "";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.rtbInfo, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.lbInfo, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(2);
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(286, 298);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tableLayoutPanel2.Controls.Add(this.btnClose, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 238);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(10);
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(276, 55);
			this.tableLayoutPanel2.TabIndex = 1;
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnClose.Location = new System.Drawing.Point(89, 13);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(96, 29);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Закрыть";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// lbInfo
			// 
			this.lbInfo.AutoSize = true;
			this.lbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lbInfo.Location = new System.Drawing.Point(5, 2);
			this.lbInfo.Name = "lbInfo";
			this.lbInfo.Size = new System.Drawing.Size(276, 29);
			this.lbInfo.TabIndex = 2;
			this.lbInfo.Text = "lbInfo";
			this.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CellExtensionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(286, 298);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CellExtensionForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button btnClose;
		public System.Windows.Forms.RichTextBox rtbInfo;
		public System.Windows.Forms.Label lbInfo;
	}
}