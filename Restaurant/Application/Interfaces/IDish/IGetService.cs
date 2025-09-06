using Application.Models.Response;
using Application.Enum;

//Responsabilidad: Coordinar validaciones, transformar DTOs, orquestar comandos y consultas.
namespace Application.Interfaces.IDish
{
    public interface IGetService
    {
        Task<IReadOnlyList<DishResponse>> GetAllDishesAsync(string? name, int? category, OrderPrice? sortByPrice, bool onlyActive);
        Task<DishResponse> GetDishByIdAsync(Guid id);
        Task<DishResponse> GetDishByNameAsync(string name);
    }
}
