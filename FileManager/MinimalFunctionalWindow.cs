using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace FileManager
{
    public partial class MinimalFunctionalWindow : Form
    {
        private readonly Process mainWindowProcess = Process.GetCurrentProcess();
        private readonly MainWindow mainWindow;

        public MinimalFunctionalWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private bool saveAllStartedProcesses()
        {
            if (allStartedProcessesTextBox.TextLength != 0)
            {
                if (allStartedProcessesTextBox.Text.Contains("\\") || allStartedProcessesTextBox.Text.Contains("/") ||
                    allStartedProcessesTextBox.Text.Contains(":") || allStartedProcessesTextBox.Text.Contains("*") ||
                    allStartedProcessesTextBox.Text.Contains("?") || allStartedProcessesTextBox.Text.Contains("\"") ||
                    allStartedProcessesTextBox.Text.Contains("<") || allStartedProcessesTextBox.Text.Contains(">") ||
                    allStartedProcessesTextBox.Text.Contains("|"))
                {
                    MessageBox.Show("Имя файла не должно содержать следующих знаков: \\ / : * \" < > |.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Cursor = Cursors.WaitCursor;
                    saveAllStartedProcessesButton.Enabled = false;

                    Process[] processes = Process.GetProcesses();

                    StringBuilder buffer = new StringBuilder();

                    foreach (Process process in processes)
                    {
                        try
                        {
                            if (process.StartTime > mainWindowProcess.StartTime && !process.ProcessName.Equals("MainFunctional"))
                            {
                                buffer.Append(string.Format("{0} {1}\n", process.ProcessName, process.StartTime.ToString()));
                            }
                        }
                        catch { }
                    }

                    mainWindow.SaveProcesses(allStartedProcessesTextBox.Text, buffer.ToString());

                    saveAllStartedProcessesButton.Enabled = true;
                    Cursor = Cursors.Arrow;
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Введите имя файла.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return false;
        }

        private void saveAllStartedProcessesButton_Click(object sender, EventArgs e)
        {
            saveAllStartedProcesses();
        }

        private void allStartedProcessesTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                saveAllStartedProcesses();
            }
        }

        private void MinimalFunctionalWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
    }
}
