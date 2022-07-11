using Microsoft.AspNetCore.Mvc;
using Nest;
using TopicSearch.Models;

namespace TopicSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ElasticClient elasticClient;

        public UserController(ElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await elasticClient.SearchAsync<User>(s => s.
            Index("users"));
            return Ok(response?.Documents);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var response = await elasticClient.SearchAsync<User>(s => s
            .Index("users")
            .Query(q => q
            .Term(t => t.Name, name)
            ||
            q.Match(m => m.Field(f => f.Name).Query(name))));


            return Ok(response?.Documents?.FirstOrDefault());
        }

        [HttpPost]
        public async Task<string> Post([FromBody] User value)
        {
            var response = await elasticClient.IndexAsync<User>(value, x => x.Index("users"));

            return response.Id;
        }
    }
}
