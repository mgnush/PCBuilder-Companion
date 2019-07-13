using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Management;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;

namespace Builder_Companion
{
    public static class Paths
    {
        // Desktop & desktop folders
        public static string User()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).ToString();
        }
        public static string Desktop()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString();
        }
        public const string TEST = "AAA Testing";
        public const string RGB = "RGB";

        // Programs & settings
        public const string FILES = "Companion Files";
        public const string RGB_SCRIPTS = "Companion Files/RGB Scripts";
        public const string FURMARK_EXE = "Benchmark/FurMark.exe";
        public const string FURMARK_XML = "Benchmark/startup_options.xml";
        public const string FURMARK_TEMP = "Benchmark/furmark-gpu-monitoring.xml";
        public const string PRIME_EXE = "Benchmark/prime95.exe";
        public const string PRIME_RESULT = "Benchmark/results.txt";
        public const string HEAVEN_BAT = "Benchmark/Heaven/heaven.bat";
        public const string CT_FOLDER = "CoreTemp64";
        public const string CT_EXE = "CoreTemp64/Core Temp.exe";
        public const string HWMONITOR_EXE = "Benchmark/HWMonitor_x64.exe";
        public const string HWMONITOR_INI = "Benchmark/hwmonitorw.ini";
        public const string RGB_XML = "rgbs.xml";

        // Scripts
        public const string HEAVEN_SCRIPT = "UAC_CONTROL.exe";
        public const string TEMP_SCRIPT = "TEMP_LOGGING.exe";
        public const string SYSTEM_SCRIPT = "SYSTEM_LAUNCH.exe";

        // NAS / Server
        public const string NAS = @"\\CustServiceNAS\SysbBuilds\_Software";
    }
}
