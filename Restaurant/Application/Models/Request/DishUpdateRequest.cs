using System.ComponentModel.DataAnnotations;

namespace Application.Models.Request
{
    public class DishUpdateRequest
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Category { get; set; }
        public string Image { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
