using Application.Enum;
using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Models.Response;
using Application.Validators;

namespace Application.Services
{
    public class GetDishService : IGetService
    {
        private readonly IDishQuery _dishQuery;
        private readonly IGetValidation _dishValidator;

        public GetDishService(IDishQuery dishQuery, IGetValidation dishValidator)
        {
            _dishQuery = dishQuery;
            _dishValidator = dishValidator;
        }

        public async Task<IReadOnlyList<DishResponse>> GetAllDishesAsync(string? name, int? category, OrderPrice? sortByPrice, bool onlyActive)
        {
            // Validación de parámetros
            await _dishValidator.ValidateAllAsync(name, category, sortByPrice);

            var dishes = await _dishQuery.GetAllAsync();

            //Filtro por nombre
            if (!string.IsNullOrWhiteSpace(name))
            {
                var normalized = name.Trim().Normalize().ToLowerInvariant();
                dishes = dishes.Where(d => !string.IsNullOrWhiteSpace(d.Name) && d.Name.Normalize().ToLowerInvariant().Contains(normalized)).ToList();
            }

            // Filtro por categoría
            if (category.HasValue)
                dishes = dishes.Where(d => d.Category == category.Value).ToList();

            // Filtro por estado activo
            if (onlyActive)
                dishes = dishes.Where(d => d.Available).ToList();

            if (!dishes.Any())
            {
                if (!string.IsNullOrWhiteSpace(name) && category != null)
                    throw new Exceptions.NotFoundException($"No se encontró ningún plato llamado '{name}' en la categoría '{category}'");

                if (!string.IsNullOrWhiteSpace(name))
                    throw new Exceptions.NotFoundException($"No se encontró ningún plato llamado '{name}'");

                if (category != null)
                    throw new Exceptions.NotFoundException($"No se encontraron platos en la categoría '{category}'");

                throw new Exceptions.NotFoundException("No se encontraron platos con los filtros aplicados");
            }

            return dishes.Select(dish => new DishResponse
            {             
                Id = dish.DishId,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                Category = new GenericResponse
                {
                    Id = dish.CategoryNavigation.Id,
                    Name = dish.CategoryNavigation.Name,
                },
                IsActive = dish.Available,
                CreateAt = dish.CreateDate,
                UpdateAt = dish.UpdateDate,

            }).ToList();
        }
    }
}
