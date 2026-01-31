

LibraryLimitException.OnLimitReached += (vrijeme, poruka) =>
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"[LOG - {vrijeme: hh,mm,ss}]: {poruka}");
    Console.ResetColor();
};

LibraryLimitException.OnLimitReached += (vrijeme, poruka) => {

    File.AppendAllText("log.txt", poruka);

};




// Lista od 8 knjiga
List<Knjiga> knjige = new List<Knjiga>(); /*
{
    new Knjiga(1, "Na Drini Ćuprija", 1945, "Ivo Andrić", 350),
    new Knjiga(2, "Stranac", 1942, "Albert Camus", 120),
    new Knjiga(3, "Siddhartha", 1922, "Hermann Hesse", 150),
    new Knjiga(4, "Stope u pijesku", 1990, "Nepoznat Autor", 410),
    new Knjiga(5, "Zločin i kazna", 1866, "Fjodor Dostojevski", 600),
    new Knjiga(6, "Sarajevski Marlboro", 1994, "Miljenko Jergović", 180),
    new Knjiga(7, "Sila prirode", 2010, "Jane Harper", 320),
    new Knjiga(8, "Tvrđava", 1970, "Meša Selimović", 450)
};   */

// Lista od 8 filmova
List<Film> filmovi = new List<Film>(); /*
{
    new Film(101, "Inception", 2010, "Christopher Nolan", 148),
    new Film(102, "The Godfather", 1972, "Francis Ford Coppola", 175),
    new Film(103, "Parasite", 2019, "Bong Joon-ho", 132),
    new Film(104, "Interstellar", 2014, "Christopher Nolan", 169),
    new Film(105, "Pulp Fiction", 1994, "Quentin Tarantino", 154),
    new Film(106, "The Matrix", 1999, "Lana Wachowski", 136),
    new Film(107, "Seven", 1995, "David Fincher", 127),
    new Film(108, "Gladiator", 2000, "Ridley Scott", 155)
};  
 */

List<IPozajmica> sviArtikli = new List<IPozajmica>();



// test da li su svi objekti u listi: 
//sviArtikli.ForEach(x => Console.WriteLine(x.GetType()));


StorageManager<Knjiga> knjiznica = new StorageManager<Knjiga>();
//await knjiznica.SpasiPodatke("knjige.json", knjige);
knjige = await knjiznica.UcitajPodatke("knjige.json");
knjige.ForEach(x => Console.WriteLine($"Knjiga: {x.Naslov}, godinaIzdanja: {x.GodinaIzdanja}, Autor {x.Autor}, broj Stranica {x.BrojStranica}"));

Console.WriteLine("\n \n");
StorageManager<Film> filmoteka = new StorageManager<Film>();
//await filmoteka.SpasiPodatke("filmovi.json", filmovi);
filmovi = await filmoteka.UcitajPodatke("filmovi.json");
filmovi.ForEach(x => Console.WriteLine($"Film {x.Naslov}, Godina izdanja {x.GodinaIzdanja}, Reziser {x.Reziser}, Trajanje u minutama {x.TrajanjeUMinutama}"));
sviArtikli.AddRange(knjige);
sviArtikli.AddRange(filmovi);

/*try
{
    Knjiga knjiga = new Knjiga(5, "Zločin i kazna", 1866, "Fjodor Dostojevski", 0);
    knjige.Add(knjiga);
}catch(LibraryLimitException ex)
{
    Console.WriteLine(ex.Message );
} */

SafeExecutor.Execute(() => { // kraci zapis try catch blokova koristeci static klasu i static metodu 
    Knjiga knjiga = new Knjiga(5, "Zločin i kazna", 1866, "Fjodor Dostojevski", 0);
    knjige.Add(knjiga); 
}); 



Film film = new Film();
film.Info();
Console.WriteLine(film.GetDostupnost(sviArtikli)); 
var datumpozajmice1 = new DateOnly(2026, 1, 15);
var datumpozajmice2 = new DateOnly(2026, 1, 27);
Console.WriteLine($"Kasnjenje: {film.IzracunajKasnjenje(datumpozajmice1)}");
Console.WriteLine($"Kasnjenje: {film.IzracunajKasnjenje(datumpozajmice2)}"); 


Analitika analiza = new Analitika();
analiza.Analiza(knjige);
analiza.GenerisiIzvjestaj(sviArtikli);


var herman = analiza.FiltrirajKnjige(knjige, k => k.Autor == "Hermann Hesse").ToList();
Console.WriteLine($"Knjige u listi koje je napisao Herman Hesse: ");
herman.ForEach(x => Console.WriteLine(x.Naslov));
Console.WriteLine("\n \n");
var debeleKnjige = analiza.FiltrirajKnjige(knjige, k => k.BrojStranica > 150).ToList();
debeleKnjige.ForEach(x => Console.WriteLine(x.Naslov)); 




