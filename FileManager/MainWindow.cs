using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using FMF = FileManagerFunctions;

namespace FileManager
{
    public partial class MainWindow : Form
    {
        private readonly MailslotServer mailslotServer = new MailslotServer("FM_Mailslot_ToMain");
        private readonly MailslotClient mailslotClient = new MailslotClient("FM_Mailslot_ToMF");

        private readonly Thread mailslotServerThread;
        private bool isWorking = false;

        private readonly MinimalFunctionalWindow minimalFunctionalWindow;
        private readonly Process mainFunctionalProcess;

        private readonly List<string> copyPasteBuffer = new List<string>();
        private bool isCut = false;

        private readonly List<string> pathsHistory = new List<string>();
        private int currentPathHistoryIndex = 0;

        private readonly int originalNameColumnSize;

        private readonly ListViewItemComparer listViewItemComparer = new ListViewItemComparer();

        private bool isUpdateRequired = false;
        private bool isCreatedDirectoryRenaming = false;

        public MainWindow()
        {
            InitializeComponent();

            isWorking = true;
            mailslotServerThread = new Thread(MailslotServerWork);
            mailslotServerThread.Priority = ThreadPriority.BelowNormal;
            mailslotServerThread.Start();

            originalNameColumnSize = itemsListView.Columns[0].Width;
            itemsListView.ListViewItemSorter = listViewItemComparer;
            itemsListView.Columns[listViewItemComparer.GetCurrentSortingColumn()].Text =
                string.Format("{0}{1}", itemsListView.Columns[listViewItemComparer.GetCurrentSortingColumn()].Text, " ˄");

            ActiveControl = pathTextBox;

            if (openItem(FMF.GetCurrentDirectory()))
            {
                pathsHistory.Add(FMF.GetCurrentDirectory());
            }

            mainFunctionalProcess = new Process();
            mainFunctionalProcess.StartInfo =
                new ProcessStartInfo(
                    Path.Combine(FMF.GetSystemDirectory(), "MainFunctional.exe"));
            mainFunctionalProcess.StartInfo.UseShellExecute = false;

            mainFunctionalProcess.Start();

            minimalFunctionalWindow = new MinimalFunctionalWindow(this);
            minimalFunctionalWindow.Visible = false;

            update();
        }

        private void MailslotServerWork()
        {
            string message;
            while (isWorking)
            {
                if ((message = mailslotServer.GetNextMessage()) != null)
                {
                    string[] buffer = message.Split('|');

                    if (buffer[0].Equals("SAVE"))
                    {
                        string folderPath = Path.Combine(FMF.GetRootDirectory(), "Логи");
                        string filePath = Path.Combine(folderPath, DateTime.Now.ToString().Replace(':', '.') + ".txt");

                        if (!Directory.Exists(folderPath))
                        {
                            if (File.Exists(folderPath))
                            {
                                try
                                {
                                    File.Delete(folderPath);
                                }
                                catch { }
                            }

                            Directory.CreateDirectory(folderPath);

                            if (FMF.GetCurrentDirectory().Equals(FMF.GetRootDirectory()))
                            {
                                isUpdateRequired = true;
                            }
                        }

                        if (FMF.GetCurrentDirectory().Equals(folderPath))
                        {
                            isUpdateRequired = true;
                        }

                        if (FMF.IsItemExists(filePath))
                        {
                            FMF.Delete(new string[] { filePath });
                        }

                        FMF.CreateFile(filePath);

                        StreamWriter streamWriter = new StreamWriter(filePath);

                        for (int i = 1; i < buffer.Length; ++i)
                        {
                            streamWriter.WriteLine(buffer[i]);
                        }

                        streamWriter.Close();
                    }
                    else if (buffer[0].Equals("STARTED"))
                    {
                        mailslotClient.SendMessage(string.Format("LOCATION|{0}|{1}",
                            Location.X + Width - 10, Location.Y + minimalFunctionalWindow.Height - 5));
                    }
                    //else if (buffer[0].Equals("QUITED"))
                    //{
                    //    mainFunctionalProcess.Kill();
                    //    isWorking = false;
                    //}
                }
            }
        }

