using ConversorMoedas.Domain.Interfaces;
using ConversorMoedas.Services.Services;

namespace ConversorMoedas.Api.Configuration
{
    public static class ApiConfiguration
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICambioService, CambioService>();
            return services;
        }
    }
}
