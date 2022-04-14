using ConversorMoedas.Common.Enums;
using ConversorMoedas.Domain.Interfaces;

namespace ConversorMoedas.Domain.Entities
{
    public class Cotacao : IEntity
    {
        public Guid Id { get; internal set; }
        public Moeda MoedaOrigem { get; internal set; }
        public Moeda MoedaDestino { get; internal set; }
        public decimal ValorOrigem { get; internal set; }
        public decimal ValorDestino { get; internal set; }
        public string OperacaoPorExtenso { get; internal set; }
        public DateTime DataOperacao { get; internal set; }

        public Cotacao()
        {
            Id = Guid.NewGuid();
        }        
    }
}
