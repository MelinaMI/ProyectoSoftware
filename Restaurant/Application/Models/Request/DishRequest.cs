﻿using System.ComponentModel.DataAnnotations;

namespace Application.Models.Request
{
    public class DishRequest
    {
        public Guid Id;
        //[Required(ErrorMessage = "El nombre del plato es obligatorio")]
        public required string Name { get; set; }
        public string Description { get; set; }
        public required decimal Price { get; set; }
        public required int Category { get; set; }
        public string Image { get; set; }
        //buscar como integrar el enum del orderprice al dto
    }
}