        public void SaveProcesses(string fileName, string processes)
        {
            bool isUpdateRequired = false;

            string folderPath = Path.Combine(FMF.GetRootDirectory(), "Процессы");
            string filePath = Path.Combine(folderPath, fileName);

            if (!filePath.EndsWith(".txt") || !filePath.EndsWith(".txt"))
            {
                filePath = string.Format("{0}.txt", filePath);
            }

            if (!Directory.Exists(folderPath))
            {
                if (File.Exists(folderPath))
                {
                    try
                    {
                        File.Delete(folderPath);
                    }
                    catch { }
                }

                Directory.CreateDirectory(folderPath);

                if (FMF.GetCurrentDirectory().Equals(FMF.GetRootDirectory()))
                {
                    isUpdateRequired = true;
                }
            }

            if (FMF.GetCurrentDirectory().Equals(folderPath))
            {
                isUpdateRequired = true;
            }

            if (FMF.IsItemExists(filePath))
            {
                FMF.Delete(new string[] { filePath });
            }

            FMF.CreateFile(filePath);

            StreamWriter streamWriter = new StreamWriter(filePath);

            streamWriter.Write(processes);

            streamWriter.Close();

            if (isUpdateRequired)
            {
                update();
            }
        }

        private void update()
        {
            pathTextBox.Text = FMF.GetCurrentDirectory().Replace(FMF.GetRootDirectory(), "");

            if (pathTextBox.Text.Length > 0 && pathTextBox.Text[0] == '\\')
            {
                pathTextBox.Text = pathTextBox.Text.Substring(1);
            }

            pathTextBox.SelectionStart = pathTextBox.Text.Length;

            itemsListView.BeginUpdate();

            itemsListView.Items.Clear();

            string[][] items = FMF.GetCurrentDirectoryItems();

            ListViewItem[] listViewItem = new ListViewItem[items.Length];

            int index;
            for (int i = 0; i < items.Length; ++i)
            {
                index = items[i][0].LastIndexOf('\\') + 1;

                listViewItem[i] = new ListViewItem(items[i][0].Substring(index));

                listViewItem[i].SubItems.Add(items[i][1]);
                listViewItem[i].SubItems.Add(items[i][2]);
                listViewItem[i].SubItems.Add(items[i][3]);

                if (items[i][2].Equals("Папка с файлами"))
                {
                    listViewItem[i].ImageIndex = 0;
                }
                else if (items[i][2].Equals("TXT"))
                {
                    listViewItem[i].ImageIndex = 1;
                }
                else if (items[i][2].Equals("BMP") || items[i][2].Equals("PNG") ||
                    items[i][2].Equals("JPG") || items[i][2].Equals("JPEG") || items[i][2].Equals("GIF"))
                {
                    listViewItem[i].ImageIndex = 2;
                }
                else if (items[i][2].Equals("MP3") || items[i][2].Equals("WAV") || items[i][2].Equals("WMA"))
                {
                    listViewItem[i].ImageIndex = 3;
                }
                else if (items[i][2].Equals("MP4") || items[i][2].Equals("AVI") ||
                    items[i][2].Equals("WMV") || items[i][2].Equals("MOV") || items[i][2].Equals("WEBM"))
                {
                    listViewItem[i].ImageIndex = 4;
                }
                else if (items[i][2].Equals("EXE"))
                {
                    listViewItem[i].ImageIndex = 5;
                }
                else
                {
                    listViewItem[i].ImageIndex = 6;
                }
            }

            itemsListView.Items.AddRange(listViewItem);

            itemsListView.EndUpdate();
        }

