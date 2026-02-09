
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

public class CityService
{
    private readonly IUnitOfWork _unit;
    private IQueryable<CrossRoad> crossRoads => _unit.CityNodes.GetQueryable().OfType<CrossRoad>();
    private IQueryable<ParkingLot> parkingLots => _unit.CityNodes.GetQueryable().OfType<ParkingLot>();


    public CityService(IUnitOfWork unit)
    {
        _unit = unit;
    }
  
    public async Task<string> AllLocaations()
    {
        var _allNodes = await _unit.CityNodes.GetAllAsync();
        var sp = new StringBuilder();
        foreach (var node in _allNodes)
        {
            string info = $"[{node.GetType().Name}] ID: {node.Id}, Zona {node.CityZone}, Adresa {node.StreetName}";
            sp.AppendLine(info);
        }
        return sp.ToString();
    }


    public async Task<List<CityNode>> GetReportData()
    {
        var data = await _unit.CityNodes.GetAllAsync();
        return data.ToList();
    }

    public async Task<List<Grad>> GetAvailableCities()
    {
        return (await _unit.Gradovi.GetAllAsync()).ToList();
    }
    public async Task<string> TrafficJamByZones()
    {
       
        
        var rezultati = await crossRoads
            .GroupBy(x => x.CityZone)
            .Select(g => new { zona = g.Key, prosjek = g.Average(x => (double?)x.TrafficJamPercantage ?? 0.0) }).ToListAsync();
        var sp = new StringBuilder();

        foreach (var r in rezultati)
        {
            sp.AppendLine($"Zona {r.zona} | Average Traffic jam {r.prosjek}%");
        }
        return sp.ToString();
    }


    public async Task<string> AnalizeCriticalPoints()
    {
       
        
        var sp = new StringBuilder();
        var HighJamCrossRoads = crossRoads.OfType<CrossRoad>().Where(x => x.TrafficJamPercantage > 80);
        var HighlyOcuppiedParkingLots = parkingLots.Where(x => x.AvailableParkingSpots < 5);
        if (HighJamCrossRoads.Any())
        {
            foreach (var item in HighJamCrossRoads)
            {
                sp.AppendLine($"Crossroad {item.CrossName} in zone {item.CityZone} has high traffic jam of {item.TrafficJamPercantage}%");
            }
        }
        if (HighlyOcuppiedParkingLots.Any())
        {
            foreach (var p in HighlyOcuppiedParkingLots)
            {
                sp.AppendLine($"Parking {p.ParkingName} in zone {p.CityZone} has only {p.AvailableParkingSpots} available spots left");
            }
        }
        return sp.ToString();
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



