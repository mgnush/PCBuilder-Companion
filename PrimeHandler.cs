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
using System.Text;

namespace Builder_Companion
{
    public static class PrimeHandler
    {
        private static string primeResults = Path.Combine(Paths.Desktop(), Paths.TEST, Paths.PRIME_RESULT);
        private static Process prime;
        public static bool primeOK = true;
        /// <summary>
        /// Runs Prime95 and continuously checks the results file for errors.
        /// </summary>
        /// <returns>True if no errors, false if errors or IO errors.</returns>
        public static async Task<bool> RunPrime(int durationMin)
        {            
            string filename = Path.Combine(Paths.Desktop(), Paths.TEST, Paths.PRIME_EXE);

            // Clear results history (from previous builds / prior failed session)
            if (File.Exists(primeResults))
            {
                File.Delete(primeResults);
            }

            if (!File.Exists(filename))
            {
                Prompt.ShowDialog("Prime95 not found", "Error");
                return false;
            }

      
            prime = Process.Start(filename, "-t");

            // Trigger read every time prime updates results
            FileSystemWatcher watcher = new FileSystemWatcher();
            FileSystemEventHandler fsHandler = new FileSystemEventHandler(PrimeUpdate);
            watcher.Path = Path.Combine(Paths.Desktop(), Paths.TEST, Paths.BENCHMARK);
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "results.txt";
            watcher.Created += fsHandler;
            watcher.Changed += fsHandler;
            watcher.EnableRaisingEvents = true;

            // Check prime logs
            for (int i = 0; i < (durationMin * 6); i++)
            {
                await Task.Delay(10000);
                if (!primeOK)
                {
                    watcher.EnableRaisingEvents = false;

                    prime.CloseMainWindow();
                    prime.Close();

                    return false;
                }
            }

            watcher.EnableRaisingEvents = false;

            prime.CloseMainWindow();
            prime.Close();   
           
            return true;   // We made it!
        }

        private static void PrimeUpdate(object sender, FileSystemEventArgs e)
        {
            string results = "";

            using (var fs = new FileStream(primeResults, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs, Encoding.Default))
            {
                try {
                    results = sr.ReadToEnd();
                } catch
                {
                    Prompt.ShowDialog("Prime results file too large", "Error");
                }
                

                if (results.ToLower().Contains("hardware failure"))
                {
                    primeOK = false;
                }                
            }
        }
    }
}
