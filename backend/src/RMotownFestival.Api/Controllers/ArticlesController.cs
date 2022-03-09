using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using RMotownFestival.Api.Domain;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RMotownFestival.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private CosmosClient _cosmosClient { get; set; }
        private Container _webSiteArticlesContainer { get; set; }

        public ArticlesController(IConfiguration configuration)
        {
            _cosmosClient = new CosmosClient(configuration.GetConnectionString("CosmosConnection"));
            _webSiteArticlesContainer = _cosmosClient.GetContainer("RMotownArticles","WebsiteArticles");
        }

        //public async Task<ActionResult> CreateAsync

        [HttpPost("Add")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Article))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> CreateItemAsync([FromBody] Article art)
        {
            var article = new Article
            {
                Id = Guid.NewGuid().ToString(),
                Title = art.Title,
                Date = art.Date,
                ImagePath = art.ImagePath,
                Message = art.Message,
                Status = art.Status,
                Tag = art.Tag
            };

            var listArticles = await _webSiteArticlesContainer.CreateItemAsync(article);
            return Ok(listArticles);


        }
    }
}
