using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Mapper;
using Application.Request.Create;
using Application.Request.Update;
using Application.Response;
using Application.Validators;
using Domain.Entities;

namespace Application.Services
{
    public class DishService : IDishService
    {
        private readonly IDishCommand _dishCommand;
        private readonly IDishQuery _dishQuery;
        private readonly ICategoryQuery _categoryQuery;
        private readonly DishValidator _dishValidator;
        public DishService(IDishCommand dishCommand, IDishQuery dishQuery, ICategoryQuery categoryQuery)
        {
            _dishCommand = dishCommand;
            _dishQuery = dishQuery;
            _categoryQuery = categoryQuery;
            _dishValidator = new DishValidator(dishQuery, categoryQuery);
        }
        
        public async Task<IReadOnlyList<DishResponse>> GetAllDishesAsync(string? name, int? category, OrderPrice? sortByPrice, bool onlyActive)
        {
            var dishes = await _dishQuery.GetAllAsync();
            // Filtro por nombre 
            if (!string.IsNullOrWhiteSpace(name))
                dishes = dishes.Where(dish => dish.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase)).ToList();

            // Filtro por categoría
            if (category.HasValue)
                dishes = dishes.Where(d => d.Category == category.Value).ToList();

            // Filtro por estado activo
            if (onlyActive)
                dishes = dishes.Where(d => d.Available).ToList();

            if (sortByPrice.HasValue)
            {
                dishes = sortByPrice == OrderPrice.asc
                    ? dishes.OrderBy(d => d.Price).ToList()
                    : dishes.OrderByDescending(d => d.Price).ToList();
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
        public async Task<DishResponse> CreateDishAsync(DishRequest request)
        {
            await _dishValidator.CreateValidateAsync(request);
            var dish = new Dish
            {
                DishId = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Category = request.Category,
                Available = true,
                ImageUrl = request.Image,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };
            await _dishCommand.InsertDishAsync(dish);

            var category = await _categoryQuery.GetByCategoryIdAsync(dish.Category);
            return DishMapper.DishResponse(dish, category);
        }
        public async Task<DishResponse> UpdateDishAsync(Guid id, DishUpdateRequest request)
        {
            var dish = await _dishQuery.GetByIdAsync(id);
            if (dish == null)
                throw new ArgumentException("No se encontró un plato con ese ID.");

            // Mapeo de actualización
            dish.Name = request.Name;
            dish.Description = request.Description;
            dish.Price = request.Price;
            dish.Category = request.Category;
            dish.ImageUrl = request.Image;
            dish.Available = request.IsActive;
            dish.UpdateDate = DateTime.UtcNow;

            await _dishCommand.UpdateDishAsync(dish);
            var category = await _categoryQuery.GetByCategoryIdAsync(dish.Category);
            return DishMapper.DishResponse(dish, category);
        }
    }
}
