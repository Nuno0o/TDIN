using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace TTService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TTCallbacks : ITTServContracts
    {
        #region events

        static Action m_event = delegate { };

        public void Subscribe()
        {
            ITTServEvents subscriber = OperationContext.Current.GetCallbackChannel<ITTServEvents>();
            m_event += subscriber.EventCall;
        }

        public void Unsubscribe()
        {
            ITTServEvents subscriber = OperationContext.Current.GetCallbackChannel<ITTServEvents>();
            m_event -= subscriber.EventCall;
        }

        #endregion
    }
}
