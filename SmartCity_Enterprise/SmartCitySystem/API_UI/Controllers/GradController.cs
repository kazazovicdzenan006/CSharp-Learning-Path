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
        private readonly IMapper _map;

        public GradController(CityService service, IMapper mapper)
        {
            _service = service;
            _map = mapper;
        }

        [HttpGet("GetAllCities")]
        public async Task<ActionResult<IEnumerable<GradReadDto>>> GetCities()
        {
            var data = await _service.GetAllCitiesAsync();
            var cityDto = _map.Map<IEnumerable<GradReadDto>>(data);
            return Ok(cityDto);
        }


        [HttpPost("AddCity", Name = "AddCity")]
        public async Task<ActionResult<GradReadDto>> AddCity(GradCreateDto city)
        {

            var data = _map.Map<Grad>(city);
            await _service.AddCity(data);
            var cityDto = _map.Map<GradReadDto>(data);
            return CreatedAtRoute("AddCity", cityDto);


        }


        [HttpPut("UpdateCity/{id}")]
        public async Task<ActionResult> UpdateCity(int id, GradUpdateDto updatedData)
        {
            var currentCity = await _service.GetCityById(id);
            if (currentCity != null)
            {

                _map.Map(updatedData, currentCity);
                await _service.UpdateCity(id, currentCity);
                return NoContent();

            }
            else return BadRequest("There is no object with that id");
        }

        [HttpDelete("DeleteCity/{id}")]
        public async Task<ActionResult> DeleteCity(int id)
        {


            await _service.DeleteCity(id);
            return NoContent();


        }





    }
}
