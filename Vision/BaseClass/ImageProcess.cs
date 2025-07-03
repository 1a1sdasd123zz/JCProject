using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro.ImageFile;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using Cognex.VisionPro.Display;
using System.Drawing;
using System.Drawing.Imaging;
using LoggerConfig;
using System.Windows.Forms;

namespace YT_Vision

{
    class ImageProcess
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filepath);

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
        Logger mlogger = new Logger();//LOG操作类
        //保存log日志
        public void SaveLog(object msg)
        {
            String filename = Application.StartupPath + "\\Log\\" + DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                string str = DateTime.Now.ToString("HH:mm:ss") + ":" + msg.ToString();
                mlogger.Save(str, filename + ".log");
            }
            catch (Exception e)
            {
                // mlogger.Save(e.Message, filename + ".log");
            }
        }
        /// <summary>
        /// 保存原图片
        /// </summary>
        /// <param name="image">需要保存的图片</param>
        /// <param name="imageNmane">图像保存名称</param>
        /// <param name="imagePath">图片保存路径</param>
        public void SaveImage(ICogImage image, string imageNmane, string imagePath)
        {
            CogImageFile mImageFile = new CogImageFile();
            string path = "";
          
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }
            try
            {
                if (imageNmane != "")
                {

                    path = imagePath + "\\" + imageNmane + ".bmp";

                }
                else
                {
                    path = imagePath + "\\" + DateTime.Now.ToString("HHmmss");
                }
                mImageFile.Open(path, CogImageFileModeConstants.Write);
                mImageFile.Append(image);
                mImageFile.Close();
                //ErrMsg ="图片保存成功!";
                //PushEvent();
            }
            catch (Exception ex)
            {
                //ErrMsg = "图片保存失败！" + ex.Message;
                //PushEvent();
            }
        }
        /// <summary>
        /// 保存原图片
        /// </summary>
        /// <param name="image">需要保存的图片</param>
        /// <param name="imageNmane">图像保存名称</param>
        /// <param name="imagePath">图片保存路径</param>
        public void Save3DImage(ICogImage image, string imageNmane, string imagePath)
        {
            CogImageFile mImageFile = new CogImageFile();
            string path = "";
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }
            try
            {
                if (imageNmane != "")
                {

                    path = imagePath + "\\" + imageNmane + ".idb";

                }
                else
                {
                    path = imagePath + "\\" + DateTime.Now.ToString("HHmmss");
                }
                mImageFile.Open(path, CogImageFileModeConstants.Write);
                mImageFile.Append(image);
                mImageFile.Close();
                //ErrMsg ="图片保存成功!";
                //PushEvent();
            }
            catch (Exception ex)
            {
                //ErrMsg = "图片保存失败！" + ex.Message;
                //PushEvent();
            }
        }
        /// <summary>
        /// 保存带处理工具图片
        /// </summary>
        /// <param name="image">需要保存带处理工具的窗口</param>
        /// <param name="imageNmane">图像保存名称</param>
        /// <param name="imagePath">保存路径</param>
        public void SaveProcessImage(CogRecordDisplay hwindow, string imageNmane, string imagePath)
        {
            string path = "";

            try
            {
                //ThreadPool.QueueUserWorkItem(new WaitCallback(DeleteProImage), imagePath); //删除过期图片
                //imagePath = imagePath + "\\" + DateTime.Now.ToString("yyyy-MM-dd");//按日期生成文件夹

                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                CogImage24PlanarColor image;

                Bitmap temp = hwindow.CreateContentBitmap(CogDisplayContentBitmapConstants.Display) as Bitmap;
                image = new CogImage24PlanarColor(temp);

                if (imageNmane != "")
                {
                    path = imagePath + "\\" + imageNmane + ".bmp";
                }
                else
                {
                    path = imagePath + "\\" + DateTime.Now.ToString("HHmmss") + ".bmp";
                }

                temp.Save(path, ImageFormat.Bmp);

            }
            catch (Exception ex)
            {
                //ErrMsg = "图片保存失败！" + ex.Message;
                //PushEvent();
            }
        }
       
      
        private object LockDeleteWork = new object();
        /// <summary>
        /// ch:删除图片
        /// </summary>
        /// <param name="deletePath">图片文件夹路径</param>
        /// <param name="imageFileNum">文件夹数量</param>
        /// <returns></returns>
        public bool  DeleteImage(string  deletePath,int imageFileNum)
        {
            bool result = true;
            int ImageFileNums = imageFileNum;
            lock (LockDeleteWork)
            {
                DirectoryInfo CheckFolder = null;
                string DeletePath = deletePath;
                try
                {
                    //目录所有文件夹
                    GlobalValue.ImageFileNums.Clear();                 
                    CheckFolder = new DirectoryInfo(DeletePath);
                    foreach (DirectoryInfo fileName in CheckFolder.GetDirectories())
                    {
                        string DeleteFile = DeletePath + "\\" + fileName.Name;
                        GlobalValue.ImageFileNums.Add(DeleteFile);
                    }
                    if (GlobalValue.ImageFileNums.Count > ImageFileNums)
                    {
                        Directory.Delete(GlobalValue.ImageFileNums[0], true);
                    }
                }
                catch (Exception ex)
                {                  
                    result = false;
                }
               return result;




                //try
                //{    //按指定日期删除
                //    //string DeleteFile = imagePath + "\\" + DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");//要删除的过期图片文件夹
                //    //按文件容量大小来删除
                //    double diskScale = 0;
                //    long freeSpace = new long(); //当前磁盘剩余容量    
                //    string DeleteFile = imagePath as string;
                //    string[] data;
                //    string diskName;
                //    long diskSize = new long(); //当前磁盘总容量             
                //    data = DeleteFile.Split('\\');
                //    diskName = data[0] + "\\";//获取当前磁盘名称
                //    string deletePath = diskName + data[1];//获取要删除的文件夹
                //    System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
                //    foreach (System.IO.DriveInfo drive in drives)
                //    {
                //        if (drive.Name == diskName)
                //        {
                //            diskSize = drive.TotalSize / (1024 * 1024 * 1024); //获取当前磁盘总容量，指定磁盘以GB为单位
                //            freeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024);  //获取当前磁盘剩余容量
                //            diskScale = (freeSpace * 1.0) / (diskSize * 1.0);//计算当前剩余容量占比
                //        }
                //    }
                //    if (Directory.Exists(deletePath))
                //    {
                //        if (diskScale < 0.7)
                //            Directory.Delete(deletePath, true);//不经过回收站，直接删除
                //    }
                //}
                //catch (Exception ex)
                //{
                //    //ErrMsg = "图片删除失败！" + ex.Message;
                //    //PushEvent();
                //};
            }
        }
        /// <summary>
        /// 运行标定工具。.
        /// </summary>
        /// <param name="InputImage"></param>
        /// <param name="CalibTool"></param>
        /// <returns></returns>
        public ICogImage Calib(ICogImage InputImage, CogToolBlock CalibTool)
        {
            ICogImage CalibImage = null;

            try
            {
                //this.Invoke(new Action(() =>
                //{
                //****************运行标定工具******************//
                CalibTool.Inputs["Image"].Value = InputImage;

                CalibTool.Run();

                CalibImage = (ICogImage)CalibTool.Outputs["OutputImage"].Value;

                //}));                

            }
            catch (Exception ex)
            {
                CalibImage = InputImage;
            }

            return CalibImage;

        }



    }
}
