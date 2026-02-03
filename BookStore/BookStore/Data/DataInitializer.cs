




using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public static class DataInitializer
{
    public static List<Knjiga> InitializeBookData()
    {
        return new List<Knjiga>()
        {
            new Knjiga(1, "Na Drini Ćuprija", 1945, "Ivo Andrić", 350),
            new Knjiga(2, "Stranac", 1942, "Albert Camus", 120),
            new Knjiga(3, "Siddhartha", 1922, "Hermann Hesse", 150),
            new Knjiga(4, "Stope u pijesku", 1990, "Nepoznat Autor", 410),
            new Knjiga(5, "Zločin i kazna", 1866, "Fjodor Dostojevski", 600),
            new Knjiga(6, "Sarajevski Marlboro", 1994, "Miljenko Jergović", 180),
            new Knjiga(7, "Sila prirode", 2010, "Jane Harper", 320),
            new Knjiga(8, "Tvrđava", 1970, "Meša Selimović", 450)
        };

    }

    public static List<Film> InitializeMovieData()
    {
        return new List<Film>() 
        {
            new Film(101, "Inception", 2010, "Christopher Nolan", 148),
            new Film(102, "The Godfather", 1972, "Francis Ford Coppola", 175),
            new Film(103, "Parasite", 2019, "Bong Joon-ho", 132),
            new Film(104, "Interstellar", 2014, "Christopher Nolan", 169),
            new Film(105, "Pulp Fiction", 1994, "Quentin Tarantino", 154),
            new Film(106, "The Matrix", 1999, "Lana Wachowski", 136),
            new Film(107, "Seven", 1995, "David Fincher", 127),
            new Film(108, "Gladiator", 2000, "Ridley Scott", 155)
        };


    }


}