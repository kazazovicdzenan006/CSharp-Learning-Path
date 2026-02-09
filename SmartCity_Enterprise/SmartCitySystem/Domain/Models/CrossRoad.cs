

public class CrossRoad : CityNode, IMonitor
{
    public CrossRoad() { }
    public CrossRoad( string zone, string street, string crossName, double traficJamPercantage)
    {
        CityZone = zone;
        StreetName = street;
        this.CrossName = crossName;
        this.TrafficJamPercantage = traficJamPercantage;
    }


    public string CrossName { get; set; }
    private double _traffic;


    public double TrafficJamPercantage
    {
        get { return _traffic; }
        set
        {
            if (value < 0 || value > 100)
            {
                throw new CityExceptionSystem("Percantage can only be in between 0 and 100");
            }
            else if (value > 90)
            {
                CityExceptionSystem.HighTrafficJam(this, $"ATTENTION: High traffic jam {value}%");
            }
            _traffic = value;
        }

    }


    public void CurrentSituation()
    {
        Console.WriteLine($"City zone {CityZone}, Street {StreetName}, CrossRoad: {CrossName}, Traffic jam {TrafficJamPercantage}%");
    }
}



