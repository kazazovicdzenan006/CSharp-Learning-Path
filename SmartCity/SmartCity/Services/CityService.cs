
using Microsoft.EntityFrameworkCore;
using System.Text;

public class CityService
{
   private readonly CityContext _context;
    private IQueryable<CrossRoad> crossRoads => _context.CityNodes.OfType<CrossRoad>();
    private IQueryable<ParkingLot> parkingLots => _context.CityNodes.OfType<ParkingLot>();

    public CityService(CityContext context)
    {
        _context = context;
    }
    public async Task<bool> Exists(int id)
    {
        return await _context.CityNodes.AnyAsync(x => x.Id == id);
    }
    public async Task<string> AllLocaations()
    {
        var _allNodes = await _context.CityNodes.AsNoTracking().ToListAsync();
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
        var data = await _context.CityNodes.AsNoTracking().ToListAsync();
        return data;
    }

    public async Task<string> TrafficJamByZones()
    {
        var rezultati = crossRoads
            .GroupBy(x => x.CityZone)
            .Select(g => new { zona = g.Key, prosjek = g.Average(x => (double?)x.TrafficJamPercantage ?? 0.0) });
        var sp = new StringBuilder();

        foreach (var r in rezultati)
        {
            sp.AppendLine($"Zona {r.zona} | Average Traffic jam {r.prosjek}%");
        }
        return sp.ToString(); 
    }


    public string AnalizeCriticalPoints()
    {
        var sp = new StringBuilder(); 
        var HighJamCrossRoads = crossRoads.Where(x => x.TrafficJamPercantage > 80);
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
            await _context.CityNodes.AddAsync(novi);
            await _context.SaveChangesAsync();
        }
        catch (CityExceptionSystem ex) { }
        

        
    }
}



