using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using Services.DTOs.ClientDTOs;

namespace ImprovedExamWithApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ExamService _service;
        private readonly IMapper _mapper;

        public ExamController(ExamService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> GetAllData()
        {
            return await _service.GetData();
         
        }

        [HttpGet("Most-Experienced")]
        public async Task<ActionResult<Worker>> GetMostExperienced()
        {
            var exp = await _service.MostExperiencedWorker();
            if (exp == null)
            {
                return NotFound("Not found any worker with experience");
            }
            return Ok(exp);
        }

        [HttpGet("Grouped-Workers")]
        public async Task<ActionResult<IEnumerable<Worker>>> GetGrouped()
        {
            var group = await _service.GroupedWorkers();
            if(group == null)
            {
                return NotFound("No workers");
            }
            return Ok(group);

        }
        [HttpGet("Get-All-Clients")]
        public async Task<ActionResult<IEnumerable<ClientReadDto>>> GetAllClients()
        {
            var clients = await _service.GetClients();
            var clientsDto = _mapper.Map<IEnumerable<ClientReadDto>>(clients);
            return Ok(clientsDto);
        }

        [HttpPost("Add-Client", Name = "AddClient")]
        public async Task<ActionResult<ClientCreateDto>> AddClient(ClientCreateDto obj)
        {
            var ClientEntity = _mapper.Map<Client>(obj);

            try
            {
                await _service.AddClient(ClientEntity);
                var clientReadDto = _mapper.Map<ClientReadDto>(ClientEntity);

                return CreatedAtRoute("AddClient", new { Id = clientReadDto.Id }, clientReadDto);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

          

        }

        [HttpPut("Update-Client")]
        public async Task <IActionResult> UpdateClient(int id, ClientUpdateDto obj)
        {
            var existingClient = await _service.GetClientById(id);
            if (existingClient == null) return NotFound();

            _mapper.Map(obj, existingClient); // automapper do all hardwork for us 

            try
            {
                await _service.UpdateClient(existingClient);
                return NoContent();
            }catch(Exception ex) { return BadRequest(ex.Message); }
            
        }
        [HttpDelete("Delete-Client")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var delete = await _service.GetClientById(id);
            if (delete == null) return NotFound("Client is not found");
            try
            {
                await _service.RemoveClient(delete);
                return Ok();
            }catch(Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpPost("Add-Worker")]
        public async Task AddWorker(Worker obj)
        {
            await _service.AddWorker(obj);
        }

        [HttpPut("Update-Worker")]
        public async Task<IActionResult> UpdateWorker(Worker obj)
        {
            try
            {
                await _service.UpdateWorker(obj);
                return Ok();
            }catch(Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpDelete("Delete-Worker")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            try
            {
                var delete = await _service.GetWorkerById(id);
                await _service.RemoveWorker(delete);
                return Ok();
            }catch(Exception ex) { return BadRequest(ex.Message); }

        }







    }
}
