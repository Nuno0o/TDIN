﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace TTService
{
    public class TTServ : ITTServ
    {
        public string HelloWorld(string name)
        {
            return "test"; 
        }
    }
}
