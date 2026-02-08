


public class Knjiga : BibliotekaArtikal, IPozajmica
{
    public Knjiga() { } // we have to use empty constructor for ef core 

    public Knjiga(int id, string naziv, int godinaIzdanja, string autor, int brojStranica)
    {
        Id = id;
        Naslov = naziv;
        GodinaIzdanja = godinaIzdanja;

        this.Autor = autor;
        this.BrojStranica = brojStranica;
    }


    public string Autor { get; set; }
    private int _brojStranica;
    public int BrojStranica
    {
        get { return _brojStranica; }

        set
        {
            if (value < 5 || value > 5000)
            {
                throw new LibraryLimitException("Broj stranica knjige mora imati neku realnu vrijednost (od 5 do 5000)!");
            }
            else
            {
                _brojStranica = value;
            }

        }
    }

    public double IzracunajKasnjenje(DateOnly datumPozajmljivanja)
    {
        DateOnly danas = DateOnly.FromDateTime(DateTime.Now);
        double trajanjePozajmice = danas.DayNumber - datumPozajmljivanja.DayNumber;

        double kasnjenje = trajanjePozajmice - 15; // obicno se stavlja po 15 dana roka pa sam primijenio fiksnu vrijednost
        if (kasnjenje < 0)
        {
            return 0;
        }
        else
        {
            return kasnjenje;
        }
    }
}




