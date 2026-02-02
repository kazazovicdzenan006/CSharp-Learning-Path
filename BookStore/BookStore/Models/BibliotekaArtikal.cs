
using System.Text.Json.Serialization;

[JsonDerivedType(typeof(Knjiga), typeDiscriminator: "Knjiga")]
[JsonDerivedType(typeof(Film), typeDiscriminator: "Film")]
public abstract class BibliotekaArtikal
{
    public int Id { get; set; }

    public string Naslov { get; set; }

    public int GodinaIzdanja { get; set; }


    public virtual void Info()
    {
        Console.WriteLine($"Ovaj artikal se zove {Naslov} i izdan je {GodinaIzdanja}");
    }




}









