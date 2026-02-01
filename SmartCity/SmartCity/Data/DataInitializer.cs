

public static class DataInitializer { 
    public static List<CrossRoad> GetCrossRoadSeedData()
    {
        return new List<CrossRoad>
{
    new CrossRoad(1, "Centar", "Titova", "BBI Raskrsnica", 45.5),
    new CrossRoad(2, "Novo Sarajevo", "Zmaja od Bosne", "Pofalići", 92.0), // Okida HighTrafficJam event
    new CrossRoad(3, "Novi Grad", "Bulevar Meše Selimovića", "Otoka", 70.2),
    new CrossRoad(4, "Stari Grad", "Obala Kulina bana", "Vijećnica", 30.0),
    new CrossRoad(5, "Centar", "Alipašina", "Skenderija", 95.5), // Okida HighTrafficJam event
    new CrossRoad(6, "Ilidža", "Butmirska cesta", "Ilidža Centar", 55.0),
    new CrossRoad(7, "Novi Grad", "Džemala Bijedića", "Čengić Vila", 88.0)
};
     

    }

    public static List<ParkingLot> GetParkingLotSeedData()
    {
        return new List<ParkingLot>   
{
    new ParkingLot(101, "Centar", "Mis Irbina", "Parking BBI", 500, 495), // Okida CloseToTheParkingLimit (500 - 10 = 490, a ovdje je 495 dostupno)
    new ParkingLot(102, "Novo Sarajevo", "Fra Anđela Zvizdovića", "Unitic", 200, 50),
    new ParkingLot(103, "Stari Grad", "Mula Mustafe Bašeskije", "Trg Oslobođenja", 150, 145), // Okida event
    new ParkingLot(104, "Novi Grad", "Bulevar Meše Selimovića", "Opština", 300, 100),
    new ParkingLot(105, "Centar", "Terezija", "Skenderija Parking", 600, 592), // Okida event
    new ParkingLot(106, "Ilidža", "Rustempašina", "Ilidža Park", 100, 20),
    new ParkingLot(107, "Novo Sarajevo", "Zmaja od Bosne", "Importanne", 400, 395) // Okida event
};

    }




}




