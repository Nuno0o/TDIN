﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTDepartment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Operations.receiveMessageDepartment("it2");
        }
    }
}
