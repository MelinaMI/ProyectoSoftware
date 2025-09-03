using Application.Interfaces.IDish;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Commands
{
    public class DishCommand : IDishCommand
    {
        private readonly AppDbContext _context;
        public DishCommand(AppDbContext context)
        {
            _context = context;
        }
        public async Task InsertDishAsync(Dish dish)
        {
            _context.Add(dish);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateDishAsync(Dish dish)
        {
            _context.Update(dish);
            await _context.SaveChangesAsync();
        }
    }
}
