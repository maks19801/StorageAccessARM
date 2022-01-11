﻿using Microsoft.Azure.Cosmos.Table;
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
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=porkhovskyi;AccountKey=SzTqiO7s0CGnO80+3pzbCW9GT6nFDrIfFUpvMHIK4lhYx7j92SpxNQ+tmKfJdImmBROTBi8qyaJJDTmmA4iE8Q==;BlobEndpoint=https://porkhovskyi.blob.core.windows.net/;QueueEndpoint=https://porkhovskyi.queue.core.windows.net/;TableEndpoint=https://porkhovskyi.table.core.windows.net/;FileEndpoint=https://porkhovskyi.file.core.windows.net/;");
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=itstep1511gen;AccountKey=dRV5C8hAj+QpKYW+7My5xzpd1C5ZLSmWs6Wgnj/sQnaUR670ofKPcOFOXs7EEh0GUyXgrnHgpPFUqKeBBhYkhA==;BlobEndpoint=https://itstep1511gen.blob.core.windows.net/;QueueEndpoint=https://itstep1511gen.queue.core.windows.net/;TableEndpoint=https://itstep1511gen.table.core.windows.net/;FileEndpoint=https://itstep1511gen.file.core.windows.net/;");

#if CONSUMER
            Queue1 = new QueueClient(@"DefaultEndpointsProtocol=https;AccountName=porkhovskyi;AccountKey=SzTqiO7s0CGnO80+3pzbCW9GT6nFDrIfFUpvMHIK4lhYx7j92SpxNQ+tmKfJdImmBROTBi8qyaJJDTmmA4iE8Q==;BlobEndpoint=https://porkhovskyi.blob.core.windows.net/;QueueEndpoint=https://porkhovskyi.queue.core.windows.net/;TableEndpoint=https://porkhovskyi.table.core.windows.net/;FileEndpoint=https://porkhovskyi.file.core.windows.net/;", "queue1");
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
