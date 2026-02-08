

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
                $"1. View All Data \n " +
                $"2. Add New Device Object \n " +
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

            if (unos == 4)
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
                    var analiza = await _service.Analitics();
                    Console.WriteLine(analiza); 

                    break;

       

            }


        }
    }


    public async Task ConsoleAddObject()
    {
        Console.WriteLine("--- Adding a New Device ---");

        // 1. ID input with existence check
       

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
                Senzor s = new Senzor 
                {
                    Name = name, 
                    grad = city,
                    Vrijednost = value };
                await _service.AddDevice(s);
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
                Kontroler k = new Kontroler
                {
                    Name = name, 
                    ModelKontrolera = model,
                    Status = status,
                    BrojKanala = channels 
                };
                 await _service.AddDevice(k);
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



    public async Task GenerisiIzvjestaj()
    {
        var _allDevices = await _service.GetReportData();
        const int col1 = 12;
        const int col2 = 30;
        const int col3 = 40;
        const int col4 = 45;
        int sirinaTabele = col1 + col2 + col3 + col4;
        string linija = new string('-', sirinaTabele);
        Console.WriteLine(linija);
        Console.WriteLine($"\n{"TIP",-col1} | {"NAZIV",-col2} | {"STATUS",-col3} | {"DETALJI",-col4}\n\n{linija}");
        foreach (var item in _allDevices)
        {
            var tip = item.GetType();
            string detalji = "";
            var status = (item is ITemperature t) ? t.GetStatus() : "N/A";  // because I can't access to GetStatus directly from _allData except if i implement interface in base class
            if (item is Senzor s)
            {
                detalji = $"Grad: {s.grad}, vrijednost {s.Vrijednost}";
            }
            else if (item is Kontroler k)
            {
                detalji = $"Kanala: {k.BrojKanala}, model: {k.ModelKontrolera}";
            }

            Console.WriteLine($"{tip,-col1} | {((Uredjaj)item).Name,-col2} | {status,-col3} | {detalji,-col4}");
        }
        Console.WriteLine(linija);
    }



}


