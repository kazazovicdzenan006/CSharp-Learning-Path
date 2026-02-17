using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs.BookStoreAnalizeDtos
{
    public class GroupedByAuthorDto
    {
        public string imeAutora { get; set; }
        public double prosjekStranica { get; set; }
        public int brojKnjiga { get; set; }



    }
}
