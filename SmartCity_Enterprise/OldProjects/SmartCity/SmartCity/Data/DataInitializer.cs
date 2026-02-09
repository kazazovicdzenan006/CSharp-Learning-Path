using System.Collections.Generic;

public static class DataInitializer
{
    public static List<CrossRoad> GetCrossRoadSeedData()
    {
        return new List<CrossRoad>
        {
            // Koristimo Object Initializer bez ID-a jer baza sama generiše Identity
            new CrossRoad { CityZone = "Centar", StreetName = "Titova", CrossName = "BBI Raskrsnica", TrafficJamPercantage = 45.5 },
            new CrossRoad { CityZone = "Novo Sarajevo", StreetName = "Zmaja od Bosne", CrossName = "Pofalići", TrafficJamPercantage = 92.0 },
            new CrossRoad { CityZone = "Novi Grad", StreetName = "Bulevar Meše Selimovića", CrossName = "Otoka", TrafficJamPercantage = 70.2 },
            new CrossRoad { CityZone = "Stari Grad", StreetName = "Obala Kulina bana", CrossName = "Vijećnica", TrafficJamPercantage = 30.0 },
            new CrossRoad { CityZone = "Centar", StreetName = "Alipašina", CrossName = "Skenderija", TrafficJamPercantage = 95.5 },
            new CrossRoad { CityZone = "Ilidža", StreetName = "Butmirska cesta", CrossName = "Ilidža Centar", TrafficJamPercantage = 55.0 },
            new CrossRoad { CityZone = "Novi Grad", StreetName = "Džemala Bijedića", CrossName = "Čengić Vila", TrafficJamPercantage = 88.0 }
        };
    }

    public static List<ParkingLot> GetParkingLotSeedData()
    {
        return new List<ParkingLot>
        {
            new ParkingLot { CityZone = "Centar", StreetName = "Mis Irbina", ParkingName = "Parking BBI", TotalParkingSpots = 500, AvailableParkingSpots = 495 },
            new ParkingLot { CityZone = "Novo Sarajevo", StreetName = "Fra Anđela Zvizdovića", ParkingName = "Unitic", TotalParkingSpots = 200, AvailableParkingSpots = 50 },
            new ParkingLot { CityZone = "Stari Grad", StreetName = "Mula Mustafe Bašeskije", ParkingName = "Trg Oslobođenja", TotalParkingSpots = 150, AvailableParkingSpots = 145 },
            new ParkingLot { CityZone = "Novi Grad", StreetName = "Bulevar Meše Selimovića", ParkingName = "Opština", TotalParkingSpots = 300, AvailableParkingSpots = 100 },
            new ParkingLot { CityZone = "Centar", StreetName = "Terezija", ParkingName = "Skenderija Parking", TotalParkingSpots = 600, AvailableParkingSpots = 592 },
            new ParkingLot { CityZone = "Ilidža", StreetName = "Rustempašina", ParkingName = "Ilidža Park", TotalParkingSpots = 100, AvailableParkingSpots = 20 },
            new ParkingLot { CityZone = "Novo Sarajevo", StreetName = "Zmaja od Bosne", ParkingName = "Importanne", TotalParkingSpots = 400, AvailableParkingSpots = 395 }
        };
    }
}