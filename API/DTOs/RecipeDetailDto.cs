public class RecipeDetailDto
{
    public int Id { get; set; }
    public string? Turkish_Name { get; set; }
    public string? English_Name { get; set; }
    public string? MainCategory { get; set; }
    public string? Cuisine { get; set; }
    public string? ImageUrl { get; set; }

    public string? RecipeDetails { get; set; }
    public string? ShortDescription { get; set; }
    public string? Keywords { get; set; }

    public List<string> Genre { get; set; } = new();
    public List<string> MealTags { get; set; } = new();

    public float? Calorie { get; set; }
    public float? Protein { get; set; }
    public float? Fat { get; set; }
    public float? Carbohydrates { get; set; }
    public float? Fiber { get; set; }
    public float? Cholesterol { get; set; }
    public float? Saturated_Fat { get; set; }
    public float? Sodium { get; set; }
    public float? Potassium { get; set; }
    public float? Sugar { get; set; }
    public float? Serving_Size { get; set; }

    public int? ServingCount { get; set; }
    public int? PrepTimeMinutes { get; set; }
    public int? CookTimeMinutes { get; set; }
    public List<IngredientInRecipeDto> Ingredients { get; set; } = new();
}
