using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs.FilmDtos
{
    public class FilmCreateDto
    {
        public string Naslov { get; set; }
        public int GodinaIzdanja { get; set; }
        public string Reziser { get; set; }

        public double TrajanjeUMinutama { get; set; }


    }
}
