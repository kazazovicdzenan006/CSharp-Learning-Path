using Microsoft.Extensions.Diagnostics.HealthChecks;


namespace BookTrackerFirstApi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public string Author { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }




    }
}
