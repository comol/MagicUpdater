namespace MagicUpdaterMonitor.Controls
{
	partial class ConfirmationWithTableForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfirmationWithTableForm));
			this.btnOk = new System.Windows.Forms.Button();
			this.bntCancel = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.rgvInfo = new MagicUpdaterMonitor.Controls.RefreshingGridView();
			this.lbInfo = new System.Windows.Forms.Label();
			this.lbOperType = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnOk.Location = new System.Drawing.Point(189, 11);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(120, 31);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "Ок";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// bntCancel
			// 
			this.bntCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bntCancel.Dock = System.Windows.Forms.DockStyle.Left;
			this.bntCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.bntCancel.Location = new System.Drawing.Point(382, 11);
			this.bntCancel.Name = "bntCancel";
			this.bntCancel.Size = new System.Drawing.Size(120, 31);
			this.bntCancel.TabIndex = 1;
			this.bntCancel.Text = "Отмена";
			this.bntCancel.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.rgvInfo, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.lbInfo, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.lbOperType, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(699, 341);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.tableLayoutPanel2.Controls.Add(this.btnOk, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.bntCancel, 2, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 285);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(8);
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(693, 53);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// rgvInfo
			// 
			this.rgvInfo.BtnClearFiltersVisible = false;
			this.rgvInfo.CbSelectAllVisible = false;
			this.rgvInfo.ContextFilterMenuItemsVisible = true;
			this.rgvInfo.DataSource = null;
			this.rgvInfo.DataView = null;
			this.rgvInfo.DetailsForm = null;
			this.rgvInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rgvInfo.ExportToexcelVisible = true;
			this.rgvInfo.Filter = null;
			this.rgvInfo.IsColumnFilteringEnabled = true;
			this.rgvInfo.IsDetailsEnabled = false;
			this.rgvInfo.IsMultiselect = true;
			this.rgvInfo.IsShowCellExtensionFormByDoubleClick = true;
			this.rgvInfo.KeyField = null;
			this.rgvInfo.LbFilterVisible = true;
			this.rgvInfo.Location = new System.Drawing.Point(3, 47);
			this.rgvInfo.MappingColumns = null;
			this.rgvInfo.Name = "rgvInfo";
			this.rgvInfo.ResetFilterRadioButton = null;
			this.rgvInfo.Size = new System.Drawing.Size(693, 232);
			this.rgvInfo.TabIndex = 1;
			// 
			// lbInfo
			// 
			this.lbInfo.AutoSize = true;
			this.lbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lbInfo.Location = new System.Drawing.Point(0, 24);
			this.lbInfo.Margin = new System.Windows.Forms.Padding(0);
			this.lbInfo.Name = "lbInfo";
			this.lbInfo.Size = new System.Drawing.Size(699, 20);
			this.lbInfo.TabIndex = 2;
			this.lbInfo.Text = "info";
			this.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbOperType
			// 
			this.lbOperType.AutoSize = true;
			this.lbOperType.BackColor = System.Drawing.Color.Gold;
			this.lbOperType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbOperType.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lbOperType.Location = new System.Drawing.Point(0, 0);
			this.lbOperType.Margin = new System.Windows.Forms.Padding(0);
			this.lbOperType.Name = "lbOperType";
			this.lbOperType.Size = new System.Drawing.Size(699, 24);
			this.lbOperType.TabIndex = 3;
			this.lbOperType.Text = "OperType";
			this.lbOperType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ConfirmationWithTableForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bntCancel;
			this.ClientSize = new System.Drawing.Size(699, 341);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimizeBox = false;
			this.Name = "ConfirmationWithTableForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button bntCancel;
		public Controls.RefreshingGridView rgvInfo;
		private System.Windows.Forms.Label lbInfo;
		private System.Windows.Forms.Label lbOperType;
	}
}