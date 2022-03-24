using ConversorMoedas.Common.Enums;
using ConversorMoedas.Domain.Entities;
using ConversorMoedas.Domain.Interfaces;

namespace ConversorMoedas.Domain.Services
{
    public class CotacaoService : ICotacaoService
    {
        private Cotacao _cotacao;
        private readonly ICambioService _cambioService;
        private readonly ICotacaoRepository _cotacaoRepository;

        public CotacaoService(ICambioService cambioService, ICotacaoRepository cotacaoRepository)
        {
            _cambioService = cambioService;
            _cotacaoRepository = cotacaoRepository;
        }

        public void Iniciar(Guid usuarioId)
        {
            this._cotacao = new Cotacao(usuarioId);
        }

        private decimal Converter(decimal valor, double taxa)
        {
            double valorConvertidoBaseadoNoEuro = Convert.ToDouble(valor) * taxa;
            return Convert.ToDecimal(valorConvertidoBaseadoNoEuro);
        }

        private decimal Converter(double taxaMoedaOrigemBaseadoNoEuro, double taxaMoedaDestinoBaseadoNoEuro, decimal valor)
        {
            double taxaMoedaOrigemParaEuro = (1 / taxaMoedaOrigemBaseadoNoEuro);
            double valorParaEuro = Convert.ToDouble(valor) * taxaMoedaOrigemParaEuro;
            double valorConvertidoMoedaOrigemParaDestino = valorParaEuro * taxaMoedaDestinoBaseadoNoEuro;
            return Convert.ToDecimal(valorConvertidoMoedaOrigemParaDestino);
        }

        public async Task<decimal> Converter(Moeda moedaOrigem, Moeda moedaDestino, decimal valor)
        {
            decimal valorConvertido = 0;

            this._cotacao.MoedaOrigem = moedaOrigem;
            this._cotacao.MoedaDestino = moedaDestino;

            var taxasCambioAtualizadas = await _cambioService.ObterTaxas();

            var taxaMoedaOrigemBaseadaNoEuro = taxasCambioAtualizadas.ToEnumMaps(moedaOrigem);
            var taxaMoedaDestinoBasedaNoEuro = taxasCambioAtualizadas.ToEnumMaps(moedaDestino);

            if (taxaMoedaOrigemBaseadaNoEuro.Moeda != Moeda.EUR)
            {
                this._cotacao.OperacaoPorExtenso = $"EUR to {moedaOrigem} : {taxaMoedaOrigemBaseadaNoEuro} | EUR to {moedaDestino} : {taxaMoedaDestinoBasedaNoEuro}";
                valorConvertido = this.Converter(taxaMoedaOrigemBaseadaNoEuro.Taxa, taxaMoedaDestinoBasedaNoEuro.Taxa, valor);
            }
            else
            {
                this._cotacao.OperacaoPorExtenso = $"{moedaDestino} : {taxaMoedaDestinoBasedaNoEuro.Taxa}";
                valorConvertido = this.Converter(valor, taxaMoedaDestinoBasedaNoEuro.Taxa);
            }

            this._cotacao.ValorOrigem = valor;
            this._cotacao.ValorDestino = valorConvertido;

            return valorConvertido;
        }

        public async Task<Guid> SalvarCotacao()
        {
            var id = await _cotacaoRepository.SalvarCotacao(this._cotacao);
            return id;
        }
    }
}
