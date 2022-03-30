using ConversorMoedas.Domain.Interfaces;
using ConversorMoedas.Repository.Context;
using ConversorMoedas.Repository.Repository;
using ConversorMoedas.Repository.Settings;

namespace ConversorMoedas.Api.Configuration
{
    public static class MongoConfiguration
    {
        public static IServiceCollection AddMongoConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDbsettingSection = configuration.GetSection("MongoSettings");
            services.Configure<MongoSettings>(mongoDbsettingSection);

            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<ICotacaoRepository, CotacaoRepository>();

            return services;    
        }
    }
}
