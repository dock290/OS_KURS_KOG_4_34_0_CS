using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

public static class FileManagerFunctions
{
    private static readonly string ROOT_PATH;
    private static readonly string SYSTEM_PATH;

    private static readonly string[] SIZES = { "Б", "КБ", "МБ", "ГБ", "ТБ" };

    static FileManagerFunctions()
    {
        StringBuilder buffer = new StringBuilder(Application.StartupPath);
        
        buffer.Append('\\');
        SYSTEM_PATH = buffer.ToString();
        buffer.Remove(buffer.Length - 1, 1);

        int index = buffer.ToString().LastIndexOf('\\');
        buffer.Remove(index + 1, buffer.Length - index - 1);

        ROOT_PATH = buffer.ToString();

        Directory.SetCurrentDirectory(ROOT_PATH);
    }

    public static string GetRootDirectory()
    {
        return ROOT_PATH.Remove(ROOT_PATH.Length - 1);
    }

    public static string GetSystemDirectory()
    {
        return SYSTEM_PATH.Remove(SYSTEM_PATH.Length - 1);
    }

    public static bool IsPathInsideRoot(string fullPath)
    {
        string checkPath = fullPath;

        if (checkPath.Contains(".."))
        {
            return false;
        }

        if (!checkPath.EndsWith("\\"))
        {
            checkPath = string.Format("{0}\\", checkPath);
        }

        return checkPath.ToLower().Contains(ROOT_PATH.ToLower());
    }

    public static bool IsPathNotInsideSystem(string fullPath)
    {
        string checkPath = fullPath;
        if (!checkPath.EndsWith("\\"))
        {
            checkPath = string.Format("{0}\\", checkPath);
        }

        if (checkPath.ToLower().Equals(SYSTEM_PATH.ToLower()))
        {
            return true;
        }
        else
        {
            return !checkPath.ToLower().Contains(SYSTEM_PATH.ToLower());
        }
    }

    public static string FormatSize(long size)
    {
        int order = 0;
        while (size >= 1024 && order < SIZES.Length - 1)
        {
            order++;
            size /= 1024;
        }
        return string.Format("{0} {1}", size, SIZES[order]);
    }

    public static List<string> GetAllSubDirectoriesPaths(string fullPath)
    {
        List<string> paths = new List<string>();

        DirectoryInfo[] dirs = new DirectoryInfo(fullPath).GetDirectories();
        foreach (DirectoryInfo dir in dirs)
        {
            paths.Add(dir.FullName);
            paths.AddRange(GetAllSubDirectoriesPaths(dir.FullName));
        }

        return paths;
    }

    public static List<string> GetAllSubFilesPaths(string fullPath)
    {
        List<string> paths = new List<string>();

        FileInfo[] files = new DirectoryInfo(fullPath).GetFiles();
        foreach (FileInfo file in files)
        {
            paths.Add(file.FullName);
        }

        DirectoryInfo[] dirs = new DirectoryInfo(fullPath).GetDirectories();
        foreach (DirectoryInfo dir in dirs)
        {
            paths.AddRange(GetAllSubFilesPaths(dir.FullName));
        }

        return paths;
    }

    public static string ItemNameIterateUntilItExists(string fullPath)
    {
        StringBuilder number = new StringBuilder();
        for (int i = 0; i < int.MaxValue; ++i)
        {
            if (IsItemExists(fullPath + number.ToString()))
            {
                if (i == 0)
                {
                    number.Append(" (1)");
                    i++;
                }
                else
                {
                    number.Replace(string.Format("{0}", i - 1), string.Format("{0}", i));
                }
            }
            else
            {
                break;
            }
        }

        return fullPath + number.ToString();
    }

