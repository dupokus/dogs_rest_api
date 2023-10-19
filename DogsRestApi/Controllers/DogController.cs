using DogsRestApi.Data;
using DogsRestApi.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet]
        [Route("api/[controller]/GetDogs")]
        public async Task<IActionResult> GetDogs(string? searchTerm, string? sortColumn, string? sortOrder, int page, int pageSize)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<DogModel> data = await _db.GetDogsAsync(searchTerm, sortColumn, sortOrder, page, pageSize);

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

        [HttpGet]
        [Route("api/[controller]/ping")]
        public string GetPing()
        {
            return "Dogshouseservice.Version1.0.1";
        }

        [HttpGet]
        [Route("api/[controller]/GetDogById/{Id}")]
        public async Task<IActionResult> GetDogById(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                DogModel data = await _db.GetDogByIdAsync(id);

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

        [HttpPost]
        [Route("api/[controller]/SaveDog")]
        public async Task<IActionResult> Post([FromBody] DogModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                await _db.SaveDogAsync(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPut]
        [Route("api/[controller]/UpdateDog")]
        public async Task<IActionResult> Put([FromBody] DogModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                await _db.SaveDogAsync(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteDog/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                await _db.DeleteDogAsync(Id);
                return Ok(ResponseHandler.GetAppResponse(type, "Delete successful."));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
