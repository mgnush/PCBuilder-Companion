using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace PCCG_Tester
{
    public static class HeavenHandler
    {
        public static void InitHeaven(string path)
        {
            string filename = Path.Combine(path, "UAC_CONTROL.exe");
            ProcessStartInfo pInfo = new ProcessStartInfo();
            pInfo.FileName = filename;
            pInfo.WorkingDirectory = path;

            Process proc = Process.Start(pInfo);

        }
    }
}
