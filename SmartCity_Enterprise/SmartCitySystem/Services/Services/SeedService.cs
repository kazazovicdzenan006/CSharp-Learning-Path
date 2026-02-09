using Domain.Models;
using Data;

public class SeedService
{
    private readonly IUnitOfWork _unitOfWork;

    public SeedService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SeedEverythingAsync()
    {
        var postojeciGradovi = await _unitOfWork.Gradovi.GetAllAsync();
        if (postojeciGradovi.Any()) return;

        
        var gradovi = new Dictionary<string, Grad>
        {
            { "Sarajevo", new Grad { Name = "Sarajevo" } },
            { "Mostar", new Grad { Name = "Mostar" } },
            { "Banja Luka", new Grad { Name = "Banja Luka" } },
            { "Tuzla", new Grad { Name = "Tuzla" } }
        };

        
        SafeExecutor.Execute(() => {
            gradovi["Sarajevo"].CityNodes.Add(new ParkingLot { ParkingName = "BBI", StreetName = "Titova", CityZone = "Centar", TotalParkingSpots = 500, AvailableParkingSpots = 150 });
            gradovi["Mostar"].CityNodes.Add(new ParkingLot { ParkingName = "Mepas", StreetName = "Kardinala Stepinca", CityZone = "Rondo", TotalParkingSpots = 300, AvailableParkingSpots = 20 });
            gradovi["Banja Luka"].CityNodes.Add(new CrossRoad { CrossName = "Malta", StreetName = "Krajških brigada", CityZone = "Borik", TrafficJamPercantage = 65.0 });
            gradovi["Tuzla"].CityNodes.Add(new CrossRoad { CrossName = "Skver", StreetName = "Džafer mahala", CityZone = "Centar", TrafficJamPercantage = 40.2 });
        });

        
        SafeExecutor.Execute(() => {
            gradovi["Sarajevo"].Devices.Add(new Senzor { Name = "AirQuality_Centar", Vrijednost = 120.5 });
            gradovi["Mostar"].Devices.Add(new Senzor { Name = "Neretva_Temp", Vrijednost = 14.2 });
            gradovi["Banja Luka"].Devices.Add(new Kontroler { Name = "StreetLight_Main", ModelKontrolera = "Siemens", Status = true, BrojKanala = 8 });
            gradovi["Tuzla"].Devices.Add(new Kontroler { Name = "Heating_Station", ModelKontrolera = "Honeywell", Status = true, BrojKanala = 4 });
        });

        SafeExecutor.Execute(() => {
            gradovi["Sarajevo"].BookStoreInventory.Add(new Knjiga { Naslov = "Tvrđava", Autor = "Meša Selimović", GodinaIzdanja = 1970, BrojStranica = 450 });
            gradovi["Mostar"].BookStoreInventory.Add(new Knjiga { Naslov = "Na Drini Ćuprija", Autor = "Ivo Andrić", GodinaIzdanja = 1945, BrojStranica = 350 });
            gradovi["Banja Luka"].BookStoreInventory.Add(new Film { Naslov = "Inception", Reziser = "Nolan", GodinaIzdanja = 2010, TrajanjeUMinutama = 148 });
            gradovi["Tuzla"].BookStoreInventory.Add(new Film { Naslov = "The Godfather", Reziser = "Coppola", GodinaIzdanja = 1972, TrajanjeUMinutama = 175 });
        });

       
        SafeExecutor.Execute(() => {
         
            gradovi["Sarajevo"].Devices.Add(new Kontroler { Name = "Gate_Security", ModelKontrolera = "Arduino", Status = false, BrojKanala = 2 });
            gradovi["Mostar"].CityNodes.Add(new CrossRoad { CrossName = "Španjolski trg", StreetName = "Bulevar", CityZone = "Centar", TrafficJamPercantage = 92.0 });
            gradovi["Banja Luka"].BookStoreInventory.Add(new Knjiga { Naslov = "Zločin i kazna", Autor = "Dostojevski", GodinaIzdanja = 1866, BrojStranica = 600 });
            gradovi["Tuzla"].Devices.Add(new Senzor { Name = "Noise_Level_Skver", Vrijednost = 75.8 });
        });

     
        foreach (var grad in gradovi.Values)
        {
            await _unitOfWork.Gradovi.AddAsync(grad);
        }

        await _unitOfWork.CompleteAsync();
    }
}