﻿using Application.Enum;
using Application.Interfaces.IDish;
using Application.Models.Request;
using Application.Models.Response;
using Microsoft.AspNetCore.Mvc;
using static Application.Validators.Exceptions;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DishController : ControllerBase
    {
        private readonly ICreateService _createService;
        private readonly IUpdateService _updateService;
        private readonly ICreateValidation _createValidator;
        private readonly IUpdateValidation _updateValidator;
        private readonly IGetService _getService;
        private readonly IGetValidation _getValidator;


        public DishController(ICreateService createService, IUpdateService updateService, ICreateValidation createValidator, IUpdateValidation updateValidator, IGetService getService, IGetValidation getValidator)
        {
            _createService = createService;
            _updateService = updateService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _getService = getService;
            _getValidator = getValidator;
        }

        //Create
        [HttpPost]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDishAsync([FromBody] DishRequest request)
        {
            try
            {
                await _createValidator.ValidateCreateAsync(request);
                var result = await _createService.CreateDishAsync(request);
                return Ok(result);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
            catch (ConflictException ex)
            {
                return Conflict(new ApiError { Message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiError { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiError { Message = $"Ocurrió un error inesperado: {ex.Message}" });
            }
        }

        //Update
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDishAsync(Guid id, [FromBody] DishUpdateRequest request)
        {
            try
            {
                await _updateValidator.ValidateUpdateAsync(id, request);
                var result = await _updateService.UpdateDishAsync(id, request); 
                return Ok(result);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiError { Message = ex.Message });
            }
            catch (ConflictException ex)
            {
                return Conflict(new ApiError { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiError { Message = $"Error interno del servidor: {ex.Message}" });
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
                await _getValidator.ValidateQueryAsync(name, category, sortByPrice);
                var result = await _getService.GetAllDishesAsync(name, category, sortByPrice, onlyActive);
                return Ok(result);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiError { Message = ex.Message });
            }
        }
    }
}
