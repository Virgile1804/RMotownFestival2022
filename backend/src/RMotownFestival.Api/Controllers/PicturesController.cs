using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMotownFestival.Api.Common;
using System;
using System.Linq;

namespace RMotownFestival.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        public BlobUtility BlobUtility { get; }
        public PicturesController(BlobUtility blobUtility)
        {
            BlobUtility = blobUtility;
        }
        [HttpGet]
        public string[] GetAllPictureUrls()
        {
            //return Array.Empty<string>();
            var container = BlobUtility.GetThumbsContainer();
            // return container.GetBlobs().Select(blob => $"{container.Uri.AbsoluteUri}/{blob.Name}").ToArray();// THIS WONT WORK BECAUSE WE NEED A SAS
            return container.GetBlobs()
                 .Select(blob => BlobUtility.GetSasUri(container, blob.Name)).ToArray();
        }

        [HttpPost]
        public void PostPicture(IFormFile file)
        {
        }
    }
}
