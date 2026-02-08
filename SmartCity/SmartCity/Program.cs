
using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;
using var context = new CityContext();
var cityServis = new CityService(context);
await context.Database.ExecuteSqlRawAsync("DELETE FROM CityNodes");

if (!context.CityNodes.Any())
{
    var ParkingLots = DataInitializer.GetParkingLotSeedData();
    foreach (var p in ParkingLots)
    {
        await cityServis.AddNode(p);
    }

    var CrossRoads = DataInitializer.GetCrossRoadSeedData();
    foreach (var c in CrossRoads)
    {
        await cityServis.AddNode(c);
    }
}




var ui = new ConsoleUI(cityServis);
await ui.MainMenu();
