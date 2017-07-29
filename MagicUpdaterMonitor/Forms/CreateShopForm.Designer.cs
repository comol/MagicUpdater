namespace MagicUpdaterMonitor.Controls
{
	partial class CreateShopForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateShopForm));
			this.btnCancel = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.btnOk = new System.Windows.Forms.Button();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.lbShopId = new System.Windows.Forms.Label();
			this.lbShopRegion = new System.Windows.Forms.Label();
			this.tbShopId = new System.Windows.Forms.TextBox();
			this.tbShopRegion = new System.Windows.Forms.TextBox();
			this.lbError = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnCancel.Location = new System.Drawing.Point(219, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 27);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.lbError, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(372, 120);
			this.tableLayoutPanel1.TabIndex = 1;
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
			this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 81);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(6);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(360, 33);
			this.tableLayoutPanel2.TabIndex = 1;
			// 
			// btnOk
			// 
			this.btnOk.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnOk.Location = new System.Drawing.Point(66, 3);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 27);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "Ок";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.53333F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.46667F));
			this.tableLayoutPanel3.Controls.Add(this.lbShopId, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.lbShopRegion, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.tbShopId, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.tbShopRegion, 1, 1);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(366, 49);
			this.tableLayoutPanel3.TabIndex = 2;
			// 
			// lbShopId
			// 
			this.lbShopId.AutoSize = true;
			this.lbShopId.Dock = System.Windows.Forms.DockStyle.Right;
			this.lbShopId.Location = new System.Drawing.Point(12, 5);
			this.lbShopId.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
			this.lbShopId.Name = "lbShopId";
			this.lbShopId.Size = new System.Drawing.Size(60, 19);
			this.lbShopId.TabIndex = 0;
			this.lbShopId.Text = "Название:";
			// 
			// lbShopRegion
			// 
			this.lbShopRegion.AutoSize = true;
			this.lbShopRegion.Dock = System.Windows.Forms.DockStyle.Right;
			this.lbShopRegion.Location = new System.Drawing.Point(32, 29);
			this.lbShopRegion.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
			this.lbShopRegion.Name = "lbShopRegion";
			this.lbShopRegion.Size = new System.Drawing.Size(40, 20);
			this.lbShopRegion.TabIndex = 1;
			this.lbShopRegion.Text = "Город:";
			// 
			// tbShopId
			// 
			this.tbShopId.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbShopId.Location = new System.Drawing.Point(78, 3);
			this.tbShopId.Name = "tbShopId";
			this.tbShopId.Size = new System.Drawing.Size(285, 20);
			this.tbShopId.TabIndex = 2;
			this.tbShopId.Enter += new System.EventHandler(this.tbShopId_Enter);
			// 
			// tbShopRegion
			// 
			this.tbShopRegion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbShopRegion.Location = new System.Drawing.Point(78, 27);
			this.tbShopRegion.Name = "tbShopRegion";
			this.tbShopRegion.Size = new System.Drawing.Size(285, 20);
			this.tbShopRegion.TabIndex = 2;
			this.tbShopRegion.Enter += new System.EventHandler(this.tbShopId_Enter);
			// 
			// lbError
			// 
			this.lbError.AutoSize = true;
			this.lbError.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lbError.ForeColor = System.Drawing.Color.Red;
			this.lbError.Location = new System.Drawing.Point(3, 55);
			this.lbError.Name = "lbError";
			this.lbError.Size = new System.Drawing.Size(366, 20);
			this.lbError.TabIndex = 3;
			this.lbError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CreateShopForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(372, 120);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CreateShopForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Создать магазин";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Label lbShopId;
		private System.Windows.Forms.Label lbShopRegion;
		private System.Windows.Forms.TextBox tbShopId;
		private System.Windows.Forms.TextBox tbShopRegion;
		private System.Windows.Forms.Label lbError;
	}
}