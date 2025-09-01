using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.Dish
{
    public class CreateDish
    {
        [Required(ErrorMessage = "El nombre del plato es obligatorio")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Description { get; set; } = null!;
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0")]
        public decimal Price { get; set; }
        [Url(ErrorMessage = "La imagen debe ser una URL válida")]
        public string ImageUrl { get; set; } = null!;
        [Required(ErrorMessage = "La categoría es obligatoria")]
        public int Category { get; set; }
    }
}
