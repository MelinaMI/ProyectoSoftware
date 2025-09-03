using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Request.Create;

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
        public async Task<List<string>> ValidateAsync(DishRequest request)
        {
            var errors = new List<string>();

            // Validar nombre no vacío y longitud
            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("El nombre del plato es obligatorio");

            if (request.Name.Length > 100)
                errors.Add("El nombre no puede superar los 100 caracteres.");
            
            /*var existing = await _dishQuery.GetAllDishAsync(dto.Name, null, null);
            if (existing.Any(d => d.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase)))
                errors.Add("Ya existe un plato con ese nombre.");*/

            // Validar precio
            if (request.Price <=0)
                errors.Add("El precio debe ser mayor a cero.");

            // Validar categoría
            var category = await _categoryQuery.GetByCategoryIdAsync(request.Category);
            if (category == null)
                errors.Add("La categoría seleccionada no existe.");

            return errors;   
        }
    }     
}
