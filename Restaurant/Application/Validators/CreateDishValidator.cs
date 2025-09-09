﻿using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Models.Request;
using System.Text;

namespace Application.Validators
{
    public class CreateDishValidator : ICreateValidation
    {
        private readonly IDishQuery _dishQuery;
        private readonly ICategoryQuery _categoryQuery;
        public CreateDishValidator(IDishQuery query, ICategoryQuery categoryQuery)
        {
            _dishQuery = query;
            _categoryQuery = categoryQuery;
        }
        public async Task ValidateCreateAsync(DishRequest request)
        {
            request.Name = request.Name?.Trim().Normalize(NormalizationForm.FormC);

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new Exceptions.BadRequestException("El nombre del plato es obligatorio");

            if (request.Price <= 0)
                throw new Exceptions.BadRequestException("El precio debe ser mayor a 0");

            if (request.Category <= 0)
                throw new Exceptions.BadRequestException("La categoría es obligatoria");

            var category = await _categoryQuery.GetByCategoryIdAsync(request.Category);
            if (category == null)
                throw new Exceptions.NotFoundException("La categoría seleccionada no existe");

            var existingDish = await _dishQuery.GetByNameAsync(request.Name);
            if (existingDish != null)
                throw new Exceptions.ConflictException("Ya existe un plato con ese nombre");
        }
    }
}

