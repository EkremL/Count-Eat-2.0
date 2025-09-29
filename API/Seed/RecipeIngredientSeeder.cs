
// using CountEat.API.Data;
// using CountEat.API.Helpers;
// using Microsoft.EntityFrameworkCore;
// using System.Text.RegularExpressions;

// namespace CountEat.API.Seed;

// public static class RecipeIngredientSeeder
// {

//     public static async Task SeedRecipeIngredientsAsync(AppDbContext context)
//     {
//         bool DEBUG_MODE = true;
//         if (await context.RecipeIngredients.AnyAsync())
//         {
//             if (DEBUG_MODE)
//             {
//                 Console.ForegroundColor = ConsoleColor.Yellow;
//                 Console.WriteLine("⚠️ RecipeIngredients table already has data. Clearing...");
//                 Console.ResetColor();
//             }

//             context.RecipeIngredients.RemoveRange(context.RecipeIngredients);
//             await context.SaveChangesAsync();
//         }


//         var recipes = await context.Recipes.ToListAsync();
//         var allIngredients = await context.Ingredients.ToListAsync();


//         var recipeIngredients = new List<RecipeIngredient>();


//         // foreach (var recipe in recipes)
//         // {
//         //     if (string.IsNullOrWhiteSpace(recipe.Ingridients))
//         //         continue;

//         //     Console.ForegroundColor = ConsoleColor.Cyan;
//         //     Console.WriteLine($"\n🥗 [RECIPE] Processing: {recipe.Turkish_Name} (ID: {recipe.Id})");
//         //     Console.ResetColor();

//         //     var lines = recipe.Ingridients.Split('\n', StringSplitOptions.RemoveEmptyEntries);

//         //     foreach (var line in lines)
//         //     {
//         //         var parsed = IngredientParser.ParseIngredientString(line);

//         //         if (parsed.Name == null)
//         //         {
//         //             Console.ForegroundColor = ConsoleColor.DarkYellow;
//         //             Console.WriteLine($"⚠️ [SKIPPED] Could not parse: '{line}'");
//         //             Console.ResetColor();
//         //             continue;
//         //         }

//         //         Console.ForegroundColor = ConsoleColor.Green;
//         //         Console.WriteLine($"✅ [PARSED] '{line}' → Quantity: '{parsed.Quantity}', Name: '{parsed.Name}'");
//         //         Console.ResetColor();

//         //         var normalizedParsedName = StringHelper.NormalizeString(parsed.Name);

//         //         var matchedIngredient = allIngredients
//         //             .FirstOrDefault(i =>
//         //                 i.Turkish_Name != null &&
//         //                 StringHelper.NormalizeString(i.Turkish_Name).Contains(normalizedParsedName)
//         //             );

//         //         if (matchedIngredient == null)
//         //         {
//         //             Console.ForegroundColor = ConsoleColor.Red;
//         //             Console.WriteLine($"❌ [NOT FOUND] Ingredient not matched in DB: '{parsed.Name}' from Recipe #{recipe.Id}");
//         //             Console.ResetColor();
//         //             continue;
//         //         }

//         //         if (recipeIngredients.Any(ri => ri.RecipeId == recipe.Id && ri.IngredientId == matchedIngredient.Id))
//         //         {
//         //             Console.ForegroundColor = ConsoleColor.Magenta;
//         //             Console.WriteLine($"⚠️ [DUPLICATE] Skipped: Recipe #{recipe.Id} already has Ingredient #{matchedIngredient.Id} ({matchedIngredient.Turkish_Name})");
//         //             Console.ResetColor();
//         //             continue;
//         //         }

//         //         Console.ForegroundColor = ConsoleColor.Blue;
//         //         Console.WriteLine($"🔗 [MATCHED] '{parsed.Name}' → DB Ingredient: '{matchedIngredient.Turkish_Name}' (ID: {matchedIngredient.Id})");
//         //         Console.ResetColor();

//         //         recipeIngredients.Add(new RecipeIngredient
//         //         {
//         //             RecipeId = recipe.Id,
//         //             IngredientId = matchedIngredient.Id,
//         //             Quantity = parsed.Quantity,
//         //             Unit = parsed.Unit ??= "adet"
//         //         });
//         //     }

//         // }

