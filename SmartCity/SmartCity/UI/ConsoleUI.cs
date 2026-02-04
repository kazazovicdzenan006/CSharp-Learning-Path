

using System.ComponentModel.Design;
using System.Transactions;

public class ConsoleUI {
    private readonly CityService _service; 
    public ConsoleUI(CityService service) {
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


    public  void ConsoleAddObject()
    {
        Console.WriteLine("To add parking or crossroad, follow next steps: ");

        int id = ReadInt("Enter city id: ");
        Console.WriteLine("Enter city zone: ");
        var zone = Console.ReadLine();
        Console.WriteLine("Enter street name: ");
        var street = Console.ReadLine();
        Console.WriteLine("if you want to add parking enter 1, if you want to add crossroad enter 2");
        int objectType = 0;
        while (objectType != 1 && objectType != 2)
        {
            objectType = ReadInt("Your choice: ");
            if (objectType != 1 && objectType != 2) Console.WriteLine("Invalid option!");
        }
        if (objectType == 1)
        {
            Console.WriteLine("Enter a parking name: ");
            var parkName = Console.ReadLine();
            
           
            int TotalSpots = ReadInt("Enter a number of total parking spots for your parking lot");
           
            
           
            int AvailableSpots = ReadInt("Enter Available parking spots: ");
           
           
            ParkingLot parking = new ParkingLot(id, zone, street, parkName, TotalSpots, AvailableSpots);
            _service.AddNode(parking);
        }
        if (objectType == 2)
        {
            Console.WriteLine("Enter a crossroad name: ");
            var crossName = Console.ReadLine();
            Console.WriteLine();
            
            int TrafficJam = ReadInt("Enter a traffic jam from 0 to 100");
           

            CrossRoad cross = new CrossRoad(id, zone, street, crossName, TrafficJam);
            _service.AddNode(cross);
        }

    }

    
    public  async Task MainMenu()
    {
        while (true)
        {
            Console.WriteLine($"Chose one of available options: \n" +
                $"1. Load Current Data \n " +
                $"2. Save Current Data \n " +
                $"3. Add New CityNode Object \n " +
                $"4. Analitics \n " +
                $"5. exit \n");
            int unos = 0;

            bool success = false;
            while (true)
            {
                success = int.TryParse(Console.ReadLine(), out int result);
                if (success)
                {
                    if (result > 0 && result <= 5)
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

            if(unos == 5)
            {
                break; 
            }

            switch (unos)
            {
                case 1:
                    var loaded = await _service.LoadCurrentState();
                    Console.WriteLine("Data is loaded \n \n");
                    var parkLots = loaded.OfType<ParkingLot>().ToList();
                    parkLots.ForEach(x => Console.WriteLine($"City ID: {x.CityId}, City Zone {x.CityZone}, StreetName: {x.StreetName}, " +
                        $"Parking name: {x.ParkingName}, Total Space: {x.TotalParkingSpots}, Available Spots {x.AvailableParkingSpots}"));
                    var crossRoads = loaded.OfType<CrossRoad>().ToList();
                    crossRoads.ForEach(x => Console.WriteLine($"City ID: {x.CityId}, City Zone {x.CityZone}, StreetName: {x.StreetName}," +
                        $"CrossRoad name {x.CrossName}, Traffic jam {x.TrafficJamPercantage}% "));
                    break;
                case 2:

                    await _service.SaveCurrentState();
                    Console.WriteLine("Data saved");

                    break;
                case 3:
                    ConsoleAddObject();
                    break;

                case 4:
                    Console.WriteLine("All locations: ");
                    _service.AllLocaations(Console.WriteLine);
                    Console.WriteLine("Traffic jam by zones: ");
                    _service.TrafficJamByZones(Console.WriteLine);
                    Console.WriteLine("Critical points");
                    _service.AnalizeCriticalPoints(
                        text =>
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(text);
                            Console.ResetColor(); 
                        }
                        );
                    break;
                   
            }
           

        }
    }



}



