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
        private const string _path = "C://Users/Mgnus/Desktop/AAA Testing";

        public async static Task<bool> RunPrimeFurmark()
        {
            TempHandler.InitTemp(_path);
            FurmarkHandler.InitFurmark(_path);
            Task<bool> primeTask = PrimeHandler.RunPrime(_path);

            await Task.WhenAll(primeTask);
            return primeTask.Result;
        }

        public async static Task<bool> RunHeaven()
        {
            HeavenHandler.InitHeaven(_path);
            await Task.Delay(14000 * 2 + 300000 + (1000 * 3));   // This delay matches the heaven run-time script

            return true;
        }
    }
}
