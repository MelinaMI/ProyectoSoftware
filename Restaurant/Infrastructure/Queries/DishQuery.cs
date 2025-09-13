﻿using Application.Interfaces.IDish;
using Azure.Core;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Linq;
using System.Text;

namespace Infrastructure.Queries
{
    public class DishQuery : IDishQuery
    {
        private readonly AppDbContext _context;
        public DishQuery(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<IReadOnlyList<Dish>> GetAllAsync()
        {
            return await _context.Dishes.Include(c => c.CategoryNavigation).AsNoTracking().ToListAsync();
        }
        public async Task<Dish> GetByIdAsync(Guid id)
        {
            return await _context.Dishes.Include(c => c.CategoryNavigation).AsNoTracking().FirstOrDefaultAsync(d => d.DishId == id);
        }
       public async Task<Dish?> GetByNameAsync(string name)
        {
            var normalizedName = name.Trim().Normalize(NormalizationForm.FormC).ToLowerInvariant();

            // Traer platos al cliente (memoria) para poder usar Normalize
            var dishes = await _context.Dishes
                .Include(d => d.CategoryNavigation)
                .AsNoTracking()
                .ToListAsync();

            return dishes.FirstOrDefault(d =>
                !string.IsNullOrWhiteSpace(d.Name) &&
                d.Name.Trim().Normalize(NormalizationForm.FormC).ToLowerInvariant() == normalizedName
            );
        }
    }
}
