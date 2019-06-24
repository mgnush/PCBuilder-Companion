using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Timers;
using System.Runtime;

namespace PCCG_Tester
{
    public static class PrimeHandler
    {
        //string filename = "C://Users/User/Desktop/AAA Testing/Prime95/prime95.exe";
               
        public static async Task<bool> RunPrime(string path)
        {            
            string filename = Path.Combine(path, "Prime95/prime95.exe");
            var proc = System.Diagnostics.Process.Start(filename, "-t");

            await Task.Delay(5000);
            proc.CloseMainWindow();
            proc.Close();

            string resultsPath = Path.Combine(path, "results.txt");
            if (File.Exists(resultsPath))
            {
                string results = File.ReadAllText(resultsPath);
                return !results.Contains("Possible hardware failure");
            }
            return true;
        }
    }
}
