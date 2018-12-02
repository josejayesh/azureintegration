using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Azure;
using ConsoleApp1.Entites;
using Microsoft.WindowsAzure.Storage.Queue;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var cloudStorage = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnection"));
            CloudQueueClient queueClient = cloudStorage.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("webqueue");
            queue.CreateIfNotExists();
            //insert a message to quque 
            //CloudQueueMessage msg = new CloudQueueMessage("Hello World");
           // queue.AddMessage(msg);

            //TTL 
            //var ttl = new TimeSpan(24, 0, 0);
            //CloudQueueMessage msg1 = new CloudQueueMessage("Hello World");
            //queue.AddMessage(msg1,ttl);

            CloudQueueMessage qu = queue.PeekMessage();
            Console.WriteLine(qu.AsString);


            //Get Message from Queue 
            //var msg3 = queue.GetMessage();
            //Console.WriteLine(msg3.AsString);
            //queue.DeleteMessage(msg3);

            Console.ReadKey();



        }
         
    }
}

