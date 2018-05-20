using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace TTDepartment
{
    static class Operations
    {
        public static bool receiveMessageDepartment(string department)
        {
            try
            {
                string path = ".\\private$\\dep" + department;
                if (!MessageQueue.Exists(path))
                {
                    MessageQueue.Create(path);
                }
                MessageQueue q = new MessageQueue(path);
                q.Formatter = new BinaryMessageFormatter();
                Message[] m = q.GetAllMessages();
                Console.WriteLine(m.Length);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;


        }
    }
}
