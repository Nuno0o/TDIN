using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace TTService
{
    public class TTServ : ITTServ, ITTQueue
    {
        public string HelloWorld(string name)
        {
            return "test"; 
        }
        public void HelloWorld2(string name)
        {
            Console.WriteLine(name);
        }
        public dynamic AddDepartment(string name)
        {
            return Database.AddDepartment(name);
        }
    }
}
