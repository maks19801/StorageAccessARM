using Microsoft.Azure.Cosmos.Table;
using System;
using System.Linq;
using System.Threading.Tasks;
using TableSearch.Services;

namespace TableSearch
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var table = new StorageService().Table1;

            var query = new TableQuery<BadMessage>()
            .Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "BadMessages"),
                    TableOperators.And,
                    TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition("Text", QueryComparisons.GreaterThanOrEqual, "a"),
                        TableOperators.And,
                        TableQuery.GenerateFilterCondition("Text", QueryComparisons.LessThanOrEqual, "z"))));

            TableContinuationToken token = null;

            TableQuerySegment<BadMessage> seg;
            
            do
            {
                seg = await table.ExecuteQuerySegmentedAsync(query, token);
                token = seg.ContinuationToken;
                foreach (var badMessage in seg.Results)
                {
                    Console.WriteLine(badMessage.Text);
                    badMessage.Text = badMessage.Text + "1";
                    await table.ExecuteAsync(TableOperation.Replace(badMessage));
                    Console.WriteLine(badMessage.Text);
                }
                

            } while (token != null);
            for (var i = 0; i < 5; i++)
            {
                var badMessage = new MyBadMessage
                {
                    PartitionKey = "BadMessages",
                    RowKey = Guid.NewGuid().ToString(),
                    Number = i.ToString(),     
                };

                var insertOperation = TableOperation.InsertOrReplace(badMessage);

                await table.ExecuteAsync(insertOperation);
            }

            Console.ReadKey();
        }
    }
}
