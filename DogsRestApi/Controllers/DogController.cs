using DogsRestApi.Data;
using DogsRestApi.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DogsRestApi.Controllers
{
    
    [ApiController]
    public class DogController : ControllerBase
    {
        private readonly DbHelper _db;
        public DogController(DogDbContext dogDbContext)
        {
            _db = new DbHelper(dogDbContext);
        }
        // GET: api/<DogController>
        [HttpGet]
        [Route("api/[controller]/GetDogs")]
        public IActionResult Get()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<DogModel> data = _db.GetDogs();
                
                if (!data.Any()) 
                {
                    type = ResponseType.NotFound;
                }
            }
            catch (Exception )
            {
                type = ResponseType.Failure;
                return BadRequest()
            }
        }

        // GET api/<DogController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DogController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DogController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DogController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
