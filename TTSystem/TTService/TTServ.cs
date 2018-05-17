using System;
using System.Collections.Generic;
using System.Linq;
namespace TTService
{
    public class TTServ : ITTServ
    {
        public string HelloWorld(string name)
        {
            return "Hello world! My name is " + name + "!"; 
        }
    }
}
