namespace MagicUpdaterMonitor.Forms
{
	partial class ShedulerFormNew
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShedulerFormNew));
			this.rgvTasks = new MagicUpdaterMonitor.Controls.RefreshingGridView();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
			this.btnEditTask = new System.Windows.Forms.Button();
			this.btnCreateTask = new System.Windows.Forms.Button();
			this.btnDeleteTask = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel7.SuspendLayout();
			this.SuspendLayout();
			// 
			// rgvTasks
			// 
			this.rgvTasks.BaseFilter = "";
			this.rgvTasks.BtnClearFiltersVisible = false;
			this.rgvTasks.CbSelectAllVisible = false;
			this.rgvTasks.ContextFilterMenuItemsVisible = true;
			this.rgvTasks.DataSource = null;
			this.rgvTasks.DataView = null;
			this.rgvTasks.DetailsForm = null;
			this.rgvTasks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rgvTasks.ExportToexcelVisible = false;
			this.rgvTasks.Filter = null;
			this.rgvTasks.IsColumnFilteringEnabled = false;
			this.rgvTasks.IsContextMenuVisible = false;
			this.rgvTasks.IsDetailsEnabled = false;
			this.rgvTasks.IsMultiselect = true;
			this.rgvTasks.IsShowCellExtensionFormByDoubleClick = true;
			this.rgvTasks.KeyField = null;
			this.rgvTasks.LbFilterVisible = true;
			this.rgvTasks.Location = new System.Drawing.Point(3, 3);
			this.rgvTasks.MappingColumns = null;
			this.rgvTasks.Name = "rgvTasks";
			this.rgvTasks.ResetFilterRadioButton = null;
			this.rgvTasks.Size = new System.Drawing.Size(803, 371);
			this.rgvTasks.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel7, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.rgvTasks, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(809, 416);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// tableLayoutPanel7
			// 
			this.tableLayoutPanel7.ColumnCount = 5;
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel7.Controls.Add(this.btnEditTask, 0, 0);
			this.tableLayoutPanel7.Controls.Add(this.btnCreateTask, 0, 0);
			this.tableLayoutPanel7.Controls.Add(this.btnDeleteTask, 3, 0);
			this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 380);
			this.tableLayoutPanel7.Name = "tableLayoutPanel7";
			this.tableLayoutPanel7.RowCount = 1;
			this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel7.Size = new System.Drawing.Size(803, 33);
			this.tableLayoutPanel7.TabIndex = 4;
			// 
			// btnEditTask
			// 
			this.btnEditTask.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnEditTask.Location = new System.Drawing.Point(123, 3);
			this.btnEditTask.Name = "btnEditTask";
			this.btnEditTask.Size = new System.Drawing.Size(114, 23);
			this.btnEditTask.TabIndex = 1;
			this.btnEditTask.Text = "Изменить";
			this.btnEditTask.UseVisualStyleBackColor = true;
			this.btnEditTask.Click += new System.EventHandler(this.btnEditTask_Click);
			// 
			// btnCreateTask
			// 
			this.btnCreateTask.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnCreateTask.Location = new System.Drawing.Point(3, 3);
			this.btnCreateTask.Name = "btnCreateTask";
			this.btnCreateTask.Size = new System.Drawing.Size(114, 23);
			this.btnCreateTask.TabIndex = 0;
			this.btnCreateTask.Text = "Создать";
			this.btnCreateTask.UseVisualStyleBackColor = true;
			this.btnCreateTask.Click += new System.EventHandler(this.btnCreateTask_Click);
			// 
			// btnDeleteTask
			// 
			this.btnDeleteTask.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnDeleteTask.Location = new System.Drawing.Point(363, 3);
			this.btnDeleteTask.Name = "btnDeleteTask";
			this.btnDeleteTask.Size = new System.Drawing.Size(114, 23);
			this.btnDeleteTask.TabIndex = 0;
			this.btnDeleteTask.Text = "Удалить";
			this.btnDeleteTask.UseVisualStyleBackColor = true;
			this.btnDeleteTask.Click += new System.EventHandler(this.btnDeleteTask_Click);
			// 
			// ShedulerFormNew
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(809, 416);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximumSize = new System.Drawing.Size(825, 454);
			this.Name = "ShedulerFormNew";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Задания по расписанию";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel7.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		protected System.Windows.Forms.Button btnEditTask;
		protected System.Windows.Forms.Button btnCreateTask;
		protected System.Windows.Forms.Button btnDeleteTask;
		protected Controls.RefreshingGridView rgvTasks;
		protected System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		protected System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
	}
}