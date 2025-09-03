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

        //Create
        [HttpPost]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDishAsync([FromBody] DishRequest request)
        {
            var result = await _dishService.CreateDishAsync(request);
            return Ok(result);
        }

        //Update
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>UpdateDishAsync(Guid id, [FromBody] DishUpdateRequest request)
        {
            if (id != id)
                return BadRequest("El ID de la URL no coincide con el del cuerpo.");
            try
            {
                var result = await _dishService.UpdateDishAsync(id,request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new ValidationProblemDetails
                {
                    Title = "Error de validación",
                    Status = 404,
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

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
