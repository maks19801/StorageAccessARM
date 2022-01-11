using Microsoft.Azure.Cosmos.Table;

namespace TableSearch
{
    internal class MyBadMessage : TableEntity
    {
       
        public string Number { get; set; }
        
    }
}