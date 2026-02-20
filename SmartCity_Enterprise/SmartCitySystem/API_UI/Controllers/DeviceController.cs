using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.SenzorDtos;
using Services.DTOs.DevicesDtos;

[ApiController]
[Route("api/[controller]")]
public class DeviceController : ControllerBase
{
    private readonly SenzorService _service;
    private readonly IMapper _map;

    public DeviceController(SenzorService service, IMapper map)
    {
        _service = service;
        _map = map;
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
        return Ok(_map.Map<IEnumerable<DevicesReadDto>>(devices));
    }

    [HttpPost("Senzor/Add")]
    public async Task<ActionResult> AddSenzor(SenzorCreateDto dto)
    {
        var sensor = _map.Map<Senzor>(dto);
        await _service.AddSenzor(sensor);
        return Ok();
    }

    [HttpPut("Senzor/Update/{id}")]
    public async Task<ActionResult> UpdateSenzor(int id, SenzorUpdateDto dto)
    {
        var sensor = _map.Map<Senzor>(dto);
        await _service.UpdateSenzor(id, sensor);
        return NoContent();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.DeleteDevice(id);
        return Ok();
    }
}