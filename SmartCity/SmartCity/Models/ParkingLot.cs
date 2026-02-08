


public class ParkingLot : CityNode, IMonitor
{
    public ParkingLot() { }
    
    public ParkingLot(int id, string zone, string street, string parkingName, int totalParkingSpots, int availableParkingSpots)
    {
        Id = id;
        CityZone = zone; 
        StreetName = street;
        this.ParkingName = parkingName;
        this.TotalParkingSpots = totalParkingSpots;
        this.AvailableParkingSpots = availableParkingSpots; 
    }


    public string ParkingName { get; set; }

    private int _totalSpots;

    public int TotalParkingSpots { 
            get { return _totalSpots; }
        set
        {
            if(value < 15 || value > 3000) // We track public city parking lots so they are always bigger than private ones that we don't track
            {
                throw new CityExceptionSystem("Incorrect Total Parking Parammeter!"); 
            }
           _totalSpots = value;
        }
   
    }
    private int _availableSpots;
    public int AvailableParkingSpots
    {
        get { return _availableSpots; }
        set
        {
            // Validacija ima smisla samo ako je Total postavljen.
            // Ako je Total 0, znači da EF Core još uvijek "gradi" objekt.
            if (TotalParkingSpots != 0)
            {
                if (value > TotalParkingSpots)
                    throw new CityExceptionSystem("You can't have more available spaces than total spaces!");

                if (value <= 0)
                    throw new CityExceptionSystem("Sorry, all parking spots are already occupied!");
            }

            _availableSpots = value;

            // Event okidač ostaje aktivan samo ako je validacija prošla
            if (TotalParkingSpots != 0 && value > TotalParkingSpots - 10)
            {
                CityExceptionSystem.CloseToTheParkingLimit(this, "Hurry up...");
            }
        }
    }

    public override void NodeInfo()
    {
        base.NodeInfo();
        Console.WriteLine($"Total number of parking spots: {TotalParkingSpots}, Available Parking Spots: {AvailableParkingSpots}");
    }


    public void CurrentSituation()
    {
        Console.WriteLine($"On parking {ParkingName} in street {StreetName}, there is {AvailableParkingSpots} out of {TotalParkingSpots} parking spots");
    }

    }


   






