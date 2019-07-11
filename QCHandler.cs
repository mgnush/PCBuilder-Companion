using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;
using System.Threading;

namespace Builder_Companion
{
    public static class QCHandler
    {
        private const string HARD_DRIVE = "3";

        public static void LaunchManualChecks()
        {
            Process.Start("devmgmt.msc");
            Process.Start("diskmgmt.msc");

        }


        public static void FormatDrives()
        {
            //initialize disk first...


            // Create partitions
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"Root/Microsoft/Windows/Storage", "select * from MSFT_DISK");

            string testpath = "";

            foreach (ManagementObject m in searcher.Get())
            {
                testpath = m["Path"].ToString();
                if (m["NumberOfPartitions"].ToString().Equals("0"))
                {
                     //https://docs.microsoft.com/en-au/previous-versions/windows/desktop/stormgmt/createpartition-msft-disk
                    // Use 1MB alignment = 1048576 bytes
                    string createdPartition, ExtendedStatus;
                     //huge = 6
                    var res = m.InvokeMethod("CreatePartition", new object[] { null, true, null, 8192, null, true, 12, null, false, false });
                }
            }

            Thread.Sleep(5000);
            
            // Format 
            searcher = new ManagementObjectSearcher("select * from Win32_Volume");

            foreach (ManagementObject m in searcher.Get())
            {
                if ((m["DriveType"].ToString() == "2") && (m["FileSystem"] == null))
                {
                    // https://docs.microsoft.com/en-au/previous-versions/windows/desktop/stormgmt/format-msft-volume
                    //var res = m.InvokeMethod("Format", new object[] { "FAT32", null, 4096, false, false, false, false, false, false, false });

                    // https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/aa394515(v=vs.85)
                    var res = m.InvokeMethod("Format", new object[] { "FAT32", true, 8192, "MAGNUTS", false });

                    // Returning 1 = not supported
                }
                
            }

            searcher = new ManagementObjectSearcher(@"Root/Microsoft/Windows/Storage", "select * from MSFT_Partition");

            foreach (ManagementObject m in searcher.Get())
            {
                if (m["IsOffline"].ToString().ToLower().Equals("true"))
                {
                    var res = m.InvokeMethod("Online", new object[] { });
                }
            }
        }
    }
}
