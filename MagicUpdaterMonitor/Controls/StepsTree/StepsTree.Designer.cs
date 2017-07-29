namespace StepTreeControl
{
    partial class StepsTree
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
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Базовая операция");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StepsTree));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPositiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNegativeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepTree = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAddPositive = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAddNegative = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonProperty = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRemove = new System.Windows.Forms.ToolStripButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPropertiesToolStripMenuItem,
            this.addPositiveToolStripMenuItem,
            this.addNegativeToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(399, 92);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // showPropertiesToolStripMenuItem
            // 
            this.showPropertiesToolStripMenuItem.Name = "showPropertiesToolStripMenuItem";
            this.showPropertiesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.showPropertiesToolStripMenuItem.Size = new System.Drawing.Size(398, 22);
            this.showPropertiesToolStripMenuItem.Text = "Свойства";
            this.showPropertiesToolStripMenuItem.Click += new System.EventHandler(this.showPropertiesToolStripMenuItem_Click);
            // 
            // addPositiveToolStripMenuItem
            // 
            this.addPositiveToolStripMenuItem.Name = "addPositiveToolStripMenuItem";
            this.addPositiveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.addPositiveToolStripMenuItem.Size = new System.Drawing.Size(398, 22);
            this.addPositiveToolStripMenuItem.Text = "Добавить действие при положительном результате";
            this.addPositiveToolStripMenuItem.Click += new System.EventHandler(this.addPositiveToolStripMenuItem_Click);
            // 
            // addNegativeToolStripMenuItem
            // 
            this.addNegativeToolStripMenuItem.Name = "addNegativeToolStripMenuItem";
            this.addNegativeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.addNegativeToolStripMenuItem.Size = new System.Drawing.Size(398, 22);
            this.addNegativeToolStripMenuItem.Text = "Добавить действие при ошибке";
            this.addNegativeToolStripMenuItem.Click += new System.EventHandler(this.addNegativeToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(398, 22);
            this.deleteToolStripMenuItem.Text = "Удалить";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // stepTree
            // 
            this.stepTree.ContextMenuStrip = this.contextMenuStrip1;
            this.stepTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stepTree.ImageIndex = 0;
            this.stepTree.ImageList = this.imageList1;
            this.stepTree.Indent = 20;
            this.stepTree.ItemHeight = 35;
            this.stepTree.Location = new System.Drawing.Point(0, 25);
            this.stepTree.Name = "stepTree";
            treeNode2.Name = "RootNode";
            treeNode2.Text = "Базовая операция";
            this.stepTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.stepTree.SelectedImageIndex = 0;
            this.stepTree.Size = new System.Drawing.Size(699, 400);
            this.stepTree.TabIndex = 1;
            this.stepTree.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.stepTree_BeforeCollapse);
            this.stepTree.DoubleClick += new System.EventHandler(this.stepTree_DoubleClick);
            this.stepTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.stepTree_MouseDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Apply.png");
            this.imageList1.Images.SetKeyName(1, "Delete.png");
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAddPositive,
            this.toolStripButtonAddNegative,
            this.toolStripButtonProperty,
            this.toolStripButtonRemove});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(699, 25);
            this.toolStrip2.TabIndex = 14;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButtonAddPositive
            // 
            this.toolStripButtonAddPositive.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddPositive.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddPositive.Image")));
            this.toolStripButtonAddPositive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddPositive.Name = "toolStripButtonAddPositive";
            this.toolStripButtonAddPositive.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAddPositive.Text = "toolStripButton2";
            this.toolStripButtonAddPositive.ToolTipText = "Добавить действие при положительном результате";
            this.toolStripButtonAddPositive.Click += new System.EventHandler(this.toolStripButtonAddPositive_Click);
            // 
            // toolStripButtonAddNegative
            // 
            this.toolStripButtonAddNegative.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddNegative.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddNegative.Image")));
            this.toolStripButtonAddNegative.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddNegative.Name = "toolStripButtonAddNegative";
            this.toolStripButtonAddNegative.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAddNegative.Text = "toolStripButton3";
            this.toolStripButtonAddNegative.ToolTipText = "Добавить действие при ошибке";
            this.toolStripButtonAddNegative.Click += new System.EventHandler(this.toolStripButtonAddNegative_Click);
            // 
            // toolStripButtonProperty
            // 
            this.toolStripButtonProperty.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonProperty.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonProperty.Image")));
            this.toolStripButtonProperty.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonProperty.Name = "toolStripButtonProperty";
            this.toolStripButtonProperty.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonProperty.Text = "toolStripButton4";
            this.toolStripButtonProperty.ToolTipText = "Свойства";
            this.toolStripButtonProperty.Click += new System.EventHandler(this.toolStripButtonProperty_Click);
            // 
            // toolStripButtonRemove
            // 
            this.toolStripButtonRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRemove.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRemove.Image")));
            this.toolStripButtonRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemove.Name = "toolStripButtonRemove";
            this.toolStripButtonRemove.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRemove.Text = "toolStripButton5";
            this.toolStripButtonRemove.ToolTipText = "Удалить выделенный элемент";
            this.toolStripButtonRemove.Click += new System.EventHandler(this.toolStripButtonRemove_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // StepsTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.stepTree);
            this.Controls.Add(this.toolStrip2);
            this.Name = "StepsTree";
            this.Size = new System.Drawing.Size(699, 425);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPositiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNegativeToolStripMenuItem;
        public System.Windows.Forms.TreeView stepTree;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddPositive;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddNegative;
        private System.Windows.Forms.ToolStripButton toolStripButtonProperty;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemove;
        private System.Windows.Forms.Timer timer1;
    }
}
