using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.ControllersDtos;
using Services.DTOs.DevicesDtos;
using Services.DTOs.SenzorDtos;

[ApiController]
[Route("api/[controller]")]
public class DeviceController : ControllerBase
{
    private readonly SenzorService _service;


    public DeviceController(SenzorService service)
    {
        _service = service;
    }

    [HttpGet("Analytics")]
    public async Task<ActionResult> GetAnalytics()
    {
        var data = await _service.GetAnalytics();
        return Ok(data);
    }

    [HttpGet("All")]
    public async Task<ActionResult<IEnumerable<DevicesReadDto>>> GetAll()
    {
        var devices = await _service.GetAllDevices();
        return Ok(devices);
    }

    [HttpPost("Senzor/Add")]
    public async Task<ActionResult> AddSenzor(SenzorCreateDto dto)
    {
        await _service.AddSenzor(dto);
        return Ok();
    }

    [HttpPut("Senzor/Update/{id}")]
    public async Task<ActionResult> UpdateSenzor(int id, SenzorUpdateDto dto)
    {

        await _service.UpdateSenzor(id, dto);
        return NoContent();
    }

    [HttpPost("Kontroler/Add", Name = "AddKontroler")]
    public async Task<ActionResult> AddKontroler(ControllerCreateDto dto)
    {

        await _service.AddKontroler(dto);
        return CreatedAtRoute("AddKontroler", dto);
    }

    [HttpPut("Kontroler/Update/{id}")]
    public async Task<ActionResult> UpdateKontroler(int id, ControllerUpdateDto dto)
    {
  
        await _service.UpdateKontroler(id, dto);

        return NoContent();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.DeleteDevice(id);
        return Ok();
    }
}