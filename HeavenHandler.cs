/*
 * HeavenHandler.cs
 * 
 * @Author  Magnus Hjorth
 * 
 * File Description: This class will start heaven when initiated, and retrieve the saved score
 * when heaven has finished. When the score has been retrieved, it will invoke the mainform to
 * report the score.
 */

using System;
using System.IO;
using System.Diagnostics;
using HtmlAgilityPack;


namespace Builder_Companion
{
    public class HeavenHandler 
    {
        public delegate void HeavenHandle();
        public HeavenHandle heavenHandle;
        private int _score = 0;
        private Process heavenScript;

        public int Score { get => _score; set => _score = value; }

        /// <summary>
        /// Launches Heaven and starts listening for the results to be saved
        /// </summary>
        public HeavenHandler(Form1 mainform)
        {
            string heavenScriptPath = Path.Combine(Paths.Desktop(), Paths.TEST, Paths.FILES, Paths.HEAVEN_SCRIPT);
            string heavenBat = Path.Combine(Paths.Desktop(), Paths.TEST, Paths.HEAVEN_BAT);

            if (!(File.Exists(heavenScriptPath) && File.Exists(heavenBat)))
            {
                Prompt.ShowDialog("Heaven Script or bat not found", "Error");
                return;                
            }
                        
            ProcessStartInfo pInfo = new ProcessStartInfo();
            pInfo.FileName = heavenScriptPath;
            pInfo.WorkingDirectory = Path.Combine(Paths.Desktop(), Paths.TEST);
            heavenScript = new Process();
            heavenScript = Process.Start(pInfo);            

            heavenHandle = new HeavenHandle(mainform.ReportHeavenScore);
            WatchForScore(mainform);
        }

        private void WatchForScore(Form1 mainform)
        {
            string resultFolder = Paths.User();   // Script saves at default location (user folder)
            string partialName = "Unigine_Heaven_Benchmark*.html";         
                     
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = resultFolder;
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = partialName;
            // Using an anonymous event handler here means we can't actually unsubscribe!
            watcher.Created += delegate (object sender, FileSystemEventArgs e) { EvaluateHeaven(sender, e, mainform); };
            //watcher.Created += new FileSystemEventHandler(EvaluateHeaven);
            watcher.EnableRaisingEvents = true;
        }

        private void EvaluateHeaven(object source, FileSystemEventArgs e, Form1 mainform)
        {
            if (!e.Name.Contains("Unigine_Heaven_Benchmark"))
            {
                return;
            }

            heavenScript.Kill();
            Process.Start(Path.Combine(Paths.Desktop(), Paths.TEST, Paths.FILES, Paths.HEAVEN_EXIT_SCRIPT));

            var resultFile = new HtmlDocument();
            resultFile.Load(e.FullPath);
            string result = resultFile.DocumentNode.SelectSingleNode("//body/table/tr/td[text()='Score:']").NextSibling.InnerText;
            if (!Int32.TryParse(result, out _score))
            {
                Prompt.ShowDialog("Heaven Benchmark not read correctly", "Error");
            }

            File.Delete(e.FullPath);

            mainform.Invoke(this.heavenHandle);
        }

    }
}
