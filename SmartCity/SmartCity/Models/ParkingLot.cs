


public class ParkingLot : CityNode, IMonitor
{
    public ParkingLot() { }
    
    public ParkingLot(int id, string zone, string street, string parkingName, int totalParkingSpots, int availableParkingSpots)
    {
        CityId = id;
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
            if (value > TotalParkingSpots)
            {
                throw new CityExceptionSystem("You can't have more available spaces than total spaces!");
            }
            else if (value <= 0)
            {
                throw new CityExceptionSystem("Sorry, all parking spots are already ocuppied!");
            }
            else if (value > TotalParkingSpots - 10)
            {
                CityExceptionSystem.CloseToTheParkingLimit(this, "Hurry up, we have just a few more spots available!");
            }

            _availableSpots = value;

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


   






