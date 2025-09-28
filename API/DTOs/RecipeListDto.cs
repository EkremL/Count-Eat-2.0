public class RecipeListDto
{
    public int Id { get; set; }
    public required string Turkish_Name { get; set; }
    public string? English_Name { get; set; }
    public string? MainCategory { get; set; }
    public string? ImageUrl { get; set; }
    public int Calorie { get; set; }
    public float? Protein { get; set; }
    public float? Fat { get; set; }
    public float? Carbohydrates { get; set; }
    public float? Fiber { get; set; }
    public float? Cholesterol { get; set; }
    public float? Saturated_Fat { get; set; }
    public float? Sodium { get; set; }
    public float? Potassium { get; set; }
    public float? Sugar { get; set; }

}
