// Lista od 7 senzora
using Senzori;
using System.Security.Cryptography.X509Certificates;

List<Senzor> senzori = new List<Senzor>();
/*
{
    new Senzor(1, "Senzor Kvalitete Zraka", "Sarajevo", 145.2),
    new Senzor(2, "PM10 Metar", "Zenica", 210.5),
    new Senzor(3, "Vlažnost Zraka", "Mostar", 45.0),
    new Senzor(4, "CO2 Senzor", "Tuzla", 400.1),
    new Senzor(5, "Buka", "Banja Luka", 65.5),
    new Senzor(6, "Senzor Kvalitete Zraka", "Kakanj", 95.8),
    new Senzor(7, "PM2.5 Metar", "Sarajevo", 88.3)
}; */

// Lista od 7 kontrolera
List<Kontroler> kontroleri = new List<Kontroler>(); /*
{
    new Kontroler(101, "Pametni Termostat", "Siemens-T1", true, 4),
    new Kontroler(102, "Prečišćivač Vazduha", "Xiaomi-P2", false, 2),
    new Kontroler(103, "Ventilacioni Sistem", "Industro-X", true, 8),
    new Kontroler(104, "AC Jedinica", "LG-Smart", true, 1),
    new Kontroler(105, "Pumpa za vodu", "Grundfos-M", false, 3),
    new Kontroler(106, "Sistem za navodnjavanje", "Rain-Go", true, 6),
    new Kontroler(107, "Industrijski hladnjak", "Daikin-Cool", true, 5)
};    */



SafeExecutor.Execute( () =>
{
    Kontroler kontroler = new Kontroler(12, "bilo sta", "Xiaomi-P2", true, 12);
    kontroler.Opis();
    kontroler.TemperatureMeasurment();
}
    );


/* I don't need this because I made SafeExecutor
try
{
    Kontroler kontroler = new Kontroler(12, "bilo sta", "Xiaomi-P2", true, 12);
    kontroler.Opis();
    kontroler.TemperatureMeasurment();
}
catch(DeviceLimitException exception)
{
    Console.WriteLine($"jedan kontroler ne moze primiti vise od 8 kanala: {exception.Message}");
}
*/ 
DeviceLimitException.OnLimitReached += (s, v) =>
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Kriticno zagadenje detektovano");
    Console.WriteLine($"Senor {s.Name} u gradu {s.grad}");
    Console.WriteLine($"Izmjerena vrijednost: {v}");
    Console.ResetColor();
    File.AppendAllText("Log.txt", $"[{DateTime.Now}] Senzor {s.Name} ({s.grad}) - Vrijednost: {v}\n"); 
};

SafeExecutor.Execute(
    ()=>
    {
        Senzor senzor = new Senzor(1, "Senzor Kvalitete Zraka", "Sarajevo", 932);
        senzori.Add(senzor);
    }
    ); 


/* I made SafeExecutor so i don't need this
try
{
    Senzor senzor = new Senzor(1, "Senzor Kvalitete Zraka", "Sarajevo", 932);
    senzori.Add(senzor);
}
catch (DeviceLimitException exception)
{
    Console.WriteLine($"upozorenje! {exception}");
}

*/



StorageManager<Senzor> senzorMenager = new StorageManager<Senzor>();
//await senzorMenager.spasiPodatke("senzor.json", senzori);
senzori = await senzorMenager.UcitajPodatke("senzor.json"); 

StorageManager<Kontroler> kontrolerMenager = new StorageManager<Kontroler>();
//await kontrolerMenager.spasiPodatke("kontroler.json", kontroleri);
kontroleri = await kontrolerMenager.UcitajPodatke("kontroler.json");

senzori.ForEach(x => Console.WriteLine($"ID: {x.Id}, naziv senzora: {x.Name}, grad: {x.grad}, vrijednost: {x.Vrijednost}"));


kontroleri.ForEach(x => Console.WriteLine($"ID: {x.Id}, naziv senzora: {x.Name}, model kontrolera: {x.ModelKontrolera}, status: {x.Status}, broj kanala: {x.BrojKanala}"));


var veciOd100 = senzori.Where(x => x.Vrijednost > 100).Select(x => new { x.grad, x.Vrijednost }).Distinct().ToList(); 
veciOd100.ForEach(x => Console.WriteLine($"\n \nGrad: {x.grad}, Vrijednost ocitanja: {x.Vrijednost} \n \n"));
Analitika analitika = new Analitika();
analitika.analiza(senzori);


List<ITemperature> sviUredjaji = new List<ITemperature>();
sviUredjaji.AddRange(senzori);   // Možeš dodati cijelu listu odjednom
sviUredjaji.AddRange(kontroleri);


foreach (var uredjaj in sviUredjaji)
{
    Console.WriteLine($"Status uredaja: {uredjaj.GetStatus()}");
}

analitika.GenerisiIzvjestaj(sviUredjaji); 


//kontroleri[0].BrojKanala = 12;

//kontroleri.ForEach(x => Console.WriteLine($"ID: {x.Id}, naziv senzora: {x.Name}, model kontrolera: {x.ModelKontrolera}, status: {x.Status}, broj kanala: {x.BrojKanala}"));
