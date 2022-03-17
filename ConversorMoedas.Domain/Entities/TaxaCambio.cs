using ConversorMoedas.Common.Enums;

namespace ConversorMoedas.Domain.Entities
{
    public class TaxaCambio
    {
        public double BRL { get; set; }
        public double USD { get; set; }
        public double JPY { get; set; }
    }

    public static class TaxaCambioExtensions
    {
        public static TaxaCambioToEnumMaps ToEnumMaps(this TaxaCambio taxas, Moeda moeda)
        {
            List<TaxaCambioToEnumMaps> taxaCambioToEnums = new List<TaxaCambioToEnumMaps>();
            taxaCambioToEnums.Add(new TaxaCambioToEnumMaps
            {
                Moeda = Moeda.BRL,
                Taxa = taxas.BRL
            });

            taxaCambioToEnums.Add(new TaxaCambioToEnumMaps
            {
                Moeda = Moeda.JPY,
                Taxa = taxas.JPY
            });

            taxaCambioToEnums.Add(new TaxaCambioToEnumMaps
            {
                Moeda = Moeda.USD,
                Taxa = taxas.USD
            });

            return taxaCambioToEnums.Where(x => x.Moeda == moeda).First();
        }
    }

    public class TaxaCambioToEnumMaps
    {
        public Moeda Moeda { get; set; }
        public double Taxa { get; set; }
    }
}
