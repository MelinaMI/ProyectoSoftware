using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenuRestaurante.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetDish()
        {
            return new JsonResult(new { name = "JSON" });
        }
    }
}
