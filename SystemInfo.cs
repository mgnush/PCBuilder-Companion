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

        // This method must be called before accessing fields
        public static void RetrieveSystemInfo()
        {
            Gpu = new GPUInfo(DMChecker.GetGPUName());
        }

        public class GPUInfo
        {
            private string _name;

            public GPUInfo(string name)
            {
                string family = ""; 
                string model = "";
                string edition = "";
                string[] words = name.Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Equals("GeForce") || words[i].Equals("Radeon"))
                    {
                        family = words[i + 1];
                        model = words[i + 2];
                        if (words.Length >= (i + 3))
                        {
                            edition = words[i + 3];
                        }
                    }
                }
                _name = family + " " + model + " " + edition;
            }

            public string Name { get => _name; set => _name = value; }

            public bool HeavenScoreValidation(int score)
            {
                return true;
            }
        }
    }

    
}
