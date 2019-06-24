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

            string filename = Path.Combine(path, "CoreTemp64/Core Temp.exe");


            //INIFile tempConfig = new INIFile(@"C://Users/Mgnus/Desktop/CoreTemp64/CoreTemp.ini");
            //tempConfig.Write("EnLog", "1", "Core temp settings");

            var proc = System.Diagnostics.Process.Start(filename);

        //enable logging
        
        //tempConfig.DeleteKey("Core Temp settings", "EnLog");
        //bool moo = tempConfig.KeyExists("EnLog", "Core Temp settings");
        
        }
    }    
     
}
