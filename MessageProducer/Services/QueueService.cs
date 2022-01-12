using Azure.Storage.Queues;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageProducer.Services
{
    public class QueueService
    {
        public QueueClient Queue1 { get; set; }

        public QueueService()
        {
            Queue1 = new QueueClient(@"connection string", "queue1");
            Queue1.CreateIfNotExistsAsync().Wait();
        }
    }
}
