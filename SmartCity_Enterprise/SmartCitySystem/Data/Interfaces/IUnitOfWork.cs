using Domain.Models;

public interface IUnitOfWork : IDisposable
{

    public IRepository<Grad> Gradovi { get; }
    public IRepository<BibliotekaArtikal> Artikli { get;  }
    public IRepository<Uredjaj> Uredjaji { get;  }
    public IRepository<CityNode> CityNodes { get;  }
    public IRepository<Knjiga> Knjige { get; }
    public IRepository<ParkingLot> Parkinzi { get;  }
    public IRepository<Senzor> Senzori { get;  }
    public IRepository<Kontroler> Kontroleri { get;  }
    public IRepository<Film> Filmovi { get;  }
    public IRepository<CrossRoad> Raskrsnice { get; }

    Task<int> CompleteAsync();
}