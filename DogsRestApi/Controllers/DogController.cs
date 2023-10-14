using DogsRestApi.Data;
using DogsRestApi.Model;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // GET api/<DogController>/5
        [HttpGet]
        [Route("api/[controller]/GetDogById/{Id}")]
        public IActionResult Get(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                DogModel data = _db.GetDogById(id);

                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        
        // POST api/<DogController>
        [HttpPost]
        [Route("api/[controller]/SaveDog")]
        public IActionResult Post([FromBody] DogModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.SaveDog(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // PUT api/<DogController>/5
        [HttpPut]
        [Route("api/[controller]/UpdateDog")]
        public IActionResult Put([FromBody] DogModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.SaveDog(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // DELETE api/<DogController>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteDog/{Id}")]

        public IActionResult Delete(int Id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.DeleteDog(Id);
                return Ok(ResponseHandler.GetAppResponse(type, "Delete successful."));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
