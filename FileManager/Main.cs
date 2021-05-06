using System;
using System.Windows.Forms;

namespace FileManager
{
    static class _Main
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string system = FileManagerFunctions.GetSystemDirectory().Replace(FileManagerFunctions.GetRootDirectory(), "").Remove(0, 1); ;
            if (system.Equals("System"))
            {
                Application.Run(new MainWindow());
            }
            else
            {
                MessageBox.Show("Запускаемые файлы должны быть расположены в папке System.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
