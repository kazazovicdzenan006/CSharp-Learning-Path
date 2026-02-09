

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


    public  async Task ConsoleAddObject()
    {
        Console.WriteLine("To add parking or crossroad, follow next steps: ");

        
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
           
           
            ParkingLot parking = new ParkingLot
            {
               CityZone = zone, 
               StreetName = street, 
               ParkingName = parkName, 
               TotalParkingSpots = TotalSpots,
               AvailableParkingSpots = AvailableSpots };
            await _service.AddNode(parking);
        }
        if (objectType == 2)
        {
            Console.WriteLine("Enter a crossroad name: ");
            var crossName = Console.ReadLine();
            Console.WriteLine();
            
            int TrafficJam = ReadInt("Enter a traffic jam from 0 to 100");
           

            CrossRoad cross = new CrossRoad
            {
               CityZone = zone, 
                StreetName = street, 
               CrossName = crossName, 
               TrafficJamPercantage = TrafficJam 
            };
            await _service.AddNode(cross);
        }

    }

    
    public  async Task MainMenu()
    {
        while (true)
        {
            Console.WriteLine($"Chose one of available options: \n" +
                $"1. View all Data \n " +
                $"2. Add New CityNode Object \n " +
                $"3. Analitics \n " +
                $"4. exit \n");
            int unos = 0;

            bool success = false;
            while (true)
            {
                success = int.TryParse(Console.ReadLine(), out int result);
                if (success)
                {
                    if (result > 0 && result <= 4)
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

            if(unos == 4)
            {
                break; 
            }

            switch (unos)
            {
                case 1:
                    var loaded = await _service.GetReportData();
                    
                    var parkLots = loaded.OfType<ParkingLot>().ToList();
                    parkLots.ForEach(x => Console.WriteLine($"City ID: {x.Id}, City Zone {x.CityZone}, StreetName: {x.StreetName}, " +
                        $"Parking name: {x.ParkingName}, Total Space: {x.TotalParkingSpots}, Available Spots {x.AvailableParkingSpots}"));
                    var crossRoads = loaded.OfType<CrossRoad>().ToList();
                    crossRoads.ForEach(x => Console.WriteLine($"City ID: {x.Id}, City Zone {x.CityZone}, StreetName: {x.StreetName}," +
                        $"CrossRoad name {x.CrossName}, Traffic jam {x.TrafficJamPercantage}% "));
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
                    var critical = _service.AnalizeCriticalPoints();
                        
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(critical);
                            Console.ResetColor();
                        
                        
                    break;

            
            }
           

        }
    }



}