        private bool openItem(string fullPath)
        {
            if (FMF.IsPathInsideRoot(fullPath))
            {
                if (File.Exists(fullPath))
                {
                    if (!FMF.IsPathNotInsideSystem(fullPath))
                    {
                        MessageBox.Show("Запрещено открывать элементы папки System.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    FMF.StartProcess(fullPath);
                    return true;
                }
                else if (FMF.SetCurrentDirectory(fullPath))
                {
                    upButton.Enabled = !FMF.GetCurrentDirectory().Equals(FMF.GetRootDirectory());

                    update();
                    return true;
                }
            }
            else
            {
                FMF.ShowWrongPathMessageBox(fullPath.Replace(FMF.GetRootDirectory() + '\\', ""));
            }

            return false;
        }

        private void pathHistoryAdd(string fullPath)
        {
            if (currentPathHistoryIndex != pathsHistory.Count - 1)
            {
                pathHistoryRemove();
            }

            if (!pathsHistory[currentPathHistoryIndex].Equals(FMF.GetCurrentDirectory()))
            {
                pathsHistory.Add(fullPath);
                currentPathHistoryIndex++;
            }

            backButton.Enabled = currentPathHistoryIndex != 0;
            forwardButton.Enabled = currentPathHistoryIndex != pathsHistory.Count - 1;
        }

        private void pathHistoryRemove()
        {
            pathsHistory.RemoveRange(currentPathHistoryIndex + 1, pathsHistory.Count - currentPathHistoryIndex - 1);

            backButton.Enabled = currentPathHistoryIndex != 0;
            forwardButton.Enabled = currentPathHistoryIndex != pathsHistory.Count - 1;
        }

        private void directoryGoUp()
        {
            if (!FMF.GetCurrentDirectory().Equals(FMF.GetRootDirectory()))
            {
                if (openItem(FMF.GetCurrentDirectory().Substring(0, FMF.GetCurrentDirectory().LastIndexOf('\\'))))
                {
                    pathHistoryAdd(FMF.GetCurrentDirectory());
                }
            }
        }

        private void pathHistoryGoBack()
        {
            if (currentPathHistoryIndex > 0)
            {
                currentPathHistoryIndex--;
                if (!openItem(pathsHistory[currentPathHistoryIndex]))
                {
                    currentPathHistoryIndex++;
                }
            }

            backButton.Enabled = currentPathHistoryIndex != 0;
            forwardButton.Enabled = currentPathHistoryIndex != pathsHistory.Count - 1;
        }

        private void pathHistoryGoForward()
        {
            if (currentPathHistoryIndex < pathsHistory.Count - 1)
            {
                currentPathHistoryIndex++;
                if (!openItem(pathsHistory[currentPathHistoryIndex]))
                {
                    currentPathHistoryIndex--;
                }
            }

            backButton.Enabled = currentPathHistoryIndex != 0;
            forwardButton.Enabled = currentPathHistoryIndex != pathsHistory.Count - 1;
        }

        private void cutItemsToBuffer()
        {
            copyPasteBuffer.Clear();

            copyPasteBuffer.Add(FMF.GetCurrentDirectory());
            for (int i = 0; i < itemsListView.SelectedItems.Count; ++i)
            {
                if (FMF.GetCurrentDirectory().Equals(FMF.GetRootDirectory()) && itemsListView.SelectedItems[i].SubItems[0].Text.ToLower().Equals("system") ||
                    (!FMF.IsPathNotInsideSystem(Path.Combine(FMF.GetCurrentDirectory(), itemsListView.SelectedItems[i].SubItems[0].Text))))
                {
                    MessageBox.Show("Запрещено вырезать папку System или её содержимое.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    copyPasteBuffer.Clear();
                    insertItemsButton.Enabled = false;
                    return;
                }

                copyPasteBuffer.Add(itemsListView.SelectedItems[i].SubItems[0].Text);
            }

            insertItemsButton.Enabled = isCut = copyPasteBuffer.Count != 1;
        }

        private void copyItemsToBuffer()
        {
            copyPasteBuffer.Clear();

            copyPasteBuffer.Add(FMF.GetCurrentDirectory());
            for (int i = 0; i < itemsListView.SelectedItems.Count; ++i)
            {
                if (FMF.GetCurrentDirectory().Equals(FMF.GetRootDirectory()) && itemsListView.SelectedItems[i].SubItems[0].Text.ToLower().Equals("system") ||
                    (!FMF.IsPathNotInsideSystem(Path.Combine(FMF.GetCurrentDirectory(), itemsListView.SelectedItems[i].SubItems[0].Text))))
                {
                    MessageBox.Show("Запрещено копировать папку System или её содержимое.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    copyPasteBuffer.Clear();
                    insertItemsButton.Enabled = false;
                    return;
                }

                copyPasteBuffer.Add(itemsListView.SelectedItems[i].SubItems[0].Text);
            }

            isCut = false;
            insertItemsButton.Enabled = copyPasteBuffer.Count != 1;
        }

        private void insertItemsFromBuffer()
        {
            if (copyPasteBuffer.Count != 0)
            {
                if (FMF.GetCurrentDirectory().ToLower().Equals(FMF.GetSystemDirectory().ToLower()) || !FMF.IsPathNotInsideSystem(FMF.GetCurrentDirectory()))
                {
                    MessageBox.Show("Запрещено изменять папку System или её содержимое.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    isCut = insertItemsButton.Enabled = false;
                    return;
                }

                if (isCut)
                {
                    FMF.Move(copyPasteBuffer.ToArray(), FMF.GetCurrentDirectory());

                    copyPasteBuffer.Clear();

                    isCut = insertItemsButton.Enabled = false;
                }
                else
                {
                    FMF.Copy(copyPasteBuffer.ToArray(), FMF.GetCurrentDirectory());
                }

                update();
            }
        }

        private void deleteItems()
        {
            Cursor = itemsListView.Cursor = Cursors.WaitCursor;

            List<string> paths = new List<string>(itemsListView.SelectedItems.Count);
            for (int i = 0; i < itemsListView.SelectedItems.Count; ++i)
            {
                if (!FMF.IsPathNotInsideSystem(Path.Combine(FMF.GetCurrentDirectory(), itemsListView.SelectedItems[i].SubItems[0].Text)))
                {
                    MessageBox.Show("Запрещено удалять папку System или её содержимое.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                paths.Add(Path.Combine(FMF.GetCurrentDirectory(), itemsListView.SelectedItems[i].SubItems[0].Text));
            }

            FMF.Delete(paths.ToArray());

            update();

            Cursor = itemsListView.Cursor = Cursors.Arrow;
        }

        private void mainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Process.GetProcessesByName("MainFunctional").Length != 0)
            {
                //mailslotClient.SendMessage("QUIT");
                isWorking = false;
            }

            Application.Exit();
        }

        private void PathTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (openItem(Path.Combine(FMF.GetRootDirectory(), pathTextBox.Text)))
                {
                    pathHistoryAdd(FMF.GetCurrentDirectory());
                }
            }
        }

        private void itemsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (itemsListView.SelectedItems.Count > 0)
                {
                    if (openItem(Path.Combine(FMF.GetCurrentDirectory(), itemsListView.SelectedItems[0].SubItems[0].Text)))
                    {
                        pathHistoryAdd(FMF.GetCurrentDirectory());
                    }
                }
            }
        }

        private void itemsListView_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hitTestInfo = itemsListView.HitTest(e.Location);
            if (e.Button == MouseButtons.Right)
            {
                switch (hitTestInfo.Location)
                {
                    case ListViewHitTestLocations.Label:
                        itemsContextMenu.Show(MousePosition);
                        break;

                    case ListViewHitTestLocations.None:
                        itemsListViewContextMenu.Show(MousePosition);
                        break;
                }
            }
        }

