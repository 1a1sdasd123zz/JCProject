using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Cognex.VisionPro;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro3D;
using HalconDotNet;
using MvCamCtrl.NET;

namespace Vision.Hardware
{
    public class ImageData
    {
        private int sizeX;

        private int sizeY;

        private byte[] buffer;

        private Cognex.VisionPro.ICogImage _cogImage;

        private HObject hObject;

        public int SizeX
        {
            get
            {
                return sizeX;
            }
            set
            {
                sizeX = value;
            }
        }

        public int SizeY
        {
            get
            {
                return sizeY;
            }
            set
            {
                sizeY = value;
            }
        }

        public byte[] Buffer
        {
            get
            {
                return buffer;
            }
            set
            {
                buffer = value;
            }
        }

        public Cognex.VisionPro.ICogImage CogImage
        {
            get
            {
                return _cogImage;
            }
            set
            {
                _cogImage = value;
            }
        }

        public ImageData()
        {
        }

        public ImageData(Cognex.VisionPro.ICogImage externCogImage)
        {
            CogImage = externCogImage;
        }

        public ImageData(HObject externHObject)
        {
            hObject = externHObject;
        }

        public ImageData(int externSizeX, int externSizeY, byte[] externBuffer)
        {
            sizeX = externSizeX;
            sizeY = externSizeY;
            buffer = externBuffer;
        }

        public static Cognex.VisionPro.ICogImage GetMonoImage(int nHeight, int nWidth, IntPtr pImageBuf)
        {
            try
            {
                CogImage8Root cogImage8Root = new CogImage8Root();
                cogImage8Root.Initialize(nWidth, nHeight, pImageBuf, nWidth, null);
                CogImage8Grey cogImage8Grey = new CogImage8Grey();
                cogImage8Grey.SetRoot(cogImage8Root);
                return cogImage8Grey.ScaleImage(nWidth, nHeight);
            }
            catch
            {
                return null;
            }
        }

        public static Cognex.VisionPro.ICogImage GetOutputImage(Bitmap bitmap)
        {
            try
            {
                return new CogImage8Grey(bitmap);
            }
            catch
            {
                return null;
            }
        }

        public static Cognex.VisionPro.ICogImage GetOutputRGBImage(Bitmap bitmap)
        {
            try
            {
                return new CogImage24PlanarColor(bitmap);
            }
            catch
            {
                return null;
            }
        }

