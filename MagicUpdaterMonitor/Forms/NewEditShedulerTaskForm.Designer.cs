namespace MagicUpdaterMonitor.Forms
{
	partial class NewEditShedulerTaskForm
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Общие", 0);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Шаги", 0);
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Список компьютеров", 0);
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Расписание", 0);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewEditShedulerTaskForm));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageGeneral = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.tbTaskName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.rtbTaskDescription = new System.Windows.Forms.RichTextBox();
			this.chbTaskEnabled = new System.Windows.Forms.CheckBox();
			this.tabPageSteps = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.label4 = new System.Windows.Forms.Label();
			this.rgvSteps = new MagicUpdaterMonitor.Controls.RefreshingGridView();
			this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
			this.label5 = new System.Windows.Forms.Label();
			this.cbStartStep = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.btnStepMoveUp = new System.Windows.Forms.Button();
			this.bntStepMoveDown = new System.Windows.Forms.Button();
			this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
			this.btnNewStep = new System.Windows.Forms.Button();
			this.btnEditStep = new System.Windows.Forms.Button();
			this.btnDeleteStep = new System.Windows.Forms.Button();
			this.tabPageSchedules = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
			this.label7 = new System.Windows.Forms.Label();
			this.cbSheduleType = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.dtpTimeOnce = new System.Windows.Forms.DateTimePicker();
			this.dtpDateOnce = new System.Windows.Forms.DateTimePicker();
			this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
			this.dtbDateFrequency = new System.Windows.Forms.DateTimePicker();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.lbPeriod = new System.Windows.Forms.Label();
			this.cbOccurs = new System.Windows.Forms.ComboBox();
			this.nudRecurs = new System.Windows.Forms.NumericUpDown();
			this.label16 = new System.Windows.Forms.Label();
			this.dtbTimeFrequency = new System.Windows.Forms.DateTimePicker();
			this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
			this.label19 = new System.Windows.Forms.Label();
			this.rtbSheduleDescription = new System.Windows.Forms.RichTextBox();
			this.tabPageComputersList = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel16 = new System.Windows.Forms.TableLayoutPanel();
			this.rgvComputersList = new MagicUpdaterMonitor.Controls.RefreshingGridView();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.lvPages = new System.Windows.Forms.ListView();
			this.ilPages = new System.Windows.Forms.ImageList(this.components);
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPageGeneral.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.tabPageSteps.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
			this.tableLayoutPanel7.SuspendLayout();
			this.tabPageSchedules.SuspendLayout();
			this.tableLayoutPanel8.SuspendLayout();
			this.tableLayoutPanel9.SuspendLayout();
			this.tableLayoutPanel10.SuspendLayout();
			this.tableLayoutPanel11.SuspendLayout();
			this.tableLayoutPanel12.SuspendLayout();
			this.tableLayoutPanel13.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudRecurs)).BeginInit();
			this.tableLayoutPanel14.SuspendLayout();
			this.tableLayoutPanel15.SuspendLayout();
			this.tabPageComputersList.SuspendLayout();
			this.tableLayoutPanel16.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tabControl1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(782, 514);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPageGeneral);
			this.tabControl1.Controls.Add(this.tabPageSteps);
			this.tabControl1.Controls.Add(this.tabPageSchedules);
			this.tabControl1.Controls.Add(this.tabPageComputersList);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(182, 1);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(599, 467);
			this.tabControl1.TabIndex = 3;
			// 
			// tabPageGeneral
			// 
			this.tabPageGeneral.BackColor = System.Drawing.SystemColors.Control;
			this.tabPageGeneral.Controls.Add(this.tableLayoutPanel4);
			this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabPageGeneral.Margin = new System.Windows.Forms.Padding(0);
			this.tabPageGeneral.Name = "tabPageGeneral";
			this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageGeneral.Size = new System.Drawing.Size(591, 441);
			this.tabPageGeneral.TabIndex = 0;
			this.tabPageGeneral.Text = "Общие";
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.ColumnCount = 2;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.tbTaskName, 1, 0);
			this.tableLayoutPanel4.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel4.Controls.Add(this.rtbTaskDescription, 1, 2);
			this.tableLayoutPanel4.Controls.Add(this.chbTaskEnabled, 0, 1);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 3;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 287F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(585, 435);
			this.tableLayoutPanel4.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 4);
			this.label1.Margin = new System.Windows.Forms.Padding(10, 4, 4, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Имя:";
			// 
			// tbTaskName
			// 
			this.tbTaskName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbTaskName.Location = new System.Drawing.Point(203, 3);
			this.tbTaskName.Name = "tbTaskName";
			this.tbTaskName.Size = new System.Drawing.Size(379, 20);
			this.tbTaskName.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 55);
			this.label3.Margin = new System.Windows.Forms.Padding(10, 4, 4, 4);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Описание:";
			// 
			// rtbTaskDescription
			// 
			this.rtbTaskDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rtbTaskDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbTaskDescription.Location = new System.Drawing.Point(203, 54);
			this.rtbTaskDescription.Name = "rtbTaskDescription";
			this.rtbTaskDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.rtbTaskDescription.Size = new System.Drawing.Size(379, 378);
			this.rtbTaskDescription.TabIndex = 4;
			this.rtbTaskDescription.Text = "";
			// 
			// chbTaskEnabled
			// 
			this.chbTaskEnabled.AutoSize = true;
			this.chbTaskEnabled.Location = new System.Drawing.Point(10, 30);
			this.chbTaskEnabled.Margin = new System.Windows.Forms.Padding(10, 4, 4, 4);
			this.chbTaskEnabled.Name = "chbTaskEnabled";
			this.chbTaskEnabled.Size = new System.Drawing.Size(76, 17);
			this.chbTaskEnabled.TabIndex = 1;
			this.chbTaskEnabled.Text = "Включена";
			this.chbTaskEnabled.UseVisualStyleBackColor = true;
			// 
			// tabPageSteps
			// 
			this.tabPageSteps.BackColor = System.Drawing.SystemColors.Control;
			this.tabPageSteps.Controls.Add(this.tableLayoutPanel5);
			this.tabPageSteps.Location = new System.Drawing.Point(4, 22);
			this.tabPageSteps.Name = "tabPageSteps";
			this.tabPageSteps.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSteps.Size = new System.Drawing.Size(591, 441);
			this.tabPageSteps.TabIndex = 1;
			this.tabPageSteps.Text = "Шаги";
			// 
			// tableLayoutPanel5
			// 
			this.tableLayoutPanel5.ColumnCount = 1;
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel5.Controls.Add(this.label4, 0, 0);
			this.tableLayoutPanel5.Controls.Add(this.rgvSteps, 0, 1);
			this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 0, 2);
			this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel7, 0, 3);
			this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 4;
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
			this.tableLayoutPanel5.Size = new System.Drawing.Size(585, 435);
			this.tableLayoutPanel5.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(8, 8);
			this.label4.Margin = new System.Windows.Forms.Padding(8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(569, 17);
			this.label4.TabIndex = 0;
			this.label4.Text = "Список шагов задачи:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// rgvSteps
			// 
			this.rgvSteps.BaseFilter = "";
			this.rgvSteps.BtnClearFiltersVisible = false;
			this.rgvSteps.CbSelectAllVisible = false;
			this.rgvSteps.ContextFilterMenuItemsVisible = true;
			this.rgvSteps.DataSource = null;
			this.rgvSteps.DataView = null;
			this.rgvSteps.DetailsForm = null;
			this.rgvSteps.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rgvSteps.ExportToexcelVisible = false;
			this.rgvSteps.Filter = null;
			this.rgvSteps.IsColumnFilteringEnabled = false;
			this.rgvSteps.IsContextMenuVisible = false;
			this.rgvSteps.IsDetailsEnabled = false;
			this.rgvSteps.IsMultiselect = false;
			this.rgvSteps.IsShowCellExtensionFormByDoubleClick = false;
			this.rgvSteps.KeyField = null;
			this.rgvSteps.LbFilterVisible = false;
			this.rgvSteps.Location = new System.Drawing.Point(3, 36);
			this.rgvSteps.MappingColumns = null;
			this.rgvSteps.Name = "rgvSteps";
			this.rgvSteps.ResetFilterRadioButton = null;
			this.rgvSteps.Size = new System.Drawing.Size(579, 306);
			this.rgvSteps.TabIndex = 1;
			// 
			// tableLayoutPanel6
			// 
			this.tableLayoutPanel6.ColumnCount = 4;
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel6.Controls.Add(this.label5, 3, 0);
			this.tableLayoutPanel6.Controls.Add(this.cbStartStep, 3, 1);
			this.tableLayoutPanel6.Controls.Add(this.label6, 0, 0);
			this.tableLayoutPanel6.Controls.Add(this.btnStepMoveUp, 0, 1);
			this.tableLayoutPanel6.Controls.Add(this.bntStepMoveDown, 1, 1);
			this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 348);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			this.tableLayoutPanel6.RowCount = 2;
			this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel6.Size = new System.Drawing.Size(579, 46);
			this.tableLayoutPanel6.TabIndex = 2;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(123, 3);
			this.label5.Margin = new System.Windows.Forms.Padding(3);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(87, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "Стартовый шаг:";
			// 
			// cbStartStep
			// 
			this.cbStartStep.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbStartStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbStartStep.FormattingEnabled = true;
			this.cbStartStep.Location = new System.Drawing.Point(123, 22);
			this.cbStartStep.Name = "cbStartStep";
			this.cbStartStep.Size = new System.Drawing.Size(453, 21);
			this.cbStartStep.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.tableLayoutPanel6.SetColumnSpan(this.label6, 3);
			this.label6.Location = new System.Drawing.Point(3, 3);
			this.label6.Margin = new System.Windows.Forms.Padding(3);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "Переместить шаг:";
			this.label6.Visible = false;
			// 
			// btnStepMoveUp
			// 
			this.btnStepMoveUp.BackgroundImage = global::MagicUpdaterMonitor.Images.arrow_up16;
			this.btnStepMoveUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.btnStepMoveUp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnStepMoveUp.Location = new System.Drawing.Point(4, 20);
			this.btnStepMoveUp.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
			this.btnStepMoveUp.Name = "btnStepMoveUp";
			this.btnStepMoveUp.Size = new System.Drawing.Size(32, 25);
			this.btnStepMoveUp.TabIndex = 3;
			this.btnStepMoveUp.UseVisualStyleBackColor = true;
			this.btnStepMoveUp.Visible = false;
			// 
			// bntStepMoveDown
			// 
			this.bntStepMoveDown.BackgroundImage = global::MagicUpdaterMonitor.Images.arrow_down16;
			this.bntStepMoveDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.bntStepMoveDown.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bntStepMoveDown.Location = new System.Drawing.Point(44, 20);
			this.bntStepMoveDown.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
			this.bntStepMoveDown.Name = "bntStepMoveDown";
			this.bntStepMoveDown.Size = new System.Drawing.Size(32, 25);
			this.bntStepMoveDown.TabIndex = 3;
			this.bntStepMoveDown.UseVisualStyleBackColor = true;
			this.bntStepMoveDown.Visible = false;
			// 
			// tableLayoutPanel7
			// 
			this.tableLayoutPanel7.ColumnCount = 5;
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel7.Controls.Add(this.btnNewStep, 0, 0);
			this.tableLayoutPanel7.Controls.Add(this.btnEditStep, 2, 0);
			this.tableLayoutPanel7.Controls.Add(this.btnDeleteStep, 3, 0);
			this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 400);
			this.tableLayoutPanel7.Name = "tableLayoutPanel7";
			this.tableLayoutPanel7.RowCount = 1;
			this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel7.Size = new System.Drawing.Size(579, 32);
			this.tableLayoutPanel7.TabIndex = 3;
			// 
			// btnNewStep
			// 
			this.btnNewStep.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnNewStep.Location = new System.Drawing.Point(3, 3);
			this.btnNewStep.Name = "btnNewStep";
			this.btnNewStep.Size = new System.Drawing.Size(114, 23);
			this.btnNewStep.TabIndex = 0;
			this.btnNewStep.Text = "Новый...";
			this.btnNewStep.UseVisualStyleBackColor = true;
			this.btnNewStep.Click += new System.EventHandler(this.btnNewStep_Click);
			// 
			// btnEditStep
			// 
			this.btnEditStep.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnEditStep.Location = new System.Drawing.Point(243, 3);
			this.btnEditStep.Name = "btnEditStep";
			this.btnEditStep.Size = new System.Drawing.Size(114, 23);
			this.btnEditStep.TabIndex = 0;
			this.btnEditStep.Text = "Изменить";
			this.btnEditStep.UseVisualStyleBackColor = true;
			this.btnEditStep.Click += new System.EventHandler(this.btnEditStep_Click);
			// 
			// btnDeleteStep
			// 
			this.btnDeleteStep.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnDeleteStep.Location = new System.Drawing.Point(363, 3);
			this.btnDeleteStep.Name = "btnDeleteStep";
			this.btnDeleteStep.Size = new System.Drawing.Size(114, 23);
			this.btnDeleteStep.TabIndex = 0;
			this.btnDeleteStep.Text = "Удалить";
			this.btnDeleteStep.UseVisualStyleBackColor = true;
			this.btnDeleteStep.Click += new System.EventHandler(this.btnDeleteStep_Click);
			// 
			// tabPageSchedules
			// 
			this.tabPageSchedules.BackColor = System.Drawing.SystemColors.Control;
			this.tabPageSchedules.Controls.Add(this.tableLayoutPanel8);
			this.tabPageSchedules.Location = new System.Drawing.Point(4, 22);
			this.tabPageSchedules.Name = "tabPageSchedules";
			this.tabPageSchedules.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSchedules.Size = new System.Drawing.Size(591, 441);
			this.tabPageSchedules.TabIndex = 2;
			this.tabPageSchedules.Text = "Расписание";
			// 
			// tableLayoutPanel8
			// 
			this.tableLayoutPanel8.ColumnCount = 1;
			this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel9, 0, 0);
			this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel10, 0, 1);
			this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel11, 0, 2);
			this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel12, 0, 3);
			this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel13, 0, 4);
			this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel14, 0, 5);
			this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel15, 0, 6);
			this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel8.Name = "tableLayoutPanel8";
			this.tableLayoutPanel8.RowCount = 9;
			this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
			this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
			this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 115F));
			this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
			this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 138F));
			this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
			this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
			this.tableLayoutPanel8.Size = new System.Drawing.Size(585, 435);
			this.tableLayoutPanel8.TabIndex = 0;
			// 
			// tableLayoutPanel9
			// 
			this.tableLayoutPanel9.ColumnCount = 2;
			this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
			this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel9.Controls.Add(this.label7, 0, 0);
			this.tableLayoutPanel9.Controls.Add(this.cbSheduleType, 1, 0);
			this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel9.Name = "tableLayoutPanel9";
			this.tableLayoutPanel9.RowCount = 1;
			this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel9.Size = new System.Drawing.Size(579, 28);
			this.tableLayoutPanel9.TabIndex = 1;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 6);
			this.label7.Margin = new System.Windows.Forms.Padding(6);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(92, 13);
			this.label7.TabIndex = 0;
			this.label7.Text = "Тип расписания:";
			// 
			// cbSheduleType
			// 
			this.cbSheduleType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbSheduleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSheduleType.FormattingEnabled = true;
			this.cbSheduleType.Items.AddRange(new object[] {
            "Один раз",
            "Повторение"});
			this.cbSheduleType.Location = new System.Drawing.Point(163, 3);
			this.cbSheduleType.Name = "cbSheduleType";
			this.cbSheduleType.Size = new System.Drawing.Size(413, 21);
			this.cbSheduleType.TabIndex = 1;
			this.cbSheduleType.SelectedIndexChanged += new System.EventHandler(this.cbSheduleType_SelectedIndexChanged);
			// 
			// tableLayoutPanel10
			// 
			this.tableLayoutPanel10.ColumnCount = 2;
			this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134F));
			this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel10.Controls.Add(this.label8, 0, 0);
			this.tableLayoutPanel10.Controls.Add(this.label9, 1, 0);
			this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 37);
			this.tableLayoutPanel10.Name = "tableLayoutPanel10";
			this.tableLayoutPanel10.RowCount = 1;
			this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel10.Size = new System.Drawing.Size(579, 15);
			this.tableLayoutPanel10.TabIndex = 2;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Enabled = false;
			this.label8.Location = new System.Drawing.Point(6, 0);
			this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(121, 13);
			this.label8.TabIndex = 3;
			this.label8.Text = "Одноразовое событие";
			// 
			// label9
			// 
			this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label9.Dock = System.Windows.Forms.DockStyle.Top;
			this.label9.Location = new System.Drawing.Point(137, 6);
			this.label9.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(439, 2);
			this.label9.TabIndex = 4;
			this.label9.Text = "label9";
			// 
			// tableLayoutPanel11
			// 
			this.tableLayoutPanel11.ColumnCount = 5;
			this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
			this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 114F));
			this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142F));
			this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
			this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
			this.tableLayoutPanel11.Controls.Add(this.label10, 0, 0);
			this.tableLayoutPanel11.Controls.Add(this.label11, 2, 0);
			this.tableLayoutPanel11.Controls.Add(this.dtpTimeOnce, 3, 0);
			this.tableLayoutPanel11.Controls.Add(this.dtpDateOnce, 1, 0);
			this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel11.Enabled = false;
			this.tableLayoutPanel11.Location = new System.Drawing.Point(3, 58);
			this.tableLayoutPanel11.Name = "tableLayoutPanel11";
			this.tableLayoutPanel11.RowCount = 1;
			this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel11.Size = new System.Drawing.Size(579, 46);
			this.tableLayoutPanel11.TabIndex = 3;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(6, 6);
			this.label10.Margin = new System.Windows.Forms.Padding(6);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(36, 13);
			this.label10.TabIndex = 0;
			this.label10.Text = "Дата:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(304, 6);
			this.label11.Margin = new System.Windows.Forms.Padding(30, 6, 6, 6);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(43, 13);
			this.label11.TabIndex = 0;
			this.label11.Text = "Время:";
			// 
			// dtpTimeOnce
			// 
			this.dtpTimeOnce.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dtpTimeOnce.Location = new System.Drawing.Point(419, 3);
			this.dtpTimeOnce.Name = "dtpTimeOnce";
			this.dtpTimeOnce.ShowUpDown = true;
			this.dtpTimeOnce.Size = new System.Drawing.Size(92, 20);
			this.dtpTimeOnce.TabIndex = 2;
			this.dtpTimeOnce.ValueChanged += new System.EventHandler(this.dtpTimeOnce_ValueChanged);
			// 
			// dtpDateOnce
			// 
			this.dtpDateOnce.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpDateOnce.Location = new System.Drawing.Point(163, 3);
			this.dtpDateOnce.Name = "dtpDateOnce";
			this.dtpDateOnce.Size = new System.Drawing.Size(108, 20);
			this.dtpDateOnce.TabIndex = 3;
			this.dtpDateOnce.ValueChanged += new System.EventHandler(this.dtpDateOnce_ValueChanged);
			// 
			// tableLayoutPanel12
			// 
			this.tableLayoutPanel12.ColumnCount = 2;
			this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 124F));
			this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel12.Controls.Add(this.label13, 0, 0);
			this.tableLayoutPanel12.Controls.Add(this.label12, 0, 0);
			this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel12.Location = new System.Drawing.Point(3, 110);
			this.tableLayoutPanel12.Name = "tableLayoutPanel12";
			this.tableLayoutPanel12.RowCount = 1;
			this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel12.Size = new System.Drawing.Size(579, 14);
			this.tableLayoutPanel12.TabIndex = 4;
			// 
			// label13
			// 
			this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label13.Dock = System.Windows.Forms.DockStyle.Top;
			this.label13.Location = new System.Drawing.Point(127, 6);
			this.label13.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(449, 2);
			this.label13.TabIndex = 5;
			this.label13.Text = "label13";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Enabled = false;
			this.label12.Location = new System.Drawing.Point(6, 0);
			this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(111, 13);
			this.label12.TabIndex = 4;
			this.label12.Text = "Частота повторения";
			// 
			// tableLayoutPanel13
			// 
			this.tableLayoutPanel13.ColumnCount = 4;
			this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
			this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92F));
			this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel13.Controls.Add(this.dtbDateFrequency, 0, 2);
			this.tableLayoutPanel13.Controls.Add(this.label14, 0, 0);
			this.tableLayoutPanel13.Controls.Add(this.label15, 0, 1);
			this.tableLayoutPanel13.Controls.Add(this.lbPeriod, 2, 1);
			this.tableLayoutPanel13.Controls.Add(this.cbOccurs, 1, 0);
			this.tableLayoutPanel13.Controls.Add(this.nudRecurs, 1, 1);
			this.tableLayoutPanel13.Controls.Add(this.label16, 0, 2);
			this.tableLayoutPanel13.Controls.Add(this.dtbTimeFrequency, 2, 2);
			this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel13.Enabled = false;
			this.tableLayoutPanel13.Location = new System.Drawing.Point(3, 130);
			this.tableLayoutPanel13.Name = "tableLayoutPanel13";
			this.tableLayoutPanel13.RowCount = 3;
			this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
			this.tableLayoutPanel13.Size = new System.Drawing.Size(579, 109);
			this.tableLayoutPanel13.TabIndex = 5;
			// 
			// dtbDateFrequency
			// 
			this.dtbDateFrequency.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtbDateFrequency.Location = new System.Drawing.Point(163, 75);
			this.dtbDateFrequency.Name = "dtbDateFrequency";
			this.dtbDateFrequency.Size = new System.Drawing.Size(94, 20);
			this.dtbDateFrequency.TabIndex = 5;
			this.dtbDateFrequency.ValueChanged += new System.EventHandler(this.dtbDateFrequency_ValueChanged);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(6, 6);
			this.label14.Margin = new System.Windows.Forms.Padding(6);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(84, 13);
			this.label14.TabIndex = 0;
			this.label14.Text = "Срабатывание:";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Enabled = false;
			this.label15.Location = new System.Drawing.Point(6, 42);
			this.label15.Margin = new System.Windows.Forms.Padding(6);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(114, 13);
			this.label15.TabIndex = 0;
			this.label15.Text = "Повторение каджые:";
			// 
			// lbPeriod
			// 
			this.lbPeriod.AutoSize = true;
			this.lbPeriod.Location = new System.Drawing.Point(266, 42);
			this.lbPeriod.Margin = new System.Windows.Forms.Padding(6);
			this.lbPeriod.Name = "lbPeriod";
			this.lbPeriod.Size = new System.Drawing.Size(45, 13);
			this.lbPeriod.TabIndex = 0;
			this.lbPeriod.Text = "lbPeriod";
			this.lbPeriod.Visible = false;
			// 
			// cbOccurs
			// 
			this.tableLayoutPanel13.SetColumnSpan(this.cbOccurs, 2);
			this.cbOccurs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbOccurs.FormattingEnabled = true;
			this.cbOccurs.Location = new System.Drawing.Point(163, 3);
			this.cbOccurs.Name = "cbOccurs";
			this.cbOccurs.Size = new System.Drawing.Size(186, 21);
			this.cbOccurs.TabIndex = 1;
			this.cbOccurs.SelectedIndexChanged += new System.EventHandler(this.cbOccurs_SelectedIndexChanged);
			// 
			// nudRecurs
			// 
			this.nudRecurs.Enabled = false;
			this.nudRecurs.Location = new System.Drawing.Point(163, 39);
			this.nudRecurs.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.nudRecurs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudRecurs.Name = "nudRecurs";
			this.nudRecurs.Size = new System.Drawing.Size(94, 20);
			this.nudRecurs.TabIndex = 2;
			this.nudRecurs.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudRecurs.ValueChanged += new System.EventHandler(this.nudRecurs_ValueChanged);
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(6, 78);
			this.label16.Margin = new System.Windows.Forms.Padding(6);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(112, 13);
			this.label16.TabIndex = 3;
			this.label16.Text = "Начало выполнения:";
			// 
			// dtbTimeFrequency
			// 
			this.dtbTimeFrequency.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dtbTimeFrequency.Location = new System.Drawing.Point(263, 75);
			this.dtbTimeFrequency.Name = "dtbTimeFrequency";
			this.dtbTimeFrequency.ShowUpDown = true;
			this.dtbTimeFrequency.Size = new System.Drawing.Size(86, 20);
			this.dtbTimeFrequency.TabIndex = 4;
			this.dtbTimeFrequency.ValueChanged += new System.EventHandler(this.dtbTimeFrequency_ValueChanged);
			// 
			// tableLayoutPanel14
			// 
			this.tableLayoutPanel14.ColumnCount = 2;
			this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 123F));
			this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel14.Controls.Add(this.label18, 0, 0);
			this.tableLayoutPanel14.Controls.Add(this.label17, 0, 0);
			this.tableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel14.Location = new System.Drawing.Point(3, 245);
			this.tableLayoutPanel14.Name = "tableLayoutPanel14";
			this.tableLayoutPanel14.RowCount = 1;
			this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel14.Size = new System.Drawing.Size(579, 13);
			this.tableLayoutPanel14.TabIndex = 6;
			// 
			// label18
			// 
			this.label18.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label18.Dock = System.Windows.Forms.DockStyle.Top;
			this.label18.Location = new System.Drawing.Point(126, 6);
			this.label18.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(450, 2);
			this.label18.TabIndex = 6;
			this.label18.Text = "label18";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(6, 0);
			this.label17.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(44, 13);
			this.label17.TabIndex = 5;
			this.label17.Text = "Сводка";
			// 
			// tableLayoutPanel15
			// 
			this.tableLayoutPanel15.ColumnCount = 2;
			this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
			this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel15.Controls.Add(this.label19, 0, 0);
			this.tableLayoutPanel15.Controls.Add(this.rtbSheduleDescription, 1, 0);
			this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel15.Location = new System.Drawing.Point(3, 264);
			this.tableLayoutPanel15.Name = "tableLayoutPanel15";
			this.tableLayoutPanel15.RowCount = 1;
			this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel15.Size = new System.Drawing.Size(579, 132);
			this.tableLayoutPanel15.TabIndex = 7;
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(6, 6);
			this.label19.Margin = new System.Windows.Forms.Padding(6);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(60, 13);
			this.label19.TabIndex = 0;
			this.label19.Text = "Описание:";
			// 
			// rtbSheduleDescription
			// 
			this.rtbSheduleDescription.BackColor = System.Drawing.SystemColors.Control;
			this.rtbSheduleDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbSheduleDescription.Location = new System.Drawing.Point(163, 3);
			this.rtbSheduleDescription.Name = "rtbSheduleDescription";
			this.rtbSheduleDescription.ReadOnly = true;
			this.rtbSheduleDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.rtbSheduleDescription.Size = new System.Drawing.Size(413, 126);
			this.rtbSheduleDescription.TabIndex = 1;
			this.rtbSheduleDescription.Text = "";
			// 
			// tabPageComputersList
			// 
			this.tabPageComputersList.BackColor = System.Drawing.SystemColors.Control;
			this.tabPageComputersList.Controls.Add(this.tableLayoutPanel16);
			this.tabPageComputersList.Location = new System.Drawing.Point(4, 22);
			this.tabPageComputersList.Name = "tabPageComputersList";
			this.tabPageComputersList.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageComputersList.Size = new System.Drawing.Size(591, 441);
			this.tabPageComputersList.TabIndex = 3;
			this.tabPageComputersList.Text = "Список компьютеров";
			// 
			// tableLayoutPanel16
			// 
			this.tableLayoutPanel16.ColumnCount = 1;
			this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel16.Controls.Add(this.rgvComputersList, 0, 1);
			this.tableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel16.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel16.Name = "tableLayoutPanel16";
			this.tableLayoutPanel16.RowCount = 2;
			this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
			this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel16.Size = new System.Drawing.Size(585, 435);
			this.tableLayoutPanel16.TabIndex = 0;
			// 
			// rgvComputersList
			// 
			this.rgvComputersList.BaseFilter = "";
			this.rgvComputersList.BtnClearFiltersVisible = false;
			this.rgvComputersList.CbSelectAllVisible = false;
			this.rgvComputersList.ContextFilterMenuItemsVisible = true;
			this.rgvComputersList.DataSource = null;
			this.rgvComputersList.DataView = null;
			this.rgvComputersList.DetailsForm = null;
			this.rgvComputersList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rgvComputersList.ExportToexcelVisible = false;
			this.rgvComputersList.Filter = null;
			this.rgvComputersList.IsColumnFilteringEnabled = true;
			this.rgvComputersList.IsContextMenuVisible = true;
			this.rgvComputersList.IsDetailsEnabled = false;
			this.rgvComputersList.IsMultiselect = true;
			this.rgvComputersList.IsShowCellExtensionFormByDoubleClick = true;
			this.rgvComputersList.KeyField = null;
			this.rgvComputersList.LbFilterVisible = false;
			this.rgvComputersList.Location = new System.Drawing.Point(3, 18);
			this.rgvComputersList.MappingColumns = null;
			this.rgvComputersList.Name = "rgvComputersList";
			this.rgvComputersList.ResetFilterRadioButton = null;
			this.rgvComputersList.Size = new System.Drawing.Size(579, 414);
			this.rgvComputersList.TabIndex = 0;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.lvPages, 0, 1);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(1, 1);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 3;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 186F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(180, 467);
			this.tableLayoutPanel3.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(212)))), ((int)(((byte)(226)))));
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Margin = new System.Windows.Forms.Padding(0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(180, 20);
			this.label2.TabIndex = 0;
			this.label2.Text = "Выберите страницу";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lvPages
			// 
			this.lvPages.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvPages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvPages.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
			this.lvPages.Location = new System.Drawing.Point(0, 20);
			this.lvPages.Margin = new System.Windows.Forms.Padding(0);
			this.lvPages.MultiSelect = false;
			this.lvPages.Name = "lvPages";
			this.lvPages.Size = new System.Drawing.Size(180, 261);
			this.lvPages.SmallImageList = this.ilPages;
			this.lvPages.TabIndex = 2;
			this.lvPages.UseCompatibleStateImageBehavior = false;
			this.lvPages.View = System.Windows.Forms.View.SmallIcon;
			this.lvPages.SelectedIndexChanged += new System.EventHandler(this.lvPages_SelectedIndexChanged);
			// 
			// ilPages
			// 
			this.ilPages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilPages.ImageStream")));
			this.ilPages.TransparentColor = System.Drawing.Color.Transparent;
			this.ilPages.Images.SetKeyName(0, "spanner.png");
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 4;
			this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.tableLayoutPanel2.Controls.Add(this.btnOk, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.btnCancel, 2, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(1, 469);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(780, 44);
			this.tableLayoutPanel2.TabIndex = 4;
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnOk.Location = new System.Drawing.Point(574, 13);
			this.btnOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 8);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(92, 23);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "Сохранить";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnCancel.Location = new System.Drawing.Point(674, 13);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 8);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(92, 23);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// NewEditShedulerTaskForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(782, 514);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(737, 552);
			this.Name = "NewEditShedulerTaskForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Создание/редактирование задания для агентов";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewEditShedulerTaskForm_FormClosing);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPageGeneral.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.tabPageSteps.ResumeLayout(false);
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			this.tableLayoutPanel6.ResumeLayout(false);
			this.tableLayoutPanel6.PerformLayout();
			this.tableLayoutPanel7.ResumeLayout(false);
			this.tabPageSchedules.ResumeLayout(false);
			this.tableLayoutPanel8.ResumeLayout(false);
			this.tableLayoutPanel9.ResumeLayout(false);
			this.tableLayoutPanel9.PerformLayout();
			this.tableLayoutPanel10.ResumeLayout(false);
			this.tableLayoutPanel10.PerformLayout();
			this.tableLayoutPanel11.ResumeLayout(false);
			this.tableLayoutPanel11.PerformLayout();
			this.tableLayoutPanel12.ResumeLayout(false);
			this.tableLayoutPanel12.PerformLayout();
			this.tableLayoutPanel13.ResumeLayout(false);
			this.tableLayoutPanel13.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudRecurs)).EndInit();
			this.tableLayoutPanel14.ResumeLayout(false);
			this.tableLayoutPanel14.PerformLayout();
			this.tableLayoutPanel15.ResumeLayout(false);
			this.tableLayoutPanel15.PerformLayout();
			this.tabPageComputersList.ResumeLayout(false);
			this.tableLayoutPanel16.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListView lvPages;
		private System.Windows.Forms.ImageList ilPages;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageGeneral;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbTaskName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RichTextBox rtbTaskDescription;
		private System.Windows.Forms.CheckBox chbTaskEnabled;
		private System.Windows.Forms.TabPage tabPageSteps;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
		private System.Windows.Forms.Label label4;
		private Controls.RefreshingGridView rgvSteps;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cbStartStep;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnStepMoveUp;
		private System.Windows.Forms.Button bntStepMoveDown;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
		private System.Windows.Forms.Button btnNewStep;
		private System.Windows.Forms.Button btnEditStep;
		private System.Windows.Forms.Button btnDeleteStep;
		private System.Windows.Forms.TabPage tabPageSchedules;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox cbSheduleType;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.DateTimePicker dtpTimeOnce;
		private System.Windows.Forms.DateTimePicker dtpDateOnce;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label lbPeriod;
		private System.Windows.Forms.ComboBox cbOccurs;
		private System.Windows.Forms.NumericUpDown nudRecurs;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.DateTimePicker dtbTimeFrequency;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.RichTextBox rtbSheduleDescription;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TabPage tabPageComputersList;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel16;
		private Controls.RefreshingGridView rgvComputersList;
		private System.Windows.Forms.DateTimePicker dtbDateFrequency;
	}
}