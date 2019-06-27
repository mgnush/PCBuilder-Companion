using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace PCCG_Tester
{
    public static class FurmarkHandler
    {
        public static void InitFurmark(string path)
        {
            string filename = Path.Combine(path, "Benchmark/FurMark.exe");
            string xmlPath = Path.Combine(path, "Benchmark/startup_options.xml");

            XmlDocument fmSettings = new XmlDocument();
            fmSettings.Load(xmlPath);
            XmlNode root = fmSettings.DocumentElement;

            // Change options
            root.SelectSingleNode("startup_options/@width").Value = "1280";
            root.SelectSingleNode("startup_options/@height").Value = "720";
            root.SelectSingleNode("startup_options/@msaa").Value = "8";
            root.SelectSingleNode("startup_options/@fullscreen").Value = "0";
            root.SelectSingleNode("startup_options/@run_mode").Value = "BENCHMARK";
            root.SelectSingleNode("startup_options/@time_based_benchmark").Value = "1";
            root.SelectSingleNode("startup_options/@max_time").Value = "900000";
            root.SelectSingleNode("startup_options/@burn_in").Value = "1";
            root.SelectSingleNode("startup_options/@log_temperature").Value = "1";
            
            fmSettings.Save(xmlPath);


            var proc = System.Diagnostics.Process.Start(filename, "/enable_dyn_bkg=1 /bkg_img_id=2 /nogui");
        }
    }
}
