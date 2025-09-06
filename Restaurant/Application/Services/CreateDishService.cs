using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Mapper;
using Application.Models.Request;
using Application.Models.Response;
using Application.Validators;
using Domain.Entities;

namespace Application.Services
{
    public class CreateDishService : ICreateService
    {
        private readonly IDishCommand _dishCommand;
        private readonly ICategoryQuery _categoryQuery;
        private readonly CreateDishValidator _dishValidator;


        public CreateDishService(IDishCommand dishCommand, ICategoryQuery categoryQuery,IDishQuery dishQuery)
        {
            _dishCommand = dishCommand;
            _categoryQuery = categoryQuery;
            _dishValidator = new CreateDishValidator(dishQuery, categoryQuery);
        }
        public async Task<DishResponse> CreateDishAsync(DishRequest request)
        {
            await _dishValidator.ValidateCreateAsync(request);

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

    }
}
