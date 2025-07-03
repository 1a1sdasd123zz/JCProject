using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Vision.BaseClass
{
    public class BaseImageProcess
    {


        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        /// <summary>
        /// 压缩图片
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        /// <param name="quality"></param>// 压缩质量范围从0(最差，最小)到100，其中75是默认质量
        public static void CompressImage(string sourcePath, string destinationPath, long quality)
        {
            using (Image image = Image.FromFile(sourcePath))
            {
                // 设置压缩质量
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                EncoderParameters encoderParameters = new EncoderParameters(1);
                EncoderParameter encoderParameter = new EncoderParameter(Encoder.Quality, quality);
                encoderParameters.Param[0] = encoderParameter;

                // 保存压缩后的图片
                image.Save(destinationPath, jpgEncoder, encoderParameters);
            }
        }
        public static Image CompressImage(Image image, long quality, ImageFormat format)
        {
            Image compressedImage;
            using (var outputGraphics = Graphics.FromImage(image))
            {
                // 设置高质量的渲染compression
                outputGraphics.CompositingQuality = CompositingQuality.HighQuality;
                outputGraphics.SmoothingMode = SmoothingMode.HighQuality;
                outputGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // 创建压缩图像的副本
                compressedImage = new Bitmap(image.Width, image.Height);
                using (var compressedGraphics = Graphics.FromImage(compressedImage))
                {
                    // 将原图像绘制到新的Bitmap上，同时应用压缩设置
                    compressedGraphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                                                  new Rectangle(0, 0, image.Width, image.Height),
                                                  GraphicsUnit.Pixel);
                }
            }

            // 将压缩后的图像保存到流中
            using (var memoryStream = new MemoryStream())
            {
                compressedImage.Save(memoryStream, format);
                // 设置压缩质量参数（如果适用）
                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);

                // 获取图像编码解码器
                var codecInfo = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == format.Guid);

                // 重新保存图像，并应用压缩质量
                memoryStream.Seek(0, SeekOrigin.Begin);
                compressedImage = Image.FromStream(memoryStream);
                using (var outputStream = new MemoryStream())
                {
                    compressedImage.Save(outputStream, codecInfo, encoderParameters);
                    // 返回压缩后的图像
                    return Image.FromStream(new MemoryStream(outputStream.ToArray()));
                }
            }
        }

        /// <summary>
        /// 压缩bitmap
        /// </summary>
        /// <param name="originalBitmap"></param>原图
        /// <param name="quality"></param>压缩质量
        public static Bitmap CompressImage(Bitmap originalBitmap,long quality)
        {

            // 设置压缩质量
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Png);
            EncoderParameters encoderParameters = new EncoderParameters(1);
            EncoderParameter encoderParameter = new EncoderParameter(Encoder.Quality, quality);
            encoderParameters.Param[0] = encoderParameter;


            using (MemoryStream memoryStream = new MemoryStream())
            {
                // 将原始Bitmap保存到内存流中，使用JPEG压缩
                originalBitmap.Save(memoryStream, jpgEncoder, encoderParameters);

                // 重新加载内存流中的图像为新的Bitmap对象
                memoryStream.Position = 0;
                Bitmap compressedBitmap = new Bitmap(memoryStream);

                // 返回压缩后的Bitmap
                return compressedBitmap;
            }
        }

        }
    

    public class CognexImageProcess
    {
        //public static 
    }
}
