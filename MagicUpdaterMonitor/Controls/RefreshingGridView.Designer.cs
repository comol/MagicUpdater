namespace MagicUpdaterMonitor.Controls
{
	partial class RefreshingGridView
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Details = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lbCount = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.cbSelectAll = new System.Windows.Forms.CheckBox();
			this.lbFilter = new System.Windows.Forms.Label();
			this.btnExportToexcel = new System.Windows.Forms.Button();
			this.btnClearFilters = new System.Windows.Forms.Button();
			this.pbExportToExcel = new System.Windows.Forms.ProgressBar();
			this.contextMenuStripGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miFilterByCurrentValue = new System.Windows.Forms.ToolStripMenuItem();
			this.miResetFilter = new System.Windows.Forms.ToolStripMenuItem();
			this.pnLoading = new System.Windows.Forms.Panel();
			this.lbLoading = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.contextMenuStripGrid.SuspendLayout();
			this.pnLoading.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.AllowUserToOrderColumns = true;
			this.dataGridView.AllowUserToResizeRows = false;
			this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.Details});
			this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView.Location = new System.Drawing.Point(0, 0);
			this.dataGridView.MultiSelect = false;
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new System.Drawing.Size(671, 191);
			this.dataGridView.TabIndex = 0;
			this.dataGridView.DataSourceChanged += new System.EventHandler(this.dataGridView_DataSourceChanged);
			this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
			this.dataGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseClick);
			this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDown);
			this.dataGridView.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseUp);
			this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
			this.dataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_ColumnHeaderMouseClick);
			this.dataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView_CurrentCellDirtyStateChanged);
			this.dataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView_DataBindingComplete);
			this.dataGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_RowHeaderMouseClick);
			this.dataGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView_RowPostPaint);
			this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
			this.dataGridView.Sorted += new System.EventHandler(this.dataGridView_Sorted);
			this.dataGridView.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGridView_Paint);
			this.dataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseDown);
			// 
			// Selected
			// 
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle5.NullValue = false;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
			this.Selected.DefaultCellStyle = dataGridViewCellStyle5;
			this.Selected.HeaderText = "Selected";
			this.Selected.Name = "Selected";
			this.Selected.ReadOnly = true;
			this.Selected.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// Details
			// 
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Red;
			this.Details.DefaultCellStyle = dataGridViewCellStyle6;
			this.Details.HeaderText = "Details";
			this.Details.Name = "Details";
			this.Details.ReadOnly = true;
			this.Details.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.Details.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// lbCount
			// 
			this.lbCount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbCount.AutoSize = true;
			this.lbCount.Location = new System.Drawing.Point(574, 0);
			this.lbCount.Name = "lbCount";
			this.lbCount.Size = new System.Drawing.Size(43, 27);
			this.lbCount.TabIndex = 0;
			this.lbCount.Text = "lbCount";
			this.lbCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
			this.tableLayoutPanel1.ColumnCount = 6;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.lbCount, 4, 0);
			this.tableLayoutPanel1.Controls.Add(this.cbSelectAll, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.lbFilter, 5, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnExportToexcel, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnClearFilters, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.pbExportToExcel, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 191);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(671, 27);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// cbSelectAll
			// 
			this.cbSelectAll.AutoSize = true;
			this.cbSelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.cbSelectAll.Location = new System.Drawing.Point(3, 3);
			this.cbSelectAll.Name = "cbSelectAll";
			this.cbSelectAll.Size = new System.Drawing.Size(48, 17);
			this.cbSelectAll.TabIndex = 1;
			this.cbSelectAll.Text = "Все";
			this.cbSelectAll.UseVisualStyleBackColor = true;
			this.cbSelectAll.Visible = false;
			this.cbSelectAll.CheckStateChanged += new System.EventHandler(this.cbSelectAll_CheckStateChanged);
			// 
			// lbFilter
			// 
			this.lbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbFilter.AutoSize = true;
			this.lbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lbFilter.Location = new System.Drawing.Point(623, 0);
			this.lbFilter.Name = "lbFilter";
			this.lbFilter.Size = new System.Drawing.Size(45, 27);
			this.lbFilter.TabIndex = 0;
			this.lbFilter.Text = "lbFilter";
			this.lbFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnExportToexcel
			// 
			this.btnExportToexcel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnExportToexcel.Location = new System.Drawing.Point(385, 1);
			this.btnExportToexcel.Margin = new System.Windows.Forms.Padding(1);
			this.btnExportToexcel.Name = "btnExportToexcel";
			this.btnExportToexcel.Size = new System.Drawing.Size(71, 25);
			this.btnExportToexcel.TabIndex = 3;
			this.btnExportToexcel.Tag = "0";
			this.btnExportToexcel.Text = "В Excel";
			this.btnExportToexcel.UseVisualStyleBackColor = true;
			this.btnExportToexcel.Click += new System.EventHandler(this.btnExportToexcel_Click);
			// 
			// btnClearFilters
			// 
			this.btnClearFilters.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnClearFilters.Location = new System.Drawing.Point(458, 1);
			this.btnClearFilters.Margin = new System.Windows.Forms.Padding(1);
			this.btnClearFilters.Name = "btnClearFilters";
			this.btnClearFilters.Size = new System.Drawing.Size(112, 25);
			this.btnClearFilters.TabIndex = 2;
			this.btnClearFilters.Text = "Сбросить фильтры";
			this.btnClearFilters.UseVisualStyleBackColor = true;
			this.btnClearFilters.Visible = false;
			this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
			// 
			// pbExportToExcel
			// 
			this.pbExportToExcel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbExportToExcel.Location = new System.Drawing.Point(341, 3);
			this.pbExportToExcel.Maximum = 1000000;
			this.pbExportToExcel.Name = "pbExportToExcel";
			this.pbExportToExcel.Size = new System.Drawing.Size(40, 21);
			this.pbExportToExcel.TabIndex = 4;
			this.pbExportToExcel.Visible = false;
			// 
			// contextMenuStripGrid
			// 
			this.contextMenuStripGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFilterByCurrentValue,
            this.miResetFilter});
			this.contextMenuStripGrid.Name = "contextMenuStripGrid";
			this.contextMenuStripGrid.Size = new System.Drawing.Size(279, 70);
			this.contextMenuStripGrid.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripGrid_Opening);
			// 
			// miFilterByCurrentValue
			// 
			this.miFilterByCurrentValue.CheckOnClick = true;
			this.miFilterByCurrentValue.Image = global::MagicUpdaterMonitor.Images.filter;
			this.miFilterByCurrentValue.Name = "miFilterByCurrentValue";
			this.miFilterByCurrentValue.Size = new System.Drawing.Size(278, 22);
			this.miFilterByCurrentValue.Text = "Фильтровать по текущему значению";
			this.miFilterByCurrentValue.Click += new System.EventHandler(this.miFilterByCurrentValue_Click);
			// 
			// miResetFilter
			// 
			this.miResetFilter.Image = global::MagicUpdaterMonitor.Images.clearFilter;
			this.miResetFilter.Name = "miResetFilter";
			this.miResetFilter.Size = new System.Drawing.Size(278, 22);
			this.miResetFilter.Text = "Сбросить фильтр";
			this.miResetFilter.Click += new System.EventHandler(this.miResetFilter_Click);
			// 
			// pnLoading
			// 
			this.pnLoading.Controls.Add(this.lbLoading);
			this.pnLoading.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnLoading.Location = new System.Drawing.Point(0, 0);
			this.pnLoading.Name = "pnLoading";
			this.pnLoading.Size = new System.Drawing.Size(671, 191);
			this.pnLoading.TabIndex = 2;
			// 
			// lbLoading
			// 
			this.lbLoading.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lbLoading.AutoSize = true;
			this.lbLoading.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lbLoading.Location = new System.Drawing.Point(264, 85);
			this.lbLoading.Name = "lbLoading";
			this.lbLoading.Size = new System.Drawing.Size(144, 29);
			this.lbLoading.TabIndex = 1;
			this.lbLoading.Text = "Загрузка...";
			// 
			// RefreshingGridView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataGridView);
			this.Controls.Add(this.pnLoading);
			this.Controls.Add(this.tableLayoutPanel1);
			this.DoubleBuffered = true;
			this.Name = "RefreshingGridView";
			this.Size = new System.Drawing.Size(671, 218);
			this.Load += new System.EventHandler(this.RefreshingGridView_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.contextMenuStripGrid.ResumeLayout(false);
			this.pnLoading.ResumeLayout(false);
			this.pnLoading.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.Label lbCount;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.CheckBox cbSelectAll;
		private System.Windows.Forms.Button btnClearFilters;
		private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
		private System.Windows.Forms.DataGridViewTextBoxColumn Details;
		private System.Windows.Forms.Label lbFilter;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripGrid;
		private System.Windows.Forms.ToolStripMenuItem miFilterByCurrentValue;
		private System.Windows.Forms.ToolStripMenuItem miResetFilter;
		private System.Windows.Forms.Button btnExportToexcel;
		private System.Windows.Forms.ProgressBar pbExportToExcel;
		private System.Windows.Forms.Panel pnLoading;
		private System.Windows.Forms.Label lbLoading;
	}
}
