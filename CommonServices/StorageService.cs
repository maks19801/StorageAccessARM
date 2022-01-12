using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#if CONSUMER
using Azure.Storage.Queues;

namespace MessageConsumer.Services
#elif QUERY
namespace TableSearch.Services
#endif
{
    public class StorageService
    {
#if CONSUMER
        public QueueClient Queue1 { get; }
#endif
        public CloudTable Table1 { get; }

        public StorageService()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(@"connection string");
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(@"connection string");

#if CONSUMER
            Queue1 = new QueueClient(@"connection string", "queue1");
            var queueCreationTask = Queue1.CreateIfNotExistsAsync();
#endif

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            Table1 = tableClient.GetTableReference("table1");
            var tableCreationTask = Table1.CreateIfNotExistsAsync();

            Task.WaitAll(
#if CONSUMER
                queueCreationTask,
#endif
                tableCreationTask);

            ConditionalMethod();
        }

        [System.Diagnostics.Conditional("CONSUMER")]
        private static void ConditionalMethod()
        {
            Console.WriteLine("Conditional method");
        }
    }
}
