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


        public CitySystemController(CityService service)
        {
            _service = service;

        }


        [HttpGet("AllNodes")]
        public async Task<ActionResult<IEnumerable<CityNodeReadDto>>> AllNodes()
        {
            var nodes = await _service.AllLocaations();
           
            
            return Ok(nodes);
        }

        [HttpGet("AvailableCities")]
        public async Task<ActionResult<IEnumerable<GradReadDto>>> AvailableCities()
        {
            var cities = await _service.GetAvailableCities();
            
            return Ok(cities);
        }


        [HttpGet("CityNodes/AverageTrafficJam")]
        public async Task<ActionResult<IEnumerable<CrossRoadJamDto>>> TrafficJamByZones()
        {
            var traffic = await _service.TrafficJamByZones();

            return Ok(traffic); 
        }

        [HttpGet("CityNodes/CrossRoads/CriticalTrafficJam")]
        public async Task<ActionResult<IEnumerable<CrossRoadReadDto>>> HighJam()
        {
            var critical = await _service.HighJamCrossRoads();

            return Ok(critical); 
        }

        [HttpGet("CityNode/ParkingLot/HiglyOccupied")]
        public async Task<ActionResult<IEnumerable<ParkingLotReadDto>>> HighlyOccupied()
        {
            var occupied = await _service.HighlyOccupiedParkingLots();
            
            return Ok(occupied); 
        }

        [HttpPost("CityNode/CrossRoad/Add", Name = "AddCrossRoad")]
        public async Task<ActionResult<CrossRoadCreateDto>> AddCrossRoad(CrossRoadCreateDto obj)
        {

           var added = await _service.AddCrossRoad(obj);
         
            return CreatedAtRoute("AddCrossRoad", added);
        }

        [HttpPut("CityNode/CrossRoad/Update{id}")]
        public async Task<ActionResult> UpdateCrossRoad(int id, CrossRoadUpdateDto updated)
        {

                
                await _service.UpdateCrossRoad(id, updated);
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
           
            var readDto = await _service.AddParkingLot(obj);
           
            return CreatedAtRoute("AddParkingLot", readDto);
        }

        [HttpPut("CityNode/ParkingLot/Update/{id}")]
        public async Task<ActionResult> UpdateParkingLot(int id, ParkingLotUpdateDto updated)
        {
          
            await _service.UpdateParkingLot(id, updated);
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
