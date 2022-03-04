using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace RMotownFestival.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        [HttpGet]
        public string[] GetAllPictureUrls()
        {
            return Array.Empty<string>();
        }

        [HttpPost]
        public void PostPicture(IFormFile file)
        {
        }
    }
}
