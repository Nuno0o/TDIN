using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSolver
{
    class TTCallback:TTCallbacks.ITTServContractsCallback
    {
        public void EventCall()
        {
            Operations.getInformation();
            Operations.updateInterface();
        }
    }
}
