using MagicUpdater.DL;

namespace MagicUpdaterMonitor
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageShops = new System.Windows.Forms.TabPage();
			this.scMain = new System.Windows.Forms.SplitContainer();
			this.scShopsOpers = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.rgvComputers = new MagicUpdaterMonitor.Controls.RefreshingGridView();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.CheckAll = new System.Windows.Forms.ToolStripButton();
			this.UnCheckAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnSendOperation = new System.Windows.Forms.ToolStripButton();
			this.tsddbAdditionalAction = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsmiExecutionToPlan = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.Exchange = new System.Windows.Forms.ToolStripDropDownButton();
			this.вЦентреToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.вУзлеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.RemoteControl = new System.Windows.Forms.ToolStripButton();
			this.CallButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.ErrFilter = new System.Windows.Forms.ToolStripButton();
			this.OffFilter = new System.Windows.Forms.ToolStripButton();
			this.NoFilter = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.Clear = new System.Windows.Forms.ToolStripButton();
			this.Find = new System.Windows.Forms.ToolStripButton();
			this.tbFind = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.ShowOtrs = new System.Windows.Forms.ToolStripButton();
			this.lbCurrentOper = new System.Windows.Forms.Label();
			this.scOpers = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.gbShop = new System.Windows.Forms.GroupBox();
			this.btnCreateShop = new System.Windows.Forms.Button();
			this.rgvShops = new MagicUpdaterMonitor.Controls.RefreshingGridView();
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.LastOperations = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
			this.rgvOperations = new MagicUpdaterMonitor.Controls.RefreshingGridView();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cbPoolDate = new System.Windows.Forms.ComboBox();
			this.rbPoolDate = new System.Windows.Forms.RadioButton();
			this.rbOperationsByAgent = new System.Windows.Forms.RadioButton();
			this.rbOperationsAll = new System.Windows.Forms.RadioButton();
			this.rbOperationsToday = new System.Windows.Forms.RadioButton();
			this.rbOperationsLast = new System.Windows.Forms.RadioButton();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.scSendOpers = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.rgvSendOpers = new MagicUpdaterMonitor.Controls.RefreshingGridView();
			this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
			this.cbOperationGroups = new System.Windows.Forms.ComboBox();
			this.btnAddEditOperationGroup = new System.Windows.Forms.Button();
			this.operationAttributes1 = new MagicUpdaterMonitor.Controls.OperationAttributes();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.lbsUnregistredFiles = new System.Windows.Forms.ListBox();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
			this.tsbRegisterSelectedOperation = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.scErrorsMain = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.gbComputerErrorsLog = new System.Windows.Forms.GroupBox();
			this.rbComputerErrorsLogToday = new System.Windows.Forms.RadioButton();
			this.rbComputerErrorsLogAll = new System.Windows.Forms.RadioButton();
			this.rgvComputerErrorsLog = new MagicUpdaterMonitor.Controls.RefreshingGridView();
			this.cmsOperationGroups = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miCreateOperationGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.miEditOperationGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.miRemoveOperationGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.mainToolStrip = new System.Windows.Forms.ToolStrip();
			this.tsbScheduler = new System.Windows.Forms.ToolStripDropDownButton();
			this.miShedulerForAgents = new System.Windows.Forms.ToolStripMenuItem();
			this.miShedulerFOrServer = new System.Windows.Forms.ToolStripMenuItem();
			this.tsbSettings = new System.Windows.Forms.ToolStripButton();
			this.tsddbSpecialSendOpers = new System.Windows.Forms.ToolStripDropDownButton();
			this.miUpdateAllAgents = new System.Windows.Forms.ToolStripMenuItem();
			this.miSendMainCashbox = new System.Windows.Forms.ToolStripMenuItem();
			this.miSendServer = new System.Windows.Forms.ToolStripMenuItem();
			this.miSendComputers = new System.Windows.Forms.ToolStripMenuItem();
			this.miSendShops = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.miCompleted = new System.Windows.Forms.ToolStripMenuItem();
			this.miErrors = new System.Windows.Forms.ToolStripMenuItem();
			this.miErrors1C = new System.Windows.Forms.ToolStripMenuItem();
			this.miEmty1CLog = new System.Windows.Forms.ToolStripMenuItem();
			this.miCompletedWithEmptyLog = new System.Windows.Forms.ToolStripMenuItem();
			this.tsbAbout = new System.Windows.Forms.ToolStripButton();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.sbStatus = new System.Windows.Forms.StatusStrip();
			this.tslLastVersion = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsbUpdateMU = new System.Windows.Forms.ToolStripDropDownButton();
			this.shopComputerBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.tabControl1.SuspendLayout();
			this.tabPageShops.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
			this.scMain.Panel1.SuspendLayout();
			this.scMain.Panel2.SuspendLayout();
			this.scMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.scShopsOpers)).BeginInit();
			this.scShopsOpers.Panel1.SuspendLayout();
			this.scShopsOpers.Panel2.SuspendLayout();
			this.scShopsOpers.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.scOpers)).BeginInit();
			this.scOpers.Panel1.SuspendLayout();
			this.scOpers.Panel2.SuspendLayout();
			this.scOpers.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.gbShop.SuspendLayout();
			this.tabControl2.SuspendLayout();
			this.LastOperations.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.scSendOpers)).BeginInit();
			this.scSendOpers.Panel1.SuspendLayout();
			this.scSendOpers.Panel2.SuspendLayout();
			this.scSendOpers.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.tableLayoutPanel7.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.scErrorsMain)).BeginInit();
			this.scErrorsMain.Panel1.SuspendLayout();
			this.scErrorsMain.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.gbComputerErrorsLog.SuspendLayout();
			this.cmsOperationGroups.SuspendLayout();
			this.mainToolStrip.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.sbStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.shopComputerBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPageShops);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 25);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1511, 705);
			this.tabControl1.TabIndex = 6;
			// 
			// tabPageShops
			// 
			this.tabPageShops.Controls.Add(this.scMain);
			this.tabPageShops.Location = new System.Drawing.Point(4, 22);
			this.tabPageShops.Name = "tabPageShops";
			this.tabPageShops.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageShops.Size = new System.Drawing.Size(1503, 679);
			this.tabPageShops.TabIndex = 0;
			this.tabPageShops.Text = "Магазины";
			this.tabPageShops.UseVisualStyleBackColor = true;
			// 
			// scMain
			// 
			this.scMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scMain.Location = new System.Drawing.Point(3, 3);
			this.scMain.Name = "scMain";
			// 
			// scMain.Panel1
			// 
			this.scMain.Panel1.Controls.Add(this.scShopsOpers);
			// 
			// scMain.Panel2
			// 
			this.scMain.Panel2.Controls.Add(this.scSendOpers);
			this.scMain.Panel2MinSize = 160;
			this.scMain.Size = new System.Drawing.Size(1497, 673);
			this.scMain.SplitterDistance = 1269;
			this.scMain.SplitterWidth = 6;
			this.scMain.TabIndex = 5;
			// 
			// scShopsOpers
			// 
			this.scShopsOpers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.scShopsOpers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scShopsOpers.Location = new System.Drawing.Point(0, 0);
			this.scShopsOpers.Name = "scShopsOpers";
			this.scShopsOpers.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scShopsOpers.Panel1
			// 
			this.scShopsOpers.Panel1.Controls.Add(this.tableLayoutPanel2);
			this.scShopsOpers.Panel1.Padding = new System.Windows.Forms.Padding(4, 0, 4, 4);
			// 
			// scShopsOpers.Panel2
			// 
			this.scShopsOpers.Panel2.Controls.Add(this.scOpers);
			this.scShopsOpers.Size = new System.Drawing.Size(1269, 673);
			this.scShopsOpers.SplitterDistance = 428;
			this.scShopsOpers.SplitterWidth = 6;
			this.scShopsOpers.TabIndex = 4;
			this.scShopsOpers.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.rgvComputers, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.toolStrip1, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.lbCurrentOper, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 3;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(1259, 422);
			this.tableLayoutPanel2.TabIndex = 4;
			// 
			// rgvComputers
			// 
			this.rgvComputers.BaseFilter = "";
			this.rgvComputers.BtnClearFiltersVisible = false;
			this.rgvComputers.CbSelectAllVisible = false;
			this.rgvComputers.ContextFilterMenuItemsVisible = true;
			this.rgvComputers.DataSource = null;
			this.rgvComputers.DataView = null;
			this.rgvComputers.DetailsForm = null;
			this.rgvComputers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rgvComputers.ExportToexcelVisible = true;
			this.rgvComputers.Filter = null;
			this.rgvComputers.IsColumnFilteringEnabled = true;
			this.rgvComputers.IsContextMenuVisible = true;
			this.rgvComputers.IsDetailsEnabled = false;
			this.rgvComputers.IsMultiselect = true;
			this.rgvComputers.IsShowCellExtensionFormByDoubleClick = true;
			this.rgvComputers.KeyField = null;
			this.rgvComputers.LbFilterVisible = true;
			this.rgvComputers.Location = new System.Drawing.Point(3, 55);
			this.rgvComputers.MappingColumns = null;
			this.rgvComputers.Name = "rgvComputers";
			this.rgvComputers.ResetFilterRadioButton = null;
			this.rgvComputers.Size = new System.Drawing.Size(1253, 364);
			this.rgvComputers.TabIndex = 2;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CheckAll,
            this.UnCheckAll,
            this.toolStripSeparator1,
            this.tsbtnSendOperation,
            this.tsddbAdditionalAction,
            this.toolStripSeparator2,
            this.Exchange,
            this.toolStripSeparator3,
            this.RemoteControl,
            this.CallButton,
            this.toolStripSeparator4,
            this.ErrFilter,
            this.OffFilter,
            this.NoFilter,
            this.toolStripSeparator5,
            this.Clear,
            this.Find,
            this.tbFind,
            this.toolStripLabel1,
            this.ShowOtrs});
			this.toolStrip1.Location = new System.Drawing.Point(0, 26);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(1259, 26);
			this.toolStrip1.TabIndex = 3;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// CheckAll
			// 
			this.CheckAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.CheckAll.Image = ((System.Drawing.Image)(resources.GetObject("CheckAll.Image")));
			this.CheckAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.CheckAll.Name = "CheckAll";
			this.CheckAll.Size = new System.Drawing.Size(23, 23);
			this.CheckAll.Text = "toolStripButton1";
			this.CheckAll.ToolTipText = "Выделить все";
			this.CheckAll.Click += new System.EventHandler(this.CheckAll_Click);
			// 
			// UnCheckAll
			// 
			this.UnCheckAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.UnCheckAll.Image = ((System.Drawing.Image)(resources.GetObject("UnCheckAll.Image")));
			this.UnCheckAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.UnCheckAll.Name = "UnCheckAll";
			this.UnCheckAll.Size = new System.Drawing.Size(23, 23);
			this.UnCheckAll.Text = "toolStripButton1";
			this.UnCheckAll.ToolTipText = "Снять выделение";
			this.UnCheckAll.Click += new System.EventHandler(this.UnCheckAll_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
			// 
			// tsbtnSendOperation
			// 
			this.tsbtnSendOperation.BackColor = System.Drawing.Color.LightGray;
			this.tsbtnSendOperation.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tsbtnSendOperation.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSendOperation.Image")));
			this.tsbtnSendOperation.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnSendOperation.Name = "tsbtnSendOperation";
			this.tsbtnSendOperation.Size = new System.Drawing.Size(288, 23);
			this.tsbtnSendOperation.Text = "Отправить выбранным (Ctrl+Enter)";
			this.tsbtnSendOperation.Click += new System.EventHandler(this.SendOperation_Click);
			// 
			// tsddbAdditionalAction
			// 
			this.tsddbAdditionalAction.BackColor = System.Drawing.Color.LightGray;
			this.tsddbAdditionalAction.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
			this.tsddbAdditionalAction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExecutionToPlan});
			this.tsddbAdditionalAction.Image = ((System.Drawing.Image)(resources.GetObject("tsddbAdditionalAction.Image")));
			this.tsddbAdditionalAction.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsddbAdditionalAction.Name = "tsddbAdditionalAction";
			this.tsddbAdditionalAction.Size = new System.Drawing.Size(13, 23);
			this.tsddbAdditionalAction.Text = "toolStripDropDownButton1";
			this.tsddbAdditionalAction.ToolTipText = "Дополнительные действия";
			this.tsddbAdditionalAction.Visible = false;
			// 
			// tsmiExecutionToPlan
			// 
			this.tsmiExecutionToPlan.Name = "tsmiExecutionToPlan";
			this.tsmiExecutionToPlan.Size = new System.Drawing.Size(230, 22);
			this.tsmiExecutionToPlan.Text = "Запланировать выполнение";
			this.tsmiExecutionToPlan.Click += new System.EventHandler(this.tsmiExecutionToPlan_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
			// 
			// Exchange
			// 
			this.Exchange.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вЦентреToolStripMenuItem,
            this.вУзлеToolStripMenuItem});
			this.Exchange.Image = ((System.Drawing.Image)(resources.GetObject("Exchange.Image")));
			this.Exchange.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Exchange.Name = "Exchange";
			this.Exchange.Size = new System.Drawing.Size(137, 23);
			this.Exchange.Text = "Выполнить обмен";
			// 
			// вЦентреToolStripMenuItem
			// 
			this.вЦентреToolStripMenuItem.Name = "вЦентреToolStripMenuItem";
			this.вЦентреToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.вЦентреToolStripMenuItem.Text = "В центре";
			this.вЦентреToolStripMenuItem.Click += new System.EventHandler(this.вЦентреToolStripMenuItem_Click);
			// 
			// вУзлеToolStripMenuItem
			// 
			this.вУзлеToolStripMenuItem.Name = "вУзлеToolStripMenuItem";
			this.вУзлеToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.вУзлеToolStripMenuItem.Text = "В узле";
			this.вУзлеToolStripMenuItem.Click += new System.EventHandler(this.вУзлеToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
			// 
			// RemoteControl
			// 
			this.RemoteControl.Image = ((System.Drawing.Image)(resources.GetObject("RemoteControl.Image")));
			this.RemoteControl.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.RemoteControl.Name = "RemoteControl";
			this.RemoteControl.Size = new System.Drawing.Size(154, 23);
			this.RemoteControl.Text = "Подключиться (Ctrl+R)";
			this.RemoteControl.Click += new System.EventHandler(this.RemoteControl_Click);
			// 
			// CallButton
			// 
			this.CallButton.Image = ((System.Drawing.Image)(resources.GetObject("CallButton.Image")));
			this.CallButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.CallButton.Name = "CallButton";
			this.CallButton.Size = new System.Drawing.Size(86, 23);
			this.CallButton.Text = "Позвонить";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 26);
			// 
			// ErrFilter
			// 
			this.ErrFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ErrFilter.Image = global::MagicUpdaterMonitor.Images.filterError;
			this.ErrFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ErrFilter.Name = "ErrFilter";
			this.ErrFilter.Size = new System.Drawing.Size(23, 23);
			this.ErrFilter.Text = "toolStripButton1";
			this.ErrFilter.ToolTipText = "Есть ошибка";
			this.ErrFilter.Click += new System.EventHandler(this.ErrFilter_Click);
			// 
			// OffFilter
			// 
			this.OffFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.OffFilter.Image = global::MagicUpdaterMonitor.Images.filterOff;
			this.OffFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.OffFilter.Name = "OffFilter";
			this.OffFilter.Size = new System.Drawing.Size(23, 23);
			this.OffFilter.Text = "toolStripButton2";
			this.OffFilter.ToolTipText = "Выключенные";
			this.OffFilter.Click += new System.EventHandler(this.OffFilter_Click);
			// 
			// NoFilter
			// 
			this.NoFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.NoFilter.Image = global::MagicUpdaterMonitor.Images.clearFilter;
			this.NoFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.NoFilter.Name = "NoFilter";
			this.NoFilter.Size = new System.Drawing.Size(23, 23);
			this.NoFilter.Text = "toolStripButton1";
			this.NoFilter.ToolTipText = "Сбросить фильтр";
			this.NoFilter.Click += new System.EventHandler(this.NoFilter_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 26);
			// 
			// Clear
			// 
			this.Clear.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.Clear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Clear.Image = ((System.Drawing.Image)(resources.GetObject("Clear.Image")));
			this.Clear.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Clear.Name = "Clear";
			this.Clear.Size = new System.Drawing.Size(23, 23);
			this.Clear.Text = "Очистить";
			this.Clear.Click += new System.EventHandler(this.Clear_Click);
			// 
			// Find
			// 
			this.Find.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.Find.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Find.Image = ((System.Drawing.Image)(resources.GetObject("Find.Image")));
			this.Find.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Find.Name = "Find";
			this.Find.Size = new System.Drawing.Size(23, 23);
			this.Find.Text = "Найти";
			this.Find.Click += new System.EventHandler(this.Find_Click);
			// 
			// tbFind
			// 
			this.tbFind.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tbFind.Name = "tbFind";
			this.tbFind.Size = new System.Drawing.Size(100, 26);
			this.tbFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbFind_KeyDown);
			this.tbFind.TextChanged += new System.EventHandler(this.FindTextBox_TextChanged);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(92, 23);
			this.toolStripLabel1.Text = "Найти магазин:";
			// 
			// ShowOtrs
			// 
			this.ShowOtrs.Image = ((System.Drawing.Image)(resources.GetObject("ShowOtrs.Image")));
			this.ShowOtrs.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ShowOtrs.Name = "ShowOtrs";
			this.ShowOtrs.Size = new System.Drawing.Size(65, 23);
			this.ShowOtrs.Text = "Заявки";
			this.ShowOtrs.Click += new System.EventHandler(this.ShowOtrs_Click);
			// 
			// lbCurrentOper
			// 
			this.lbCurrentOper.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbCurrentOper.AutoSize = true;
			this.lbCurrentOper.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lbCurrentOper.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lbCurrentOper.Location = new System.Drawing.Point(3, 0);
			this.lbCurrentOper.Name = "lbCurrentOper";
			this.lbCurrentOper.Size = new System.Drawing.Size(1253, 26);
			this.lbCurrentOper.TabIndex = 4;
			this.lbCurrentOper.Text = "Текущее действие:";
			this.lbCurrentOper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// scOpers
			// 
			this.scOpers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.scOpers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scOpers.Location = new System.Drawing.Point(0, 0);
			this.scOpers.Name = "scOpers";
			// 
			// scOpers.Panel1
			// 
			this.scOpers.Panel1.Controls.Add(this.tableLayoutPanel4);
			this.scOpers.Panel1.Padding = new System.Windows.Forms.Padding(4, 0, 4, 4);
			this.scOpers.Panel1Collapsed = true;
			// 
			// scOpers.Panel2
			// 
			this.scOpers.Panel2.Controls.Add(this.tabControl2);
			this.scOpers.Panel2.Controls.Add(this.tableLayoutPanel3);
			this.scOpers.Panel2.Padding = new System.Windows.Forms.Padding(4, 0, 4, 4);
			this.scOpers.Size = new System.Drawing.Size(1269, 239);
			this.scOpers.SplitterDistance = 277;
			this.scOpers.SplitterWidth = 6;
			this.scOpers.TabIndex = 0;
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.ColumnCount = 1;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Controls.Add(this.gbShop, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.rgvShops, 0, 1);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(4, 0);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 2;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(267, 94);
			this.tableLayoutPanel4.TabIndex = 6;
			// 
			// gbShop
			// 
			this.gbShop.Controls.Add(this.btnCreateShop);
			this.gbShop.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbShop.Location = new System.Drawing.Point(3, 3);
			this.gbShop.Name = "gbShop";
			this.gbShop.Size = new System.Drawing.Size(261, 44);
			this.gbShop.TabIndex = 4;
			this.gbShop.TabStop = false;
			this.gbShop.Text = "Магазины";
			// 
			// btnCreateShop
			// 
			this.btnCreateShop.Location = new System.Drawing.Point(6, 16);
			this.btnCreateShop.Name = "btnCreateShop";
			this.btnCreateShop.Size = new System.Drawing.Size(121, 23);
			this.btnCreateShop.TabIndex = 0;
			this.btnCreateShop.Text = "Создать магазин";
			this.btnCreateShop.UseVisualStyleBackColor = true;
			this.btnCreateShop.Click += new System.EventHandler(this.btnCreateShop_Click);
			// 
			// rgvShops
			// 
			this.rgvShops.BaseFilter = "";
			this.rgvShops.BtnClearFiltersVisible = false;
			this.rgvShops.CbSelectAllVisible = false;
			this.rgvShops.ContextFilterMenuItemsVisible = false;
			this.rgvShops.DataSource = null;
			this.rgvShops.DataView = null;
			this.rgvShops.DetailsForm = null;
			this.rgvShops.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rgvShops.ExportToexcelVisible = false;
			this.rgvShops.Filter = null;
			this.rgvShops.IsColumnFilteringEnabled = true;
			this.rgvShops.IsContextMenuVisible = true;
			this.rgvShops.IsDetailsEnabled = false;
			this.rgvShops.IsMultiselect = true;
			this.rgvShops.IsShowCellExtensionFormByDoubleClick = true;
			this.rgvShops.KeyField = null;
			this.rgvShops.LbFilterVisible = false;
			this.rgvShops.Location = new System.Drawing.Point(3, 53);
			this.rgvShops.MappingColumns = null;
			this.rgvShops.Name = "rgvShops";
			this.rgvShops.ResetFilterRadioButton = null;
			this.rgvShops.Size = new System.Drawing.Size(261, 38);
			this.rgvShops.TabIndex = 5;
			// 
			// tabControl2
			// 
			this.tabControl2.Controls.Add(this.LastOperations);
			this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl2.Location = new System.Drawing.Point(4, 0);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(1259, 233);
			this.tabControl2.TabIndex = 8;
			// 
			// LastOperations
			// 
			this.LastOperations.Controls.Add(this.tableLayoutPanel6);
			this.LastOperations.Location = new System.Drawing.Point(4, 22);
			this.LastOperations.Name = "LastOperations";
			this.LastOperations.Padding = new System.Windows.Forms.Padding(3);
			this.LastOperations.Size = new System.Drawing.Size(1251, 207);
			this.LastOperations.TabIndex = 0;
			this.LastOperations.Text = "Операции";
			this.LastOperations.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel6
			// 
			this.tableLayoutPanel6.ColumnCount = 1;
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel6.Controls.Add(this.rgvOperations, 0, 1);
			this.tableLayoutPanel6.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			this.tableLayoutPanel6.RowCount = 2;
			this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel6.Size = new System.Drawing.Size(1245, 201);
			this.tableLayoutPanel6.TabIndex = 8;
			// 
			// rgvOperations
			// 
			this.rgvOperations.BaseFilter = "";
			this.rgvOperations.BtnClearFiltersVisible = false;
			this.rgvOperations.CbSelectAllVisible = false;
			this.rgvOperations.ContextFilterMenuItemsVisible = true;
			this.rgvOperations.DataSource = null;
			this.rgvOperations.DataView = null;
			this.rgvOperations.DetailsForm = null;
			this.rgvOperations.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rgvOperations.ExportToexcelVisible = true;
			this.rgvOperations.Filter = null;
			this.rgvOperations.IsColumnFilteringEnabled = true;
			this.rgvOperations.IsContextMenuVisible = true;
			this.rgvOperations.IsDetailsEnabled = false;
			this.rgvOperations.IsMultiselect = true;
			this.rgvOperations.IsShowCellExtensionFormByDoubleClick = true;
			this.rgvOperations.KeyField = null;
			this.rgvOperations.LbFilterVisible = true;
			this.rgvOperations.Location = new System.Drawing.Point(3, 38);
			this.rgvOperations.MappingColumns = null;
			this.rgvOperations.Name = "rgvOperations";
			this.rgvOperations.ResetFilterRadioButton = null;
			this.rgvOperations.Size = new System.Drawing.Size(1239, 160);
			this.rgvOperations.TabIndex = 7;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.cbPoolDate);
			this.panel1.Controls.Add(this.rbPoolDate);
			this.panel1.Controls.Add(this.rbOperationsByAgent);
			this.panel1.Controls.Add(this.rbOperationsAll);
			this.panel1.Controls.Add(this.rbOperationsToday);
			this.panel1.Controls.Add(this.rbOperationsLast);
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(981, 29);
			this.panel1.TabIndex = 8;
			// 
			// cbPoolDate
			// 
			this.cbPoolDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPoolDate.Enabled = false;
			this.cbPoolDate.FormattingEnabled = true;
			this.cbPoolDate.Location = new System.Drawing.Point(416, 4);
			this.cbPoolDate.Name = "cbPoolDate";
			this.cbPoolDate.Size = new System.Drawing.Size(216, 21);
			this.cbPoolDate.TabIndex = 5;
			this.cbPoolDate.SelectedValueChanged += new System.EventHandler(this.cbPoolDate_SelectedValueChanged);
			// 
			// rbPoolDate
			// 
			this.rbPoolDate.AutoSize = true;
			this.rbPoolDate.Location = new System.Drawing.Point(319, 5);
			this.rbPoolDate.Name = "rbPoolDate";
			this.rbPoolDate.Size = new System.Drawing.Size(91, 17);
			this.rbPoolDate.TabIndex = 4;
			this.rbPoolDate.TabStop = true;
			this.rbPoolDate.Text = "По дате пула";
			this.rbPoolDate.UseVisualStyleBackColor = true;
			this.rbPoolDate.CheckedChanged += new System.EventHandler(this.rbPoolDate_CheckedChanged);
			// 
			// rbOperationsByAgent
			// 
			this.rbOperationsByAgent.AutoSize = true;
			this.rbOperationsByAgent.Location = new System.Drawing.Point(238, 5);
			this.rbOperationsByAgent.Name = "rbOperationsByAgent";
			this.rbOperationsByAgent.Size = new System.Drawing.Size(75, 17);
			this.rbOperationsByAgent.TabIndex = 3;
			this.rbOperationsByAgent.TabStop = true;
			this.rbOperationsByAgent.Text = "По агенту";
			this.rbOperationsByAgent.UseVisualStyleBackColor = true;
			this.rbOperationsByAgent.CheckedChanged += new System.EventHandler(this.rbOperationsByAgent_CheckedChanged);
			// 
			// rbOperationsAll
			// 
			this.rbOperationsAll.AutoSize = true;
			this.rbOperationsAll.Location = new System.Drawing.Point(188, 5);
			this.rbOperationsAll.Name = "rbOperationsAll";
			this.rbOperationsAll.Size = new System.Drawing.Size(44, 17);
			this.rbOperationsAll.TabIndex = 2;
			this.rbOperationsAll.TabStop = true;
			this.rbOperationsAll.Text = "Все";
			this.rbOperationsAll.UseVisualStyleBackColor = true;
			this.rbOperationsAll.CheckedChanged += new System.EventHandler(this.rbOperationsAll_CheckedChanged);
			// 
			// rbOperationsToday
			// 
			this.rbOperationsToday.AutoSize = true;
			this.rbOperationsToday.Location = new System.Drawing.Point(100, 5);
			this.rbOperationsToday.Name = "rbOperationsToday";
			this.rbOperationsToday.Size = new System.Drawing.Size(82, 17);
			this.rbOperationsToday.TabIndex = 1;
			this.rbOperationsToday.TabStop = true;
			this.rbOperationsToday.Text = "За сегодня";
			this.rbOperationsToday.UseVisualStyleBackColor = true;
			this.rbOperationsToday.CheckedChanged += new System.EventHandler(this.rbOperationsToday_CheckedChanged);
			// 
			// rbOperationsLast
			// 
			this.rbOperationsLast.AutoSize = true;
			this.rbOperationsLast.Location = new System.Drawing.Point(13, 5);
			this.rbOperationsLast.Name = "rbOperationsLast";
			this.rbOperationsLast.Size = new System.Drawing.Size(81, 17);
			this.rbOperationsLast.TabIndex = 0;
			this.rbOperationsLast.TabStop = true;
			this.rbOperationsLast.Text = "Последние";
			this.rbOperationsLast.UseVisualStyleBackColor = true;
			this.rbOperationsLast.CheckedChanged += new System.EventHandler(this.rbOperationsLast_CheckedChanged);
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 233F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(1259, 233);
			this.tableLayoutPanel3.TabIndex = 7;
			// 
			// scSendOpers
			// 
			this.scSendOpers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.scSendOpers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scSendOpers.Location = new System.Drawing.Point(0, 0);
			this.scSendOpers.Name = "scSendOpers";
			this.scSendOpers.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scSendOpers.Panel1
			// 
			this.scSendOpers.Panel1.Controls.Add(this.tableLayoutPanel5);
			// 
			// scSendOpers.Panel2
			// 
			this.scSendOpers.Panel2.Controls.Add(this.operationAttributes1);
			this.scSendOpers.Size = new System.Drawing.Size(222, 673);
			this.scSendOpers.SplitterDistance = 425;
			this.scSendOpers.SplitterWidth = 6;
			this.scSendOpers.TabIndex = 1;
			// 
			// tableLayoutPanel5
			// 
			this.tableLayoutPanel5.ColumnCount = 1;
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel5.Controls.Add(this.rgvSendOpers, 0, 1);
			this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel7, 0, 0);
			this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.Padding = new System.Windows.Forms.Padding(3);
			this.tableLayoutPanel5.RowCount = 2;
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel5.Size = new System.Drawing.Size(220, 423);
			this.tableLayoutPanel5.TabIndex = 0;
			this.tableLayoutPanel5.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel5_Paint);
			// 
			// rgvSendOpers
			// 
			this.rgvSendOpers.BaseFilter = "";
			this.rgvSendOpers.BtnClearFiltersVisible = false;
			this.rgvSendOpers.CbSelectAllVisible = false;
			this.rgvSendOpers.ContextFilterMenuItemsVisible = false;
			this.rgvSendOpers.DataSource = null;
			this.rgvSendOpers.DataView = null;
			this.rgvSendOpers.DetailsForm = null;
			this.rgvSendOpers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rgvSendOpers.ExportToexcelVisible = false;
			this.rgvSendOpers.Filter = null;
			this.rgvSendOpers.IsColumnFilteringEnabled = true;
			this.rgvSendOpers.IsContextMenuVisible = true;
			this.rgvSendOpers.IsDetailsEnabled = false;
			this.rgvSendOpers.IsMultiselect = true;
			this.rgvSendOpers.IsShowCellExtensionFormByDoubleClick = true;
			this.rgvSendOpers.KeyField = null;
			this.rgvSendOpers.LbFilterVisible = false;
			this.rgvSendOpers.Location = new System.Drawing.Point(6, 35);
			this.rgvSendOpers.MappingColumns = null;
			this.rgvSendOpers.Name = "rgvSendOpers";
			this.rgvSendOpers.ResetFilterRadioButton = null;
			this.rgvSendOpers.Size = new System.Drawing.Size(208, 384);
			this.rgvSendOpers.TabIndex = 2;
			// 
			// tableLayoutPanel7
			// 
			this.tableLayoutPanel7.ColumnCount = 2;
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel7.Controls.Add(this.cbOperationGroups, 0, 0);
			this.tableLayoutPanel7.Controls.Add(this.btnAddEditOperationGroup, 1, 0);
			this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel7.Name = "tableLayoutPanel7";
			this.tableLayoutPanel7.RowCount = 1;
			this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel7.Size = new System.Drawing.Size(214, 27);
			this.tableLayoutPanel7.TabIndex = 3;
			// 
			// cbOperationGroups
			// 
			this.cbOperationGroups.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbOperationGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbOperationGroups.FormattingEnabled = true;
			this.cbOperationGroups.Location = new System.Drawing.Point(3, 3);
			this.cbOperationGroups.Name = "cbOperationGroups";
			this.cbOperationGroups.Size = new System.Drawing.Size(178, 21);
			this.cbOperationGroups.TabIndex = 5;
			this.cbOperationGroups.SelectedValueChanged += new System.EventHandler(this.cbOperationGroups_SelectedValueChanged);
			// 
			// btnAddEditOperationGroup
			// 
			this.btnAddEditOperationGroup.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnAddEditOperationGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnAddEditOperationGroup.Location = new System.Drawing.Point(184, 2);
			this.btnAddEditOperationGroup.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
			this.btnAddEditOperationGroup.Name = "btnAddEditOperationGroup";
			this.btnAddEditOperationGroup.Size = new System.Drawing.Size(30, 23);
			this.btnAddEditOperationGroup.TabIndex = 6;
			this.btnAddEditOperationGroup.Text = "+";
			this.btnAddEditOperationGroup.UseVisualStyleBackColor = true;
			this.btnAddEditOperationGroup.Click += new System.EventHandler(this.btnAddEditOperationGroup_Click);
			// 
			// operationAttributes1
			// 
			this.operationAttributes1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.operationAttributes1.Location = new System.Drawing.Point(0, 0);
			this.operationAttributes1.Name = "operationAttributes1";
			this.operationAttributes1.Size = new System.Drawing.Size(220, 240);
			this.operationAttributes1.TabIndex = 0;
			// 
			// tabPage3
			// 
			this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage3.Controls.Add(this.lbsUnregistredFiles);
			this.tabPage3.Controls.Add(this.toolStrip2);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(5);
			this.tabPage3.Size = new System.Drawing.Size(1503, 679);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Плагин-операции";
			// 
			// lbsUnregistredFiles
			// 
			this.lbsUnregistredFiles.Dock = System.Windows.Forms.DockStyle.Top;
			this.lbsUnregistredFiles.FormattingEnabled = true;
			this.lbsUnregistredFiles.Location = new System.Drawing.Point(5, 30);
			this.lbsUnregistredFiles.Name = "lbsUnregistredFiles";
			this.lbsUnregistredFiles.Size = new System.Drawing.Size(1493, 277);
			this.lbsUnregistredFiles.TabIndex = 0;
			this.lbsUnregistredFiles.SelectedValueChanged += new System.EventHandler(this.lbsUnregistredFiles_SelectedValueChanged);
			// 
			// toolStrip2
			// 
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefresh,
            this.tsbRegisterSelectedOperation,
            this.toolStripButton1});
			this.toolStrip2.Location = new System.Drawing.Point(5, 5);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new System.Drawing.Size(1493, 25);
			this.toolStrip2.TabIndex = 4;
			this.toolStrip2.Text = "toolStrip2";
			// 
			// tsbRefresh
			// 
			this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresh.Image")));
			this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRefresh.Name = "tsbRefresh";
			this.tsbRefresh.Size = new System.Drawing.Size(65, 22);
			this.tsbRefresh.Text = "Обновить";
			this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
			// 
			// tsbRegisterSelectedOperation
			// 
			this.tsbRegisterSelectedOperation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbRegisterSelectedOperation.Image = ((System.Drawing.Image)(resources.GetObject("tsbRegisterSelectedOperation.Image")));
			this.tsbRegisterSelectedOperation.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRegisterSelectedOperation.Name = "tsbRegisterSelectedOperation";
			this.tsbRegisterSelectedOperation.Size = new System.Drawing.Size(239, 22);
			this.tsbRegisterSelectedOperation.Text = "Зарегистрировать выбранную операцию";
			this.tsbRegisterSelectedOperation.Click += new System.EventHandler(this.tsbRegisterSelectedOperation_Click);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(189, 22);
			this.toolStripButton1.Text = "Зарегистрировать все операции";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.scErrorsMain);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(1503, 679);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Ошибки";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// scErrorsMain
			// 
			this.scErrorsMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.scErrorsMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scErrorsMain.Location = new System.Drawing.Point(3, 3);
			this.scErrorsMain.Name = "scErrorsMain";
			this.scErrorsMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scErrorsMain.Panel1
			// 
			this.scErrorsMain.Panel1.Controls.Add(this.tableLayoutPanel1);
			this.scErrorsMain.Size = new System.Drawing.Size(1497, 673);
			this.scErrorsMain.SplitterDistance = 335;
			this.scErrorsMain.SplitterWidth = 6;
			this.scErrorsMain.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.gbComputerErrorsLog, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.rgvComputerErrorsLog, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1495, 333);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// gbComputerErrorsLog
			// 
			this.gbComputerErrorsLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbComputerErrorsLog.Controls.Add(this.rbComputerErrorsLogToday);
			this.gbComputerErrorsLog.Controls.Add(this.rbComputerErrorsLogAll);
			this.gbComputerErrorsLog.Location = new System.Drawing.Point(3, 3);
			this.gbComputerErrorsLog.Name = "gbComputerErrorsLog";
			this.gbComputerErrorsLog.Size = new System.Drawing.Size(1489, 44);
			this.gbComputerErrorsLog.TabIndex = 4;
			this.gbComputerErrorsLog.TabStop = false;
			this.gbComputerErrorsLog.Text = "Ошибки с компьютеров";
			// 
			// rbComputerErrorsLogToday
			// 
			this.rbComputerErrorsLogToday.AutoSize = true;
			this.rbComputerErrorsLogToday.Checked = true;
			this.rbComputerErrorsLogToday.Location = new System.Drawing.Point(6, 19);
			this.rbComputerErrorsLogToday.Name = "rbComputerErrorsLogToday";
			this.rbComputerErrorsLogToday.Size = new System.Drawing.Size(82, 17);
			this.rbComputerErrorsLogToday.TabIndex = 1;
			this.rbComputerErrorsLogToday.TabStop = true;
			this.rbComputerErrorsLogToday.Text = "За сегодня";
			this.rbComputerErrorsLogToday.UseVisualStyleBackColor = true;
			this.rbComputerErrorsLogToday.CheckedChanged += new System.EventHandler(this.rbComputerErrorsLogToday_CheckedChanged);
			// 
			// rbComputerErrorsLogAll
			// 
			this.rbComputerErrorsLogAll.AutoSize = true;
			this.rbComputerErrorsLogAll.Location = new System.Drawing.Point(101, 19);
			this.rbComputerErrorsLogAll.Name = "rbComputerErrorsLogAll";
			this.rbComputerErrorsLogAll.Size = new System.Drawing.Size(44, 17);
			this.rbComputerErrorsLogAll.TabIndex = 0;
			this.rbComputerErrorsLogAll.Text = "Все";
			this.rbComputerErrorsLogAll.UseVisualStyleBackColor = true;
			this.rbComputerErrorsLogAll.CheckedChanged += new System.EventHandler(this.rbComputerErrorsLogAll_CheckedChanged);
			// 
			// rgvComputerErrorsLog
			// 
			this.rgvComputerErrorsLog.BaseFilter = "";
			this.rgvComputerErrorsLog.BtnClearFiltersVisible = false;
			this.rgvComputerErrorsLog.CbSelectAllVisible = false;
			this.rgvComputerErrorsLog.ContextFilterMenuItemsVisible = false;
			this.rgvComputerErrorsLog.DataSource = null;
			this.rgvComputerErrorsLog.DataView = null;
			this.rgvComputerErrorsLog.DetailsForm = null;
			this.rgvComputerErrorsLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rgvComputerErrorsLog.ExportToexcelVisible = false;
			this.rgvComputerErrorsLog.Filter = null;
			this.rgvComputerErrorsLog.IsColumnFilteringEnabled = true;
			this.rgvComputerErrorsLog.IsContextMenuVisible = true;
			this.rgvComputerErrorsLog.IsDetailsEnabled = false;
			this.rgvComputerErrorsLog.IsMultiselect = true;
			this.rgvComputerErrorsLog.IsShowCellExtensionFormByDoubleClick = true;
			this.rgvComputerErrorsLog.KeyField = null;
			this.rgvComputerErrorsLog.LbFilterVisible = false;
			this.rgvComputerErrorsLog.Location = new System.Drawing.Point(3, 53);
			this.rgvComputerErrorsLog.MappingColumns = null;
			this.rgvComputerErrorsLog.Name = "rgvComputerErrorsLog";
			this.rgvComputerErrorsLog.ResetFilterRadioButton = null;
			this.rgvComputerErrorsLog.Size = new System.Drawing.Size(1489, 277);
			this.rgvComputerErrorsLog.TabIndex = 5;
			// 
			// cmsOperationGroups
			// 
			this.cmsOperationGroups.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCreateOperationGroup,
            this.miEditOperationGroup,
            this.miRemoveOperationGroup});
			this.cmsOperationGroups.Name = "cmsOperationGroups";
			this.cmsOperationGroups.Size = new System.Drawing.Size(196, 70);
			// 
			// miCreateOperationGroup
			// 
			this.miCreateOperationGroup.Name = "miCreateOperationGroup";
			this.miCreateOperationGroup.Size = new System.Drawing.Size(195, 22);
			this.miCreateOperationGroup.Text = "Создать группу";
			this.miCreateOperationGroup.Click += new System.EventHandler(this.miCreateOperationGroup_Click);
			// 
			// miEditOperationGroup
			// 
			this.miEditOperationGroup.Name = "miEditOperationGroup";
			this.miEditOperationGroup.Size = new System.Drawing.Size(195, 22);
			this.miEditOperationGroup.Text = "Редактировать группу";
			this.miEditOperationGroup.Click += new System.EventHandler(this.miEditOperationGroup_Click);
			// 
			// miRemoveOperationGroup
			// 
			this.miRemoveOperationGroup.Name = "miRemoveOperationGroup";
			this.miRemoveOperationGroup.Size = new System.Drawing.Size(195, 22);
			this.miRemoveOperationGroup.Text = "Удалить группу";
			this.miRemoveOperationGroup.Click += new System.EventHandler(this.miRemoveOperationGroup_Click);
			// 
			// mainToolStrip
			// 
			this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbScheduler,
            this.tsbSettings,
            this.tsddbSpecialSendOpers,
            this.tsbAbout});
			this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
			this.mainToolStrip.Name = "mainToolStrip";
			this.mainToolStrip.Size = new System.Drawing.Size(1511, 25);
			this.mainToolStrip.TabIndex = 3;
			this.mainToolStrip.Text = "toolStrip1";
			// 
			// tsbScheduler
			// 
			this.tsbScheduler.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbScheduler.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miShedulerForAgents,
            this.miShedulerFOrServer});
			this.tsbScheduler.Image = ((System.Drawing.Image)(resources.GetObject("tsbScheduler.Image")));
			this.tsbScheduler.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbScheduler.Name = "tsbScheduler";
			this.tsbScheduler.Size = new System.Drawing.Size(100, 22);
			this.tsbScheduler.Text = "Планировщик";
			// 
			// miShedulerForAgents
			// 
			this.miShedulerForAgents.Name = "miShedulerForAgents";
			this.miShedulerForAgents.Size = new System.Drawing.Size(142, 22);
			this.miShedulerForAgents.Text = "Для агентов";
			this.miShedulerForAgents.Click += new System.EventHandler(this.miShedulerForAgents_Click);
			// 
			// miShedulerFOrServer
			// 
			this.miShedulerFOrServer.Name = "miShedulerFOrServer";
			this.miShedulerFOrServer.Size = new System.Drawing.Size(142, 22);
			this.miShedulerFOrServer.Text = "Для сервера";
			this.miShedulerFOrServer.Click += new System.EventHandler(this.miShedulerFOrServer_Click);
			// 
			// tsbSettings
			// 
			this.tsbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbSettings.Image = ((System.Drawing.Image)(resources.GetObject("tsbSettings.Image")));
			this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSettings.Name = "tsbSettings";
			this.tsbSettings.Size = new System.Drawing.Size(71, 22);
			this.tsbSettings.Text = "Настройки";
			this.tsbSettings.Click += new System.EventHandler(this.tsbSettings_Click_1);
			// 
			// tsddbSpecialSendOpers
			// 
			this.tsddbSpecialSendOpers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsddbSpecialSendOpers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miUpdateAllAgents,
            this.miSendMainCashbox,
            this.miSendServer,
            this.miSendComputers,
            this.miSendShops,
            this.toolStripMenuItem1,
            this.miCompleted,
            this.miErrors,
            this.miErrors1C,
            this.miEmty1CLog,
            this.miCompletedWithEmptyLog});
			this.tsddbSpecialSendOpers.Image = ((System.Drawing.Image)(resources.GetObject("tsddbSpecialSendOpers.Image")));
			this.tsddbSpecialSendOpers.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsddbSpecialSendOpers.Name = "tsddbSpecialSendOpers";
			this.tsddbSpecialSendOpers.Size = new System.Drawing.Size(203, 22);
			this.tsddbSpecialSendOpers.Text = "Специальная отправка операций";
			// 
			// miUpdateAllAgents
			// 
			this.miUpdateAllAgents.Name = "miUpdateAllAgents";
			this.miUpdateAllAgents.Size = new System.Drawing.Size(303, 22);
			this.miUpdateAllAgents.Text = "Обновить все агенты";
			this.miUpdateAllAgents.Click += new System.EventHandler(this.miUpdateAllAgents_Click);
			// 
			// miSendMainCashbox
			// 
			this.miSendMainCashbox.Name = "miSendMainCashbox";
			this.miSendMainCashbox.Size = new System.Drawing.Size(303, 22);
			this.miSendMainCashbox.Text = "Отправить включенным главным кассам";
			this.miSendMainCashbox.Click += new System.EventHandler(this.miSendMainCashbox_Click);
			// 
			// miSendServer
			// 
			this.miSendServer.Name = "miSendServer";
			this.miSendServer.Size = new System.Drawing.Size(303, 22);
			this.miSendServer.Text = "Отправить включенным серверам";
			this.miSendServer.Click += new System.EventHandler(this.miSendServer_Click);
			// 
			// miSendComputers
			// 
			this.miSendComputers.Name = "miSendComputers";
			this.miSendComputers.Size = new System.Drawing.Size(303, 22);
			this.miSendComputers.Text = "Отправить компьютерам";
			this.miSendComputers.Click += new System.EventHandler(this.miSendComputers_Click);
			// 
			// miSendShops
			// 
			this.miSendShops.Name = "miSendShops";
			this.miSendShops.Size = new System.Drawing.Size(303, 22);
			this.miSendShops.Text = "Отправить магазинам";
			this.miSendShops.Click += new System.EventHandler(this.miSendShops_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(300, 6);
			// 
			// miCompleted
			// 
			this.miCompleted.Name = "miCompleted";
			this.miCompleted.Size = new System.Drawing.Size(303, 22);
			this.miCompleted.Text = "Выполненные";
			this.miCompleted.Click += new System.EventHandler(this.miCompleted_Click);
			// 
			// miErrors
			// 
			this.miErrors.Name = "miErrors";
			this.miErrors.Size = new System.Drawing.Size(303, 22);
			this.miErrors.Text = "Ошибочные";
			this.miErrors.Click += new System.EventHandler(this.miErrors_Click);
			// 
			// miErrors1C
			// 
			this.miErrors1C.Name = "miErrors1C";
			this.miErrors1C.Size = new System.Drawing.Size(303, 22);
			this.miErrors1C.Text = "С ошибкой 1С";
			this.miErrors1C.Click += new System.EventHandler(this.miErrors1C_Click);
			// 
			// miEmty1CLog
			// 
			this.miEmty1CLog.Name = "miEmty1CLog";
			this.miEmty1CLog.Size = new System.Drawing.Size(303, 22);
			this.miEmty1CLog.Text = "С пустым логом 1С";
			this.miEmty1CLog.Click += new System.EventHandler(this.miEmty1CLog_Click);
			// 
			// miCompletedWithEmptyLog
			// 
			this.miCompletedWithEmptyLog.Name = "miCompletedWithEmptyLog";
			this.miCompletedWithEmptyLog.Size = new System.Drawing.Size(303, 22);
			this.miCompletedWithEmptyLog.Text = "Выполненные с пустым логом";
			this.miCompletedWithEmptyLog.Click += new System.EventHandler(this.miCompletedWithEmptyLog_Click);
			// 
			// tsbAbout
			// 
			this.tsbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbAbout.Name = "tsbAbout";
			this.tsbAbout.Size = new System.Drawing.Size(86, 22);
			this.tsbAbout.Text = "О программе";
			this.tsbAbout.Click += new System.EventHandler(this.tsbAbout_Click);
			// 
			// toolStripContainer1
			// 
			this.toolStripContainer1.BottomToolStripPanelVisible = false;
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1511, 705);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.LeftToolStripPanelVisible = false;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.RightToolStripPanelVisible = false;
			this.toolStripContainer1.Size = new System.Drawing.Size(1511, 730);
			this.toolStripContainer1.TabIndex = 3;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// sbStatus
			// 
			this.sbStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslLastVersion,
            this.tsbUpdateMU});
			this.sbStatus.Location = new System.Drawing.Point(0, 730);
			this.sbStatus.Name = "sbStatus";
			this.sbStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.sbStatus.Size = new System.Drawing.Size(1511, 22);
			this.sbStatus.TabIndex = 8;
			this.sbStatus.Text = "statusStrip1";
			// 
			// tslLastVersion
			// 
			this.tslLastVersion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.tslLastVersion.Name = "tslLastVersion";
			this.tslLastVersion.Size = new System.Drawing.Size(84, 17);
			this.tslLastVersion.Text = "tslLastVersion";
			// 
			// tsbUpdateMU
			// 
			this.tsbUpdateMU.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbUpdateMU.Enabled = false;
			this.tsbUpdateMU.Image = ((System.Drawing.Image)(resources.GetObject("tsbUpdateMU.Image")));
			this.tsbUpdateMU.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbUpdateMU.Name = "tsbUpdateMU";
			this.tsbUpdateMU.ShowDropDownArrow = false;
			this.tsbUpdateMU.Size = new System.Drawing.Size(262, 20);
			this.tsbUpdateMU.Text = "Обновить MagicUpdater на всех компьютерах";
			this.tsbUpdateMU.Visible = false;
			this.tsbUpdateMU.Click += new System.EventHandler(this.tsbUpdateMU_Click);
			// 
			// shopComputerBindingSource
			// 
			this.shopComputerBindingSource.DataSource = typeof(MagicUpdater.DL.ShopComputer);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(1511, 752);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.mainToolStrip);
			this.Controls.Add(this.toolStripContainer1);
			this.Controls.Add(this.sbStatus);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Монитор обновления 1С";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
			this.tabControl1.ResumeLayout(false);
			this.tabPageShops.ResumeLayout(false);
			this.scMain.Panel1.ResumeLayout(false);
			this.scMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
			this.scMain.ResumeLayout(false);
			this.scShopsOpers.Panel1.ResumeLayout(false);
			this.scShopsOpers.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scShopsOpers)).EndInit();
			this.scShopsOpers.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.scOpers.Panel1.ResumeLayout(false);
			this.scOpers.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scOpers)).EndInit();
			this.scOpers.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.gbShop.ResumeLayout(false);
			this.tabControl2.ResumeLayout(false);
			this.LastOperations.ResumeLayout(false);
			this.tableLayoutPanel6.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.scSendOpers.Panel1.ResumeLayout(false);
			this.scSendOpers.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scSendOpers)).EndInit();
			this.scSendOpers.ResumeLayout(false);
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel7.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.scErrorsMain.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scErrorsMain)).EndInit();
			this.scErrorsMain.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.gbComputerErrorsLog.ResumeLayout(false);
			this.gbComputerErrorsLog.PerformLayout();
			this.cmsOperationGroups.ResumeLayout(false);
			this.mainToolStrip.ResumeLayout(false);
			this.mainToolStrip.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.sbStatus.ResumeLayout(false);
			this.sbStatus.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.shopComputerBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.BindingSource shopComputerBindingSource;
		private System.Windows.Forms.SplitContainer scShopsOpers;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageShops;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.GroupBox gbShop;
		private Controls.RefreshingGridView rgvShops;
		private System.Windows.Forms.SplitContainer scOpers;
		private System.Windows.Forms.SplitContainer scMain;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
		private Controls.RefreshingGridView rgvSendOpers;
		private System.Windows.Forms.StatusStrip sbStatus;
		private System.Windows.Forms.ToolStripStatusLabel tslLastVersion;
		private System.Windows.Forms.ToolStripDropDownButton tsbUpdateMU;
		private System.Windows.Forms.SplitContainer scErrorsMain;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.GroupBox gbComputerErrorsLog;
		private System.Windows.Forms.RadioButton rbComputerErrorsLogToday;
		private System.Windows.Forms.RadioButton rbComputerErrorsLogAll;
		private Controls.RefreshingGridView rgvComputerErrorsLog;
		private System.Windows.Forms.Button btnCreateShop;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.ListBox lbsUnregistredFiles;
		private System.Windows.Forms.SplitContainer scSendOpers;
		private Controls.OperationAttributes operationAttributes1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private Controls.RefreshingGridView rgvComputers;
		private System.Windows.Forms.ToolStrip mainToolStrip;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.TabPage LastOperations;
		private Controls.RefreshingGridView rgvOperations;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton CheckAll;
		private System.Windows.Forms.ToolStripButton UnCheckAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsbtnSendOperation;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton RemoteControl;
		private System.Windows.Forms.ToolStripButton CallButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton NoFilter;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripButton Find;
		private System.Windows.Forms.ToolStripButton Clear;
		private System.Windows.Forms.ToolStripTextBox tbFind;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripButton tsbSettings;
		private System.Windows.Forms.ComboBox cbOperationGroups;
		private System.Windows.Forms.ToolStripDropDownButton tsddbSpecialSendOpers;
		private System.Windows.Forms.ToolStripMenuItem miSendMainCashbox;
		private System.Windows.Forms.ToolStripMenuItem miSendServer;
		private System.Windows.Forms.ToolStripMenuItem miSendComputers;
		private System.Windows.Forms.ToolStripMenuItem miSendShops;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton rbOperationsAll;
		private System.Windows.Forms.RadioButton rbOperationsToday;
		private System.Windows.Forms.RadioButton rbOperationsLast;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem miCompleted;
		private System.Windows.Forms.ToolStripMenuItem miErrors;
		private System.Windows.Forms.ToolStripMenuItem miErrors1C;
		private System.Windows.Forms.ToolStripMenuItem miEmty1CLog;
		private System.Windows.Forms.ToolStripButton ErrFilter;
		private System.Windows.Forms.ToolStripButton OffFilter;
		private System.Windows.Forms.RadioButton rbOperationsByAgent;
		private System.Windows.Forms.ToolStripDropDownButton Exchange;
		private System.Windows.Forms.ToolStripMenuItem вЦентреToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem вУзлеToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton tsbAbout;
		private System.Windows.Forms.ComboBox cbPoolDate;
		private System.Windows.Forms.RadioButton rbPoolDate;
		private System.Windows.Forms.ToolStripMenuItem miCompletedWithEmptyLog;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ToolStripButton tsbRefresh;
		private System.Windows.Forms.ToolStripButton tsbRegisterSelectedOperation;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripButton ShowOtrs;
		private System.Windows.Forms.ContextMenuStrip cmsOperationGroups;
		private System.Windows.Forms.ToolStripMenuItem miCreateOperationGroup;
		private System.Windows.Forms.ToolStripMenuItem miEditOperationGroup;
		private System.Windows.Forms.ToolStripMenuItem miRemoveOperationGroup;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
		private System.Windows.Forms.Button btnAddEditOperationGroup;
		private System.Windows.Forms.ToolStripDropDownButton tsddbAdditionalAction;
		private System.Windows.Forms.ToolStripMenuItem tsmiExecutionToPlan;
		private System.Windows.Forms.Label lbCurrentOper;
		private System.Windows.Forms.ToolStripMenuItem miUpdateAllAgents;
		private System.Windows.Forms.ToolStripDropDownButton tsbScheduler;
		private System.Windows.Forms.ToolStripMenuItem miShedulerForAgents;
		private System.Windows.Forms.ToolStripMenuItem miShedulerFOrServer;
	}
}

