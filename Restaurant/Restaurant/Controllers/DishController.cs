using Application;
using Application.Interfaces.IDish;
using Application.Request.Create;
using Application.Request.Update;
using Application.Response;
using Application.Validators;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;
        private readonly DishValidator _dishValidator;
        
        public DishController(IDishService dishService, DishValidator dishValidator)
        {
            _dishService = dishService;
            _dishValidator = dishValidator;
        }

        //Create
        [HttpPost]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDishAsync([FromBody] DishRequest request)
        {
            var validationErrors = await _dishValidator.ValidateAsync(request);
            if (validationErrors.Any())
            {
                return BadRequest(new ApiError { Message = string.Join(" | ", validationErrors) });
            }
            try
            {
                //Crear el plato si pasó las validaciones
                var result = await _dishService.CreateDishAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
             //Captura de errores inesperados
             return StatusCode(StatusCodes.Status500InternalServerError, new ApiError{Message = $"Ocurrió un error al crear el plato: {ex.Message}"});
            }

        }
        


        //Update
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status200OK)]
        /*[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]*/
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

       

        // Buscar platos con filtros --REVISAR ESTO--
       /* [HttpGet]
         public async Task<IActionResult> GetAllDishes([FromQuery] string? name, [FromQuery] int? category, [FromQuery] SortOrder sortByPrice, [FromQuery] bool onlyActive = true)
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
             var dishes = await _dishService.GetAllDishesAsync(name, category, sortByPrice, onlyActive);
             return Ok(dishes);
         }*/


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
