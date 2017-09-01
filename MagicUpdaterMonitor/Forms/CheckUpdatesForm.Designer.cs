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
			// CheckUpdatesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(657, 416);
			this.Controls.Add(this.btnCheckUpdates);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CheckUpdatesForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnCheckUpdates;
	}
}