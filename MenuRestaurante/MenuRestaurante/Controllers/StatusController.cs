using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
/* Responsabilidad del controller
 * Recibir la petición HTTP
 * Llamar al Service
 * Devolver un Response adecuado al cliente
 */
namespace MenuRestaurante.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
    }
}
