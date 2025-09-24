using System.Text.Json;
using CountEat.API.Data;
using CountEat.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CountEat.API.Seed;

public static class IngredientSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Ingredients.AnyAsync()) return; //if it exists, do not add again!
        var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Seed", "ingredientson.json");
        if (!File.Exists(jsonPath)) return;

        var jsonData = await File.ReadAllTextAsync(jsonPath);
        var ingredients = JsonSerializer.Deserialize<List<Ingredient>>(jsonData);

        if (ingredients != null && ingredients.Count > 0)
        {
            await context.Ingredients.AddRangeAsync(ingredients);
            await context.SaveChangesAsync();
        }
    }
}