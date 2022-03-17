using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorMoedas.Services.Data
{
    public  class ExchangeRateData
    {
        public bool Success { get; set; }
        public int Timestamp { get; set; }
        public string Base { get; set; }
        public string Date { get; set; }
        public Rates Rates { get; set; }
        public Error error { get; set; }
    }

    public class Error
    {
        public int code { get; set; }
        public string info { get; set; }
    }

    public class Rates
    {
        public double BRL { get; set; }
        public double USD { get; set; }
        public double JPY { get; set; }
    }
}
