using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Request.Create;
using Application.Request.Update;
using Application.Response;
using Application.Validators;
using Domain.Entities;
using System.Text.Json.Nodes;

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
        public async Task<DishResponse> CreateDishAsync(DishRequest request)
        {
            //await _dishValidator.ValidateAsync(request);
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
            return new DishResponse
            {
                Id = dish.DishId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Category = new GenericResponse
                {
                    Id = category.Id,
                    Name = category.Name,
                },
                Image = dish.ImageUrl,
                IsActive = dish.Available,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
                
            };
        }
        public async Task<DishResponse> GetDishByIdAsync(Guid id)
        {
            var dish = await _dishQuery.GetDishByIdAsync(id);

            if (dish == null)
                return null;

            var category = await _categoryQuery.GetByCategoryIdAsync(dish.Category);
            return new DishResponse
            {
                Id = dish.DishId,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                Image = dish.ImageUrl,
                IsActive = dish.Available,
                Category = new GenericResponse
                {
                    Id = category.Id,
                    Name = category.Name,
                },
                CreateAt = dish.CreateDate,
                UpdateAt = dish.UpdateDate,
            };
        }
        public async Task<DishResponse> UpdateDishAsync(Guid id, DishUpdateRequest request)
        {
            var dish = await _dishQuery.GetDishByIdAsync(id);
            if (dish == null)
                throw new ArgumentException("No se encontró un plato con ese ID.");
            // Actualización
            dish.Name = request.Name;
            dish.Description = request.Description;
            dish.Price = request.Price;
            dish.Category = request.Category;
            dish.ImageUrl = request.Image;
            dish.Available = request.IsActive;
            dish.UpdateDate = DateTime.UtcNow;

            await _dishCommand.UpdateDishAsync(dish);

            var category = await _categoryQuery.GetByCategoryIdAsync(dish.Category);

            return new DishResponse
            {
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                Category = new GenericResponse
                {
                    Id = category.Id,
                },
                Image = dish.ImageUrl,
                IsActive = dish.Available,
            };


        }
        /*public async Task<IReadOnlyList<DishResponse>> GetAllDishesAsync(string? name, int? categoryId, SortOrder? sortByPrice, bool? onlyActive)
        {
            var dish = await _dishQuery.GetAllDishesAsync(name, categoryId, sortByPrice,onlyActive);
            
            // Por cada plato, buscamos la categoría y armamos el DishResponse
            var dishResponses = new List<DishResponse>();

            foreach (var d in dish)
            {
                var category = await _categoryQuery.GetByCategoryIdAsync(d.CategoryNavigation.Id); 

                dishResponses.Add(new DishResponse
                {
                    Id = d.DishId,
                    Name = d.Name,
                    Description = d.Description,
                    Price = d.Price,
                    Category = new GenericResponse
                    {
                        Id = category.Id,
                        Name = category.Name
                    }
                });
            }

            return dishResponses;

        }*/

        


        

    }
}
