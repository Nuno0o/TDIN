using System.ServiceModel;

namespace TTService
{
    [ServiceContract]
    public interface ITTServ
    {
        [OperationContract]
        string HelloWorld(string name);

        [OperationContract]
        dynamic AddDepartment(string name);

        [OperationContract]
        dynamic AddTicket(string title, string description, int author, int? parent);

        [OperationContract]
        dynamic AssignTicket(int id, int assignee);

        [OperationContract]
        dynamic GetTicket(int id);
    }

    public interface ITTServ2
    {

    }
}
