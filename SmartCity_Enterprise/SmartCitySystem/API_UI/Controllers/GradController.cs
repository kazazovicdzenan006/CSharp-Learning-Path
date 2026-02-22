using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.GradDtos;

namespace API_UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradController : ControllerBase
    {
        private readonly CityService _service;


        public GradController(CityService service)
        {
            _service = service;

        }

        [HttpGet("GetAllCities")]
        public async Task<ActionResult<IEnumerable<GradReadDto>>> GetCities()
        {
            var data = await _service.GetAllCitiesAsync();

            return Ok(data);
        }


        [HttpPost("AddCity", Name = "AddCity")]
        public async Task<ActionResult<GradReadDto>> AddCity(GradCreateDto city)
        {

            var cityDto = await _service.AddCity(city);
          
            return CreatedAtRoute("AddCity", cityDto);

        }


        [HttpPut("UpdateCity/{id}")]
        public async Task<ActionResult> UpdateCity(int id, GradUpdateDto updatedData)
        {
            await _service.UpdateCity(id, updatedData);
            return NoContent();
        }

        [HttpDelete("DeleteCity/{id}")]
        public async Task<ActionResult> DeleteCity(int id)
        {


            await _service.DeleteCity(id);
            return NoContent();


        }





    }
}
