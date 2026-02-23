


public class BookStoreService : BaseService
{

    private readonly IUnitOfWork _unit;
    private readonly IMapper _mapper;
    private IQueryable<Knjiga> Knjige => _unit.Artikli.GetQueryable().OfType<Knjiga>();
    private IQueryable<Film> Filmovi => _unit.Artikli.GetQueryable().OfType<Film>();
    // Using IQueryable for minimal data transfer and Deferred Execution
    //Data filtering is done on the server and we only get the finished product (only data that we need instead all of the data) 
    // so we improving performance plus IQueryable is waiting for ToListAsync or similar methods to send data (Deferred Execution)
    public BookStoreService(IUnitOfWork unit, IMapper mapper, IServiceProvider provider) : base(provider)   
    {

        _unit = unit; 
        _mapper = mapper;

    }



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

    public async Task<IEnumerable<BooksReadDto>> GetBooks()
    {
        var data = await _unit.Knjige.GetAllAsync();
        var dto = _mapper.Map<IEnumerable<BooksReadDto>>(data);
        return dto;
    }
    public async Task<IEnumerable<FilmReadDto>> GetMovies()
    {
        var data = await _unit.Filmovi.GetAllAsync();
        var dto = _mapper.Map<IEnumerable<FilmReadDto>>(data);
        return dto;
    }


    public async Task<IEnumerable<BooksReadDto>> LongBooks()
    {
      var data = await Knjige
            .Where(x => x.BrojStranica > 300).ToListAsync();
        var dto = _mapper.Map<IEnumerable<BooksReadDto>>(data); 
        return dto;
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


    public async Task UpdateBook(int id, BooksUpdateDto dto)
    {
        await ValidateAsync(dto);

        var existing = await _unit.Artikli.GetByIdAsync(id) as Knjiga;

        if (existing == null) throw new Exception("Knjiga nije pronađena!");

        _mapper.Map(dto, existing);

        await _unit.CompleteAsync();
    }


    public async Task UpdateFilm(int id, FilmUpdateDto dto)
    {
        await ValidateAsync(dto);
        var existing = await _unit.Artikli.GetByIdAsync(id) as Film;

        if (existing == null) throw new Exception("Film nije pronađen!");

        _mapper.Map(dto, existing);

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

    public async Task<IEnumerable<BookStoreItemsReadDto>> GetReportData()
    {
        var AllData = await _unit.Artikli
            .GetQueryable()
            .Include(x => x.Grad)
            .ToListAsync();
        var dto = _mapper.Map<IEnumerable<BookStoreItemsReadDto>>(AllData); 
        // if we will only read data, we use .AsNoTracking() so we don't waste resources for monitoring data
        return dto;
    }



    public async Task<List<Knjiga>> FiltrirajKnjige(Expression<Func<Knjiga, bool>> uslov)
    {
        return await Knjige.Where(uslov).ToListAsync();
        //instead of foreach loop because foreach load all data from the database, this way server do the hardwork, we just say what we need
    }



    public async Task<BooksReadDto> AddNewBook(BooksCreateDto dto)
    {
        await ValidateAsync(dto);
        var item = _mapper.Map<Knjiga>(dto);
        await _unit.Knjige.AddAsync(item);
        await _unit.CompleteAsync();
        return _mapper.Map<BooksReadDto>(item);
    }

    public async Task<FilmReadDto> AddNewFilm(FilmCreateDto dto)
    {
        await ValidateAsync(dto);
        var item = _mapper.Map<Film>(dto);
        await _unit.Filmovi.AddAsync(item);
        await _unit.CompleteAsync();
        return _mapper.Map<FilmReadDto>(item);
    }

    public async Task<bool> GetDostupnost(string FilmName)
    {
        // radi i klasicna opcija ali .ToLower kreira novi privremeni string koji GC mora ocistiti, dok StringComparison radi u hodu, ne kreira nove stringove vec odmah provjerava karakter po karakter
        bool postoji = await Filmovi.AsNoTracking().AnyAsync(x => x.Naslov == FilmName);
        // bool postoji = _biblioteka.Any(x => x is Film && ((Film)x).Naslov.ToLower() == FilmName.ToLower());

        return postoji;
    }

    public async Task<List<GradReadDto>> GetAllCitiesAsync()
    {
        var data = (await _unit.Gradovi.GetAllAsync()).ToList();
        var dto = _mapper.Map<List<GradReadDto>>(data); 
        return dto;
    }

    public async Task<Grad> GetFirstCityAsync()
    {
        var cities = await _unit.Gradovi.GetAllAsync();
        return cities.FirstOrDefault();
    }


}