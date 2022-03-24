using Api.Autenticacao.Models;
using AspNetCore.Identity.MongoDbCore.Models;
using ConversorMoedas.Repository.Settings;

namespace Api.Autenticacao.Configuration
{
    public static class IdentityMongoConfiguration
    {
        public static IServiceCollection AddIdentityMongoConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDbSettingAuthDb = configuration.GetSection("AuthDBSetting").Get<MongoSettings>();

            services.AddIdentity<UsuarioModel, MongoIdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddMongoDbStores<UsuarioModel, MongoIdentityRole, Guid>(mongoDbSettingAuthDb.ConnectionString, mongoDbSettingAuthDb.DatabaseName);

            return services;
        }
    }
}
