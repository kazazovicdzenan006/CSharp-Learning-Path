



public abstract class Uredjaj
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public virtual void Opis()
    {
        Console.WriteLine($"Ovo je uredjaj {Id} koji se zove: {Name} ");
    } 
}