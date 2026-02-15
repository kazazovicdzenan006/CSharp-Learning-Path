using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs.BooksDtos
{
    public class BooksReadDto
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public int GodinaIzdanja { get; set; }
        public string Autor { get; set; }
        public int BrojStranica { get; set; }



    }
}
