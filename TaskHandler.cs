using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Permissions;

namespace PCCG_Tester
{
    public static class TaskHandler
    {
        private const string _path = "C://Users/User/Desktop/AAA Testing";

        public async static Task<bool> RunPrimeFurmark()
        {
            TempHandler.InitTemp(_path);
            FurmarkHandler.InitFurmark(_path);
            Task<bool> primeTask = PrimeHandler.RunPrime(_path);

            await Task.WhenAll(primeTask);
            return primeTask.Result;
        }

        public static void RunHeaven()
        {
            HeavenHandler.InitHeaven(_path);
        }
    }
}
