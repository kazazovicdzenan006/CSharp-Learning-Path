
public class CityService
{
    private List<CityNode> _allNodes = new List<CityNode>();
    private CityManager<CityNode> _manager = new(); 

    public CityService(List<CityNode> initialNodes, CityManager<CityNode> manager)
    {
        _allNodes = initialNodes;
        _manager = manager; 
    }
    public bool Exists(int id)
    {
        return _allNodes.Any(x => x.CityId == id);
    }
    public void AllLocaations(Action<string> formatizer)
    {
        foreach (var node in _allNodes)
        {
            string info = $"[{node.GetType().Name}] ID: {node.CityId}, Zona {node.CityZone}, Adresa {node.StreetName}";
            formatizer(info); 
        }
    }


    public void TrafficJamByZones(Action<string> writer)
    {
        var rezultati = _allNodes.OfType<CrossRoad>()
            .GroupBy(x => x.CityZone)
            .Select(g => new { zona = g.Key, prosjek = g.Average(x => x.TrafficJamPercantage) });


        foreach (var r in rezultati)
        {
            writer($"Zona {r.zona} | Average Traffic jam {r.prosjek}%");
        }

    }


    public void AnalizeCriticalPoints(Action<string> alarm)
    {
        var HighJamCrossRoads = _allNodes.OfType<CrossRoad>().Where(x => x.TrafficJamPercantage > 80);
        var HighlyOcuppiedParkingLots = _allNodes.OfType<ParkingLot>().Where(x => x.AvailableParkingSpots < 5);  
        if (HighJamCrossRoads.Any())
        {
            foreach (var item in HighJamCrossRoads)
            {
                alarm($"Crossroad {item.CrossName} in zone {item.CityZone} has high traffic jam of {item.TrafficJamPercantage}%"); 
            }
        }
        if (HighlyOcuppiedParkingLots.Any())
        {
            foreach (var p in HighlyOcuppiedParkingLots)
            {
                alarm($"Parking {p.ParkingName} in zone {p.CityZone} has only {p.AvailableParkingSpots} available spots left"); 
            }
        }
    }
 

    public async Task SaveCurrentState()
    {
        await _manager.SaveData(_allNodes, "data.json"); 
    }
    public async Task<List<CityNode>> loadCurrentState()
    {
        var loadedData = await _manager.LoadData("data.json");
        _allNodes = loadedData; 
        return loadedData; 
    }


    public void AddNode(CityNode novi)
    {
        SafeExecutor.Execute(
            () => { _allNodes.Add(novi);  } 
            ); 
    }
}



