

public class Film : BibliotekaArtikal, IPozajmica
{

    public Film() { }
    public Film( string naziv, int godinaIzdanja, string reziser, double trajanjeUMinutama)
    {
        Naslov = naziv;
        GodinaIzdanja = godinaIzdanja;
        this.Reziser = reziser;
        this.TrajanjeUMinutama = trajanjeUMinutama;
    }

    public string Reziser { get; set; }

    private double _trajanje;

    public double TrajanjeUMinutama
    {
        get { return _trajanje; }

        set
        {
            if (value < 20 || value > 400)
            {
                throw new LibraryLimitException("Trajanje filma moze trajati izmedu 20 i 400 minuta. ");
            }

            _trajanje = value;

        }
    }

    public double IzracunajKasnjenje(DateOnly datumPozajmljivanja)
    {
        DateOnly danas = DateOnly.FromDateTime(DateTime.Now);
        double trajanjePozajmice = danas.DayNumber - datumPozajmljivanja.DayNumber;

        double kasnjenje = trajanjePozajmice - 5; // film se realno moze pogledati u 5 dana jer ne bi ga uzeli ako nemamo vremena da ga pogledamo (odnosi se na oldSchool cd pozajmice) 
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






