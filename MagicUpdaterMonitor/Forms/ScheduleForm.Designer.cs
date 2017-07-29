namespace MagicUpdaterMonitor.Forms
{
	partial class ScheduleForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScheduleForm));
			this.stepsTree1 = new StepTreeControl.StepsTree();
			this.lbTasks = new System.Windows.Forms.ListBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.splitContainerTaskProperties = new System.Windows.Forms.SplitContainer();
			this.btnRefreshGrid = new System.Windows.Forms.Button();
			this.rgvComputersForSheduler = new MagicUpdaterMonitor.Controls.RefreshingGridView();
			this.label5 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.chbEnable = new System.Windows.Forms.CheckBox();
			this.pckrStartDateTime = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbTaskModes = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.edTaskName = new System.Windows.Forms.TextBox();
			this.savePanel = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.labelEditing = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonAddTask = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonRemoveTask = new System.Windows.Forms.ToolStripButton();
			this.label4 = new System.Windows.Forms.Label();
			this.contextMenuStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerTaskProperties)).BeginInit();
			this.splitContainerTaskProperties.Panel1.SuspendLayout();
			this.splitContainerTaskProperties.Panel2.SuspendLayout();
			this.splitContainerTaskProperties.SuspendLayout();
			this.panel1.SuspendLayout();
			this.savePanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// stepsTree1
			// 
			this.stepsTree1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.stepsTree1.Location = new System.Drawing.Point(0, 199);
			this.stepsTree1.Name = "stepsTree1";
			this.stepsTree1.Size = new System.Drawing.Size(380, 531);
			this.stepsTree1.TabIndex = 0;
			// 
			// lbTasks
			// 
			this.lbTasks.ContextMenuStrip = this.contextMenuStrip1;
			this.lbTasks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lbTasks.FormattingEnabled = true;
			this.lbTasks.ItemHeight = 18;
			this.lbTasks.Location = new System.Drawing.Point(0, 53);
			this.lbTasks.Name = "lbTasks";
			this.lbTasks.Size = new System.Drawing.Size(261, 741);
			this.lbTasks.TabIndex = 1;
			this.lbTasks.SelectedIndexChanged += new System.EventHandler(this.lbTasks_SelectedIndexChanged);
			this.lbTasks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbTasks_MouseDown);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTaskToolStripMenuItem,
            this.deleteTaskToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(188, 48);
			// 
			// addTaskToolStripMenuItem
			// 
			this.addTaskToolStripMenuItem.Name = "addTaskToolStripMenuItem";
			this.addTaskToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Insert;
			this.addTaskToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
			this.addTaskToolStripMenuItem.Text = "Добавить задачу";
			this.addTaskToolStripMenuItem.Click += new System.EventHandler(this.addTaskToolStripMenuItem_Click);
			// 
			// deleteTaskToolStripMenuItem
			// 
			this.deleteTaskToolStripMenuItem.Name = "deleteTaskToolStripMenuItem";
			this.deleteTaskToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.deleteTaskToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
			this.deleteTaskToolStripMenuItem.Text = "Удалить задачу";
			this.deleteTaskToolStripMenuItem.Click += new System.EventHandler(this.deleteTaskToolStripMenuItem_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.splitContainerTaskProperties);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(940, 794);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Свойства задачи";
			// 
			// splitContainerTaskProperties
			// 
			this.splitContainerTaskProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerTaskProperties.Location = new System.Drawing.Point(3, 16);
			this.splitContainerTaskProperties.Name = "splitContainerTaskProperties";
			// 
			// splitContainerTaskProperties.Panel1
			// 
			this.splitContainerTaskProperties.Panel1.Controls.Add(this.btnRefreshGrid);
			this.splitContainerTaskProperties.Panel1.Controls.Add(this.rgvComputersForSheduler);
			this.splitContainerTaskProperties.Panel1.Controls.Add(this.label5);
			// 
			// splitContainerTaskProperties.Panel2
			// 
			this.splitContainerTaskProperties.Panel2.Controls.Add(this.stepsTree1);
			this.splitContainerTaskProperties.Panel2.Controls.Add(this.panel1);
			this.splitContainerTaskProperties.Panel2.Controls.Add(this.savePanel);
			this.splitContainerTaskProperties.Size = new System.Drawing.Size(934, 775);
			this.splitContainerTaskProperties.SplitterDistance = 550;
			this.splitContainerTaskProperties.TabIndex = 3;
			// 
			// btnRefreshGrid
			// 
			this.btnRefreshGrid.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnRefreshGrid.Enabled = false;
			this.btnRefreshGrid.Location = new System.Drawing.Point(234, 0);
			this.btnRefreshGrid.Name = "btnRefreshGrid";
			this.btnRefreshGrid.Size = new System.Drawing.Size(83, 23);
			this.btnRefreshGrid.TabIndex = 4;
			this.btnRefreshGrid.Text = "Обновить";
			this.btnRefreshGrid.UseVisualStyleBackColor = true;
			this.btnRefreshGrid.Click += new System.EventHandler(this.btnRefreshGrid_Click);
			// 
			// rgvComputersForSheduler
			// 
			this.rgvComputersForSheduler.BtnClearFiltersVisible = false;
			this.rgvComputersForSheduler.CbSelectAllVisible = false;
			this.rgvComputersForSheduler.ContextFilterMenuItemsVisible = true;
			this.rgvComputersForSheduler.DataSource = null;
			this.rgvComputersForSheduler.DataView = null;
			this.rgvComputersForSheduler.DetailsForm = null;
			this.rgvComputersForSheduler.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rgvComputersForSheduler.ExportToexcelVisible = true;
			this.rgvComputersForSheduler.Filter = null;
			this.rgvComputersForSheduler.IsDetailsEnabled = false;
			this.rgvComputersForSheduler.IsMultiselect = true;
			this.rgvComputersForSheduler.KeyField = null;
			this.rgvComputersForSheduler.LbFilterVisible = true;
			this.rgvComputersForSheduler.Location = new System.Drawing.Point(0, 50);
			this.rgvComputersForSheduler.MappingColumns = null;
			this.rgvComputersForSheduler.Name = "rgvComputersForSheduler";
			this.rgvComputersForSheduler.ResetFilterRadioButton = null;
			this.rgvComputersForSheduler.Size = new System.Drawing.Size(550, 725);
			this.rgvComputersForSheduler.TabIndex = 3;
			// 
			// label5
			// 
			this.label5.Dock = System.Windows.Forms.DockStyle.Top;
			this.label5.Location = new System.Drawing.Point(0, 0);
			this.label5.Name = "label5";
			this.label5.Padding = new System.Windows.Forms.Padding(10);
			this.label5.Size = new System.Drawing.Size(550, 50);
			this.label5.TabIndex = 2;
			this.label5.Text = "Список компьютеров";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.chbEnable);
			this.panel1.Controls.Add(this.pckrStartDateTime);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.cmbTaskModes);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.edTaskName);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(380, 199);
			this.panel1.TabIndex = 7;
			// 
			// chbEnable
			// 
			this.chbEnable.AutoSize = true;
			this.chbEnable.Location = new System.Drawing.Point(9, 170);
			this.chbEnable.Name = "chbEnable";
			this.chbEnable.Size = new System.Drawing.Size(106, 17);
			this.chbEnable.TabIndex = 12;
			this.chbEnable.Text = "Задача активна";
			this.chbEnable.UseVisualStyleBackColor = true;
			// 
			// pckrStartDateTime
			// 
			this.pckrStartDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pckrStartDateTime.Location = new System.Drawing.Point(12, 132);
			this.pckrStartDateTime.Name = "pckrStartDateTime";
			this.pckrStartDateTime.Size = new System.Drawing.Size(361, 20);
			this.pckrStartDateTime.TabIndex = 11;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 116);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Дата / время запуска";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Повтор";
			// 
			// cmbTaskModes
			// 
			this.cmbTaskModes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmbTaskModes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTaskModes.FormattingEnabled = true;
			this.cmbTaskModes.Items.AddRange(new object[] {
            "Один раз",
            "Каждый день",
            "Каждую неделю",
            "Каждый месяц"});
			this.cmbTaskModes.Location = new System.Drawing.Point(12, 76);
			this.cmbTaskModes.Name = "cmbTaskModes";
			this.cmbTaskModes.Size = new System.Drawing.Size(361, 21);
			this.cmbTaskModes.TabIndex = 8;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Название задачи";
			// 
			// edTaskName
			// 
			this.edTaskName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.edTaskName.Location = new System.Drawing.Point(9, 28);
			this.edTaskName.Name = "edTaskName";
			this.edTaskName.Size = new System.Drawing.Size(364, 20);
			this.edTaskName.TabIndex = 6;
			// 
			// savePanel
			// 
			this.savePanel.Controls.Add(this.btnCancel);
			this.savePanel.Controls.Add(this.btnSave);
			this.savePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.savePanel.Location = new System.Drawing.Point(0, 730);
			this.savePanel.Name = "savePanel";
			this.savePanel.Size = new System.Drawing.Size(380, 45);
			this.savePanel.TabIndex = 3;
			// 
			// btnCancel
			// 
			this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnCancel.Location = new System.Drawing.Point(176, 10);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(158, 26);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Visible = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnSave.Location = new System.Drawing.Point(12, 10);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(158, 26);
			this.btnSave.TabIndex = 3;
			this.btnSave.Text = "Сохранить";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.labelEditing);
			this.splitContainer1.Panel1.Controls.Add(this.lbTasks);
			this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
			this.splitContainer1.Panel1.Controls.Add(this.label4);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
			this.splitContainer1.Size = new System.Drawing.Size(1205, 794);
			this.splitContainer1.SplitterDistance = 261;
			this.splitContainer1.TabIndex = 3;
			// 
			// labelEditing
			// 
			this.labelEditing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelEditing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelEditing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelEditing.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.labelEditing.Location = new System.Drawing.Point(12, 255);
			this.labelEditing.Name = "labelEditing";
			this.labelEditing.Size = new System.Drawing.Size(238, 170);
			this.labelEditing.TabIndex = 3;
			this.labelEditing.Text = "Производится добавление \r\nновой записи. Завершите \r\nредактирование полей\r\nи нажми" +
    "те кнопку [Сохранить]. \r\nДля отмены добавления\r\n задачи нажмите кнопку\r\n [Отмена" +
    "]";
			this.labelEditing.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelEditing.Visible = false;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAddTask,
            this.toolStripButtonRemoveTask});
			this.toolStrip1.Location = new System.Drawing.Point(0, 28);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(261, 25);
			this.toolStrip1.TabIndex = 4;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButtonAddTask
			// 
			this.toolStripButtonAddTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonAddTask.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddTask.Image")));
			this.toolStripButtonAddTask.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonAddTask.Name = "toolStripButtonAddTask";
			this.toolStripButtonAddTask.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonAddTask.Text = "toolStripButton1";
			this.toolStripButtonAddTask.ToolTipText = "Добавить новую задачу";
			this.toolStripButtonAddTask.Click += new System.EventHandler(this.toolStripButtonAddTask_Click);
			// 
			// toolStripButtonRemoveTask
			// 
			this.toolStripButtonRemoveTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonRemoveTask.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRemoveTask.Image")));
			this.toolStripButtonRemoveTask.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonRemoveTask.Name = "toolStripButtonRemoveTask";
			this.toolStripButtonRemoveTask.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonRemoveTask.Text = "toolStripButton6";
			this.toolStripButtonRemoveTask.ToolTipText = "Удалить выделенную задачу";
			this.toolStripButtonRemoveTask.Click += new System.EventHandler(this.toolStripButtonRemoveTask_Click);
			// 
			// label4
			// 
			this.label4.Dock = System.Windows.Forms.DockStyle.Top;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(0, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(261, 28);
			this.label4.TabIndex = 2;
			this.label4.Text = "Список задач";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ScheduleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1205, 794);
			this.Controls.Add(this.splitContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ScheduleForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ScheduleForm";
			this.Load += new System.EventHandler(this.ScheduleForm_Load);
			this.contextMenuStrip1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.splitContainerTaskProperties.Panel1.ResumeLayout(false);
			this.splitContainerTaskProperties.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerTaskProperties)).EndInit();
			this.splitContainerTaskProperties.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.savePanel.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

        #endregion

        private StepTreeControl.StepsTree stepsTree1;
        private System.Windows.Forms.ListBox lbTasks;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteTaskToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainerTaskProperties;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chbEnable;
        private System.Windows.Forms.DateTimePicker pckrStartDateTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTaskModes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edTaskName;
        private System.Windows.Forms.Panel savePanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Controls.RefreshingGridView rgvComputersForSheduler;
        private System.Windows.Forms.Label labelEditing;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddTask;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemoveTask;
		private System.Windows.Forms.Button btnRefreshGrid;
	}
}