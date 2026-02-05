using (var context = new AppDbContext())
{
    var novaKategorija = new Category { Name = "Pasta" };
    var noviRecept = new Recipe
    {

        Name = "Pasta Carbonara",
        Description = "Traditional italian recipe",
        MinutesToPrepare = 20,
        Category = novaKategorija

    };
    

    await context.Recipes.AddAsync(noviRecept);
    await context.Categories.AddAsync(novaKategorija);
    await context.SaveChangesAsync();

    var AllRecipes = context.Recipes.ToList();
    foreach (var r in AllRecipes)
    {
        Console.WriteLine($"ID {r.Id} Name: {r.Name}, Category {r.Category}, Description {r.Description}, Minutes to prepare {r.MinutesToPrepare}");
    }

    var Categories = context.Categories.ToList();
    foreach (var cat in Categories) {
        Console.WriteLine($"Id: {cat.Id}, Name: {cat.Name}");
    }




   
    

}