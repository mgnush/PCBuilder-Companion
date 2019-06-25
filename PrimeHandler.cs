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
        public static async Task<bool> RunPrime(string path)
        {            
            string filename = Path.Combine(path, "Benchmark/prime95.exe");
            await Task.Delay(4000);
            var proc = System.Diagnostics.Process.Start(filename, "-t");

            await Task.Delay(900000);
            proc.CloseMainWindow();
            proc.Close();

            string resultsPath = Path.Combine(path, "Benchmark/results.txt");
            if (File.Exists(resultsPath))
            {
                string results = File.ReadAllText(resultsPath).ToLower();
                return !results.Contains("hardware failure");
            }
            return true;
        }
    }
}
