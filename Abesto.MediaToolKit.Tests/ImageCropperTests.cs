using Abesto.MediaToolKit.Functions.ImageProcessor;
using Abesto.MediaToolKit.Functions.Utilities;
using Microsoft.Extensions.Logging;
using Moq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Abesto.MediaToolKit.Tests
{
    public class ImageCropperTests
    {
        [Fact]
        public void ApplyCropConfiguration_ShouldNotCrop_When_CanCropFalse()
        {
            var configuration = new ImageConfiguration
            {
                CanCrop = false,
                ImageWidth = 100,
                ImageHeight = 50,
                MaintainAspectRatio = false
            };
            var image = new Image<Rgba32>(200, 100);
            var loggerMock = new Mock<ILogger>();

            ImageCropper.ApplyCropConfiguration(configuration, image, loggerMock.Object);

            Assert.Equal(200, image.Width);
            Assert.Equal(100, image.Height);
        }

        [Fact]
        public void ApplyCropConfiguration_ShouldNotCrop_When_WidthAndHeightLessThanConfig()
        {
            var configuration = new ImageConfiguration
            {
                CanCrop = true,
                ImageWidth = 200,
                ImageHeight = 150
            };
            var image = new Image<Rgba32>(100, 80);
            var loggerMock = new Mock<ILogger>();

            ImageCropper.ApplyCropConfiguration(configuration, image, loggerMock.Object);

            Assert.Equal(100, image.Width);
            Assert.Equal(80, image.Height);
        }
    }
}
