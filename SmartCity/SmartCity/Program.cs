
using System.Threading.Channels;
var manager = new CityManager<CityNode>(); 
List<CityNode> allData = new List<CityNode>();
allData.AddRange(DataInitializer.GetParkingLotSeedData());
allData.AddRange(DataInitializer.GetCrossRoadSeedData());
var cityServis = new CityService(allData, manager);



var ui = new ConsoleUI(cityServis);
await ui.MainMenu();