    public static bool SetCurrentDirectory(string fullPath)
    {
        if (fullPath.Contains("\\\\"))
        {
            return false;
        }

        if (IsPathSizeTooLarge(fullPath))
        {
            if (!fullPath.EndsWith("\\"))
            {
                fullPath = string.Format("{0}\\", fullPath);
            }
            fullPath = fullPath.Replace(ROOT_PATH, "");
            ShowPathIsTooLargeMessageBox(fullPath.Remove(fullPath.Length - 1));
        }

        if (Directory.Exists(fullPath))
        {
            DirectoryInfo directory = new DirectoryInfo(fullPath);
            if (IsPathInsideRoot(fullPath))
            {
                Directory.SetCurrentDirectory(directory.Parent.GetFileSystemInfos(directory.Name)[0].FullName);
                return true;
            }
        }

        if (!fullPath.EndsWith("\\"))
        {
            fullPath = string.Format("{0}\\", fullPath);
        }
        fullPath = fullPath.Replace(ROOT_PATH, "");
        ShowWrongPathMessageBox(fullPath.Remove(fullPath.Length - 1));

        return false;
    }

    public static string GetCurrentDirectory()
    {
        return Directory.GetCurrentDirectory();
    }

    public static string[][] GetDirectoryFiles(string fullPath)
    {
        List<string[]> result = new List<string[]>();
        string[] items = Directory.GetFiles(fullPath);

        for (int i = 0; i < items.Length; ++i)
        {
            result.Add(GetItemInfo(items[i]));
        }

        return result.ToArray();
    }

    public static string[][] GetDirectoryDirectories(string fullPath)
    {
        List<string[]> result = new List<string[]>();
        string[] items = Directory.GetDirectories(fullPath);

        for (int i = 0; i < items.Length; ++i)
        {
            result.Add(GetItemInfo(items[i]));
        }

        return result.ToArray();
    }

    public static string[][] GetDirectoryItems(string fullPath)
    {
        List<string[]> result = new List<string[]>();

        result.AddRange(GetDirectoryDirectories(fullPath));
        result.AddRange(GetDirectoryFiles(fullPath));

        return result.ToArray();
    }

    public static string[] GetItemInfo(string fullPath)
    {
        string[] item = new string[4];

        item[0] = fullPath;
        item[1] = File.GetCreationTime(fullPath).ToString();
        if (File.Exists(fullPath))
        {
            if (!Path.GetExtension(fullPath).Equals(""))
            {
                item[2] = Path.GetExtension(fullPath).Substring(1).ToUpper();
            }
            else
            {
                item[2] = "Файл";
            }
            item[3] = FormatSize(new FileInfo(fullPath).Length);
        }
        else
        {
            item[2] = "Папка с файлами";
            item[3] = "";
        }

        return item;
    }

    public static long GetFileSizeInBytes(string fullPath)
    {
        if (File.Exists(fullPath))
        {
            return new FileInfo(fullPath).Length;
        }
        return 0;
    }

