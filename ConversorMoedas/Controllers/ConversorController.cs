using ConversorMoedas.Api.Models;
using ConversorMoedas.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConversorMoedas.Api.Controllers
{
    [ApiController]
    [Route("api/conversor")]
    public class ConversorController : ControllerBase
    {

        private readonly ICotacaoService _cotacaoService;

        public ConversorController(ICotacaoService cotacaoService)
        {
            _cotacaoService = cotacaoService;
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async IActionResult Converter(CotacaoModel model)
        {
            await _cotacaoService.Iniciar();

            return Ok(valor);
        }
    }
}