        public static Cognex.VisionPro.ICogImage GetOutputImage(uint nHeight, uint nWidth, IntPtr pImageBuf, MvGvspPixelType enPixelType)
        {
            Cognex.VisionPro.ICogImage tmpImage;
            try
            {
                if (enPixelType == MvGvspPixelType.PixelType_Gvsp_Mono8)
                {
                    CogImage8Root cogImage8Root = new CogImage8Root();
                    cogImage8Root.Initialize((int)nWidth, (int)nHeight, pImageBuf, (int)nWidth, null);
                    CogImage8Grey cogImage8Grey = new CogImage8Grey();
                    cogImage8Grey.SetRoot(cogImage8Root);
                    tmpImage = cogImage8Grey.ScaleImage((int)nWidth, (int)nHeight);
                    GC.Collect();
                }
                else
                {
                    uint m_nRowStep = nWidth * nHeight;
                    CogImage8Root image0 = new CogImage8Root();
                    IntPtr ptr0 = new IntPtr(pImageBuf.ToInt64());
                    image0.Initialize((int)nWidth, (int)nHeight, ptr0, (int)nWidth, null);
                    CogImage8Root image1 = new CogImage8Root();
                    IntPtr ptr1 = new IntPtr(pImageBuf.ToInt64() + m_nRowStep);
                    image1.Initialize((int)nWidth, (int)nHeight, ptr1, (int)nWidth, null);
                    CogImage8Root image2 = new CogImage8Root();
                    IntPtr ptr2 = new IntPtr(pImageBuf.ToInt64() + m_nRowStep * 2);
                    image2.Initialize((int)nWidth, (int)nHeight, ptr2, (int)nWidth, null);
                    CogImage24PlanarColor colorImage = new CogImage24PlanarColor();
                    colorImage.SetRoots(image0, image1, image2);
                    tmpImage = colorImage.ScaleImage((int)nWidth, (int)nHeight);
                    GC.Collect();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return tmpImage;
        }

        public static Bitmap BytesToBitmap(byte[] data, int nHeight, int nWidth)
        {
            Bitmap bmp = new Bitmap(nWidth, nHeight, PixelFormat.Format8bppIndexed);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, nWidth, nHeight), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            int stride = bmpData.Stride;
            int offset = stride - nWidth;
            IntPtr iptr = bmpData.Scan0;
            int scanBytes = stride * nHeight;
            int posScan = 0;
            int posReal = 0;
            byte[] pixelValues = new byte[scanBytes];
            for (int x = 0; x < nHeight; x++)
            {
                for (int y = 0; y < nWidth; y++)
                {
                    pixelValues[posScan++] = data[posReal++];
                }
                posScan += offset;
            }
            Marshal.Copy(pixelValues, 0, iptr, scanBytes);
            bmp.UnlockBits(bmpData);
            ColorPalette tempPalette;
            using (Bitmap tempBmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                tempPalette = tempBmp.Palette;
            }
            for (int i = 0; i < 256; i++)
            {
                tempPalette.Entries[i] = Color.FromArgb(i, i, i);
            }
            bmp.Palette = tempPalette;
            return bmp;
        }

        public static Cognex.VisionPro.ICogImage GetRGBImage(int nHeight, int nWidth, IntPtr pImageBuf)
        {
            try
            {
                uint m_nRowStep = (uint)(nWidth * nHeight);
                CogImage8Root image0 = new CogImage8Root();
                IntPtr ptr0 = new IntPtr(pImageBuf.ToInt64());
                image0.Initialize(nWidth, nHeight, ptr0, nWidth, null);
                CogImage8Root image1 = new CogImage8Root();
                IntPtr ptr1 = new IntPtr(pImageBuf.ToInt64() + m_nRowStep);
                image1.Initialize(nWidth, nHeight, ptr1, nWidth, null);
                CogImage8Root image2 = new CogImage8Root();
                IntPtr ptr2 = new IntPtr(pImageBuf.ToInt64() + m_nRowStep * 2);
                image2.Initialize(nWidth, nHeight, ptr2, nWidth, null);
                CogImage24PlanarColor colorImage = new CogImage24PlanarColor();
                colorImage.SetRoots(image0, image1, image2);
                return colorImage.ScaleImage(nWidth, nHeight);
            }
            catch
            {
                return null;
            }
        }

        public static void ConvertToRangeImage(DataContext context, out CogImage16Range RangeImageConverted, ref CogImage16Grey HeightImage)
        {
            Cog3DMatrix3x3 tm = new Cog3DMatrix3x3(1.0 / context.xResolution, 0.0, 0.0, 0.0, 1.0 / context.yResolution, 0.0, 0.0, 0.0, 1.0 / context.zResolution);
            Cog3DVect3 tv = new Cog3DVect3(context.xOffset, context.yOffset, context.zOffset);
            Cog3DTransformLinear tf = new Cog3DTransformLinear(tm, tv);
            RangeImageConverted = new CogImage16Range(HeightImage, 0, tf);
        }

        public static void ConvertToRangeImageWithGrey(DataContext context, out CogImage16Range RangeImageConverted, ref CogImage16Grey HeightImage, ref CogImage16Grey LuminanceImage)
        {
            Cog3DMatrix3x3 tm = new Cog3DMatrix3x3(1.0 / context.xResolution, 0.0, 0.0, 0.0, 1.0 / context.yResolution, 0.0, 0.0, 0.0, 1.0 / context.zResolution);
            Cog3DVect3 tv = new Cog3DVect3(context.xOffset, context.yOffset, context.zOffset);
            Cog3DTransformLinear tf = new Cog3DTransformLinear(tm, tv);
            CogImage16Grey withGreyImage = new CogImage16Grey(HeightImage.Width * 2, HeightImage.Height);
            CogCopyRegion ccr = new CogCopyRegion();
            ccr.ImageAlignmentEnabled = true;
            ccr.DestinationImageAlignmentX = 0.0;
            ccr.DestinationImageAlignmentY = 0.0;
            ccr.Execute(HeightImage, null, withGreyImage, out var sourceClipped, out var destinationClipped, out var destinationRegion);
            ccr.DestinationImageAlignmentX = HeightImage.Width;
            ccr.DestinationImageAlignmentY = 0.0;
            ccr.Execute(LuminanceImage, null, withGreyImage, out sourceClipped, out destinationClipped, out destinationRegion);
            RangeImageConverted = new CogImage16Range(withGreyImage, 0, tf);
        }

        //public static CogImage16Range Keyence3DTransformToRange(DataContext context, int _xImageSize, int _yImageSize, SafeBufferExt _bufHeight, SafeBufferExt _bufLuminance, RangeImageFormatEnum formatEnum)
        //{
        //    CogImage16Root cogRootHeight = new CogImage16Root();
        //    CogImage16Root cogRootLumi = new CogImage16Root();
        //    CogImage16Grey HeightImage = new CogImage16Grey();
        //    CogImage16Grey LuminanceImage = new CogImage16Grey();
        //    CogImage16Range RangeImageConverted = new CogImage16Range();
        //    switch (formatEnum)
        //    {
        //        case RangeImageFormatEnum.rangeH:
        //            {
        //                cogRootHeight.Initialize(_xImageSize, _yImageSize, _bufHeight, _xImageSize, _bufHeight);
        //                HeightImage.SetRoot(cogRootHeight);
        //                CogImage16Grey heightImage = HeightImage.Copy();
        //                context.xOffset = HeightImage.Width / 2;
        //                context.yOffset = HeightImage.Height / 2;
        //                ConvertToRangeImage(context, out var RangeImageConvertedH, ref heightImage);
        //                RangeImageConverted = RangeImageConvertedH;
        //                break;
        //            }
        //        case RangeImageFormatEnum.rangeHL:
        //            {
        //                cogRootHeight.Initialize(_xImageSize, _yImageSize, _bufHeight, _xImageSize, _bufHeight);
        //                cogRootLumi.Initialize(_xImageSize, _yImageSize, _bufLuminance, _xImageSize, _bufLuminance);
        //                HeightImage.SetRoot(cogRootHeight);
        //                LuminanceImage.SetRoot(cogRootLumi);
        //                CogImage16Grey heightImage2 = HeightImage.Copy();
        //                CogImage16Grey luminanceImage2 = LuminanceImage.Copy();
        //                context.xOffset = HeightImage.Width / 2;
        //                context.yOffset = HeightImage.Height / 2;
        //                ConvertToRangeImageWithGrey(context, out var RangeImageConvertedHL, ref heightImage2, ref luminanceImage2);
        //                RangeImageConverted = RangeImageConvertedHL;
        //                break;
        //            }
        //    }
        //    return RangeImageConverted;
        //}

        //public static CogImage16Range Lmi3DTransformToRange(DataContext context, SafeBufferExt _buffer, int width, int height)
        //{
        //    CogImage16Root cogRootHeight = new CogImage16Root();
        //    CogImage16Grey HeightImage = new CogImage16Grey();
        //    cogRootHeight.Initialize(width, height, _buffer, width, _buffer);
        //    HeightImage.SetRoot(cogRootHeight);
        //    CogImage16Grey heightImage = (CogImage16Grey)HeightImage.ScaleImage(width, height);
        //    context.xOffset = HeightImage.Width / 2;
        //    context.yOffset = HeightImage.Height / 2;
        //    ConvertToRangeImage(context, out var RangeImageConvertedH, ref heightImage);
        //    return RangeImageConvertedH;
        //}

        [DllImport("kernel32.dll")]
        public static extern void CopyMemory(IntPtr destination, IntPtr source, UIntPtr length);

        public static void SaveRawImage(Cognex.VisionPro.ICogImage image, string imageName, string path, PicFormat picFormat)
        {
            CogImageFile imageFile = new CogImageFile();
            try
            {
                string fileName = path + "\\" + imageName + "." + picFormat;
                imageFile.Open(fileName, CogImageFileModeConstants.Write);
                imageFile.Append(image);
                imageFile.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("存图失败****" + e.Message);
            }
        }
    }
}