    public static bool Move(string[] oldFullPath, string newFullPath)
    {
        List<string> oldFullPaths = new List<string>();
        List<string> newFullPaths = new List<string>();

        if (CopySubFunction(oldFullPath, newFullPath, ref oldFullPaths, ref newFullPaths))
        {
            for (int i = 0; i < oldFullPaths.Count; ++i)
            {
                if (!oldFullPaths[i].Equals(newFullPaths[i]))
                {
                    bool isError = false;
                    if (File.Exists(newFullPaths[i]))
                    {
                        try
                        {
                            File.Delete(newFullPaths[i]);
                        }
                        catch (IOException)
                        {
                            isError = true;
                            ShowFileIsOpenErrorMessageBox(newFullPaths[i].Substring(newFullPaths[i].LastIndexOf('\\') + 1));
                        }
                    }

                    try
                    {
                        File.Move(oldFullPaths[i], newFullPaths[i]);
                    }
                    catch (IOException)
                    {
                        if (isError)
                        {
                            ShowFileIsOpenErrorMessageBox(oldFullPaths[i].Substring(oldFullPaths[i].LastIndexOf('\\') + 1));
                        }
                    }
                }
            }

            for (int i = 1; i < oldFullPath.Length; ++i)
            {
                string pathCombine = Path.Combine(oldFullPath[0], oldFullPath[i]);

                if (Directory.Exists(pathCombine))
                {
                    if (!(Path.Combine(newFullPath, oldFullPath[i]) + '\\').Contains(pathCombine + '\\') &&
                        !pathCombine.Equals(newFullPath) && !pathCombine.Equals(Path.Combine(newFullPath, oldFullPath[i])) &&
                        IsPathNotInsideSystem(Path.Combine(newFullPath, oldFullPath[i])))
                    {
                        try
                        {
                            Directory.Delete(pathCombine, true);
                        }
                        catch (IOException)
                        {
                            ShowDirectoryIsOpenErrorMessageBox(oldFullPath[i]);
                        }
                    }
                }
                else if (File.Exists(pathCombine))
                {
                    if (!pathCombine.Equals(Path.Combine(newFullPath, oldFullPath[i])))
                    {
                        try
                        {
                            File.Delete(pathCombine);
                        }
                        catch (IOException)
                        {
                            ShowFileIsOpenErrorMessageBox(oldFullPath[i]);
                        }
                    }
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool Copy(string[] oldFullPath, string newFullPath)
    {
        List<string> oldFullPaths = new List<string>();
        List<string> newFullPaths = new List<string>();

        if (CopySubFunction(oldFullPath, newFullPath, ref oldFullPaths, ref newFullPaths))
        {
            for (int i = 0; i < oldFullPaths.Count; ++i)
            {
                bool isError = false;
                if (Directory.Exists(newFullPaths[i]))
                {
                    continue;
                }
                else if (File.Exists(newFullPaths[i]) && !oldFullPaths[i].Equals(newFullPaths[i]))
                {
                    try
                    {
                        File.Delete(newFullPaths[i]);
                    }
                    catch (IOException)
                    {
                        isError = true;
                        ShowFileIsOpenErrorMessageBox(newFullPaths[i].Substring(oldFullPaths[i].LastIndexOf('\\') + 1));
                    }
                }

                try
                {
                    File.Copy(oldFullPaths[i], newFullPaths[i]);
                }
                catch (IOException)
                {
                    if (!isError)
                    {
                        ShowFileIsOpenErrorMessageBox(newFullPaths[i].Substring(oldFullPaths[i].LastIndexOf('\\') + 1));
                    }
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    private static bool CopySubFunction(string[] oldFullPath, string newFullPath, ref List<string> oldFullPaths, ref List<string> newFullPaths)
    {
        if (oldFullPath == null || newFullPath == null)
        {
            return false;
        }

        if (!IsPathNotInsideSystem(newFullPath))
        {
            ShowWrongPathMessageBox(newFullPath.Replace(ROOT_PATH, ""));
            return false;
        }

        List<string> warnings = new List<string>();

        for (int i = 1; i < oldFullPath.Length; ++i)
        {
            string pathCombine = Path.Combine(oldFullPath[0], oldFullPath[i]);

            if (pathCombine.Equals(Path.Combine(newFullPath, oldFullPath[i])))
            {
                continue;
            }

            if (Directory.Exists(pathCombine))
            {
                string checkOldPath = '\\' + pathCombine.Replace(ROOT_PATH, "") + '\\';
                string checkNewPath = '\\' + (newFullPath + '\\').Replace(ROOT_PATH, "") + '\\';

                if (checkNewPath.Contains(checkOldPath))
                {
                    string newFullPathShortBuffer = newFullPath.Substring(newFullPath.LastIndexOf('\\') + 1);

                    newFullPathShortBuffer = newFullPathShortBuffer.Remove(0, newFullPathShortBuffer.LastIndexOf('\\') + 1);
                    ShowParentCutToDaughterPathMessageBox(oldFullPath[i], newFullPathShortBuffer);
                }
                else if (!IsPathNotInsideSystem(pathCombine.Replace(oldFullPath[0], newFullPath)))
                {
                    ShowWrongPathMessageBox(pathCombine.Replace(oldFullPath[0], newFullPath).Replace(ROOT_PATH, ""));
                }
                else
                {
                    oldFullPaths.Add(pathCombine);
                    oldFullPaths.AddRange(GetAllSubDirectoriesPaths(pathCombine));
                }
            }

            if (!Directory.Exists(pathCombine) && !File.Exists(pathCombine))
            {
                warnings.Add(pathCombine);
            }
        }

        if (warnings.Count == 1)
        {
            ShowItemDoesNotExistAnymoreMessageBox(warnings[0]);
        }
        else if (warnings.Count > 1)
        {
            ShowItemsDoNotExistAnymoreMessageBox(warnings.Count);
        }
        warnings.Clear();

        for (int i = 0; i < oldFullPaths.Count; ++i)
        {
            string newFullPathBuffer = oldFullPaths[i].Replace(oldFullPath[0], newFullPath);

            if (File.Exists(newFullPathBuffer))
            {
                warnings.Add(oldFullPaths[i].Substring(oldFullPaths[i].LastIndexOf('\\') + 1));
                oldFullPaths.RemoveAt(i);
                i--;
            }
            else
            {
                newFullPaths.Add(newFullPathBuffer);
            }
        }

        if (warnings.Count == 1)
        {
            ShowAskToRenameDirectoryMessageBox(warnings[0]);
        }
        else if (warnings.Count > 1)
        {
            ShowAskToRenameDirectorysMessageBox(warnings.Count);
        }
        warnings.Clear();

        for (int i = 0; i < newFullPaths.Count; ++i)
        {
            if (IsPathSizeTooLarge(newFullPaths[i]))
            {
                warnings.Add(newFullPaths[i]);
                oldFullPaths.RemoveAt(i);
                newFullPaths.RemoveAt(i);
                i--;
            }
        }

        if (warnings.Count == 1)
        {
            ShowPathIsTooLargeMessageBox(warnings[0]);
        }
        else if (warnings.Count > 1)
        {
            ShowPathsAreTooLargeMessageBox(warnings.Count);
        }
        warnings.Clear();

        for (int i = 0; i < newFullPaths.Count; ++i)
        {
            if (!Directory.Exists(newFullPaths[i]) && !File.Exists(newFullPaths[i]))
            {
                Directory.CreateDirectory(newFullPaths[i]);
            }
        }

        oldFullPaths.Clear();
        newFullPaths.Clear();

        for (int i = 1; i < oldFullPath.Length; ++i)
        {
            string pathCombine = Path.Combine(oldFullPath[0], oldFullPath[i]);

            if (Directory.Exists(pathCombine))
            {
                string checkOldPath = "\\" + pathCombine.Replace(ROOT_PATH, "") + '\\';
                string checkNewPath = "\\" + (newFullPath + '\\').Replace(ROOT_PATH, "") + "\\";

                if (checkNewPath.Contains(checkOldPath))
                {
                    continue;
                }
            }

            if (File.Exists(pathCombine))
            {
                oldFullPaths.Add(pathCombine);
            }
            else if (Directory.Exists(pathCombine) &&
                !IsPathNotInsideSystem(pathCombine.Replace(oldFullPath[0], newFullPath + '\\')))
            {
                oldFullPaths.AddRange(GetAllSubFilesPaths(pathCombine));
            }
        }

        List<string> existingFilesBuffer = new List<string>();
        for (int i = 0; i < oldFullPaths.Count; ++i)
        {
            string newFullPathBuffer = oldFullPaths[i].Replace(oldFullPath[0], newFullPath);

            if (oldFullPaths[i].Equals(newFullPathBuffer))
            {
                oldFullPaths.RemoveAt(i);
                continue;
            }

            if (Directory.Exists(newFullPathBuffer))
            {
                warnings.Add(oldFullPaths[i].Substring(oldFullPaths[i].LastIndexOf('\\') + 1));
                oldFullPaths.RemoveAt(i);
                i--;
            }
            else if (File.Exists(newFullPathBuffer))
            {
                existingFilesBuffer.Add(oldFullPaths[i]);
                oldFullPaths.RemoveAt(i);
                i--;
            }
            else
            {
                newFullPaths.Add(newFullPathBuffer);
            }
        }

        if (warnings.Count == 1)
        {
            ShowAskToRenameFileMessageBox(warnings[0]);
        }
        else if (warnings.Count > 1)
        {
            ShowAskToRenameFilesMessageBox(warnings.Count);
        }
        warnings.Clear();

        if (existingFilesBuffer.Count == 1)
        {
            if (ShowAskToOverrideFileMessageBox(existingFilesBuffer[0]))
            {
                oldFullPaths.Add(existingFilesBuffer[0]);
                newFullPaths.Add(existingFilesBuffer[0].Replace(oldFullPath[0], newFullPath));
            }
        }
        else if (existingFilesBuffer.Count > 1)
        {
            if (ShowAskToOverrideFilesMessageBox(existingFilesBuffer.Count))
            {
                for (int i = 0; i < existingFilesBuffer.Count; ++i)
                {
                    oldFullPaths.Add(existingFilesBuffer[i]);
                    newFullPaths.Add(existingFilesBuffer[i].Replace(oldFullPath[0], newFullPath));
                }
            }
        }
        existingFilesBuffer.Clear();

        return true;
    }

    public static string[][] GetCurrentDirectoryFiles()
    {
        return GetDirectoryFiles(Directory.GetCurrentDirectory());
    }

    public static string[][] GetCurrentDirectoryDirectories()
    {
        return GetDirectoryDirectories(Directory.GetCurrentDirectory());
    }

    public static string[][] GetCurrentDirectoryItems()
    {
        return GetDirectoryItems(Directory.GetCurrentDirectory());
    }

    public static bool CreateDirectory(string path)
    {
        if (!IsItemExists(path))
        {
            Directory.CreateDirectory(path);
            return true;
        }

        return false;
    }

    public static bool CreateFile(string path)
    {
        if (!IsItemExists(path))
        {
            File.Create(path).Close();
            return true;
        }

        return false;
    }

    public static bool IsItemExists(string path)
    {
        return Directory.Exists(path) || File.Exists(path);
    }

    public static bool Delete(string[] paths)
    {
        bool isChanged = false;

        for (int i = 0; i < paths.Length; ++i)
        {
            if (Directory.Exists(paths[i]))
            {
                try
                {
                    Directory.Delete(paths[i], true);
                }
                catch (Exception)
                {
                    ShowDirectoryIsOpenErrorMessageBox(paths[i].Substring(paths[i].LastIndexOf('\\') + 1));
                }
                isChanged = true;
            }
            else if (File.Exists(paths[i]))
            {
                try
                {
                    File.Delete(paths[i]);
                }
                catch (Exception)
                {
                    ShowFileIsOpenErrorMessageBox(paths[i].Substring(paths[i].LastIndexOf('\\') + 1));
                }

                isChanged = true;
            }
        }

        return isChanged;
    }

    public static string Rename(string oldName, string newName)
    {
        if (oldName == null || newName == null)
        {
            return null;
        }

        if (newName.Contains("\\") || newName.Contains("/") || newName.Contains(":") ||
            newName.Contains("*") || newName.Contains("?") || newName.Contains("\"") ||
            newName.Contains("<") || newName.Contains(">") || newName.Contains("|"))
        {
            ShowWrongSignsFileNameMessageBox();
            return null;
        }

        while (newName.StartsWith(" "))
        {
            newName = newName.Remove(0, 1);
        }

        while (newName.EndsWith(" "))
        {
            newName = newName.Remove(newName.Length - 1, 1);
        }

        if (newName.Equals(""))
        {
            if (Directory.Exists(oldName))
            {
                newName = "Новая папка";
            }
            else if (File.Exists(oldName))
            {
                newName = "Новый файл";
            }

            newName = ItemNameIterateUntilItExists(newName);
        }

        if (IsItemExists(newName))
        {
            ShowItemAlreadyExists(newName);
            return null;
        }

        if (Directory.Exists(oldName) && !IsItemExists(newName))
        {
            try
            {
                Directory.Move(oldName, newName);
                return newName;
            }
            catch (IOException)
            {
                ShowDirectoryIsOpenErrorMessageBox(oldName);
                return newName;
            }
        }
        else if (File.Exists(oldName) && !IsItemExists(newName))
        {
            try
            {
                File.Move(oldName, newName);
                return newName;
            }
            catch (IOException)
            {
                ShowFileIsOpenErrorMessageBox(oldName);
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public static string GetFullPath(string shortPath)
    {
        return Directory.Exists(shortPath) || File.Exists(shortPath) ? Path.GetFullPath(shortPath) : null;
    }

    public static void StartProcess(string fullPath)
    {
        try
        {
            Process.Start(fullPath);
        }
        catch { }
    }

    public static bool IsPathSizeTooLarge(string fullPath)
    {
        if (fullPath == null)
        {
            return false;
        }

        if (fullPath.Length >= 248)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void ShowWrongSignsFileNameMessageBox()
    {
        MessageBox.Show("Имя файла не должно содержать следующих знаков: \\ / : * \" < > |.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static void ShowFileIsOpenErrorMessageBox(string shortFileName)
    {
        MessageBox.Show(string.Format("Операция не может быть завершена, так как файл \"{0}\" открыт в другой программе. " +
            "Файл будет пропущен.", shortFileName),
            "Файл уже используется", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static void ShowDirectoryIsOpenErrorMessageBox(string shortDirectoryName)
    {
        MessageBox.Show(string.Format("Операция не может быть завершена, так как папка \"{0}\" открыт в другой программе. " +
            "Папка будет пропущена.", shortDirectoryName),
            "Папка уже используется", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static bool ShowAskToOverrideFileMessageBox(string shortFileName)
    {
        return MessageBox.Show(string.Format("В папке назначения уже есть файл \"{0}\". Вы хотите заменить его?", shortFileName),
            "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
    }

    public static bool ShowAskToOverrideFilesMessageBox(int amount)
    {
        return MessageBox.Show(string.Format("В папке назначения уже есть файлы ({0}). Вы хотите заменить их?", amount),
            "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
    }

    public static void ShowAskToRenameFileMessageBox(string shortFileName)
    {
        MessageBox.Show(string.Format("Папка с указанным именем \"{0}\" уже существует. Файл будет пропущен.", shortFileName),
            "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static void ShowAskToRenameFilesMessageBox(int amount)
    {
        MessageBox.Show(string.Format("Папки с указанными именами ({0}) уже существует. Файлы будут пропущены.", amount),
            "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static void ShowAskToRenameDirectoryMessageBox(string shortDirectoryName)
    {
        MessageBox.Show(string.Format("Указанное имя папки \"{0}\" совпадает с уже именем уже существующиего файла. " +
            "Папка будет пропущена.", shortDirectoryName),
            "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static void ShowAskToRenameDirectorysMessageBox(int amount)
    {
        MessageBox.Show(string.Format("Указанные имена папок ({0}) совпадают с уже именами уже существующих файлов. " +
            "Папки будут пропущены.", amount),
            "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static void ShowPathIsTooLargeMessageBox(string shortPath)
    {
        MessageBox.Show(string.Format("Путь \"{0}\" слишком длинный.", shortPath),
            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static void ShowPathsAreTooLargeMessageBox(int amount)
    {
        MessageBox.Show(string.Format("Пути ({0}) слишком длинные.", amount),
            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static void ShowParentCutToDaughterPathMessageBox(string oldShortPath, string newShortPath)
    {
        MessageBox.Show(
            string.Format("Конечная папка \"{1}\", куда следует поместить файлы, " +
            "является дочерней для папки \"{0}\", в которой они находятся.", oldShortPath, newShortPath),
            "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static void ShowItemDoesNotExistAnymoreMessageBox(string shortPath)
    {
        MessageBox.Show(
            string.Format("Не удаётся найти элемент, расположенный по пути \"{0}\". " +
            "Проверьте расположение этого элемента и повторите попытку.",
            (shortPath.Equals("") ? "Корневая папка" : shortPath)), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static void ShowItemsDoNotExistAnymoreMessageBox(int amount)
    {
        MessageBox.Show(
            string.Format("Не удаётся найти элементы ({0}). Проверьте расположение данных элементов и повторите попытку.",
            amount), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static void ShowWrongPathMessageBox(string shortPath)
    {
        MessageBox.Show(
            string.Format("Не удаётся найти \"{0}\". Проверьте правильность пути и попробуйте ещё раз.",
            shortPath), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static void ShowItemAlreadyExists(string shortPath)
    {
        MessageBox.Show(string.Format("Элемент с названием \"{0}\" уже существует.",
            shortPath), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
