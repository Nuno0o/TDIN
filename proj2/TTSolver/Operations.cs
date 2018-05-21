using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace TTSolver
{
    static class Operations
    {
        public static bool sendMessageToDepartment(string department, string message)
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
                q.Send(message);
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;  
        }
    }
}
