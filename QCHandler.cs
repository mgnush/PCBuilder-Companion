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
using Windows.UI.Notifications;

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
        /// Deletes all folders and files from desktop, except links and 'blacklisted' items.
        /// </summary>
        public static void ClearDesktop()
        {
            DirectoryInfo di = new DirectoryInfo(Paths.Desktop());
            //SetAttributesNormal(di);   // Uncomment if access isnt granted to non-exe files

            List<AFile> desktopFiles = new List<AFile>();
            List<AFile> desktopFolders = new List<AFile>();
            bool filesDeleted = false;
            bool cleaningDone = false;

            // Watcher for files deleted on the desktop
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = di.FullName;
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.*";
            watcher.Deleted += delegate (object sender, FileSystemEventArgs e) { DesktopItemDeleted(sender, e, desktopFiles, desktopFolders, ref filesDeleted, ref cleaningDone); };
            watcher.EnableRaisingEvents = true;

            foreach (FileInfo file in di.EnumerateFiles())
            {
                // Delete all desktop files that are not shortcuts or this program itself (unless its a NAS-shortcut)
                if ((!IsLink(file.FullName) || file.Name.Contains("CustService")) && 
                    !file.Name.Equals(Path.GetFileName(Application.ExecutablePath)))
                {
                    desktopFiles.Add(new AFile(file.FullName));
                }                    
            }

            // Add all desktop folders to list of folders to be deleted
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                desktopFolders.Add(new AFile(dir.FullName));
            }

            if (desktopFiles.Count > 0)
            {
                // Delete files first if any
                foreach (AFile file in desktopFiles)
                {
                    File.Delete(file.FullPath);
                }               
            } else
            {
                // Delete folders if any
                if (desktopFolders.Count > 0)
                {
                    ClearDesktopFolders(desktopFolders);
                } else
                {
                    watcher.EnableRaisingEvents = false;
                    OrganizeDesktop();   // Organize desktop if already clear
                }                
            }               
        }

        private static void ClearDesktopFolders(List<AFile> desktopFolders) 
        {
            // Start deleting any folder that hasn't already begun deleting
            foreach (AFile file in desktopFolders)
            {
                if (!file.DeleteStarted)
                {
                    lock (file)
                    {
                        file.DeleteStarted = true;
                    }                    
                    DirectoryInfo d = new DirectoryInfo(file.FullPath);
                    d.Delete(true);
                }                
            }
        }

        private static void DesktopItemDeleted(object source, FileSystemEventArgs e, List<AFile> desktopFiles, 
            List<AFile> desktopFolders, ref bool filesDeleted, ref bool cleaningDone)
        {
            if (cleaningDone) { return; }

            // Declare the deleted item deleted
            foreach (AFile file in desktopFiles)
            {
                if (file.FullPath.Equals(e.FullPath))
                {
                    lock (file)
                    {
                        file.Deleted = true;
                    }
                }
            }

            // Declare the deleted folder deleted
            foreach (AFile folder in desktopFolders)
            {
                if (folder.FullPath.Equals(e.FullPath))
                {
                    lock (folder)
                    {
                        folder.Deleted = true;
                    }
                }
            }

            // Advance only if all files are deleted
            if (!filesDeleted)
            {
                foreach (AFile file in desktopFiles)
                {
                    if (!file.Deleted)
                    {
                        return;
                    }
                }
                filesDeleted = true;
                ClearDesktopFolders(desktopFolders);
            }

            // Advance only if all folders are deleted
            foreach (AFile folder in desktopFolders)
            {
                if (!folder.Deleted)
                {
                    return;
                }
            }
            
            cleaningDone = true;
            Thread.Sleep(300);
            OrganizeDesktop();            
        }

        private static async void OrganizeDesktop()
        {
            DirectoryInfo di = new DirectoryInfo(Paths.Desktop());

            List<Task<bool>> copyOps = new List<Task<bool>>();

            // Rearrange
            // This simply moves all remaining items to a temporary folder and back -
            // the alternative is a shell aproach
            // https://devblogs.microsoft.com/oldnewthing/?p=4933
            DirectoryInfo tempDesktop = Directory.CreateDirectory(Path.Combine(Paths.User(), "TempDesktop"));

            // Copy files to temp folder
            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (!file.Name.Equals(Path.GetFileName(Application.ExecutablePath)))
                {
                    copyOps.Add(MoveFileAsync(file.FullName, Path.Combine(tempDesktop.FullName, file.Name)));
                }
            }
            // Wait for copy operations to finish
            await Task.WhenAll(copyOps.ToArray());

            tempDesktop.Refresh();
            copyOps.Clear();
            // Move back...
            foreach (FileInfo file in tempDesktop.EnumerateFiles())
            {
                copyOps.Add(MoveFileAsync(file.FullName, Path.Combine(di.FullName, file.Name)));
            }
            // Wait for copy operations to finish
            await Task.WhenAll(copyOps.ToArray());


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
            // Can only instantiate notificationmanagers for apps in the same package as caller...
            var toastMngr = ToastNotificationManager.CreateToastNotifier(APP_ID);
            var notifs = toastMngr.GetScheduledToastNotifications();

            for (int i = 0; i < notifs.Count; i++)
            {
                toastMngr.RemoveFromSchedule(notifs[i]);
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

        public class AFile
        {
            public string FullPath { get; internal set; }
            public bool Deleted { get; set; }
            public bool DeleteStarted { get; set; }

            public AFile(string path)
            {
                FullPath = path;
                Deleted = false;
                DeleteStarted = false;
            }
        }
    }    
}
