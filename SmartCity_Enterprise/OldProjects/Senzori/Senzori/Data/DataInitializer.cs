using System.Collections.Generic;

public static class DataInitializer
{
    public static List<Uredjaj> GetSeedData()
    {
        return new List<Uredjaj>
        {
            // --- 7 SENZORA ---
            new Senzor { Name = "Living Room Temp", grad = "Sarajevo", Vrijednost = 22.5 },
            new Senzor { Name = "Kitchen Humidity", grad = "Banja Luka", Vrijednost = 45.0 },
            new Senzor { Name = "Server Room Heat", grad = "Tuzla", Vrijednost = 18.2 },
            new Senzor { Name = "Garden Moisture", grad = "Mostar", Vrijednost = 30.5 },
            new Senzor { Name = "Office Air Quality", grad = "Zenica", Vrijednost = 12.0 },
            new Senzor { Name = "Bedroom Thermostat", grad = "Bihać", Vrijednost = 21.0 },
            new Senzor { Name = "Warehouse Sensor", grad = "Doboj", Vrijednost = 15.8 },

            // --- 7 KONTROLERA ---
            new Kontroler { Name = "Main Hub", ModelKontrolera = "Siemens-S7", Status = true, BrojKanala = 4 },
            new Kontroler { Name = "Light Switcher", ModelKontrolera = "Arduino-Uno", Status = false, BrojKanala = 2 },
            new Kontroler { Name = "HVAC Manager", ModelKontrolera = "Honeywell-T1", Status = true, BrojKanala = 6 },
            new Kontroler { Name = "Gate Opener", ModelKontrolera = "Raspberry-Pi-4", Status = true, BrojKanala = 1 },
            new Kontroler { Name = "Industrial PLC", ModelKontrolera = "Schneider-Electric", Status = true, BrojKanala = 8 },
            new Kontroler { Name = "Security Node", ModelKontrolera = "ESP32", Status = false, BrojKanala = 4 },
            new Kontroler { Name = "Irrigation Master", ModelKontrolera = "Orbit-BHyve", Status = true, BrojKanala = 3 }
        };
    }
}