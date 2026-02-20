using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Services.DTOs.CityAnaliticsDto;
using Services.DTOs.CityNodeDto;
using Services.DTOs.CrossRoadDto;
using Services.DTOs.GradDtos;
using Services.DTOs.ParkingLotDto;
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

        [HttpGet("AvailableCities")]
        public async Task<ActionResult<IEnumerable<GradReadDto>>> AvailableCities()
        {
            var cities = await _service.GetAvailableCities();
            var cityDto = _map.Map<IEnumerable<GradReadDto>>(cities);
            return Ok(cityDto);
        }


        [HttpGet("CityNodes/AverageTrafficJam")]
        public async Task<ActionResult<IEnumerable<CrossRoadJamDto>>> TrafficJamByZones()
        {
            var traffic = await _service.TrafficJamByZones();
            var trafficDto = _map.Map<IEnumerable<CrossRoadJamDto>>(traffic);
            return Ok(trafficDto); 
        }

        [HttpGet("CityNodes/CrossRoads/CriticalTrafficJam")]
        public async Task<ActionResult<IEnumerable<CrossRoadReadDto>>> HighJam()
        {
            var critical = await _service.HighJamCrossRoads();
            var dto = _map.Map<IEnumerable<CrossRoadReadDto>>(critical);
            return Ok(dto); 
        }

        [HttpGet("CityNode/ParkingLot/HiglyOccupied")]
        public async Task<ActionResult<IEnumerable<ParkingLotReadDto>>> HighlyOccupied()
        {
            var occupied = await _service.HighlyOccupiedParkingLots();
            var dto = _map.Map<IEnumerable<ParkingLotReadDto>>(occupied);
            return Ok(dto); 
        }

        [HttpPost("CityNode/CrossRoad/Add", Name = "AddCrossRoad")]
        public async Task<ActionResult<CrossRoadCreateDto>> AddCrossRoad(CrossRoadCreateDto obj)
        {
            var Create = _map.Map<CrossRoad>(obj); 
            await _service.AddCrossRoad(Create);
            var ReadDto = _map.Map<CrossRoadReadDto>(Create);
            return CreatedAtRoute("AddCrossRoad", ReadDto);
        }

        [HttpPut("CityNode/CrossRoad/Update{id}")]
        public async Task<ActionResult> UpdateCrossRoad(int id, CrossRoadUpdateDto updated)
        {
            var exist = _map.Map<CrossRoad>(updated); 
                
                await _service.UpdateCrossRoad(id, exist);
                return NoContent(); 

        }


        [HttpDelete("CityNode/CrossRoad/Delete")]
        public async Task<ActionResult> DeleteNode(int id)
        {
            await _service.DeleteNode(id);
            return Ok();
        }

        [HttpPost("CityNode/ParkingLot/Add", Name = "AddParkingLot")]
        public async Task<ActionResult<ParkingLotReadDto>> AddParkingLot(ParkingLotCreateDto obj)
        {
            var create = _map.Map<ParkingLot>(obj);
            await _service.AddParkingLot(create);
            var readDto = _map.Map<ParkingLotReadDto>(create);
            return CreatedAtRoute("AddParkingLot", readDto);
        }

        [HttpPut("CityNode/ParkingLot/Update/{id}")]
        public async Task<ActionResult> UpdateParkingLot(int id, ParkingLotUpdateDto updated)
        {
            var exist = _map.Map<ParkingLot>(updated);
            await _service.UpdateParkingLot(id, exist);
            return NoContent();
        }

        [HttpDelete("CityNode/ParkingLot/Delete/{id}")]
        public async Task<ActionResult> DeleteParkingLot(int id)
        {
            await _service.DeleteNode(id);
            return Ok();
        }






    }
}
