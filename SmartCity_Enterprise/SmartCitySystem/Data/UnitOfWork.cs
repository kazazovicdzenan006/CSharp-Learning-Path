using Domain.Models;

public class UnitOfWork : IUnitOfWork
{
    private readonly MasterContext _context;

    public UnitOfWork(MasterContext context)
    {
        _context = context;
        Gradovi = new GenericRepository<Grad>(_context);
        Artikli = new GenericRepository<BibliotekaArtikal>(_context);
        Uredjaji = new GenericRepository<Uredjaj>(_context);
        CityNodes = new GenericRepository<CityNode>(_context);
        Knjige = new GenericRepository<Knjiga>(_context);
        Parkinzi = new GenericRepository<ParkingLot>(_context);
        Senzori = new GenericRepository<Senzor>(_context);
        Kontroleri = new GenericRepository<Kontroler>(_context);
        Filmovi = new GenericRepository<Film>(_context);
        Raskrsnice = new GenericRepository<CrossRoad>(_context);
    }

    public IRepository<Grad> Gradovi { get; private set; }
    public IRepository<BibliotekaArtikal> Artikli { get; private set; }
    public IRepository<Uredjaj> Uredjaji { get; private set; }
    public IRepository<CityNode> CityNodes { get; private set; }
    public IRepository<Knjiga> Knjige { get; private set; }
    public IRepository<ParkingLot> Parkinzi { get; private set; }
    public IRepository<Senzor> Senzori { get; private set; }
    public IRepository<Kontroler> Kontroleri { get; private set; }
    public IRepository<Film> Filmovi { get; private set; }
    public IRepository<CrossRoad> Raskrsnice { get; private set; }

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
    public void Dispose() => _context.Dispose(); // don't wait to GC 
}