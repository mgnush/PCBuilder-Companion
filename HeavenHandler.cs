using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PCCG_Tester
{
    public static class HeavenHandler
    {
        public static void InitHeaven(string path)
        {
            string filename = Path.Combine(path, "UAC_CONTROL.exe");

            var proc = System.Diagnostics.Process.Start(filename);

        }
    }
}
