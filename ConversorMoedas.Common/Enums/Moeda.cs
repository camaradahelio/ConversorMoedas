using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorMoedas.Common.Enums
{
    public enum Moeda : short
    {
        [Description("BRL")]
        BRL = 1,

        [Description("USD")]
        USD = 2,

        [Description("JPY")]
        JPY = 3,

        [Description("EUR")]
        EUR = 4
    }
}
