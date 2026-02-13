using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public abstract class Person
    {
        public int Id {  get; set; }

        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }


    }
}
