using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;
using ZXing;
using ZXing.Common;
using ZXing.QrCode.Internal;

namespace TianYu.Core.Common
{
    /// <summary>
    /// 二维码帮助类
    /// </summary>
    public class QrCodeHelper
    {
        /// <summary>
        /// 合并Logo图片
        /// </summary>
        /// <param name="img">原图</param>
        /// <param name="logo">logo</param>
        /// <param name="rectangle">矩形</param>
        /// <returns></returns>
        private static Bitmap MergeLogo(Bitmap img, Bitmap logo, int[] rectangle)
        {
            //计算插入图片的大小和位置
            int middleW = Math.Min((int)(rectangle[2] / 4.5), logo.Width);
            int middleH = Math.Min((int)(rectangle[3] / 4.5), logo.Height);
            int middleL = (img.Width - middleW) / 2;
            int middleT = (img.Height - middleH) / 2;

            //将二维码插入图片
            Bitmap bmpimg = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmpimg))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(img, 0, 0);
            }
            Graphics myGraphic = Graphics.FromImage(bmpimg);
            //白底
            int bgSize = 8;
            int bgXy = bgSize / 2;
            myGraphic.FillRectangle(Brushes.White, middleL - bgXy, middleT - bgXy, middleW + bgSize, middleH + bgSize);
            myGraphic.DrawImage(logo, middleL, middleT, middleW, middleH);

            return bmpimg;
        }
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="content">二维码内容</param>
        /// <param name="saveFileName">二维码保存路径</param>
        /// <param name="logoImgPath">logo图片路径</param>
        /// <returns>位图</returns>
        public static Bitmap Encoder(string content, string saveFileName = "", string logoImgPath = "")
        {
            return Encoder(content, 320, 320, saveFileName, logoImgPath);
        }
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="content">二维码内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="saveFileName">二维码保存路径</param>
        /// <param name="logoImgPath">logo图片路径</param>
        /// <returns>位图</returns>
        public static Bitmap Encoder(string content, int width, int height, string saveFileName = "", string logoImgPath = "")
        {
            Bitmap qrCodeImage = null;
            BarcodeWriter barCodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE
            };
            barCodeWriter.Options.Hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
            barCodeWriter.Options.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.M);
            barCodeWriter.Options.Height = height;
            barCodeWriter.Options.Width = width;
            barCodeWriter.Options.Margin = 0;
            BitMatrix bm = barCodeWriter.Encode(content);
            qrCodeImage = barCodeWriter.Write(bm);

            //Logo
            Bitmap logoImg = null;
            if (!logoImgPath.IsNullOrWhiteSpace())
            {
                logoImg = new Bitmap(logoImgPath);

                qrCodeImage = MergeLogo(qrCodeImage, logoImg, bm.getEnclosingRectangle());
            }

            if (!saveFileName.IsNullOrWhiteSpace())
            {
                qrCodeImage.Save(saveFileName, ImageFormat.Jpeg);
                qrCodeImage.Dispose();
                if (logoImg != null)
                {
                    logoImg.Dispose();
                }
            }
            return qrCodeImage;
        }
         
        /// <summary>
        /// 红包分享图片
        /// </summary>
        /// <param name="Type">类型（1=APP，2=小程序）</param>
        /// <param name="QrContent">二维码内容</param>
        /// <param name="BackImg">背景图</param>
        /// <param name="SaveFilePath">保存地址</param>
        /// <param name="UserName">用户名</param>
        /// <param name="UserImg">用户头像</param>
        /// <returns></returns>
        public static Bitmap EncoderShareImg(int Type, string QrContent, string BackImg, string SaveFilePath = "", string UserName = "", string UserImg = "")
        {
            Bitmap bmp = null;
            if (Type == 1)
            {
                byte[] backImgBytes = File.ReadAllBytes(BackImg);
                using (var imgBack = Image.FromStream(new MemoryStream(backImgBytes)))
                {
                    bmp = new Bitmap(imgBack.Width, imgBack.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.White);
                        //第一层 背景
                        g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);

                        //第二层 姓名
                        if (!string.IsNullOrEmpty(UserName))
                            g.DrawString(UserName, new Font("微软雅黑", 11), new SolidBrush(Color.White), new Point(182, 5));

                        //第三层 头像
                        if (!string.IsNullOrEmpty(UserImg))
                        {
                            using (var stmHead = HttpHelper.StreamPost(UserImg, null, "GET"))
                            {
                                using (var imgHead = Image.FromStream(stmHead))
                                {
                                    using (Bitmap bitmap = new Bitmap(42, 42))
                                    {
                                        using (Graphics ghead = Graphics.FromImage(bitmap))
                                        {
                                            Rectangle rec = new Rectangle(0, 0, imgHead.Width, imgHead.Height);
                                            using (TextureBrush br = new TextureBrush(imgHead, WrapMode.Clamp, rec))
                                            {
                                                br.ScaleTransform(bitmap.Width / (float)rec.Width, bitmap.Height / (float)rec.Height);
                                                ghead.SmoothingMode = SmoothingMode.AntiAlias;
                                                ghead.FillEllipse(br, new Rectangle(new Point(0, 0), bitmap.Size));
                                            }
                                        }
                                        g.DrawImage(bitmap, 134, 6, bitmap.Width, bitmap.Height);
                                    }
                                }
                            }
                        }
                        //第四层 二维码
                        QRCodeEncoder qrCodeEncoder = new QRCodeEncoder
                        {
                            QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
                            QRCodeScale = 16,
                            QRCodeVersion = 8,
                            QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L
                        };
                        using (var qrImg = qrCodeEncoder.Encode(QrContent))
                        {
                            //g.DrawImage(qrImg, 140, 230, 152, 152);
                            g.DrawImage(qrImg, 140, 244, 152, 152);
                        }
                    }
                    if (!string.IsNullOrEmpty(SaveFilePath))
                        bmp.Save(SaveFilePath, ImageFormat.Jpeg);
                }
            }
            else
            {
                //二维码
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder
                {
                    QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
                    QRCodeScale = 16,
                    QRCodeVersion = 8,
                    QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L
                };

                using (bmp = qrCodeEncoder.Encode(QrContent))
                {
                    if (!string.IsNullOrEmpty(SaveFilePath))
                        bmp.Save(SaveFilePath, ImageFormat.Jpeg);
                }
            }
            GC.Collect();

            return bmp;
        }
         
    }
}
