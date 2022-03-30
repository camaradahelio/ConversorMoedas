using Api.Autenticacao;
using Api.Autenticacao.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostContext, config) =>
{
    var env = hostContext.HostingEnvironment;

    config.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
        .AddEnvironmentVariables();
});

var appsettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appsettingsSection);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddIdentityMongoConfiguration(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API de Autenticação JWT",
        Description = "API de Autenticação JWT",
        Version = "v1",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Hélio Dutra",
            Url = new Uri("https://github.com/camaradahelio")
        }
    });
});

var app = builder.Build();

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