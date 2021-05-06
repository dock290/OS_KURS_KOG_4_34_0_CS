namespace FileManager
{
    partial class MinimalFunctionalWindow
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
            this.allStartedProcessesFileNameLabel = new System.Windows.Forms.Label();
            this.allStartedProcessesTextBox = new System.Windows.Forms.TextBox();
            this.saveAllStartedProcessesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // allStartedProcessesFileNameLabel
            // 
            this.allStartedProcessesFileNameLabel.AutoSize = true;
            this.allStartedProcessesFileNameLabel.Location = new System.Drawing.Point(12, 9);
            this.allStartedProcessesFileNameLabel.Name = "allStartedProcessesFileNameLabel";
            this.allStartedProcessesFileNameLabel.Size = new System.Drawing.Size(67, 13);
            this.allStartedProcessesFileNameLabel.TabIndex = 5;
            this.allStartedProcessesFileNameLabel.Text = "Имя файла:";
            // 
            // allStartedProcessesTextBox
            // 
            this.allStartedProcessesTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.allStartedProcessesTextBox.Location = new System.Drawing.Point(12, 25);
            this.allStartedProcessesTextBox.Name = "allStartedProcessesTextBox";
            this.allStartedProcessesTextBox.Size = new System.Drawing.Size(229, 23);
            this.allStartedProcessesTextBox.TabIndex = 4;
            this.allStartedProcessesTextBox.WordWrap = false;
            this.allStartedProcessesTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.allStartedProcessesTextBox_KeyPress);
            // 
            // saveAllStartedProcessesButton
            // 
            this.saveAllStartedProcessesButton.Location = new System.Drawing.Point(247, 25);
            this.saveAllStartedProcessesButton.Name = "saveAllStartedProcessesButton";
            this.saveAllStartedProcessesButton.Size = new System.Drawing.Size(75, 23);
            this.saveAllStartedProcessesButton.TabIndex = 3;
            this.saveAllStartedProcessesButton.Text = "Сохранить";
            this.saveAllStartedProcessesButton.UseVisualStyleBackColor = true;
            this.saveAllStartedProcessesButton.Click += new System.EventHandler(this.saveAllStartedProcessesButton_Click);
            // 
            // MinimalFunctionalWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 61);
            this.Controls.Add(this.allStartedProcessesFileNameLabel);
            this.Controls.Add(this.allStartedProcessesTextBox);
            this.Controls.Add(this.saveAllStartedProcessesButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MinimalFunctionalWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Протокол запущенных процессов";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MinimalFunctionalWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label allStartedProcessesFileNameLabel;
        private System.Windows.Forms.TextBox allStartedProcessesTextBox;
        private System.Windows.Forms.Button saveAllStartedProcessesButton;
    }
}