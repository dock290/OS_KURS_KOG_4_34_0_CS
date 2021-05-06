namespace FileManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.backButton = new System.Windows.Forms.Button();
            this.forwardButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.itemsListView = new System.Windows.Forms.ListView();
            this.nameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.modificationDateHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.extensionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sizeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemsImageList = new System.Windows.Forms.ImageList(this.components);
            this.itemsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openItemButton = new System.Windows.Forms.ToolStripMenuItem();
            this.cutItemsButton = new System.Windows.Forms.ToolStripMenuItem();
            this.copyItemsButton = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteItemsButton = new System.Windows.Forms.ToolStripMenuItem();
            this.renameItemButton = new System.Windows.Forms.ToolStripMenuItem();
            this.itemsListViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createItemButton = new System.Windows.Forms.ToolStripMenuItem();
            this.createItemMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createDirectoryButton = new System.Windows.Forms.ToolStripMenuItem();
            this.createFileButton = new System.Windows.Forms.ToolStripMenuItem();
            this.insertItemsButton = new System.Windows.Forms.ToolStripMenuItem();
            this.updateItemsButton = new System.Windows.Forms.ToolStripMenuItem();
            this.mainWindowMenu = new System.Windows.Forms.MenuStrip();
            this.functionalMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.utilitiesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutProgramButton = new System.Windows.Forms.ToolStripMenuItem();
            this.functionalMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.utilitiesMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.minimalFunctionalButton = new System.Windows.Forms.ToolStripMenuItem();
            this.mainFunctionalButton = new System.Windows.Forms.ToolStripMenuItem();
            this.taskManagerButton = new System.Windows.Forms.ToolStripMenuItem();
            this.commandLineButton = new System.Windows.Forms.ToolStripMenuItem();
            this.itemsContextMenu.SuspendLayout();
            this.itemsListViewContextMenu.SuspendLayout();
            this.createItemMenu.SuspendLayout();
            this.mainWindowMenu.SuspendLayout();
            this.functionalMenuStrip.SuspendLayout();
            this.utilitiesMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Enabled = false;
            this.backButton.Image = ((System.Drawing.Image)(resources.GetObject("backButton.Image")));
            this.backButton.Location = new System.Drawing.Point(5, 24);
            this.backButton.Margin = new System.Windows.Forms.Padding(2);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(26, 26);
            this.backButton.TabIndex = 1;
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // forwardButton
            // 
            this.forwardButton.Enabled = false;
            this.forwardButton.Image = ((System.Drawing.Image)(resources.GetObject("forwardButton.Image")));
            this.forwardButton.Location = new System.Drawing.Point(32, 24);
            this.forwardButton.Margin = new System.Windows.Forms.Padding(2);
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(26, 26);
            this.forwardButton.TabIndex = 2;
            this.forwardButton.UseVisualStyleBackColor = true;
            this.forwardButton.Click += new System.EventHandler(this.forwardButton_Click);
            // 
            // upButton
            // 
            this.upButton.Image = ((System.Drawing.Image)(resources.GetObject("upButton.Image")));
            this.upButton.Location = new System.Drawing.Point(62, 24);
            this.upButton.Margin = new System.Windows.Forms.Padding(2);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(26, 26);
            this.upButton.TabIndex = 3;
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.upButton_MouseClick);
            // 
            // pathTextBox
            // 
            this.pathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pathTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pathTextBox.Location = new System.Drawing.Point(93, 26);
            this.pathTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 2, 2);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(458, 24);
            this.pathTextBox.TabIndex = 4;
            this.pathTextBox.WordWrap = false;
            this.pathTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PathTextBox_KeyPress);
            // 
            // itemsListView
            // 
            this.itemsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemsListView.AutoArrange = false;
            this.itemsListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.itemsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameHeader,
            this.modificationDateHeader,
            this.extensionHeader,
            this.sizeHeader});
            this.itemsListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.itemsListView.FullRowSelect = true;
            this.itemsListView.HideSelection = false;
            this.itemsListView.LabelEdit = true;
            this.itemsListView.LabelWrap = false;
            this.itemsListView.Location = new System.Drawing.Point(5, 54);
            this.itemsListView.Margin = new System.Windows.Forms.Padding(2);
            this.itemsListView.Name = "itemsListView";
            this.itemsListView.ShowGroups = false;
            this.itemsListView.Size = new System.Drawing.Size(546, 295);
            this.itemsListView.SmallImageList = this.itemsImageList;
            this.itemsListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.itemsListView.TabIndex = 6;
            this.itemsListView.UseCompatibleStateImageBehavior = false;
            this.itemsListView.View = System.Windows.Forms.View.Details;
            this.itemsListView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.itemsListView_AfterLabelEdit);
            this.itemsListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.itemsListView_ColumnClick);
            this.itemsListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.itemsListView_ColumnWidthChanging);
            this.itemsListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.itemsListView_KeyDown);
            this.itemsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.itemsListView_MouseDoubleClick);
            this.itemsListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.itemsListView_MouseDown);
            // 
            // nameHeader
            // 
            this.nameHeader.Text = "Имя";
            this.nameHeader.Width = 220;
            // 
            // modificationDateHeader
            // 
            this.modificationDateHeader.Text = "Дата создания";
            this.modificationDateHeader.Width = 115;
            // 
            // extensionHeader
            // 
            this.extensionHeader.Text = "Расширение";
            this.extensionHeader.Width = 105;
            // 
            // sizeHeader
            // 
            this.sizeHeader.Text = "Размер";
            this.sizeHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.sizeHeader.Width = 65;
            // 
            // itemsImageList
            // 
            this.itemsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("itemsImageList.ImageStream")));
            this.itemsImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.itemsImageList.Images.SetKeyName(0, "DirectoryImage.png");
            this.itemsImageList.Images.SetKeyName(1, "TextFileImage.png");
            this.itemsImageList.Images.SetKeyName(2, "ImageFileImage.png");
            this.itemsImageList.Images.SetKeyName(3, "AudioFileImage.png");
            this.itemsImageList.Images.SetKeyName(4, "VideoFileImage.png");
            this.itemsImageList.Images.SetKeyName(5, "ExeFileImage.png");
            this.itemsImageList.Images.SetKeyName(6, "OtherFileImage.png");
            // 
            // itemsContextMenu
            // 
            this.itemsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openItemButton,
            this.cutItemsButton,
            this.copyItemsButton,
            this.deleteItemsButton,
            this.renameItemButton});
            this.itemsContextMenu.Name = "itemsContextMenu";
            this.itemsContextMenu.ShowImageMargin = false;
            this.itemsContextMenu.Size = new System.Drawing.Size(137, 114);
            // 
            // openItemButton
            // 
            this.openItemButton.Name = "openItemButton";
            this.openItemButton.Size = new System.Drawing.Size(136, 22);
            this.openItemButton.Text = "Открыть";
            this.openItemButton.Click += new System.EventHandler(this.openItem_Click);
            // 
            // cutItemsButton
            // 
            this.cutItemsButton.Name = "cutItemsButton";
            this.cutItemsButton.Size = new System.Drawing.Size(136, 22);
            this.cutItemsButton.Text = "Вырезать";
            this.cutItemsButton.Click += new System.EventHandler(this.cutItemsButton_Click);
            // 
            // copyItemsButton
            // 
            this.copyItemsButton.Name = "copyItemsButton";
            this.copyItemsButton.Size = new System.Drawing.Size(136, 22);
            this.copyItemsButton.Text = "Копировать";
            this.copyItemsButton.Click += new System.EventHandler(this.copyItemsButton_Click);
            // 
            // deleteItemsButton
            // 
            this.deleteItemsButton.Name = "deleteItemsButton";
            this.deleteItemsButton.Size = new System.Drawing.Size(136, 22);
            this.deleteItemsButton.Text = "Удалить";
            this.deleteItemsButton.Click += new System.EventHandler(this.deleteItemsButton_Click);
            // 
            // renameItemButton
            // 
            this.renameItemButton.Name = "renameItemButton";
            this.renameItemButton.Size = new System.Drawing.Size(136, 22);
            this.renameItemButton.Text = "Переименовать";
            this.renameItemButton.Click += new System.EventHandler(this.renameItemButton_Click);
            // 
            // itemsListViewContextMenu
            // 
            this.itemsListViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createItemButton,
            this.insertItemsButton,
            this.updateItemsButton});
            this.itemsListViewContextMenu.Name = "itemsViewListContextMenu";
            this.itemsListViewContextMenu.ShowImageMargin = false;
            this.itemsListViewContextMenu.Size = new System.Drawing.Size(104, 70);
            // 
            // createItemButton
            // 
            this.createItemButton.DropDown = this.createItemMenu;
            this.createItemButton.Name = "createItemButton";
            this.createItemButton.Size = new System.Drawing.Size(103, 22);
            this.createItemButton.Text = "Создать";
            // 
            // createItemMenu
            // 
            this.createItemMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createDirectoryButton,
            this.createFileButton});
            this.createItemMenu.Name = "createItemMenu";
            this.createItemMenu.ShowImageMargin = false;
            this.createItemMenu.Size = new System.Drawing.Size(84, 48);
            // 
            // createDirectoryButton
            // 
            this.createDirectoryButton.Name = "createDirectoryButton";
            this.createDirectoryButton.Size = new System.Drawing.Size(83, 22);
            this.createDirectoryButton.Text = "Папку";
            this.createDirectoryButton.Click += new System.EventHandler(this.createDirectoryButton_Click);
            // 
            // createFileButton
            // 
            this.createFileButton.Name = "createFileButton";
            this.createFileButton.Size = new System.Drawing.Size(83, 22);
            this.createFileButton.Text = "Файл";
            this.createFileButton.Click += new System.EventHandler(this.createFileButton_Click);
            // 
            // insertItemsButton
            // 
            this.insertItemsButton.Enabled = false;
            this.insertItemsButton.Name = "insertItemsButton";
            this.insertItemsButton.Size = new System.Drawing.Size(103, 22);
            this.insertItemsButton.Text = "Вставить";
            this.insertItemsButton.Click += new System.EventHandler(this.insertItemsButton_Click);
            // 
            // updateItemsButton
            // 
            this.updateItemsButton.Name = "updateItemsButton";
            this.updateItemsButton.Size = new System.Drawing.Size(103, 22);
            this.updateItemsButton.Text = "Обновить";
            this.updateItemsButton.Click += new System.EventHandler(this.updateItemsButton_Click);
            // 
            // mainWindowMenu
            // 
            this.mainWindowMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.functionalMenu,
            this.utilitiesMenu,
            this.aboutProgramButton});
            this.mainWindowMenu.Location = new System.Drawing.Point(0, 0);
            this.mainWindowMenu.Name = "mainWindowMenu";
            this.mainWindowMenu.Size = new System.Drawing.Size(554, 24);
            this.mainWindowMenu.TabIndex = 0;
            this.mainWindowMenu.TabStop = true;
            this.mainWindowMenu.Text = "menuStrip1";
            // 
            // functionalMenu
            // 
            this.functionalMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.functionalMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.functionalMenu.DropDown = this.functionalMenuStrip;
            this.functionalMenu.Name = "functionalMenu";
            this.functionalMenu.Size = new System.Drawing.Size(88, 20);
            this.functionalMenu.Text = "Функционал";
            // 
            // utilitiesMenu
            // 
            this.utilitiesMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.utilitiesMenu.DropDown = this.utilitiesMenuStrip;
            this.utilitiesMenu.Name = "utilitiesMenu";
            this.utilitiesMenu.Size = new System.Drawing.Size(131, 20);
            this.utilitiesMenu.Text = "Системные утилиты";
            // 
            // aboutProgramButton
            // 
            this.aboutProgramButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutProgramButton.Name = "aboutProgramButton";
            this.aboutProgramButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.aboutProgramButton.Size = new System.Drawing.Size(94, 20);
            this.aboutProgramButton.Text = "О программе";
            this.aboutProgramButton.Click += new System.EventHandler(this.aboutProgramButton_Click);
            // 
            // functionalMenuStrip
            // 
            this.functionalMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minimalFunctionalButton,
            this.mainFunctionalButton});
            this.functionalMenuStrip.Name = "functionalMenuStrip";
            this.functionalMenuStrip.ShowImageMargin = false;
            this.functionalMenuStrip.Size = new System.Drawing.Size(205, 48);
            // 
            // utilitiesMenuStrip
            // 
            this.utilitiesMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taskManagerButton,
            this.commandLineButton});
            this.utilitiesMenuStrip.Name = "utilitiesMenuStrip";
            this.utilitiesMenuStrip.OwnerItem = this.utilitiesMenu;
            this.utilitiesMenuStrip.ShowImageMargin = false;
            this.utilitiesMenuStrip.Size = new System.Drawing.Size(156, 70);
            // 
            // minimalFunctionalButton
            // 
            this.minimalFunctionalButton.Name = "minimalFunctionalButton";
            this.minimalFunctionalButton.Size = new System.Drawing.Size(204, 22);
            this.minimalFunctionalButton.Text = "Минимальный функционал";
            this.minimalFunctionalButton.Click += new System.EventHandler(this.minimalFunctionalButton_Click);
            // 
            // mainFunctionalButton
            // 
            this.mainFunctionalButton.Name = "mainFunctionalButton";
            this.mainFunctionalButton.Size = new System.Drawing.Size(204, 22);
            this.mainFunctionalButton.Text = "Основной функционал";
            this.mainFunctionalButton.Click += new System.EventHandler(this.mainFunctionalButton_Click);
            // 
            // taskManagerButton
            // 
            this.taskManagerButton.Name = "taskManagerButton";
            this.taskManagerButton.Size = new System.Drawing.Size(155, 22);
            this.taskManagerButton.Text = "Диспетчер задач";
            this.taskManagerButton.Click += new System.EventHandler(this.taskManagerButton_Click);
            // 
            // commandLineButton
            // 
            this.commandLineButton.Name = "commandLineButton";
            this.commandLineButton.Size = new System.Drawing.Size(155, 22);
            this.commandLineButton.Text = "Командная строка";
            this.commandLineButton.Click += new System.EventHandler(this.commandLineButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 361);
            this.Controls.Add(this.mainWindowMenu);
            this.Controls.Add(this.itemsListView);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.forwardButton);
            this.Controls.Add(this.backButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainWindowMenu;
            this.MinimumSize = new System.Drawing.Size(570, 400);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Файловый менеджер";
            this.Activated += new System.EventHandler(this.MainWindow_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mainWindow_FormClosed);
            this.SizeChanged += new System.EventHandler(this.mainWindow_SizeChanged);
            this.itemsContextMenu.ResumeLayout(false);
            this.itemsListViewContextMenu.ResumeLayout(false);
            this.createItemMenu.ResumeLayout(false);
            this.mainWindowMenu.ResumeLayout(false);
            this.mainWindowMenu.PerformLayout();
            this.functionalMenuStrip.ResumeLayout(false);
            this.utilitiesMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button forwardButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.ListView itemsListView;
        private System.Windows.Forms.ColumnHeader nameHeader;
        private System.Windows.Forms.ColumnHeader modificationDateHeader;
        private System.Windows.Forms.ColumnHeader extensionHeader;
        private System.Windows.Forms.ColumnHeader sizeHeader;
        private System.Windows.Forms.ImageList itemsImageList;
        private System.Windows.Forms.ContextMenuStrip itemsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem openItemButton;
        private System.Windows.Forms.ContextMenuStrip itemsListViewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem cutItemsButton;
        private System.Windows.Forms.ToolStripMenuItem copyItemsButton;
        private System.Windows.Forms.ToolStripMenuItem deleteItemsButton;
        private System.Windows.Forms.ToolStripMenuItem renameItemButton;
        private System.Windows.Forms.ToolStripMenuItem insertItemsButton;
        private System.Windows.Forms.MenuStrip mainWindowMenu;
        private System.Windows.Forms.ToolStripMenuItem aboutProgramButton;
        private System.Windows.Forms.ToolStripMenuItem updateItemsButton;
        private System.Windows.Forms.ToolStripMenuItem createItemButton;
        private System.Windows.Forms.ContextMenuStrip createItemMenu;
        private System.Windows.Forms.ToolStripMenuItem createDirectoryButton;
        private System.Windows.Forms.ToolStripMenuItem createFileButton;
        private System.Windows.Forms.ToolStripMenuItem functionalMenu;
        private System.Windows.Forms.ToolStripMenuItem utilitiesMenu;
        private System.Windows.Forms.ContextMenuStrip functionalMenuStrip;
        private System.Windows.Forms.ContextMenuStrip utilitiesMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem minimalFunctionalButton;
        private System.Windows.Forms.ToolStripMenuItem mainFunctionalButton;
        private System.Windows.Forms.ToolStripMenuItem taskManagerButton;
        private System.Windows.Forms.ToolStripMenuItem commandLineButton;
    }
}

