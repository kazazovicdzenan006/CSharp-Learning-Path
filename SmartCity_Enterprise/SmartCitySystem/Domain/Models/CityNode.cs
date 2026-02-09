
using Domain.Models;

public abstract class CityNode
{
    public int Id { get; set; }

    public string CityZone { get; set; }

    public string StreetName { get; set; }

    public int GradId { get; set; }
    public virtual Grad Grad { get; set; }

    public virtual void NodeInfo()
    {
        Console.WriteLine($"CityId: {Id}, City zone: {CityZone}, Street: {StreetName}");
    }


}
