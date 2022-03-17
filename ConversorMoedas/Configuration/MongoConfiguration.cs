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
            services.Configure<MongoSettings>(config => 
            {
                config.ConnectionString = configuration["MongoDb:ConnectionString"];
                config.DatabaseName = configuration["MongoDb:DatabaseName"];
            });

            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<ICotacaoRepository, CotacaoRepository>();

            return services;    
        }
    }
}
