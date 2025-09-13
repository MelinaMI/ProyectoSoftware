﻿using Application.Enum;
using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using System.Text;

namespace Application.Validators
{
    public class GetDishValidator : IGetValidation
    {
        private readonly ICategoryQuery _categoryQuery;

        public GetDishValidator(ICategoryQuery categoryQuery)
        {
            _categoryQuery = categoryQuery;
        }

        public async Task ValidateAllAsync(string? name, int? category, OrderPrice? sortByPrice)
        {
            name = name?.Trim().Normalize(NormalizationForm.FormC);
            // Validación de nombre
            if (!string.IsNullOrWhiteSpace(name) && name.Length > 100)
                throw new Exceptions.BadRequestException("El nombre no puede superar los 100 caracteres");

            // Validación de categoría
            if (category.HasValue)
            {
                if (category.Value <= 0)
                    throw new Exceptions.BadRequestException("La categoría debe ser mayor a cero");

                var exists = await _categoryQuery.GetByCategoryIdAsync(category.Value);
                if (exists == null)
                    throw new Exceptions.NotFoundException("La categoría no existe");
            }
        }
    }
}
