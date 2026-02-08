
using System.Text.Json.Serialization;

[JsonDerivedType(typeof(Knjiga), typeDiscriminator: "Knjiga")]
[JsonDerivedType(typeof(Film), typeDiscriminator: "Film")]
public abstract class BibliotekaArtikal
{
    public int Id { get; set; } // EF Core autoincrement because of name and it will be used as primary key here and foreign 
    // key in children classes

    public string Naslov { get; set; }

    public int GodinaIzdanja { get; set; }


    public virtual void Info()
    {
        Console.WriteLine($"Ovaj artikal se zove {Naslov} i izdan je {GodinaIzdanja}");
    }




}









