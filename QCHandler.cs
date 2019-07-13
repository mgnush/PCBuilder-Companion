﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;

namespace Builder_Companion
{
    public static class QCHandler
    {
        private const string HARD_DRIVE = "3";
        private const int MBR = 1;
        private const int GPT = 2;

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        public static void ClearDesktop()
        {
            DirectoryInfo di = new DirectoryInfo(Paths.Desktop());
            //SetAttributesNormal(di);

            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (!IsLink(file.FullName))
                    file.Delete();
            }
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                dir.Delete(true);
            }         
        }

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

            ProcessStartInfo explInfo = new ProcessStartInfo("explorer.exe");
            Process expl = new Process();
            expl.StartInfo = explInfo;
            expl.Start();
            string explName = Path.GetFileNameWithoutExtension(explInfo.FileName);

            // System info is handled by standalone script
            try
            {
                ProcessStartInfo sysInfo = new ProcessStartInfo(Path.Combine(Paths.Desktop(), Paths.TEST, Paths.FILES, Paths.SYSTEM_SCRIPT));
                Process sys = new Process();
                sys.StartInfo = sysInfo;
                sys.Start();
            } catch { }
            
            Thread.Sleep(1000);   // Need to wait for mainwindowtitles to correctly 'guess' the gui window

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

            // Reposition windows explorer - this does not work most of the time
            Process[] explProcs = Process.GetProcessesByName(explName);
            foreach (var proc in explProcs)
            {
                var handle = proc.MainWindowHandle;
                SetWindowPos(handle, 0, 0, 0, 960, 540, SWP_SHOWWINDOW);                
            }

            // Launch windows update
            string systemFolder = Environment.GetFolderPath(Environment.SpecialFolder.System);
            ProcessStartInfo wupInfo = new ProcessStartInfo(Path.Combine(systemFolder, "control.exe"), "/name Microsoft.WindowsUpdate");
            Process wup = new Process();
            wup.StartInfo = wupInfo;
            wup.Start();
            
        }


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
                    //Thread.Sleep(1000);
                }
            }

            //Thread.Sleep(2000);  // Formatting is unstable if drive letters are not given time to be created
            
            // Format 
            searcher = new ManagementObjectSearcher("select * from Win32_Volume");
            foreach (ManagementObject m in searcher.Get())
            {
                if ((m["DriveType"].ToString() == HARD_DRIVE) && (m["FileSystem"] == null))
                {
                    // https://docs.microsoft.com/en-au/previous-versions/windows/desktop/stormgmt/format-msft-volume
                    var res = m.InvokeMethod("Format", new object[] { "NTFS", true, 8192, "Disk", false });
                }
                
            }

            // In case any drives are offline...
            searcher = new ManagementObjectSearcher(@"Root/Microsoft/Windows/Storage", "select * from MSFT_Partition");
            foreach (ManagementObject m in searcher.Get())
            {
                if (m["IsOffline"].ToString().ToLower().Equals("true"))
                {
                    var res = m.InvokeMethod("Online", new object[] { });
                }
            }
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