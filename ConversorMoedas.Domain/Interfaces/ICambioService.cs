using ConversorMoedas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorMoedas.Domain.Interfaces
{
    public interface ICambioService
    {
        Task<TaxaCambio> ObterTaxas();
    }
}
