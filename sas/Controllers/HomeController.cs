using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageAccount"));
            var blogClient = storageAccount.CreateCloudBlobClient();
            var container = blogClient.GetContainerReference("images");
            var blobs = new List<BlobImage>();
            foreach (var blob in container.ListBlobs())
            {

                if (blob.GetType() == typeof(CloudBlockBlob))
                {
                    var sastoken = GetSASToken(storageAccount);
                    blobs.Add(new BlobImage { BlobUri = blob.Uri.ToString() + sastoken });
                }
             

            }
            return View(blobs);
        }
        public string GetSASToken(CloudStorageAccount storageAccount)
        {
            SharedAccessAccountPolicy policy = new SharedAccessAccountPolicy()
            {
                Permissions = SharedAccessAccountPermissions.Write | SharedAccessAccountPermissions.Read,
                Services = SharedAccessAccountServices.Blob,
                ResourceTypes = SharedAccessAccountResourceTypes.Object,
                SharedAccessExpiryTime = DateTime.Now.AddMinutes(30),
                Protocols = SharedAccessProtocol.HttpsOnly

            };
            return storageAccount.GetSharedAccessSignature(policy);
        }
    }
}