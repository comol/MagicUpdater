namespace MagicUpdaterInstaller
{
	partial class UninstallerForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UninstallerForm));
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.label4 = new System.Windows.Forms.Label();
			this.tbInstallationpath = new System.Windows.Forms.TextBox();
			this.btnUnInstall = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.rtbInstallServiceLog = new System.Windows.Forms.RichTextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.BackColor = System.Drawing.Color.Silver;
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.tbInstallationpath, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(790, 38);
			this.tableLayoutPanel2.TabIndex = 33;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(3, 10);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(389, 28);
			this.label4.TabIndex = 0;
			this.label4.Text = "Путь установки:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tbInstallationpath
			// 
			this.tbInstallationpath.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbInstallationpath.Location = new System.Drawing.Point(398, 13);
			this.tbInstallationpath.Name = "tbInstallationpath";
			this.tbInstallationpath.Size = new System.Drawing.Size(389, 20);
			this.tbInstallationpath.TabIndex = 1;
			this.tbInstallationpath.Text = "C:\\SystemUtils\\MagicUpdater\\";
			// 
			// btnUnInstall
			// 
			this.btnUnInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnUnInstall.Location = new System.Drawing.Point(298, 53);
			this.btnUnInstall.Name = "btnUnInstall";
			this.btnUnInstall.Size = new System.Drawing.Size(194, 33);
			this.btnUnInstall.TabIndex = 34;
			this.btnUnInstall.Text = "Удалить";
			this.btnUnInstall.UseVisualStyleBackColor = true;
			this.btnUnInstall.Click += new System.EventHandler(this.btnUnInstall_Click);
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(8, 99);
			this.progressBar1.Maximum = 10;
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(774, 23);
			this.progressBar1.TabIndex = 35;
			// 
			// rtbInstallServiceLog
			// 
			this.rtbInstallServiceLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbInstallServiceLog.Location = new System.Drawing.Point(8, 8);
			this.rtbInstallServiceLog.Name = "rtbInstallServiceLog";
			this.rtbInstallServiceLog.ReadOnly = true;
			this.rtbInstallServiceLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.rtbInstallServiceLog.Size = new System.Drawing.Size(774, 287);
			this.rtbInstallServiceLog.TabIndex = 36;
			this.rtbInstallServiceLog.Text = "";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.rtbInstallServiceLog);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 128);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(8);
			this.panel1.Size = new System.Drawing.Size(790, 303);
			this.panel1.TabIndex = 37;
			// 
			// UninstallerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(790, 431);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.btnUnInstall);
			this.Controls.Add(this.tableLayoutPanel2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "UninstallerForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MagicUpdaterInstaller";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UninstallerForm_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UninstallerForm_FormClosed);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbInstallationpath;
		private System.Windows.Forms.Button btnUnInstall;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.RichTextBox rtbInstallServiceLog;
		private System.Windows.Forms.Panel panel1;
	}
}