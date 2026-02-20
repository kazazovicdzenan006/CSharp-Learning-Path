

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using Domain.Models;
using Services.DTOs.BookStoreAnalizeDtos;
using System.ComponentModel;
using Services.DTOs.BooksDtos;

public class BookStoreService
{
    //private readonly List<BibliotekaArtikal> _biblioteka;
    //private readonly StorageManager<BibliotekaArtikal> _manager;
    //private List<Knjiga> Knjige => _biblioteka.OfType<Knjiga>().ToList();
    //private List<Film> Filmovi => _biblioteka.OfType<Film>().ToList();

    private readonly IUnitOfWork _unit;
    private IQueryable<Knjiga> Knjige => _unit.Artikli.GetQueryable().OfType<Knjiga>();
    private IQueryable<Film> Filmovi => _unit.Artikli.GetQueryable().OfType<Film>();
    // Using IQueryable for minimal data transfer and Deferred Execution
    //Data filtering is done on the server and we only get the finished product (only data that we need instead all of the data) 
    // so we improving performance plus IQueryable is waiting for ToListAsync or similar methods to send data (Deferred Execution)
    public BookStoreService(IUnitOfWork unit)
    {

        _unit = unit; 

    }

    // we don't need method SaveCurrentState because we have now database and Method AddNewItem do all hardwork

    public async Task<List<GroupedByAuthorDto>> GroupedByAuthor()
    {
        return await Knjige
        .GroupBy(x => x.Autor)
        .Select(grupa => new GroupedByAuthorDto()
        {
            imeAutora = grupa.Key,
            prosjekStranica = grupa.Average(k => (double?)k.BrojStranica) ?? 0.0,
            brojKnjiga = grupa.Count()
        }).ToListAsync();

    }

    public async Task<IEnumerable<Knjiga>> GetBooks()
    {
        return await _unit.Knjige.GetAllAsync();
    }
    public async Task<IEnumerable<Film>> GetMovies()
    {
        return await _unit.Filmovi.GetAllAsync();
    }


    public async Task<IEnumerable<Knjiga>> LongBooks()
    {
      var data = await Knjige
            .Where(x => x.BrojStranica > 300).ToListAsync();
   
        return data;
    }

    public async Task<List<MoviesByDirectorDto>> GroupedByDirector()
    {
        return await Filmovi
            .GroupBy(x => x.Reziser)
            .Select(group => new MoviesByDirectorDto()
            {
                reziser = group.Key,
                maksTrajanje = group.Max(f => (int?)f.TrajanjeUMinutama) ?? 00,
                brojFilmova = group.Count()
            })
            .ToListAsync();
    }
   


    public async Task UpdateArtikal(int id, BibliotekaArtikal updatedData)
    {
       
        var existingArtikal = await _unit.Artikli.GetByIdAsync(id);

        if (existingArtikal == null) throw new Exception("Artikal nije pronađen!");


        existingArtikal.Naslov = updatedData.Naslov;
        existingArtikal.GodinaIzdanja = updatedData.GodinaIzdanja;
        //existingArtikal.GradId = updatedData.GradId;


        if (existingArtikal is Knjiga eKnjiga && updatedData is Knjiga nKnjiga)
        {
            eKnjiga.Autor = nKnjiga.Autor;
            eKnjiga.BrojStranica = nKnjiga.BrojStranica;
        }
        else if (existingArtikal is Film eFilm && updatedData is Film nFilm)
        {
            eFilm.Reziser = nFilm.Reziser;
            eFilm.TrajanjeUMinutama = nFilm.TrajanjeUMinutama;
        }

        await _unit.CompleteAsync();
    }
    public async Task<BibliotekaArtikal> GetArtikalById(int id)
    {
        return await _unit.Artikli.GetByIdAsync(id);
    }
  



    public async Task DeleteArtikal(int id)
    {
        var Artikal = await _unit.Artikli.GetByIdAsync(id);

        if (Artikal != null)
        {
            _unit.Artikli.Delete(Artikal);
            await _unit.CompleteAsync();
        }
        else throw new Exception("There is no item with that id");

    }


    public async Task<List<Grad>> GetAvailableCities()
    {
        return (await _unit.Gradovi.GetAllAsync()).ToList();
    }

    public async Task<IEnumerable<BibliotekaArtikal>> GetReportData()
    {
        var AllData = await _unit.Artikli
            .GetQueryable()
            .Include(x => x.Grad)
            .ToListAsync();
        // if we will only read data, we use .AsNoTracking() so we don't waste resources for monitoring data
        return AllData;
    }



    public async Task<List<Knjiga>> FiltrirajKnjige(Expression<Func<Knjiga, bool>> uslov)
    {
        return await Knjige.Where(uslov).ToListAsync();
        //instead of foreach loop because foreach load all data from the database, this way server do the hardwork, we just say what we need
    }


    public async Task AddNewItem(BibliotekaArtikal item)
    {
        await _unit.Artikli.AddAsync(item);
        await _unit.CompleteAsync();
    }
    public async Task AddNewBook(Knjiga item)
    {
        await _unit.Knjige.AddAsync(item);
        await _unit.CompleteAsync();
    }

    public async Task AddNewFilm(Film item)
    {
        await _unit.Filmovi.AddAsync(item);
        await _unit.CompleteAsync();
    }

    public async Task<bool> GetDostupnost(string FilmName)
    {
        // radi i klasicna opcija ali .ToLower kreira novi privremeni string koji GC mora ocistiti, dok StringComparison radi u hodu, ne kreira nove stringove vec odmah provjerava karakter po karakter
        bool postoji = await Filmovi.AsNoTracking().AnyAsync(x => x.Naslov == FilmName);
        // bool postoji = _biblioteka.Any(x => x is Film && ((Film)x).Naslov.ToLower() == FilmName.ToLower());

        return postoji;
    }

    public async Task<List<Grad>> GetAllCitiesAsync()
    {
        return (await _unit.Gradovi.GetAllAsync()).ToList();
    }

    public async Task<Grad> GetFirstCityAsync()
    {
        var cities = await _unit.Gradovi.GetAllAsync();
        return cities.FirstOrDefault();
    }


}