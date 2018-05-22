using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace TTService
{
    
    interface ITTServEvents
    {
        [OperationContract(IsOneWay = true)]
        void EventCall();
    }
}
