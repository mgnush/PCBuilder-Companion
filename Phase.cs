/*
 * Phase.cs
 * 
 * @Author  Magnus Hjorth
 * 
 * File Description: This enumeration specifies all phases that the program can operate in.
 * The current phase is saved as a default setting.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_Companion
{
    [Serializable]
    public enum Phase
    {
        Testing,
        Benchmarking,
        Updating,
        QCReady, 
        QC,
        Exiting
    }

    public enum TestConfig
    {
        FullPrep,
        TestOnly,
        PrimeFurmark
    }
}
