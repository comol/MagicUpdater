namespace MagicUpdateObfuscationBuilder
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
			this.gbProjects = new System.Windows.Forms.GroupBox();
			this.chbScheduler = new System.Windows.Forms.CheckBox();
			this.chbMonitor = new System.Windows.Forms.CheckBox();
			this.chbAgentInstaller = new System.Windows.Forms.CheckBox();
			this.chbAgent = new System.Windows.Forms.CheckBox();
			this.gbSolution = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnOpenSolutionFile = new System.Windows.Forms.Button();
			this.tbSolutionFile = new System.Windows.Forms.TextBox();
			this.cbConfiguration = new System.Windows.Forms.ComboBox();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.gbOutput = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnChooseSaveFolder = new System.Windows.Forms.Button();
			this.tbSaveFolder = new System.Windows.Forms.TextBox();
			this.btnBuild = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.rtbLog = new System.Windows.Forms.RichTextBox();
			this.gbProjects.SuspendLayout();
			this.gbSolution.SuspendLayout();
			this.gbOutput.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbProjects
			// 
			this.gbProjects.Controls.Add(this.chbScheduler);
			this.gbProjects.Controls.Add(this.chbMonitor);
			this.gbProjects.Controls.Add(this.chbAgentInstaller);
			this.gbProjects.Controls.Add(this.chbAgent);
			this.gbProjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gbProjects.Location = new System.Drawing.Point(7, 4);
			this.gbProjects.Name = "gbProjects";
			this.gbProjects.Size = new System.Drawing.Size(175, 117);
			this.gbProjects.TabIndex = 0;
			this.gbProjects.TabStop = false;
			this.gbProjects.Text = "Проекты:";
			// 
			// chbScheduler
			// 
			this.chbScheduler.AutoSize = true;
			this.chbScheduler.Checked = true;
			this.chbScheduler.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbScheduler.Location = new System.Drawing.Point(14, 90);
			this.chbScheduler.Name = "chbScheduler";
			this.chbScheduler.Size = new System.Drawing.Size(74, 17);
			this.chbScheduler.TabIndex = 5;
			this.chbScheduler.Text = "Scheduler";
			this.chbScheduler.UseVisualStyleBackColor = true;
			// 
			// chbMonitor
			// 
			this.chbMonitor.AutoSize = true;
			this.chbMonitor.Checked = true;
			this.chbMonitor.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbMonitor.Location = new System.Drawing.Point(14, 67);
			this.chbMonitor.Name = "chbMonitor";
			this.chbMonitor.Size = new System.Drawing.Size(61, 17);
			this.chbMonitor.TabIndex = 4;
			this.chbMonitor.Text = "Monitor";
			this.chbMonitor.UseVisualStyleBackColor = true;
			// 
			// chbAgentInstaller
			// 
			this.chbAgentInstaller.AutoSize = true;
			this.chbAgentInstaller.Checked = true;
			this.chbAgentInstaller.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbAgentInstaller.Location = new System.Drawing.Point(14, 44);
			this.chbAgentInstaller.Name = "chbAgentInstaller";
			this.chbAgentInstaller.Size = new System.Drawing.Size(90, 17);
			this.chbAgentInstaller.TabIndex = 3;
			this.chbAgentInstaller.Text = "AgentInstaller";
			this.chbAgentInstaller.UseVisualStyleBackColor = true;
			// 
			// chbAgent
			// 
			this.chbAgent.AutoSize = true;
			this.chbAgent.Checked = true;
			this.chbAgent.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbAgent.Location = new System.Drawing.Point(14, 21);
			this.chbAgent.Name = "chbAgent";
			this.chbAgent.Size = new System.Drawing.Size(54, 17);
			this.chbAgent.TabIndex = 2;
			this.chbAgent.Text = "Agent";
			this.chbAgent.UseVisualStyleBackColor = true;
			// 
			// gbSolution
			// 
			this.gbSolution.Controls.Add(this.label2);
			this.gbSolution.Controls.Add(this.label1);
			this.gbSolution.Controls.Add(this.btnOpenSolutionFile);
			this.gbSolution.Controls.Add(this.tbSolutionFile);
			this.gbSolution.Controls.Add(this.cbConfiguration);
			this.gbSolution.Location = new System.Drawing.Point(188, 4);
			this.gbSolution.Name = "gbSolution";
			this.gbSolution.Size = new System.Drawing.Size(335, 117);
			this.gbSolution.TabIndex = 1;
			this.gbSolution.TabStop = false;
			this.gbSolution.Text = "Параметры решения:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 70);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(118, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Выбор конфигурации:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(178, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Путь к решению MagicUpdater.sln:";
			// 
			// btnOpenSolutionFile
			// 
			this.btnOpenSolutionFile.Location = new System.Drawing.Point(303, 40);
			this.btnOpenSolutionFile.Name = "btnOpenSolutionFile";
			this.btnOpenSolutionFile.Size = new System.Drawing.Size(26, 22);
			this.btnOpenSolutionFile.TabIndex = 2;
			this.btnOpenSolutionFile.Text = "...";
			this.btnOpenSolutionFile.UseVisualStyleBackColor = true;
			this.btnOpenSolutionFile.Click += new System.EventHandler(this.btnOpenSolutionFile_Click);
			// 
			// tbSolutionFile
			// 
			this.tbSolutionFile.Location = new System.Drawing.Point(6, 41);
			this.tbSolutionFile.Name = "tbSolutionFile";
			this.tbSolutionFile.Size = new System.Drawing.Size(297, 20);
			this.tbSolutionFile.TabIndex = 1;
			this.tbSolutionFile.Leave += new System.EventHandler(this.tbSolutionFile_Leave);
			// 
			// cbConfiguration
			// 
			this.cbConfiguration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbConfiguration.FormattingEnabled = true;
			this.cbConfiguration.Location = new System.Drawing.Point(6, 86);
			this.cbConfiguration.Name = "cbConfiguration";
			this.cbConfiguration.Size = new System.Drawing.Size(323, 21);
			this.cbConfiguration.TabIndex = 0;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "VSSolutionFiles(*.sln)|*.sln";
			// 
			// gbOutput
			// 
			this.gbOutput.Controls.Add(this.label3);
			this.gbOutput.Controls.Add(this.btnChooseSaveFolder);
			this.gbOutput.Controls.Add(this.tbSaveFolder);
			this.gbOutput.Location = new System.Drawing.Point(7, 127);
			this.gbOutput.Name = "gbOutput";
			this.gbOutput.Size = new System.Drawing.Size(516, 78);
			this.gbOutput.TabIndex = 2;
			this.gbOutput.TabStop = false;
			this.gbOutput.Text = "Параметры сохранения:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 23);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(125, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Папка для сохранения:";
			// 
			// btnChooseSaveFolder
			// 
			this.btnChooseSaveFolder.Location = new System.Drawing.Point(484, 41);
			this.btnChooseSaveFolder.Name = "btnChooseSaveFolder";
			this.btnChooseSaveFolder.Size = new System.Drawing.Size(26, 22);
			this.btnChooseSaveFolder.TabIndex = 4;
			this.btnChooseSaveFolder.Text = "...";
			this.btnChooseSaveFolder.UseVisualStyleBackColor = true;
			this.btnChooseSaveFolder.Click += new System.EventHandler(this.button1_Click);
			// 
			// tbSaveFolder
			// 
			this.tbSaveFolder.Location = new System.Drawing.Point(13, 42);
			this.tbSaveFolder.Name = "tbSaveFolder";
			this.tbSaveFolder.Size = new System.Drawing.Size(471, 20);
			this.tbSaveFolder.TabIndex = 3;
			// 
			// btnBuild
			// 
			this.btnBuild.Location = new System.Drawing.Point(7, 379);
			this.btnBuild.Name = "btnBuild";
			this.btnBuild.Size = new System.Drawing.Size(112, 48);
			this.btnBuild.TabIndex = 3;
			this.btnBuild.Text = "Собрать!";
			this.btnBuild.UseVisualStyleBackColor = true;
			this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(411, 379);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(112, 48);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Выйти";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// rtbLog
			// 
			this.rtbLog.Location = new System.Drawing.Point(7, 211);
			this.rtbLog.Name = "rtbLog";
			this.rtbLog.ReadOnly = true;
			this.rtbLog.Size = new System.Drawing.Size(516, 162);
			this.rtbLog.TabIndex = 5;
			this.rtbLog.Text = "";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(530, 434);
			this.Controls.Add(this.rtbLog);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnBuild);
			this.Controls.Add(this.gbOutput);
			this.Controls.Add(this.gbSolution);
			this.Controls.Add(this.gbProjects);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MagicUpdateObfuscationBuilder";
			this.gbProjects.ResumeLayout(false);
			this.gbProjects.PerformLayout();
			this.gbSolution.ResumeLayout(false);
			this.gbSolution.PerformLayout();
			this.gbOutput.ResumeLayout(false);
			this.gbOutput.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbProjects;
		private System.Windows.Forms.GroupBox gbSolution;
		private System.Windows.Forms.CheckBox chbScheduler;
		private System.Windows.Forms.CheckBox chbMonitor;
		private System.Windows.Forms.CheckBox chbAgentInstaller;
		private System.Windows.Forms.CheckBox chbAgent;
		private System.Windows.Forms.ComboBox cbConfiguration;
		private System.Windows.Forms.TextBox tbSolutionFile;
		private System.Windows.Forms.Button btnOpenSolutionFile;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox gbOutput;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnChooseSaveFolder;
		private System.Windows.Forms.TextBox tbSaveFolder;
		private System.Windows.Forms.Button btnBuild;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.RichTextBox rtbLog;
	}
}

