
namespace CountEat.API.Services
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientListDto>> GetAllAsync();
        Task<IngredientDetailDto?> GetByIdAsync(int id);
    }
}
