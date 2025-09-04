using Application.Request.Create;
using Application.Request.Update;
using Application.Response;
using Domain.Entities;
//Responsabilidad: Coordinar validaciones, transformar DTOs, orquestar comandos y consultas.
namespace Application.Interfaces.IDish
{
    public interface IDishService
    {
        Task<DishResponse> CreateDishAsync(DishRequest request); //uso CreateDishRequest para separar las responsabilidades
       // Task<DishResponse> GetDishByIdAsync(Guid id);
       // Task<DishResponse> GetDishByNameAsync(string name);
        Task<DishResponse> UpdateDishAsync(Guid id, DishUpdateRequest request);
        Task<IReadOnlyList<DishResponse>> GetAllDishesAsync(string? name, int? category, OrderPrice? sortByPrice, bool onlyActive);
    }
}
