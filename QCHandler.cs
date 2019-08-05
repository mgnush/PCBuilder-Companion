/*
 * QCHandler.cs
 * 
 * @Author  Magnus Hjorth
 * 
 * File Description: This class holds all static methods to do with the QC and system cleanup procedures.
 * Each cleanup method should be called individually because the exact point of program termination may vary
 * on a use to use basis.
 */

using System;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Builder_Companion
{
    public static class QCHandler
    {
        private const string HARD_DRIVE = "3";
        private const int MBR = 1;
        private const int GPT = 2;
        private const String APP_ID = "Microsoft.Samples.DesktopToastsSample";

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        /// <summary>
        /// Deletes all folders and files from desktop, then sorts shortcuts
        /// </summary>
        public static async void ClearDesktop()
        {
            DirectoryInfo desktop = new DirectoryInfo(Paths.Desktop());
            DirectoryInfo desktopCommon = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory));
            DirectoryInfo tempDesktop = Directory.CreateDirectory(Path.Combine(desktop.Parent.FullName, "TempDesktop"));
            List<Task<bool>> copyOps = new List<Task<bool>>();

            foreach (FileInfo file in desktop.EnumerateFiles())
            {
                // Move all files to tempdesktop
                if (!file.Name.Equals(Path.GetFileName(Application.ExecutablePath)))
                {
                    copyOps.Add(MoveFileAsync(file.FullName, Path.Combine(tempDesktop.FullName, file.Name)));
                }                    
            }
            foreach (FileInfo file in desktopCommon.EnumerateFiles())
            {
                // Move all files to tempdesktop
                if (!file.Name.Equals(Path.GetFileName(Application.ExecutablePath)))
                {
                    copyOps.Add(MoveFileAsync(file.FullName, Path.Combine(tempDesktop.FullName, file.Name)));
                }
            }

            // Move all folders to tempdesktop            
            foreach (DirectoryInfo dir in desktop.EnumerateDirectories())
            {
                try
                {
                    dir.MoveTo(Path.Combine(tempDesktop.FullName, dir.Name));
                } catch
                {
                    Prompt.ShowDialog("DesktopSort error", "Error");
                }                
            }            

            await Task.WhenAll(copyOps.ToArray());
            tempDesktop.Refresh();
            copyOps.Clear();

            // Move shortcuts back
            foreach (FileInfo file in tempDesktop.EnumerateFiles())
            {
                if (IsLink(file.FullName) && !file.Name.ToLower().Contains("edge") && !file.Name.Contains("CustService"))
                {
                    copyOps.Add(MoveFileAsync(file.FullName, Path.Combine(desktop.FullName, file.Name)));
                } 
            }

            await Task.WhenAll(copyOps.ToArray());
            tempDesktop.Refresh();

            tempDesktop.Delete(true);
        }

        private static async Task<bool> MoveFileAsync(string sourcePath, string destPath)
        {
            try
            {
                using (Stream source = File.Open(sourcePath, FileMode.Open))
                {
                    using (Stream destination = File.Create(destPath))
                    {
                        await source.CopyToAsync(destination);
                        source.Close();
                        File.Delete(sourcePath);
                    }
                }
            } catch
            {
                // swallow
            }
            return true;
        } 

        /// <summary>
        /// Deletes the appsettings folder for this application for the local user.
        /// </summary>
        public static void ClearSettings()
        {
            DirectoryInfo di = new DirectoryInfo(Path.Combine(Paths.User(), Paths.APPDATA));
            foreach (FileInfo file in di.EnumerateFiles())
            {
                file.Delete();            
            }
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                dir.Delete(true);
            }
            di.Delete();
        }

        public static void ClearHeaven()
        {
            string heavenPath = Path.Combine(Paths.User(), "HEAVEN");

            if (Directory.Exists(heavenPath))
            {
                DirectoryInfo di = new DirectoryInfo(heavenPath);

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();

                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }
                di.Delete();
            }            
        }

        /// <summary>
        /// Launch all windows used for manual QC by the QC'er.
        /// </summary>
        public static void LaunchManualChecks()
        {
            const int SWP_SHOWWINDOW = 0x0040;
            
            // Launch the four main windows
            ProcessStartInfo devmInfo = new ProcessStartInfo("devmgmt.msc");
            Process devm = new Process();
            devm.StartInfo = devmInfo;
            devm.Start();

            ProcessStartInfo diskmInfo = new ProcessStartInfo("diskmgmt.msc");
            Process diskm = new Process();
            diskm.StartInfo = diskmInfo;
            diskm.Start();     
            
            Thread.Sleep(1000);   // Need to wait for mainwindowtitles to correctly 'guess' the gui window
                                  // Unfortunately, WaitForInputIdle is not sufficient!

            // Reposition mmc class processes
            
            Process[] mmcProcs = Process.GetProcessesByName("mmc");
            foreach (var proc in mmcProcs)
            {
                if (proc.MainWindowTitle.Equals("Disk Management"))
                {
                    var handle = proc.MainWindowHandle;
                    SetWindowPos(handle, 0, 0, 540, 960, 540, SWP_SHOWWINDOW);
                }
                if (proc.MainWindowTitle.Equals("Device Manager"))
                {
                    var handle = proc.MainWindowHandle;
                    SetWindowPos(handle, 0, 960, 540, 960, 540, SWP_SHOWWINDOW);
                }
            }
            
            // Launch windows update
            string systemFolder = Environment.GetFolderPath(Environment.SpecialFolder.System);
            ProcessStartInfo wupInfo = new ProcessStartInfo(Path.Combine(systemFolder, "control.exe"), "/name Microsoft.WindowsUpdate");
            Process wup = new Process();
            wup.StartInfo = wupInfo;
            wup.Start();
            wup.WaitForInputIdle();

            // System info, explorer is handled by standalone script
            string sysInfoScript = Path.Combine(Paths.Desktop(), Paths.TEST, Paths.FILES, Paths.QCWINDOWS_SCRIPT);
            if (File.Exists(sysInfoScript))
            {
                ProcessStartInfo sysInfo = new ProcessStartInfo(sysInfoScript);
                Process sys = new Process();
                sys.StartInfo = sysInfo;
                sys.Start();
                sys.WaitForExit();
            }
        }

        /// <summary>
        /// NEEDS IMPLEMENTATION. Clears all current toasts from Windows Center.
        /// </summary>
        public static void ClearToasts()
        {
            string scriptPath = Path.Combine(Paths.Desktop(), Paths.TEST, Paths.FILES, Paths.CLEAR_TOASTS_SCRIPT);

            if (File.Exists(scriptPath))
            {
                Process.Start(scriptPath);
            }            
        }

        /// <summary>
        /// Initializes all uninitialised hard drives, and formats all unformatted hard drives. 
        /// Has no effect if all drives are online, initialised and formatted.
        /// </summary>
        public static void FormatDrives()
        {
            // Create partitions
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"Root/Microsoft/Windows/Storage", "select * from MSFT_DISK");
            foreach (ManagementObject m in searcher.Get())
            {
                // Initialize disk first
                var res = m.InvokeMethod("Initialize", new object[] { GPT });   // Will have no effect on intialized disks

                if (m["NumberOfPartitions"].ToString().Equals("0"))
                {
                    // https://docs.microsoft.com/en-au/previous-versions/windows/desktop/stormgmt/createpartition-msft-disk
                    // Use 1MB alignment = 1048576 bytes
                    var parRes = m.InvokeMethod("CreatePartition", new object[] { null, true, null, 1048576, null, true, null, null, false, false });
                    // Thread.Sleep(1000);
                }
            }

            // Thread.Sleep(500);  // Formatting is unstable if drive letters are not given time to be created
            
            // Format 
            searcher = new ManagementObjectSearcher("select * from Win32_Volume");
            foreach (ManagementObject m in searcher.Get())
            {
                if ((m["DriveType"].ToString() == HARD_DRIVE) && (m["FileSystem"] == null))
                {
                    // https://docs.microsoft.com/en-au/previous-versions/windows/desktop/stormgmt/format-msft-volume
                    var res = m.InvokeMethod("Format", new object[] { "NTFS", true, 8192, "New Volume", false });
                }
                
            }

            // In case any drives are offline...
            /*
            searcher = new ManagementObjectSearcher(@"Root/Microsoft/Windows/Storage", "select * from MSFT_Partition");
            foreach (ManagementObject m in searcher.Get())
            {
                if (m["IsOffline"].ToString().ToLower().Equals("true"))
                {
                    var res = m.InvokeMethod("Online", new object[] { });
                }
            }
            */
        }

        /// <summary>
        /// Returns whether the given path/file is a link
        /// </summary>
        /// <param name="shortcutFilename"></param>
        /// <returns></returns>
        private static bool IsLink(string shortcutFilename)
        {
            string pathOnly = Path.GetDirectoryName(shortcutFilename);
            string filenameOnly = Path.GetFileName(shortcutFilename);

            Shell32.Shell shell = new Shell32.Shell();
            Shell32.Folder folder = shell.NameSpace(pathOnly);
            Shell32.FolderItem folderItem = folder.ParseName(filenameOnly);
            if (folderItem != null)
            {
                return folderItem.IsLink;
            }
            return false; // not found
        }
     
    }    
}