        private void upButton_MouseClick(object sender, MouseEventArgs e)
        {
            directoryGoUp();
        }

        private void openItem_Click(object sender, EventArgs e)
        {
            if (itemsListView.SelectedItems.Count > 0)
            {
                if (openItem(Path.Combine(FMF.GetCurrentDirectory(), itemsListView.SelectedItems[0].SubItems[0].Text)))
                {
                    pathHistoryAdd(FMF.GetCurrentDirectory());
                }
            }
        }

        private void cutItemsButton_Click(object sender, EventArgs e)
        {
            cutItemsToBuffer();
        }

        private void copyItemsButton_Click(object sender, EventArgs e)
        {
            copyItemsToBuffer();
        }

        private void deleteItemsButton_Click(object sender, EventArgs e)
        {
            deleteItems();
        }

        private void renameItemButton_Click(object sender, EventArgs e)
        {
            itemsListView.SelectedItems[0].BeginEdit();
        }

        private void insertItemsButton_Click(object sender, EventArgs e)
        {
            insertItemsFromBuffer();
        }

        private void updateItemsButton_Click(object sender, EventArgs e)
        {
            update();
        }

        private void createDirectoryButton_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(FMF.GetCurrentDirectory(), FMF.ItemNameIterateUntilItExists("Новая папка"));

