

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int MinutesToPrepare { get; set; }

    public int CategoryId { get; set;  }

    public Category Category { get; set; }
}



