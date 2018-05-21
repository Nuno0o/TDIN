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
        public static string department = "";
        public static Message[] messages;
        public static bool receiveMessageDepartment()
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
                messages = m;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;


        }
        public static void beginAsyncReceiveMessages()
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
                q.PeekCompleted += QueuePeeker;
                q.BeginPeek();
            }
            catch (Exception ex)
            {

            }
        }
        public static void removeMessageFromQueue(string id)
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
                Message m = q.ReceiveById(id);
                Console.WriteLine(m.Body);
            }
            catch (Exception ex)
            {

            }
        }
        public static void QueuePeeker(object src, PeekCompletedEventArgs rcea)
        {
            MessageQueue q = (MessageQueue)src;
            Message m = q.EndReceive(rcea.AsyncResult);
            receiveMessageDepartment();
            q.BeginPeek();
        }
    }
}
