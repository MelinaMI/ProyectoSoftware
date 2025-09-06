using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Models.Request;
using static Application.Validators.Exceptions;

namespace Application.Validators
{
    public class UpdateDishValidator : IUpdateValidation
    {
        private readonly IDishQuery _dishQuery;
        private readonly ICategoryQuery _categoryQuery;
        public UpdateDishValidator(IDishQuery query, ICategoryQuery categoryQuery)
        {
            _dishQuery = query;
            _categoryQuery = categoryQuery;
        }
        public async Task ValidateUpdateAsync(Guid id, DishUpdateRequest request)
        {           
            var dish = await _dishQuery.GetByIdAsync(id);
            if (dish == null)
                throw new NotFoundException("El plato que desea actualizar no existe");

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new BadRequestException("El nombre del plato es obligatorio.");

            if (request.Name.Length > 100)
                throw new BadRequestException("El nombre no puede superar los 100 caracteres.");

            var existing = await _dishQuery.GetByNameAsync(request.Name);
            if (existing != null && existing.DishId != id) 
                throw new ConflictException("Ya existe un plato con el nombre: " + request.Name);

            if (request.Price <= 0)
                throw new BadRequestException("El precio debe ser mayor a cero.");

            var category = await _categoryQuery.GetByCategoryIdAsync(request.Category);
            if (category == null)
                throw new NotFoundException("La categoría seleccionada no existe.");

        }
    }
}
