namespace MagicUpdaterMonitor.Forms
{
	partial class AgentSettingsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentSettingsForm));
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.lbShopId = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.tbUser1C = new System.Windows.Forms.TextBox();
			this.tbPass1C = new System.Windows.Forms.TextBox();
			this.tbOperationsListCheckTimeout = new System.Windows.Forms.TextBox();
			this.tbSelfUpdatePath = new System.Windows.Forms.TextBox();
			this.tbVersion1C = new System.Windows.Forms.TextBox();
			this.tbSelfUpdateFtpServer = new System.Windows.Forms.TextBox();
			this.tbSelfUpdateFtpUser = new System.Windows.Forms.TextBox();
			this.tbSelfUpdateFtpPassword = new System.Windows.Forms.TextBox();
			this.tbSelfUpdateFtpPath = new System.Windows.Forms.TextBox();
			this.chbIsCheck1C = new System.Windows.Forms.CheckBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.rbServerBase = new System.Windows.Forms.RadioButton();
			this.rbFileBase = new System.Windows.Forms.RadioButton();
			this.lbServerOrPath1C = new System.Windows.Forms.Label();
			this.lbBase1C = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.tbServerOrPath1C = new System.Windows.Forms.TextBox();
			this.tbBase1C = new System.Windows.Forms.TextBox();
			this.lbSettingsReadingCheck = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(547, 367);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(180, 30);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Отменить";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(12, 367);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(180, 30);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "Сохранить";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.lbShopId, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.label8, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.label9, 0, 8);
			this.tableLayoutPanel1.Controls.Add(this.label10, 0, 9);
			this.tableLayoutPanel1.Controls.Add(this.label11, 0, 10);
			this.tableLayoutPanel1.Controls.Add(this.label12, 0, 11);
			this.tableLayoutPanel1.Controls.Add(this.tbUser1C, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.tbPass1C, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.tbOperationsListCheckTimeout, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.tbSelfUpdatePath, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.tbVersion1C, 1, 6);
			this.tableLayoutPanel1.Controls.Add(this.tbSelfUpdateFtpServer, 1, 8);
			this.tableLayoutPanel1.Controls.Add(this.tbSelfUpdateFtpUser, 1, 9);
			this.tableLayoutPanel1.Controls.Add(this.tbSelfUpdateFtpPassword, 1, 10);
			this.tableLayoutPanel1.Controls.Add(this.tbSelfUpdateFtpPath, 1, 11);
			this.tableLayoutPanel1.Controls.Add(this.chbIsCheck1C, 1, 7);
			this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 12;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(739, 345);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// lbShopId
			// 
			this.lbShopId.AutoSize = true;
			this.lbShopId.BackColor = System.Drawing.Color.Gold;
			this.tableLayoutPanel1.SetColumnSpan(this.lbShopId, 2);
			this.lbShopId.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbShopId.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lbShopId.Location = new System.Drawing.Point(4, 1);
			this.lbShopId.Name = "lbShopId";
			this.lbShopId.Size = new System.Drawing.Size(731, 27);
			this.lbShopId.TabIndex = 4;
			this.lbShopId.Text = "lbShopId";
			this.lbShopId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(4, 81);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(362, 26);
			this.label3.TabIndex = 0;
			this.label3.Text = "Пользователь 1С";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(4, 108);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(362, 26);
			this.label4.TabIndex = 0;
			this.label4.Text = "Пароль 1С";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Location = new System.Drawing.Point(4, 135);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(362, 26);
			this.label5.TabIndex = 0;
			this.label5.Text = "Интервал опроса операций из базы заданий (мс)";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Location = new System.Drawing.Point(4, 162);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(362, 26);
			this.label6.TabIndex = 0;
			this.label6.Text = "Локальный путь самообновления";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label7.Location = new System.Drawing.Point(4, 189);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(362, 26);
			this.label7.TabIndex = 0;
			this.label7.Text = "Версия 1С";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label8.Location = new System.Drawing.Point(4, 216);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(362, 20);
			this.label8.TabIndex = 0;
			this.label8.Text = "Проверка установки 1С";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label9.Location = new System.Drawing.Point(4, 237);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(362, 26);
			this.label9.TabIndex = 0;
			this.label9.Text = "Сервер Ftp для самообновления";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label10.Location = new System.Drawing.Point(4, 264);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(362, 26);
			this.label10.TabIndex = 0;
			this.label10.Text = "Логин сервера Ftp для самообновления";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label11.Location = new System.Drawing.Point(4, 291);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(362, 26);
			this.label11.TabIndex = 0;
			this.label11.Text = "Пароль сервера Ftp для самообновления";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label12.Location = new System.Drawing.Point(4, 318);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(362, 26);
			this.label12.TabIndex = 0;
			this.label12.Text = "Путь до папки Ftp для самообновления";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tbUser1C
			// 
			this.tbUser1C.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbUser1C.Location = new System.Drawing.Point(373, 84);
			this.tbUser1C.Name = "tbUser1C";
			this.tbUser1C.Size = new System.Drawing.Size(362, 20);
			this.tbUser1C.TabIndex = 1;
			this.tbUser1C.TextChanged += new System.EventHandler(this.tbAll_TextChanged);
			// 
			// tbPass1C
			// 
			this.tbPass1C.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbPass1C.Location = new System.Drawing.Point(373, 111);
			this.tbPass1C.Name = "tbPass1C";
			this.tbPass1C.Size = new System.Drawing.Size(362, 20);
			this.tbPass1C.TabIndex = 1;
			this.tbPass1C.TextChanged += new System.EventHandler(this.tbAll_TextChanged);
			// 
			// tbOperationsListCheckTimeout
			// 
			this.tbOperationsListCheckTimeout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbOperationsListCheckTimeout.Location = new System.Drawing.Point(373, 138);
			this.tbOperationsListCheckTimeout.Name = "tbOperationsListCheckTimeout";
			this.tbOperationsListCheckTimeout.Size = new System.Drawing.Size(362, 20);
			this.tbOperationsListCheckTimeout.TabIndex = 1;
			this.tbOperationsListCheckTimeout.TextChanged += new System.EventHandler(this.tbAll_TextChanged);
			this.tbOperationsListCheckTimeout.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbOperationsListCheckTimeout_KeyPress);
			// 
			// tbSelfUpdatePath
			// 
			this.tbSelfUpdatePath.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbSelfUpdatePath.Location = new System.Drawing.Point(373, 165);
			this.tbSelfUpdatePath.Name = "tbSelfUpdatePath";
			this.tbSelfUpdatePath.Size = new System.Drawing.Size(362, 20);
			this.tbSelfUpdatePath.TabIndex = 1;
			this.tbSelfUpdatePath.TextChanged += new System.EventHandler(this.tbAll_TextChanged);
			// 
			// tbVersion1C
			// 
			this.tbVersion1C.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbVersion1C.Location = new System.Drawing.Point(373, 192);
			this.tbVersion1C.Name = "tbVersion1C";
			this.tbVersion1C.Size = new System.Drawing.Size(362, 20);
			this.tbVersion1C.TabIndex = 1;
			this.tbVersion1C.TextChanged += new System.EventHandler(this.tbAll_TextChanged);
			// 
			// tbSelfUpdateFtpServer
			// 
			this.tbSelfUpdateFtpServer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbSelfUpdateFtpServer.Location = new System.Drawing.Point(373, 240);
			this.tbSelfUpdateFtpServer.Name = "tbSelfUpdateFtpServer";
			this.tbSelfUpdateFtpServer.Size = new System.Drawing.Size(362, 20);
			this.tbSelfUpdateFtpServer.TabIndex = 1;
			this.tbSelfUpdateFtpServer.TextChanged += new System.EventHandler(this.tbAll_TextChanged);
			// 
			// tbSelfUpdateFtpUser
			// 
			this.tbSelfUpdateFtpUser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbSelfUpdateFtpUser.Location = new System.Drawing.Point(373, 267);
			this.tbSelfUpdateFtpUser.Name = "tbSelfUpdateFtpUser";
			this.tbSelfUpdateFtpUser.Size = new System.Drawing.Size(362, 20);
			this.tbSelfUpdateFtpUser.TabIndex = 1;
			this.tbSelfUpdateFtpUser.TextChanged += new System.EventHandler(this.tbAll_TextChanged);
			// 
			// tbSelfUpdateFtpPassword
			// 
			this.tbSelfUpdateFtpPassword.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbSelfUpdateFtpPassword.Location = new System.Drawing.Point(373, 294);
			this.tbSelfUpdateFtpPassword.Name = "tbSelfUpdateFtpPassword";
			this.tbSelfUpdateFtpPassword.Size = new System.Drawing.Size(362, 20);
			this.tbSelfUpdateFtpPassword.TabIndex = 1;
			this.tbSelfUpdateFtpPassword.TextChanged += new System.EventHandler(this.tbAll_TextChanged);
			// 
			// tbSelfUpdateFtpPath
			// 
			this.tbSelfUpdateFtpPath.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbSelfUpdateFtpPath.Location = new System.Drawing.Point(373, 321);
			this.tbSelfUpdateFtpPath.Name = "tbSelfUpdateFtpPath";
			this.tbSelfUpdateFtpPath.Size = new System.Drawing.Size(362, 20);
			this.tbSelfUpdateFtpPath.TabIndex = 1;
			this.tbSelfUpdateFtpPath.TextChanged += new System.EventHandler(this.tbAll_TextChanged);
			// 
			// chbIsCheck1C
			// 
			this.chbIsCheck1C.AutoSize = true;
			this.chbIsCheck1C.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chbIsCheck1C.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chbIsCheck1C.Location = new System.Drawing.Point(373, 219);
			this.chbIsCheck1C.Name = "chbIsCheck1C";
			this.chbIsCheck1C.Size = new System.Drawing.Size(362, 14);
			this.chbIsCheck1C.TabIndex = 2;
			this.chbIsCheck1C.UseVisualStyleBackColor = true;
			this.chbIsCheck1C.CheckedChanged += new System.EventHandler(this.chbIsCheck1C_CheckedChanged);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.tableLayoutPanel2);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(1, 29);
			this.panel2.Margin = new System.Windows.Forms.Padding(0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(368, 51);
			this.panel2.TabIndex = 7;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.01657F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.98343F));
			this.tableLayoutPanel2.Controls.Add(this.rbServerBase, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.rbFileBase, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.lbServerOrPath1C, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.lbBase1C, 1, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(368, 51);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// rbServerBase
			// 
			this.rbServerBase.AutoSize = true;
			this.rbServerBase.Checked = true;
			this.rbServerBase.Location = new System.Drawing.Point(10, 3);
			this.rbServerBase.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			this.rbServerBase.Name = "rbServerBase";
			this.rbServerBase.Size = new System.Drawing.Size(113, 17);
			this.rbServerBase.TabIndex = 0;
			this.rbServerBase.TabStop = true;
			this.rbServerBase.Text = "Серврерная база";
			this.rbServerBase.UseVisualStyleBackColor = true;
			this.rbServerBase.CheckedChanged += new System.EventHandler(this.rbServerBase_CheckedChanged);
			// 
			// rbFileBase
			// 
			this.rbFileBase.AutoSize = true;
			this.rbFileBase.Location = new System.Drawing.Point(10, 28);
			this.rbFileBase.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			this.rbFileBase.Name = "rbFileBase";
			this.rbFileBase.Size = new System.Drawing.Size(105, 17);
			this.rbFileBase.TabIndex = 0;
			this.rbFileBase.Text = "Файловая база";
			this.rbFileBase.UseVisualStyleBackColor = true;
			this.rbFileBase.CheckedChanged += new System.EventHandler(this.rbFileBase_CheckedChanged);
			// 
			// lbServerOrPath1C
			// 
			this.lbServerOrPath1C.AutoSize = true;
			this.lbServerOrPath1C.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbServerOrPath1C.Location = new System.Drawing.Point(139, 0);
			this.lbServerOrPath1C.Name = "lbServerOrPath1C";
			this.lbServerOrPath1C.Size = new System.Drawing.Size(226, 25);
			this.lbServerOrPath1C.TabIndex = 1;
			this.lbServerOrPath1C.Text = "Сервер 1С";
			this.lbServerOrPath1C.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbBase1C
			// 
			this.lbBase1C.AutoSize = true;
			this.lbBase1C.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbBase1C.Location = new System.Drawing.Point(139, 25);
			this.lbBase1C.Name = "lbBase1C";
			this.lbBase1C.Size = new System.Drawing.Size(226, 26);
			this.lbBase1C.TabIndex = 1;
			this.lbBase1C.Text = "База 1С";
			this.lbBase1C.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tableLayoutPanel3);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(370, 29);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(368, 51);
			this.panel1.TabIndex = 8;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Controls.Add(this.tbServerOrPath1C, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.tbBase1C, 0, 1);
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(368, 51);
			this.tableLayoutPanel3.TabIndex = 1;
			// 
			// tbServerOrPath1C
			// 
			this.tbServerOrPath1C.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbServerOrPath1C.Location = new System.Drawing.Point(3, 3);
			this.tbServerOrPath1C.Name = "tbServerOrPath1C";
			this.tbServerOrPath1C.Size = new System.Drawing.Size(362, 20);
			this.tbServerOrPath1C.TabIndex = 0;
			this.tbServerOrPath1C.TextChanged += new System.EventHandler(this.tbAll_TextChanged);
			// 
			// tbBase1C
			// 
			this.tbBase1C.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbBase1C.Location = new System.Drawing.Point(3, 28);
			this.tbBase1C.Name = "tbBase1C";
			this.tbBase1C.Size = new System.Drawing.Size(362, 20);
			this.tbBase1C.TabIndex = 0;
			this.tbBase1C.TextChanged += new System.EventHandler(this.tbAll_TextChanged);
			// 
			// lbSettingsReadingCheck
			// 
			this.lbSettingsReadingCheck.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.lbSettingsReadingCheck.BackColor = System.Drawing.Color.Salmon;
			this.lbSettingsReadingCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lbSettingsReadingCheck.Location = new System.Drawing.Point(239, 362);
			this.lbSettingsReadingCheck.Name = "lbSettingsReadingCheck";
			this.lbSettingsReadingCheck.Size = new System.Drawing.Size(261, 39);
			this.lbSettingsReadingCheck.TabIndex = 3;
			this.lbSettingsReadingCheck.Text = "lbSettingsReadingCheck";
			this.lbSettingsReadingCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// AgentSettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(739, 406);
			this.Controls.Add(this.lbSettingsReadingCheck);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AgentSettingsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Настройки агента";
			this.Load += new System.EventHandler(this.AgentSettingsForm_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox tbUser1C;
		private System.Windows.Forms.TextBox tbPass1C;
		private System.Windows.Forms.TextBox tbOperationsListCheckTimeout;
		private System.Windows.Forms.TextBox tbSelfUpdatePath;
		private System.Windows.Forms.TextBox tbVersion1C;
		private System.Windows.Forms.TextBox tbSelfUpdateFtpServer;
		private System.Windows.Forms.TextBox tbSelfUpdateFtpUser;
		private System.Windows.Forms.TextBox tbSelfUpdateFtpPassword;
		private System.Windows.Forms.TextBox tbSelfUpdateFtpPath;
		private System.Windows.Forms.CheckBox chbIsCheck1C;
		private System.Windows.Forms.Label lbShopId;
		private System.Windows.Forms.Label lbSettingsReadingCheck;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.TextBox tbServerOrPath1C;
		private System.Windows.Forms.TextBox tbBase1C;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.RadioButton rbServerBase;
		private System.Windows.Forms.RadioButton rbFileBase;
		private System.Windows.Forms.Label lbServerOrPath1C;
		private System.Windows.Forms.Label lbBase1C;
	}
}