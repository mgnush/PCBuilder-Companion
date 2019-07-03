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
        public async static Task<bool> RunPrimeFurmark()
        {            
            FurmarkHandler.InitFurmark();
            TempHandler.InitTemp();

            await Task.Delay(2000);
            TempHandler.ReadTemp();
            await Task.Delay(2000);

            Task<bool> primeTask = PrimeHandler.RunPrime();

            await Task.WhenAll(primeTask);

            return primeTask.Result;
        }

        public async static Task<bool> RunHeaven()
        {
            HeavenHandler.InitHeaven();
            await Task.Delay(14000 * 2 + 300000 + (1000 * 3));   // This delay matches the heaven run-time script

            return true;
        }
    }
}
