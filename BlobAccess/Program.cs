using Azure.Storage.Blobs;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            BlobServiceClient serviceClient = new BlobServiceClient(@"DefaultEndpointsProtocol=https;AccountName=itstep1511blob;AccountKey=ucnFo5pw6zUUlxb1cParY2D+qNX7NVgyUmawYX0Sy2TxGli/IydNewcEBui+3EvQHp7pFXxeMaJ6Xt/Zf7ItFw==;BlobEndpoint=https://itstep1511blob.blob.core.windows.net/;");
            BlobContainerClient containerClient = serviceClient.GetBlobContainerClient("container1");

            foreach (var blob in containerClient.GetBlobs())
            {
                var reference = containerClient.GetBlobClient(blob.Name);
                reference.DownloadTo(blob.Name);
            }

            Console.ReadKey();
        }
    }
}
