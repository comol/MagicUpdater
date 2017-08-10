using System;
using System.Windows.Forms;

namespace MagicUpdaterInstaller
{
    partial class InstallerForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallerForm));
			this.tlpInput = new System.Windows.Forms.TableLayoutPanel();
			this.txtPass1C = new System.Windows.Forms.TextBox();
			this.txtUser1C = new System.Windows.Forms.TextBox();
			this.cbShopID = new System.Windows.Forms.ComboBox();
			this.lbShopID = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cbIsMainCashbox = new System.Windows.Forms.CheckBox();
			this.lblIsServerLocated = new System.Windows.Forms.Label();
			this.cbIsServerLocated = new System.Windows.Forms.CheckBox();
			this.lbLastVersion1C = new System.Windows.Forms.Label();
			this.txtVersion1C = new System.Windows.Forms.TextBox();
			this.lbUser1C = new System.Windows.Forms.Label();
			this.lbPassword1C = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.txtTimeOut = new System.Windows.Forms.TextBox();
			this.txtSelfUpdatePath = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.cbIsCheck1C = new System.Windows.Forms.CheckBox();
			this.lbSelfUpdateFtpServer = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.tbSelfUpdateFtpServer = new System.Windows.Forms.TextBox();
			this.tbSelfUpdateFtpUser = new System.Windows.Forms.TextBox();
			this.tbSelfUpdateFtpPassword = new System.Windows.Forms.TextBox();
			this.tbSelfUpdateFtpPath = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.rbServerBase = new System.Windows.Forms.RadioButton();
			this.rbFileBase = new System.Windows.Forms.RadioButton();
			this.lbServerOrPath1C = new System.Windows.Forms.Label();
			this.lbBase1C = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.tbServerOrPath1C = new System.Windows.Forms.TextBox();
			this.tbBase1C = new System.Windows.Forms.TextBox();
			this.txtUserTask = new System.Windows.Forms.TextBox();
			this.txtBaseTask = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.txtPasswordTask = new System.Windows.Forms.TextBox();
			this.btnCheckConnection1C = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.txtServerTask = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tabControlSettings = new System.Windows.Forms.TabControl();
			this.tabPageConnectionString = new System.Windows.Forms.TabPage();
			this.btnTestSqlConnection = new System.Windows.Forms.Button();
			this.tabPageSettings = new System.Windows.Forms.TabPage();
			this.tabInstallService = new System.Windows.Forms.TabPage();
			this.btnInstall = new System.Windows.Forms.Button();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.label4 = new System.Windows.Forms.Label();
			this.tbInstallationpath = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.rtbInstallServiceLog = new System.Windows.Forms.RichTextBox();
			this.tabControlTest = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.lbServiceStatus = new System.Windows.Forms.Label();
			this.lbAgentStatus = new System.Windows.Forms.Label();
			this.btnRestartService = new System.Windows.Forms.Button();
			this.btnPrevousStep = new System.Windows.Forms.Button();
			this.btnNextStep = new System.Windows.Forms.Button();
			this.rtbLog = new System.Windows.Forms.RichTextBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tlpInput.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabControlSettings.SuspendLayout();
			this.tabPageConnectionString.SuspendLayout();
			this.tabPageSettings.SuspendLayout();
			this.tabInstallService.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tabControlTest.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlpInput
			// 
			this.tlpInput.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tlpInput.ColumnCount = 2;
			this.tlpInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.38298F));
			this.tlpInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.61702F));
			this.tlpInput.Controls.Add(this.txtPass1C, 1, 3);
			this.tlpInput.Controls.Add(this.txtUser1C, 1, 2);
			this.tlpInput.Controls.Add(this.cbShopID, 1, 0);
			this.tlpInput.Controls.Add(this.lbShopID, 0, 0);
			this.tlpInput.Controls.Add(this.label2, 0, 7);
			this.tlpInput.Controls.Add(this.label1, 0, 6);
			this.tlpInput.Controls.Add(this.cbIsMainCashbox, 1, 6);
			this.tlpInput.Controls.Add(this.lblIsServerLocated, 0, 5);
			this.tlpInput.Controls.Add(this.cbIsServerLocated, 1, 5);
			this.tlpInput.Controls.Add(this.lbLastVersion1C, 0, 4);
			this.tlpInput.Controls.Add(this.txtVersion1C, 1, 4);
			this.tlpInput.Controls.Add(this.lbUser1C, 0, 2);
			this.tlpInput.Controls.Add(this.lbPassword1C, 0, 3);
			this.tlpInput.Controls.Add(this.label10, 0, 9);
			this.tlpInput.Controls.Add(this.txtTimeOut, 1, 9);
			this.tlpInput.Controls.Add(this.txtSelfUpdatePath, 1, 8);
			this.tlpInput.Controls.Add(this.label6, 0, 8);
			this.tlpInput.Controls.Add(this.cbIsCheck1C, 1, 7);
			this.tlpInput.Controls.Add(this.lbSelfUpdateFtpServer, 0, 10);
			this.tlpInput.Controls.Add(this.label13, 0, 11);
			this.tlpInput.Controls.Add(this.label14, 0, 12);
			this.tlpInput.Controls.Add(this.tbSelfUpdateFtpServer, 1, 10);
			this.tlpInput.Controls.Add(this.tbSelfUpdateFtpUser, 1, 11);
			this.tlpInput.Controls.Add(this.tbSelfUpdateFtpPassword, 1, 12);
			this.tlpInput.Controls.Add(this.tbSelfUpdateFtpPath, 1, 13);
			this.tlpInput.Controls.Add(this.label15, 0, 13);
			this.tlpInput.Controls.Add(this.panel2, 0, 1);
			this.tlpInput.Controls.Add(this.panel1, 1, 1);
			this.tlpInput.Dock = System.Windows.Forms.DockStyle.Top;
			this.tlpInput.Location = new System.Drawing.Point(3, 3);
			this.tlpInput.Name = "tlpInput";
			this.tlpInput.RowCount = 14;
			this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpInput.Size = new System.Drawing.Size(1033, 357);
			this.tlpInput.TabIndex = 0;
			// 
			// txtPass1C
			// 
			this.txtPass1C.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPass1C.Location = new System.Drawing.Point(276, 115);
			this.txtPass1C.Name = "txtPass1C";
			this.txtPass1C.PasswordChar = '*';
			this.txtPass1C.Size = new System.Drawing.Size(753, 20);
			this.txtPass1C.TabIndex = 5;
			this.txtPass1C.TextChanged += new System.EventHandler(this.TbAll_TextChanged);
			// 
			// txtUser1C
			// 
			this.txtUser1C.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtUser1C.Location = new System.Drawing.Point(276, 88);
			this.txtUser1C.Name = "txtUser1C";
			this.txtUser1C.Size = new System.Drawing.Size(753, 20);
			this.txtUser1C.TabIndex = 4;
			this.txtUser1C.TextChanged += new System.EventHandler(this.TbAll_TextChanged);
			// 
			// cbShopID
			// 
			this.cbShopID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbShopID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cbShopID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbShopID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbShopID.ForeColor = System.Drawing.SystemColors.WindowText;
			this.cbShopID.FormattingEnabled = true;
			this.cbShopID.Location = new System.Drawing.Point(276, 4);
			this.cbShopID.Name = "cbShopID";
			this.cbShopID.Size = new System.Drawing.Size(753, 21);
			this.cbShopID.TabIndex = 1;
			// 
			// lbShopID
			// 
			this.lbShopID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbShopID.AutoSize = true;
			this.lbShopID.Location = new System.Drawing.Point(4, 1);
			this.lbShopID.Name = "lbShopID";
			this.lbShopID.Size = new System.Drawing.Size(265, 27);
			this.lbShopID.TabIndex = 0;
			this.lbShopID.Text = "*ID магазина";
			this.lbShopID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(4, 220);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(265, 26);
			this.label2.TabIndex = 14;
			this.label2.Text = "Проверка установки 1С при старте";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 193);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(265, 26);
			this.label1.TabIndex = 12;
			this.label1.Text = "Является ли главной кассой";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label1.Visible = false;
			// 
			// cbIsMainCashbox
			// 
			this.cbIsMainCashbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbIsMainCashbox.Location = new System.Drawing.Point(276, 196);
			this.cbIsMainCashbox.Name = "cbIsMainCashbox";
			this.cbIsMainCashbox.Size = new System.Drawing.Size(753, 20);
			this.cbIsMainCashbox.TabIndex = 8;
			this.cbIsMainCashbox.UseVisualStyleBackColor = true;
			this.cbIsMainCashbox.Visible = false;
			this.cbIsMainCashbox.CheckedChanged += new System.EventHandler(this.ChbAll_CheckedChanged);
			// 
			// lblIsServerLocated
			// 
			this.lblIsServerLocated.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblIsServerLocated.AutoSize = true;
			this.lblIsServerLocated.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblIsServerLocated.Location = new System.Drawing.Point(4, 166);
			this.lblIsServerLocated.Name = "lblIsServerLocated";
			this.lblIsServerLocated.Size = new System.Drawing.Size(265, 26);
			this.lblIsServerLocated.TabIndex = 11;
			this.lblIsServerLocated.Text = "Расположен ли на сервере 1С";
			this.lblIsServerLocated.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblIsServerLocated.Visible = false;
			// 
			// cbIsServerLocated
			// 
			this.cbIsServerLocated.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbIsServerLocated.Checked = true;
			this.cbIsServerLocated.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbIsServerLocated.Location = new System.Drawing.Point(276, 169);
			this.cbIsServerLocated.Name = "cbIsServerLocated";
			this.cbIsServerLocated.Size = new System.Drawing.Size(753, 20);
			this.cbIsServerLocated.TabIndex = 7;
			this.cbIsServerLocated.UseVisualStyleBackColor = true;
			this.cbIsServerLocated.Visible = false;
			this.cbIsServerLocated.CheckedChanged += new System.EventHandler(this.ChbAll_CheckedChanged);
			// 
			// lbLastVersion1C
			// 
			this.lbLastVersion1C.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbLastVersion1C.AutoSize = true;
			this.lbLastVersion1C.Location = new System.Drawing.Point(4, 139);
			this.lbLastVersion1C.Name = "lbLastVersion1C";
			this.lbLastVersion1C.Size = new System.Drawing.Size(265, 26);
			this.lbLastVersion1C.TabIndex = 3;
			this.lbLastVersion1C.Text = "*Используемая версия 1С";
			this.lbLastVersion1C.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtVersion1C
			// 
			this.txtVersion1C.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtVersion1C.Location = new System.Drawing.Point(276, 142);
			this.txtVersion1C.Name = "txtVersion1C";
			this.txtVersion1C.Size = new System.Drawing.Size(753, 20);
			this.txtVersion1C.TabIndex = 6;
			this.txtVersion1C.TextChanged += new System.EventHandler(this.TbAll_TextChanged);
			this.txtVersion1C.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLastVersion1C_KeyPress);
			// 
			// lbUser1C
			// 
			this.lbUser1C.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbUser1C.AutoSize = true;
			this.lbUser1C.Location = new System.Drawing.Point(4, 85);
			this.lbUser1C.Name = "lbUser1C";
			this.lbUser1C.Size = new System.Drawing.Size(265, 26);
			this.lbUser1C.TabIndex = 18;
			this.lbUser1C.Text = "*Пользователь 1С предприятия";
			this.lbUser1C.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbPassword1C
			// 
			this.lbPassword1C.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbPassword1C.AutoSize = true;
			this.lbPassword1C.Location = new System.Drawing.Point(4, 112);
			this.lbPassword1C.Name = "lbPassword1C";
			this.lbPassword1C.Size = new System.Drawing.Size(265, 26);
			this.lbPassword1C.TabIndex = 19;
			this.lbPassword1C.Text = "*Пароль 1С предприятия";
			this.lbPassword1C.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(4, 274);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(265, 26);
			this.label10.TabIndex = 23;
			this.label10.Text = "*Тайм-аут обращений к базе заданий (мс)";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtTimeOut
			// 
			this.txtTimeOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTimeOut.Location = new System.Drawing.Point(276, 277);
			this.txtTimeOut.Name = "txtTimeOut";
			this.txtTimeOut.Size = new System.Drawing.Size(753, 20);
			this.txtTimeOut.TabIndex = 14;
			this.txtTimeOut.Text = "5000";
			this.txtTimeOut.TextChanged += new System.EventHandler(this.TbAll_TextChanged);
			this.txtTimeOut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimeOut_KeyPress);
			// 
			// txtSelfUpdatePath
			// 
			this.txtSelfUpdatePath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSelfUpdatePath.Location = new System.Drawing.Point(276, 250);
			this.txtSelfUpdatePath.Name = "txtSelfUpdatePath";
			this.txtSelfUpdatePath.Size = new System.Drawing.Size(753, 20);
			this.txtSelfUpdatePath.TabIndex = 9;
			this.txtSelfUpdatePath.Text = "C:\\SystemUtils\\MagicUpdaterNewVer";
			this.txtSelfUpdatePath.TextChanged += new System.EventHandler(this.TbAll_TextChanged);
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(4, 247);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(265, 26);
			this.label6.TabIndex = 14;
			this.label6.Text = "*Путь для самообновления";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbIsCheck1C
			// 
			this.cbIsCheck1C.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbIsCheck1C.Checked = true;
			this.cbIsCheck1C.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbIsCheck1C.Location = new System.Drawing.Point(276, 223);
			this.cbIsCheck1C.Name = "cbIsCheck1C";
			this.cbIsCheck1C.Size = new System.Drawing.Size(753, 20);
			this.cbIsCheck1C.TabIndex = 8;
			this.cbIsCheck1C.UseVisualStyleBackColor = true;
			this.cbIsCheck1C.CheckedChanged += new System.EventHandler(this.ChbAll_CheckedChanged);
			// 
			// lbSelfUpdateFtpServer
			// 
			this.lbSelfUpdateFtpServer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbSelfUpdateFtpServer.AutoSize = true;
			this.lbSelfUpdateFtpServer.Location = new System.Drawing.Point(4, 301);
			this.lbSelfUpdateFtpServer.Name = "lbSelfUpdateFtpServer";
			this.lbSelfUpdateFtpServer.Size = new System.Drawing.Size(265, 26);
			this.lbSelfUpdateFtpServer.TabIndex = 23;
			this.lbSelfUpdateFtpServer.Text = "*Сервер Ftp для самообновления";
			this.lbSelfUpdateFtpServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label13
			// 
			this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(4, 328);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(265, 26);
			this.label13.TabIndex = 23;
			this.label13.Text = "*Логин сервера Ftp для самообновления";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label14
			// 
			this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(4, 355);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(265, 26);
			this.label14.TabIndex = 23;
			this.label14.Text = "*Пароль сервера Ftp для самообновления";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tbSelfUpdateFtpServer
			// 
			this.tbSelfUpdateFtpServer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbSelfUpdateFtpServer.Location = new System.Drawing.Point(276, 304);
			this.tbSelfUpdateFtpServer.Name = "tbSelfUpdateFtpServer";
			this.tbSelfUpdateFtpServer.Size = new System.Drawing.Size(753, 20);
			this.tbSelfUpdateFtpServer.TabIndex = 9;
			this.tbSelfUpdateFtpServer.TextChanged += new System.EventHandler(this.TbAll_TextChanged);
			// 
			// tbSelfUpdateFtpUser
			// 
			this.tbSelfUpdateFtpUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbSelfUpdateFtpUser.Location = new System.Drawing.Point(276, 331);
			this.tbSelfUpdateFtpUser.Name = "tbSelfUpdateFtpUser";
			this.tbSelfUpdateFtpUser.Size = new System.Drawing.Size(753, 20);
			this.tbSelfUpdateFtpUser.TabIndex = 9;
			this.tbSelfUpdateFtpUser.TextChanged += new System.EventHandler(this.TbAll_TextChanged);
			// 
			// tbSelfUpdateFtpPassword
			// 
			this.tbSelfUpdateFtpPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbSelfUpdateFtpPassword.Location = new System.Drawing.Point(276, 358);
			this.tbSelfUpdateFtpPassword.Name = "tbSelfUpdateFtpPassword";
			this.tbSelfUpdateFtpPassword.PasswordChar = '*';
			this.tbSelfUpdateFtpPassword.Size = new System.Drawing.Size(753, 20);
			this.tbSelfUpdateFtpPassword.TabIndex = 9;
			this.tbSelfUpdateFtpPassword.TextChanged += new System.EventHandler(this.TbAll_TextChanged);
			// 
			// tbSelfUpdateFtpPath
			// 
			this.tbSelfUpdateFtpPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbSelfUpdateFtpPath.Location = new System.Drawing.Point(276, 385);
			this.tbSelfUpdateFtpPath.Name = "tbSelfUpdateFtpPath";
			this.tbSelfUpdateFtpPath.Size = new System.Drawing.Size(753, 20);
			this.tbSelfUpdateFtpPath.TabIndex = 9;
			this.tbSelfUpdateFtpPath.Text = "MagicUpdater";
			this.tbSelfUpdateFtpPath.TextChanged += new System.EventHandler(this.TbAll_TextChanged);
			// 
			// label15
			// 
			this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(4, 382);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(265, 26);
			this.label15.TabIndex = 23;
			this.label15.Text = "*Путь до папки Ftp для самообновления";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.tableLayoutPanel4);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(1, 29);
			this.panel2.Margin = new System.Windows.Forms.Padding(0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(271, 55);
			this.panel2.TabIndex = 24;
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.ColumnCount = 2;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.01657F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.98343F));
			this.tableLayoutPanel4.Controls.Add(this.rbServerBase, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.rbFileBase, 0, 1);
			this.tableLayoutPanel4.Controls.Add(this.lbServerOrPath1C, 1, 0);
			this.tableLayoutPanel4.Controls.Add(this.lbBase1C, 1, 1);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 2;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(271, 55);
			this.tableLayoutPanel4.TabIndex = 0;
			// 
			// rbServerBase
			// 
			this.rbServerBase.AutoSize = true;
			this.rbServerBase.Checked = true;
			this.rbServerBase.Location = new System.Drawing.Point(10, 3);
			this.rbServerBase.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			this.rbServerBase.Name = "rbServerBase";
			this.rbServerBase.Size = new System.Drawing.Size(87, 17);
			this.rbServerBase.TabIndex = 0;
			this.rbServerBase.TabStop = true;
			this.rbServerBase.Text = "Серверная база";
			this.rbServerBase.UseVisualStyleBackColor = true;
			this.rbServerBase.CheckedChanged += new System.EventHandler(this.rbServerBase_CheckedChanged);
			// 
			// rbFileBase
			// 
			this.rbFileBase.AutoSize = true;
			this.rbFileBase.Location = new System.Drawing.Point(10, 30);
			this.rbFileBase.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			this.rbFileBase.Name = "rbFileBase";
			this.rbFileBase.Size = new System.Drawing.Size(87, 17);
			this.rbFileBase.TabIndex = 0;
			this.rbFileBase.Text = "Файловая база";
			this.rbFileBase.UseVisualStyleBackColor = true;
			this.rbFileBase.CheckedChanged += new System.EventHandler(this.rbFileBase_CheckedChanged);
			// 
			// lbServerOrPath1C
			// 
			this.lbServerOrPath1C.AutoSize = true;
			this.lbServerOrPath1C.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbServerOrPath1C.Location = new System.Drawing.Point(103, 0);
			this.lbServerOrPath1C.Name = "lbServerOrPath1C";
			this.lbServerOrPath1C.Size = new System.Drawing.Size(165, 27);
			this.lbServerOrPath1C.TabIndex = 1;
			this.lbServerOrPath1C.Text = "*Сервер 1С предприятия";
			this.lbServerOrPath1C.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbBase1C
			// 
			this.lbBase1C.AutoSize = true;
			this.lbBase1C.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbBase1C.Location = new System.Drawing.Point(103, 27);
			this.lbBase1C.Name = "lbBase1C";
			this.lbBase1C.Size = new System.Drawing.Size(165, 28);
			this.lbBase1C.TabIndex = 1;
			this.lbBase1C.Text = "*База 1С предприятия";
			this.lbBase1C.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tableLayoutPanel5);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(273, 29);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(759, 55);
			this.panel1.TabIndex = 25;
			// 
			// tableLayoutPanel5
			// 
			this.tableLayoutPanel5.ColumnCount = 1;
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel5.Controls.Add(this.tbServerOrPath1C, 0, 0);
			this.tableLayoutPanel5.Controls.Add(this.tbBase1C, 0, 1);
			this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 2;
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel5.Size = new System.Drawing.Size(759, 55);
			this.tableLayoutPanel5.TabIndex = 1;
			// 
			// tbServerOrPath1C
			// 
			this.tbServerOrPath1C.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbServerOrPath1C.Location = new System.Drawing.Point(3, 3);
			this.tbServerOrPath1C.Name = "tbServerOrPath1C";
			this.tbServerOrPath1C.Size = new System.Drawing.Size(753, 20);
			this.tbServerOrPath1C.TabIndex = 0;
			this.tbServerOrPath1C.TextChanged += new System.EventHandler(this.TbAll_TextChanged);
			// 
			// tbBase1C
			// 
			this.tbBase1C.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbBase1C.Location = new System.Drawing.Point(3, 30);
			this.tbBase1C.Name = "tbBase1C";
			this.tbBase1C.Size = new System.Drawing.Size(753, 20);
			this.tbBase1C.TabIndex = 0;
			this.tbBase1C.TextChanged += new System.EventHandler(this.TbAll_TextChanged);
			// 
			// txtUserTask
			// 
			this.txtUserTask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtUserTask.Location = new System.Drawing.Point(316, 58);
			this.txtUserTask.Name = "txtUserTask";
			this.txtUserTask.Size = new System.Drawing.Size(713, 20);
			this.txtUserTask.TabIndex = 12;
			this.txtUserTask.TextChanged += new System.EventHandler(this.TbAllJsonSettings_TextChanged);
			// 
			// txtBaseTask
			// 
			this.txtBaseTask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBaseTask.Location = new System.Drawing.Point(316, 31);
			this.txtBaseTask.Name = "txtBaseTask";
			this.txtBaseTask.Size = new System.Drawing.Size(713, 20);
			this.txtBaseTask.TabIndex = 11;
			this.txtBaseTask.TextChanged += new System.EventHandler(this.TbAllJsonSettings_TextChanged);
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(4, 28);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(305, 26);
			this.label7.TabIndex = 25;
			this.label7.Text = "*База заданий";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(4, 55);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(305, 26);
			this.label8.TabIndex = 26;
			this.label8.Text = "*Имя пользователя базы заданий";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(4, 82);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(305, 28);
			this.label9.TabIndex = 27;
			this.label9.Text = "*Пароль базы заданий";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtPasswordTask
			// 
			this.txtPasswordTask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPasswordTask.Location = new System.Drawing.Point(316, 85);
			this.txtPasswordTask.Name = "txtPasswordTask";
			this.txtPasswordTask.PasswordChar = '*';
			this.txtPasswordTask.Size = new System.Drawing.Size(713, 20);
			this.txtPasswordTask.TabIndex = 13;
			this.txtPasswordTask.TextChanged += new System.EventHandler(this.TbAllJsonSettings_TextChanged);
			// 
			// btnCheckConnection1C
			// 
			this.btnCheckConnection1C.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnCheckConnection1C.Location = new System.Drawing.Point(578, 371);
			this.btnCheckConnection1C.Name = "btnCheckConnection1C";
			this.btnCheckConnection1C.Size = new System.Drawing.Size(458, 38);
			this.btnCheckConnection1C.TabIndex = 15;
			this.btnCheckConnection1C.Text = "Проверить подключение к 1С";
			this.btnCheckConnection1C.UseVisualStyleBackColor = true;
			this.btnCheckConnection1C.Click += new System.EventHandler(this.btnCheckConnection1C_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.24227F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.75774F));
			this.tableLayoutPanel1.Controls.Add(this.txtUserTask, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.txtServerTask, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtBaseTask, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label8, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label9, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.txtPasswordTask, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.label7, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1033, 111);
			this.tableLayoutPanel1.TabIndex = 22;
			// 
			// txtServerTask
			// 
			this.txtServerTask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtServerTask.Location = new System.Drawing.Point(316, 4);
			this.txtServerTask.Name = "txtServerTask";
			this.txtServerTask.Size = new System.Drawing.Size(713, 20);
			this.txtServerTask.TabIndex = 10;
			this.txtServerTask.TextChanged += new System.EventHandler(this.TbAllJsonSettings_TextChanged);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 1);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(305, 26);
			this.label3.TabIndex = 16;
			this.label3.Text = "*Сервер заданий";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabControlSettings
			// 
			this.tabControlSettings.Controls.Add(this.tabPageConnectionString);
			this.tabControlSettings.Controls.Add(this.tabPageSettings);
			this.tabControlSettings.Controls.Add(this.tabInstallService);
			this.tabControlSettings.Controls.Add(this.tabControlTest);
			this.tabControlSettings.Dock = System.Windows.Forms.DockStyle.Top;
			this.tabControlSettings.ItemSize = new System.Drawing.Size(96, 18);
			this.tabControlSettings.Location = new System.Drawing.Point(10, 10);
			this.tabControlSettings.Name = "tabControlSettings";
			this.tabControlSettings.SelectedIndex = 0;
			this.tabControlSettings.Size = new System.Drawing.Size(1047, 492);
			this.tabControlSettings.TabIndex = 23;
			// 
			// tabPageConnectionString
			// 
			this.tabPageConnectionString.Controls.Add(this.btnTestSqlConnection);
			this.tabPageConnectionString.Controls.Add(this.tableLayoutPanel1);
			this.tabPageConnectionString.Location = new System.Drawing.Point(4, 22);
			this.tabPageConnectionString.Name = "tabPageConnectionString";
			this.tabPageConnectionString.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageConnectionString.Size = new System.Drawing.Size(1039, 466);
			this.tabPageConnectionString.TabIndex = 0;
			this.tabPageConnectionString.Text = "Connection String";
			this.tabPageConnectionString.UseVisualStyleBackColor = true;
			// 
			// btnTestSqlConnection
			// 
			this.btnTestSqlConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnTestSqlConnection.Location = new System.Drawing.Point(3, 120);
			this.btnTestSqlConnection.Name = "btnTestSqlConnection";
			this.btnTestSqlConnection.Size = new System.Drawing.Size(327, 35);
			this.btnTestSqlConnection.TabIndex = 23;
			this.btnTestSqlConnection.Text = "Тест подключения к базе заданий";
			this.btnTestSqlConnection.UseVisualStyleBackColor = true;
			this.btnTestSqlConnection.Click += new System.EventHandler(this.btnTestSqlConnection_Click);
			// 
			// tabPageSettings
			// 
			this.tabPageSettings.Controls.Add(this.tlpInput);
			this.tabPageSettings.Controls.Add(this.btnCheckConnection1C);
			this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
			this.tabPageSettings.Name = "tabPageSettings";
			this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSettings.Size = new System.Drawing.Size(1039, 466);
			this.tabPageSettings.TabIndex = 1;
			this.tabPageSettings.Text = "Settings";
			this.tabPageSettings.UseVisualStyleBackColor = true;
			// 
			// tabInstallService
			// 
			this.tabInstallService.Controls.Add(this.btnInstall);
			this.tabInstallService.Controls.Add(this.tableLayoutPanel2);
			this.tabInstallService.Controls.Add(this.label11);
			this.tabInstallService.Controls.Add(this.progressBar1);
			this.tabInstallService.Controls.Add(this.rtbInstallServiceLog);
			this.tabInstallService.Location = new System.Drawing.Point(4, 22);
			this.tabInstallService.Name = "tabInstallService";
			this.tabInstallService.Padding = new System.Windows.Forms.Padding(3);
			this.tabInstallService.Size = new System.Drawing.Size(1039, 466);
			this.tabInstallService.TabIndex = 2;
			this.tabInstallService.Text = "tabInstallService";
			this.tabInstallService.UseVisualStyleBackColor = true;
			// 
			// btnInstall
			// 
			this.btnInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnInstall.Location = new System.Drawing.Point(422, 97);
			this.btnInstall.Name = "btnInstall";
			this.btnInstall.Size = new System.Drawing.Size(194, 33);
			this.btnInstall.TabIndex = 33;
			this.btnInstall.Text = "Установить";
			this.btnInstall.UseVisualStyleBackColor = true;
			this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.BackColor = System.Drawing.Color.Silver;
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.tbInstallationpath, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 38);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(1033, 38);
			this.tableLayoutPanel2.TabIndex = 32;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(3, 10);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(510, 28);
			this.label4.TabIndex = 0;
			this.label4.Text = "Путь установки:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tbInstallationpath
			// 
			this.tbInstallationpath.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbInstallationpath.Location = new System.Drawing.Point(519, 13);
			this.tbInstallationpath.Name = "tbInstallationpath";
			this.tbInstallationpath.Size = new System.Drawing.Size(511, 20);
			this.tbInstallationpath.TabIndex = 1;
			this.tbInstallationpath.Text = "C:\\SystemUtils\\MagicUpdater\\";
			// 
			// label11
			// 
			this.label11.Dock = System.Windows.Forms.DockStyle.Top;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label11.Location = new System.Drawing.Point(3, 3);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(1033, 35);
			this.label11.TabIndex = 31;
			this.label11.Text = "Установка службы MagicUpdater";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(3, 152);
			this.progressBar1.Maximum = 17;
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(1027, 23);
			this.progressBar1.TabIndex = 30;
			// 
			// rtbInstallServiceLog
			// 
			this.rtbInstallServiceLog.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.rtbInstallServiceLog.Location = new System.Drawing.Point(3, 184);
			this.rtbInstallServiceLog.Name = "rtbInstallServiceLog";
			this.rtbInstallServiceLog.ReadOnly = true;
			this.rtbInstallServiceLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.rtbInstallServiceLog.Size = new System.Drawing.Size(1033, 279);
			this.rtbInstallServiceLog.TabIndex = 29;
			this.rtbInstallServiceLog.Text = "";
			// 
			// tabControlTest
			// 
			this.tabControlTest.Controls.Add(this.tableLayoutPanel3);
			this.tabControlTest.Controls.Add(this.btnRestartService);
			this.tabControlTest.Location = new System.Drawing.Point(4, 22);
			this.tabControlTest.Name = "tabControlTest";
			this.tabControlTest.Padding = new System.Windows.Forms.Padding(3);
			this.tabControlTest.Size = new System.Drawing.Size(1039, 466);
			this.tabControlTest.TabIndex = 3;
			this.tabControlTest.Text = "tabControlTest";
			this.tabControlTest.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Controls.Add(this.lbServiceStatus, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.lbAgentStatus, 0, 1);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(300, 10, 300, 0);
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(1033, 96);
			this.tableLayoutPanel3.TabIndex = 31;
			// 
			// lbServiceStatus
			// 
			this.lbServiceStatus.BackColor = System.Drawing.Color.Pink;
			this.lbServiceStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbServiceStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbServiceStatus.Location = new System.Drawing.Point(303, 10);
			this.lbServiceStatus.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
			this.lbServiceStatus.Name = "lbServiceStatus";
			this.lbServiceStatus.Size = new System.Drawing.Size(427, 33);
			this.lbServiceStatus.TabIndex = 29;
			this.lbServiceStatus.Text = "Служба остановлена";
			this.lbServiceStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbAgentStatus
			// 
			this.lbAgentStatus.BackColor = System.Drawing.Color.Pink;
			this.lbAgentStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbAgentStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbAgentStatus.Location = new System.Drawing.Point(303, 53);
			this.lbAgentStatus.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
			this.lbAgentStatus.Name = "lbAgentStatus";
			this.lbAgentStatus.Size = new System.Drawing.Size(427, 33);
			this.lbAgentStatus.TabIndex = 30;
			this.lbAgentStatus.Text = "Агент не отчитывается в базу";
			this.lbAgentStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnRestartService
			// 
			this.btnRestartService.BackColor = System.Drawing.SystemColors.Control;
			this.btnRestartService.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRestartService.Location = new System.Drawing.Point(832, 394);
			this.btnRestartService.Name = "btnRestartService";
			this.btnRestartService.Size = new System.Drawing.Size(166, 23);
			this.btnRestartService.TabIndex = 28;
			this.btnRestartService.Text = "btnRestartService";
			this.btnRestartService.UseVisualStyleBackColor = false;
			this.btnRestartService.Visible = false;
			this.btnRestartService.Click += new System.EventHandler(this.btnRestartService_Click_1);
			// 
			// btnPrevousStep
			// 
			this.btnPrevousStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnPrevousStep.Location = new System.Drawing.Point(11, 603);
			this.btnPrevousStep.Name = "btnPrevousStep";
			this.btnPrevousStep.Size = new System.Drawing.Size(300, 36);
			this.btnPrevousStep.TabIndex = 24;
			this.btnPrevousStep.Text = "< Назад";
			this.btnPrevousStep.UseVisualStyleBackColor = true;
			this.btnPrevousStep.Click += new System.EventHandler(this.btnPrevousStep_Click);
			// 
			// btnNextStep
			// 
			this.btnNextStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnNextStep.Location = new System.Drawing.Point(764, 603);
			this.btnNextStep.Name = "btnNextStep";
			this.btnNextStep.Size = new System.Drawing.Size(294, 36);
			this.btnNextStep.TabIndex = 25;
			this.btnNextStep.Text = "Сохранить и перейти к настройкам 1С >";
			this.btnNextStep.UseVisualStyleBackColor = true;
			this.btnNextStep.Click += new System.EventHandler(this.btnNextStep_Click);
			// 
			// rtbLog
			// 
			this.rtbLog.Location = new System.Drawing.Point(11, 508);
			this.rtbLog.Name = "rtbLog";
			this.rtbLog.ReadOnly = true;
			this.rtbLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.rtbLog.Size = new System.Drawing.Size(1046, 84);
			this.rtbLog.TabIndex = 28;
			this.rtbLog.Text = "";
			// 
			// toolTip1
			// 
			this.toolTip1.IsBalloon = true;
			// 
			// InstallerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1067, 646);
			this.Controls.Add(this.rtbLog);
			this.Controls.Add(this.btnNextStep);
			this.Controls.Add(this.btnPrevousStep);
			this.Controls.Add(this.tabControlSettings);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "InstallerForm";
			this.Padding = new System.Windows.Forms.Padding(10);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MagicUpdaterInstaller";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InstallerForm_FormClosed);
			this.Load += new System.EventHandler(this.SettingsForm_Load);
			this.Shown += new System.EventHandler(this.SettingsForm_Shown);
			this.Resize += new System.EventHandler(this.SettingsForm_Resize);
			this.tlpInput.ResumeLayout(false);
			this.tlpInput.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tabControlSettings.ResumeLayout(false);
			this.tabPageConnectionString.ResumeLayout(false);
			this.tabPageSettings.ResumeLayout(false);
			this.tabInstallService.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tabControlTest.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.ResumeLayout(false);

        }


        #endregion
        private System.Windows.Forms.Label lbLastVersion1C;
        private System.Windows.Forms.Label lbShopID;
        private System.Windows.Forms.TextBox txtVersion1C;
        private System.Windows.Forms.TableLayoutPanel tlpInput;
        private System.Windows.Forms.Label lblIsServerLocated;
        private System.Windows.Forms.CheckBox cbIsServerLocated;
        private System.Windows.Forms.ComboBox cbShopID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbIsMainCashbox;
        private System.Windows.Forms.TextBox txtSelfUpdatePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbUser1C;
        private System.Windows.Forms.Label lbPassword1C;
        private System.Windows.Forms.TextBox txtTimeOut;
        private System.Windows.Forms.Button btnCheckConnection1C;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPasswordTask;
        private System.Windows.Forms.TextBox txtUserTask;
        private System.Windows.Forms.TextBox txtBaseTask;
        private System.Windows.Forms.TextBox txtPass1C;
        private System.Windows.Forms.TextBox txtUser1C;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox txtServerTask;
        private Label label3;
        private TabControl tabControlSettings;
        private TabPage tabPageConnectionString;
        private TabPage tabPageSettings;
        private Button btnPrevousStep;
        private Button btnNextStep;
        private Button btnTestSqlConnection;
		private Label label10;
		private Label label6;
		private CheckBox cbIsCheck1C;
		private RichTextBox rtbLog;
		private TabPage tabInstallService;
		private RichTextBox rtbInstallServiceLog;
		private ProgressBar progressBar1;
		private Label label11;
		private TabPage tabControlTest;
		private Label lbSelfUpdateFtpServer;
		private Label label13;
		private Label label14;
		private Label label15;
		private TextBox tbSelfUpdateFtpServer;
		private TextBox tbSelfUpdateFtpUser;
		private TextBox tbSelfUpdateFtpPassword;
		private TextBox tbSelfUpdateFtpPath;
		private TableLayoutPanel tableLayoutPanel2;
		private Label label4;
		private TextBox tbInstallationpath;
		private Button btnInstall;
		private Label lbServiceStatus;
		private Button btnRestartService;
		private Label lbAgentStatus;
		private TableLayoutPanel tableLayoutPanel3;
		private ToolTip toolTip1;
		private Panel panel2;
		private TableLayoutPanel tableLayoutPanel4;
		private RadioButton rbServerBase;
		private RadioButton rbFileBase;
		private Label lbServerOrPath1C;
		private Label lbBase1C;
		private Panel panel1;
		private TableLayoutPanel tableLayoutPanel5;
		private TextBox tbServerOrPath1C;
		private TextBox tbBase1C;
	}
}