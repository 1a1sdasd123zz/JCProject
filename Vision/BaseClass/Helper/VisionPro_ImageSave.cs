using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.ImageFile;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace Vision.BaseClass.Helper
{
    public class VisionPro_ImageSave
    {
        private static Stopwatch sp;

        private static ConcurrentQueue<RawImageInfo> rawImageQueue;

        private static ConcurrentQueue<RawImageInfo> rawImageQueue_RemoteDisk;

        private static ConcurrentQueue<ToolImageInfo> toolImageQueue;

        private static ConcurrentQueue<ToolImageInfo> toolImageQueue_RemoteDisk;

        private static Queue<string> DeleteQueue;

        private static int CountDelete;

        private static List<string> col;

        private static object _lock;

        private static object _lockRaw;

        private static string[] strImageTypes;

        public static string PathDelete { get; set; }

        public static int SaveDays { get; set; }

        public static bool FlagDelete { get; set; }

        static VisionPro_ImageSave()
        {
            sp = new Stopwatch();
            rawImageQueue = new ConcurrentQueue<RawImageInfo>();
            rawImageQueue_RemoteDisk = new ConcurrentQueue<RawImageInfo>();
            toolImageQueue = new ConcurrentQueue<ToolImageInfo>();
            toolImageQueue_RemoteDisk = new ConcurrentQueue<ToolImageInfo>();
            DeleteQueue = new Queue<string>();
            _lock = new object();
            _lockRaw = new object();
            strImageTypes = new string[4] { ".bmp", ".jpg", ".cdb", ".png" };
            Thread thread = new Thread((ThreadStart)delegate
            {
                while (true)
                {
                    if (rawImageQueue.TryDequeue(out var result))
                    {
                        LogUtil.Log("存图队列原图 " + result.ImageName + " 图片出队列");
                        SaveRawImage(result.Path, result.Station, result.Info, result.ImageName, result.Image, result.ImageBuffer, result.mImageType, result.ThumbPercent);
                    }
                    if (rawImageQueue_RemoteDisk.TryDequeue(out var result2))
                    {
                        LogUtil.Log("公共盘存图队列图 " + result2.ImageName + " 图片出队列");
                        if (result2.DiskType == 1)
                        {
                            SaveRawImageFtp(result2.Path, result2.Station, result2.Info, result2.ImageName, result2.Image, result2.ImageBuffer, result2.mImageType, result2.UserName, result2.Pwd, result2.ThumbPercent);
                        }
                        else
                        {
                            SaveRawImage(result2.Path, result2.Station, result2.Info, result2.ImageName, result2.Image, result2.ImageBuffer, result2.mImageType, result2.ThumbPercent);
                        }
                    }
                    if (toolImageQueue.TryDequeue(out var result3))
                    {
                        LogUtil.Log("存图队列处理图 " + result3.ImageName + " 图片出队列");
                        SaveToolImage(result3.Path, result3.Station, result3.Info, result3.ImageName, result3.mImageType, result3.Image, result3.ImageBuffer, result3.ThumbPercent);
                    }
                    if (toolImageQueue_RemoteDisk.TryDequeue(out var result4))
                    {
                        LogUtil.Log("公共盘存结果图 " + result4.ImageName + " 图片出队列");
                        if (result4.DiskType == 1)
                        {
                            SaveToolImageFtp(result4.Path, result4.Station, result4.Info, result4.ImageName, result4.mImageType, result4.Image, result4.ImageBuffer, result4.ThumbPercent, result4.UserName, result4.Pwd);
                        }
                        else
                        {
                            SaveToolImage(result4.Path, result4.Station, result4.Info, result4.ImageName, result4.mImageType, result4.Image, result4.ImageBuffer, result4.ThumbPercent);
                        }
                    }
                    if (FlagDelete && rawImageQueue.Count == 0 && toolImageQueue.Count == 0 && rawImageQueue_RemoteDisk.Count == 0 && toolImageQueue_RemoteDisk.Count == 0)
                    {
                        if (DeleteQueue.Count > 0)
                        {
                            string path = DeleteQueue.Dequeue();
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                        }
                        else
                        {
                            if (++CountDelete > 1000)
                            {
                                FileOperator.DelEmptyDir(PathDelete, SaveDays, flag: true);
                                CountDelete = 0;
                            }
                            col = FileOperator.FileCollection(new List<string>(), PathDelete, SaveDays, 1000, flag: true);
                            for (int i = 0; i < col.Count; i++)
                            {
                                DeleteQueue.Enqueue(col[i]);
                            }
                        }
                    }
                }
            })
            {
                IsBackground = true
            };
            thread.Start();
        }

        public static void DispMessage(string str, int Rownum, int Colnum, float fontSize, CogRecordDisplay recordDisplay, CogColorConstants LabelColor)
        {
            CogGraphicLabel mylabel = new CogGraphicLabel();
            mylabel.SelectedSpaceName = "#";
            mylabel.Alignment = CogGraphicLabelAlignmentConstants.TopLeft;
            mylabel.Color = LabelColor;
            mylabel.BackgroundColor = CogColorConstants.Black;
            mylabel.Font = new Font("宋体", fontSize);
            mylabel.SetXYText(30 + Rownum * 100, 30 + Colnum * 100, str);
            CogGraphicCollection mygraphic = new CogGraphicCollection();
            mygraphic.Add(mylabel);
            recordDisplay.StaticGraphics.AddList(mygraphic, "groups");
            recordDisplay.Fit(graphicsToo: false);
        }

        public static void SaveToolImageSync(string path, string station, string info, string imageName, CogRecordDisplay mDisplay)
        {
            try
            {
                path = path + "\\" + DateTime.Now.ToString("yyyy年M月d日") + "\\" + station;
                string pathAfter = path + "\\" + info;
                lock (_lock)
                {
                    sp.Restart();
                    if (!Directory.Exists(pathAfter))
                    {
                        Directory.CreateDirectory(pathAfter);
                    }
                    string strImageName = pathAfter + "\\" + imageName + "_" + DateTime.Now.ToString("HH_mm_ss_ffff") + ".jpg";
                    Image mImage = mDisplay.CreateContentBitmap(CogDisplayContentBitmapConstants.Image, null, 0);
                    mImage.Save(strImageName);
                    sp.Stop();
                    LogUtil.Log($"{imageName} 处理图片保存成功！用时{sp.ElapsedMilliseconds}");
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError("处理图像保存失败" + ex.Message);
            }
        }

        public static void SaveToolImage(string path, string station, string info, string imageName, ImageType imageType, Image image, byte[] imageBuffer, int ThumbPercent)
        {
            try
            {
                path = path + "\\" + DateTime.Now.ToString("yyyy年M月d日") + "\\" + station;
                string pathAfter = path + "\\" + info;
                sp.Restart();
                if (!Directory.Exists(pathAfter))
                {
                    Directory.CreateDirectory(pathAfter);
                }
                string strImageName = pathAfter + "\\" + imageName + "_" + DateTime.Now.ToString("HH_mm_ss_ffff") + strImageTypes[(int)imageType];
                if (imageType == ImageType.bmp)
                {
                    Bitmap bitmap = (Bitmap)image;
                    Mat mat = bitmap.ToMat();
                    int height = ((ThumbPercent == 100) ? image.Height : ((int)((double)image.Height / 100.0 * (double)ThumbPercent)));
                    int width = ((ThumbPercent == 100) ? image.Width : ((int)((double)image.Width / 100.0 * (double)ThumbPercent)));
                    Mat matThumb;
                    if (ThumbPercent != 100)
                    {
                        OpenCvSharp.Size size = new OpenCvSharp.Size(width, height);
                        matThumb = new Mat(size, mat.Type());
                        Cv2.Resize(mat, matThumb, matThumb.Size());
                    }
                    else
                    {
                        matThumb = mat;
                    }
                    matThumb.ImWrite(strImageName);
                }
                else
                {
                    using FileStream fs = new FileStream(strImageName, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(imageBuffer, 0, imageBuffer.Length);
                }
                sp.Stop();
                LogUtil.Log($"{imageName} 处理图片保存成功！用时{sp.ElapsedMilliseconds}");
            }
            catch (Exception ex)
            {
                LogUtil.LogError("处理图像保存失败" + ex.Message);
            }
        }

        private static void SaveToolImageFtp(string path, string station, string info, string imageName, ImageType imageType, Image image, byte[] imageBuffer, int ThumbPercent, string userName, string pwd)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(pwd))
            {
                LogUtil.LogError("图上传到FTP保存失败，ftp服务器的用户名和密码未设置");
                return;
            }
            sp.Restart();
            List<string> str = new List<string>();
            str.Add(path.Replace('\\', '/'));
            str.Add(DateTime.Now.ToString("yyyyMMdd"));
            if (!string.IsNullOrWhiteSpace(station))
            {
                str.Add(station);
            }
            if (!string.IsNullOrWhiteSpace(info))
            {
                str.Add(info);
            }
            path = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + station;
            string pathAfter = path + "\\" + info;
            pathAfter = pathAfter.Replace('\\', '/');
            string ftppath = str[0];
            try
            {
                if (str != null && str.Count > 0)
                {
                    for (int j = 0; j < str.Count; j++)
                    {
                        if (!string.IsNullOrWhiteSpace(str[j]))
                        {
                            if (!DirectoryExists(ftppath, userName, pwd))
                            {
                                FtpWebRequest reqFTP2 = WebRequest.Create(new Uri(ftppath)) as FtpWebRequest;
                                reqFTP2.Credentials = new NetworkCredential(userName, pwd);
                                reqFTP2.Method = "MKD";
                                reqFTP2.UseBinary = true;
                                reqFTP2.UsePassive = true;
                                FtpWebResponse response2 = (FtpWebResponse)reqFTP2.GetResponse();
                                response2.Close();
                                reqFTP2.Abort();
                            }
                            if (j + 1 < str.Count)
                            {
                                ftppath = ftppath + "/" + str[j + 1];
                            }
                        }
                    }
                }
                string strImageName2 = pathAfter + "\\" + imageName + "_" + DateTime.Now.ToString("HH_mm_ss_ffff") + strImageTypes[(int)imageType];
                byte[] buffer2 = imageBuffer;
                if (imageType == ImageType.bmp)
                {
                    Bitmap bitmap2 = (Bitmap)image;
                    Mat mat2 = bitmap2.ToMat();
                    int height2 = ((ThumbPercent == 100) ? image.Height : ((int)((double)image.Height / 100.0 * (double)ThumbPercent)));
                    int width2 = ((ThumbPercent == 100) ? image.Width : ((int)((double)image.Width / 100.0 * (double)ThumbPercent)));
                    Mat matThumb2;
                    if (ThumbPercent != 100)
                    {
                        OpenCvSharp.Size size2 = new OpenCvSharp.Size(width2, height2);
                        matThumb2 = new Mat(size2, mat2.Type());
                        Cv2.Resize(mat2, matThumb2, matThumb2.Size());
                    }
                    else
                    {
                        matThumb2 = mat2;
                    }
                    buffer2 = matThumb2.ToBytes();
                }
                strImageName2 = strImageName2.Replace('\\', '/');
                FtpWebRequest request2 = WebRequest.Create(new Uri(strImageName2)) as FtpWebRequest;
                request2.Method = "STOR";
                request2.UseBinary = true;
                request2.UsePassive = true;
                request2.Credentials = new NetworkCredential(userName, pwd);
                using (new MemoryStream())
                {
                    Stream requestStream2 = request2.GetRequestStream();
                    requestStream2.Write(buffer2, 0, buffer2.Length);
                    requestStream2.Flush();
                    requestStream2.Close();
                }
                sp.Stop();
                LogUtil.Log($"{imageName} 处理图上传到FTP保存成功！用时{sp.ElapsedMilliseconds}");
            }
            catch (Exception ex)
            {
                try
                {
                    ftppath = str[0];
                    if (str != null && str.Count > 0)
                    {
                        for (int i = 0; i < str.Count; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(str[i]))
                            {
                                if (!DirectoryExists(ftppath, userName, pwd))
                                {
                                    FtpWebRequest reqFTP = WebRequest.Create(new Uri(ftppath)) as FtpWebRequest;
                                    reqFTP.Credentials = new NetworkCredential(userName, pwd);
                                    reqFTP.Method = "MKD";
                                    reqFTP.UseBinary = true;
                                    reqFTP.UsePassive = true;
                                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                                    response.Close();
                                    reqFTP.Abort();
                                }
                                if (i + 1 < str.Count)
                                {
                                    ftppath = ftppath + "/" + str[i + 1];
                                }
                            }
                        }
                    }
                    string strImageName = pathAfter + "\\" + imageName + "_" + DateTime.Now.ToString("HH_mm_ss_ffff") + strImageTypes[(int)imageType];
                    byte[] buffer = imageBuffer;
                    if (imageType == ImageType.bmp)
                    {
                        Bitmap bitmap = (Bitmap)image;
                        Mat mat = bitmap.ToMat();
                        int height = ((ThumbPercent == 100) ? image.Height : ((int)((double)image.Height / 100.0 * (double)ThumbPercent)));
                        int width = ((ThumbPercent == 100) ? image.Width : ((int)((double)image.Width / 100.0 * (double)ThumbPercent)));
                        Mat matThumb;
                        if (ThumbPercent != 100)
                        {
                            OpenCvSharp.Size size = new OpenCvSharp.Size(width, height);
                            matThumb = new Mat(size, mat.Type());
                            Cv2.Resize(mat, matThumb, matThumb.Size());
                        }
                        else
                        {
                            matThumb = mat;
                        }
                        buffer = matThumb.ToBytes();
                    }
                    strImageName = strImageName.Replace('\\', '/');
                    FtpWebRequest request = WebRequest.Create(new Uri(strImageName)) as FtpWebRequest;
                    request.Method = "STOR";
                    request.UseBinary = true;
                    request.UsePassive = true;
                    request.Credentials = new NetworkCredential(userName, pwd);
                    using (new MemoryStream())
                    {
                        Stream requestStream = request.GetRequestStream();
                        requestStream.Write(buffer, 0, buffer.Length);
                        requestStream.Flush();
                        requestStream.Close();
                    }
                    sp.Stop();
                    LogUtil.Log($"{imageName} 处理图重新上传到FTP保存成功！用时{sp.ElapsedMilliseconds}");
                }
                catch
                {
                    LogUtil.LogError("图上传到FTP保存失败，信息：" + ex.Message);
                }
            }
        }

        public static void SaveToolImageRemoteDiskAsync(string path, string station, string info, string imageName, Image image, string userName = null, string pwd = null, int thumbPercent = 100, int diskType = 0)
        {
            try
            {
                ToolImageInfo imageInfo = new ToolImageInfo
                {
                    Path = path,
                    Station = station,
                    Info = info,
                    ImageName = imageName,
                    ImageBuffer = null,
                    ThumbPercent = thumbPercent,
                    DiskType = diskType,
                    UserName = userName,
                    Pwd = pwd
                };
                Action<Image, ToolImageInfo> ToolImageProcessRun = delegate (Image _image, ToolImageInfo _imageInfo)
                {
                    Task.Run(delegate
                    {
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        _imageInfo.ImageBuffer = ToolImageProcess(_image, _imageInfo.ThumbPercent);
                        toolImageQueue_RemoteDisk.Enqueue(_imageInfo);
                        stopwatch.Stop();
                        LogUtil.Log($"{_imageInfo.ImageName} 转换网盘处理图片！用时{stopwatch.ElapsedMilliseconds}");
                    });
                };
                ToolImageProcessRun(image, imageInfo);
            }
            catch (Exception ex)
            {
                LogUtil.LogError("处理图像入队列失败" + ex.Message);
            }
        }

        public static void SaveToolImageAsync(string path, string station, string info, string imageName, ImageType imageType, Image image, int thumbPercent = 100)
        {
            try
            {
                ToolImageInfo imageInfo = new ToolImageInfo
                {
                    Path = path,
                    Station = station,
                    Info = info,
                    ImageName = imageName,
                    mImageType = imageType,
                    ImageBuffer = null,
                    ThumbPercent = thumbPercent
                };
                Action<Image, ToolImageInfo> ToolImageProcessRun = delegate (Image _image, ToolImageInfo _imageInfo)
                {
                    Task.Run(delegate
                    {
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        _imageInfo.ImageBuffer = ToolImageProcess(_image, _imageInfo.ThumbPercent);
                        toolImageQueue.Enqueue(_imageInfo);
                        stopwatch.Stop();
                        LogUtil.Log($"{_imageInfo.ImageName} 转换处理图片！用时{stopwatch.ElapsedMilliseconds}");
                    });
                };
                ToolImageProcessRun(image, imageInfo);
            }
            catch (Exception ex)
            {
                LogUtil.LogError("处理图像入队列失败" + ex.Message);
            }
        }

        public static void SaveToolImageAsync(ToolImageInfo imageInfo_Tool, ToolImageInfo imageInfo_ToolRemote)
        {
            try
            {
                if ((imageInfo_Tool != null && imageInfo_Tool.mImageType != ImageType.cdb && imageInfo_Tool.mImageType != 0) || (imageInfo_ToolRemote != null && imageInfo_ToolRemote.mImageType != ImageType.cdb && imageInfo_ToolRemote.mImageType != 0))
                {
                    Action<ToolImageInfo, ToolImageInfo> ToolImageProcessRun = delegate (ToolImageInfo _imageInfo_Tool, ToolImageInfo _imageInfo_ToolRemote)
                    {
                        Task.Run(delegate
                        {
                            Stopwatch stopwatch = new Stopwatch();
                            stopwatch.Start();
                            ToolImageProcess(_imageInfo_Tool, _imageInfo_ToolRemote);
                            if (_imageInfo_Tool != null)
                            {
                                toolImageQueue.Enqueue(_imageInfo_Tool);
                            }
                            if (_imageInfo_ToolRemote != null)
                            {
                                toolImageQueue_RemoteDisk.Enqueue(_imageInfo_ToolRemote);
                            }
                            stopwatch.Stop();
                            string arg = ((_imageInfo_Tool != null) ? _imageInfo_Tool.ImageName : _imageInfo_ToolRemote.ImageName);
                            LogUtil.Log($"{arg} 转换结果图！用时{stopwatch.ElapsedMilliseconds}");
                        });
                    };
                    ToolImageProcessRun(imageInfo_Tool, imageInfo_ToolRemote);
                }
                else
                {
                    if (imageInfo_Tool != null)
                    {
                        toolImageQueue.Enqueue(imageInfo_Tool);
                    }
                    if (imageInfo_ToolRemote != null)
                    {
                        toolImageQueue_RemoteDisk.Enqueue(imageInfo_ToolRemote);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError("结果图像入队列失败" + ex.Message);
            }
        }

        private static void ToolImageProcess(ToolImageInfo imageInfo_Tool = null, ToolImageInfo imageInfo_ToolRemote = null)
        {
            if (imageInfo_Tool == null && imageInfo_ToolRemote == null)
            {
                return;
            }
            Mat mat = null;
            if (imageInfo_Tool != null && imageInfo_Tool.mImageType != 0)
            {
                Image image = imageInfo_Tool.Image;
                Bitmap bitmap = new Bitmap(image);
                mat = bitmap.ToMat();
            }
            else if (imageInfo_ToolRemote != null && imageInfo_ToolRemote.mImageType != 0)
            {
                Image image2 = imageInfo_ToolRemote.Image;
                Bitmap bitmap2 = new Bitmap(image2);
                mat = bitmap2.ToMat();
            }
            if (imageInfo_Tool != null && imageInfo_ToolRemote != null)
            {
                if (imageInfo_Tool.ThumbPercent == imageInfo_ToolRemote.ThumbPercent && imageInfo_Tool.mImageType == imageInfo_ToolRemote.mImageType)
                {
                    ImageScale(imageInfo_Tool, mat);
                    if (imageInfo_ToolRemote.mImageType != ImageType.cdb && imageInfo_ToolRemote.mImageType != 0 && mat != null)
                    {
                        imageInfo_ToolRemote.ImageBuffer = imageInfo_Tool.ImageBuffer;
                        imageInfo_ToolRemote.Image = null;
                        mat.Dispose();
                    }
                }
                else if (imageInfo_Tool.ThumbPercent == imageInfo_ToolRemote.ThumbPercent && imageInfo_Tool.mImageType != imageInfo_ToolRemote.mImageType)
                {
                    ImageScale(imageInfo_Tool, mat);
                    ImageScale(imageInfo_ToolRemote, mat);
                    mat?.Dispose();
                }
                else
                {
                    ImageScale(imageInfo_Tool, mat);
                    ImageScale(imageInfo_ToolRemote, mat);
                    mat?.Dispose();
                }
            }
            else if (imageInfo_Tool != null)
            {
                ImageScale(imageInfo_Tool, mat);
                mat?.Dispose();
            }
            else if (imageInfo_ToolRemote != null)
            {
                ImageScale(imageInfo_ToolRemote, mat);
                mat?.Dispose();
            }
        }

        private static void ImageScale(ToolImageInfo imageInfo, Mat mat)
        {
            if (imageInfo.mImageType != ImageType.cdb && imageInfo.mImageType != 0 && mat != null)
            {
                int ThumbPercent = imageInfo.ThumbPercent;
                Image mImage = imageInfo.Image;
                int height = ((ThumbPercent == 100) ? mImage.Height : ((int)((double)mImage.Height / 100.0 * (double)ThumbPercent)));
                int width = ((ThumbPercent == 100) ? mImage.Width : ((int)((double)mImage.Width / 100.0 * (double)ThumbPercent)));
                Mat matThumb;
                if (ThumbPercent != 100)
                {
                    OpenCvSharp.Size size = new OpenCvSharp.Size(width, height);
                    matThumb = new Mat(size, mat.Type());
                    Cv2.Resize(mat, matThumb, matThumb.Size());
                }
                else
                {
                    matThumb = mat;
                }
                byte[] buffer = null;
                if (imageInfo.mImageType == ImageType.jpg)
                {
                    ImageEncodingParam imageEncodingParam = new ImageEncodingParam(ImwriteFlags.JpegQuality, 70);
                    Cv2.ImEncode(".jpg", matThumb, out buffer, imageEncodingParam);
                    imageInfo.ImageBuffer = buffer;
                    imageInfo.Image = null;
                }
                else if (imageInfo.mImageType == ImageType.png)
                {
                    Cv2.ImEncode(".png", matThumb, out buffer);
                    imageInfo.ImageBuffer = buffer;
                    imageInfo.Image = null;
                }
            }
        }

        private static byte[] ToolImageProcess(Image image, int thumbPercent)
        {
            int height = ((thumbPercent == 100) ? image.Height : ((int)((double)image.Height / 100.0 * (double)thumbPercent)));
            int width = ((thumbPercent == 100) ? image.Width : ((int)((double)image.Width / 100.0 * (double)thumbPercent)));
            Bitmap bitmap = new Bitmap(image);
            Mat mat = bitmap.ToMat();
            Mat matImage;
            if (thumbPercent != 100)
            {
                OpenCvSharp.Size size = new OpenCvSharp.Size(width, height);
                Mat matThumb = new Mat(size, mat.Type());
                Cv2.Resize(mat, matThumb, matThumb.Size());
                matImage = matThumb;
            }
            else
            {
                matImage = mat;
            }
            ImageEncodingParam imageEncodingParam = new ImageEncodingParam(ImwriteFlags.JpegQuality, 70);
            Cv2.ImEncode(".jpg", matImage, out var buffer, imageEncodingParam);
            matImage.Dispose();
            return buffer;
        }

        public static void SaveRawImageSync(string path, string station, string imageName, Cognex.VisionPro.ICogImage mImage, ImageType imageType)
        {
            try
            {
                path = path + "\\" + DateTime.Now.ToString("yyyy年M月d日") + "\\" + station;
                lock (_lockRaw)
                {
                    sp.Restart();
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string strImageName = path + "\\" + imageName + "_" + DateTime.Now.ToString("HH_mm_ss_ffff") + strImageTypes[(int)imageType];
                    CogImageFile imagefileWrite = new CogImageFile();
                    imagefileWrite.Open(strImageName, CogImageFileModeConstants.Write);
                    imagefileWrite.Append(mImage);
                    sp.Restart();
                    LogUtil.Log($"{imageName} 原图保存成功！用时{sp.ElapsedMilliseconds}");
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError("原始图像保存失败" + ex.Message);
            }
        }

        private static void SaveRawImage(string path, string station, string info, string imageName, Cognex.VisionPro.ICogImage mImage, byte[] imageBuffer, ImageType imageType, int ThumbPercent = 100)
        {
            try
            {
                sp.Restart();
                string thumbName = path + "\\" + DateTime.Now.ToString("yyyy年M月d日") + "\\" + station + "\\" + info;
                path = path + "\\" + DateTime.Now.ToString("yyyy年M月d日") + "\\" + station;
                string dtNow = DateTime.Now.ToString("HH_mm_ss_ffff");
                if (!Directory.Exists(thumbName))
                {
                    Directory.CreateDirectory(thumbName);
                }
                thumbName = thumbName + "\\" + imageName + "_" + dtNow + strImageTypes[(int)imageType];
                if (imageType != ImageType.cdb && imageType != 0)
                {
                    using FileStream fileStream = new FileStream(thumbName, FileMode.OpenOrCreate, FileAccess.Write);
                    fileStream.Write(imageBuffer, 0, imageBuffer.Length);
                }
                else if (imageType == ImageType.bmp)
                {
                    Mat mat = null;
                    Type type = mImage.GetType();
                    if (type.Name == "CogImage8Grey")
                    {
                        Cognex.VisionPro.ICogImage8PixelMemory pixelMemory = ((CogImage8Grey)mImage).Get8GreyPixelMemory(CogImageDataModeConstants.ReadWrite, 0, 0, mImage.Width, mImage.Height);
                        mat = new Mat(mImage.Height, mImage.Width, MatType.CV_8UC1, pixelMemory.Scan0, pixelMemory.Stride);
                    }
                    else if (type.Name == "CogImage24PlanarColor")
                    {
                        CogImage24PlanarColor colorImage = mImage as CogImage24PlanarColor;
                        Bitmap bitColor = colorImage.ToBitmap();
                        mat = bitColor.ToMat();
                        bitColor.Dispose();
                    }
                    if (mat != null)
                    {
                        int height = ((ThumbPercent == 100) ? mImage.Height : ((int)((double)mImage.Height / 100.0 * (double)ThumbPercent)));
                        int width = ((ThumbPercent == 100) ? mImage.Width : ((int)((double)mImage.Width / 100.0 * (double)ThumbPercent)));
                        Mat matThumb;
                        if (ThumbPercent != 100)
                        {
                            OpenCvSharp.Size size = new OpenCvSharp.Size(width, height);
                            matThumb = new Mat(size, mat.Type());
                            Cv2.Resize(mat, matThumb, matThumb.Size());
                        }
                        else
                        {
                            matThumb = mat;
                        }
                        matThumb.ImWrite(thumbName);
                    }
                }
                else
                {
                    CogImageFile imagefileWrite = new CogImageFile();
                    imagefileWrite.Open(thumbName, CogImageFileModeConstants.Write);
                    imagefileWrite.Append(mImage);
                }
                sp.Stop();
                LogUtil.Log($"{imageName} 原图保存成功！用时{sp.ElapsedMilliseconds}");
                string txtpath = path + "\\ImagePathRecord.txt";
                if (!File.Exists(txtpath))
                {
                    using FileStream fileStream2 = new FileStream(txtpath, FileMode.Create);
                    fileStream2.Close();
                }
                using FileStream fs = new FileStream(txtpath, FileMode.Append, FileAccess.Write);
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(thumbName);
                    sw.Flush();
                }
                fs.Close();
            }
            catch (Exception ex)
            {
                LogUtil.LogError("原始图像保存失败" + ex.Message);
            }
        }

        private static void SaveRawImageRemoteDisk(string path, string station, string info, string imageName, Cognex.VisionPro.ICogImage mImage, ImageType imageType, int ThumbPercent = 100)
        {
            try
            {
                sp.Restart();
                path = path + "\\" + DateTime.Now.ToString("yyyy年M月d日") + "\\" + station;
                string pathAfter = path + "\\" + info;
                if (!Directory.Exists(pathAfter))
                {
                    Directory.CreateDirectory(pathAfter);
                }
                string strImageName = pathAfter + "\\" + imageName + "_" + DateTime.Now.ToString("HH_mm_ss_ffff") + strImageTypes[(int)imageType];
                Type type = mImage.GetType();
                int height = ((ThumbPercent == 100) ? mImage.Height : ((int)((double)mImage.Height / 100.0 * (double)ThumbPercent)));
                int width = ((ThumbPercent == 100) ? mImage.Width : ((int)((double)mImage.Width / 100.0 * (double)ThumbPercent)));
                if (type.Name == "CogImage8Grey")
                {
                    Cognex.VisionPro.ICogImage8PixelMemory pixelMemory = ((CogImage8Grey)mImage).Get8GreyPixelMemory(CogImageDataModeConstants.ReadWrite, 0, 0, mImage.Width, mImage.Height);
                    Mat mat2 = new Mat(mImage.Height, mImage.Width, MatType.CV_8UC1, pixelMemory.Scan0, pixelMemory.Stride);
                    if (ThumbPercent != 100)
                    {
                        OpenCvSharp.Size size2 = new OpenCvSharp.Size(width, height);
                        Mat matThumb2 = new Mat(size2, mat2.Type());
                        Cv2.Resize(mat2, matThumb2, matThumb2.Size());
                        Cv2.ImWrite(strImageName, matThumb2);
                    }
                    else
                    {
                        Cv2.ImWrite(strImageName, mat2);
                    }
                    LogUtil.Log(imageName + " 原图上传到公共盘保存成功(OpenCv)！");
                }
                else if (type.Name == "CogImage24PlanarColor")
                {
                    CogImage24PlanarColor colorImage = mImage as CogImage24PlanarColor;
                    Bitmap bitColor = colorImage.ToBitmap();
                    Mat mat = bitColor.ToMat();
                    if (ThumbPercent != 100)
                    {
                        OpenCvSharp.Size size = new OpenCvSharp.Size(width, height);
                        Mat matThumb = new Mat(size, mat.Type());
                        Cv2.Resize(mat, matThumb, matThumb.Size());
                        Cv2.ImWrite(strImageName, matThumb);
                    }
                    else
                    {
                        Cv2.ImWrite(strImageName, mat);
                    }
                    LogUtil.Log(strImageName + " 图保存成功(OpenCv)！");
                    bitColor.Dispose();
                }
                else
                {
                    CogImageFile imagefileWrite = new CogImageFile();
                    imagefileWrite.Open(strImageName, CogImageFileModeConstants.Write);
                    imagefileWrite.Append(mImage);
                }
                if (type.Name == "CogImage16Range")
                {
                }
                sp.Stop();
                LogUtil.Log($"{imageName} 原图上传到公共盘保存成功！用时{sp.ElapsedMilliseconds}");
            }
            catch (Exception ex)
            {
                LogUtil.LogError("原始图像上传到公共盘保存失败" + ex.Message);
            }
        }

        private static void SaveRawImageFtp(string path, string station, string info, string imageName, Cognex.VisionPro.ICogImage mImage, byte[] imageBuffer, ImageType imageType, string userName, string pwd, int ThumbPercent = 100)
        {
            if (imageType == ImageType.cdb)
            {
                LogUtil.Log("图片" + imageName + "格式为cdb，不上传ftp服务器");
                return;
            }
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(pwd))
            {
                LogUtil.LogError("图上传到FTP保存失败，ftp服务器的用户名和密码未设置");
                return;
            }
            try
            {
                sp.Restart();
                List<string> str = new List<string>();
                str.Add(path.Replace("/\\Raw", "/Raw").Replace('\\', '/'));
                str.Add(DateTime.Now.ToString("yyyyMMdd"));
                if (!string.IsNullOrWhiteSpace(station))
                {
                    str.Add(station);
                }
                if (!string.IsNullOrWhiteSpace(info))
                {
                    str.Add(info);
                }
                path = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + station;
                string pathAfter = path + "\\" + info;
                pathAfter = pathAfter.Replace('\\', '/');
                string ftppath = str[0];
                if (str != null && str.Count > 0)
                {
                    for (int i = 0; i < str.Count; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(str[i]))
                        {
                            if (!DirectoryExists(ftppath, userName, pwd))
                            {
                                FtpWebRequest reqFTP = WebRequest.Create(new Uri(ftppath)) as FtpWebRequest;
                                reqFTP.Credentials = new NetworkCredential(userName, pwd);
                                reqFTP.Method = "MKD";
                                reqFTP.UseBinary = true;
                                reqFTP.UsePassive = true;
                                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                                response.Close();
                                reqFTP.Abort();
                            }
                            if (i + 1 < str.Count)
                            {
                                ftppath = ftppath + "/" + str[i + 1];
                            }
                        }
                    }
                }
                string strImageName = pathAfter + "\\" + imageName + "_" + DateTime.Now.ToString("HH_mm_ss_ffff") + strImageTypes[(int)imageType];
                byte[] buffer = imageBuffer;
                if (imageType == ImageType.bmp)
                {
                    Mat mat = null;
                    Type type = mImage.GetType();
                    if (type.Name == "CogImage8Grey")
                    {
                        Cognex.VisionPro.ICogImage8PixelMemory pixelMemory = ((CogImage8Grey)mImage).Get8GreyPixelMemory(CogImageDataModeConstants.ReadWrite, 0, 0, mImage.Width, mImage.Height);
                        mat = new Mat(mImage.Height, mImage.Width, MatType.CV_8UC1, pixelMemory.Scan0, pixelMemory.Stride);
                    }
                    else if (type.Name == "CogImage24PlanarColor")
                    {
                        CogImage24PlanarColor colorImage = mImage as CogImage24PlanarColor;
                        Bitmap bitColor = colorImage.ToBitmap();
                        mat = bitColor.ToMat();
                        bitColor.Dispose();
                    }
                    if (mat != null)
                    {
                        int height = ((ThumbPercent == 100) ? mImage.Height : ((int)((double)mImage.Height / 100.0 * (double)ThumbPercent)));
                        int width = ((ThumbPercent == 100) ? mImage.Width : ((int)((double)mImage.Width / 100.0 * (double)ThumbPercent)));
                        Mat matThumb;
                        if (ThumbPercent != 100)
                        {
                            OpenCvSharp.Size size = new OpenCvSharp.Size(width, height);
                            matThumb = new Mat(size, mat.Type());
                            Cv2.Resize(mat, matThumb, matThumb.Size());
                        }
                        else
                        {
                            matThumb = mat;
                        }
                        buffer = matThumb.ToBytes();
                    }
                }
                strImageName = strImageName.Replace('\\', '/');
                FtpWebRequest request = WebRequest.Create(new Uri(strImageName)) as FtpWebRequest;
                request.Method = "STOR";
                request.UseBinary = true;
                request.UsePassive = true;
                request.Credentials = new NetworkCredential(userName, pwd);
                using (new MemoryStream())
                {
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(buffer, 0, buffer.Length);
                    requestStream.Flush();
                    requestStream.Close();
                }
                sp.Stop();
                LogUtil.Log($"{imageName} 原图上传到FTP保存成功！用时{sp.ElapsedMilliseconds}");
            }
            catch (Exception ex)
            {
                LogUtil.LogError("图上传到FTP保存失败，信息：" + ex.Message);
            }
        }

        private static bool DirectoryExists(string url, string userName, string pwd)
        {
            bool exists = true;
            try
            {
                string file = "directoryexists.test";
                string path = url + "/" + file;
                FtpWebRequest req = (FtpWebRequest)WebRequest.Create(path);
                req.ConnectionGroupName = "conngroup1";
                req.Method = "STOR";
                req.Credentials = new NetworkCredential(userName, pwd);
                req.Timeout = 10000;
                byte[] fileContents = Encoding.Unicode.GetBytes("SAFE TO DELETE");
                req.ContentLength = fileContents.Length;
                Stream s = req.GetRequestStream();
                s.Write(fileContents, 0, fileContents.Length);
                s.Close();
                req = (FtpWebRequest)WebRequest.Create(path);
                req.ConnectionGroupName = "conngroup1";
                req.Method = "DELE";
                req.Credentials = new NetworkCredential(userName, pwd);
                req.Timeout = 10000;
                FtpWebResponse res = (FtpWebResponse)req.GetResponse();
                res.Close();
            }
            catch (WebException)
            {
                exists = false;
            }
            return exists;
        }

        public static void SaveRawImageAsync(string path, string station, string info, string imageName, Cognex.VisionPro.ICogImage mImage, ImageType imageType, int thumbPercent = 100)
        {
            try
            {
                rawImageQueue.Enqueue(new RawImageInfo
                {
                    Path = path,
                    Station = station,
                    Info = info,
                    ImageName = imageName,
                    mImageType = imageType,
                    Image = mImage,
                    ThumbPercent = thumbPercent
                });
            }
            catch (Exception ex)
            {
                LogUtil.LogError("原始图像入队列失败" + ex.Message);
            }
        }

        public static void SaveRawImageRemoteDiskAsync(string path, string station, string info, string imageName, Cognex.VisionPro.ICogImage mImage, ImageType imageType, string userName = null, string pwd = null, int thumbPercent = 100, int diskType = 0)
        {
            try
            {
                rawImageQueue_RemoteDisk.Enqueue(new RawImageInfo
                {
                    Path = path,
                    Station = station,
                    Info = info,
                    ImageName = imageName,
                    mImageType = imageType,
                    Image = mImage,
                    ThumbPercent = thumbPercent,
                    DiskType = diskType,
                    UserName = userName,
                    Pwd = pwd
                });
            }
            catch (Exception ex)
            {
                LogUtil.LogError("原始图像入公共盘图像队列失败" + ex.Message);
            }
        }

        public static void SaveRawImageAsync(RawImageInfo imageInfo_Raw = null, RawImageInfo imageInfo_RawRemote = null)
        {
            try
            {
                if ((imageInfo_Raw != null && imageInfo_Raw.mImageType != ImageType.cdb && imageInfo_Raw.mImageType != 0) || (imageInfo_RawRemote != null && imageInfo_RawRemote.mImageType != ImageType.cdb && imageInfo_RawRemote.mImageType != 0))
                {
                    Action<RawImageInfo, RawImageInfo> RawImageProcessRun = delegate (RawImageInfo _imageInfo_Raw, RawImageInfo _imageInfo_RawRemote)
                    {
                        Task.Run(delegate
                        {
                            Stopwatch stopwatch = new Stopwatch();
                            stopwatch.Start();
                            RawImageProcess(_imageInfo_Raw, _imageInfo_RawRemote);
                            if (_imageInfo_Raw != null)
                            {
                                rawImageQueue.Enqueue(_imageInfo_Raw);
                            }
                            if (_imageInfo_RawRemote != null)
                            {
                                rawImageQueue_RemoteDisk.Enqueue(_imageInfo_RawRemote);
                            }
                            stopwatch.Stop();
                            string arg = ((_imageInfo_Raw != null) ? _imageInfo_Raw.ImageName : _imageInfo_RawRemote.ImageName);
                            LogUtil.Log($"{arg} 转换原图！用时{stopwatch.ElapsedMilliseconds}");
                        });
                    };
                    RawImageProcessRun(imageInfo_Raw, imageInfo_RawRemote);
                }
                else
                {
                    if (imageInfo_Raw != null)
                    {
                        rawImageQueue.Enqueue(imageInfo_Raw);
                    }
                    if (imageInfo_RawRemote != null)
                    {
                        rawImageQueue_RemoteDisk.Enqueue(imageInfo_RawRemote);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError("原始图像入队列失败" + ex.Message);
            }
        }

        private static void RawImageProcess(RawImageInfo imageInfo_Raw = null, RawImageInfo imageInfo_RawRemote = null)
        {
            if (imageInfo_Raw == null && imageInfo_RawRemote == null)
            {
                return;
            }
            Mat mat = null;
            if (imageInfo_Raw != null && imageInfo_Raw.mImageType != 0)
            {
                Cognex.VisionPro.ICogImage cogImage = imageInfo_Raw.Image;
                Type type = cogImage.GetType();
                if (type.Name == "CogImage8Grey")
                {
                    Cognex.VisionPro.ICogImage8PixelMemory pixelMemory = ((CogImage8Grey)cogImage).Get8GreyPixelMemory(CogImageDataModeConstants.ReadWrite, 0, 0, cogImage.Width, cogImage.Height);
                    mat = new Mat(cogImage.Height, cogImage.Width, MatType.CV_8UC1, pixelMemory.Scan0, pixelMemory.Stride);
                }
                else if (type.Name == "CogImage24PlanarColor")
                {
                    CogImage24PlanarColor colorImage2 = cogImage as CogImage24PlanarColor;
                    Bitmap bitColor2 = colorImage2.ToBitmap();
                    mat = bitColor2.ToMat();
                    bitColor2.Dispose();
                }
            }
            else if (imageInfo_RawRemote != null && imageInfo_RawRemote.mImageType != 0)
            {
                Cognex.VisionPro.ICogImage cogImage2 = imageInfo_RawRemote.Image;
                Type type2 = cogImage2.GetType();
                if (type2.Name == "CogImage8Grey")
                {
                    Cognex.VisionPro.ICogImage8PixelMemory pixelMemory2 = ((CogImage8Grey)cogImage2).Get8GreyPixelMemory(CogImageDataModeConstants.ReadWrite, 0, 0, cogImage2.Width, cogImage2.Height);
                    mat = new Mat(cogImage2.Height, cogImage2.Width, MatType.CV_8UC1, pixelMemory2.Scan0, pixelMemory2.Stride);
                }
                else if (type2.Name == "CogImage24PlanarColor")
                {
                    CogImage24PlanarColor colorImage = cogImage2 as CogImage24PlanarColor;
                    Bitmap bitColor = colorImage.ToBitmap();
                    mat = bitColor.ToMat();
                    bitColor.Dispose();
                }
            }
            if (imageInfo_Raw != null && imageInfo_RawRemote != null)
            {
                if (imageInfo_Raw.ThumbPercent == imageInfo_RawRemote.ThumbPercent && imageInfo_Raw.mImageType == imageInfo_RawRemote.mImageType)
                {
                    ImageScale(imageInfo_Raw, mat);
                    if (imageInfo_RawRemote.mImageType != ImageType.cdb && imageInfo_RawRemote.mImageType != 0 && mat != null)
                    {
                        imageInfo_RawRemote.ImageBuffer = imageInfo_Raw.ImageBuffer;
                        imageInfo_RawRemote.Image = null;
                        mat.Dispose();
                    }
                }
                else if (imageInfo_Raw.ThumbPercent == imageInfo_RawRemote.ThumbPercent && imageInfo_Raw.mImageType != imageInfo_RawRemote.mImageType)
                {
                    ImageScale(imageInfo_Raw, mat);
                    ImageScale(imageInfo_RawRemote, mat);
                    mat?.Dispose();
                }
                else
                {
                    ImageScale(imageInfo_Raw, mat);
                    ImageScale(imageInfo_RawRemote, mat);
                    mat?.Dispose();
                }
            }
            else if (imageInfo_Raw != null)
            {
                ImageScale(imageInfo_Raw, mat);
                mat?.Dispose();
            }
            else if (imageInfo_RawRemote != null)
            {
                ImageScale(imageInfo_RawRemote, mat);
                mat?.Dispose();
            }
        }

        private static void ImageScale(RawImageInfo imageInfo, Mat mat)
        {
            if (imageInfo.mImageType != ImageType.cdb && imageInfo.mImageType != 0 && mat != null)
            {
                int ThumbPercent = imageInfo.ThumbPercent;
                Cognex.VisionPro.ICogImage mImage = imageInfo.Image;
                int height = ((ThumbPercent == 100) ? mImage.Height : ((int)((double)mImage.Height / 100.0 * (double)ThumbPercent)));
                int width = ((ThumbPercent == 100) ? mImage.Width : ((int)((double)mImage.Width / 100.0 * (double)ThumbPercent)));
                Mat matThumb;
                if (ThumbPercent != 100)
                {
                    OpenCvSharp.Size size = new OpenCvSharp.Size(width, height);
                    matThumb = new Mat(size, mat.Type());
                    Cv2.Resize(mat, matThumb, matThumb.Size());
                }
                else
                {
                    matThumb = mat;
                }
                byte[] buffer = null;
                if (imageInfo.mImageType == ImageType.jpg)
                {
                    ImageEncodingParam imageEncodingParam = new ImageEncodingParam(ImwriteFlags.JpegQuality, 100);
                    Cv2.ImEncode(".jpg", matThumb, out buffer, imageEncodingParam);
                    imageInfo.ImageBuffer = buffer;
                    imageInfo.Image = null;
                }
                else if (imageInfo.mImageType == ImageType.png)
                {
                    Cv2.ImEncode(".png", matThumb, out buffer);
                    imageInfo.ImageBuffer = buffer;
                    imageInfo.Image = null;
                }
            }
        }

        public static void Display_Clear(CogRecordDisplay RecordDisplay, bool ClearImage)
        {
            if (ClearImage)
            {
                RecordDisplay.Image = null;
            }
            RecordDisplay.StaticGraphics.Clear();
            RecordDisplay.InteractiveGraphics.Clear();
        }
    }
}
