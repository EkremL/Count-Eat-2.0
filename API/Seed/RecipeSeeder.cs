using System.Text.Json;
using CountEat.API.Data;
using CountEat.API.Models;
using Microsoft.EntityFrameworkCore;

public static class RecipeSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Recipes.AnyAsync()) return;

        var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Seed", "recipes.json");
        if (!File.Exists(jsonPath)) return;

        var json = await File.ReadAllTextAsync(jsonPath);
        var recipes = JsonSerializer.Deserialize<List<Recipe>>(json);

        if (recipes != null && recipes.Count > 0)
        {
            await context.Recipes.AddRangeAsync(recipes);
            await context.SaveChangesAsync();
        }
    }
}
