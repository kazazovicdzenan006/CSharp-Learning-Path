

using System.IO;
using System.Threading.Channels;

public class ConsoleUI
{
    private  readonly ParseExecutor _executor;
    private readonly BookStoreService _service; 
    public ConsoleUI(BookStoreService service, ParseExecutor executor)
    {
        _service = service;
        _executor = executor;
    }

    public  async Task MainMenu()
    {
        while (true)
        {
            Console.WriteLine($"Chose one of available options: \n" +
               $"1. Load Current Data \n " +
               $"2. Save Current Data \n " +
               $"3. Add New Movie or Book \n " +
               $"4. Analitics \n " +
               $"5. exit \n");

            string input = Console.ReadLine();
            int option = 0; 
                SafeExecutor.Execute(
                    () => { option = _executor.SafeParse( input ); });
            if (option == 0) { continue; }
            if (option == 5) { break; }
            switch (option)
            {
                case 1:
                    Console.WriteLine("Data loaded successfully!");
                    var data = await _service.LoadCurrentState();
                    var Books = data.OfType<Knjiga>().ToList();
                    var Movies = data.OfType<Film>().ToList();
                    Console.WriteLine("\nBooks: ");
                    Books.ForEach(x => Console.WriteLine($"Book ID {x.Id}, Book Name {x.Naslov}, Release date {x.GodinaIzdanja}, Author {x.Autor}, Page number {x.BrojStranica}"));
                    Console.WriteLine("\n\n");
                    Movies.ForEach(x => Console.WriteLine($"Movie ID {x.Id}, Movie Name {x.Naslov}, Release date {x.GodinaIzdanja}, Movie Director {x.Reziser}, Duration in minutes {x.TrajanjeUMinutama}"));
                    break;
                case 2:
                    await _service.SaveCurrentState();
                    Console.WriteLine("Data saved successfully");
                    break;
                case 3:
                    ConsoleAddObject(_service); 
                    break;
                case 4:
                    _service.Analiza(Console.WriteLine);
                    Console.WriteLine("\n books written after year 1800: \n");
                    var books = _service.FiltrirajKnjige(k => k.GodinaIzdanja > 1800);
                    books.ForEach(x => Console.WriteLine($"Book ID {x.Id}, Book Name {x.Naslov}, Release date {x.GodinaIzdanja}, Author {x.Autor}, Page number {x.BrojStranica}"));
                    _service.GenerisiIzvjestaj(Console.WriteLine);

                    break;
            }









        }
    }




    public void ConsoleAddObject(BookStoreService service)
    {
        Console.WriteLine("To add Movie or Book, follow next steps: ");


        Console.WriteLine("if you want to add Book enter 1, if you want to add Movie enter 2");
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
        Console.WriteLine("Enter id: ");
        bool idsuccess = false;
        int id = 0;
        while (true)
        {
            bool success = false;
            SafeExecutor.Execute(() =>
            {
                id = _executor.SafeParse(Console.ReadLine());
                success = service.Exists(id);
            });
            if (success)
            {
                Console.WriteLine("ID already taken!");
                continue;
            }
            else
            {
                break;
            }

        }
        if (objectType == 1)
        {
            Console.WriteLine("Enter Book Name: ");
            var bookName = Console.ReadLine();
            Console.WriteLine("Enter a Year of Release Date: ");
            string releaseDate = Console.ReadLine();
            int year = 0; 
            bool success = false; 
            while (true)
            {
                success = int.TryParse(Console.ReadLine(), out int total);
                if (success)
                {
                    year = total;
                    break;
                }
                else
                {
                    Console.WriteLine("You didn't enter a number");
                }
            }
            Console.WriteLine("Enter an Author: ");
            var author = Console.ReadLine();
            Console.WriteLine("Enter page number: ");
            bool totalsuccess = false;
            int PageNumber;
            while (true)
            {
                totalsuccess = int.TryParse(Console.ReadLine(), out int total);
                if (totalsuccess)
                {
                    PageNumber = total;
                    break;
                }
                else
                {
                    Console.WriteLine("You didn't enter a number");
                }
            }
           
            Knjiga book = new Knjiga(id, bookName, year, author , PageNumber);
            service.AddNewItem(book);
                }
        if (objectType == 2)
        {
            Console.WriteLine("Enter Movie Name: ");
            var movieName = Console.ReadLine();
            Console.WriteLine("Enter a Year of Release Date: ");
            string releaseDate = Console.ReadLine();
            int year = 0;
            bool success = false;
            while (true)
            {
                success = int.TryParse(Console.ReadLine(), out int total);
                if (success)
                {
                    year = total;
                    break;
                }
                else
                {
                    Console.WriteLine("You didn't enter a number");
                }
            }
            Console.WriteLine("Enter a Movie Director: ");
            var movieDirector = Console.ReadLine();
            Console.WriteLine("Enter a Movie Duration in minutes: ");
            bool totalsuccess = false;
            int DurationInMinutes;
            while (true)
            {
                totalsuccess = int.TryParse(Console.ReadLine(), out int total);
                if (totalsuccess)
                {
                    DurationInMinutes = total;
                    break;
                }
                else
                {
                    Console.WriteLine("You didn't enter a number");
                }
            }

            Film movie = new Film(id, movieName, year, movieDirector, DurationInMinutes);
            service.AddNewItem(movie);
        }

    
  



    }
}



