using ConversorMoedas.Api;
using ConversorMoedas.Api.Configuration;
using ConversorMoedas.Services.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostContext, config) => 
{
    var env = hostContext.HostingEnvironment;

    config.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
        .AddEnvironmentVariables();

    config.AddUserSecrets<Program>();
});

var exchangerateapi = builder.Configuration.GetSection("ExchangeRatesApi");
builder.Services.Configure<ExchangeRatesApiSettings>(exchangerateapi);

builder.Services.AddSingleton<ExchangeRatesApiSettings>();

builder.Services.AddScoped<HttpClient>();

builder.Services.AddMongoConfiguration(builder.Configuration);
builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(options => 
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => 
{
    options.UseInlineDefinitionsForEnums();

    options.DescribeAllParametersInCamelCase();

    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    {
        Title = "Conversor de moedas V1",
        Description = "API para conversão de moedas",
        Version = "v1", 
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Hélio Dutra",
            Url = new Uri("https://github.com/camaradahelio")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setup => 
    {
        setup.SwaggerEndpoint($"/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints =>
 {
     endpoints.MapControllers();
 });

 app.Run();