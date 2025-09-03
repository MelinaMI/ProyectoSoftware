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
        Task<DishResponse> GetDishByIdAsync(Guid id);
        Task<DishResponse> UpdateDishAsync(Guid id, DishUpdateRequest request);
        //Task<List<Dish>> GetAllDishAsync(string? name, int? category, string? sortByPrice, bool? onlyActive);
        //Task <IReadOnlyList<DishResponse>> GetAllDishAsync(string? name, int? category, string? sortByPrice, bool? onlyActive);

    }
}
