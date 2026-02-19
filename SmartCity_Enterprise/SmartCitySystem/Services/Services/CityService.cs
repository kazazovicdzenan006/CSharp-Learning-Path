
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Services.DTOs.CityAnaliticsDto;

public class CityService
{
    private readonly IUnitOfWork _unit;
    private IQueryable<CrossRoad> crossRoads => _unit.CityNodes.GetQueryable().OfType<CrossRoad>();
    private IQueryable<ParkingLot> parkingLots => _unit.CityNodes.GetQueryable().OfType<ParkingLot>();


    public CityService(IUnitOfWork unit)
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

    public async Task UpdateNode(int id, CityNode updatedData)
    {
        var existingNode = await _unit.CityNodes.GetByIdAsync(id);

        if (existingNode == null) throw new Exception("City node not found!");

        existingNode.CityZone = updatedData.CityZone;
        existingNode.StreetName = updatedData.StreetName;
        existingNode.GradId = updatedData.GradId;


        if (existingNode is ParkingLot existingParking && updatedData is ParkingLot newParking)
        {
            existingParking.ParkingName = newParking.ParkingName;
            existingParking.TotalParkingSpots = newParking.TotalParkingSpots;
            existingParking.AvailableParkingSpots = newParking.AvailableParkingSpots;
        }
        else if (existingNode is CrossRoad existingCross && updatedData is CrossRoad newCross)
        {
            existingCross.CrossName = newCross.CrossName;
            existingCross.TrafficJamPercantage = newCross.TrafficJamPercantage;
        }

        await _unit.CompleteAsync();
    }

    public async Task DeleteNode(int id)
    {
        var node = await _unit.CityNodes.GetByIdAsync(id);
        if (node != null)
        {
            _unit.CityNodes.Delete(node) ;
            await _unit.CompleteAsync();
        }
    }



    public async Task AddNode(CityNode novi)
    {
        /*
        SafeExecutor.Execute(async () =>
        {
            await _context.CityNodes.AddAsync(novi);
            await _context.SaveChangesAsync();
        }); */

        try
        {
            await _unit.CityNodes.AddAsync(novi);
            await _unit.CompleteAsync();
        }
        catch (CityExceptionSystem ex) { }



    }
}



