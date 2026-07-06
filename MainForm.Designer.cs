using System.Windows.Forms;

namespace FileManager
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
            this.panelLeft = new System.Windows.Forms.Panel();
            this.cboLeftDrive = new System.Windows.Forms.ComboBox();
            this.txtLeftPath = new System.Windows.Forms.TextBox();
            this.treeViewDirectories = new System.Windows.Forms.TreeView();
            this.listViewLeft = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuLeft = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.smallImageList = new System.Windows.Forms.ImageList(this.components);
            this.panelRight = new System.Windows.Forms.Panel();
            this.cboRightDrive = new System.Windows.Forms.ComboBox();
            this.txtRightPath = new System.Windows.Forms.TextBox();
            this.listViewRight = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panelSearch = new System.Windows.Forms.Panel();
            this.lblSearchStatus = new System.Windows.Forms.Label();
            this.progressBarSearch = new System.Windows.Forms.ProgressBar();
            this.listViewSearchResults = new System.Windows.Forms.ListView();
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnCancelSearch = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.chkSearchSubdirs = new System.Windows.Forms.CheckBox();
            this.chkCaseSensitive = new System.Windows.Forms.CheckBox();
            this.txtExtension = new System.Windows.Forms.TextBox();
            this.lblExtension = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.lblFileName = new System.Windows.Forms.Label();
            this.btnSearchPanel = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnProperties = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnMoveToRight = new System.Windows.Forms.Button();
            this.btnCopyToRight = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblItemCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSelectedSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.largeImageList = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileNewFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditMove = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditRename = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditDeselectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewLargeIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewSmallIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuViewToggleTree = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewToggleSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuToolsRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuHelpDevelopment = new System.Windows.Forms.ToolStripMenuItem();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.controlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLeft.Controls.Add(this.cboLeftDrive);
            this.panelLeft.Controls.Add(this.txtLeftPath);
            this.panelLeft.Controls.Add(this.treeViewDirectories);
            this.panelLeft.Controls.Add(this.listViewLeft);
            this.panelLeft.Location = new System.Drawing.Point(11, 43);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(651, 448);
            this.panelLeft.TabIndex = 0;
            // 
            // cboLeftDrive
            // 
            this.cboLeftDrive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLeftDrive.FormattingEnabled = true;
            this.cboLeftDrive.Location = new System.Drawing.Point(11, 11);
            this.cboLeftDrive.Name = "cboLeftDrive";
            this.cboLeftDrive.Size = new System.Drawing.Size(171, 24);
            this.cboLeftDrive.TabIndex = 0;
            // 
            // txtLeftPath
            // 
            this.txtLeftPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLeftPath.Location = new System.Drawing.Point(194, 11);
            this.txtLeftPath.Name = "txtLeftPath";
            this.txtLeftPath.Size = new System.Drawing.Size(866, 22);
            this.txtLeftPath.TabIndex = 1;
            // 
            // treeViewDirectories
            // 
            this.treeViewDirectories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewDirectories.HideSelection = false;
            this.treeViewDirectories.Location = new System.Drawing.Point(11, 48);
            this.treeViewDirectories.Name = "treeViewDirectories";
            this.treeViewDirectories.Size = new System.Drawing.Size(285, 728);
            this.treeViewDirectories.TabIndex = 2;
            // 
            // listViewLeft
            // 
            this.listViewLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewLeft.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listViewLeft.ContextMenuStrip = this.contextMenuLeft;
            this.listViewLeft.FullRowSelect = true;
            this.listViewLeft.HideSelection = false;
            this.listViewLeft.Location = new System.Drawing.Point(309, 48);
            this.listViewLeft.Name = "listViewLeft";
            this.listViewLeft.Size = new System.Drawing.Size(751, 728);
            this.listViewLeft.SmallImageList = this.smallImageList;
            this.listViewLeft.TabIndex = 3;
            this.listViewLeft.UseCompatibleStateImageBehavior = false;
            this.listViewLeft.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Имя";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Размер";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Тип";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Изменен";
            this.columnHeader4.Width = 120;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Атрибуты";
            this.columnHeader5.Width = 100;
            // 
            // contextMenuLeft
            // 
            this.contextMenuLeft.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuLeft.Name = "contextMenuLeft";
            this.contextMenuLeft.Size = new System.Drawing.Size(61, 4);
            // 
            // smallImageList
            // 
            this.smallImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.smallImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.smallImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panelRight
            // 
            this.panelRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRight.Controls.Add(this.cboRightDrive);
            this.panelRight.Controls.Add(this.txtRightPath);
            this.panelRight.Controls.Add(this.listViewRight);
            this.panelRight.Location = new System.Drawing.Point(709, 43);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(616, 448);
            this.panelRight.TabIndex = 1;
            // 
            // cboRightDrive
            // 
            this.cboRightDrive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRightDrive.FormattingEnabled = true;
            this.cboRightDrive.Location = new System.Drawing.Point(11, 11);
            this.cboRightDrive.Name = "cboRightDrive";
            this.cboRightDrive.Size = new System.Drawing.Size(171, 24);
            this.cboRightDrive.TabIndex = 0;
            // 
            // txtRightPath
            // 
            this.txtRightPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRightPath.Location = new System.Drawing.Point(194, 11);
            this.txtRightPath.Name = "txtRightPath";
            this.txtRightPath.Size = new System.Drawing.Size(831, 22);
            this.txtRightPath.TabIndex = 1;
            // 
            // listViewRight
            // 
            this.listViewRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewRight.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10});
            this.listViewRight.ContextMenuStrip = this.contextMenuRight;
            this.listViewRight.FullRowSelect = true;
            this.listViewRight.HideSelection = false;
            this.listViewRight.Location = new System.Drawing.Point(11, 48);
            this.listViewRight.Name = "listViewRight";
            this.listViewRight.Size = new System.Drawing.Size(1014, 728);
            this.listViewRight.SmallImageList = this.smallImageList;
            this.listViewRight.TabIndex = 2;
            this.listViewRight.UseCompatibleStateImageBehavior = false;
            this.listViewRight.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Имя";
            this.columnHeader6.Width = 150;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Размер";
            this.columnHeader7.Width = 80;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Тип";
            this.columnHeader8.Width = 80;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Изменен";
            this.columnHeader9.Width = 120;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Атрибуты";
            this.columnHeader10.Width = 100;
            // 
            // contextMenuRight
            // 
            this.contextMenuRight.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuRight.Name = "contextMenuRight";
            this.contextMenuRight.Size = new System.Drawing.Size(61, 4);
            // 
            // panelSearch
            // 
            this.panelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearch.Controls.Add(this.lblSearchStatus);
            this.panelSearch.Controls.Add(this.progressBarSearch);
            this.panelSearch.Controls.Add(this.listViewSearchResults);
            this.panelSearch.Controls.Add(this.btnCancelSearch);
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this.chkSearchSubdirs);
            this.panelSearch.Controls.Add(this.chkCaseSensitive);
            this.panelSearch.Controls.Add(this.txtExtension);
            this.panelSearch.Controls.Add(this.lblExtension);
            this.panelSearch.Controls.Add(this.txtFileName);
            this.panelSearch.Controls.Add(this.lblFileName);
            this.panelSearch.Location = new System.Drawing.Point(11, 501);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(1314, 192);
            this.panelSearch.TabIndex = 2;
            this.panelSearch.Visible = false;
            // 
            // lblSearchStatus
            // 
            this.lblSearchStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearchStatus.Location = new System.Drawing.Point(2283, 243);
            this.lblSearchStatus.Name = "lblSearchStatus";
            this.lblSearchStatus.Size = new System.Drawing.Size(103, 21);
            this.lblSearchStatus.TabIndex = 12;
            this.lblSearchStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progressBarSearch
            // 
            this.progressBarSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarSearch.Location = new System.Drawing.Point(11, 243);
            this.progressBarSearch.Name = "progressBarSearch";
            this.progressBarSearch.Size = new System.Drawing.Size(2261, 21);
            this.progressBarSearch.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarSearch.TabIndex = 11;
            this.progressBarSearch.Visible = false;
            // 
            // listViewSearchResults
            // 
            this.listViewSearchResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15});
            this.listViewSearchResults.FullRowSelect = true;
            this.listViewSearchResults.HideSelection = false;
            this.listViewSearchResults.Location = new System.Drawing.Point(11, 48);
            this.listViewSearchResults.MultiSelect = false;
            this.listViewSearchResults.Name = "listViewSearchResults";
            this.listViewSearchResults.Size = new System.Drawing.Size(2680, 190);
            this.listViewSearchResults.TabIndex = 7;
            this.listViewSearchResults.UseCompatibleStateImageBehavior = false;
            this.listViewSearchResults.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Имя";
            this.columnHeader11.Width = 200;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Путь";
            this.columnHeader12.Width = 400;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Размер";
            this.columnHeader13.Width = 100;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Тип";
            this.columnHeader14.Width = 80;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Изменен";
            this.columnHeader15.Width = 120;
            // 
            // btnCancelSearch
            // 
            this.btnCancelSearch.Enabled = false;
            this.btnCancelSearch.Location = new System.Drawing.Point(1076, 11);
            this.btnCancelSearch.Name = "btnCancelSearch";
            this.btnCancelSearch.Size = new System.Drawing.Size(114, 27);
            this.btnCancelSearch.TabIndex = 6;
            this.btnCancelSearch.Text = "Отмена";
            this.btnCancelSearch.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(951, 11);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(114, 27);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Найти";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // chkSearchSubdirs
            // 
            this.chkSearchSubdirs.AutoSize = true;
            this.chkSearchSubdirs.Checked = true;
            this.chkSearchSubdirs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSearchSubdirs.Location = new System.Drawing.Point(754, 15);
            this.chkSearchSubdirs.Name = "chkSearchSubdirs";
            this.chkSearchSubdirs.Size = new System.Drawing.Size(191, 20);
            this.chkSearchSubdirs.TabIndex = 4;
            this.chkSearchSubdirs.Text = "Включая поддиректории";
            this.chkSearchSubdirs.UseVisualStyleBackColor = true;
            // 
            // chkCaseSensitive
            // 
            this.chkCaseSensitive.AutoSize = true;
            this.chkCaseSensitive.Location = new System.Drawing.Point(571, 15);
            this.chkCaseSensitive.Name = "chkCaseSensitive";
            this.chkCaseSensitive.Size = new System.Drawing.Size(155, 20);
            this.chkCaseSensitive.TabIndex = 3;
            this.chkCaseSensitive.Text = "Учитывать регистр";
            this.chkCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // txtExtension
            // 
            this.txtExtension.Location = new System.Drawing.Point(446, 11);
            this.txtExtension.Name = "txtExtension";
            this.txtExtension.Size = new System.Drawing.Size(114, 22);
            this.txtExtension.TabIndex = 2;
            // 
            // lblExtension
            // 
            this.lblExtension.AutoSize = true;
            this.lblExtension.Location = new System.Drawing.Point(354, 15);
            this.lblExtension.Name = "lblExtension";
            this.lblExtension.Size = new System.Drawing.Size(91, 16);
            this.lblExtension.TabIndex = 1;
            this.lblExtension.Text = "Расширение:";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(137, 11);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(205, 22);
            this.txtFileName.TabIndex = 0;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(11, 15);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(125, 16);
            this.lblFileName.TabIndex = 0;
            this.lblFileName.Text = "Имя файла/маска:";
            // 
            // btnSearchPanel
            // 
            this.btnSearchPanel.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearchPanel.Location = new System.Drawing.Point(0, 197);
            this.btnSearchPanel.Name = "btnSearchPanel";
            this.btnSearchPanel.Size = new System.Drawing.Size(29, 27);
            this.btnSearchPanel.TabIndex = 5;
            this.btnSearchPanel.Text = "🔍";
            this.btnSearchPanel.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(0, 160);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(29, 27);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "↻";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnProperties
            // 
            this.btnProperties.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnProperties.ForeColor = System.Drawing.Color.Blue;
            this.btnProperties.Location = new System.Drawing.Point(0, 123);
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(29, 27);
            this.btnProperties.TabIndex = 3;
            this.btnProperties.Text = "i";
            this.btnProperties.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.Red;
            this.btnDelete.Location = new System.Drawing.Point(0, 85);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(29, 27);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "X";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnMoveToRight
            // 
            this.btnMoveToRight.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnMoveToRight.Location = new System.Drawing.Point(0, 48);
            this.btnMoveToRight.Name = "btnMoveToRight";
            this.btnMoveToRight.Size = new System.Drawing.Size(29, 27);
            this.btnMoveToRight.TabIndex = 1;
            this.btnMoveToRight.Text = "⇒";
            this.btnMoveToRight.UseVisualStyleBackColor = true;
            // 
            // btnCopyToRight
            // 
            this.btnCopyToRight.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnCopyToRight.Location = new System.Drawing.Point(0, 11);
            this.btnCopyToRight.Name = "btnCopyToRight";
            this.btnCopyToRight.Size = new System.Drawing.Size(29, 27);
            this.btnCopyToRight.TabIndex = 0;
            this.btnCopyToRight.Text = "→";
            this.btnCopyToRight.UseVisualStyleBackColor = true;
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblItemCount,
            this.lblSelectedSize});
            this.statusStrip.Location = new System.Drawing.Point(0, 699);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(1353, 26);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(48, 20);
            this.lblStatus.Text = "Готов";
            // 
            // lblItemCount
            // 
            this.lblItemCount.Name = "lblItemCount";
            this.lblItemCount.Size = new System.Drawing.Size(100, 20);
            this.lblItemCount.Text = "Элементов: 0";
            // 
            // lblSelectedSize
            // 
            this.lblSelectedSize.Name = "lblSelectedSize";
            this.lblSelectedSize.Size = new System.Drawing.Size(88, 20);
            this.lblSelectedSize.Text = "Выбрано: 0";
            // 
            // largeImageList
            // 
            this.largeImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.largeImageList.ImageSize = new System.Drawing.Size(32, 32);
            this.largeImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1353, 28);
            this.menuStrip.TabIndex = 5;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileNewFolder,
            this.toolStripSeparator1,
            this.menuFileExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // menuFileNewFolder
            // 
            this.menuFileNewFolder.Name = "menuFileNewFolder";
            this.menuFileNewFolder.Size = new System.Drawing.Size(181, 26);
            this.menuFileNewFolder.Text = "Новая папка";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // menuFileExit
            // 
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.Size = new System.Drawing.Size(181, 26);
            this.menuFileExit.Text = "Выход";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEditCopy,
            this.menuEditMove,
            this.menuEditDelete,
            this.menuEditRename,
            this.toolStripSeparator2,
            this.menuEditSelectAll,
            this.menuEditDeselectAll});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.editToolStripMenuItem.Text = "Правка";
            // 
            // menuEditCopy
            // 
            this.menuEditCopy.Name = "menuEditCopy";
            this.menuEditCopy.Size = new System.Drawing.Size(228, 26);
            this.menuEditCopy.Text = "Копировать";
            // 
            // menuEditMove
            // 
            this.menuEditMove.Name = "menuEditMove";
            this.menuEditMove.Size = new System.Drawing.Size(228, 26);
            this.menuEditMove.Text = "Переместить";
            // 
            // menuEditDelete
            // 
            this.menuEditDelete.Name = "menuEditDelete";
            this.menuEditDelete.Size = new System.Drawing.Size(228, 26);
            this.menuEditDelete.Text = "Удалить";
            // 
            // menuEditRename
            // 
            this.menuEditRename.Name = "menuEditRename";
            this.menuEditRename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.menuEditRename.Size = new System.Drawing.Size(228, 26);
            this.menuEditRename.Text = "Переименовать";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(225, 6);
            // 
            // menuEditSelectAll
            // 
            this.menuEditSelectAll.Name = "menuEditSelectAll";
            this.menuEditSelectAll.Size = new System.Drawing.Size(228, 26);
            this.menuEditSelectAll.Text = "Выбрать все";
            // 
            // menuEditDeselectAll
            // 
            this.menuEditDeselectAll.Name = "menuEditDeselectAll";
            this.menuEditDeselectAll.Size = new System.Drawing.Size(228, 26);
            this.menuEditDeselectAll.Text = "Снять выделение";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuViewLargeIcons,
            this.menuViewSmallIcons,
            this.menuViewList,
            this.menuViewDetails,
            this.toolStripSeparator3,
            this.menuViewToggleTree,
            this.menuViewToggleSearch});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.viewToolStripMenuItem.Text = "Вид";
            // 
            // menuViewLargeIcons
            // 
            this.menuViewLargeIcons.Name = "menuViewLargeIcons";
            this.menuViewLargeIcons.Size = new System.Drawing.Size(264, 26);
            this.menuViewLargeIcons.Text = "Крупные значки";
            // 
            // menuViewSmallIcons
            // 
            this.menuViewSmallIcons.Name = "menuViewSmallIcons";
            this.menuViewSmallIcons.Size = new System.Drawing.Size(264, 26);
            this.menuViewSmallIcons.Text = "Мелкие значки";
            // 
            // menuViewList
            // 
            this.menuViewList.Name = "menuViewList";
            this.menuViewList.Size = new System.Drawing.Size(264, 26);
            this.menuViewList.Text = "Список";
            // 
            // menuViewDetails
            // 
            this.menuViewDetails.Name = "menuViewDetails";
            this.menuViewDetails.Size = new System.Drawing.Size(264, 26);
            this.menuViewDetails.Text = "Таблица";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(261, 6);
            // 
            // menuViewToggleTree
            // 
            this.menuViewToggleTree.Name = "menuViewToggleTree";
            this.menuViewToggleTree.Size = new System.Drawing.Size(264, 26);
            this.menuViewToggleTree.Text = "Показать/скрыть дерево";
            // 
            // menuViewToggleSearch
            // 
            this.menuViewToggleSearch.Name = "menuViewToggleSearch";
            this.menuViewToggleSearch.Size = new System.Drawing.Size(264, 26);
            this.menuViewToggleSearch.Text = "Показать/скрыть поиск";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolsSearch,
            this.menuToolsProperties,
            this.toolStripSeparator4,
            this.menuToolsRefresh});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(117, 24);
            this.toolsToolStripMenuItem.Text = "Инструменты";
            // 
            // menuToolsSearch
            // 
            this.menuToolsSearch.Name = "menuToolsSearch";
            this.menuToolsSearch.Size = new System.Drawing.Size(191, 26);
            this.menuToolsSearch.Text = "Поиск файлов";
            // 
            // menuToolsProperties
            // 
            this.menuToolsProperties.Name = "menuToolsProperties";
            this.menuToolsProperties.Size = new System.Drawing.Size(191, 26);
            this.menuToolsProperties.Text = "Свойства";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(188, 6);
            // 
            // menuToolsRefresh
            // 
            this.menuToolsRefresh.Name = "menuToolsRefresh";
            this.menuToolsRefresh.Size = new System.Drawing.Size(191, 26);
            this.menuToolsRefresh.Text = "Обновить";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHelpAbout,
            this.toolStripSeparator5,
            this.menuHelpDevelopment});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.helpToolStripMenuItem.Text = "Помощь";
            // 
            // menuHelpAbout
            // 
            this.menuHelpAbout.Name = "menuHelpAbout";
            this.menuHelpAbout.Size = new System.Drawing.Size(187, 26);
            this.menuHelpAbout.Text = "О программе";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(184, 6);
            // 
            // menuHelpDevelopment
            // 
            this.menuHelpDevelopment.Name = "menuHelpDevelopment";
            this.menuHelpDevelopment.Size = new System.Drawing.Size(187, 26);
            this.menuHelpDevelopment.Text = "В разработке";
            // 
            // controlPanel
            // 
            this.controlPanel.Controls.Add(this.btnCopyToRight);
            this.controlPanel.Controls.Add(this.btnMoveToRight);
            this.controlPanel.Controls.Add(this.btnDelete);
            this.controlPanel.Controls.Add(this.btnProperties);
            this.controlPanel.Controls.Add(this.btnRefresh);
            this.controlPanel.Controls.Add(this.btnSearchPanel);
            this.controlPanel.Location = new System.Drawing.Point(669, 213);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(34, 277);
            this.controlPanel.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1353, 725);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(912, 637);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Файловый менеджер";
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.controlPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panelLeft;
        private ComboBox cboLeftDrive;
        private TextBox txtLeftPath;
        private TreeView treeViewDirectories;
        private ListView listViewLeft;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ContextMenuStrip contextMenuLeft;
        private Panel panelRight;
        private ComboBox cboRightDrive;
        private TextBox txtRightPath;
        private ListView listViewRight;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private ColumnHeader columnHeader10;
        private ContextMenuStrip contextMenuRight;
        private Panel panelSearch;
        private Label lblSearchStatus;
        private ProgressBar progressBarSearch;
        private ListView listViewSearchResults;
        private ColumnHeader columnHeader11;
        private ColumnHeader columnHeader12;
        private ColumnHeader columnHeader13;
        private ColumnHeader columnHeader14;
        private ColumnHeader columnHeader15;
        private Button btnCancelSearch;
        private Button btnSearch;
        private CheckBox chkSearchSubdirs;
        private CheckBox chkCaseSensitive;
        private TextBox txtExtension;
        private Label lblExtension;
        private TextBox txtFileName;
        private Label lblFileName;
        private Button btnSearchPanel;
        private Button btnRefresh;
        private Button btnProperties;
        private Button btnDelete;
        private Button btnMoveToRight;
        private Button btnCopyToRight;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblItemCount;
        private ToolStripStatusLabel lblSelectedSize;
        private ImageList smallImageList;
        private ImageList largeImageList;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem menuFileNewFolder;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem menuFileExit;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem menuEditCopy;
        private ToolStripMenuItem menuEditMove;
        private ToolStripMenuItem menuEditDelete;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem menuEditSelectAll;
        private ToolStripMenuItem menuEditDeselectAll;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem menuViewLargeIcons;
        private ToolStripMenuItem menuViewSmallIcons;
        private ToolStripMenuItem menuViewList;
        private ToolStripMenuItem menuViewDetails;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem menuViewToggleTree;
        private ToolStripMenuItem menuViewToggleSearch;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem menuToolsSearch;
        private ToolStripMenuItem menuToolsProperties;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem menuToolsRefresh;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem menuHelpAbout;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem menuHelpDevelopment;
        private Panel controlPanel;
        private System.Windows.Forms.ToolStripMenuItem menuEditRename;
    }
}