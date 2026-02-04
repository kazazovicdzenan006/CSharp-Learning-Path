public static class DataInitializer
{
    public static List<Uredjaj> GetSeedData()
    {
        return new List<Uredjaj>
        {
            // --- 7 SENSORS ---
            new Senzor(1, "Living Room Temp", "Sarajevo", 22.5),
            new Senzor(2, "Kitchen Humidity", "Banja Luka", 45.0),
            new Senzor(3, "Server Room Heat", "Tuzla", 18.2),
            new Senzor(4, "Garden Moisture", "Mostar", 30.5),
            new Senzor(5, "Office Air Quality", "Zenica", 12.0),
            new Senzor(6, "Bedroom Thermostat", "Bihać", 21.0),
            new Senzor(7, "Warehouse Sensor", "Doboj", 15.8),

            // --- 7 CONTROLLERS ---
            new Kontroler(8, "Main Hub", "Siemens-S7", true, 4),
            new Kontroler(9, "Light Switcher", "Arduino-Uno", false, 2),
            new Kontroler(10, "HVAC Manager", "Honeywell-T1", true, 6),
            new Kontroler(11, "Gate Opener", "Raspberry-Pi-4", true, 1),
            // Example of a controller with 8 channels (max limit)
            new Kontroler(12, "Industrial PLC", "Schneider-Electric", true, 8),
            new Kontroler(13, "Security Node", "ESP32", false, 4),
            new Kontroler(14, "Irrigation Master", "Orbit-BHyve", true, 3)
        };
    }
}