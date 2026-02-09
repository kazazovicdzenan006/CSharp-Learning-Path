

using System.IO;
using System.Threading.Channels;
using Domain.Models;

public class BookStoreUI
{
    private readonly ParseExecutor _executor;
    private readonly BookStoreService _service;
    public BookStoreUI(BookStoreService service, ParseExecutor executor)
    {
        _service = service;
        _executor = executor;
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


    public async Task MainMenu()
    {
        while (true)
        {
            Console.WriteLine($"Chose one of available options: \n" +
               $"1. View All Items \n " +
               $"2. Add New Movie or Book \n " +
               $"3. Analitics \n " +
               $"4. Update Object \n" + 
               $"5. Delete Object \n" +
               $"6. exit \n");
            // we don't need option save because database is now saving automatically
            
            int option = 0;
            
            
                option = ReadInt("Enter option that you want: ");
            

          
            if (option == 0) { continue; }
            if (option == 6) { break; }
            switch (option)
            {
                case 1:
                    await GenerisiIzvjestaj();
                    break;
                case 2:
                    await ConsoleAddObject(_service);
                    break;
                case 3:
                    var analiza = await _service.Analiza();
                    Console.WriteLine(analiza);
                    Console.WriteLine("\n books written after year 1800: \n");
                    var books = await _service.FiltrirajKnjige(k => k.GodinaIzdanja > 1800);
                    books.ForEach(x => Console.WriteLine($"Book ID {x.Id}, Book Name {x.Naslov}, Release date {x.GodinaIzdanja}, Author {x.Autor}, Page number {x.BrojStranica}"));

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


    public async Task ConsoleAddObject(BookStoreService service)
    {
        Console.WriteLine("\n--- Adding New Item to Library ---");

      
        var cities = await service.GetAvailableCities();
        if (cities == null || !cities.Any())
        {
            Console.WriteLine("Error: No cities found. Please seed the database first.");
            return;
        }

        Console.WriteLine("Select a city for this item:");
        for (int i = 0; i < cities.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cities[i].Name}");
        }

        int cityIndex = 0;
        while (cityIndex < 1 || cityIndex > cities.Count)
        {
            cityIndex = ReadInt($"Choice (1-{cities.Count}): ");
        }

  
        int selectedGradId = cities[cityIndex - 1].Id;


        Console.WriteLine("\nWhat do you want to add? (1 - Book, 2 - Movie)");
        int objectType = 0;
        while (objectType != 1 && objectType != 2)
        {
            objectType = ReadInt("Your choice: ");
        }

     
        if (objectType == 1)
        {
            Console.Write("Enter Book Name: ");
            var bookName = Console.ReadLine();
            int year = ReadInt("Enter Year: ");
            Console.Write("Enter Author: ");
            var author = Console.ReadLine();
            int pageNumber = ReadInt("Enter page number: ");

            Knjiga book = new Knjiga
            {
                Naslov = bookName,
                GodinaIzdanja = year,
                Autor = author,
                BrojStranica = pageNumber,
                GradId = selectedGradId 
            };

            await service.AddNewItem(book);
            Console.WriteLine("Book successfully added!");
        }
        else
        {
            Console.Write("Enter Movie Name: ");
            var movieName = Console.ReadLine();
            int year = ReadInt("Enter Year: ");
            Console.Write("Enter Director: ");
            var movieDirector = Console.ReadLine();
            int duration = ReadInt("Enter Duration (min): ");

            Film movie = new Film
            {
                Naslov = movieName,
                GodinaIzdanja = year,
                Reziser = movieDirector,
                TrajanjeUMinutama = duration,
                GradId = selectedGradId 
            };

            await service.AddNewItem(movie);
            Console.WriteLine("Movie successfully added!");
        }
    }


    public async Task DeleteObject()
    {
      
        await GenerisiIzvjestaj();
        int id = ReadInt("Enter id of the element you want to delete: ");

        await _service.DeleteDevice(id);
        Console.WriteLine("Device deleted succesfully!");
    }



    public async Task ConsoleUpdateObject()
    {
        await GenerisiIzvjestaj();
        int id = ReadInt("Enter the ID of the item you want to update: ");


        var item = await _service.GetArtikalById(id);

        if (item == null)
        {
            Console.WriteLine($"Item with ID {id} was not found.");
            return;
        }

        BibliotekaArtikal updatedData = null;

        if (item is Knjiga)
        {
            Console.WriteLine($"Updating Book: {item.Naslov}");
            Console.Write("New Title: ");
            string title = Console.ReadLine();
            int year = ReadInt("New Release Year: ");
            Console.Write("New Author: ");
            string author = Console.ReadLine();
            int pages = ReadInt("New Page Count: ");
            int cityId = ReadInt("New City ID: ");

            updatedData = new Knjiga
            {
                Naslov = title,
                GodinaIzdanja = year,
                Autor = author,
                BrojStranica = pages,
                GradId = cityId
            };
        }
        else if (item is Film)
        {
            Console.WriteLine($"Updating Movie: {item.Naslov}");
            Console.Write("New Title: ");
            string title = Console.ReadLine();
            int year = ReadInt("New Release Year: ");
            Console.Write("New Director: ");
            string director = Console.ReadLine();
            int duration = ReadInt("New Duration (min): ");
            int cityId = ReadInt("New City ID: ");

            updatedData = new Film
            {
                Naslov = title,
                GodinaIzdanja = year,
                Reziser = director,
                TrajanjeUMinutama = duration,
                GradId = cityId
            };
        }


        string error = await SafeExecutor.ExecuteAsync(async () => {
            await _service.UpdateArtikal(id, updatedData);
        });

        if (error != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("Item updated successfully!");
        }
    }

    public async Task GenerisiIzvjestaj()
    {
        var biblioteka = await _service.GetReportData();

   
        const int col0 = 5;  
        const int col1 = 15; 
        const int col2 = 25; 
        const int col3 = 15; 
        const int col4 = 60; 

        int sirinaTabele = col0 + col1 + col2 + col3 + col4 + 10;
        string linija = new string('-', sirinaTabele);

        Console.WriteLine($"\n\n{linija}");
        Console.WriteLine($"{"ID",-col0} | {"TYPE",-col1} | {"TITLE",-col2} | {"YEAR",-col3} | {"DETAILS",-col4}");
        Console.WriteLine(linija);

        foreach (var item in biblioteka)
        {
            string tip = item.GetType().Name;
            string detalji = "";

            if (item is Knjiga k)
            {
                detalji = $"Author: {k.Autor}, Pages: {k.BrojStranica}";
            }
            else if (item is Film f)
            {
                detalji = $"Director: {f.Reziser}, Duration: {f.TrajanjeUMinutama} min";
            }

            var baseItem = (BibliotekaArtikal)item;
            Console.WriteLine($"{baseItem.Id,-col0} | {tip,-col1} | {baseItem.Naslov,-col2} | {baseItem.GodinaIzdanja,-col3} | {detalji,-col4}");
        }

        Console.WriteLine(linija);
    }
}



