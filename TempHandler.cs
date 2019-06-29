using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace PCCG_Tester
{
    // this might migrate to abstract class
    public static class TempHandler
    {
        private static int ReadLinesCount = 0;   

        public static void InitTemp(string path)
        {
            // CoreTemp for logging purposes (they agree on clock speeds & temps)
            string filename = Path.Combine(path, "CoreTemp64/Core Temp.exe");
            try
            {
                var proc = System.Diagnostics.Process.Start(filename);
            } catch (Exception e)
            {
                Console.WriteLine("File {0} not found.", filename);
            }

            // HWMonitor for details, does not support logging natively
            string configPath = Path.Combine(path, "Benchmark/hwmonitorw.ini");
            INIFile hwConfig = new INIFile(configPath);
            hwConfig.Write("UPDATES", "0", "HWMonitor");   // Disable update prompts

            filename = Path.Combine(path, "Benchmark/HWMonitor_x64");
            try
            {
                var proc = System.Diagnostics.Process.Start(filename);
            }
            catch (Exception e)
            {
                Console.WriteLine("File {0} not found.", filename);
            }

            // Launch script to enable logging, see commented section below
            filename = Path.Combine(path, "TEMP_LOGGING.exe");
            ProcessStartInfo pInfo = new ProcessStartInfo();
            pInfo.FileName = filename;
            pInfo.WorkingDirectory = path;
            try
            {
                Process script = Process.Start(pInfo);
            }
            catch (Exception e)
            {
                Console.WriteLine("File {0} not found.", filename);
            }
            
            
            //---Below code enables logging by INI-tampering, but CoreTemp uses a corrupted INI-file. 
            //---Uncomment code if future update addresses this (or manually declare DTD)
            //---This approach is favourable to script above

            //INIFile tempConfig = new INIFile(@"C://Users/User/Desktop/AAA Testing/CoreTemp64/CoreTemp.ini");
            //tempConfig.Write("EnLog", "1", "Core temp settings");
        }

        public static void ReadTemp(string path)
        {
            string partialName = "CT-Log";
            string ctFolder = Path.Combine(path, "CoreTemp64");

            DirectoryInfo folder = new DirectoryInfo(ctFolder);
            FileInfo[] results = folder.GetFiles(partialName + "*.csv");
            if (results.Length == 0)
            {
                Prompt.ShowDialog("No temp logs were found", "Error");
                return;
            }
            string fullName = results.Last().FullName;   // Choose latest log file

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = ctFolder;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = results.Last().Name;
            watcher.Changed += new FileSystemEventHandler(TempUpdate);
            watcher.EnableRaisingEvents = true;


        }

        private static void TempUpdate(object source, FileSystemEventArgs e)
        {
            using (var fs = new FileStream(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs, Encoding.Default))
            {
                /*int totalLines = File.ReadLines(e.FullPath).Count();
                int newLinesCount = totalLines - ReadLinesCount;
                File.ReadLines(e.FullPath).Skip(ReadLinesCount).Take(newLinesCount);
                ReadLinesCount = totalLines;
                */
            }
          
        }
    }    
     
}
