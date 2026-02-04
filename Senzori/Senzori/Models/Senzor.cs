



using System.ComponentModel.Design;

public class Senzor : Uredjaj, ITemperature
{
    public Senzor() { }
    public Senzor(int id, string name, string grad, double vrijednost)
    {
        Id = id;
        Name = name;
        this.grad = grad;
        this.Vrijednost = vrijednost;

    }

    public string grad { get; set; }
    private double _vrijednost;
    public double Vrijednost
    {
        get { return _vrijednost; }
        set
        {
            if (value < 0 || value > 1000)
            {
                
                throw new DeviceLimitException("Vrijednosti nisu ispravne, setam na 0");
            }

            if (value > 800)
            {
                // Šaljemo "ovaj" senzor (this) i vrijednost (value)
                DeviceLimitException.Trigger(this, value);
            }


            _vrijednost = value;
            
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
                Console.WriteLine("Niste unijeli broj!");
            }



        };

        return temp;
    }


    public override void Opis(Action<string> writer)
    {
        base.Opis(writer);
        writer($"Uredjaj se nalazi u {this.grad}");
    }


    public string GetStatus()
    {
        if(this.Vrijednost > 100)
        {
            return $"{this.Name}: KRITICNO"; 
        }
        else
        {
            return $"{this.Name}: Normalno"; 
        }
    }

}


