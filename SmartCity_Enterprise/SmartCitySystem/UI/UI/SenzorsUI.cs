

public class SenzorUI
{
    private readonly SenzorService _service;
    public SenzorUI(SenzorService service)
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
                $"4. Update object \n" +
                $"5. Delete object \n" +
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
                    var analiza = await _service.Analitics();
                    Console.WriteLine(analiza);

                    break;
                case 4:
                    await ConsoleUpdateObject();
                    break;
                case 5:
                    await DeleteObject();
                    break;

            }


        }
    }

    public async Task DeleteObject()
    {
        await GenerisiIzvjestaj();
        int id = ReadInt("Enter id of the element you want to delete: ");


        string greska = await SafeExecutor.ExecuteAsync(async () => {
            await _service.DeleteDevice(id);
        });

        if (greska != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(greska);
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("Device deleted successfully!");
        }
    }
    public async Task ConsoleAddObject()
    {
        Console.WriteLine("--- Adding a New Device ---");

        var cities = await _service.GetAvailableCities();
        if (cities == null || !cities.Any())
        {
            Console.WriteLine("Error: No cities found in database.");
            return;
        }

        Console.WriteLine("\nSelect a city for this device:");
        for (int i = 0; i < cities.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cities[i].Name}");
        }

        int cityIndex = 0;
        while (cityIndex < 1 || cityIndex > cities.Count)
        {
            cityIndex = ReadInt($"Selection (1-{cities.Count}): ");
        }
        int selectedGradId = cities[cityIndex - 1].Id;


        Console.Write("Enter Device Name: ");
        string name = Console.ReadLine() ?? "Unknown";

        Console.WriteLine("Select Type: 1 - Sensor, 2 - Controller");
        int type = 0;
        while (type != 1 && type != 2)
        {
            type = ReadInt("Your choice: ");
        }

        if (type == 1) 
        {
 
            double value = ReadDouble("Enter initial value: ");

            try
            {
                Senzor s = new Senzor
                {
                    Name = name,
                    GradId = selectedGradId, 
                    Vrijednost = value
                };
                await _service.AddDevice(s);
                Console.WriteLine("Sensor successfully added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        else if (type == 2) 
        {
            Console.Write("Enter Controller Model: ");
            string model = Console.ReadLine() ?? "DefaultModel";
            bool status = ReadInt("Is active? (1 - Yes, 0 - No): ") == 1;
            int channels = ReadInt("Number of channels (0-8): ");

            try
            {
                Kontroler k = new Kontroler
                {
                    Name = name,
                    GradId = selectedGradId, 
                    ModelKontrolera = model,
                    Status = status,
                    BrojKanala = channels
                };
                await _service.AddDevice(k);
                Console.WriteLine("Controller successfully added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
    public async Task ConsoleUpdateObject()
    {
        await GenerisiIzvjestaj();
        int id = ReadInt("Enter ID to update: ");
        var device = await _service.GetDeviceById(id);
        if (device == null)
        {
            Console.WriteLine($"Uređaj sa ID-em {id} nije pronađen.");
            return;
        }

        if (device is Senzor)
        {
            Console.WriteLine("Updating Sensor...");
            Console.Write("Novi naziv: ");
            string newName = Console.ReadLine();
            double newVal = ReadDouble("Nova vrijednost: ");
            int newGradId = ReadInt("Novi ID grada: ");


            var newSenzor = new Senzor {
                Name = newName,
                Vrijednost = newVal,
                GradId = newGradId
            };
            await _service.UpdateDevice(id, newSenzor);
        }
        else if (device is Kontroler)
        {
            Console.WriteLine("Updating Controller...");
            Console.Write("Novi naziv: ");
            string newName = Console.ReadLine() ?? "Unknown";

            Console.Write("Novi model kontrolera: ");
            string newModel = Console.ReadLine() ?? "DefaultModel";

            Console.WriteLine("Novi status (1 - Aktivan, 0 - Neaktivan): ");
            bool newStatus = ReadInt("Status: ") == 1;

            int newChannels = ReadInt("Novi broj kanala (0-8): ");

            int newGradId = ReadInt("Novi ID grada: ");

            
            Kontroler newKontroler = new Kontroler
            {
                Name = newName,
                ModelKontrolera = newModel,
                Status = newStatus,
                BrojKanala = newChannels,
                GradId = newGradId
            };
            
            await _service.UpdateDevice(id, newKontroler);
        }
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


        const int col0 = 5;  
        const int col1 = 12; 
        const int col2 = 25; 
        const int col3 = 35;
        const int col4 = 45; 

        int sirinaTabele = col0 + col1 + col2 + col3 + col4 + 10; 
        string linija = new string('-', sirinaTabele);

        Console.WriteLine(linija);
        Console.WriteLine($"{"ID",-col0} | {"TYPE",-col1} | {"NAME",-col2} | {"STATUS",-col3} | {"DETAILS",-col4}");
        Console.WriteLine(linija);

        foreach (var item in _allDevices)
        {
            var tip = item.GetType().Name; 
            string detalji = "";

   
            var status = (item is ITemperature t) ? t.GetStatus() : "N/A";

       
            if (item is Senzor s)
            {
             
                string gradIme = s.Grad != null ? s.Grad.Name : "N/A";
                detalji = $"City: {gradIme}, Value: {s.Vrijednost}";
            }
            else if (item is Kontroler k)
            {
                detalji = $"Channels: {k.BrojKanala}, Model: {k.ModelKontrolera}";
            }

            
            Console.WriteLine($"{item.Id,-col0} | {tip,-col1} | {item.Name,-col2} | {status,-col3} | {detalji,-col4}");
        }
        Console.WriteLine(linija);
    }



}


