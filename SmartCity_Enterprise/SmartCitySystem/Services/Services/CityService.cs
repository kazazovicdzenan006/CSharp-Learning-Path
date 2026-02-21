
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Services.DTOs.CityAnaliticsDto;
using Services.Services;

public class CityService : BaseService
{
    private readonly IUnitOfWork _unit;
    private IQueryable<CrossRoad> crossRoads => _unit.CityNodes.GetQueryable().OfType<CrossRoad>();
    private IQueryable<ParkingLot> parkingLots => _unit.CityNodes.GetQueryable().OfType<ParkingLot>();


    public CityService(IUnitOfWork unit, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _unit = unit;
    }
  
    public async Task<IEnumerable<CityNode>> AllLocaations()
    {
        var _allNodes = await _unit.CityNodes
            .GetQueryable()
            .Include(x => x.Grad)
            .ToListAsync();

        return _allNodes;
    }


    public async Task<List<CityNode>> GetReportData()
    {
        var data = await _unit.CityNodes
            .GetQueryable()
            .Include(x => x.Grad)
            .ToListAsync();
        return data;
    }

    public async Task<List<Grad>> GetAvailableCities()
    {
        return (await _unit.Gradovi.GetAllAsync()).ToList();
    }
    public async Task<Grad> GetCityById(int id)
    {
        return await _unit.Gradovi.GetByIdAsync(id);
    }

    public async Task AddCity(Grad city)
    {
            await _unit.Gradovi.AddAsync(city);
            await _unit.CompleteAsync();
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

    public async Task UpdateCity(int id, Grad city)
    {
        var toUpdate = await _unit.Gradovi.GetByIdAsync(id);
        if (toUpdate != null)
        {
            toUpdate.Name = city.Name;
            _unit.Gradovi.Update(toUpdate);
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


    public async Task<IEnumerable<CrossRoad>> HighJamCrossRoads()
    {
        var query = crossRoads.OfType<CrossRoad>().Where(x => x.TrafficJamPercantage > 80);

        var data = await query.ToListAsync();

        return data;
    }


    public async Task<IEnumerable<ParkingLot>> HighlyOccupiedParkingLots()
    {
        var query = parkingLots.Where(x => x.AvailableParkingSpots < 5);

        var data = await query.ToListAsync();

        return data;
    }


    public async Task<List<Grad>> GetAllCitiesAsync()
    {
        var cities = await _unit.Gradovi.GetAllAsync();
        return cities.ToList();
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
    public async Task AddParkingLot(ParkingLot parking)
    {
        await _unit.CityNodes.AddAsync(parking);
        var result = await _unit.CompleteAsync();
        if (result <= 0) throw new Exception("Failed to add parking lot to the database!");
    }

    public async Task UpdateParkingLot(int id, ParkingLot updatedData)
    {
        var existing = await _unit.CityNodes.GetByIdAsync(id) as ParkingLot;
        if (existing == null) throw new Exception("Parking lot not found!");

        existing.CityZone = updatedData.CityZone;
        existing.StreetName = updatedData.StreetName;


        existing.ParkingName = updatedData.ParkingName;
        existing.TotalParkingSpots = updatedData.TotalParkingSpots;
        existing.AvailableParkingSpots = updatedData.AvailableParkingSpots;

        await _unit.CompleteAsync();
    }

    // --- CROSSROAD ---
    public async Task AddCrossRoad(CrossRoad crossRoad)
    {
        await _unit.CityNodes.AddAsync(crossRoad);
        var result = await _unit.CompleteAsync();
        if (result <= 0) throw new Exception("Failed to add crossroad to the database!");
    }

    public async Task UpdateCrossRoad(int id, CrossRoad updatedData)
    {
        var existing = await _unit.CityNodes.GetByIdAsync(id) as CrossRoad;
        if (existing == null) throw new Exception("Crossroad not found!");

        existing.CityZone = updatedData.CityZone;
        existing.StreetName = updatedData.StreetName;


        existing.CrossName = updatedData.CrossName;
        existing.TrafficJamPercantage = updatedData.TrafficJamPercantage;

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



