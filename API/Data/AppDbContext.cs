using Microsoft.EntityFrameworkCore;
using CountEat.API.Models;

namespace CountEat.API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
}