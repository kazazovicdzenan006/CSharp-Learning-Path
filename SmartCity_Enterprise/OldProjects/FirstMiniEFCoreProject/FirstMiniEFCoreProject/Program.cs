using Microsoft.EntityFrameworkCore;

using (var context = new AppDbContext())
{
    // EF core is actually pretty smart because it will recognise relations automatically and add objects 
    var novaKategorija = new Category { Name = "Pasta" };
    var dorucak = new Category { Name = "Dorucak" };
    var postojiPasta = await context.Recipes.FirstOrDefaultAsync(r => r.Name == "Pasta Carbonara");
    var postojiOmlet = await context.Recipes.FirstOrDefaultAsync(r => r.Name == "Omlet");
    if (postojiPasta != null)
    {
        Console.WriteLine("Pasta vec postoji, dodavanje preskoceno!"); 
    }
    else
    {
        var noviRecept = new Recipe
        {

            Name = "Pasta Carbonara",
            Description = "Traditional italian recipe",
            MinutesToPrepare = 20,
            Category = novaKategorija

        };
        await context.AddAsync(noviRecept);
        
    }
    if (postojiOmlet != null)
    {
        Console.WriteLine("Omlet vec postoji, dodavanje preskoceno!");
    }
    else
    {

        var receptZaOmlet = new Recipe
        {
            Name = "Omlet",
            Description = "3 eggs and cheese",
            MinutesToPrepare = 15,
            Category = dorucak,
        };
        await context.AddAsync(receptZaOmlet);
        
    }

    await context.SaveChangesAsync();

    /*
    await context.Recipes.AddAsync(noviRecept);
    await context.Categories.AddAsync(novaKategorija);
    await context.Recipes.AddAsync(receptZaOmlet);
    await context.Categories.AddAsync(dorucak); 
    await context.SaveChangesAsync();



    All this we can do with .AddRange()
    */


    //await context.SaveChangesAsync();
    // I don't have to add Category because ef core will do that for me because Recipe has Foreign key to Category 
    var AllRecipes = await context.Recipes.Include(r => r.Category).ToListAsync();
    
    foreach (var r in AllRecipes)
    {
        Console.WriteLine($"ID {r.Id} Name: {r.Name}, Category {r.Category.Name}, Description {r.Description}, Minutes to prepare {r.MinutesToPrepare}");
    }
    /*
    foreach (var r in AllRecipes)
    {
        Console.WriteLine($"ID {r.Id} Name: {r.Name}, Category {r.Category}, Description {r.Description}, Minutes to prepare {r.MinutesToPrepare}");
    }

    var Categories = context.Categories.ToList();
    foreach (var cat in Categories) {
        Console.WriteLine($"Id: {cat.Id}, Name: {cat.Name}");
    }
    */



   
    

}