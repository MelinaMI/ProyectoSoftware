using Application.Interfaces.IDish;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Queries
{
    public class DishQuery : IDishQuery
    {
        private readonly AppDbContext _context;
        public DishQuery(AppDbContext context) 
        {
            _context = context;
        }

       

        public Task<List<Dish>> GetAllDishesAsync(string? name, int? category, string? sortByPrice, bool? onlyActive)
        {
            throw new NotImplementedException();
        }

        /* public async Task<Dish?> GetDishByIdAsync(Guid id)
         {
             return await _context.Dishes.Include(d => d.CategoryNavigation).AsNoTracking().FirstOrDefaultAsync(d => d.DishId == id);
         }
        */

        /*  public async Task<Dish?> GetDishByNameAsync(string name)
          {
              return await _context.Dishes.Include(d => d.CategoryNavigation).AsNoTracking().FirstOrDefaultAsync(d => d.Name.ToLower() == name.ToLower());
          }
          */



        /* async Task<List<Dish>> IDishQuery.GetAllDishesAsync(string? name, int? category, decimal? sortByPrice, bool? onlyActive)
         {
             var query = _context.Dishes.Include(d => d.CategoryNavigation).AsNoTracking().AsQueryable();

             // Filtro por nombre (contiene, case-insensitive)
             if (!string.IsNullOrWhiteSpace(name))
                 query = query.Where(d => d.Name.ToLower().Contains(name.ToLower()));

             // Filtro por categoría
             if (category.HasValue)
                 query = query.Where(d => d.Category == category.Value);

             // Filtro por estado activo
             if (onlyActive.HasValue && onlyActive.Value)
                 query = query.Where(d => d.Available);

             // Ordenamiento por precio
             if (!decimal.)
             {
                 switch (sortByPrice.ToLower())
                 {
                     case "asc":
                         query = query.OrderBy(d => d.Price);
                         break;
                     case "desc":
                         query = query.OrderByDescending(d => d.Price);
                         break;
                     default:
                         throw new ValidationException("Parámetros de ordenamiento inválidos");
                 }
             }

             return await query.ToListAsync();
         }*/
    }
}
