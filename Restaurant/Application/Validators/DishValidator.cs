using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Request.Create;
using Application.Request.Update;

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

        public async Task<List<string>> CreateValidateAsync(DishRequest request)
        {
            var errors = new List<string>();

            // Validar nombre no vacío y longitud
            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("El nombre del plato es obligatorio");

            if (request.Name.Length > 100)
                errors.Add("El nombre no puede superar los 100 caracteres.");

            //Validar existencia del plato
            var existing = await _dishQuery.GetByNameAsync(request.Name);
            if (existing != null)
                errors.Add("Ya existe un plato con el nombre: " + request.Name);

            // Validar precio
            if (request.Price <=0)
                errors.Add("El precio debe ser mayor a cero.");

            // Validar categoría
            var category = await _categoryQuery.GetByCategoryIdAsync(request.Category);
            if (category == null)
                errors.Add("La categoría seleccionada no existe.");

            return errors;   
        }

        public async Task<List<string>> UpdateValidateAsync(Guid id, DishUpdateRequest request)
        {
            var errors = new List<string>();

            var existingDish = await _dishQuery.GetByIdAsync(id);

            if (existingDish == null)
                errors.Add("El plato no existe en el sistema.");

            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("El nombre del plato es obligatorio");

            if (request.Name.Length > 100)
                errors.Add("El nombre no puede superar los 100 caracteres.");

            var existing = await _dishQuery.GetByNameAsync(request.Name);
            if (existing != null)
                errors.Add("Ya existe un plato con el nombre: " + request.Name);

            if (request.Price <= 0)
                errors.Add("El precio debe ser mayor a cero.");

            var category = await _categoryQuery.GetByCategoryIdAsync(request.Category);
            if (category == null)
                errors.Add("La categoría seleccionada no existe.");

            return errors;
        }    
    }     
}
