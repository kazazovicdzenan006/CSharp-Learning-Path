using System.Collections.Generic;

public static class DataInitializer
{
    public static List<Knjiga> InitializeBookData()
    {
        return new List<Knjiga>()
        {
            new Knjiga { Naslov = "Na Drini Ćuprija", GodinaIzdanja = 1945, Autor = "Ivo Andrić", BrojStranica = 350 },
            new Knjiga { Naslov = "Stranac", GodinaIzdanja = 1942, Autor = "Albert Camus", BrojStranica = 120 },
            new Knjiga { Naslov = "Siddhartha", GodinaIzdanja = 1922, Autor = "Hermann Hesse", BrojStranica = 150 },
            new Knjiga { Naslov = "Stope u pijesku", GodinaIzdanja = 1990, Autor = "Nepoznat Autor", BrojStranica = 410 },
            new Knjiga { Naslov = "Zločin i kazna", GodinaIzdanja = 1866, Autor = "Fjodor Dostojevski", BrojStranica = 600 },
            new Knjiga { Naslov = "Sarajevski Marlboro", GodinaIzdanja = 1994, Autor = "Miljenko Jergović", BrojStranica = 180 },
            new Knjiga { Naslov = "Sila prirode", GodinaIzdanja = 2010, Autor = "Jane Harper", BrojStranica = 320 },
            new Knjiga { Naslov = "Tvrđava", GodinaIzdanja = 1970, Autor = "Meša Selimović", BrojStranica = 450 }
        };
    }

    public static List<Film> InitializeMovieData()
    {
        return new List<Film>()
        {
            new Film { Naslov = "Inception", GodinaIzdanja = 2010, Reziser = "Christopher Nolan", TrajanjeUMinutama = 148 },
            new Film { Naslov = "The Godfather", GodinaIzdanja = 1972, Reziser = "Francis Ford Coppola", TrajanjeUMinutama = 175 },
            new Film { Naslov = "Parasite", GodinaIzdanja = 2019, Reziser = "Bong Joon-ho", TrajanjeUMinutama = 132 },
            new Film { Naslov = "Interstellar", GodinaIzdanja = 2014, Reziser = "Christopher Nolan", TrajanjeUMinutama = 169 },
            new Film { Naslov = "Pulp Fiction", GodinaIzdanja = 1994, Reziser = "Quentin Tarantino", TrajanjeUMinutama = 154 },
            new Film { Naslov = "The Matrix", GodinaIzdanja = 1999, Reziser = "Lana Wachowski", TrajanjeUMinutama = 136 },
            new Film { Naslov = "Seven", GodinaIzdanja = 1995, Reziser = "David Fincher", TrajanjeUMinutama = 127 },
            new Film { Naslov = "Gladiator", GodinaIzdanja = 2000, Reziser = "Ridley Scott", TrajanjeUMinutama = 155 }
        };
    }
}