using Application.Response;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Request.Create
{
    public class DishRequest
    {
        internal Guid Id;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        public int Category { get; set; }

        public string Image { get; set; }

    }
}

