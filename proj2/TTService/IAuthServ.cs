using System.ServiceModel;

namespace TTService
{
    [ServiceContract]
    public interface IAuthServ
    {
        [OperationContract]
        string Login(string email, string hash);

        [OperationContract]
        string Register(string name, string email, string hash, string salt, int department);

        [OperationContract]
        string Logout(string token);

        [OperationContract]
        string GetSalt(string email);
    }
}
