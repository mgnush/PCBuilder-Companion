using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Permissions;
using System.IO;
using System.Diagnostics;

namespace Builder_Companion
{
    // Class for managing asynchronous tasks.
    // Heaven is handled by stand-alone script and does not need to be asynchronous.
    // If logging (temps etc.) need be collected while heaven is runnning, it should
    // be executed asynchronously.
    public static class TaskHandler
    {
        public async static Task<bool> RunPrimeFurmark(int durationMin)
        {            
            Process furmark = FurmarkHandler.InitFurmark(durationMin);
            TempHandler.InitTemp();

            await Task.Delay(2000);
            TempHandler.ReadTemp();
            await Task.Delay(2000);

            Task<bool> primeTask = PrimeHandler.RunPrime(durationMin);

            await Task.WhenAll(primeTask);
            if (!primeTask.Result )
            {
                furmark.CloseMainWindow();
                furmark.Close();
            }

            return primeTask.Result;
        }
    }
}
