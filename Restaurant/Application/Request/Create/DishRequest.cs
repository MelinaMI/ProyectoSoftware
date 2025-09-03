using Application.Response;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Request.Create
{
    public class DishRequest
    {
        internal Guid Id;
        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Category { get; set; }
        public string Image { get; set; }
    }
}

