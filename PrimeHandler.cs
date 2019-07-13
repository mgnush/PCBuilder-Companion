using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Timers;
using System.Runtime;

namespace Builder_Companion
{
    public static class PrimeHandler
    {               
        public static async Task<bool> RunPrime(int durationMin)
        {            
            string filename = Path.Combine(Paths.Desktop(), Paths.TEST, Paths.PRIME_EXE);
            string resultsPath = Path.Combine(Paths.Desktop(), Paths.TEST, Paths.PRIME_RESULT);

            // Clear results history (from previous builds / prior failed session)
            if (File.Exists(resultsPath))
            {
                File.Delete(resultsPath);
            }

            try
            {
                var proc = System.Diagnostics.Process.Start(filename, "-t");

                // Check prime logs every minute
                for (int i = 0; i < durationMin; i++)
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
