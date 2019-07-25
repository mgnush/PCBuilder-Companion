/*
 * TaskHandler.cs
 * 
 * @Author  Magnus Hjorth
 * 
 * File Description: This class manages all asynchronous stress testing methods
 */

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
    public static class TaskHandler
    {
        /// <summary>
        /// Runs Prime95 and FurMark for a specified duration. If Prime fails before both has finished,
        /// Both programs are terminated.
        /// </summary>
        /// <param name="durationMin">The duration to run the programs for in minutes.</param>
        /// <returns>True if Prime completed without errors, false if prime errors.</returns>
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
