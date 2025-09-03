using Application.Request.Create;
using Application.Request.Update;
using Application.Response;
using Domain.Entities;
//Responsabilidad: Coordinar validaciones, transformar DTOs, orquestar comandos y consultas.
namespace Application.Interfaces.IDish
{
    public interface IDishService
    {
        
       
        //Task<List<Dish>> GetAllDishAsync();
       
        Task<DishResponse> CreateDishAsync(CreateDishRequest request); //uso CreateDishRequest para separar las responsabilidades
       // Task UpdateDishAsync(UpdateDishRequest request);
        //Task <IReadOnlyList<DishResponse>> GetAllDishAsync(string? name, int? category, string? sortByPrice, bool? onlyActive);
        
    }
}
