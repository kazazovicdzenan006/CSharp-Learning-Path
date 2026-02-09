


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Text;

public class SenzorService
{
    private readonly SenzorsContext _context;
    private IQueryable<Senzor> senzors => _context.Devices.OfType<Senzor>();
    private IQueryable<Kontroler> controllers => _context.Devices.OfType<Kontroler>();
 
    
    public SenzorService(SenzorsContext context)
    {
        _context = context;
    }


    public async Task<string> Analitics()
    {
        var sb = new StringBuilder(); 
       

        var prosjek = await senzors.GroupBy(x => x.grad).Select(m => new { grad = m.Key, Prosjek = m.Average(n => (double?)n.Vrijednost ?? 0.0) }).ToListAsync();
        prosjek.ForEach(x => sb.AppendLine($"prosjecna vrijednost u gradu {x.grad} iznosi {x.Prosjek}"));
        var max = prosjek.MaxBy(x => x.Prosjek);
        sb.AppendLine($"Grad sa najlosijom kvalitetom zraka je: {max.grad}");
        var prosjekDrzave = prosjek.Average(x => (double?)x.Prosjek ?? 0.0);
        var gradoviIznadProsjeka = prosjek.Where(x => x.Prosjek > prosjekDrzave).ToList();
        gradoviIznadProsjeka.ForEach(x => sb.AppendLine($"kriticni grad: {x.grad}"));

        return sb.ToString();
}

    public async Task<List<Uredjaj>> GetReportData()
    {
        var allData = await _context.Devices.AsNoTracking().ToListAsync();
        return allData; 
    }


 


    public async Task<bool> Exists(int id)
    {
        return await _context.Devices.AnyAsync(x => x.Id == id);
    }

    public async Task AddDevice(Uredjaj obj)
    {
        await _context.Devices.AddAsync(obj);
        await _context.SaveChangesAsync();
    }



}




