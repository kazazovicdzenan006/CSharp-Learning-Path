

using System.ComponentModel.Design;
using System.Transactions;

public class ConsoleUI {


    public static void ConsoleAddObject(CityService service)
    {
        Console.WriteLine("To add parking or crossroad, follow next steps: ");
        Console.WriteLine("Enter city id: ");
        bool idsuccess = false;
        int id;
        while (true)
        {
            idsuccess = int.TryParse(Console.ReadLine(), out int idTemp);
            if (idsuccess)
            {
                id = idTemp;
                break;
            }
            else
            {
                Console.WriteLine("You didn't enter a number");
            }
        }
        Console.WriteLine("Enter city zone: ");
        var zone = Console.ReadLine();
        Console.WriteLine("Enter street name: ");
        var street = Console.ReadLine();
        Console.WriteLine("if you want to add parking enter 1, if you want to add crossroad enter 2");
        bool objectTypeSuccess = false;
        int objectType;
        while (true)
        {
            objectTypeSuccess = int.TryParse(Console.ReadLine(), out int type);
            if (objectTypeSuccess)
            {
                if (type == 1 || type == 2)
                {
                    objectType = type;
                    break;
                }
                else
                {
                    Console.WriteLine("We don't have that option!");
                }

            }
            else
            {
                Console.WriteLine("You didn't enter a number");
            }
        }
        if (objectType == 1)
        {
            Console.WriteLine("Enter a parking name: ");
            var parkName = Console.ReadLine();
            Console.WriteLine("Enter a number of total parking spots for your parking lot");
            bool totalsuccess = false;
            int TotalSpots;
            while (true)
            {
                totalsuccess = int.TryParse(Console.ReadLine(), out int total);
                if (totalsuccess)
                {
                    TotalSpots = total;
                    break;
                }
                else
                {
                    Console.WriteLine("You didn't enter a number");
                }
            }
            Console.WriteLine("Enter Available parking spots: ");
            bool availablesuccess = false;
            int AvailableSpots;
            while (true)
            {
                availablesuccess = int.TryParse(Console.ReadLine(), out int available);
                if (availablesuccess)
                {
                    AvailableSpots = available;
                    break;
                }
                else
                {
                    Console.WriteLine("You didn't enter a number");
                }
            }
            ParkingLot parking = new ParkingLot(id, zone, street, parkName, TotalSpots, AvailableSpots);
            service.AddNode(parking);
        }
        if (objectType == 2)
        {
            Console.WriteLine("Enter a crossroad name: ");
            var crossName = Console.ReadLine();
            Console.WriteLine("Enter a traffic jam from 0 to 100");
            bool trafficsuccess = false;
            int TrafficJam;
            while (true)
            {
                trafficsuccess = int.TryParse(Console.ReadLine(), out int jam);
                if (trafficsuccess)
                {
                    TrafficJam = jam;
                    break;
                }
                else
                {
                    Console.WriteLine("You didn't enter a number");
                }
            }

            CrossRoad cross = new CrossRoad(id, zone, street, crossName, TrafficJam);
            service.AddNode(cross);
        }

    }

    public ConsoleUI(CityService service) { }
    public static async Task MainMenu(CityService service)
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
                    var loaded = await service.loadCurrentState();
                    Console.WriteLine("Data is loaded \n \n");
                    var parkLots = loaded.OfType<ParkingLot>().ToList();
                    parkLots.ForEach(x => Console.WriteLine($"City ID: {x.CityId}, City Zone {x.CityZone}, StreetName: {x.StreetName}, " +
                        $"Parking name: {x.ParkingName}, Total Space: {x.TotalParkingSpots}, Available Spots {x.AvailableParkingSpots}"));
                    var crossRoads = loaded.OfType<CrossRoad>().ToList();
                    crossRoads.ForEach(x => Console.WriteLine($"City ID: {x.CityId}, City Zone {x.CityZone}, StreetName: {x.StreetName}," +
                        $"CrossRoad name {x.CrossName}, Traffic jam {x.TrafficJamPercantage}% "));
                    break;
                case 2:

                    await service.SaveCurrentState();
                    Console.WriteLine("Data saved");

                    break;
                case 3:
                    ConsoleAddObject(service);
                    break;

                case 4:
                    Console.WriteLine("All locations: ");
                    service.AllLocaations(Console.WriteLine);
                    Console.WriteLine("Traffic jam by zones: ");
                    service.TrafficJamByZones(Console.WriteLine);
                    Console.WriteLine("Critical points");
                    service.AnalizeCriticalPoints(
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



