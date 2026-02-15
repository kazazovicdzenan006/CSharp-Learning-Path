using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs.ControllersDtos
{
    public class ControllerCreateDto
    {
        public string Name { get; set; }
        public string ModelKontrolera { get; set; }
        public bool Status { get; set; }

        public int BrojKanala { get; set; }

    }
}
