namespace MagicUpdaterMonitor.Forms
{
	partial class CheckUpdatesForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckUpdatesForm));
			this.btnCheckUpdates = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pbMonitor = new System.Windows.Forms.ProgressBar();
			this.pbAgent = new System.Windows.Forms.ProgressBar();
			this.btnDownloadUpdateMonitor = new System.Windows.Forms.Button();
			this.btnDownloadUpdateAgent = new System.Windows.Forms.Button();
			this.lbMonitor = new System.Windows.Forms.Label();
			this.lbAgent = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.tbServer = new System.Windows.Forms.TextBox();
			this.tbLogin = new System.Windows.Forms.TextBox();
			this.tbPassword = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCheckUpdates
			// 
			this.btnCheckUpdates.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnCheckUpdates.Location = new System.Drawing.Point(12, 173);
			this.btnCheckUpdates.Name = "btnCheckUpdates";
			this.btnCheckUpdates.Size = new System.Drawing.Size(633, 40);
			this.btnCheckUpdates.TabIndex = 0;
			this.btnCheckUpdates.Text = "Проверить наличие обновлений";
			this.btnCheckUpdates.UseVisualStyleBackColor = true;
			this.btnCheckUpdates.Click += new System.EventHandler(this.btnCheckUpdates_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.tbServer, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.tbLogin, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.tbPassword, 1, 2);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(633, 151);
			this.tableLayoutPanel1.TabIndex = 7;
			// 
			// panel1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
			this.panel1.Controls.Add(this.pbMonitor);
			this.panel1.Controls.Add(this.pbAgent);
			this.panel1.Controls.Add(this.btnDownloadUpdateMonitor);
			this.panel1.Controls.Add(this.btnDownloadUpdateAgent);
			this.panel1.Controls.Add(this.lbMonitor);
			this.panel1.Controls.Add(this.lbAgent);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(1, 82);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(631, 68);
			this.panel1.TabIndex = 8;
			// 
			// pbMonitor
			// 
			this.pbMonitor.Location = new System.Drawing.Point(6, 41);
			this.pbMonitor.MarqueeAnimationSpeed = 30;
			this.pbMonitor.Name = "pbMonitor";
			this.pbMonitor.Size = new System.Drawing.Size(80, 15);
			this.pbMonitor.TabIndex = 12;
			// 
			// pbAgent
			// 
			this.pbAgent.Location = new System.Drawing.Point(6, 10);
			this.pbAgent.MarqueeAnimationSpeed = 30;
			this.pbAgent.Name = "pbAgent";
			this.pbAgent.Size = new System.Drawing.Size(80, 15);
			this.pbAgent.TabIndex = 11;
			// 
			// btnDownloadUpdateMonitor
			// 
			this.btnDownloadUpdateMonitor.Enabled = false;
			this.btnDownloadUpdateMonitor.Location = new System.Drawing.Point(433, 38);
			this.btnDownloadUpdateMonitor.Name = "btnDownloadUpdateMonitor";
			this.btnDownloadUpdateMonitor.Size = new System.Drawing.Size(192, 23);
			this.btnDownloadUpdateMonitor.TabIndex = 10;
			this.btnDownloadUpdateMonitor.Text = "Скачать обновления для монитора";
			this.btnDownloadUpdateMonitor.UseVisualStyleBackColor = true;
			this.btnDownloadUpdateMonitor.Click += new System.EventHandler(this.btnDownloadUpdateMonitor_Click);
			// 
			// btnDownloadUpdateAgent
			// 
			this.btnDownloadUpdateAgent.Enabled = false;
			this.btnDownloadUpdateAgent.Location = new System.Drawing.Point(433, 7);
			this.btnDownloadUpdateAgent.Name = "btnDownloadUpdateAgent";
			this.btnDownloadUpdateAgent.Size = new System.Drawing.Size(192, 23);
			this.btnDownloadUpdateAgent.TabIndex = 9;
			this.btnDownloadUpdateAgent.Text = "Скачать обновления для агента";
			this.btnDownloadUpdateAgent.UseVisualStyleBackColor = true;
			this.btnDownloadUpdateAgent.Click += new System.EventHandler(this.btnDownloadUpdateAgent_Click);
			// 
			// lbMonitor
			// 
			this.lbMonitor.AutoSize = true;
			this.lbMonitor.Location = new System.Drawing.Point(92, 43);
			this.lbMonitor.Name = "lbMonitor";
			this.lbMonitor.Size = new System.Drawing.Size(51, 13);
			this.lbMonitor.TabIndex = 8;
			this.lbMonitor.Text = "Монитор";
			// 
			// lbAgent
			// 
			this.lbAgent.AutoSize = true;
			this.lbAgent.Location = new System.Drawing.Point(92, 12);
			this.lbAgent.Name = "lbAgent";
			this.lbAgent.Size = new System.Drawing.Size(36, 13);
			this.lbAgent.TabIndex = 7;
			this.lbAgent.Text = "Агент";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 7);
			this.label1.Margin = new System.Windows.Forms.Padding(6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Сервер:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 34);
			this.label3.Margin = new System.Windows.Forms.Padding(6);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Логин:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(7, 61);
			this.label4.Margin = new System.Windows.Forms.Padding(6);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Пароль:";
			// 
			// tbServer
			// 
			this.tbServer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbServer.Location = new System.Drawing.Point(320, 4);
			this.tbServer.Name = "tbServer";
			this.tbServer.Size = new System.Drawing.Size(309, 20);
			this.tbServer.TabIndex = 1;
			// 
			// tbLogin
			// 
			this.tbLogin.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbLogin.Location = new System.Drawing.Point(320, 31);
			this.tbLogin.Name = "tbLogin";
			this.tbLogin.Size = new System.Drawing.Size(309, 20);
			this.tbLogin.TabIndex = 1;
			// 
			// tbPassword
			// 
			this.tbPassword.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbPassword.Location = new System.Drawing.Point(320, 58);
			this.tbPassword.Name = "tbPassword";
			this.tbPassword.PasswordChar = '*';
			this.tbPassword.Size = new System.Drawing.Size(309, 20);
			this.tbPassword.TabIndex = 1;
			// 
			// CheckUpdatesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(657, 223);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.btnCheckUpdates);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CheckUpdatesForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnCheckUpdates;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbServer;
		private System.Windows.Forms.TextBox tbLogin;
		private System.Windows.Forms.TextBox tbPassword;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ProgressBar pbMonitor;
		private System.Windows.Forms.ProgressBar pbAgent;
		private System.Windows.Forms.Button btnDownloadUpdateMonitor;
		private System.Windows.Forms.Button btnDownloadUpdateAgent;
		private System.Windows.Forms.Label lbMonitor;
		private System.Windows.Forms.Label lbAgent;
	}
}