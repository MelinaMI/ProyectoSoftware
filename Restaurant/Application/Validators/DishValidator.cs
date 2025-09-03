using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Request.Create;
using System.ComponentModel.DataAnnotations;

namespace Application.Validators
{
    public class DishValidator
    {
        private readonly IDishQuery _dishQuery;
        private readonly ICategoryQuery _categoryQuery;
        public DishValidator(IDishQuery dishQuery, ICategoryQuery categoryQuery)
        {
            _dishQuery = dishQuery;
            _categoryQuery = categoryQuery;
        }

        //Creacion
        public async Task ValidateAsync(DishRequest request)
        {
            // Validar nombre no vacío y longitud
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException("El nombre no puede estar vacío.");

            if (request.Name.Length > 100)
                throw new ValidationException("El nombre no puede superar los 100 caracteres.");
            // Validar precio
            if (request.Price <= 0)
                throw new ValidationException("El precio debe ser mayor a cero.");
            // Validar categoría
            var category = await _categoryQuery.GetByCategoryIdAsync(request.Category);
            if (category == null)
                throw new ValidationException("La categoría especificada no existe.");
            // Validación de imagen
            if (string.IsNullOrWhiteSpace(request.Image))
                throw new ArgumentException("La URL de la imagen es obligatoria.");
            /*  
              // Validar unicidad del nombre
              var existing = await _dishQuery.GetDishByNameAsync(request.Name);
              if (existing != null)
                  throw new ValidationException("Ya existe un plato con ese nombre.");

              // Validar precio
              if (request.Price <= 0)
                  throw new ValidationException("El precio debe ser mayor a cero.");

              // Validar categoría
              var category = await _categoryQuery.GetByCategoryIdAsync(request.Category);
              if (category == null)
                  throw new ValidationException("La categoría especificada no existe.");
          }

          //Actualizacion
          public async Task ValidateAsync(UpdateDishRequest request)
          {
              // Validar nombre
              if (string.IsNullOrWhiteSpace(request.Name))
                  throw new ValidationException("El nombre no puede estar vacío.");

              if (request.Name.Length > 100)
                  throw new ValidationException("El nombre no puede superar los 100 caracteres.");

              // Validar unicidad del nombre (excluyendo el plato actual)
              var existing = await _dishQuery.GetDishByNameAsync(request.Name);
              if (existing != null && existing.DishId != request.Id)
                  throw new ValidationException("Ya existe un plato con ese nombre.");

              // Validar precio
              if (request.Price <= 0)
                  throw new ValidationException("El precio debe ser mayor a cero.");

              // Validar categoría
              var category = await _categoryQuery.GetByCategoryIdAsync(request.Category);
              if (category == null)
                  throw new ValidationException("La categoría especificada no existe.");*/
        }
    }     
}
