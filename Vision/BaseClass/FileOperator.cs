// BaseClass.FileOperator

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

//using BaseClass;

namespace Vision.BaseClass;

public class FileOperator
{
    public static long GetHardDiskSpace(string str_HardDiskName)
    {
        long totalSize = 0L;
        str_HardDiskName += ":\\";
        DriveInfo[] drives = DriveInfo.GetDrives();
        DriveInfo[] array = drives;
        foreach (DriveInfo drive in array)
        {
            if (drive.Name == str_HardDiskName)
            {
                totalSize = drive.TotalSize / 1073741824;
            }
        }
        return totalSize;
    }

    public static long GetHardDiskFreeSpace(string str_HardDiskName)
    {
        long freeSpace = 0L;
        str_HardDiskName += ":\\";
        DriveInfo[] drives = DriveInfo.GetDrives();
        DriveInfo[] array = drives;
        foreach (DriveInfo drive in array)
        {
            if (drive.Name == str_HardDiskName)
            {
                freeSpace = drive.TotalFreeSpace / 1073741824;
            }
        }
        return freeSpace;
    }

    public static void DeleteFileN(string folderPath, int saveDay, bool flag = true)
    {
        DateTime nowTime = DateTime.Now;
        DirectoryInfo root = new DirectoryInfo(folderPath);
        try
        {
            if (flag)
            {
                DirectoryInfo[] dirs = root.GetDirectories();
                FileAttributes attr2 = File.GetAttributes(folderPath);
                if (attr2 != FileAttributes.Directory)
                {
                    return;
                }
                DirectoryInfo[] array = dirs;
                foreach (DirectoryInfo dir in array)
                {
                    int day2 = (nowTime - dir.CreationTime).Days;
                    if (day2 > saveDay)
                    {
                        Directory.Delete(dir.FullName, recursive: true);
                    }
                }
                return;
            }
            FileInfo[] files = root.GetFiles();
            FileAttributes attr = File.GetAttributes(folderPath);
            if (attr != FileAttributes.Directory)
            {
                return;
            }
            FileInfo[] array2 = files;
            foreach (FileInfo file in array2)
            {
                int day = (nowTime - file.CreationTime).Days;
                if (day > saveDay)
                {
                    File.Delete(file.FullName);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<string> GetNameList(string path, string extension)
    {
        List<string> nameList = new List<string>();
        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] op = dir.GetFiles();
        FileInfo[] array = op;
        foreach (FileInfo file in array)
        {
            if (file.Extension.Equals(extension))
            {
                nameList.Add(file.Name.Split('.')[0]);
            }
        }
        return nameList;
    }

    public static bool SaveCSV(DataTable dt, string fileName, FileMode fileMode = FileMode.OpenOrCreate)
    {
        bool flag = true;
        try
        {
            bool f = !File.Exists(fileName);
            using (FileStream fs = new FileStream(fileName, fileMode, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                {
                    StringBuilder sb = new StringBuilder();
                    if (fileMode == FileMode.OpenOrCreate || f)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            sb.Append(dt.Columns[j].ColumnName.ToString());
                            if (j < dt.Columns.Count - 1)
                            {
                                sb.Append(",");
                            }
                        }
                        sw.WriteLine(sb.ToString());
                    }
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sb.Clear();
                            for (int k = 0; k < dt.Columns.Count; k++)
                            {
                                sb.Append((dt.Rows[i][k].ToString() == "") ? "null" : dt.Rows[i][k].ToString().Trim());
                                if (k < dt.Columns.Count - 1)
                                {
                                    sb.Append(",");
                                }
                            }
                            sw.WriteLine(sb.ToString());
                        }
                        LogUtil.Log("将数据库数据成功存储到本地CSV文件");
                    }
                    else
                    {
                        LogUtil.LogError("write empty data");
                    }
                    sw.Flush();
                }
                fs.Close();
            }
            return flag;
        }
        catch (Exception ex)
        {
            flag = false;
            LogUtil.LogError("将数据库文件导出到本地CSV文件异常，错误信息：" + ex.Message);
            return flag;
        }
    }

    public static bool DeleteFileByPath(string fileFullPath)
    {
        try
        {
            if (File.Exists(fileFullPath))
            {
                FileAttributes attrs = File.GetAttributes(fileFullPath);
                if (attrs == FileAttributes.Directory)
                {
                    Directory.Delete(fileFullPath, recursive: true);
                }
                else
                {
                    File.Delete(fileFullPath);
                }
                File.Delete(fileFullPath);
            }
            LogUtil.Log("文件删除成功");
            return true;
        }
        catch (Exception ex)
        {
            LogUtil.LogError("文件删除错误，错误代码：" + ex.Message);
            return false;
        }
    }

    public static List<string> FileCollection(List<string> files, string path, int saveDay, int countMax, bool flag)
    {
        try
        {
            if (flag && Directory.Exists(path))
            {
                string[] dirs = Directory.GetFileSystemEntries(path);
                string[] array = dirs;
                foreach (string file in array)
                {
                    if (File.Exists(file))
                    {
                        FileInfo info = new FileInfo(file);
                        if ((DateTime.Now - info.CreationTime).Days >= saveDay && files.Count < countMax)
                        {
                            files.Add(file);
                        }
                    }
                    else if (files.Count < countMax)
                    {
                        FileCollection(files, file, saveDay, countMax, flag);
                    }
                }
            }
            return files;
        }
        catch (Exception)
        {
            return files;
        }
    }

    public static void DelEmptyDir(string path, int saveDay, bool flag)
    {
        if (!flag || !Directory.Exists(path))
        {
            return;
        }
        string[] dirs = Directory.GetDirectories(path);
        string[] array = dirs;
        foreach (string dir in array)
        {
            if (Directory.Exists(dir))
            {
                DelEmptyDir(dir, saveDay, flag);
                DirectoryInfo info = new DirectoryInfo(dir);
                if ((DateTime.Now - info.CreationTime).Days >= saveDay && info.GetFileSystemInfos().Length == 0)
                {
                    info.Delete();
                }
            }
        }
    }
}