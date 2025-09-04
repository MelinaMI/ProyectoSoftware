using Application.Interfaces.IDish;
using Application.Request.Create;
using Application.Request.Update;
using Application.Response;
using Application.Validators;
using Microsoft.AspNetCore.Mvc;

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
            var validationErrors = await _dishValidator.CreateValidateAsync(request);
            if (validationErrors.Any())
                return BadRequest(new ApiError { Message = string.Join(" | ", validationErrors) });
            try
            {
                var result = await _dishService.CreateDishAsync(request); //Crea el plato si pasó las validaciones
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiError { Message = $"Ocurrió un error al crear el plato: {ex.Message}" });
            }
        }

        //Update
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDishAsync(Guid id, [FromBody] DishUpdateRequest request)
        {
            var validationErrors = await _dishValidator.UpdateValidateAsync(id, request);
            if (validationErrors.Any())
                return BadRequest(new ApiError{Message = string.Join(" | ", validationErrors)});
            try
            {
                var result = await _dishService.UpdateDishAsync(id, request);
                if (result == null)
                    return NotFound(new ApiError{Message = "No se encontró el plato para actualizar"});
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiError{Message = $"Error interno del servidor: {ex.Message}"});
            }
        }

        // Buscar platos con filtros 
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DishResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
        
        public async Task<ActionResult<DishResponse>> GetAllAsync([FromQuery] string? name,[FromQuery] int? category,[FromQuery] OrderPrice? sortByPrice,[FromQuery] bool onlyActive = true)
        {
            try
            {
                var result = await _dishService.GetAllDishesAsync(name,category, sortByPrice, onlyActive);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
        }
    }
}
