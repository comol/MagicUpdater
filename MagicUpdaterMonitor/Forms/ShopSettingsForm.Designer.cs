namespace MagicUpdaterMonitor.Forms
{
	partial class ShopSettingsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShopSettingsForm));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.lbEmail = new System.Windows.Forms.Label();
			this.lbPhone = new System.Windows.Forms.Label();
			this.tbEmail = new System.Windows.Forms.TextBox();
			this.tbPhone = new System.Windows.Forms.TextBox();
			this.lbShopId = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.tbName = new System.Windows.Forms.TextBox();
			this.tbRegion = new System.Windows.Forms.TextBox();
			this.tbAddressToConnect = new System.Windows.Forms.TextBox();
			this.chbIsClosed = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.31185F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.68815F));
			this.tableLayoutPanel1.Controls.Add(this.lbEmail, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.lbPhone, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.tbEmail, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.tbPhone, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.lbShopId, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.tbName, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.tbRegion, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.tbAddressToConnect, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.chbIsClosed, 1, 6);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 7;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(482, 185);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// lbEmail
			// 
			this.lbEmail.AutoSize = true;
			this.lbEmail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbEmail.Location = new System.Drawing.Point(4, 110);
			this.lbEmail.Name = "lbEmail";
			this.lbEmail.Size = new System.Drawing.Size(234, 26);
			this.lbEmail.TabIndex = 0;
			this.lbEmail.Text = "Email";
			this.lbEmail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbPhone
			// 
			this.lbPhone.AutoSize = true;
			this.lbPhone.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbPhone.Location = new System.Drawing.Point(4, 137);
			this.lbPhone.Name = "lbPhone";
			this.lbPhone.Size = new System.Drawing.Size(234, 26);
			this.lbPhone.TabIndex = 0;
			this.lbPhone.Text = "Телефон";
			this.lbPhone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tbEmail
			// 
			this.tbEmail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbEmail.Location = new System.Drawing.Point(245, 113);
			this.tbEmail.Name = "tbEmail";
			this.tbEmail.Size = new System.Drawing.Size(233, 20);
			this.tbEmail.TabIndex = 1;
			this.tbEmail.TextChanged += new System.EventHandler(this.tbAll_TextChanged);
			// 
			// tbPhone
			// 
			this.tbPhone.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbPhone.Location = new System.Drawing.Point(245, 140);
			this.tbPhone.Name = "tbPhone";
			this.tbPhone.Size = new System.Drawing.Size(233, 20);
			this.tbPhone.TabIndex = 1;
			this.tbPhone.TextChanged += new System.EventHandler(this.tbAll_TextChanged);
			this.tbPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPhone_KeyPress);
			// 
			// lbShopId
			// 
			this.lbShopId.AutoSize = true;
			this.lbShopId.BackColor = System.Drawing.Color.Gold;
			this.tableLayoutPanel1.SetColumnSpan(this.lbShopId, 2);
			this.lbShopId.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbShopId.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lbShopId.Location = new System.Drawing.Point(4, 1);
			this.lbShopId.Name = "lbShopId";
			this.lbShopId.Size = new System.Drawing.Size(474, 27);
			this.lbShopId.TabIndex = 2;
			this.lbShopId.Text = "lbShopId";
			this.lbShopId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(4, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(234, 26);
			this.label1.TabIndex = 3;
			this.label1.Text = "Наименование";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(4, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(234, 26);
			this.label2.TabIndex = 3;
			this.label2.Text = "Регион";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(4, 83);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(234, 26);
			this.label3.TabIndex = 3;
			this.label3.Text = "Адрес подключения";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(4, 164);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(234, 20);
			this.label4.TabIndex = 3;
			this.label4.Text = "Магазин закрыт";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tbName
			// 
			this.tbName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbName.Location = new System.Drawing.Point(245, 32);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(233, 20);
			this.tbName.TabIndex = 4;
			this.tbName.TextChanged += new System.EventHandler(this.tbAll_TextChanged);
			// 
			// tbRegion
			// 
			this.tbRegion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbRegion.Location = new System.Drawing.Point(245, 59);
			this.tbRegion.Name = "tbRegion";
			this.tbRegion.Size = new System.Drawing.Size(233, 20);
			this.tbRegion.TabIndex = 4;
			this.tbRegion.TextChanged += new System.EventHandler(this.tbAll_TextChanged);
			// 
			// tbAddressToConnect
			// 
			this.tbAddressToConnect.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbAddressToConnect.Location = new System.Drawing.Point(245, 86);
			this.tbAddressToConnect.Name = "tbAddressToConnect";
			this.tbAddressToConnect.Size = new System.Drawing.Size(233, 20);
			this.tbAddressToConnect.TabIndex = 4;
			this.tbAddressToConnect.TextChanged += new System.EventHandler(this.tbAll_TextChanged);
			// 
			// chbIsClosed
			// 
			this.chbIsClosed.AutoSize = true;
			this.chbIsClosed.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chbIsClosed.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chbIsClosed.Location = new System.Drawing.Point(245, 167);
			this.chbIsClosed.Name = "chbIsClosed";
			this.chbIsClosed.Size = new System.Drawing.Size(233, 14);
			this.chbIsClosed.TabIndex = 5;
			this.chbIsClosed.UseVisualStyleBackColor = true;
			this.chbIsClosed.CheckedChanged += new System.EventHandler(this.chbIsClosed_CheckedChanged);
			// 
			// ShopSettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = null;
			this.ClientSize = new System.Drawing.Size(482, 230);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ShopSettingsForm";
			this.Text = "Настройки магазина";
			this.Load += new System.EventHandler(this.ShopSettingsForm_Load);
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label lbEmail;
		private System.Windows.Forms.Label lbPhone;
		private System.Windows.Forms.TextBox tbEmail;
		private System.Windows.Forms.TextBox tbPhone;
		private System.Windows.Forms.Label lbShopId;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.TextBox tbRegion;
		private System.Windows.Forms.TextBox tbAddressToConnect;
		private System.Windows.Forms.CheckBox chbIsClosed;
	}
}