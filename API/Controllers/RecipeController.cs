using CountEat.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;

    public RecipeController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRecipes()
    {
        return Ok(await _recipeService.GetAllRecipesAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecipeById(int id)
    {
        var recipe = await _recipeService.GetRecipeByIdAsync(id);
        if (recipe == null) return NotFound();

        return Ok(recipe);
    }
}
