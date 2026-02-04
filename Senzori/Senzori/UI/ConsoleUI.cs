

public class ConsoleUI
{
    private readonly SenzorService _service;
    public ConsoleUI(SenzorService service)
    {
        _service = service; 
    }

    public async Task MainMenu()
    {
        while (true)
        {
            Console.WriteLine($"Chose one of available options: \n" +
                $"1. Load Current Data \n " +
                $"2. Save Current Data \n " +
                $"3. Add New Device Object \n " +
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

            if (unos == 5)
            {
                break;
            }

            switch (unos)
            {
                case 1:
                    var loaded = await _service.LoadCurrentState();
                    Console.WriteLine("Data is loaded \n \n");
                    var senzor = loaded.OfType<Senzor>().ToList();
                    senzor.ForEach(x => Console.WriteLine($"Senzor ID: {x.Id}, Senzor Name {x.Name}, City Name {x.grad}, Senzor Value: {x.Vrijednost}"));
                      
                    var kontroler = loaded.OfType<Kontroler>().ToList();
                    kontroler.ForEach(x => Console.WriteLine($"Controller ID: {x.Id}, Controller Name {x.Name}, Controller Model {x.ModelKontrolera}," +
                        $"Controller Status: {x.Status}, Controller Channell Number {x.BrojKanala} "));
                    break;
                case 2:

                    await _service.SaveCurrentState();
                    Console.WriteLine("Data saved");

                    break;
                case 3:
                    ConsoleAddObject();
                    break;

                case 4:
                    Console.WriteLine("Analitics: ");
                    _service.Analitics(Console.WriteLine);
                    Console.WriteLine("Data Report: \n");
                    _service.GenerisiIzvjestaj(Console.WriteLine);
                    
                    break;

            }


        }
    }


    public void ConsoleAddObject()
    {
        Console.WriteLine("--- Adding a New Device ---");

        // 1. ID input with existence check
        int id = ReadInt("Enter Device ID: ");
        while (_service.Exists(id))
        {
            Console.WriteLine("ID already taken! Please try again.");
            id = ReadInt("Enter Device ID: ");
        }

        // 2. Common properties (Base class)
        Console.Write("Enter Device Name: ");
        string name = Console.ReadLine() ?? "Unknown";

        // 3. Device type selection
        Console.WriteLine("Select Type: 1 - Sensor, 2 - Controller");
        int type = 0;
        while (type != 1 && type != 2)
        {
            type = ReadInt("Your choice: ");
            if (type != 1 && type != 2) Console.WriteLine("Invalid option!");
        }

        // 4. Specific input based on type
        if (type == 1)
        {
            Console.Write("Enter City: ");
            string city = Console.ReadLine() ?? "Unknown";
            double value = ReadDouble("Enter initial value: ");

            try
            {
                Senzor s = new Senzor(id, name, city, value);
                _service.AddDevice(s);
                Console.WriteLine("Sensor successfully added!");
            }
            catch (DeviceLimitException ex)
            {
                Console.WriteLine($"Error creating sensor: {ex.Message}");
            }
        }
        else if (type == 2)
        {
            Console.Write("Enter Controller Model: ");
            string model = Console.ReadLine() ?? "DefaultModel";

            Console.WriteLine("Is it active? (1 - Yes, 0 - No): ");
            bool status = ReadInt("Status: ") == 1;

            int channels = ReadInt("Enter number of channels (0-8): ");

            try
            {
                Kontroler k = new Kontroler(id, name, model, status, channels);
                _service.AddDevice(k);
                Console.WriteLine("Controller successfully added!");
            }
            catch (DeviceLimitException ex)
            {
                Console.WriteLine($"Error creating controller: {ex.Message}");
            }
        }
    }

    // Helper methods to handle parsing and loops centrally
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

    private double ReadDouble(string message)
    {
        double result;
        Console.Write(message);
        while (!double.TryParse(Console.ReadLine(), out result))
        {
            Console.WriteLine("Invalid input! Please enter a valid number (e.g., 25.5).");
            Console.Write(message);
        }
        return result;
    }


}


