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
        Updating,
        QCReady, 
        QC
    }
}
