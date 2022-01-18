using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
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
       private static string accountName = "porkhovskyi";
       private static string accountKey = "SzTqiO7s0CGnO80+3pzbCW9GT6nFDrIfFUpvMHIK4lhYx7j92SpxNQ+tmKfJdImmBROTBi8qyaJJDTmmA4iE8Q==";
        private static string containerName = "my-container";
        static void Main(string[] args)
        {
           
            var storageAccount = new CloudStorageAccount(new StorageCredentials(accountName, accountKey), true);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            var blobs = container.ListBlobs();
            DownLoadBlobs(blobs);
            Console.WriteLine("Input this file name and extention(EN-EURUSD-20220113-M.pdf): ");//EN-EURUSD-20220113-M.pdf
            string fileName = Console.ReadLine();
            Console.WriteLine("This is you URI to get file: " + GetFileUri(fileName));
            Console.ReadKey();
        }

        private static void DownLoadBlobs(IEnumerable<IListBlobItem> blobs)
        {
            foreach (var blob in blobs)
            {
                if (blob is CloudBlockBlob blockBlob)
                {
                    blockBlob.DownloadToFile(blockBlob.Name, FileMode.Create);
                }
                else if (blob is CloudBlobDirectory blobDirectory)
                {
                    Directory.CreateDirectory(blobDirectory.Prefix);
                    DownLoadBlobs(blobDirectory.ListBlobs());
                }

            }
        }
        private static Uri GetFileUri(string fileName)
        {
            
           
            UriBuilder fileUri = new UriBuilder($"https://{accountName}.blob.core.windows.net/{containerName}/{fileName}");
           
            return fileUri.Uri;
        }
    }
}
