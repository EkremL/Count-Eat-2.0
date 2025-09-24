using AutoMapper;
using CountEat.API.Models;

namespace CountEat.API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Ingredient, IngredientListDto>();
        CreateMap<Ingredient, IngredientDetailDto>();
    }
}