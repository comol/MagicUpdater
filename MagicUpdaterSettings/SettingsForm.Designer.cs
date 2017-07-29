using System;
using System.Windows.Forms;

namespace MagicUpdaterInstaller
{
    partial class SettingsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
			this.tlpInput = new System.Windows.Forms.TableLayoutPanel();
			this.txtPass1C = new System.Windows.Forms.TextBox();
			this.txtUser1C = new System.Windows.Forms.TextBox();
			this.txtBase1C = new System.Windows.Forms.TextBox();
			this.cbShopID = new System.Windows.Forms.ComboBox();
			this.lbServer = new System.Windows.Forms.Label();
			this.lbShopID = new System.Windows.Forms.Label();
			this.txtServer1C = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cbIsMainCashbox = new System.Windows.Forms.CheckBox();
			this.lblIsServerLocated = new System.Windows.Forms.Label();
			this.cbIsServerLocated = new System.Windows.Forms.CheckBox();
			this.lbLastVersion = new System.Windows.Forms.Label();
			this.txtVersion1C = new System.Windows.Forms.TextBox();
			this.lbPort = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.txtTimeOut = new System.Windows.Forms.TextBox();
			this.txtSelfUpdatePath = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.cbIsCheck1C = new System.Windows.Forms.CheckBox();
			this.txtUserTask = new System.Windows.Forms.TextBox();
			this.txtBaseTask = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.txtPasswordTask = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnCheckConnection1C = new System.Windows.Forms.Button();
			this.btnCheckConnectionTasks = new System.Windows.Forms.Button();
			this.btnConnectTaskBase = new System.Windows.Forms.Button();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.txtServerTask = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tabControlSettings = new System.Windows.Forms.TabControl();
			this.tabPageConnectionString = new System.Windows.Forms.TabPage();
			this.btnTestSqlConnection = new System.Windows.Forms.Button();
			this.tabPageSettings = new System.Windows.Forms.TabPage();
			this.btnPrevousStep = new System.Windows.Forms.Button();
			this.btnNextStep = new System.Windows.Forms.Button();
			this.btnRestartService = new System.Windows.Forms.Button();
			this.lbServiceStatus = new System.Windows.Forms.Label();
			this.tlpInput.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabControlSettings.SuspendLayout();
			this.tabPageConnectionString.SuspendLayout();
			this.tabPageSettings.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlpInput
			// 
			this.tlpInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tlpInput.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tlpInput.ColumnCount = 2;
			this.tlpInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.38298F));
			this.tlpInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.61702F));
			this.tlpInput.Controls.Add(this.txtPass1C, 1, 4);
			this.tlpInput.Controls.Add(this.txtUser1C, 1, 3);
			this.tlpInput.Controls.Add(this.txtBase1C, 1, 2);
			this.tlpInput.Controls.Add(this.cbShopID, 1, 0);
			this.tlpInput.Controls.Add(this.lbServer, 0, 1);
			this.tlpInput.Controls.Add(this.lbShopID, 0, 0);
			this.tlpInput.Controls.Add(this.txtServer1C, 1, 1);
			this.tlpInput.Controls.Add(this.label2, 0, 8);
			this.tlpInput.Controls.Add(this.label1, 0, 7);
			this.tlpInput.Controls.Add(this.cbIsMainCashbox, 1, 7);
			this.tlpInput.Controls.Add(this.lblIsServerLocated, 0, 6);
			this.tlpInput.Controls.Add(this.cbIsServerLocated, 1, 6);
			this.tlpInput.Controls.Add(this.lbLastVersion, 0, 5);
			this.tlpInput.Controls.Add(this.txtVersion1C, 1, 5);
			this.tlpInput.Controls.Add(this.lbPort, 0, 2);
			this.tlpInput.Controls.Add(this.label4, 0, 3);
			this.tlpInput.Controls.Add(this.label5, 0, 4);
			this.tlpInput.Controls.Add(this.label10, 0, 10);
			this.tlpInput.Controls.Add(this.txtTimeOut, 1, 10);
			this.tlpInput.Controls.Add(this.txtSelfUpdatePath, 1, 9);
			this.tlpInput.Controls.Add(this.label6, 0, 9);
			this.tlpInput.Controls.Add(this.cbIsCheck1C, 1, 8);
			this.tlpInput.Location = new System.Drawing.Point(6, 6);
			this.tlpInput.Name = "tlpInput";
			this.tlpInput.RowCount = 11;
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
			this.tlpInput.Size = new System.Drawing.Size(1027, 301);
			this.tlpInput.TabIndex = 0;
			// 
			// txtPass1C
			// 
			this.txtPass1C.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPass1C.Location = new System.Drawing.Point(275, 113);
			this.txtPass1C.Name = "txtPass1C";
			this.txtPass1C.PasswordChar = '*';
			this.txtPass1C.Size = new System.Drawing.Size(748, 20);
			this.txtPass1C.TabIndex = 5;
			// 
			// txtUser1C
			// 
			this.txtUser1C.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtUser1C.Location = new System.Drawing.Point(275, 86);
			this.txtUser1C.Name = "txtUser1C";
			this.txtUser1C.Size = new System.Drawing.Size(748, 20);
			this.txtUser1C.TabIndex = 4;
			// 
			// txtBase1C
			// 
			this.txtBase1C.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBase1C.Location = new System.Drawing.Point(275, 59);
			this.txtBase1C.Name = "txtBase1C";
			this.txtBase1C.Size = new System.Drawing.Size(748, 20);
			this.txtBase1C.TabIndex = 3;
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
			this.cbShopID.Location = new System.Drawing.Point(275, 4);
			this.cbShopID.Name = "cbShopID";
			this.cbShopID.Size = new System.Drawing.Size(748, 21);
			this.cbShopID.TabIndex = 1;
			// 
			// lbServer
			// 
			this.lbServer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbServer.AutoSize = true;
			this.lbServer.Location = new System.Drawing.Point(4, 29);
			this.lbServer.Name = "lbServer";
			this.lbServer.Size = new System.Drawing.Size(264, 26);
			this.lbServer.TabIndex = 1;
			this.lbServer.Text = "Сервер 1С предприятия";
			this.lbServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbShopID
			// 
			this.lbShopID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbShopID.AutoSize = true;
			this.lbShopID.Location = new System.Drawing.Point(4, 1);
			this.lbShopID.Name = "lbShopID";
			this.lbShopID.Size = new System.Drawing.Size(264, 27);
			this.lbShopID.TabIndex = 0;
			this.lbShopID.Text = "ID магазина";
			this.lbShopID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtServer1C
			// 
			this.txtServer1C.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtServer1C.Location = new System.Drawing.Point(275, 32);
			this.txtServer1C.Name = "txtServer1C";
			this.txtServer1C.Size = new System.Drawing.Size(748, 20);
			this.txtServer1C.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(4, 218);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(264, 26);
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
			this.label1.Location = new System.Drawing.Point(4, 191);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(264, 26);
			this.label1.TabIndex = 12;
			this.label1.Text = "Является ли главной кассой";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbIsMainCashbox
			// 
			this.cbIsMainCashbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbIsMainCashbox.Location = new System.Drawing.Point(275, 194);
			this.cbIsMainCashbox.Name = "cbIsMainCashbox";
			this.cbIsMainCashbox.Size = new System.Drawing.Size(748, 20);
			this.cbIsMainCashbox.TabIndex = 8;
			this.cbIsMainCashbox.UseVisualStyleBackColor = true;
			// 
			// lblIsServerLocated
			// 
			this.lblIsServerLocated.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblIsServerLocated.AutoSize = true;
			this.lblIsServerLocated.Location = new System.Drawing.Point(4, 164);
			this.lblIsServerLocated.Name = "lblIsServerLocated";
			this.lblIsServerLocated.Size = new System.Drawing.Size(264, 26);
			this.lblIsServerLocated.TabIndex = 11;
			this.lblIsServerLocated.Text = "Расположен ли на сервере 1С";
			this.lblIsServerLocated.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbIsServerLocated
			// 
			this.cbIsServerLocated.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbIsServerLocated.Location = new System.Drawing.Point(275, 167);
			this.cbIsServerLocated.Name = "cbIsServerLocated";
			this.cbIsServerLocated.Size = new System.Drawing.Size(748, 20);
			this.cbIsServerLocated.TabIndex = 7;
			this.cbIsServerLocated.UseVisualStyleBackColor = true;
			// 
			// lbLastVersion
			// 
			this.lbLastVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbLastVersion.AutoSize = true;
			this.lbLastVersion.Location = new System.Drawing.Point(4, 137);
			this.lbLastVersion.Name = "lbLastVersion";
			this.lbLastVersion.Size = new System.Drawing.Size(264, 26);
			this.lbLastVersion.TabIndex = 3;
			this.lbLastVersion.Text = "Последняя версия 1С";
			this.lbLastVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtVersion1C
			// 
			this.txtVersion1C.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtVersion1C.Location = new System.Drawing.Point(275, 140);
			this.txtVersion1C.Name = "txtVersion1C";
			this.txtVersion1C.Size = new System.Drawing.Size(748, 20);
			this.txtVersion1C.TabIndex = 6;
			this.txtVersion1C.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLastVersion1C_KeyPress);
			// 
			// lbPort
			// 
			this.lbPort.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbPort.AutoSize = true;
			this.lbPort.Location = new System.Drawing.Point(4, 56);
			this.lbPort.Name = "lbPort";
			this.lbPort.Size = new System.Drawing.Size(264, 26);
			this.lbPort.TabIndex = 2;
			this.lbPort.Text = "База 1С предприятия";
			this.lbPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(4, 83);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(264, 26);
			this.label4.TabIndex = 18;
			this.label4.Text = "Пользователь 1С предприятия";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(4, 110);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(264, 26);
			this.label5.TabIndex = 19;
			this.label5.Text = "Пароль 1С предприятия";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(4, 272);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(264, 28);
			this.label10.TabIndex = 23;
			this.label10.Text = "Тайм-аут обращений к базе заданий (мс)";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtTimeOut
			// 
			this.txtTimeOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTimeOut.Location = new System.Drawing.Point(275, 275);
			this.txtTimeOut.Name = "txtTimeOut";
			this.txtTimeOut.Size = new System.Drawing.Size(748, 20);
			this.txtTimeOut.TabIndex = 14;
			this.txtTimeOut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimeOut_KeyPress);
			// 
			// txtSelfUpdatePath
			// 
			this.txtSelfUpdatePath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSelfUpdatePath.Location = new System.Drawing.Point(275, 248);
			this.txtSelfUpdatePath.Name = "txtSelfUpdatePath";
			this.txtSelfUpdatePath.Size = new System.Drawing.Size(748, 20);
			this.txtSelfUpdatePath.TabIndex = 9;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(4, 245);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(264, 26);
			this.label6.TabIndex = 14;
			this.label6.Text = "Путь для самообновления";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbIsCheck1C
			// 
			this.cbIsCheck1C.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbIsCheck1C.Checked = true;
			this.cbIsCheck1C.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbIsCheck1C.Location = new System.Drawing.Point(275, 221);
			this.cbIsCheck1C.Name = "cbIsCheck1C";
			this.cbIsCheck1C.Size = new System.Drawing.Size(748, 20);
			this.cbIsCheck1C.TabIndex = 8;
			this.cbIsCheck1C.UseVisualStyleBackColor = true;
			// 
			// txtUserTask
			// 
			this.txtUserTask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtUserTask.Location = new System.Drawing.Point(314, 58);
			this.txtUserTask.Name = "txtUserTask";
			this.txtUserTask.Size = new System.Drawing.Size(709, 20);
			this.txtUserTask.TabIndex = 12;
			// 
			// txtBaseTask
			// 
			this.txtBaseTask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBaseTask.Location = new System.Drawing.Point(314, 31);
			this.txtBaseTask.Name = "txtBaseTask";
			this.txtBaseTask.Size = new System.Drawing.Size(709, 20);
			this.txtBaseTask.TabIndex = 11;
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(4, 28);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(303, 26);
			this.label7.TabIndex = 25;
			this.label7.Text = "База заданий";
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
			this.label8.Size = new System.Drawing.Size(303, 26);
			this.label8.TabIndex = 26;
			this.label8.Text = "Имя пользователя базы заданий";
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
			this.label9.Size = new System.Drawing.Size(303, 28);
			this.label9.TabIndex = 27;
			this.label9.Text = "Пароль базы заданий";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtPasswordTask
			// 
			this.txtPasswordTask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPasswordTask.Location = new System.Drawing.Point(314, 85);
			this.txtPasswordTask.Name = "txtPasswordTask";
			this.txtPasswordTask.PasswordChar = '*';
			this.txtPasswordTask.Size = new System.Drawing.Size(709, 20);
			this.txtPasswordTask.TabIndex = 13;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnCancel.Location = new System.Drawing.Point(753, 410);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(304, 36);
			this.btnCancel.TabIndex = 19;
			this.btnCancel.Text = "Закрыть без сохранения";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnCheckConnection1C
			// 
			this.btnCheckConnection1C.Location = new System.Drawing.Point(574, 317);
			this.btnCheckConnection1C.Name = "btnCheckConnection1C";
			this.btnCheckConnection1C.Size = new System.Drawing.Size(149, 38);
			this.btnCheckConnection1C.TabIndex = 15;
			this.btnCheckConnection1C.Text = "Проверить подключение к 1С";
			this.btnCheckConnection1C.UseVisualStyleBackColor = true;
			this.btnCheckConnection1C.Click += new System.EventHandler(this.btnCheckConnection1C_Click);
			// 
			// btnCheckConnectionTasks
			// 
			this.btnCheckConnectionTasks.Location = new System.Drawing.Point(884, 317);
			this.btnCheckConnectionTasks.Name = "btnCheckConnectionTasks";
			this.btnCheckConnectionTasks.Size = new System.Drawing.Size(149, 38);
			this.btnCheckConnectionTasks.TabIndex = 17;
			this.btnCheckConnectionTasks.Text = "Проверить подключение к базе заданий";
			this.btnCheckConnectionTasks.UseVisualStyleBackColor = true;
			this.btnCheckConnectionTasks.Click += new System.EventHandler(this.btnCheckConnectionTasks_Click);
			// 
			// btnConnectTaskBase
			// 
			this.btnConnectTaskBase.Location = new System.Drawing.Point(729, 317);
			this.btnConnectTaskBase.Name = "btnConnectTaskBase";
			this.btnConnectTaskBase.Size = new System.Drawing.Size(149, 38);
			this.btnConnectTaskBase.TabIndex = 16;
			this.btnConnectTaskBase.Text = "Подключиться к базе заданий";
			this.btnConnectTaskBase.UseVisualStyleBackColor = true;
			this.btnConnectTaskBase.Click += new System.EventHandler(this.btnConnectTaskBase_Click);
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "MagicUpdater";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(117, 26);
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.settingsToolStripMenuItem.Text = "Settings";
			this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
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
			this.tableLayoutPanel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 22);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1027, 111);
			this.tableLayoutPanel1.TabIndex = 22;
			// 
			// txtServerTask
			// 
			this.txtServerTask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtServerTask.Location = new System.Drawing.Point(314, 4);
			this.txtServerTask.Name = "txtServerTask";
			this.txtServerTask.Size = new System.Drawing.Size(709, 20);
			this.txtServerTask.TabIndex = 10;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 1);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(303, 26);
			this.label3.TabIndex = 16;
			this.label3.Text = "Сервер заданий";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabControlSettings
			// 
			this.tabControlSettings.Controls.Add(this.tabPageConnectionString);
			this.tabControlSettings.Controls.Add(this.tabPageSettings);
			this.tabControlSettings.Dock = System.Windows.Forms.DockStyle.Top;
			this.tabControlSettings.ItemSize = new System.Drawing.Size(96, 18);
			this.tabControlSettings.Location = new System.Drawing.Point(10, 10);
			this.tabControlSettings.Name = "tabControlSettings";
			this.tabControlSettings.SelectedIndex = 0;
			this.tabControlSettings.Size = new System.Drawing.Size(1047, 387);
			this.tabControlSettings.TabIndex = 23;
			// 
			// tabPageConnectionString
			// 
			this.tabPageConnectionString.Controls.Add(this.btnTestSqlConnection);
			this.tabPageConnectionString.Controls.Add(this.tableLayoutPanel1);
			this.tabPageConnectionString.Location = new System.Drawing.Point(4, 22);
			this.tabPageConnectionString.Name = "tabPageConnectionString";
			this.tabPageConnectionString.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageConnectionString.Size = new System.Drawing.Size(1039, 361);
			this.tabPageConnectionString.TabIndex = 0;
			this.tabPageConnectionString.Text = "Connection String";
			this.tabPageConnectionString.UseVisualStyleBackColor = true;
			// 
			// btnTestSqlConnection
			// 
			this.btnTestSqlConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnTestSqlConnection.Location = new System.Drawing.Point(6, 147);
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
			this.tabPageSettings.Controls.Add(this.btnConnectTaskBase);
			this.tabPageSettings.Controls.Add(this.btnCheckConnection1C);
			this.tabPageSettings.Controls.Add(this.btnCheckConnectionTasks);
			this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
			this.tabPageSettings.Name = "tabPageSettings";
			this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSettings.Size = new System.Drawing.Size(1039, 361);
			this.tabPageSettings.TabIndex = 1;
			this.tabPageSettings.Text = "Settings";
			this.tabPageSettings.UseVisualStyleBackColor = true;
			// 
			// btnPrevousStep
			// 
			this.btnPrevousStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnPrevousStep.Location = new System.Drawing.Point(10, 410);
			this.btnPrevousStep.Name = "btnPrevousStep";
			this.btnPrevousStep.Size = new System.Drawing.Size(300, 36);
			this.btnPrevousStep.TabIndex = 24;
			this.btnPrevousStep.Text = "< Вернуться к настройкам базы заданий";
			this.btnPrevousStep.UseVisualStyleBackColor = true;
			this.btnPrevousStep.Click += new System.EventHandler(this.btnPrevousStep_Click);
			// 
			// btnNextStep
			// 
			this.btnNextStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnNextStep.Location = new System.Drawing.Point(318, 410);
			this.btnNextStep.Name = "btnNextStep";
			this.btnNextStep.Size = new System.Drawing.Size(294, 36);
			this.btnNextStep.TabIndex = 25;
			this.btnNextStep.Text = "Сохранить и перейти к настройкам 1С >";
			this.btnNextStep.UseVisualStyleBackColor = true;
			this.btnNextStep.Click += new System.EventHandler(this.btnNextStep_Click);
			// 
			// btnRestartService
			// 
			this.btnRestartService.BackColor = System.Drawing.SystemColors.Control;
			this.btnRestartService.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRestartService.Location = new System.Drawing.Point(653, 455);
			this.btnRestartService.Name = "btnRestartService";
			this.btnRestartService.Size = new System.Drawing.Size(166, 23);
			this.btnRestartService.TabIndex = 24;
			this.btnRestartService.Text = "btnRestartService";
			this.btnRestartService.UseVisualStyleBackColor = false;
			this.btnRestartService.Click += new System.EventHandler(this.btnRestartService_Click);
			// 
			// lbServiceStatus
			// 
			this.lbServiceStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.lbServiceStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbServiceStatus.Location = new System.Drawing.Point(825, 455);
			this.lbServiceStatus.Name = "lbServiceStatus";
			this.lbServiceStatus.Size = new System.Drawing.Size(232, 23);
			this.lbServiceStatus.TabIndex = 27;
			this.lbServiceStatus.Text = "lbServiceStatus";
			this.lbServiceStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(1067, 488);
			this.Controls.Add(this.lbServiceStatus);
			this.Controls.Add(this.btnRestartService);
			this.Controls.Add(this.btnNextStep);
			this.Controls.Add(this.btnPrevousStep);
			this.Controls.Add(this.tabControlSettings);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "SettingsForm";
			this.Padding = new System.Windows.Forms.Padding(10);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Settings";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
			this.Load += new System.EventHandler(this.SettingsForm_Load);
			this.Shown += new System.EventHandler(this.SettingsForm_Shown);
			this.Resize += new System.EventHandler(this.SettingsForm_Resize);
			this.tlpInput.ResumeLayout(false);
			this.tlpInput.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tabControlSettings.ResumeLayout(false);
			this.tabPageConnectionString.ResumeLayout(false);
			this.tabPageSettings.ResumeLayout(false);
			this.ResumeLayout(false);

        }


        #endregion
        private System.Windows.Forms.Label lbLastVersion;
        private System.Windows.Forms.Label lbShopID;
        private System.Windows.Forms.TextBox txtVersion1C;
        private System.Windows.Forms.Label lbServer;
        private System.Windows.Forms.TableLayoutPanel tlpInput;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblIsServerLocated;
        private System.Windows.Forms.CheckBox cbIsServerLocated;
        private System.Windows.Forms.ComboBox cbShopID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbIsMainCashbox;
        private System.Windows.Forms.TextBox txtSelfUpdatePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTimeOut;
        private System.Windows.Forms.Button btnCheckConnection1C;
        private System.Windows.Forms.Button btnCheckConnectionTasks;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPasswordTask;
        private System.Windows.Forms.TextBox txtUserTask;
        private System.Windows.Forms.TextBox txtBaseTask;
        private System.Windows.Forms.TextBox txtServer1C;
        private System.Windows.Forms.TextBox txtPass1C;
        private System.Windows.Forms.TextBox txtUser1C;
        private System.Windows.Forms.TextBox txtBase1C;
        private System.Windows.Forms.Button btnConnectTaskBase;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem settingsToolStripMenuItem;
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
		private Button btnRestartService;
		private Label lbServiceStatus;
	}
}