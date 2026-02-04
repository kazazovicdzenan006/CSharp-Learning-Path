



using System.Text.Json.Serialization;

[JsonDerivedType(typeof(Senzor), typeDiscriminator: "Senzor")]
[JsonDerivedType(typeof(Kontroler), typeDiscriminator: "Kontroler")]
public abstract class Uredjaj
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public virtual void Opis(Action<string> writer)
    {
        writer($"Ovo je uredjaj {Id} koji se zove: {Name} ");
    } 
}