



using Domain.Models;

public abstract class Uredjaj
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int GradId { get; set; }

    public virtual Grad Grad { get; set; }

    public virtual string Opis()
    {
        return ($"Ovo je uredjaj {Id} koji se zove: {Name} ");
    }
}