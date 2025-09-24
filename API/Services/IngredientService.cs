using AutoMapper;
using AutoMapper.QueryableExtensions;
using CountEat.API.Data;
using Microsoft.EntityFrameworkCore;

namespace CountEat.API.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public IngredientService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IngredientListDto>> GetAllAsync()
        {
            return await _context.Ingredients
                .ProjectTo<IngredientListDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            //?projectTo => linq-friendly usage. Prevent unnecessery memory process. 
        }

        public async Task<IngredientDetailDto?> GetByIdAsync(int id)
        {
            return await _context.Ingredients
                .Where(x => x.Id == id)
                .ProjectTo<IngredientDetailDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }
    }
}
