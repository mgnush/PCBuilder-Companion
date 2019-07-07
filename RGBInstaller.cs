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
    public static class RGBInstaller
    {
        public static List<string> software = new List<string>();   // Software name as defined in xml
        private static List<string> setupName = new List<string>();   // Software setup filename as defined in xml
        private static List<string> scriptName = new List<string>();   // Script name as defined in xml

        public static void ReadRGBSoftware()
        {          
            // Load in RGB options specified on server file
            string rgbXmlPath = Path.Combine(Paths.TEST, Paths.FILES, Paths.RGB_XML);

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
                setupName.Add(node.SelectSingleNode(".//FILENAME").InnerText);
                scriptName.Add(node.SelectSingleNode(".//SCRIPTNAME").InnerText);
            }
        }

        public static void InstallSelectedSoftware(CheckedListBox.CheckedIndexCollection indeces)
        {
            PullSoftware(indeces);

            string scriptPath = "";
            string setupFolder = "";
            string setupPath = "";
            ProcessStartInfo pInfo;
            Process script;

            foreach (int index in indeces)
            {
                scriptPath = Path.Combine(Paths.TEST, Paths.RGB_SCRIPTS, scriptName.ElementAt(index));
                setupFolder = Path.Combine(Paths.RGB, software.ElementAt(index));

                // Find the (newest) setup file
                foreach (string file in Directory.GetFiles(setupFolder, setupName.ElementAt(index), SearchOption.AllDirectories))
                {
                    string[] partialPaths = file.Split('/');
                    setupPath = partialPaths.Last();
                }

                if (File.Exists(scriptPath))
                {
                    pInfo = new ProcessStartInfo();
                    pInfo.FileName = scriptPath;
                    pInfo.WorkingDirectory = Paths.DESKTOP;
                    pInfo.Arguments = "\"" + setupPath + "\"";
                    script = Process.Start(pInfo);
                    script.WaitForExit();   // Stall entire program until the software is installed
                } else
                {
                    Prompt.ShowDialog(scriptName.ElementAt(index) + "\n Does not exist, install manually", "Warning");
                }
               
            }
        }

        private static void PullSoftware(CheckedListBox.CheckedIndexCollection indeces)
        {  
            if (!File.Exists(Paths.RGB))
            {
                Directory.CreateDirectory(Paths.RGB);
            }

            using (new Impersonator ("BUILDER", Environment.MachineName, "pxe"))
            {
                string softwarePath = "";
                string destPath = "";
                foreach (int index in indeces)
                {
                    softwarePath = Path.Combine(Paths.NAS, software.ElementAt(index));   // Software names in xml must match folder names on NAS!
                    destPath = Path.Combine(Paths.RGB, software.ElementAt(index));
                    File.Copy(softwarePath, destPath, true);
                }
            }
        }
    }
 
}