//         var totalIngredientsInDb = allIngredients.Count;
//         if (DEBUG_MODE)
//         {
//             Console.ForegroundColor = ConsoleColor.Cyan;
//             Console.WriteLine($"📦 Toplam Ingredient veritabanında: {totalIngredientsInDb}");
//             Console.ResetColor();
//         }

//         foreach (var recipe in recipes)
//         {
//             if (string.IsNullOrWhiteSpace(recipe.Ingridients))
//                 continue;

//             if (DEBUG_MODE)
//             {
//                 Console.ForegroundColor = ConsoleColor.Cyan;
//                 Console.WriteLine($"\n🥗 [RECIPE] Processing: {recipe.Turkish_Name} (ID: {recipe.Id})");
//                 Console.ResetColor();
//             }

//             var lines = recipe.Ingridients.Split('\n', StringSplitOptions.RemoveEmptyEntries);

//             foreach (var line in lines)
//             {
//                 var parsed = IngredientParser.ParseIngredientString(line);

//                 if (parsed.Name == null)
//                 {
//                     if (DEBUG_MODE)
//                     {
//                         Console.ForegroundColor = ConsoleColor.DarkYellow;
//                         Console.WriteLine($"⚠️ [SKIPPED] Could not parse: '{line}'");
//                         Console.ResetColor();
//                     }
//                     continue;
//                 }

//                 if (DEBUG_MODE)
//                 {
//                     Console.ForegroundColor = ConsoleColor.Green;
//                     Console.WriteLine($"✅ [PARSED] '{line}' → Quantity: '{parsed.Quantity}', Name: '{parsed.Name}'");
//                     Console.ResetColor();
//                 }

//                 // var normalizedParsedName = StringHelper.NormalizeString(parsed.Name);
//                 var cleanedName = IngredientParser.CleanName(parsed.Name);
//                 var normalizedParsedName = StringHelper.NormalizeString(cleanedName);

//                 var matchedIngredient = allIngredients
//                 .FirstOrDefault(i =>
//                     i.Turkish_Name != null &&
//                     (
//                         StringHelper.NormalizeString(i.Turkish_Name).Contains(normalizedParsedName) ||
//                         normalizedParsedName.Contains(StringHelper.NormalizeString(i.Turkish_Name))
//                     )
//                 );

//                 if (matchedIngredient == null)
//                 {
//                     if (DEBUG_MODE)
//                     {
//                         Console.ForegroundColor = ConsoleColor.Red;
//                         Console.WriteLine($"❌ [NOT FOUND] Ingredient not matched in DB: '{parsed.Name}' from Recipe #{recipe.Id}");
//                         Console.WriteLine("🛑 Debug mode aktif. Burada bekleniyor...");
//                         Console.ReadLine();
//                         Console.ResetColor();
//                     }
//                     continue;
//                 }

//                 if (recipeIngredients.Any(ri => ri.RecipeId == recipe.Id && ri.IngredientId == matchedIngredient.Id))
//                 {
//                     if (DEBUG_MODE)
//                     {
//                         Console.ForegroundColor = ConsoleColor.Magenta;
//                         Console.WriteLine($"⚠️ [DUPLICATE] Skipped: Recipe #{recipe.Id} already has Ingredient #{matchedIngredient.Id} ({matchedIngredient.Turkish_Name})");
//                         Console.ResetColor();
//                     }
//                     continue;
//                 }

//                 if (DEBUG_MODE)
//                 {
//                     Console.ForegroundColor = ConsoleColor.Blue;
//                     Console.WriteLine($"🔗 [MATCHED] '{parsed.Name}' → DB Ingredient: '{matchedIngredient.Turkish_Name}' (ID: {matchedIngredient.Id})");
//                     Console.ResetColor();
//                 }

//                 recipeIngredients.Add(new RecipeIngredient
//                 {
//                     RecipeId = recipe.Id,
//                     IngredientId = matchedIngredient.Id,
//                     Quantity = parsed.Quantity,
//                     Unit = parsed.Unit ??= "adet"
//                 });

//             }
//         }


//         await context.RecipeIngredients.AddRangeAsync(recipeIngredients);
//         await context.SaveChangesAsync();

//         if (DEBUG_MODE)
//         {
//             Console.ForegroundColor = ConsoleColor.Green;
//             Console.WriteLine($"✅ [COMPLETED] {recipeIngredients.Count} recipe-ingredient relationships added.");
//             Console.ResetColor();
//         }

//     }
// }

