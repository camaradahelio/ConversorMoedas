using ConversorMoedas.Common.Enums;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConversorMoedas.Api.Models
{
    public class CotacaoModel
    {
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public Moeda MoedaOriginal { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public Moeda MoedaDestino { get; set; }

        [Required]
        public decimal Valor { get; set; }
    }
}
