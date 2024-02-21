using Abesto.MediaToolKit.Functions.ImageProcessor;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Abesto.MediaToolKit.Functions.Utilities
{
    public static class ImageCropper
    {
        public static void ApplyCropConfiguration(ImageConfiguration configuration, Image image, ILogger logger)
        {
            if (ShouldCropImage(configuration, image))
            {
                var cropRectangle = CalculateCropRectangle(configuration, image);

                if (cropRectangle.Width > cropRectangle.Height)
                {
                    CropImageHorizontally(image, cropRectangle);
                }
                else
                {
                    CropImageVertically(image, cropRectangle);
                }

            }
            logger.LogInformation($"Applied crop configurations - {configuration.CanCrop}");
        }

        private static bool ShouldCropImage(ImageConfiguration configuration, Image image)
        {
            return configuration.CanCrop && (image.Width > configuration.ImageWidth || image.Height > configuration.ImageHeight);
        }

        private static Rectangle CalculateCropRectangle(ImageConfiguration configuration, Image image)
        {
            //Calculated the starting point and the dimensions to crop

            var cropX = Math.Min(configuration.ImageWidth, image.Width);
            var cropY = Math.Min(configuration.ImageHeight, image.Height);

            var cropWidth = Math.Min(configuration.ImageWidth, image.Width - cropX);
            var cropHeight = Math.Min(configuration.ImageHeight, image.Height - cropY);

            return new Rectangle(cropX, cropY, cropWidth, cropHeight);
        }

        private static void CropImageHorizontally(Image image, Rectangle cropRectangle)
        {
            image.Mutate(x => x.Crop(new Rectangle(cropRectangle.X, 0, cropRectangle.Width, image.Height)));
        }

        private static void CropImageVertically(Image image, Rectangle cropRectangle)
        {
            image.Mutate(x => x.Crop(new Rectangle(0, cropRectangle.Y, image.Width, cropRectangle.Height)));
        }
    }
}
