public class IngredientInRecipeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string BaseName { get; set; } = string.Empty;
    public string Quantity { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
}
