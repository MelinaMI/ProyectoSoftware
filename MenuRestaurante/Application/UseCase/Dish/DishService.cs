using Application.Interfaces.Services;

namespace Application.UseCase.Dish
{
     public class DishService :IDishService
    {
        private readonly AppDbContext _context;
        public DishService(AppDbContext context)
        {
            _context = context;
        }

        public Task<Domain.Entities.Dish> CreateDish(CreateDish dish)
        {
            throw new NotImplementedException();
        }
    }
}
