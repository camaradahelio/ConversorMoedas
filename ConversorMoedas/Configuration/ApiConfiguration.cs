using ConversorMoedas.Domain.Interfaces;
using ConversorMoedas.Domain.Services;
using ConversorMoedas.Repository.Context;
using ConversorMoedas.Repository.Repository;
using ConversorMoedas.Services.Services;

namespace ConversorMoedas.Api.Configuration
{
    public static class ApiConfiguration
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICambioService, CambioService>();
            services.AddScoped<ICotacaoService, CotacaoService>();
            services.AddScoped<ICotacaoRepository, CotacaoRepository>();
            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            return services;
        }
    }
}
