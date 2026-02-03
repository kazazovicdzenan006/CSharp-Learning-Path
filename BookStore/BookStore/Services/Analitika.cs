


public class Analitika
{
    public void Analiza(List<Knjiga> knjige)
    {
        Console.WriteLine("\n \n");
        var poAutoru = knjige.GroupBy(x => x.Autor).Select(grupa => new { imeAutora = grupa.Key, prosjekStranica = grupa.Average(k => k.BrojStranica), brojKnjiga = grupa.Count()}).ToList() ;
   
        poAutoru.ForEach(x => Console.WriteLine($"autor: {x.imeAutora}, Prosjek stranica: {x.prosjekStranica}, Ukupno knjiga: {x.brojKnjiga}"));

       
        
        Console.WriteLine("\n");
        var filter = knjige.Where(x => x.BrojStranica > 300 && x.Naslov.ToLower().StartsWith("s")).ToList();
        filter.ForEach(x => Console.WriteLine($"Knjiga: {x.Naslov}, godinaIzdanja: {x.GodinaIzdanja}, Autor {x.Autor}, broj Stranica {x.BrojStranica}"));
    }




    public void GenerisiIzvjestaj(List<IPozajmica> lista)
    {
        const int col1 = 15;
        const int col2 = 20;
        const int col3 = 10;
        const int col4 = 65;



        int sirinaTabele = col1 + col2 + col3 + col4;
        string linija = new string('-', sirinaTabele);
        Console.WriteLine($"\n\n{linija}\n");
        Console.WriteLine($"{"TIP", -col1} | {"NAZIV", -col2} | {"GODINA IZDANJA", -col3} | {"DETALJI", -col4}");
        Console.WriteLine($"\n{linija}");
        foreach (var item in lista)
        {
            string tip = item.GetType().Name;
            string detalji = "";
            if(item is Knjiga k)
            {
                detalji = $"Autor: {k.Autor}, broj stranica: {k.BrojStranica} "; 
            }
            else if (item is Film f)
            {
                detalji = $"Reziser: {f.Reziser}, trajanje u minutama: {f.TrajanjeUMinutama} ";
            }
            Console.WriteLine($"{tip, -col1} | {((BibliotekaArtikal)item).Naslov, -col2} | {((BibliotekaArtikal)item).GodinaIzdanja,-col3} | {detalji, -col4}");
            
        }

        Console.WriteLine($"\n{linija}");
    }


    public List<Knjiga> FiltrirajKnjige(List<Knjiga> lista, Func<Knjiga, bool> uslov)
    {
        List<Knjiga> rezultat = new List<Knjiga>();

        foreach (var k in lista)
        {
            if (uslov(k))
            {
                rezultat.Add(k);
            }
        }

        return rezultat; 

    }
        
    




}




