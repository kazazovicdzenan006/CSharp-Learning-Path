using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Services.DTOs.FilmDtos
{
    public class FilmReadDto
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public int GodinaIzdanja { get; set; }
        public string Reziser { get; set; }

        public double TrajanjeUMinutama { get; set; }


    }
}
