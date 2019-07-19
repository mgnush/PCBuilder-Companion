using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Xml;
using HtmlAgilityPack;
using System.Threading;

namespace Builder_Companion
{
    public static class HeavenHandler
    {
        // Launch heaven stand-alone script
        public static bool RunHeaven(Form1 mainform)
        {
            string heavenScript = Path.Combine(Paths.Desktop(), Paths.TEST, Paths.FILES, Paths.HEAVEN_SCRIPT);
            string heavenBat = Path.Combine(Paths.Desktop(), Paths.TEST, Paths.HEAVEN_BAT);

            if (!(File.Exists(heavenScript) && File.Exists(heavenBat)))
            {
                Prompt.ShowDialog("Heaven Script or bat not found", "Error");
                return false;                
            }
                        
            ProcessStartInfo pInfo = new ProcessStartInfo();
            pInfo.FileName = heavenScript;
            pInfo.WorkingDirectory = Path.Combine(Paths.Desktop(), Paths.TEST);
            Process proc = Process.Start(pInfo);

            //WatchForScore(proc, mainform);
            proc.WaitForExit();

            return true;
        }

        private static void WatchForScore(Process heavenScript, Form1 mainform)
        {
            string resultFolder = Paths.User();   // Script saves at default location (user folder)
            string partialName = "Unigine_Heaven_Benchmark*.html";         
                     
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = resultFolder;
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = partialName;
            // Using an anonymous event handler here means we can't actually unsubscribe!
            watcher.Created += delegate (object sender, FileSystemEventArgs e) { EvaluateHeaven(sender, e, heavenScript, mainform); };
            watcher.EnableRaisingEvents = true;
        }

        private static void EvaluateHeaven(object source, FileSystemEventArgs e, Process heavenScript, Form1 mainform)
        {
            if (!e.Name.Contains("Unigine_Heaven_Benchmark"))
            {
                return;
            }

            heavenScript.Close();
            Process.Start(Path.Combine(Paths.Desktop(), Paths.FILES, Paths.HEAVEN_EXIT_SCRIPT));

            var resultFile = new HtmlDocument();
            resultFile.Load(e.FullPath);
            string result = resultFile.DocumentNode.SelectSingleNode("//body/table/tr/td[text()='Score:']").NextSibling.InnerText;
            int score = 0;
            if (!Int32.TryParse(result, out score))
            {
                Prompt.ShowDialog("Heaven Benchmark not read correctly", "Error");
            }

            File.Delete(e.FullPath);

            mainform.ReportHeavenScore(score);
        }

        /// <summary>

        /// Retrieves the heaven score via the saved html file

        /// </summary>

        /// <param name="durationMin">The duration to run prime & furmark for</param>

        /// <returns></returns>

        public static int EvaluateHeaven()

        {

            // Get the most recent benchmark file

            string resultFolder = Paths.User();   // Script saves at default location (user folder)

            string partialName = "Unigine_Heaven_Benchmark";



            DirectoryInfo folder = new DirectoryInfo(resultFolder);

            if (!folder.Exists)

            {

                Prompt.ShowDialog("Heaven Benchmark folder does not exist", "Error");

                return 0;

            }

            FileInfo[] results = folder.GetFiles(partialName + "*.*");

            if (results.Length == 0)

            {

                Prompt.ShowDialog("No Heaven benchmark was found", "Error");

                return 0;

            }

            string fullName = results.Last().FullName;



            // Retrieve the score from the file

            var resultFile = new HtmlDocument();

            resultFile.Load(fullName);

            string result = resultFile.DocumentNode.SelectSingleNode("//body/table/tr/td[text()='Score:']").NextSibling.InnerText;



            int score = 0;

            if (!Int32.TryParse(result, out score))

            {

                Prompt.ShowDialog("Heaven Benchmark not read correctly", "Error");

            }



            // Delete benchmark file

            if (File.Exists(fullName))

            {

                File.Delete(fullName);

            }



            return score;

        }

    }
}
