namespace Abesto.MediaToolKit.Functions.Utilities
{
    public static class DirectoryUtilities
    {
        public static string GenerateFileLocation(string imagePath, string basePath, string fileNameSuffix = "")
        {
            var fileNameParts = imagePath.Trim('/').Split('/');
            var directoryStructure = string.Join(Path.DirectorySeparatorChar.ToString(), fileNameParts.Take(fileNameParts.Length - 1));
            var localDirectoryPath = Path.Combine(basePath, directoryStructure);

            var extension = Path.GetExtension(fileNameParts.Last());
            var fileNameWithSuffix = Path.GetFileNameWithoutExtension(fileNameParts.Last()) + fileNameSuffix + extension;
            var localFilePath = Path.Combine(localDirectoryPath, fileNameWithSuffix);

            CreateDirectoryIfNotExists(localDirectoryPath);

            return localFilePath;
        }

        private static void CreateDirectoryIfNotExists(string localDirectoryPath)
        {
            if (!Directory.Exists(localDirectoryPath))
            {
                Directory.CreateDirectory(localDirectoryPath);
            }
        }

        public static string GenerateFilePath(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return filePath;
            }

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            string fileExtension = Path.GetExtension(filePath);
            string newFileName = $"{fileNameWithoutExtension}_duplicate{fileExtension}";

            filePath = Path.Combine(Path.GetDirectoryName(filePath), newFileName);

            return filePath;
        }
    }
}
