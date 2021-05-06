using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MainFunctional
{
    static class _Main
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process[] processes = Process.GetProcesses();

            bool start = false;

            foreach (Process process in processes)
            {
                try
                {
                    if (process.ProcessName.Equals("FileManager"))
                    {
                        start = true;
                        break;
                    }
                }
                catch { }
            }

            if (start)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainFunctionalWindow());
            }
            else
            {
                MessageBox.Show("Основной функционал должен быть запущен после файлового менеджера", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
