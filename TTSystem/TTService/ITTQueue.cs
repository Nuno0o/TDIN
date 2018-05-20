using System.ServiceModel;

namespace TTService
{
    [ServiceContract]
    interface ITTQueue
    {
        [OperationContract(IsOneWay = true)]
        void HelloWorld2(string name);
    }
}
