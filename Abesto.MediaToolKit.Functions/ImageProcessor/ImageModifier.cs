using Abesto.MediaToolKit.Functions.Utilities;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Abesto.MediaToolKit.Functions.ImageProcessor
{
    public static class ImageModifier
    {
        public static async Task ModifyAsync(ImageConfiguration configuration, string filePath, string outputPath, ILogger logger)
        {
            using var input = File.OpenRead(filePath);
            using var output = File.OpenWrite(outputPath);
            logger.LogInformation($"----- Started Modifying File - {filePath}");
            try
            {
                var image = await Image.LoadAsync(input);
                LogImageFileInformation(input.Length, image, logger);

                ResizeBasedOnAspectRatio(configuration, image, logger);

                ImageCropper.ApplyCropConfiguration(configuration, image, logger);

                logger.LogInformation($"Applied image modifications");

                //Saving as JPEG as of now
                image.Save(output, new JpegEncoder());

                LogImageFileInformation(output.Length, image, logger);
                logger.LogInformation($"----- Saved file - {outputPath}");
            }
            catch (UnknownImageFormatException)
            {
                logger.LogInformation($" ******* Bad file format or not an image - {filePath} ******* ");
            }

            logger.LogInformation($"----- Completed Modifying File - {filePath}");
        }

        private static void ResizeBasedOnAspectRatio(ImageConfiguration configuration, Image image, ILogger logger)
        {
            if (configuration.MaintainAspectRatio)
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(configuration.ImageWidth, configuration.ImageHeight),
                    Mode = ResizeMode.Max,
                    Position = AnchorPositionMode.Center,
                }));
            }
            else
            {
                image.Mutate(x => x.Resize(configuration.ImageWidth, configuration.ImageHeight));
            }

            logger.LogInformation($"Applied Aspect ratio configurations - {configuration.MaintainAspectRatio}");
        }

        private static void LogImageFileInformation(long fileLength, Image image, ILogger logger)
        {
            logger.LogInformation($"File Size: {fileLength / 1024} KB");
            logger.LogInformation($"File Dimension: {image.Width}x{image.Height}");
        }
    }
}
