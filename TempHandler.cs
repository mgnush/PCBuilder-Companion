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
using System.Text.RegularExpressions;


namespace PCCG_Tester
{
    // this might migrate to abstract class
    public static class TempHandler
    {
        public static int MaxTemp = 0;
        public static double MaxSpeed = 0;
        public static double MaxPwr = 0;

        public static double MaxGPUTemp = 0;

        private static string furmarkTemp = Path.Combine(Paths.TEST, Paths.FURMARK_TEMP);

        public static void InitTemp()
        {
            // CoreTemp for logging purposes (they agree on clock speeds & temps)
            string filename = Path.Combine(Paths.TEST, Paths.CT_EXE);
            try
            {
                var proc = System.Diagnostics.Process.Start(filename);
            } catch (Exception e)
            {
                Console.WriteLine("File {0} not found.", filename);
            }

            // HWMonitor for details, does not support logging natively
            string configPath = Path.Combine(Paths.TEST, Paths.HWMONITOR_INI);
            INIFile hwConfig = new INIFile(configPath);
            hwConfig.Write("UPDATES", "0", "HWMonitor");   // Disable update prompts

            filename = Path.Combine(Paths.TEST, Paths.HWMONITOR_EXE);
            try
            {
                var proc = System.Diagnostics.Process.Start(filename);
            }
            catch (Exception e)
            {
                Console.WriteLine("File {0} not found.", filename);
            }

            // Launch script to enable logging, see commented section below
            filename = Path.Combine(Paths.TEST, Paths.FILES, Paths.TEMP_SCRIPT);
            ProcessStartInfo pInfo = new ProcessStartInfo();
            pInfo.FileName = filename;
            pInfo.WorkingDirectory = Paths.TEST;
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

        public static void ReadTemp()
        {
            // Acquire full name of newest log
            string partialName = "CT-Log";
            string ctFolder = Path.Combine(Paths.TEST, Paths.CT_FOLDER);
            string fullName = "";
            string lastFile = "";

            try
            {
                DirectoryInfo folder = new DirectoryInfo(ctFolder);
                FileInfo[] results = folder.GetFiles(partialName + "*.csv");
                fullName = results.Last().FullName;   // Choose latest log file  
                lastFile = results.Last().Name;
            } catch
            {
                Prompt.ShowDialog("No CT temp log file was found", "Error");
                return;
            }                    

            // Trigger read every time CT updates log
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = ctFolder;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = lastFile;
            watcher.Changed += new FileSystemEventHandler(TempUpdate);
            watcher.EnableRaisingEvents = true;
        }

        //This is only called from the CT event handler to reduce threads & load
        private static void ReadFurmarkTemp()
        {
            string newLine = "";

            using (var fs = new FileStream(furmarkTemp, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    newLine = sr.ReadLine();
                }

                string[] entries = newLine.Split(' ');
                foreach (string entry in entries)
                {
                    if (entry.Contains("core_temp") && !entry.Contains("fahrenheit"))
                    {
                        Match number = Regex.Match(entry, @"\d+");
                        double temp = Convert.ToDouble(number.Value);
                        if (temp > MaxGPUTemp)
                        {
                            MaxGPUTemp = temp;
                        }
                    }
                }
            }
        }

        // Consider async read if cpu usage becomes noticeable
        private static void TempUpdate(object source, FileSystemEventArgs e)
        {
            ReadFurmarkTemp();

            bool FirstUpdate = true;
            List<int> TempIndeces = new List<int>();
            List<int> SpeedIndeces = new List<int>();
            int CPUPowerIndex = 0;
            string newLine = "";

            using (var fs = new FileStream(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs, Encoding.Default))
            {                
                while(!sr.EndOfStream)
                {                    
                    newLine = sr.ReadLine();

                    // Retrieve data positions on first file read
                    if (FirstUpdate)
                    {
                        if (newLine.Contains("Time"))   // This is the data entry header line
                        {
                            string[] headers = newLine.Split(',');
                            for (int i = 0; i < headers.Length; i++)
                            {
                                if (headers[i].Contains("High temp"))
                                {
                                    TempIndeces.Add(i);
                                }
                                if (headers[i].Contains("Core speed"))
                                {
                                    SpeedIndeces.Add(i);
                                }
                                if (headers[i].Contains("Power"))
                                {
                                    CPUPowerIndex = i;
                                }
                            }
                            FirstUpdate = false;
                        }                        
                    }                     
                }       
            }
            // Update maximum values
            if (!FirstUpdate)
            {
                int temp;
                double speed, pwr;
                string[] data = newLine.Split(',');
                if (data.Length > 2)
                {
                    foreach (int index in TempIndeces)
                    {
                        Int32.TryParse(data[index], out temp);
                        if (temp > MaxTemp) { MaxTemp = temp; }
                    }
                    foreach (int index in SpeedIndeces)
                    {
                        Double.TryParse(data[index], out speed);
                        if (speed > MaxSpeed) { MaxSpeed = speed; }
                    }
                    Double.TryParse(data[CPUPowerIndex], out pwr);
                    if (pwr > MaxPwr) { MaxPwr = pwr; }
                }
            }

            // For debugging only:
            //Prompt.ShowDialog(MaxTemp.ToString(), "max temp");

        }

    }

}
