using Microsoft.EntityFrameworkCore;

public class SenzorService
{
    private readonly IUnitOfWork _unit;
    private IQueryable<Senzor> senzors => _unit.Uredjaji.GetQueryable().OfType<Senzor>();
    private IQueryable<Kontroler> controllers => _unit.Uredjaji.GetQueryable().OfType<Kontroler>();

    public SenzorService(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<object> GetAnalytics()
    {
        var prosjek = await senzors
            .Include(x => x.Grad)
            .GroupBy(x => x.Grad.Name)
            .Select(m => new {
                Grad = m.Key,
                Prosjek = m.Average(n => (double?)n.Vrijednost ?? 0.0)
            }).ToListAsync();

        if (!prosjek.Any()) return null;

        var prosjekDrzave = prosjek.Average(x => x.Prosjek);
        var maxKritican = prosjek.MaxBy(x => x.Prosjek);

        return new
        {
            GradoviProsjeci = prosjek,
            DrzavniProsjek = prosjekDrzave,
            NajgoriGrad = maxKritican,
            KriticniGradovi = prosjek.Where(x => x.Prosjek > prosjekDrzave).ToList()
        };
    }

    public async Task<IEnumerable<Uredjaj>> GetAllDevices()
    {
        return await _unit.Uredjaji
            .GetQueryable()
            .Include(x => x.Grad)
            .ToListAsync();
    }

    public async Task AddSenzor(Senzor senzor)
    {
        await _unit.Uredjaji.AddAsync(senzor);
        var res = await _unit.CompleteAsync();
        if (res <= 0) throw new Exception("Failed to add sensor");
    }

    public async Task UpdateSenzor(int id, Senzor updated)
    {
        var existing = await _unit.Uredjaji.GetByIdAsync(id) as Senzor;
        if (existing == null) throw new Exception("Sensor not found");



        existing.Vrijednost = updated.Vrijednost;

        await _unit.CompleteAsync();
    }

    public async Task AddKontroler(Kontroler kontroler)
    {
        await _unit.Uredjaji.AddAsync(kontroler);
        var res = await _unit.CompleteAsync();
        if (res <= 0) throw new Exception("Failed to add controller");
    }
    public async Task UpdateKontroler(int id, Kontroler updated)
    {
        var existing = await _unit.Uredjaji.GetByIdAsync(id) as Kontroler;

      if (existing == null) throw new Exception("Controller not found");

        existing.Status = updated.Status;
        existing.BrojKanala = updated.BrojKanala;

        await _unit.CompleteAsync();
    }

    public async Task DeleteDevice(int id)
    {
        var device = await _unit.Uredjaji.GetByIdAsync(id);
        if (device == null) throw new Exception("Device not found");

        _unit.Uredjaji.Delete(device);
        await _unit.CompleteAsync();
    }
}