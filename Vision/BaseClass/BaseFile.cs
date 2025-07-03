using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;


namespace Vision.BaseClass
{
    public class BaseFile
    {
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filepath);

        /// <summary>
        /// 写ini文件
        /// </summary>
        /// <param name="Section">=文件标识</param>
        /// <param name="Key">=写入的变量名</param>
        /// <param name="Value">变量值</param>
        /// <param name="m_Path">保存路径</param>
        public void WirteIni(string Section, string Key, string Value, string m_Path)
        {
            WritePrivateProfileString(Section, Key, Value, m_Path);
        }
        /// <summary>
        /// 读取ini文件
        /// </summary>
        /// <param name="Section">=文件标识</param>
        /// <param name="Key">=读取的变量名</param>
        /// <param name="m_Path">路径</param>
        /// <returns></returns>
        public string GetIni(string Section, string Key, string m_Path)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, m_Path);
            return temp.ToString();
        }

        public static void CopyDirectory(string sourceDir, string targetDir)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDir);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException($"Source directory does not exist or could not be found: {sourceDir}");
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(targetDir, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to the new location.
            foreach (DirectoryInfo subdir in dirs)
            {
                string tempPath = Path.Combine(targetDir, subdir.Name);
                CopyDirectory(subdir.FullName, tempPath);
            }
        }

        /// <summary>
        /// 保存Csv表格
        /// </summary>
        /// <param name="data">=数据</param>
        /// <param name="datatype">=数据类型</param>
        /// <param name="datapath">=数据存放路径</param>

        public void SaveCsvData(string data, string datatype, string datapath, string dataName)
        {
            try
            {
                // String filename = datapath + DateTime.Now.ToString("yyyy-MM-dd") + "_" + dataName + ".csv";
                String filename = datapath + dataName + ".csv";
                string dir = Path.GetDirectoryName(filename);
                if (Directory.Exists(dir) == false)
                {
                    Directory.CreateDirectory(dir);
                }

                if (File.Exists(filename) == false)
                {
                    StreamWriter sw = new StreamWriter(filename, true, Encoding.Default);

                    string Savename = datatype;//表格表头信息

                    sw.WriteLine(Savename);
                    sw.Flush();
                    sw.Close();
                }
                StreamWriter sw1 = new StreamWriter(filename, true, Encoding.Default);

                sw1.WriteLine(data, true);
                sw1.Flush();
                sw1.Close();
            }
            catch
            {

            }
        }


        /// <summary>
        /// 移动文件到共享盘
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="destinationSharePath"></param>
        /// <param name="destinationFilePath"></param>
        /// <returns></returns>
        public static bool UploadShare(string sourceFilePath, string destinationSharePath, string fileName)
        {
            string path = Path.Combine(destinationSharePath, fileName); // 目标文件路径

            try
            {
                // 确保目标路径的共享对当前用户是可访问的
                // 如果需要，可以添加代码来检查共享权限
                if (HasWriteAccess(destinationSharePath))
                {
                    // 移动文件到网络共享盘
                    File.Move(sourceFilePath, path); // true 表示如果目标文件存在则覆盖它
                    return true;
                }
                else
                {
                    LogUtil.LogError("检查网络文件夹无权限或异常！");
                    return false;
                }

            }
            catch (Exception ex)
            {
                LogUtil.LogError(ex.ToString());
                return false;
            }
        }


        public static bool HasWriteAccess(string sharedPath)
        {
            try
            {
                // 获取文件夹信息
                var di = new DirectoryInfo(sharedPath);
                // 获取访问控制列表
                var acl = di.GetAccessControl();
                // 获取当前用户的权限
                var userPermissions = acl.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
                // 检查权限
                foreach (System.Security.AccessControl.FileSystemAccessRule rule in userPermissions)
                {
                    if ((rule.AccessControlType == AccessControlType.Allow) &&
                        (rule.FileSystemRights == FileSystemRights.Write) &&
                        (rule.IdentityReference.Value == WindowsIdentity.GetCurrent().Name))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                // 没有足够的权限访问文件夹或其ACL
                return false;
            }
            catch (Exception)
            {
                // 其他错误处理
                return false;
            }
        }

        public static void CutFile(string sourceFilePath, string destinationFilePath)
        {
            // 确保目标路径存在
            var destinationDirectory = Path.GetDirectoryName(destinationFilePath);
            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            // 剪切文件
            File.Move(sourceFilePath, destinationFilePath);
        }

        public static FileInfo HasFilesOlderThan(string folderPath, TimeSpan olderThan)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            FileInfo[] files = directoryInfo.GetFiles();

            foreach (FileInfo file in files)
            {
                if (DateTime.Now - file.CreationTime > olderThan)
                {
                    return file; // 发现至少一个文件超过指定时间
                }
            }

            return null; // 没有发现任何文件超过指定时间
        }

        public static DirectoryInfo HasDirectorysOlderThan(string folderPath, TimeSpan olderThan)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            FileSystemInfo[] folders = di.GetFileSystemInfos();

            foreach (FileSystemInfo folder in folders)
            {
                if (folder is DirectoryInfo)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(folder.FullName);
                    DateTime lastAccess = dirInfo.CreationTime;
                    TimeSpan ts = DateTime.Now - lastAccess;

                    if (ts.Days >= olderThan.Days)
                    {
                        return dirInfo;
                    }
                }
            }
            return null;
        }


        public static void InsertDataToExcel(string station, string data)
        {
            try
            {
                string directorypath = GlobalValue.SaveFileDisks + $"Data\\{station}\\";
                string filepath = directorypath + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
                if (!File.Exists(filepath))
                {
                    if (!Directory.Exists(directorypath))
                        Directory.CreateDirectory(directorypath);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 设置许可类型
                    using (var package = new ExcelPackage())
                    {
                        var worksheet = package.Workbook.Worksheets.Add("Sheet1"); // 创建一个工作表

                        switch (station)
                        {
                            case "按键贴合工位1":
                                {
                                    worksheet.Cells["A1"].Value = "时间";
                                    worksheet.Cells["B1"].Value = "结果"; // 在A1单元格写入文本
                                    worksheet.Cells["C1"].Value = "长度"; // 在B1单元格写入文本
                                    worksheet.Cells["D1"].Value = "宽度"; // 在B1单元格写入文本
                                    worksheet.Cells[2, 1].Value = "公差上限";
                                    break;
                                }
                            case "按键贴合工位2":
                                {
                                    worksheet.Cells["A1"].Value = "时间";
                                    worksheet.Cells["B1"].Value = "结果"; // 在A1单元格写入文本
                                    worksheet.Cells["C1"].Value = "长度"; // 在B1单元格写入文本
                                    worksheet.Cells["D1"].Value = "宽度"; // 在B1单元格写入文
                                    worksheet.Cells[2, 1].Value = "公差上限";
                                    break;
                                }
                            case "胶路检测":
                                {
                                    worksheet.Cells["A1"].Value = "时间";
                                    worksheet.Cells["B1"].Value = "结果"; // 在A1单元格写入文本
                                    worksheet.Cells["C1"].Value = "长度"; // 在B1单元格写入文本
                                    worksheet.Cells["D1"].Value = "宽度"; // 在B1单元格写入文
                                    worksheet.Cells["E1"].Value = "上偏移"; // 在B1单元格写入文本
                                    worksheet.Cells["F1"].Value = "下偏移"; // 在B1单元格写入文
                                    worksheet.Cells["G1"].Value = "左偏移"; // 在B1单元格写入文本
                                    worksheet.Cells["H1"].Value = "右偏移"; // 在B1单元格写入文
                                    worksheet.Cells[2, 1].Value = "公差上限";
                                    worksheet.Cells[3, 1].Value = "公差下限";
                                    break;
                                }
                        }

                        // 可以继续添加更多的数据到工作表...

                        var fileInfo = new FileInfo(filepath); // 创建一个文件信息对象
                        package.SaveAs(fileInfo); // 保存Excel文件
                    }
                }

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 设置许可
                using (var package = new ExcelPackage((new System.IO.FileInfo(filepath))))
                {
                    var worksheet = package.Workbook.Worksheets[0];

                    // 获取工作表的行数和列数
                    int lastRow = worksheet.Dimension.Rows + 1;
                    int lastColumn = worksheet.Dimension.Columns;
                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    data = time + "," + data;
                    string[] strData = data.Split(',');
                    // 在新行的每一列中插入数据
                    for (int i = 0; i < strData.Length; i++)
                    {
                        // 假设我们要插入的数据是"新数据"，可以根据需要修改
                        worksheet.Cells[lastRow, i + 1].Value = strData[i];
                    }
                    package.Save();
                }
            }
            catch (Exception ex) { LogUtil.LogError(ex.ToString()); }
        }
    }

    public class UploadShare
    {
        string _SourceFilePath;
        string _DestinationSharePath;
        TimeSpan _Time;
        long _Quality;
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="sourePath"></param>图片目录
        /// <param name="sharePath"></param>网络共享目录
        /// <param name="time"></param>设定上传多久前的文件
        /// <param name="quality"></param>压缩质量 0 - 100
        public UploadShare(string sourePath,string sharePath, TimeSpan time,long quality = 50)
        {
            _SourceFilePath = sourePath;
            _DestinationSharePath = sharePath;
            _Time = time;
            _Quality = quality;
        }
        public void ThreadPress()
        {
            try
            {
                FileInfo file = BaseFile.HasFilesOlderThan(_SourceFilePath, _Time);
                if (file != null)
                {
                    if (string.Equals(file.Extension, ".bmp", StringComparison.OrdinalIgnoreCase))
                    {
                        string path = file.FullName;
                        string temDic = Path.Combine(file.DirectoryName, "tem");
                        string dsFilepath = Path.Combine(temDic, file.Name);
                        Directory.CreateDirectory(temDic);
                        BaseImageProcess.CompressImage(path, dsFilepath, _Quality);
                        //BaseFile.UploadShare(dsFilepath, _DestinationSharePath, file.Name);
                        File.Delete(dsFilepath);
                    }
                }
            }
            catch { throw; }
        }
    }
    
}
