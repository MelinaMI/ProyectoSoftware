using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Mapper;
using Application.Models.Request;
using Application.Models.Response;
using Application.Validators;

namespace Application.Services
{
    public class UpdateDishService : IUpdateService
    {
        private readonly IDishCommand _dishCommand;
        private readonly ICategoryQuery _categoryQuery;
        private readonly IDishQuery _dishQuery;
        private readonly UpdateDishValidator _dishValidator;

        public UpdateDishService(IDishCommand dishCommand, ICategoryQuery categoryQuery, IDishQuery dishQuery)
        {
            _dishCommand = dishCommand;
            _categoryQuery = categoryQuery;
            _dishValidator = new UpdateDishValidator(dishQuery, categoryQuery);
            _dishQuery = dishQuery;
        }
        public async Task<DishResponse> UpdateDishAsync(Guid id, DishUpdateRequest request)
        {
            var dish = await _dishQuery.GetByIdAsync(id);
            await _dishValidator.ValidateUpdateAsync(id, request);

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
