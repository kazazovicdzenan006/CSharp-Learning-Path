

public class ConsoleUI {
    
    public ConsoleUI(CityService service) { }
        public static async Task MainMenu(CityService service)
    {
        Console.WriteLine($"Chose one of available options: \n" +
            $"1. Load Current Data \n " +
            $"2. Save Current Data \n " +
            $"3. Add New CityNode Object \n " +
            $"4. Analitics \n ");
        int unos = 0;

        bool success = false;
        while (true)
        {
            success = int.TryParse(Console.ReadLine(), out int result);
            if (success)
            {
                unos = result;
                break;
            }
            else
            {
                Console.WriteLine("Niste odabrali nijednu od ponduenih opcija!");
            }
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

                break; 
        }

    }



}



