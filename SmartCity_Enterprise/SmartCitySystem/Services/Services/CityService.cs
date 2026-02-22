
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Services.DTOs.CityAnaliticsDto;
using Services.DTOs.CityNodeDto;
using Services.DTOs.GradDtos;
using Services.DTOs.CrossRoadDto;
using Services.DTOs.ParkingLotDto;
using Services.Services;
using AutoMapper;

public class CityService : BaseService
{
    private readonly IUnitOfWork _unit;
    private readonly IMapper _mapper; 
    private IQueryable<CrossRoad> crossRoads => _unit.CityNodes.GetQueryable().OfType<CrossRoad>();
    private IQueryable<ParkingLot> parkingLots => _unit.CityNodes.GetQueryable().OfType<ParkingLot>();


    public CityService(IUnitOfWork unit, IMapper mapper, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _unit = unit;
        _mapper = mapper;
    }
  
    public async Task<IEnumerable<CityNodeReadDto>> AllLocaations()
    {
        var _allNodes = await _unit.CityNodes
            .GetQueryable()
            .Include(x => x.Grad)
            .ToListAsync();
        var dto = _mapper.Map<IEnumerable<CityNodeReadDto>>(_allNodes);
        return dto;
    }

    public async Task<List<GradReadDto>> GetAvailableCities()
    {
        var data = await _unit.Gradovi.GetAllAsync();
        var dto = _mapper.Map<List<GradReadDto>>(data);

        return dto;
    }
    public async Task<GradReadDto> GetCityById(int id)
    {
        var city = await _unit.Gradovi.GetByIdAsync(id);
        var dto = _mapper.Map<GradReadDto>(city);
        return dto;
    }

    public async Task<GradReadDto> AddCity(GradCreateDto dto)
    {
            await ValidateAsync(dto);
            var city = _mapper.Map<Grad>(dto);
            await _unit.Gradovi.AddAsync(city);
            await _unit.CompleteAsync();

        return _mapper.Map<GradReadDto>(city);

    }

    public async Task DeleteCity(int id)
    {
        var city = await _unit.Gradovi.GetByIdAsync(id);
        if (city != null)
        {
            _unit.Gradovi.Delete(city);
            await _unit.CompleteAsync();
        }
        else throw new Exception("There is no city with that id");
    }

    public async Task UpdateCity(int id, GradUpdateDto dto)
    {
        await ValidateAsync(dto);
       
        var toUpdate = await _unit.Gradovi.GetByIdAsync(id);
        if (toUpdate != null)
        {
            _mapper.Map(dto, toUpdate);
            await _unit.CompleteAsync();
        }else { throw new Exception("Couldn't find city with that id"); }
    }
    public async Task<List<CrossRoadJamDto>> TrafficJamByZones()
    {
       
        
        return await crossRoads
            .GroupBy(x => x.CityZone)
            .Select(g => new CrossRoadJamDto { zona = g.Key, 
                prosjek = g.Average(x => (double?)x.TrafficJamPercantage ?? 0.0) })
            .ToListAsync();
       
    }


    public async Task<IEnumerable<CrossRoadReadDto>> HighJamCrossRoads()
    {
        var query = crossRoads.OfType<CrossRoad>().Where(x => x.TrafficJamPercantage > 80);

        var data = await query.ToListAsync();
        var dto = _mapper.Map<IEnumerable<CrossRoadReadDto>>(data);

        return dto;
    }


    public async Task<IEnumerable<ParkingLotReadDto>> HighlyOccupiedParkingLots()
    {
        var query = parkingLots.Where(x => x.AvailableParkingSpots < 5);

        var data = await query.ToListAsync();

        var dto = _mapper.Map<IEnumerable<ParkingLotReadDto>>(data);

        return dto;
    }


    public async Task<IEnumerable<GradReadDto>> GetAllCitiesAsync()
    {
        var cities = await _unit.Gradovi.GetAllAsync();
        var dto = _mapper.Map<IEnumerable<GradReadDto>>(cities);
        return dto;
    }

    public async Task<CityNode> GetNodeById(int id)
    {
        return await _unit.CityNodes.GetByIdAsync(id);
    }
    public async Task<CrossRoad> GetCrossRoadById(int id)
    {
        return await _unit.Raskrsnice.GetByIdAsync(id);
    }
    public async Task<ParkingLot> GetParkingById(int id)
    {
        return await _unit.Parkinzi.GetByIdAsync(id);
    }

    // --- PARKING LOT ---
    public async Task<ParkingLotReadDto> AddParkingLot(ParkingLotCreateDto dto)
    {
        await ValidateAsync(dto);
        var parking = _mapper.Map<ParkingLot>(dto);
        await _unit.CityNodes.AddAsync(parking);
        var result = await _unit.CompleteAsync();
        if (result <= 0) throw new Exception("Failed to add parking lot to the database!");

        return _mapper.Map<ParkingLotReadDto>(parking);
    }

    public async Task UpdateParkingLot(int id, ParkingLotUpdateDto dto)
    {
        await ValidateAsync(dto);
        
        var existing = await _unit.CityNodes.GetByIdAsync(id) as ParkingLot;
        if (existing == null) throw new Exception("Parking lot not found!");

        _mapper.Map(dto, existing);

        await _unit.CompleteAsync();
    }

    // --- CROSSROAD ---
    public async Task<CrossRoadReadDto> AddCrossRoad(CrossRoadCreateDto dto)
    {
        await ValidateAsync(dto);
        var crossRoad = _mapper.Map<CrossRoad>(dto);
        await _unit.CityNodes.AddAsync(crossRoad);
        var result = await _unit.CompleteAsync();
        if (result <= 0) throw new Exception("Failed to add crossroad to the database!");

        return _mapper.Map<CrossRoadReadDto>(crossRoad);
    }

    public async Task UpdateCrossRoad(int id, CrossRoadUpdateDto dto)
    {
        await ValidateAsync(dto);
       
        var existing = await _unit.CityNodes.GetByIdAsync(id) as CrossRoad;
        if (existing == null) throw new Exception("Crossroad not found!");

        _mapper.Map(dto, existing);

        await _unit.CompleteAsync();
    }

    // --- DELETE ---
    public async Task DeleteNode(int id)
    {
        var node = await _unit.CityNodes.GetByIdAsync(id);
        if (node == null) throw new Exception("Node not found!");

        _unit.CityNodes.Delete(node);
        var result = await _unit.CompleteAsync();
        if (result <= 0) throw new Exception("Failed to delete node from the database!");
    }
}



