namespace MainFunctional
{
    partial class MainFunctionalWindow
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
            this.processNameLabel2 = new System.Windows.Forms.Label();
            this.processNameLabel1 = new System.Windows.Forms.Label();
            this.logListView = new System.Windows.Forms.ListView();
            this.dateHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pathHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.oldNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.newNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.saveLogButton = new System.Windows.Forms.Button();
            this.logLabel = new System.Windows.Forms.Label();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // processNameLabel2
            // 
            this.processNameLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.processNameLabel2.AutoEllipsis = true;
            this.processNameLabel2.AutoSize = true;
            this.processNameLabel2.Location = new System.Drawing.Point(94, 9);
            this.processNameLabel2.Name = "processNameLabel2";
            this.processNameLabel2.Size = new System.Drawing.Size(89, 13);
            this.processNameLabel2.TabIndex = 1;
            this.processNameLabel2.Text = "%ProcessName%";
            // 
            // processNameLabel1
            // 
            this.processNameLabel1.AutoSize = true;
            this.processNameLabel1.Location = new System.Drawing.Point(12, 9);
            this.processNameLabel1.Name = "processNameLabel1";
            this.processNameLabel1.Size = new System.Drawing.Size(83, 13);
            this.processNameLabel1.TabIndex = 0;
            this.processNameLabel1.Text = "Имя процесса:";
            // 
            // logListView
            // 
            this.logListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.dateHeader,
            this.pathHeader,
            this.oldNameHeader,
            this.newNameHeader});
            this.logListView.FullRowSelect = true;
            this.logListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.logListView.HideSelection = false;
            this.logListView.Location = new System.Drawing.Point(13, 42);
            this.logListView.Name = "logListView";
            this.logListView.Size = new System.Drawing.Size(309, 212);
            this.logListView.TabIndex = 2;
            this.logListView.UseCompatibleStateImageBehavior = false;
            this.logListView.View = System.Windows.Forms.View.Details;
            // 
            // dateHeader
            // 
            this.dateHeader.Text = "Дата";
            this.dateHeader.Width = 115;
            // 
            // pathHeader
            // 
            this.pathHeader.Text = "Путь";
            this.pathHeader.Width = 65;
            // 
            // oldNameHeader
            // 
            this.oldNameHeader.Text = "Старое имя";
            this.oldNameHeader.Width = 55;
            // 
            // newNameHeader
            // 
            this.newNameHeader.Text = "Новое имя";
            this.newNameHeader.Width = 55;
            // 
            // saveLogButton
            // 
            this.saveLogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveLogButton.Location = new System.Drawing.Point(247, 13);
            this.saveLogButton.Name = "saveLogButton";
            this.saveLogButton.Size = new System.Drawing.Size(75, 23);
            this.saveLogButton.TabIndex = 3;
            this.saveLogButton.Text = "Сохранить";
            this.saveLogButton.UseVisualStyleBackColor = true;
            this.saveLogButton.Click += new System.EventHandler(this.saveLogButton_Click);
            // 
            // logLabel
            // 
            this.logLabel.AutoSize = true;
            this.logLabel.Location = new System.Drawing.Point(12, 26);
            this.logLabel.Name = "logLabel";
            this.logLabel.Size = new System.Drawing.Size(155, 13);
            this.logLabel.TabIndex = 4;
            this.logLabel.Text = "Переименованные каталоги:";
            // 
            // updateTimer
            // 
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // MainFunctionalWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 266);
            this.Controls.Add(this.logLabel);
            this.Controls.Add(this.saveLogButton);
            this.Controls.Add(this.logListView);
            this.Controls.Add(this.processNameLabel2);
            this.Controls.Add(this.processNameLabel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1900, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 305);
            this.Name = "MainFunctionalWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Основной функционал";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFunctionalWindow_FormClosing);
            this.SizeChanged += new System.EventHandler(this.MainFunctionalWindow_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label processNameLabel2;
        private System.Windows.Forms.Label processNameLabel1;
        private System.Windows.Forms.ListView logListView;
        private System.Windows.Forms.ColumnHeader dateHeader;
        private System.Windows.Forms.ColumnHeader newNameHeader;
        private System.Windows.Forms.Button saveLogButton;
        private System.Windows.Forms.Label logLabel;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.ColumnHeader oldNameHeader;
        private System.Windows.Forms.ColumnHeader pathHeader;
    }
}

