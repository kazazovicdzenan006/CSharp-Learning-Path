

public class Kontroler : Uredjaj, ITemperature { 
    public Kontroler() { }

    public Kontroler(int id, string name, string modelKontrolera, bool status, int brojKanala)
    {
        Id = id;
        Name = name;
        this.ModelKontrolera = modelKontrolera;
        this.Status = status;
        this.BrojKanala = brojKanala;
    }


    public string ModelKontrolera { get; set; }
    public bool Status { get; set; }

    private int _brojKanala;

    public int BrojKanala
    {
        get { return _brojKanala; }

        set
        {
            if (value < 0)
            {
                _brojKanala = 0;
                throw new DeviceLimitException("Ne moze biti postavljeno manje od 0 kanala!"); 
                

            }
            else if (value > 8)
            {
                _brojKanala = 8;
                throw new DeviceLimitException("Jednom kontroleru moze biti dodijeljeno maksimalno 8 kanala!"); 
                
            }
            else
            {
                _brojKanala = value;
            }
        }
    }

        public double TemperatureMeasurment()
    {
        double temp;
        Console.WriteLine($"Simulacija senozora za temperaturu, unesite temperaturu: ");
        while (true)
        {
           
                bool succes = double.TryParse(Console.ReadLine(), out double result);
                if (succes)
                {
                    temp = result;
                    break;
            }
            else
            {
                Console.WriteLine("Niste unijeli broj, pokusajte ponovo: ");
            }
          

          
        };
        

        return temp;
    }

    public string GetStatus()
    {
        if (this.Status)
        {
            return $"{this.Name}: UKLJUCENO";
        }
        else
        {
            return $"{this.Name}: ISKLJUCENO";
        }
    }


}










