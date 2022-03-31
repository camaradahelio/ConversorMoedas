using ConversorMoedas.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace ConversorMoedas.Api.Models
{
    public class CotacaoModel
    {
        [Required]
        [EnumDataType(typeof(Moeda))]
        public Moeda MoedaOriginal { get; set; }

        [Required]
        [EnumDataType(typeof(Moeda))]
        public Moeda MoedaDestino { get; set; }

        [Required]
        public decimal Valor { get; set; }
    }
}
