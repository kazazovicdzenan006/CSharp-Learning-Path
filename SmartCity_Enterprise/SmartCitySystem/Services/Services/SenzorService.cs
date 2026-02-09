


using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.ComponentModel.DataAnnotations;
using System.Text;

public class SenzorService
{
    private readonly IUnitOfWork _unit;
    private IQueryable<Senzor> senzors => _unit.Uredjaji.GetQueryable().OfType<Senzor>();
    private IQueryable<Kontroler> controllers => _unit.Uredjaji.GetQueryable().OfType<Kontroler>();


    public SenzorService(IUnitOfWork unit)
    {
        _unit = unit; 
    }


    public async Task<string> Analitics()
    {
        var sb = new StringBuilder();


        var prosjek = await senzors.GroupBy(x => x.Grad).Select(m => new { grad = m.Key, Prosjek = m.Average(n => (double?)n.Vrijednost ?? 0.0) }).ToListAsync();
        prosjek.ForEach(x => sb.AppendLine($"prosjecna vrijednost u gradu {x.grad} iznosi {x.Prosjek}"));
        if (prosjek.Any()) 
        { 
            var max = prosjek.MaxBy(x => x.Prosjek);
            sb.AppendLine($"Grad sa najlosijom kvalitetom zraka je: {max.grad}");
        } 
            
        var prosjekDrzave = prosjek.Average(x => (double?)x.Prosjek ?? 0.0);
        var gradoviIznadProsjeka = prosjek.Where(x => x.Prosjek > prosjekDrzave).ToList();
        gradoviIznadProsjeka.ForEach(x => sb.AppendLine($"kriticni grad: {x.grad}"));

        return sb.ToString();
    }

    public async Task<List<Uredjaj>> GetReportData()
    {
        var allData = await _unit.Uredjaji.GetAllAsync();
        return allData.ToList();
    }


    public async Task<List<Grad>> GetAvailableCities()
    {
        return (await _unit.Gradovi.GetAllAsync()).ToList();
    }


    public async Task AddDevice(Uredjaj obj)
    {
        await _unit.Uredjaji.AddAsync(obj);
        await _unit.CompleteAsync();
    }
    public async Task UpdateDevice(int id, Uredjaj updatedData)
    {
       
        var existingDevice = await _unit.Uredjaji.GetByIdAsync(id);

        if (existingDevice == null) throw new Exception("Device not found!");

     
        existingDevice.Name = updatedData.Name;
        existingDevice.GradId = updatedData.GradId;

        if (existingDevice is Senzor existingSenzor && updatedData is Senzor newSenzor)
        {
            existingSenzor.Vrijednost = newSenzor.Vrijednost;
        }
        else if (existingDevice is Kontroler existingKontroler && updatedData is Kontroler newKontroler)
        {
            existingKontroler.ModelKontrolera = newKontroler.ModelKontrolera;
            existingKontroler.Status = newKontroler.Status;
            existingKontroler.BrojKanala = newKontroler.BrojKanala;
        }

        await _unit.CompleteAsync();
    }
    public async Task<Uredjaj> GetDeviceById(int id)
    {
        return await _unit.Uredjaji.GetByIdAsync(id);
    }
    public async Task DeleteDevice(int id)
    {
        var device = await _unit.Uredjaji.GetByIdAsync(id);

        if(device != null)
        {
            _unit.Uredjaji.Delete(device);
            await _unit.CompleteAsync();
        }

    }


}




