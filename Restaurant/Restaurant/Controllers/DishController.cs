using Application.Interfaces.IDish;
using Application.Request.Create;
using Application.Request.Update;
using Application.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }


        [HttpPost]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDishAsync([FromBody] DishRequest request)
        {
            var result = await _dishService.CreateDishAsync(request);
            return Ok(result);

            //return CreatedAtAction(nameof(GetDishById), new { id = result.Id }, result);
        }

        /* public async Task<IActionResult> GetAllDishes([FromQuery] string? name, [FromQuery] int? category, [FromQuery] string? sortByPrice, [FromQuery] bool? onlyActive)
         {
             var result = await _dishService.GetAllDishAsync(name, category, sortByPrice, onlyActive);
             return new JsonResult(result);
         }*/

        // Buscar platos con filtros
        /* [HttpGet]
         public async Task<IActionResult> GetAllDishes([FromQuery] string? name, [FromQuery] int? category, [FromQuery] string? sortByPrice, [FromQuery] bool onlyActive = true)
         {
             // Validación de parámetros
             var validSortValues = new[] { "asc", "desc", null };
             if (!validSortValues.Contains(sortByPrice?.ToLower()) || !validSortValues.Contains(sortByPrice?.ToLower()))
             {
                 return BadRequest(new ProblemDetails
                 {
                     Title = "Parámetros inválidos",
                     Status = 400,
                     Detail = "Los valores permitidos para sortByPrice y sortByName son: 'asc', 'desc' o vacío.",
                     Instance = HttpContext.Request.Path
                 });
             }
             var dishes = await _dishService.GetAllDishAsync(name, category, sortByPrice, onlyActive);
             return Ok(dishes);
         }

             //Crear nuevo plato
             [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateDish([FromBody] CreateDishRequest request)
        {
             if (!ModelState.IsValid)
                 return BadRequest(ModelState);

             await _dishService.CreateDishAsync(request);
             return StatusCode(201); // Created
        }

         */

    }















    /*[Route("api/v1/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;
        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }
        //Obtengo todos los platos

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DishResponse>), 200)]//respuesta
        public async Task<IActionResult> GetAllDishes(
            [FromQuery] string? nameFilter,
            [FromQuery] int categoryId = 0,
            [FromQuery] string? priceOrder = null)
        {
            var result = await _dishService.GetAllDishesAsync(nameFilter ?? string.Empty, categoryId, priceOrder ?? string.Empty);
            return Ok(result);
        }

        //Creo un plato
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateDish([FromBody] CreateDishRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _dishService.CreateDishAsync(request);
            return StatusCode(201); // Created
        }

        //Actualizo un plato
        [HttpPut("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateDish(Guid id, [FromBody] UpdateDishRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != request.id)
                return BadRequest("El ID de la URL no coincide con el body");

            try
            {
                await _dishService.UpdateDishAsync(request);
                return NoContent(); // 204
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Plato no encontrado");
            }
        }
    }*/
}
