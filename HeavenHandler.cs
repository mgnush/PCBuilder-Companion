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

namespace PCCG_Tester
{
    public static class HeavenHandler
    {
        public static void InitHeaven(string path)
        {
            string filename = Path.Combine(path, "UAC_CONTROL.exe");
            ProcessStartInfo pInfo = new ProcessStartInfo();
            pInfo.FileName = filename;
            pInfo.WorkingDirectory = path;

            Process proc = Process.Start(pInfo);

        }

        public static int EvaluateHeaven()
        {
            // Get the most recent benchmark file
            string resultFolder = "C://Users/Mgnus";
            string partialName = "Unigine_Heaven_Benchmark";
            DirectoryInfo folder = new DirectoryInfo(resultFolder);
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
