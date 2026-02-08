

using System.IO;
using System.Threading.Channels;

public class ConsoleUI
{
    private readonly ParseExecutor _executor;
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


    public async Task MainMenu()
    {
        while (true)
        {
            Console.WriteLine($"Chose one of available options: \n" +
               $"1. View All Items \n " +
               $"2. Add New Movie or Book \n " +
               $"3. Analitics \n " +
               $"4. exit \n");
            // we don't need option save because database is now saving automatically
            string input = Console.ReadLine();
            int option = 0;
            SafeExecutor.Execute(
                () => { option = _executor.SafeParse(input); });
            if (option == 0) { continue; }
            if (option == 4) { break; }
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

            }









        }
    }




    public async Task ConsoleAddObject(BookStoreService service)
    {
        Console.WriteLine("To add Movie or Book, follow next steps: ");


        Console.WriteLine("if you want to add Book enter 1, if you want to add Movie enter 2");
        int objectType = 0;
        while (objectType != 1 && objectType != 2)
        {
            objectType = ReadInt("Your choice: ");
            if (objectType != 1 && objectType != 2) Console.WriteLine("Invalid option!");
        }

        if (objectType == 1)
        {
            Console.WriteLine("Enter Book Name: ");
            var bookName = Console.ReadLine();


            int year = ReadInt("Enter a Year of Release Date: ");

            Console.WriteLine("Enter an Author: ");
            var author = Console.ReadLine();

            int PageNumber = ReadInt("Enter page number: ");


            Knjiga book = new Knjiga
            {
                Naslov = bookName,
                GodinaIzdanja = year,
                Autor = author,
                BrojStranica = PageNumber
            };

            await service.AddNewItem(book);
        }
        if (objectType == 2)
        {
            Console.WriteLine("Enter Movie Name: ");
            var movieName = Console.ReadLine();


            int year = ReadInt("Enter a Year of Release Date: ");

            Console.WriteLine("Enter a Movie Director: ");
            var movieDirector = Console.ReadLine();


            int DurationInMinutes = ReadInt("Enter a Movie Duration in minutes: ");

            Film movie = new Film
            {
                Naslov = movieName,
                GodinaIzdanja = year,
                Reziser = movieDirector,
                TrajanjeUMinutama = DurationInMinutes
            };
            await service.AddNewItem(movie);
        }






    }

    public async Task GenerisiIzvjestaj()
    {
        var biblioteka = await _service.GetReportData();
        const int col1 = 15;
        const int col2 = 20;
        const int col3 = 10;
        const int col4 = 65;



        int sirinaTabele = col1 + col2 + col3 + col4;
        string linija = new string('-', sirinaTabele);
        Console.WriteLine($"\n\n{linija}\n");
        Console.WriteLine($"{"TIP",-col1} | {"NAZIV",-col2} | {"GODINA IZDANJA",-col3} | {"DETALJI",-col4}");
        Console.WriteLine($"\n{linija}");
        foreach (var item in biblioteka)
        {
            string tip = item.GetType().Name;
            string detalji = "";
            if (item is Knjiga k)
            {
                detalji = $"Autor: {k.Autor}, broj stranica: {k.BrojStranica} ";
            }
            else if (item is Film f)
            {
                detalji = $"Reziser: {f.Reziser}, trajanje u minutama: {f.TrajanjeUMinutama} ";
            }
            Console.WriteLine($"{tip,-col1} | {((BibliotekaArtikal)item).Naslov,-col2} | {((BibliotekaArtikal)item).GodinaIzdanja,-col3} | {detalji,-col4}");

        }

        Console.WriteLine($"\n{linija}");
    }

}



