using System.Diagnostics;

namespace RemoteTV
{
    public static class ProgramSettings
    {
        public static string loginPassword = "P@ssword";
        public static string mediaDirectory = @"/";
        public static float broadcastFrequency = 46.25f;
        public static bool 
        public static string runtime = "Linux";

        public static bool isPlaying = false;
        private static List<Process> runningProcesses = new List<Process>();

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
        public static bool IsTopLevel(string directory)
        {
            DirectoryInfo d = new DirectoryInfo(directory);
            if(d.Parent == null) 
            { 
                return true; 
            }   
            return false;
        }
        public static string GetParent(string directory)
        {
            return Directory.GetParent(directory).FullName;
        }
        public static List<String> GetFiles(string directory)
        {
            return Directory.GetFiles(directory).ToList();
        }
        public static List<String> GetFolders(string directory)
        {
            return Directory.GetDirectories(directory).ToList();
        }
        public static List<String> GetDrives()
        {
            DriveInfo[] allDrives =  DriveInfo.GetDrives();
            List<string> drives = new List<string>();
            foreach (DriveInfo d in allDrives)
            {
                drives.Add(d.Name);
            }
            return drives;
        }
        public static bool HasAccessTo(string directory)
        {
            try
            {
                Directory.GetFiles(directory).ToList();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool isFile(string directory)
        {
            // Check if the requested directory is a File or a Directory
            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(directory);

            if (attr.HasFlag(FileAttributes.Directory))
            {
                return false;
            }
            return true;
        }
        public static void PlayMedia(string dir)
        {
            isPlaying = true;

            ProcessStartInfo startInfo = new ProcessStartInfo();        
            startInfo.FileName = @"hacktv";
            startInfo.Arguments = $"-m g -f {broadcastFrequency * 1000000} -s 16000000 -g 0 \"{dir}\"";
            if(startInfo != null)
            {
                try
                {
                    Process hacktv = Process.Start(startInfo);
                    runningProcesses.Add(hacktv);
                }
                catch
                {
                    isPlaying = false;
                }
            }
        }
        public static void StopMedia()
        {
            foreach (Process process in runningProcesses)
            {
                // now check the modules of the process
                foreach (ProcessModule module in process.Modules)
                {
                    if (module.FileName.Equals("MyProcess.exe"))
                    {
                        process.Kill();
                    } else 
                    {
                        // Process not found
                    }
                }
            }
            isPlaying = false;
        }
    }
}