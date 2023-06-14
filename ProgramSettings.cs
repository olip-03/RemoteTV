using System.Diagnostics;
using RemoteTV.Models;

namespace RemoteTV
{
    public static class ProgramSettings
    {
        public static string loginPassword = "P@ssword";
        public static string mediaDirectory = @"/";
        public static float broadcastFrequency = 46.25f;
        public static bool TXRFAmp = false;
        public static int TXGain = 0;
        public static string runtime = "Linux";

        public static bool isPlaying = false;
        private static Process proc = new Process();

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
        // #everythingisasync <3
        public static async Task PlayMediaAsync(string dir)
        {
            await StopMedia();
            await Task.Run(async () => {
                isPlaying = true;

                // Generate args
                string args = $"-m g -f {broadcastFrequency * 1000000} -s 16000000 -g {TXGain} ";
                if(TXRFAmp) { args += "--amp "; }
                args += "--nonicam --nocolour ";
                args += $"\"{dir}\"";
                Console.WriteLine(args);

                await Bash("hacktv " + args);
            });
        }
        public static async Task StopMedia()
        {
            Console.WriteLine("Killing media...");
            await Bash("pkill hacktv");
            isPlaying = false;
        }
        public static async Task Bash(string cmd)
        {
            await Task.Run(() => {
                var escapedArgs = cmd.Replace("\"", "\\\"");

                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = $"-c \"{escapedArgs}\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                process.Start();
            });
        }
    }
}