using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace ImprovedExamWithApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ExamService _service;

        public ExamController(ExamService service)
        {
            _service = service; 
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

        [HttpPost("Add-Client")]
        public async Task AddClient(Client obj)
        {
            await _service.AddClient(obj);
        }

        [HttpPut("Update-Client")]
        public async Task <IActionResult> UpdateClient(Client obj)
        {
            try
            {
                await _service.UpdateClient(obj);
                return Ok();
            }catch(Exception ex) { return BadRequest(ex.Message); }
            
        }
        [HttpDelete("Delete-Client")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                var delete = await _service.GetClientById(id);
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
