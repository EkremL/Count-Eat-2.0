using AutoMapper;
using CountEat.API.Models;

namespace CountEat.API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Ingredient, IngredientListDto>();
        CreateMap<Ingredient, IngredientDetailDto>();
        CreateMap<Recipe, RecipeListDto>();
        CreateMap<Recipe, RecipeDetailDto>();

        CreateMap<RecipeIngredient, IngredientInRecipeDto>()
    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Ingredient.Turkish_Name))
    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Ingredient.ImageUrl));
    }

}