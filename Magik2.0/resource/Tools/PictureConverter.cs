using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Resource.Tools;

public class PictureConverter
{
    /// <summary>
    /// Create icon from image
    /// </summary>
    /// <param name="image">Binary image that should be converted</param>
    /// <param name="iconSize">Size of the resulting icon</param>
    /// <returns>Icon</returns>
    public byte[] CreateIconFromImage(byte[] image, int iconSize=128) {
        Bitmap bmp = new Bitmap(ByteToImage(image));
        bmp = ResizeImage(CropImage(bmp), iconSize, iconSize);
        return ImageToByte(bmp);
    }

    /// <summary>
    /// Restrict image height and width
    /// </summary>
    /// <param name="image">Binary image that should be restricted</param>
    /// <param name="maxWidth">New max width for image</param>
    /// <param name="maxHeight">New max height for image</param>
    /// <returns>Restricted image</returns>
    public byte[] RestrictImage(byte[] image, int maxWidth = 512, int maxHeight = 512) {
        Bitmap bmp = new Bitmap(ByteToImage(image));
        if(bmp.Width > maxWidth) {
            double ratio = (double)maxWidth / bmp.Width;
            int newHeight = (int)(bmp.Height * ratio);
            bmp = ResizeImage(bmp, maxWidth, newHeight);
        }
        if(bmp.Height > maxHeight) {
            double ratio = (double)maxHeight / bmp.Height;
            int newWidth = (int)(bmp.Width * ratio);
            bmp = ResizeImage(bmp, newWidth, maxHeight);
        }
        return ImageToByte(bmp);
    }

    public byte[] ImageToByte(Image img)
    {
        using var stream = new MemoryStream();
        img.Save(stream, ImageFormat.Png);
        return stream.ToArray();
    }

    public Image ByteToImage(byte[] img) {
        using var stream = new MemoryStream(img);
        return Image.FromStream(stream);
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
}