            if (FMF.IsPathSizeTooLarge(path))
            {
                if (!path.EndsWith("\\"))
                {
                    path = string.Format("{0}\\", path);
                }
                FMF.ShowPathIsTooLargeMessageBox(path.Replace(FMF.GetRootDirectory() + '\\', ""));
                return;
            }
            else if (!FMF.IsPathNotInsideSystem(Path.Combine(FMF.GetCurrentDirectory(), path)))
            {
                MessageBox.Show("Запрещено изменять содержимое папки System", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (FMF.CreateDirectory(Path.Combine(FMF.GetCurrentDirectory(), path)))
            {
                string[] item = FMF.GetItemInfo(path);

                int index = item[0].LastIndexOf('\\') + 1;
                ListViewItem listViewItem = new ListViewItem(item[0].Substring(index));
                listViewItem.SubItems.Add(item[1]);
                listViewItem.SubItems.Add(item[2]);
                listViewItem.SubItems.Add(item[3]);
                listViewItem.ImageIndex = 0;

                itemsListView.Items.Add(listViewItem);

                isCreatedDirectoryRenaming = true;

                listViewItem.BeginEdit();
            }
        }

        private void createFileButton_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(FMF.GetCurrentDirectory(), FMF.ItemNameIterateUntilItExists("Новый файл"));

            if (FMF.IsPathSizeTooLarge(path))
            {
                if (!path.EndsWith("\\"))
                {
                    path = string.Format("{0}\\", path);
                }
                FMF.ShowPathIsTooLargeMessageBox(path.Replace(FMF.GetRootDirectory() + '\\', ""));
                return;
            }
            else if (!FMF.IsPathNotInsideSystem(Path.Combine(FMF.GetCurrentDirectory(), path)))
            {
                MessageBox.Show("Запрещено изменять содержимое папки System", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (FMF.CreateFile(path))
            {
                string[] item = FMF.GetItemInfo(path);

                int index = item[0].LastIndexOf('\\') + 1;
                ListViewItem listViewItem = new ListViewItem(item[0].Substring(index));
                listViewItem.SubItems.Add(item[1]);
                listViewItem.SubItems.Add(item[2]);
                listViewItem.SubItems.Add(item[3]);
                listViewItem.ImageIndex = 6;

                itemsListView.Items.Add(listViewItem);

                listViewItem.BeginEdit();
            }
            else
            {
                FMF.ShowItemAlreadyExists(path.Remove(0, path.LastIndexOf('\\') + 1));
            }
        }

        private void itemsListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                e.CancelEdit = true;
                isCreatedDirectoryRenaming = false;
                return;
            }

            if (itemsListView.SelectedItems.Count > 0)
            {
                if ((FMF.GetCurrentDirectory().Equals(FMF.GetRootDirectory()) &&
                    itemsListView.SelectedItems[0].SubItems[0].Text.ToLower().Equals("system")) ||
                    !FMF.IsPathNotInsideSystem(Path.Combine(FMF.GetCurrentDirectory(), itemsListView.SelectedItems[0].SubItems[0].Text)))
                {
                    MessageBox.Show("Запрещено переименовывать папку System или её содержимое.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    isCreatedDirectoryRenaming = false;
                    e.CancelEdit = true;
                    return;
                }

                if (itemsListView.SelectedItems[0].SubItems[0].Text.Equals(e.Label))
                {
                    isCreatedDirectoryRenaming = false;
                    e.CancelEdit = true;
                    return;
                }

                if (FMF.IsPathSizeTooLarge(Path.Combine(FMF.GetCurrentDirectory(), e.Label)))
                {
                    FMF.ShowPathIsTooLargeMessageBox(Path.Combine(FMF.GetCurrentDirectory(), e.Label).Replace(FMF.GetRootDirectory() + '\\', ""));
                }
                else
                {
                    string oldName;
                    string newName;

                    oldName = itemsListView.SelectedItems[0].SubItems[0].Text;

                    if ((newName = FMF.Rename(itemsListView.SelectedItems[0].SubItems[0].Text, e.Label)) != null)
                    {

                        update();
                        itemsListView.FindItemWithText(newName).EnsureVisible();

                        if (Directory.Exists(newName) && !isCreatedDirectoryRenaming)
                        {
                            string message = FMF.GetCurrentDirectory().Replace(FMF.GetRootDirectory(), "");

                            if (message == "")
                            {
                                message = "Корневой путь";
                            }
                            else
                            {
                                message = message.Substring(1);
                            }

                            message = string.Format("{0}|{1}|{2}|{3}", DateTime.Now.ToString(), message, oldName, newName);

                            mailslotClient.SendMessage(message);
                        }
                    }
                }

                e.CancelEdit = true;
            }

            isCreatedDirectoryRenaming = false;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            pathHistoryGoBack();
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            pathHistoryGoForward();
        }

        private void itemsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (itemsListView.SelectedItems.Count > 0)
                {
                    if (openItem(Path.Combine(FMF.GetCurrentDirectory(), itemsListView.SelectedItems[0].SubItems[0].Text)))
                    {
                        pathHistoryAdd(FMF.GetCurrentDirectory());
                    }
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                deleteItems();
            }
            else if (e.Modifiers == Keys.Control)
            {
                if (e.KeyCode == Keys.X)
                {
                    cutItemsToBuffer();
                }
                else if (e.KeyCode == Keys.C)
                {
                    copyItemsToBuffer();
                }
                else if (e.KeyCode == Keys.V)
                {
                    insertItemsFromBuffer();
                }
            }
            else if (e.Modifiers == Keys.Alt)
            {
                if (e.KeyCode == Keys.Up)
                {
                    directoryGoUp();
                }
                else if (e.KeyCode == Keys.Left)
                {
                    pathHistoryGoBack();
                }
                else if (e.KeyCode == Keys.Right)
                {
                    pathHistoryGoForward();
                }
            }
        }

