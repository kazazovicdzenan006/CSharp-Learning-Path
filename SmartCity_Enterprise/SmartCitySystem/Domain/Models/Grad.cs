namespace Domain.Models;


public class Grad
{
    public int Id { get; set; }
    public string Name { get; set; }
    

    public virtual ICollection<CityNode> CityNodes { get; set; } = new List<CityNode>();
    public  virtual ICollection<Uredjaj> Devices { get; set; } = new List<Uredjaj>();

    public virtual ICollection<BibliotekaArtikal> BookStoreInventory { get; set; } = new List<BibliotekaArtikal>();
}



