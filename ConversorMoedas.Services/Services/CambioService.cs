using ConversorMoedas.Domain.Entities;
using ConversorMoedas.Domain.Interfaces;
using ConversorMoedas.Services.Data;
using ConversorMoedas.Services.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConversorMoedas.Services.Services
{
    public class CambioService : ICambioService
    {
        private readonly HttpClient _httpClient;
        private readonly ExchangeRatesApiSettings _exchangeRatesApiSettings;

        public CambioService(HttpClient httpClient, IOptions<ExchangeRatesApiSettings> exchangeRatesApiSettings) 
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(exchangeRatesApiSettings.Value.Url);
            _exchangeRatesApiSettings = exchangeRatesApiSettings.Value;
        }

        public async Task<TaxaCambio> ObterTaxas()
        {
            var response = await _httpClient.GetAsync($"latest?access_key={_exchangeRatesApiSettings.Key}&base=EUR&symbols=BRL,USD,JPY");
            response.EnsureSuccessStatusCode();
            string resultJson = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            ExchangeRateData exchangeRateData = JsonSerializer.Deserialize<ExchangeRateData>(resultJson, options);

            if (exchangeRateData.Success)
            {
                TaxaCambio taxaCambio = new TaxaCambio();
                taxaCambio.BRL = exchangeRateData.Rates.BRL;
                taxaCambio.USD = exchangeRateData.Rates.USD;
                taxaCambio.JPY = exchangeRateData.Rates.JPY;

                return taxaCambio;
            }
            else
            {
                throw new Exception($"Exchangeratesapi Erro: {exchangeRateData.error.code} - {exchangeRateData.error.info}");
            }                
        }
    }
}
