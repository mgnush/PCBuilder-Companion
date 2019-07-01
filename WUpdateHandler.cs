using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCCG_Tester
{
    public static class WUpdateHandler
    {
        public static void EnableAutoUpdates()
        {
            WUApiLib.AutomaticUpdates auc = new WUApiLib.AutomaticUpdates();
            auc.EnableService();
        }

    }
}
