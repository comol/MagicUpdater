namespace MagicUpdaterMonitor.Forms
{
	partial class NewEditShedulerStepForm
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
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Цепочка", 0);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewEditShedulerStepForm));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageGeneral = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.operationAttributes1 = new MagicUpdaterMonitor.Controls.OperationAttributes();
			this.cbOperation = new System.Windows.Forms.ComboBox();
			this.tabPageChain = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.cbSuccess = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.nudCheckOperationResultInterval = new System.Windows.Forms.NumericUpDown();
			this.cbFailure = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.nudError1CRepeatCount = new System.Windows.Forms.NumericUpDown();
			this.nudError1CCheckInterval = new System.Windows.Forms.NumericUpDown();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.lvPages = new System.Windows.Forms.ListView();
			this.ilPages = new System.Windows.Forms.ImageList(this.components);
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.chbTaskEnabled = new System.Windows.Forms.CheckBox();
			this.rtbTaskDescription = new System.Windows.Forms.RichTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tbTaskName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.rgvComputersList = new MagicUpdaterMonitor.Controls.RefreshingGridView();
			this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
			this.btnDeleteStep = new System.Windows.Forms.Button();
			this.btnEditStep = new System.Windows.Forms.Button();
			this.btnInsertStep = new System.Windows.Forms.Button();
			this.btnNewStep = new System.Windows.Forms.Button();
			this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
			this.bntStepMoveDown = new System.Windows.Forms.Button();
			this.btnStepMoveUp = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.cbStartStep = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.rgvSteps = new MagicUpdaterMonitor.Controls.RefreshingGridView();
			this.label4 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPageGeneral.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.tabPageChain.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudCheckOperationResultInterval)).BeginInit();
			this.tableLayoutPanel8.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudError1CRepeatCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudError1CCheckInterval)).BeginInit();
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
			this.tableLayoutPanel1.Size = new System.Drawing.Size(721, 514);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPageGeneral);
			this.tabControl1.Controls.Add(this.tabPageChain);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(182, 1);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(538, 467);
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
			this.tabPageGeneral.Size = new System.Drawing.Size(530, 441);
			this.tabPageGeneral.TabIndex = 0;
			this.tabPageGeneral.Text = "Общие";
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.ColumnCount = 1;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel4.Controls.Add(this.label7, 0, 2);
			this.tableLayoutPanel4.Controls.Add(this.label8, 0, 4);
			this.tableLayoutPanel4.Controls.Add(this.operationAttributes1, 0, 5);
			this.tableLayoutPanel4.Controls.Add(this.cbOperation, 0, 3);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 6;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 260F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(524, 435);
			this.tableLayoutPanel4.TabIndex = 0;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 6);
			this.label7.Margin = new System.Windows.Forms.Padding(6);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(60, 12);
			this.label7.TabIndex = 0;
			this.label7.Text = "Операция:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 68);
			this.label8.Margin = new System.Windows.Forms.Padding(6);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(58, 13);
			this.label8.TabIndex = 0;
			this.label8.Text = "Атрибуты:";
			// 
			// operationAttributes1
			// 
			this.operationAttributes1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.operationAttributes1.Location = new System.Drawing.Point(3, 90);
			this.operationAttributes1.Name = "operationAttributes1";
			this.operationAttributes1.Size = new System.Drawing.Size(518, 342);
			this.operationAttributes1.TabIndex = 2;
			// 
			// cbOperation
			// 
			this.cbOperation.Dock = System.Windows.Forms.DockStyle.Top;
			this.cbOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbOperation.FormattingEnabled = true;
			this.cbOperation.Location = new System.Drawing.Point(3, 27);
			this.cbOperation.Name = "cbOperation";
			this.cbOperation.Size = new System.Drawing.Size(518, 21);
			this.cbOperation.TabIndex = 3;
			this.cbOperation.SelectedValueChanged += new System.EventHandler(this.cbOperation_SelectedValueChanged);
			// 
			// tabPageChain
			// 
			this.tabPageChain.BackColor = System.Drawing.SystemColors.Control;
			this.tabPageChain.Controls.Add(this.tableLayoutPanel5);
			this.tabPageChain.Location = new System.Drawing.Point(4, 22);
			this.tabPageChain.Name = "tabPageChain";
			this.tabPageChain.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageChain.Size = new System.Drawing.Size(530, 441);
			this.tabPageChain.TabIndex = 1;
			this.tabPageChain.Text = "Цепочка";
			// 
			// tableLayoutPanel5
			// 
			this.tableLayoutPanel5.ColumnCount = 1;
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel5.Controls.Add(this.label12, 0, 6);
			this.tableLayoutPanel5.Controls.Add(this.label11, 0, 4);
			this.tableLayoutPanel5.Controls.Add(this.cbSuccess, 0, 1);
			this.tableLayoutPanel5.Controls.Add(this.label9, 0, 0);
			this.tableLayoutPanel5.Controls.Add(this.label10, 0, 2);
			this.tableLayoutPanel5.Controls.Add(this.nudCheckOperationResultInterval, 0, 3);
			this.tableLayoutPanel5.Controls.Add(this.cbFailure, 0, 5);
			this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel8, 0, 7);
			this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 8;
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
			this.tableLayoutPanel5.Size = new System.Drawing.Size(524, 435);
			this.tableLayoutPanel5.TabIndex = 1;
			this.tableLayoutPanel5.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel5_Paint);
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(6, 163);
			this.label12.Margin = new System.Windows.Forms.Padding(6);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(201, 13);
			this.label12.TabIndex = 7;
			this.label12.Text = "Повтор операции в случае ошибки 1С:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(6, 110);
			this.label11.Margin = new System.Windows.Forms.Padding(6);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(97, 13);
			this.label11.TabIndex = 5;
			this.label11.Text = "В случае неудачи:";
			// 
			// cbSuccess
			// 
			this.cbSuccess.Dock = System.Windows.Forms.DockStyle.Top;
			this.cbSuccess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSuccess.FormattingEnabled = true;
			this.cbSuccess.Location = new System.Drawing.Point(3, 29);
			this.cbSuccess.Name = "cbSuccess";
			this.cbSuccess.Size = new System.Drawing.Size(518, 21);
			this.cbSuccess.TabIndex = 3;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6, 6);
			this.label9.Margin = new System.Windows.Forms.Padding(6);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(91, 13);
			this.label9.TabIndex = 0;
			this.label9.Text = "В случае успеха:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(6, 60);
			this.label10.Margin = new System.Windows.Forms.Padding(6);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(280, 13);
			this.label10.TabIndex = 2;
			this.label10.Text = "Интервал проверки результата операции (в минутах):";
			// 
			// nudCheckOperationResultInterval
			// 
			this.nudCheckOperationResultInterval.Location = new System.Drawing.Point(3, 82);
			this.nudCheckOperationResultInterval.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
			this.nudCheckOperationResultInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudCheckOperationResultInterval.Name = "nudCheckOperationResultInterval";
			this.nudCheckOperationResultInterval.Size = new System.Drawing.Size(120, 20);
			this.nudCheckOperationResultInterval.TabIndex = 4;
			this.nudCheckOperationResultInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// cbFailure
			// 
			this.cbFailure.Dock = System.Windows.Forms.DockStyle.Top;
			this.cbFailure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFailure.FormattingEnabled = true;
			this.cbFailure.Location = new System.Drawing.Point(3, 132);
			this.cbFailure.Name = "cbFailure";
			this.cbFailure.Size = new System.Drawing.Size(518, 21);
			this.cbFailure.TabIndex = 6;
			// 
			// tableLayoutPanel8
			// 
			this.tableLayoutPanel8.ColumnCount = 2;
			this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 164F));
			this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel8.Controls.Add(this.label13, 0, 0);
			this.tableLayoutPanel8.Controls.Add(this.label14, 1, 0);
			this.tableLayoutPanel8.Controls.Add(this.nudError1CRepeatCount, 0, 1);
			this.tableLayoutPanel8.Controls.Add(this.nudError1CCheckInterval, 1, 1);
			this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 186);
			this.tableLayoutPanel8.Name = "tableLayoutPanel8";
			this.tableLayoutPanel8.RowCount = 2;
			this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
			this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel8.Size = new System.Drawing.Size(518, 41);
			this.tableLayoutPanel8.TabIndex = 8;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(3, 0);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(119, 13);
			this.label13.TabIndex = 0;
			this.label13.Text = "Количество повторов:";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(167, 0);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(118, 13);
			this.label14.TabIndex = 0;
			this.label14.Text = "Интервал (в минутах):";
			// 
			// nudError1CRepeatCount
			// 
			this.nudError1CRepeatCount.Location = new System.Drawing.Point(3, 19);
			this.nudError1CRepeatCount.Name = "nudError1CRepeatCount";
			this.nudError1CRepeatCount.Size = new System.Drawing.Size(120, 20);
			this.nudError1CRepeatCount.TabIndex = 1;
			// 
			// nudError1CCheckInterval
			// 
			this.nudError1CCheckInterval.Location = new System.Drawing.Point(167, 19);
			this.nudError1CCheckInterval.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
			this.nudError1CCheckInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudError1CCheckInterval.Name = "nudError1CCheckInterval";
			this.nudError1CCheckInterval.Size = new System.Drawing.Size(120, 20);
			this.nudError1CCheckInterval.TabIndex = 1;
			this.nudError1CCheckInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
            listViewItem2});
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
			this.tableLayoutPanel2.Size = new System.Drawing.Size(719, 44);
			this.tableLayoutPanel2.TabIndex = 4;
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnOk.Location = new System.Drawing.Point(513, 13);
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
			this.btnCancel.Location = new System.Drawing.Point(613, 13);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 8);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(92, 23);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
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
			// rtbTaskDescription
			// 
			this.rtbTaskDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rtbTaskDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbTaskDescription.Location = new System.Drawing.Point(265, 54);
			this.rtbTaskDescription.Name = "rtbTaskDescription";
			this.rtbTaskDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.rtbTaskDescription.Size = new System.Drawing.Size(256, 378);
			this.rtbTaskDescription.TabIndex = 4;
			this.rtbTaskDescription.Text = "";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 55);
			this.label3.Margin = new System.Windows.Forms.Padding(10, 4, 4, 4);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 13);
			this.label3.TabIndex = 3;
			// 
			// tbTaskName
			// 
			this.tbTaskName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbTaskName.Location = new System.Drawing.Point(265, 3);
			this.tbTaskName.Name = "tbTaskName";
			this.tbTaskName.Size = new System.Drawing.Size(256, 20);
			this.tbTaskName.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 4);
			this.label1.Margin = new System.Windows.Forms.Padding(10, 4, 4, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 13);
			this.label1.TabIndex = 0;
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
			this.rgvComputersList.Size = new System.Drawing.Size(518, 414);
			this.rgvComputersList.TabIndex = 0;
			// 
			// tableLayoutPanel7
			// 
			this.tableLayoutPanel7.ColumnCount = 5;
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 400);
			this.tableLayoutPanel7.Name = "tableLayoutPanel7";
			this.tableLayoutPanel7.RowCount = 1;
			this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel7.Size = new System.Drawing.Size(518, 32);
			this.tableLayoutPanel7.TabIndex = 3;
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
			// 
			// btnInsertStep
			// 
			this.btnInsertStep.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnInsertStep.Location = new System.Drawing.Point(123, 3);
			this.btnInsertStep.Name = "btnInsertStep";
			this.btnInsertStep.Size = new System.Drawing.Size(114, 23);
			this.btnInsertStep.TabIndex = 0;
			this.btnInsertStep.Text = "Вставить..";
			this.btnInsertStep.UseVisualStyleBackColor = true;
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
			// 
			// tableLayoutPanel6
			// 
			this.tableLayoutPanel6.ColumnCount = 4;
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 348);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			this.tableLayoutPanel6.RowCount = 2;
			this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel6.Size = new System.Drawing.Size(518, 46);
			this.tableLayoutPanel6.TabIndex = 2;
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
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(3, 3);
			this.label6.Margin = new System.Windows.Forms.Padding(3);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 13);
			this.label6.TabIndex = 2;
			// 
			// cbStartStep
			// 
			this.cbStartStep.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbStartStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbStartStep.FormattingEnabled = true;
			this.cbStartStep.Location = new System.Drawing.Point(133, 22);
			this.cbStartStep.Name = "cbStartStep";
			this.cbStartStep.Size = new System.Drawing.Size(382, 21);
			this.cbStartStep.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(133, 3);
			this.label5.Margin = new System.Windows.Forms.Padding(3);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(87, 13);
			this.label5.TabIndex = 0;
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
			this.rgvSteps.IsContextMenuVisible = true;
			this.rgvSteps.IsDetailsEnabled = false;
			this.rgvSteps.IsMultiselect = false;
			this.rgvSteps.IsShowCellExtensionFormByDoubleClick = false;
			this.rgvSteps.KeyField = null;
			this.rgvSteps.LbFilterVisible = false;
			this.rgvSteps.Location = new System.Drawing.Point(3, 36);
			this.rgvSteps.MappingColumns = null;
			this.rgvSteps.Name = "rgvSteps";
			this.rgvSteps.ResetFilterRadioButton = null;
			this.rgvSteps.Size = new System.Drawing.Size(518, 306);
			this.rgvSteps.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(8, 8);
			this.label4.Margin = new System.Windows.Forms.Padding(8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(508, 17);
			this.label4.TabIndex = 0;
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// NewEditShedulerStepForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(721, 514);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(737, 552);
			this.Name = "NewEditShedulerStepForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Создание/редактирование шага";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewEditShedulerStepForm_FormClosing);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPageGeneral.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.tabPageChain.ResumeLayout(false);
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudCheckOperationResultInterval)).EndInit();
			this.tableLayoutPanel8.ResumeLayout(false);
			this.tableLayoutPanel8.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudError1CRepeatCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudError1CCheckInterval)).EndInit();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListView lvPages;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ImageList ilPages;
		private System.Windows.Forms.TabPage tabPageGeneral;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.TabPage tabPageChain;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
		private System.Windows.Forms.CheckBox chbTaskEnabled;
		private System.Windows.Forms.RichTextBox rtbTaskDescription;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbTaskName;
		private System.Windows.Forms.Label label1;
		private Controls.RefreshingGridView rgvComputersList;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
		private System.Windows.Forms.Button btnDeleteStep;
		private System.Windows.Forms.Button btnEditStep;
		private System.Windows.Forms.Button btnInsertStep;
		private System.Windows.Forms.Button btnNewStep;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
		private System.Windows.Forms.Button bntStepMoveDown;
		private System.Windows.Forms.Button btnStepMoveUp;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cbStartStep;
		private System.Windows.Forms.Label label5;
		private Controls.RefreshingGridView rgvSteps;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private Controls.OperationAttributes operationAttributes1;
		private System.Windows.Forms.ComboBox cbOperation;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ComboBox cbSuccess;
		private System.Windows.Forms.NumericUpDown nudCheckOperationResultInterval;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox cbFailure;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.NumericUpDown nudError1CRepeatCount;
		private System.Windows.Forms.NumericUpDown nudError1CCheckInterval;
	}
}