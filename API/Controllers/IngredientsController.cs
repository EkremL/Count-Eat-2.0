
using CountEat.API.Data;
using CountEat.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CountEat.API.Services;


namespace CountEat.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class IngredientController : ControllerBase
{
    private readonly IIngredientService _ingredientService;

    public IngredientController(IIngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    //!GET Ingredients
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IngredientListDto>>> GetIngredients()
    {
        var ingredients = await _ingredientService.GetAllAsync();
        return Ok(ingredients);
    }

    //!GET Single Ingredient
    [HttpGet("{id}")]
    public async Task<ActionResult<IngredientDetailDto>> GetSingleIngredient(int id)
    {
        var ingredient = await _ingredientService.GetByIdAsync(id);
        if (ingredient == null) return NotFound();

        return Ok(ingredient);

    }
}