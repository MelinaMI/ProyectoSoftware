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
            await _dishValidator.ValidateAsync(request);
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
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Category = new GenericResponse
                {
                    Id = category.Id,
                    Name = category.Name,
                },
                Image = dish.ImageUrl,
                IsActive = dish.Available
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



        /*  public Task<IReadOnlyList<DishResponse>> GetAllDishAsync(string? name, int? category, string? sortByPrice, bool? onlyActive)
          {
              throw new NotImplementedException();
          }

          public Task UpdateDishAsync(UpdateDishRequest request)
          {
              throw new NotImplementedException();
          }
        */

        /*  public Task<List<Dish>> GetAllDishAsync()
          {
              throw new NotImplementedException();
          }*/




        /* public async Task UpdateDishAsync(UpdateDishRequest request)
         {
             await _dishValidator.ValidateAsync(request);
             var dish = new Dish
             {
                 DishId = request.Id,
                 Name = request.Name,
                 Description = request.Description,
                 Price = request.Price,
                 Category = request.Category,
                 Available = true,
                 UpdateDate = DateTime.UtcNow
             };

             await _dishCommand.UpdateDishAsync(dish);

         }*/

        /* public Task<Dish> UpdateDishAsync(Guid dishId)
         {
             throw new NotImplementedException();
         }

         async Task<IReadOnlyList<DishResponse>> IDishService.GetAllDishesAsync(string? name, int? category, string? sortByPrice, bool? onlyActive)
         {
             //Obtengo los platos desde la base de datos
             var dishes = await _dishQuery.GetAllDishesAsync(name, category, sortByPrice,onlyActive);

             //Transformo cada entidad de Dish de dominio en un DTO DishResponse
             return dishes.Select(d => new DishResponse
             {
                 //Mapeo
                 Id = d.DishId,
                 Name = d.Name,
                 Description = d.Description,
                 Price = d.Price,
                 ImageUrl = d.ImageUrl,
                 Available = d.Available,
                 CreateDate = d.CreateDate,
                 UpdateDate = d.UpdateDate,
                 //Mapeo la categoría asociada a un objeto más simple
                 Category = new GenericResponse
                 {
                     Id = d.CategoryNavigation.Id,
                     Name = d.CategoryNavigation.Name
                 }
             }).ToList();
         }*/
    }

        
    }
