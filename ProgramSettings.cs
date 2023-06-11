namespace RemoteTV
{
    public static class ProgramSettings
    {
        public static string loginPassword = "P@ssword";
        public static string mediaDirectory = @"C:\";

        public static List<String> folders = new List<String>();
        public static List<String> files = new List<String>();

        public static void SetMediaDirectory(string setDirectory)
        {
            mediaDirectory = setDirectory;
            UpdateFolders();
        }
        public static void UpdateFolders()
        {
            if (Directory.Exists(mediaDirectory))
            {
                folders = Directory.GetDirectories(mediaDirectory).ToList();
                files = Directory.GetFiles(mediaDirectory).ToList();
            }
            else
            {
                throw new DirectoryNotFoundException(mediaDirectory + " does not exist.");
            }
        }
    }
}