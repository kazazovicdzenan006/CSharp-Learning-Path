using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Services.DTOs.CityNodeDto;
using System.Runtime.CompilerServices;

namespace API_UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitySystemController : ControllerBase
    {

        private readonly CityService _service;
        private readonly IMapper _map;

        public CitySystemController(CityService service, IMapper mapper)
        {
            _service = service;
            _map = mapper;
        }


        [HttpGet("AllNodes")]
        public async Task<ActionResult<IEnumerable<CityNodeReadDto>>> AllNodes()
        {
            var nodes = await _service.AllLocaations();
            var dto = _map.Map<IEnumerable<CityNodeReadDto>>(nodes);
            return Ok(dto);
        }






    }
}
