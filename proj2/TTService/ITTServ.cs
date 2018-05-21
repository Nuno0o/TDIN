﻿using System.ServiceModel;

namespace TTService
{
    [ServiceContract]
    public interface ITTServ
    {
        [OperationContract]
        string InitDb(bool overwrite);

        [OperationContract]
        string HelloWorld(string name);

        [OperationContract]
        dynamic AddDepartment(string name);
    }

    public interface ITTServ2
    {

    }
}
