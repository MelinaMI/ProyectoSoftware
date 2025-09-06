using Application.Models.Response;
using Domain.Entities;

namespace Application.Mapper
{
    public static class DishMapper
    {
        public static DishResponse DishResponse(Dish dish, Category category)
        {
            return new DishResponse
            {
                Id = dish.DishId,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                Category = new GenericResponse
                {
                    Id = category.Id,
                    Name = category.Name
                },
                Image = dish.ImageUrl,
                IsActive = dish.Available,
                CreateAt = dish.CreateDate,
                UpdateAt = dish.UpdateDate
            };
        }
    }
}
