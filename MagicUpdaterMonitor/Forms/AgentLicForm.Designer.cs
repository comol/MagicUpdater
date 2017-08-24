namespace MagicUpdaterMonitor.Forms
{
	partial class AgentLicForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentLicForm));
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.lbName = new System.Windows.Forms.Label();
			this.rgvLicAgent = new MagicUpdaterMonitor.Controls.RefreshingGridView();
			this.btnOk = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnOk);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 343);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(805, 51);
			this.panel1.TabIndex = 1;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.lbName);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(805, 33);
			this.panel2.TabIndex = 2;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.rgvLicAgent);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 33);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(805, 310);
			this.panel3.TabIndex = 3;
			// 
			// lbName
			// 
			this.lbName.BackColor = System.Drawing.Color.Gold;
			this.lbName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lbName.Location = new System.Drawing.Point(0, 0);
			this.lbName.Margin = new System.Windows.Forms.Padding(0);
			this.lbName.Name = "lbName";
			this.lbName.Size = new System.Drawing.Size(805, 33);
			this.lbName.TabIndex = 4;
			this.lbName.Text = "Получение лицензий";
			this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// rgvLicAgent
			// 
			this.rgvLicAgent.BaseFilter = "";
			this.rgvLicAgent.BtnClearFiltersVisible = false;
			this.rgvLicAgent.CbSelectAllVisible = false;
			this.rgvLicAgent.ContextFilterMenuItemsVisible = false;
			this.rgvLicAgent.DataSource = null;
			this.rgvLicAgent.DataView = null;
			this.rgvLicAgent.DetailsForm = null;
			this.rgvLicAgent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rgvLicAgent.ExportToexcelVisible = false;
			this.rgvLicAgent.Filter = "";
			this.rgvLicAgent.IsColumnFilteringEnabled = false;
			this.rgvLicAgent.IsContextMenuVisible = false;
			this.rgvLicAgent.IsDetailsEnabled = false;
			this.rgvLicAgent.IsMultiselect = false;
			this.rgvLicAgent.IsShowCellExtensionFormByDoubleClick = true;
			this.rgvLicAgent.KeyField = null;
			this.rgvLicAgent.LbFilterVisible = false;
			this.rgvLicAgent.Location = new System.Drawing.Point(0, 0);
			this.rgvLicAgent.MappingColumns = null;
			this.rgvLicAgent.Name = "rgvLicAgent";
			this.rgvLicAgent.ResetFilterRadioButton = null;
			this.rgvLicAgent.Size = new System.Drawing.Size(805, 310);
			this.rgvLicAgent.TabIndex = 0;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(353, 12);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(101, 27);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "Ок";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// AgentLicForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(805, 394);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AgentLicForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Shown += new System.EventHandler(this.AgentLicForm_Shown);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label lbName;
		private Controls.RefreshingGridView rgvLicAgent;
		private System.Windows.Forms.Button btnOk;
	}
}