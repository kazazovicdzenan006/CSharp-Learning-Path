
using System.Text.Json.Serialization;

[JsonDerivedType(typeof(ParkingLot), typeDiscriminator: "Parking")]
[JsonDerivedType(typeof(CrossRoad), typeDiscriminator: "Cross")]
public abstract class CityNode
    {
            public int Id {  get; set; }

            public string CityZone { get; set; }
            
            public string StreetName { get; set; }


    public virtual void NodeInfo()
    {
        Console.WriteLine($"CityId: {Id}, City zone: {CityZone}, Street: {StreetName}");
    }


    }
