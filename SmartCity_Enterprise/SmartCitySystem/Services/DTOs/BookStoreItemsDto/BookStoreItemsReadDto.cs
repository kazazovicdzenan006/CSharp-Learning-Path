using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs.BookStoreItemsDto
{
    public class BookStoreItemsReadDto
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public int GodinaIzdanja { get; set; }
        public int GradId { get; set; }
        public string GradName { get; set; }

    }
}
