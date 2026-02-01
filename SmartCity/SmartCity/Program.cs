
using System.Threading.Channels;
var manager = new CityManager<CityNode>(); 
List<CityNode> allData = new List<CityNode>();
var cityServis = new CityService(allData, manager);
allData.AddRange(DataInitializer.GetParkingLotSeedData());
allData.AddRange(DataInitializer.GetCrossRoadSeedData());


await ConsoleUI.MainMenu(cityServis); 
/* this will go in Console UI
Console.WriteLine("Izvjestaj o zonama: ");


cityServis.AllLocaations(Console.WriteLine);

cityServis.TrafficJamByZones(
    text =>
    {
        Console.ForegroundColor = ConsoleColor.Blue; 
        Console.WriteLine(text);
        Console.ResetColor(); 
    }
    ); */