


public class SenzorService
{
    private readonly List<Uredjaj> _allDevices;
    private readonly StorageManager<Uredjaj> _manager;
    
    public SenzorService(List<Uredjaj> devices, StorageManager<Uredjaj> manager)
    {
        _allDevices = devices;
        _manager = manager; 
    }

    public async Task SaveCurrentState()
    {
        await _manager.spasiPodatke("Data.json", _allDevices); 
    }


    public async Task<List<Uredjaj>> LoadCurrentState()
    {
        var loaded = await _manager.UcitajPodatke("Data.json");
        _allDevices.Clear();
        _allDevices.AddRange(loaded);
        return _allDevices; 
    } 

    public void Analitics(Action<string> writer)
    {
        var senzors = _allDevices.OfType<Senzor>().ToList();
        var controllers = _allDevices.OfType<Kontroler>().ToList();

        var prosjek = senzors.GroupBy(x => x.grad).Select(m => new { grad = m.Key, Prosjek = m.Average(n => n.Vrijednost) }).ToList();
        prosjek.ForEach(x => writer($"prosjecna vrijednost u gradu {x.grad} iznosi {x.Prosjek}"));
        var max = prosjek.MaxBy(x => x.Prosjek);
        writer($"Grad sa najlosijom kvalitetom zraka je: {max.grad}");
        var prosjekDrzave = prosjek.Average(x => x.Prosjek);
        var gradoviIznadProsjeka = prosjek.Where(x => x.Prosjek > prosjekDrzave).ToList();
        gradoviIznadProsjeka.ForEach(x => writer($"kriticni grad: {x.grad}"));

}

    public void GenerisiIzvjestaj(Action<string> formatizer)
    {
        const int col1 = 12;
        const int col2 = 30;
        const int col3 = 40;
        const int col4 = 45;
        int sirinaTabele = col1 + col2 + col3 + col4;
        string linija = new string('-', sirinaTabele);
        formatizer(linija);
        formatizer($"\n{"TIP",-col1} | {"NAZIV",-col2} | {"STATUS",-col3} | {"DETALJI",-col4}\n\n{linija}");
        foreach (var item in _allDevices)
        {
            var tip = item.GetType();
            string detalji = "";
            var status = (item is ITemperature t)? t.GetStatus() : "N/A";  // because I can't access to GetStatus directly from _allData except if i implement interface in base class
            if (item is Senzor s)
            {
                detalji = $"Grad: {s.grad}, vrijednost {s.Vrijednost}";
            }
            else if (item is Kontroler k)
            {
                detalji = $"Kanala: {k.BrojKanala}, model: {k.ModelKontrolera}";
            }

                formatizer($"{tip,-col1} | {((Uredjaj)item).Name,-col2} | {status,-col3} | {detalji,-col4}");
        }
        formatizer(linija);
    }


    public bool Exists(int id)
    {
        return _allDevices.Any(x => x.Id == id);
    }

    public void AddDevice(Uredjaj obj)
    {
        _allDevices.Add(obj);
    }



}




