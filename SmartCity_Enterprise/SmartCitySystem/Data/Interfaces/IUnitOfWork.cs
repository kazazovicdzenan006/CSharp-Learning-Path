using Domain.Models;

public interface IUnitOfWork : IDisposable
{

    IRepository<Grad> Gradovi { get; }
    IRepository<BibliotekaArtikal> Artikli { get; }
    IRepository<Uredjaj> Uredjaji { get; }
    IRepository<CityNode> CityNodes { get; }


    IRepository<Knjiga> Knjige { get; }
    IRepository<ParkingLot> Parkinzi { get; }

    Task<int> CompleteAsync();
}