using CountEat.API.Data;
using CountEat.API.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace CountEat.API.Seed;

public static class RecipeIngredientSeeder
{
    public static async Task SeedRecipeIngredientsAsync(AppDbContext context)
    {
        bool DEBUG_MODE = true;

        if (await context.RecipeIngredients.AnyAsync())
        {
            if (DEBUG_MODE)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠️ RecipeIngredients table already has data. Clearing...");
                Console.ResetColor();
            }

            context.RecipeIngredients.RemoveRange(context.RecipeIngredients);
            await context.SaveChangesAsync();
        }

        var recipes = await context.Recipes.ToListAsync();
        var allIngredients = await context.Ingredients.ToListAsync();
        var recipeIngredients = new List<RecipeIngredient>();

        if (DEBUG_MODE)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"📦 Toplam Ingredient veritabanında: {allIngredients.Count}");
            Console.ResetColor();
        }

        foreach (var recipe in recipes)
        {
            if (string.IsNullOrWhiteSpace(recipe.Ingridients))
                continue;

            if (DEBUG_MODE)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\n🥗 [RECIPE] Processing: {recipe.Turkish_Name} (ID: {recipe.Id})");
                Console.ResetColor();
            }

            var lines = recipe.Ingridients.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var parsed = IngredientParser.ParseIngredientString(line);

                if (parsed.Name == null)
                {
                    if (DEBUG_MODE)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"⚠️ [SKIPPED] Could not parse: '{line}'");
                        Console.ResetColor();
                    }
                    continue;
                }

                // Cleaning + Normalization
                var cleanedName = IngredientParser.CleanName(parsed.Name);
                var normalizedParsedName = StringHelper.NormalizeString(
                    IngredientParser.NormalizeName(cleanedName)
                );

                if (DEBUG_MODE)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✅ [PARSED] '{line}' → Quantity: '{parsed.Quantity}', Name: '{parsed.Name}' → Normalized: '{normalizedParsedName}'");
                    Console.ResetColor();
                }

                var matchedIngredient = allIngredients
                    .FirstOrDefault(i =>
                        i.Turkish_Name != null &&
                        (
                            StringHelper.NormalizeString(IngredientParser.NormalizeName(i.Turkish_Name)).Contains(normalizedParsedName)
                            || normalizedParsedName.Contains(StringHelper.NormalizeString(IngredientParser.NormalizeName(i.Turkish_Name)))
                        )
                    );

                if (matchedIngredient == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"❌ [NOT FOUND] Ingredient not matched in DB: '{parsed.Name}' from Recipe #{recipe.Id}");

                    // ✅ log dosyasına da yazalım
                    File.AppendAllText("MissingIngredientsLog.txt", $"[MISSING] Recipe #{recipe.Id} → {parsed.Name}\n");

                    if (DEBUG_MODE)
                    {
                        Console.WriteLine("🛑 Debug mode aktif. Burada bekleniyor...");
                        // Console.ReadLine();
                    }

                    Console.ResetColor();

                    // ✅ bu satır if bloğunun DIŞINA DEĞİL, en sonuna koyulmalı
                    continue;
                }

                if (recipeIngredients.Any(ri => ri.RecipeId == recipe.Id && ri.IngredientId == matchedIngredient.Id))
                {
                    if (DEBUG_MODE)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"⚠️ [DUPLICATE] Skipped: Recipe #{recipe.Id} already has Ingredient #{matchedIngredient.Id} ({matchedIngredient.Turkish_Name})");
                        Console.ResetColor();
                    }
                    continue;
                }

                if (DEBUG_MODE)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"🔗 [MATCHED] '{parsed.Name}' → DB Ingredient: '{matchedIngredient.Turkish_Name}' (ID: {matchedIngredient.Id})");
                    Console.ResetColor();
                }

                recipeIngredients.Add(new RecipeIngredient
                {
                    RecipeId = recipe.Id,
                    IngredientId = matchedIngredient.Id,
                    Quantity = parsed.Quantity,
                    Unit = parsed.Unit ?? "adet"
                });
            }
        }

        await context.RecipeIngredients.AddRangeAsync(recipeIngredients);
        await context.SaveChangesAsync();

        if (DEBUG_MODE)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ [COMPLETED] {recipeIngredients.Count} recipe-ingredient relationships added.");
            Console.ResetColor();
        }
    }
}
