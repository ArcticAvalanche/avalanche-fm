namespace avalanche_fm
{
    partial class MainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Desktop", 2, 2);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.buttonBack = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.treeView = new System.Windows.Forms.TreeView();
            this.treeViewImageList = new System.Windows.Forms.ImageList(this.components);
            this.listView = new System.Windows.Forms.ListView();
            this.listViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stripCreateFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.stripCreateFile = new System.Windows.Forms.ToolStripMenuItem();
            this.stripCreateTxtFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.stripRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.listViewImageList = new System.Windows.Forms.ImageList(this.components);
            this.stripCreateCommonFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(12, 12);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 23);
            this.buttonBack.TabIndex = 0;
            this.buttonBack.Text = "<<";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(93, 14);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(695, 20);
            this.txtPath.TabIndex = 1;
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.treeViewImageList;
            this.treeView.Location = new System.Drawing.Point(12, 41);
            this.treeView.Name = "treeView";
            treeNode1.ImageIndex = 2;
            treeNode1.Name = "nodeDesktop";
            treeNode1.SelectedImageIndex = 2;
            treeNode1.Text = "Desktop";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(161, 397);
            this.treeView.TabIndex = 2;
            // 
            // treeViewImageList
            // 
            this.treeViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeViewImageList.ImageStream")));
            this.treeViewImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.treeViewImageList.Images.SetKeyName(0, "hard-drive-disk-icon.png");
            this.treeViewImageList.Images.SetKeyName(1, "agf_greene.png");
            this.treeViewImageList.Images.SetKeyName(2, "Computer-256x256.png");
            // 
            // listView
            // 
            this.listView.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.ContextMenuStrip = this.listViewContextMenu;
            this.listView.LargeImageList = this.listViewImageList;
            this.listView.Location = new System.Drawing.Point(179, 41);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(609, 397);
            this.listView.TabIndex = 3;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.ItemActivate += new System.EventHandler(this.listView_ItemActivate);
            // 
            // listViewContextMenu
            // 
            this.listViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripCreateFolder,
            this.stripCreateFile,
            this.toolStripSeparator1,
            this.stripRefresh,
            this.toolStripSeparator2});
            this.listViewContextMenu.Name = "listViewContextMenu";
            this.listViewContextMenu.Size = new System.Drawing.Size(143, 82);
            this.listViewContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.listViewContextMenu_Opening);
            // 
            // stripCreateFolder
            // 
            this.stripCreateFolder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripCreateCommonFolder});
            this.stripCreateFolder.Name = "stripCreateFolder";
            this.stripCreateFolder.Size = new System.Drawing.Size(180, 22);
            this.stripCreateFolder.Text = "Create folder";
            // 
            // stripCreateFile
            // 
            this.stripCreateFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripCreateTxtFile});
            this.stripCreateFile.Name = "stripCreateFile";
            this.stripCreateFile.Size = new System.Drawing.Size(180, 22);
            this.stripCreateFile.Text = "Create file";
            // 
            // stripCreateTxtFile
            // 
            this.stripCreateTxtFile.Name = "stripCreateTxtFile";
            this.stripCreateTxtFile.Size = new System.Drawing.Size(180, 22);
            this.stripCreateTxtFile.Text = "Create text file";
            this.stripCreateTxtFile.Click += new System.EventHandler(this.stripCreateTxtFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // stripRefresh
            // 
            this.stripRefresh.Name = "stripRefresh";
            this.stripRefresh.Size = new System.Drawing.Size(180, 22);
            this.stripRefresh.Text = "Refresh";
            this.stripRefresh.Click += new System.EventHandler(this.stripRefresh_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // listViewImageList
            // 
            this.listViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("listViewImageList.ImageStream")));
            this.listViewImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.listViewImageList.Images.SetKeyName(0, "hard-drive-disk-icon.png");
            this.listViewImageList.Images.SetKeyName(1, "agf_greene.png");
            // 
            // stripCreateCommonFolder
            // 
            this.stripCreateCommonFolder.Name = "stripCreateCommonFolder";
            this.stripCreateCommonFolder.Size = new System.Drawing.Size(180, 22);
            this.stripCreateCommonFolder.Text = "Common folder";
            this.stripCreateCommonFolder.Click += new System.EventHandler(this.stripCreateFolder_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.buttonBack);
            this.Name = "MainWindow";
            this.Text = "Avalanche File Manager";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.listViewContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ImageList listViewImageList;
        private System.Windows.Forms.ImageList treeViewImageList;
        private System.Windows.Forms.ContextMenuStrip listViewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem stripCreateFile;
        private System.Windows.Forms.ToolStripMenuItem stripCreateFolder;
        private System.Windows.Forms.ToolStripMenuItem stripCreateTxtFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem stripRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem stripCreateCommonFolder;
    }
}

