

public class BookStoreService
{
    private readonly List<BibliotekaArtikal> _biblioteka;
    private readonly StorageManager<BibliotekaArtikal> _manager;
    private List<Knjiga> Knjige => _biblioteka.OfType<Knjiga>().ToList();
    private List<Film> Filmovi => _biblioteka.OfType<Film>().ToList();


    public BookStoreService(List<BibliotekaArtikal> artikli, StorageManager<BibliotekaArtikal> manager)
    {
        _biblioteka = artikli;
        _manager = manager;
       
    }

    public async Task SaveCurrentState()
    {
        await _manager.SpasiPodatke("Data.json", _biblioteka);
    }


    public async Task<List<BibliotekaArtikal>> LoadCurrentState()
    {
        var loadedData = await _manager.UcitajPodatke("Data.json");
        _biblioteka.Clear();
        _biblioteka.AddRange(loadedData); 
        return _biblioteka;
    }


    public void Analiza(Action<string> writer)
    {
     
                writer("\n \n");
                var poAutoru = Knjige.GroupBy(x => x.Autor).Select(grupa => new { imeAutora = grupa.Key, prosjekStranica = grupa.Average(k => k.BrojStranica), brojKnjiga = grupa.Count() }).ToList();
 
                poAutoru.ForEach(x => writer($"autor: {x.imeAutora}, Prosjek stranica: {x.prosjekStranica}, Ukupno knjiga: {x.brojKnjiga}"));

                writer("\n");
                var filter = Knjige.Where(x => x.BrojStranica > 300 && x.Naslov.ToLower().StartsWith("s")).ToList();
                filter.ForEach(x => writer($"Knjiga: {x.Naslov}, godinaIzdanja: {x.GodinaIzdanja}, Autor {x.Autor}, broj Stranica {x.BrojStranica}"));
  

     
    }



    public void GenerisiIzvjestaj(Action<string> formatizer)
    {
        const int col1 = 15;
        const int col2 = 20;
        const int col3 = 10;
        const int col4 = 65;



        int sirinaTabele = col1 + col2 + col3 + col4;
        string linija = new string('-', sirinaTabele);
        formatizer($"\n\n{linija}\n");
        formatizer($"{"TIP",-col1} | {"NAZIV",-col2} | {"GODINA IZDANJA",-col3} | {"DETALJI",-col4}");
        formatizer($"\n{linija}");
        foreach (var item in _biblioteka)
        {
            string tip = item.GetType().Name;
            string detalji = "";
            if (item is Knjiga k)
            {
                detalji = $"Autor: {k.Autor}, broj stranica: {k.BrojStranica} ";
            }
            else if (item is Film f)
            {
                detalji = $"Reziser: {f.Reziser}, trajanje u minutama: {f.TrajanjeUMinutama} ";
            }
            formatizer($"{tip,-col1} | {((BibliotekaArtikal)item).Naslov,-col2} | {((BibliotekaArtikal)item).GodinaIzdanja,-col3} | {detalji,-col4}");

        }

        formatizer($"\n{linija}");
    }

    public List<Knjiga> FiltrirajKnjige(Func<Knjiga, bool> uslov)
    {
        List<Knjiga> rezultat = new List<Knjiga>();

        foreach (var item in _biblioteka)
        {
            if (item is Knjiga k)
            {
                if (uslov(k))
                {
                    rezultat.Add(k);
                }
            }
        }

        return rezultat;

    }






}