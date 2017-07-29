namespace MagicUpdaterMonitor.Forms
{
	partial class AboutForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.lbVersion = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lbName = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(150, 150);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 298F));
			this.tableLayoutPanel1.Controls.Add(this.lbVersion, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.lbName, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(168, 12);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.72727F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.27273F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(404, 92);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// lbVersion
			// 
			this.lbVersion.AutoSize = true;
			this.lbVersion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lbVersion.Location = new System.Drawing.Point(109, 48);
			this.lbVersion.Name = "lbVersion";
			this.lbVersion.Size = new System.Drawing.Size(292, 44);
			this.lbVersion.TabIndex = 0;
			this.lbVersion.Text = "version";
			this.lbVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(3, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 44);
			this.label2.TabIndex = 1;
			this.label2.Text = "Версия:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbName
			// 
			this.lbName.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.lbName, 2);
			this.lbName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lbName.Location = new System.Drawing.Point(3, 0);
			this.lbName.Name = "lbName";
			this.lbName.Size = new System.Drawing.Size(398, 48);
			this.lbName.TabIndex = 0;
			this.lbName.Text = "Монитор обновления 1С";
			this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// AboutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 172);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "О программе";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label lbVersion;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lbName;
	}
}