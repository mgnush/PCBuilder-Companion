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
            string resultsPath = Path.Combine(path, "Benchmark/results.txt");

            // Clear results history (from previous builds)
            if (File.Exists(resultsPath))
            {
                File.Delete(resultsPath);
            }

            await Task.Delay(4000);

            try
            {
                var proc = System.Diagnostics.Process.Start(filename, "-t");

                // Check prime logs every minute for 15min
                for (int i = 0; i < 15; i++)
                {
                    await Task.Delay(60000);
                    if (File.Exists(resultsPath))
                    {
                        string results = File.ReadAllText(resultsPath).ToLower();
                        if (results.Contains("hardware failure")) { return false; }
                    }
                }

                proc.CloseMainWindow();
                proc.Close();
            }
            catch
            {
                Prompt.ShowDialog("Prime95 not found", "Error");
                return false;
            }
           
           
            return true; // We made it!
        }
    }
}
