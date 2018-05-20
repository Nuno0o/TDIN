using System.ServiceModel;

namespace TTService
{
    [ServiceContract]
    public interface ITTQueue
    {
        [OperationContract(IsOneWay = true)]
        void HelloWorld2(string name);
    }
}
