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

            //HWMonitor for details, does not support logging natively
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



            //INIFile tempConfig = new INIFile(@"C://Users/Mgnus/Desktop/CoreTemp64/CoreTemp.ini");
            //tempConfig.Write("EnLog", "1", "Core temp settings");



            //enable logging

            //tempConfig.DeleteKey("Core Temp settings", "EnLog");
            //bool moo = tempConfig.KeyExists("EnLog", "Core Temp settings");

        }
    }    
     
}
