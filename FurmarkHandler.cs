/*
 * FurmarkHandler.cs
 * 
 * @Author  Magnus Hjorth
 * 
 * File Description: All static methods needed to launch and interact with FurMark3D
 */

using System.IO;
using System.Xml;

using System.Diagnostics;

namespace Builder_Companion
{
    public static class FurmarkHandler
    {
        public static Process InitFurmark(int durationMin)
        {
            string filename = Path.Combine(Paths.Desktop(), Paths.TEST, Paths.FURMARK_EXE);
            string xmlPath = Path.Combine(Paths.Desktop(), Paths.TEST, Paths.FURMARK_XML);
            string tempPath = Path.Combine(Paths.Desktop(), Paths.TEST, Paths.FURMARK_TEMP);

            // Clear old temp results to prepare for temp readings handled by TempHandler
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }

            XmlDocument fmSettings = new XmlDocument();
            try {
                fmSettings.Load(xmlPath);
            } catch
            {
                Prompt.ShowDialog("Furmark startup_option file not found", "Error");
                return null;
            }
            
            XmlNode root = fmSettings.DocumentElement;

            int durationMs = durationMin * 60 * 1000;

            // Change options
            root.SelectSingleNode("startup_options/@width").Value = "1280";
            root.SelectSingleNode("startup_options/@height").Value = "720";
            root.SelectSingleNode("startup_options/@msaa").Value = "8";
            root.SelectSingleNode("startup_options/@fullscreen").Value = "0";
            root.SelectSingleNode("startup_options/@run_mode").Value = "BENCHMARK";
            root.SelectSingleNode("startup_options/@time_based_benchmark").Value = "1";
            root.SelectSingleNode("startup_options/@max_time").Value = durationMs.ToString();
            root.SelectSingleNode("startup_options/@burn_in").Value = "1";
            root.SelectSingleNode("startup_options/@log_temperature").Value = "1";
            
            fmSettings.Save(xmlPath);

            if (!File.Exists(filename))
            {
                Prompt.ShowDialog("Furmark application not found", "Error");
            }

            Process proc = Process.Start(filename, "/enable_dyn_bkg=1 /bkg_img_id=2 /nogui");

            return proc;
        }
    }
}
