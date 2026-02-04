

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
        int objectType = 0;
        while (objectType != 1 && objectType != 2)
        {
            objectType = ReadInt("Your choice: ");
            if (objectType != 1 && objectType != 2) Console.WriteLine("Invalid option!");
        }
        int id = ReadInt("Enter  ID: ");
        while (_service.Exists(id))
        {
            Console.WriteLine("ID already taken! Please try again.");
            id = ReadInt("Enter  ID: ");
        }
        if (objectType == 1)
        {
            Console.WriteLine("Enter Book Name: ");
            var bookName = Console.ReadLine();
          
            
            int year = ReadInt("Enter a Year of Release Date: "); 
            
            Console.WriteLine("Enter an Author: ");
            var author = Console.ReadLine();
            
            int PageNumber = ReadInt("Enter page number: ");
           
           
            Knjiga book = new Knjiga(id, bookName, year, author , PageNumber);
            service.AddNewItem(book);
                }
        if (objectType == 2)
        {
            Console.WriteLine("Enter Movie Name: ");
            var movieName = Console.ReadLine();
           
     
            int year = ReadInt("Enter a Year of Release Date: ");
          
            Console.WriteLine("Enter a Movie Director: ");
            var movieDirector = Console.ReadLine();
        
          
            int DurationInMinutes = ReadInt("Enter a Movie Duration in minutes: ");
           
            Film movie = new Film(id, movieName, year, movieDirector, DurationInMinutes);
            service.AddNewItem(movie);
        }

    
  



    }
}



