using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //creating a container if it is not exit
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnection"));
            var blogClient = storageAccount.CreateCloudBlobClient();
            var container = blogClient.GetContainerReference("assets");
            container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);

            //Uploading a blob 
            var blockBlob = container.GetBlockBlobReference("hello.txt");
            using (var fread = System.IO.File.OpenRead(@"c:\hello.txt"))
            {
                blockBlob.UploadFromStream(fread);
            }
            Console.ReadKey();
     
        }
    }
}

