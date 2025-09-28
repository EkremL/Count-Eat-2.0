public interface IRecipeService
{
    Task<IEnumerable<RecipeListDto>> GetAllRecipesAsync();
    Task<RecipeDetailDto?> GetRecipeByIdAsync(int id);
}