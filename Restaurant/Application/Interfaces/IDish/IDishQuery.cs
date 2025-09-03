﻿using Domain.Entities;
//Responsabilidad: Obtener información de platos, con filtros y ordenamientos.
namespace Application.Interfaces.IDish
{
    public interface IDishQuery
    {
        //Task<IReadOnlyList<Dish>> GetAllDishesAsync(string? name, int? category, SortOrder? sortByPrice, bool? onlyActive);// ASC/DESC
        Task<Dish> GetDishByIdAsync(Guid id);
       
        
    }
}
