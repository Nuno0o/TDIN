using System.ServiceModel;

namespace TTService
{
    [ServiceContract]
    public interface IAuthServ
    {
        [OperationContract]
        string login(string email, string hash);

        [OperationContract]
        string register(string name, string email, string hash, string salt, int department);

        [OperationContract]
        string logout(string token);

        [OperationContract]
        string getSalt(string email);
    }
}
