using Domain.Entities;
//Responsabilidad: Obtener información de platos, con filtros y ordenamientos.
namespace Application.Interfaces.IDish
{
    public interface IDishQuery
    {
        Task<List<Dish>> GetAllDishesAsync(string? name, int? category, string? sortByPrice, bool? onlyActive);// ASC/DESC
        Task<Dish> GetDishByIdAsync(Guid id);
       // Task<Dish> GetDishByNameAsync(string name);
        
    }
}
