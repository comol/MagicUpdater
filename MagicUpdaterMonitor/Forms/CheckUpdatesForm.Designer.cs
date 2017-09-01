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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnDownloadUpdateAgent = new System.Windows.Forms.Button();
			this.btnDownloadUpdateMonitor = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnCheckUpdates
			// 
			this.btnCheckUpdates.Location = new System.Drawing.Point(12, 12);
			this.btnCheckUpdates.Name = "btnCheckUpdates";
			this.btnCheckUpdates.Size = new System.Drawing.Size(138, 54);
			this.btnCheckUpdates.TabIndex = 0;
			this.btnCheckUpdates.Text = "Проверить наличие обновлений";
			this.btnCheckUpdates.UseVisualStyleBackColor = true;
			this.btnCheckUpdates.Click += new System.EventHandler(this.btnCheckUpdates_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(22, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "label1";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(22, 59);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "label2";
			// 
			// btnDownloadUpdateAgent
			// 
			this.btnDownloadUpdateAgent.Location = new System.Drawing.Point(328, 59);
			this.btnDownloadUpdateAgent.Name = "btnDownloadUpdateAgent";
			this.btnDownloadUpdateAgent.Size = new System.Drawing.Size(138, 54);
			this.btnDownloadUpdateAgent.TabIndex = 3;
			this.btnDownloadUpdateAgent.Text = "Скачать обновления для агента";
			this.btnDownloadUpdateAgent.UseVisualStyleBackColor = true;
			// 
			// btnDownloadUpdateMonitor
			// 
			this.btnDownloadUpdateMonitor.Location = new System.Drawing.Point(258, 177);
			this.btnDownloadUpdateMonitor.Name = "btnDownloadUpdateMonitor";
			this.btnDownloadUpdateMonitor.Size = new System.Drawing.Size(138, 54);
			this.btnDownloadUpdateMonitor.TabIndex = 4;
			this.btnDownloadUpdateMonitor.Text = "Скачать обновления для агента";
			this.btnDownloadUpdateMonitor.UseVisualStyleBackColor = true;
			// 
			// CheckUpdatesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(657, 416);
			this.Controls.Add(this.btnDownloadUpdateMonitor);
			this.Controls.Add(this.btnDownloadUpdateAgent);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCheckUpdates);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CheckUpdatesForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCheckUpdates;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnDownloadUpdateAgent;
		private System.Windows.Forms.Button btnDownloadUpdateMonitor;
	}
}