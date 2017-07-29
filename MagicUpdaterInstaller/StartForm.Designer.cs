namespace MagicUpdaterInstaller
{
	partial class StartForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
			this.btnInstall = new System.Windows.Forms.Button();
			this.btnUninstall = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnInstall
			// 
			this.btnInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnInstall.Location = new System.Drawing.Point(12, 12);
			this.btnInstall.Name = "btnInstall";
			this.btnInstall.Size = new System.Drawing.Size(265, 47);
			this.btnInstall.TabIndex = 0;
			this.btnInstall.Text = "Установить";
			this.btnInstall.UseVisualStyleBackColor = true;
			this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
			// 
			// btnUninstall
			// 
			this.btnUninstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnUninstall.Location = new System.Drawing.Point(12, 65);
			this.btnUninstall.Name = "btnUninstall";
			this.btnUninstall.Size = new System.Drawing.Size(265, 47);
			this.btnUninstall.TabIndex = 1;
			this.btnUninstall.Text = "Удалить";
			this.btnUninstall.UseVisualStyleBackColor = true;
			this.btnUninstall.Click += new System.EventHandler(this.btnUninstall_Click);
			// 
			// StartForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(289, 125);
			this.Controls.Add(this.btnUninstall);
			this.Controls.Add(this.btnInstall);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "StartForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MagicUpdaterInstaller";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnInstall;
		private System.Windows.Forms.Button btnUninstall;
	}
}