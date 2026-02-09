

using System.ComponentModel.Design;
using System.Transactions;

public class SmartCityUI
{
    private readonly CityService _service;
    public SmartCityUI(CityService service)
    {
        _service = service;
    }

    private int ReadInt(string message)
    {
        int result;
        Console.Write(message);
        while (!int.TryParse(Console.ReadLine(), out result))
        {
            Console.WriteLine("Invalid input! Please enter a whole number.");
            Console.Write(message);
        }
        return result;
    }


    public async Task ConsoleAddObject()
    {
        Console.WriteLine("To add parking or crossroad, follow next steps: ");


        var cities = await _service.GetAvailableCities(); 

        if (cities == null || !cities.Any())
        {
            Console.WriteLine("No cities found. Create a city first!");
            return;
        }

        Console.WriteLine("Select city:");
        for (int i = 0; i < cities.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cities[i].Name}");
        }

        int cityIndex = ReadInt("Choice: ");
        int selectedGradId = cities[cityIndex - 1].Id;


        Console.WriteLine("Enter city zone: ");
        var zone = Console.ReadLine();
        Console.WriteLine("Enter street name: ");
        var street = Console.ReadLine();

        Console.WriteLine("Select type: 1 - Parking, 2 - Crossroad");
        int objectType = ReadInt("Your choice: ");

    
        if (objectType == 1)
        {
            Console.WriteLine("Enter a parking name: ");
            var parkName = Console.ReadLine();
            int TotalSpots = ReadInt("Total spots: ");
            int AvailableSpots = ReadInt("Available spots: ");

            ParkingLot parking = new ParkingLot
            {
                CityZone = zone,
                StreetName = street,
                ParkingName = parkName,
                TotalParkingSpots = TotalSpots,
                AvailableParkingSpots = AvailableSpots,
                GradId = selectedGradId 
            };
            await _service.AddNode(parking);
        }
        else if (objectType == 2)
        {
            Console.WriteLine("Enter a crossroad name: ");
            var crossName = Console.ReadLine();
            int TrafficJam = ReadInt("Traffic jam (0-100): ");

            CrossRoad cross = new CrossRoad
            {
                CityZone = zone,
                StreetName = street,
                CrossName = crossName,
                TrafficJamPercantage = TrafficJam,
                GradId = selectedGradId // OBAVEZNO DODIJELITI ID
            };
            await _service.AddNode(cross);
        }
    }


    public async Task MainMenu()
    {
        while (true)
        {
            Console.WriteLine($"Chose one of available options: \n" +
                $"1. View all Data \n " +
                $"2. Add New CityNode Object \n " +
                $"3. Analitics \n " +
                $"4. Update Object \n" + 
                $"5. Delete Object \n"+
                $"6. exit \n");
            int unos = 0;

            bool success = false;
            while (true)
            {
                success = int.TryParse(Console.ReadLine(), out int result);
                if (success)
                {
                    if (result > 0 && result <= 6)
                    {
                        unos = result;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("We don't have that option!");
                    }
                }
                else
                {
                    Console.WriteLine("You didn't enter a number! ");
                }
            }

            if (unos == 6)
            {
                break;
            }

            switch (unos)
            {
                case 1:
                    await GenerisiIzvjestaj();
                    break;
                case 2:
                    await ConsoleAddObject();


                    break;
                case 3:
                    Console.WriteLine("All locations: ");
                    var locations = await _service.AllLocaations();
                    Console.WriteLine(locations);
                    Console.WriteLine("Traffic jam by zones: ");
                    var jam = await _service.TrafficJamByZones();
                    Console.WriteLine(jam);
                    Console.WriteLine("Critical points");
                    var critical = await _service.AnalizeCriticalPoints();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(critical);
                    Console.ResetColor();


                    break;

                case 4:
                    await ConsoleUpdateObject();
                    break;
                case 5:
                    await ConsoleDeleteObject();
                    break;


            }


        }
    }

    public async Task ConsoleUpdateObject()
    {
        await GenerisiIzvjestaj();
        int id = ReadInt("Enter the ID of the node you want to update: ");
        var node = await _service.GetNodeById(id);

        if (node == null)
        {
            Console.WriteLine("Node not found!");
            return;
        }

        var cities = await _service.GetAllCitiesAsync();
        Console.WriteLine("\nSelect City:");
        for (int i = 0; i < cities.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cities[i].Name}");
        }
        int cityIndex = ReadInt("Choice: ");
        int selectedGradId = cities[cityIndex - 1].Id;

        Console.Write("New City Zone: ");
        string zone = Console.ReadLine();
        Console.Write("New Street Name: ");
        string street = Console.ReadLine();

        CityNode updatedData = null;

        if (node is ParkingLot)
        {
            Console.WriteLine("Updating Parking Lot...");
            Console.Write("New Parking Name: ");
            string pName = Console.ReadLine();
            int total = ReadInt("Total Spots: ");
            int available = ReadInt("Available Spots: ");

            updatedData = new ParkingLot
            {
                CityZone = zone,
                StreetName = street,
                ParkingName = pName,
                TotalParkingSpots = total,
                AvailableParkingSpots = available,
                GradId = selectedGradId
            };
        }
        else if (node is CrossRoad)
        {
            Console.WriteLine("Updating Crossroad...");
            Console.Write("New Crossroad Name: ");
            string cName = Console.ReadLine();
            int jam = ReadInt("Traffic Jam Percentage (0-100): ");

            updatedData = new CrossRoad
            {
                CityZone = zone,
                StreetName = street,
                CrossName = cName,
                TrafficJamPercantage = jam,
                GradId = selectedGradId
            };
        }


        string error = await SafeExecutor.ExecuteAsync(async () => {
            await _service.UpdateNode(id, updatedData);
        });

        if (error != null) Console.WriteLine(error);
        else Console.WriteLine("Node updated successfully!");
    }

    public async Task ConsoleDeleteObject()
    {
        await GenerisiIzvjestaj();
        int id = ReadInt("Enter ID to delete: ");

        string error = await SafeExecutor.ExecuteAsync(async () => {
            await _service.DeleteNode(id);
        });

        if (error != null) Console.WriteLine(error);
        else Console.WriteLine("Node deleted successfully.");
    }

    public async Task GenerisiIzvjestaj()
    {
        
        var locations = await _service.GetReportData();

        const int col0 = 5;  
        const int col1 = 12; 
        const int col2 = 20; 
        const int col3 = 25; 
        const int col4 = 55; 

        int sirinaTabele = col0 + col1 + col2 + col3 + col4 + 10;
        string linija = new string('-', sirinaTabele);

        Console.WriteLine($"\n\n{linija}");
        Console.WriteLine($"{"ID",-col0} | {"TYPE",-col1} | {"ZONE",-col2} | {"STREET",-col3} | {"DETAILS",-col4}");
        Console.WriteLine(linija);

        foreach (var node in locations)
        {
            string tip = node.GetType().Name;
            string detalji = "";

            if (node is ParkingLot p)
            {
                detalji = $"Name: {p.ParkingName}, Spots: {p.AvailableParkingSpots}/{p.TotalParkingSpots}";
            }
            else if (node is CrossRoad c)
            {
                detalji = $"Name: {c.CrossName}, Traffic Jam: {c.TrafficJamPercantage}%";
            }

            Console.WriteLine($"{node.Id,-col0} | {tip,-col1} | {node.CityZone,-col2} | {node.StreetName,-col3} | {detalji,-col4}");
        }

        Console.WriteLine(linija);
    }


}



