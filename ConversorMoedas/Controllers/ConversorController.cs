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
        public async Task<IActionResult> Converter(CotacaoModel model)
        {
            var valorConvertido = await _cotacaoService.Converter(model.MoedaOriginal, model.MoedaDestino, model.Valor);
            await _cotacaoService.SalvarCotacao();
            return Ok(valorConvertido);
        }
    }
}
