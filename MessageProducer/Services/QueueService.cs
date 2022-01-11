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
            Queue1 = new QueueClient(@"DefaultEndpointsProtocol=https;AccountName=porkhovskyi;AccountKey=SzTqiO7s0CGnO80+3pzbCW9GT6nFDrIfFUpvMHIK4lhYx7j92SpxNQ+tmKfJdImmBROTBi8qyaJJDTmmA4iE8Q==;BlobEndpoint=https://porkhovskyi.blob.core.windows.net/;QueueEndpoint=https://porkhovskyi.queue.core.windows.net/;TableEndpoint=https://porkhovskyi.table.core.windows.net/;FileEndpoint=https://porkhovskyi.file.core.windows.net/;", "queue1");
            Queue1.CreateIfNotExistsAsync().Wait();
        }
    }
}
