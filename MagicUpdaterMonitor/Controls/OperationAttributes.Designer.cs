namespace MagicUpdaterMonitor.Controls
{
	partial class OperationAttributes
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tlpPropertyControls = new System.Windows.Forms.TableLayoutPanel();
			this.pnScroll = new System.Windows.Forms.Panel();
			this.pnHeader = new System.Windows.Forms.Panel();
			this.tlpHeader = new System.Windows.Forms.TableLayoutPanel();
			this.lbHeaderName = new System.Windows.Forms.Label();
			this.lbHeaderValue = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.pnScroll.SuspendLayout();
			this.pnHeader.SuspendLayout();
			this.tlpHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlpPropertyControls
			// 
			this.tlpPropertyControls.AutoSize = true;
			this.tlpPropertyControls.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpPropertyControls.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tlpPropertyControls.ColumnCount = 2;
			this.tlpPropertyControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlpPropertyControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlpPropertyControls.Dock = System.Windows.Forms.DockStyle.Top;
			this.tlpPropertyControls.Location = new System.Drawing.Point(0, 0);
			this.tlpPropertyControls.Name = "tlpPropertyControls";
			this.tlpPropertyControls.RowCount = 1;
			this.tlpPropertyControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpPropertyControls.Size = new System.Drawing.Size(246, 2);
			this.tlpPropertyControls.TabIndex = 0;
			this.tlpPropertyControls.SizeChanged += new System.EventHandler(this.tlpPropertyControls_SizeChanged);
			// 
			// pnScroll
			// 
			this.pnScroll.AutoScroll = true;
			this.pnScroll.AutoSize = true;
			this.pnScroll.Controls.Add(this.button1);
			this.pnScroll.Controls.Add(this.tlpPropertyControls);
			this.pnScroll.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnScroll.Location = new System.Drawing.Point(0, 30);
			this.pnScroll.Name = "pnScroll";
			this.pnScroll.Size = new System.Drawing.Size(246, 356);
			this.pnScroll.TabIndex = 3;
			this.pnScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.pnScroll_Scroll);
			// 
			// pnHeader
			// 
			this.pnHeader.Controls.Add(this.tlpHeader);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Size = new System.Drawing.Size(246, 30);
			this.pnHeader.TabIndex = 1;
			// 
			// tlpHeader
			// 
			this.tlpHeader.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tlpHeader.ColumnCount = 2;
			this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpHeader.Controls.Add(this.lbHeaderName, 0, 0);
			this.tlpHeader.Controls.Add(this.lbHeaderValue, 1, 0);
			this.tlpHeader.Location = new System.Drawing.Point(0, 0);
			this.tlpHeader.Name = "tlpHeader";
			this.tlpHeader.RowCount = 1;
			this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpHeader.Size = new System.Drawing.Size(246, 30);
			this.tlpHeader.TabIndex = 0;
			// 
			// lbHeaderName
			// 
			this.lbHeaderName.AutoSize = true;
			this.lbHeaderName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbHeaderName.Location = new System.Drawing.Point(4, 1);
			this.lbHeaderName.Name = "lbHeaderName";
			this.lbHeaderName.Size = new System.Drawing.Size(115, 28);
			this.lbHeaderName.TabIndex = 0;
			this.lbHeaderName.Text = "Имя";
			this.lbHeaderName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbHeaderValue
			// 
			this.lbHeaderValue.AutoSize = true;
			this.lbHeaderValue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbHeaderValue.Location = new System.Drawing.Point(126, 1);
			this.lbHeaderValue.Name = "lbHeaderValue";
			this.lbHeaderValue.Size = new System.Drawing.Size(116, 28);
			this.lbHeaderValue.TabIndex = 0;
			this.lbHeaderValue.Text = "Значение";
			this.lbHeaderValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(86, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Visible = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// OperationAttributes
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnScroll);
			this.Controls.Add(this.pnHeader);
			this.Name = "OperationAttributes";
			this.Size = new System.Drawing.Size(246, 386);
			this.pnScroll.ResumeLayout(false);
			this.pnScroll.PerformLayout();
			this.pnHeader.ResumeLayout(false);
			this.tlpHeader.ResumeLayout(false);
			this.tlpHeader.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlpPropertyControls;
		private System.Windows.Forms.Panel pnScroll;
		private System.Windows.Forms.Panel pnHeader;
		private System.Windows.Forms.TableLayoutPanel tlpHeader;
		private System.Windows.Forms.Label lbHeaderName;
		private System.Windows.Forms.Label lbHeaderValue;
		private System.Windows.Forms.Button button1;
	}
}
