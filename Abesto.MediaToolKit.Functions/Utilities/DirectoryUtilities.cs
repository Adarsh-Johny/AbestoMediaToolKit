namespace Abesto.MediaToolKit.Functions.Utilities
{
    public static class DirectoryUtilities
    {
        public static string GenerateFileLocation(string imagePath, string basePath)
        {
            var fileNameParts = imagePath.Trim('/').Split('/');
            var directoryStructure = string.Join(Path.DirectorySeparatorChar.ToString(), fileNameParts.Take(fileNameParts.Length - 1));
            var localDirectoryPath = Path.Combine(basePath, directoryStructure);
            var localFilePath = Path.Combine(localDirectoryPath, fileNameParts.Last());

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
    }
}
