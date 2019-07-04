using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCCG_Tester
{
    public static class SystemInfo
    {
        public static GPUInfo Gpu;
        public static CPUInfo Cpu;

        // This method must be called before accessing fields
        public static void RetrieveSystemInfo()
        {
            Gpu = new GPUInfo(DMChecker.GetGPUName(), DMChecker.GetGPUDriver());
            Cpu = new CPUInfo(DMChecker.GetCPUName());
        }

        public class GPUInfo
        {
            private string _name;
            private string _driver;
            private bool nvidia = false; // True for nVIDIA, false for AMD

            public GPUInfo(string gpuname, string gpudriver)
            {
                string family = ""; 
                string model = "";
                string edition = "";
                string[] words = gpuname.Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Equals("GeForce") || words[i].Equals("Radeon"))
                    {
                        family = words[i + 1];
                        model = words[i + 2];
                        if (words.Length > (i + 3))
                        {
                            edition = words[i + 3];
                        }
                        if (words[i].Equals("GeForce")) { nvidia = true; }
                        break;
                    }                    
                }
                _name = family + " " + model + " " + edition;

                if (nvidia)
                {
                    string[] numbers = gpudriver.Split('.');
                    _driver = numbers[2].Substring(1) + numbers[3].Substring(0, 2) + "." + numbers[3].Substring(2, 2);
                } else
                {
                    _driver = "Unknown driver";
                }
               
            }

            public string Name { get => _name;}
            public string Driver { get => _driver; }

            public bool HeavenScoreValidation(int score)
            {
                return true;
            }
        }

        public class CPUInfo
        {
            private string _name;

            public CPUInfo(string name)
            {
                string[] words = name.Split('_');
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].ToLower().Contains("intel(r)"))
                    {
                        _name = words[i + 2];
                        return;
                    }
                    /*else if (words[i].Contains("AMD"))
                    {
                        _name = words[i + 1] + " " + words[i + 2] + " " +  words[i + 3];
                        return;
                    }*/
                }
                _name = "Unknown CPU";
            }

            public string Name { get => _name; }
        }
    }    
}
