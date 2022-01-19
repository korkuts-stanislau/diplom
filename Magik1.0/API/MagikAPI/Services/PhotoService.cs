using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace MagikAPI.Services
{
    public class PhotoService
    {
        public byte[] ProcessBase64Image(string base64Photo, int width, int height)
        {
            var photo = Convert.FromBase64String(base64Photo);

            Bitmap bmp;
            using (var ms = new MemoryStream(photo))
            {
                bmp = new Bitmap(ms);
            }
            bmp = ResizeImage(CropImage(bmp), width, height);


            return ImageToByte(bmp);
        }

        private Bitmap CropImage(Image orgImg)
        {
            var width = orgImg.Width;
            var height = orgImg.Height;
            var centerW = width / 2;
            var centerH = height / 2;

            Rectangle sRect = default;
            Rectangle destRect = default;

            if (width > height)
            {
                sRect = new Rectangle(centerW - centerH, 0, height, height);
                destRect = new Rectangle(0, 0, height, height);
            }
            else
            {
                sRect = new Rectangle(0, centerH - centerW, width, width);
                destRect = new Rectangle(0, 0, width, width);
            }

            var cropImage = new Bitmap(destRect.Width, destRect.Height);
            using (var graphics = Graphics.FromImage(cropImage))
            {
                graphics.DrawImage(orgImg, destRect, sRect, GraphicsUnit.Pixel);
            }
            return cropImage;
        }

        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private byte[] ImageToByte(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
