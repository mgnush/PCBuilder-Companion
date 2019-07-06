using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Principal;

namespace PCCG_Tester
{
    // This class collects all installation methods to be executed by stand-alone scripts.
    // Listen for script process to determine progress. 
    // Use enum? 
    public static class RGBInstaller
    {
        public static List<string> software = new List<string>();
        private static List<string> rgbPath = new List<string>();

        public static void ReadRGBSoftware()
        {          
            // Load in RGB options specified on server file
            string rgbXmlPath = Path.Combine(Paths.TEST, Paths.RGB_XML);

            XmlDocument rgbXml = new XmlDocument();
            try
            {
                rgbXml.Load(rgbXmlPath);
            }
            catch
            {
                Prompt.ShowDialog("No RGB xml file found", "Error");
                return;
            }

            XmlNode root = rgbXml.DocumentElement;

            foreach (XmlNode node in root.ChildNodes)
            {
                software.Add(node.SelectSingleNode(".//GUI").InnerText);
                rgbPath.Add(node.SelectSingleNode(".//FILENAME").InnerText);
            }
        }

        public static void InstallSelectedSoftware(ListBox.SelectedIndexCollection indeces)
        {
            PullSoftware(indeces);

            string scriptPath = "";
            ProcessStartInfo pInfo;
            Process script;

            foreach (int index in indeces)
            {
                scriptPath = Path.Combine(Paths.TEST, rgbPath.ElementAt(index));
                pInfo = new ProcessStartInfo();
                pInfo.FileName = scriptPath;
                pInfo.WorkingDirectory = Paths.TEST;
                try
                {
                    script = Process.Start(pInfo);
                    script.WaitForExit();   // Stall entire program until the software is installed
                }
                catch (Exception e)
                {
                    Console.WriteLine("File {0} not found.", scriptPath);
                }     

            }
        }

        private static void PullSoftware(ListBox.SelectedIndexCollection indeces)
        {            
            using (new Impersonator ("BUILDER", Environment.MachineName, "pxe"))
            {
                string softwarePath = "";
                foreach (int index in indeces)
                {
                    softwarePath = Path.Combine(Paths.NAS, software.ElementAt(index));   // Software names in xml must match folder names on NAS!
                    File.Copy(softwarePath, Paths.TEST, true);
                }
            }
        }
    }
 
}
