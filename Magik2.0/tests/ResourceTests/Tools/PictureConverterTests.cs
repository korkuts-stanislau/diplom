using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Resource.Tools;
using Xunit;

namespace Tests.ResourceTests.Tools;

public class PictureConverterTests {
    private readonly PictureConverter pictureConverter = new PictureConverter();
    private readonly Image img;
    private readonly byte[] bImg;
    private readonly Bitmap imgBm;

    public PictureConverterTests()
    {
        img = Image.FromFile("../../../Data/picture.jpg");
        bImg = pictureConverter.ImageToByte(img);
        imgBm = new Bitmap(img);
    }

    [Fact]
    public void CreateIconFromImage() {
        //Arrange
        int iconSize = 64;

        //Act
        var bIcon = pictureConverter.CreateIconFromImage(bImg, iconSize);
        var icon = pictureConverter.ByteToImage(bIcon);
        var iconBm = new Bitmap(icon);

        //Assert
        Assert.Equal(iconSize, iconBm.Width);
        Assert.Equal(iconSize, iconBm.Height);
    }

    [Fact]
    public void RestrictImage() {
        //Arrange
        int maxWidth = 128;
        int maxHeight = 128;

        //Act
        var bRestricted = pictureConverter.RestrictImage(bImg, maxWidth, maxHeight);
        var restricted = pictureConverter.ByteToImage(bRestricted);
        var restrictedBm = new Bitmap(restricted);

        double ratioDifference = (restrictedBm.Width / (double)imgBm.Width) - (restrictedBm.Height / (double)imgBm.Height);

        //Assert
        Assert.True(restrictedBm.Width <= maxWidth);
        Assert.True(restrictedBm.Height <= maxHeight);
        Assert.True(ratioDifference < 0.05); // Difference between width and height ratio should be less then 5%
    }
}