using System.Diagnostics;
using System.Linq;
using RemoteTV.Models;

namespace RemoteTV
{
    public static class ProgramSettings
    {
        public static string loginPassword = "P@ssword";
        public static string mediaDirectory = @"/";
        public static float broadcastFrequency = 46.25f;
        public static bool TXRFAmp = false;
        public static int TXGain = 20;
        public static bool hideNonPlayableFiles = true;
        public static List<AlarmModel> alarms = new(); 

        public static string runtime = "Linux";
        public static bool isPlaying = false;
        public static float currentPlaytime = 0;
        public static float currentFileLength = 0;

        public static List<String> folders = new();
        public static List<String> files = new();
        public static int processId = 0;
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
        public static string? GetParent(string directory)
        {
            return Directory.GetParent(directory).FullName;
        }
        public static async Task SaveData() 
        {
            await Task.Run(() => {

            }); 
        }
        public static async Task LoadDate() 
        {
            await Task.Run(() => {

            }); 
        }
        public static List<String> GetFiles(string directory)
        {
            List<string> returnFiles = Directory.GetFiles(directory).ToList();
            if(hideNonPlayableFiles)
            {
                List<string> modiFiles = new(returnFiles);
                foreach (string file in returnFiles)
                {
                    FileInfo fileInfo = new(file);
                    string type = GetFileType(fileInfo.Extension);
                    // fuck fix this
                    if(type != "Video")
                    {
                        if(type != "Audio")
                        {
                            modiFiles.Remove(file);
                        }  
                    }
                }
                return modiFiles;
            }
            return returnFiles;
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
        public static string GetFileType(string directory)
        {
            string filetype = "File";
            FileInfo fi = new(directory);  
            string[] audioFileFormats = { ".mp3", ".wav", ".flac", ".aac", ".wma", ".ogg", ".m4a" };
            string[] videoFileFormats = { ".mp4", ".mov", ".avi", ".wmv", ".mkv", ".flv", ".webm", ".m4v", ".mpeg", ".3gp" };
            string[] imageFileFormats = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".ico", ".svg" };
            string[] codeFileFormats = { ".cs", ".java", ".cpp", ".py", ".html", ".css", ".js", ".php", ".swift", ".rb", ".go", ".ts", ".vb", ".pl", ".lua", ".scala", ".rust", ".r", ".matlab" };
            string[] excelFileFormats = { ".xlsx", ".xls", ".xlsm", ".xlsb", ".csv" };
            string[] textFileFormats = { ".txt", ".doc", ".docx", ".rtf", ".csv" };
            string[] pdfFileFormats = { ".pdf" };
            string[] powerpointFileFormats = { ".pptx", ".ppt", ".pps", ".ppsx" };
            string[] wordFileFormats = { ".docx", ".doc", ".rtf", ".txt" };
            string[] zipFileFormats = { ".zip", ".rar", ".7z", ".tar", ".gz" };

            if(Array.IndexOf(audioFileFormats, fi.Extension) > -1)
            {
                filetype = "Audio";
            }
            if(Array.IndexOf(videoFileFormats, fi.Extension) > -1)
            {
                filetype = "Video";
            }
            if(Array.IndexOf(imageFileFormats, fi.Extension) > -1)
            {
                filetype = "Image";
            }
            if(Array.IndexOf(codeFileFormats, fi.Extension) > -1)
            {
                filetype = "Code";
            }
            if(Array.IndexOf(excelFileFormats, fi.Extension) > -1)
            {
                filetype = "Code";
            }
            if(Array.IndexOf(textFileFormats, fi.Extension) > -1)
            {
                filetype = "Text";
            }
            if(Array.IndexOf(pdfFileFormats, fi.Extension) > -1)
            {
                filetype = "PDF";
            }
            if(Array.IndexOf(powerpointFileFormats, fi.Extension) > -1)
            {
                filetype = "PowerPoint";
            }
            if(Array.IndexOf(wordFileFormats, fi.Extension) > -1)
            {
                filetype = "Word";
            }
            if(Array.IndexOf(zipFileFormats, fi.Extension) > -1)
            {
                filetype = "Zip";
            }
            
            return filetype;
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
        // this is the shittest way of doing this
        // except it works so ??? 
        public static async Task PauseStream(){
            string pid = "hacktv process ID";
            await Bash($"pkill -s SIGSTOP {pid}");
        }
        public static async Task ResumeStream(){
            string pid = "hacktv process ID";
            await Bash($"pkill -s SIGSTOP {pid}");
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
                processId = process.Id;
            });
        }
    }
}