




using System.Xml;

public  class Analitika {
    public List<Senzor> Lista { get; set;  } = new List<Senzor>();
    public List<ITemperature> svi { get; set; } = new List<ITemperature>();

    public void analiza(List<Senzor> lista)
    {
        var prosjek = lista.GroupBy(x => x.grad).Select(m => new { grad = m.Key, Prosjek = m.Average(n => n.Vrijednost) }).ToList();
        prosjek.ForEach(x => Console.WriteLine($"prosjecna vrijednost u gradu {x.grad} iznosi {x.Prosjek}"));
        var max = prosjek.MaxBy(x => x.Prosjek); 
        Console.WriteLine($"Grad sa najlosijom kvalitetom zraka je: {max.grad}");
        var prosjekDrzave = prosjek.Average(x => x.Prosjek);
        var gradoviIznadProsjeka = prosjek.Where(x => x.Prosjek > prosjekDrzave).ToList();
        gradoviIznadProsjeka.ForEach(x => Console.WriteLine($"kriticni grad: {x.grad}")); 
    }

    public void GenerisiIzvjestaj(List<ITemperature> svi)
    {
        const int col1 = 12;
        const int col2 = 30;
        const int col3 = 40;
        const int col4 = 45;
        int sirinaTabele = col1 + col2 + col3 + col4;
        string linija = new string('-', sirinaTabele);
        Console.WriteLine(linija);
        Console.WriteLine($"\n{"TIP", -col1} | {"NAZIV", -col2} | {"STATUS", -col3} | {"DETALJI", -col4}\n\n{linija}");
        foreach (var item in svi)
        {
            var tip = item.GetType();
            string detalji = ""; 

            if(item is Senzor s)
            {
                detalji = $"Grad: {s.grad}, vrijednost {s.Vrijednost}"; 
            }else if(item is Kontroler k)
            {
                detalji = $"Kanala: {k.BrojKanala}, model: {k.ModelKontrolera}";
            }

            Console.WriteLine($"{tip, -col1} | {((Uredjaj)item).Name, -col2} | {item.GetStatus(), -col3} | {detalji,-col4}");
        }
        Console.WriteLine(linija);
    }

}





