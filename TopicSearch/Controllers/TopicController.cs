using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using TopicSearch.Models;

namespace TopicSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ElasticClient elasticClient;

        public TopicController(ElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await elasticClient.SearchAsync<Topic>(s => s.
            Index("topics"));
            return Ok(response?.Documents);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var response = await elasticClient.SearchAsync<Topic>(s => s
            .Index("topics")
            .Query(q => q
            .Term(t => t.Name, name)
            ||
            q.Match(m => m.Field(f => f.Name).Query(name))));

            return Ok(response?.Documents?.FirstOrDefault());
        }

        [HttpPost]
        public async Task<string> Post([FromBody] Topic value)
        {
            var response = await elasticClient.IndexAsync<Topic>(value, x => x.Index("topics"));

            return response.Id;
        }
    }
}
