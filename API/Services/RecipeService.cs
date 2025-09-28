using AutoMapper;
using CountEat.API.Data;
using Microsoft.EntityFrameworkCore;

public class RecipeService : IRecipeService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public RecipeService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RecipeListDto>> GetAllRecipesAsync()
    {
        var recipes = await _context.Recipes.ToListAsync();
        return _mapper.Map<List<RecipeListDto>>(recipes);
    }

    public async Task<RecipeDetailDto?> GetRecipeByIdAsync(int id)
    {
        var recipe = await _context.Recipes
            .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
            .FirstOrDefaultAsync(r => r.Id == id);

        return recipe is null ? null : _mapper.Map<RecipeDetailDto>(recipe);
    }
}