        private void itemsListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = itemsListView.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void aboutProgramButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Операционная система: Windows\nЯзык программирования: C#\n\nКиргизов Олег Геннадьевич\nГруппа: ПО-83",
                "О программе", MessageBoxButtons.OK);
        }

        private void mainWindow_SizeChanged(object sender, EventArgs e)
        {
            itemsListView.Columns[0].Width = Width - MinimumSize.Width + originalNameColumnSize;
        }

        private void itemsListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (listViewItemComparer.GetCurrentSortingColumn() == e.Column)
            {
                string oldChar, newChar;
                if (listViewItemComparer.IsAscending())
                {
                    oldChar = " ˄";
                    newChar = " ˅";
                }
                else
                {
                    oldChar = " ˅";
                    newChar = " ˄";
                }

                itemsListView.Columns[e.Column].Text = itemsListView.Columns[e.Column].Text.Replace(oldChar, newChar);
                listViewItemComparer.SetAscending(!listViewItemComparer.IsAscending());
            }
            else
            {
                itemsListView.Columns[listViewItemComparer.GetCurrentSortingColumn()].Text =
                    itemsListView.Columns[listViewItemComparer.GetCurrentSortingColumn()].Text.
                    Remove(itemsListView.Columns[listViewItemComparer.GetCurrentSortingColumn()].Text.Length - 2);

                itemsListView.Columns[e.Column].Text = string.Format("{0}{1}",
                    itemsListView.Columns[e.Column].Text, listViewItemComparer.IsAscending() ? " ˄" : " ˅");
                listViewItemComparer.SetCurrentSortingColumn(e.Column);
            }

            update();
        }

        private void mainFunctionalButton_Click(object sender, EventArgs e)
        {
            Cursor = itemsListView.Cursor = Cursors.WaitCursor;
            mainFunctionalButton.Enabled = false;

            Process[] processes = Process.GetProcessesByName("MainFunctional");

            if (processes.Length != 0)
            {
                int newXPosition = Location.X + Width - 10;

                if (newXPosition >= Screen.PrimaryScreen.Bounds.Width - 20)
                {
                    newXPosition = 0;
                }

                mailslotClient.SendMessage(string.Format("SHOW|{0}|{1}|{2}",
                    Process.GetCurrentProcess().ProcessName,
                    newXPosition, Location.Y + minimalFunctionalWindow.Height - 5));
            }

            mainFunctionalButton.Enabled = true;
            Cursor = itemsListView.Cursor = Cursors.Arrow;
        }

        private void minimalFunctionalButton_Click(object sender, EventArgs e)
        {
            if (!minimalFunctionalWindow.Visible)
            {
                int newXPosition = Location.X + Width - 10;

                if (newXPosition >= Screen.PrimaryScreen.Bounds.Width - 20)
                {
                    newXPosition = 0;
                }

                minimalFunctionalWindow.Location = new Point(newXPosition, Location.Y);
                minimalFunctionalWindow.Show();
            }

            minimalFunctionalWindow.Focus();
        }

        private void taskManagerButton_Click(object sender, EventArgs e)
        {
            Cursor = itemsListView.Cursor = Cursors.WaitCursor;
            taskManagerButton.Enabled = false;

            Process.Start("taskmgr");

            taskManagerButton.Enabled = true;
            Cursor = itemsListView.Cursor = Cursors.Arrow;
        }

        private void commandLineButton_Click(object sender, EventArgs e)
        {
            Cursor = itemsListView.Cursor = Cursors.WaitCursor;
            commandLineButton.Enabled = false;

            Process.Start("cmd");

            commandLineButton.Enabled = true;
            Cursor = itemsListView.Cursor = Cursors.Arrow;
        }

        private void MainWindow_Activated(object sender, EventArgs e)
        {
            if (isUpdateRequired)
            {
                update();
                isUpdateRequired = false;
            }
        }
    }

    public class ListViewItemComparer : IComparer
    {
        private int index = 0;
        private bool _isAscending = true;

        public void SetCurrentSortingColumn(int index)
        {
            this.index = index;
        }

        public void SetAscending(bool isAscending)
        {
            _isAscending = isAscending;
        }

        public int GetCurrentSortingColumn()
        {
            return index;
        }

        public bool IsAscending()
        {
            return _isAscending;
        }

        public int Compare(object x, object y)
        {
            ListViewItem.ListViewSubItemCollection A = ((ListViewItem)x).SubItems;
            ListViewItem.ListViewSubItemCollection B = ((ListViewItem)y).SubItems;

            if (A[2].Text.Equals("Папка с файлами"))
            {
                if (!B[2].Text.Equals("Папка с файлами"))
                {
                    return -1;
                }
            }
            else if (B[2].Text.Equals("Папка с файлами"))
            {
                return 1;
            }

            int res = 0;

            if (index == 1)
            {
                DateTime DTA = DateTime.Parse(A[index].Text);
                DateTime DTB = DateTime.Parse(B[index].Text);

                res = DTB.CompareTo(DTA);
            }
            else if (index == 2)
            {
                res = A[index].Text.CompareTo(B[index].Text);
            }
            else if (index == 3)
            {
                string pathA = FMF.GetFullPath(A[0].Text);
                string pathB = FMF.GetFullPath(B[0].Text);

                if (pathA != null && pathB != null)
                {
                    res = FMF.GetFileSizeInBytes(pathA).CompareTo(FMF.GetFileSizeInBytes(pathB));
                }
            }

            if (res != 0)
            {
                return _isAscending ? res : -res;
            }

            res = A[0].Text.CompareTo(B[0].Text);

            return _isAscending ? res : -res;
        }
    }
}
