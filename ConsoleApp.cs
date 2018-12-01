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
            var container = blogClient.GetContainerReference("data");
            container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);

            //Uploading a blob 
            var blockBlob = container.GetBlockBlobReference("hello1.txt");
            var blockBlobCopy = container.GetBlockBlobReference("hello2.txt");
            using (var fread = System.IO.File.OpenRead(@"c:\hello1.txt"))
            {
                blockBlob.UploadFromStream(fread);
            }
            //list blobs
            var blobs = container.ListBlobs();
            foreach (var blob in blobs)
            {
                Console.WriteLine(blob.Uri);
            }
            Console.WriteLine(blockBlob.Uri);
            

            //copy a blob hello.txt into hello1.txt async
            var cb = new AsyncCallback(x=> Console.WriteLine("Blob file copy completed "));
            blockBlobCopy.BeginStartCopy(blockBlob.Uri, cb, null);

            // Blog by hierarchy
            var blockBlobHierarchy = container.GetBlockBlobReference("text/hello2.txt");
            using (var fread1 = System.IO.File.OpenRead(@"c:\hello1.txt"))
            {
                blockBlobHierarchy.UploadFromStream(fread1);
          
            }

            setMetaData(container);
            getMetaData(container);


        }
        static void setMetaData(CloudBlobContainer container)
        {
            //clear the existing metadata
            container.Metadata.Clear();
            container.Metadata.Add("Owner", "Jayesh Jose");
            container.Metadata["last_updated"] = DateTime.Now.ToString();
            container.SetMetadata();
            Console.WriteLine("Done");
            Console.ReadKey();
        }
        static void getMetaData(CloudBlobContainer container)
        {
            container.FetchAttributes();
            Console.WriteLine("Container Metadata: \n");
            foreach(var item in container.Metadata)
            {
                Console.WriteLine(string.Format("{0}:{1}",item.Key,item.Value));
                Console.WriteLine("Done");
                Console.ReadKey();
            }

        }
    }
}
