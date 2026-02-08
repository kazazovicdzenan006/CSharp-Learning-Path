

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

public class BookStoreService
{
    //private readonly List<BibliotekaArtikal> _biblioteka;
    //private readonly StorageManager<BibliotekaArtikal> _manager;
    //private List<Knjiga> Knjige => _biblioteka.OfType<Knjiga>().ToList();
    //private List<Film> Filmovi => _biblioteka.OfType<Film>().ToList();

    private readonly BookStoreContext _context;
    private IQueryable<Knjiga> Knjige => _context.StoreItems.OfType<Knjiga>();
    private IQueryable<Film> Filmovi => _context.StoreItems.OfType<Film>();
    // Using IQueryable for minimal data transfer and Deferred Execution
    //Data filtering is done on the server and we only get the finished product (only data that we need instead all of the data) 
    // so we improving performance plus IQueryable is waiting for ToListAsync or similar methods to send data (Deferred Execution)
    public BookStoreService(BookStoreContext context /*List<BibliotekaArtikal> artikli, StorageManager<BibliotekaArtikal> manager*/)
    {
        // _biblioteka = artikli;
        //  _manager = manager;

        _context = context;

    }

    // we don't need method SaveCurrentState because we have now database and Method AddNewItem do all hardwork


    public async Task<string> Analiza()
    {

        var sb = new StringBuilder(); // we use StringBuilder instead of writer (delegate) so we can get maximum layer independency
                                      // and also full clean architecture (Separation of Concerns)

        sb.AppendLine("\n \nBooks Grouped by Author: \n");
        var poAutoru = await Knjige.GroupBy(x => x.Autor).Select(grupa => new { imeAutora = grupa.Key, prosjekStranica = grupa.Average(k => (double?)k.BrojStranica) ?? 0.0, brojKnjiga = grupa.Count() }).ToListAsync();

        poAutoru.ForEach(x => sb.AppendLine($"autor: {x.imeAutora}, Prosjek stranica: {x.prosjekStranica}, Ukupno knjiga: {x.brojKnjiga}"));

        sb.AppendLine("\nBooks with more than 300 pages and name that starts with letter s: \n");
        var filter = await Knjige.Where(x => x.BrojStranica > 300 && x.Naslov.StartsWith("s")).ToListAsync(); // now we don't have to use ToLower
                                                                                                              // because we are working with database and sql is case insensitive 
        filter.ForEach(x => sb.AppendLine($"Knjiga: {x.Naslov}, godinaIzdanja: {x.GodinaIzdanja}, Autor {x.Autor}, broj Stranica {x.BrojStranica}"));

        sb.AppendLine("\nMovies grouped by movie director: \n");
        var poReziseru = await Filmovi
     .GroupBy(x => x.Reziser)
     .Select(group => new {
         reziser = group.Key,
         maksTrajanje = group.Max(f => (int?)f.TrajanjeUMinutama) ?? 0,
         // (int?) casting int to nullable value and if it is null it will return 0 (?? 0)
         brojFilmova = group.Count()
     }).ToListAsync();
        poReziseru.ForEach(x => sb.AppendLine($"Reziser: {x.reziser}, najduze trajanje filma {x.maksTrajanje}, broj filmova {x.brojFilmova}"));

        return sb.ToString();

    }

    public async Task<List<BibliotekaArtikal>> GetReportData()
    {
        var AllData = await _context.StoreItems.AsNoTracking().ToListAsync();
        // if we will only read data, we use .AsNoTracking() so we don't waste resources for monitoring data
        return AllData;
    }



    public async Task<List<Knjiga>> FiltrirajKnjige(Expression<Func<Knjiga, bool>> uslov)
    {
        return await Knjige.Where(uslov).ToListAsync();
        //instead of foreach loop because foreach load all data from the database, this way server do the hardwork, we just say what we need
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.StoreItems.AnyAsync(x => x.Id == id); // we need == to  check if it exists

    }

    public async Task AddNewItem(BibliotekaArtikal item)
    {
        await _context.StoreItems.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task<string> GetDostupnost(string FilmName)
    {
        // radi i klasicna opcija ali .ToLower kreira novi privremeni string koji GC mora ocistiti, dok StringComparison radi u hodu, ne kreira nove stringove vec odmah provjerava karakter po karakter
        bool postoji = await Filmovi.AsNoTracking().AnyAsync(x => x.Naslov == FilmName);
        // bool postoji = _biblioteka.Any(x => x is Film && ((Film)x).Naslov.ToLower() == FilmName.ToLower());

        return postoji ? "We have that movie in our collection" : "Sorry we don't have that movie in our collection";
    }


}