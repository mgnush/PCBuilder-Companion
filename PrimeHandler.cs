/*
 * PrimeHandler.cs
 * 
 * @Author  Magnus Hjorth
 * 
 * File Description: This class holds all static methods to run and interact with Prime95.
 * Running and evaluating is done as a single sequence of actions with a large asynchronous wait,
 * rather than calling an evaluation method once the caller has terminated Prime95.
 */
 
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Builder_Companion
{
    public static class PrimeHandler
    {
        /// <summary>
        /// Runs Prime95 and continuously checks the results file for errors.
        /// </summary>
        /// <returns>True if no errors, false if errors or IO errors.</returns>
        public static async Task<bool> RunPrime(int durationMin)
        {            
            string filename = Path.Combine(Paths.Desktop(), Paths.TEST, Paths.PRIME_EXE);
            string resultsPath = Path.Combine(Paths.Desktop(), Paths.TEST, Paths.PRIME_RESULT);

            // Clear results history (from previous builds / prior failed session)
            if (File.Exists(resultsPath))
            {
                File.Delete(resultsPath);
            }

            if (!File.Exists(filename))
            {
                Prompt.ShowDialog("Prime95 not found", "Error");
                return false;
            }

      
            Process proc = Process.Start(filename, "-t");

            // Check prime logs every minute
            for (int i = 0; i < durationMin; i++)
            {
                await Task.Delay(60000);
                if (File.Exists(resultsPath))
                {
                    string results = File.ReadAllText(resultsPath).ToLower();
                    if (results.Contains("hardware failure"))
                    {
                        proc.CloseMainWindow();
                        proc.Close();
                        return false;
                    }
                }
            }

            proc.CloseMainWindow();
            proc.Close();   
           
            return true; // We made it!
        }
    }
}
