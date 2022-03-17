using Microsoft.AspNetCore.Mvc;

namespace ConversorMoedas.Api.Controllers
{
    [ApiController]
    [Route("api/conversor")]
    public class ConversorController : ControllerBase
    {
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public IActionResult Converter(int valor)
        {
            return Ok(valor);
        }
    }
}
