using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace TTService
{
    [ServiceContract(CallbackContract = typeof(ITTServEvents))]
    interface ITTServContracts
    {
        [OperationContract]
        void Subscribe();
    }
}
