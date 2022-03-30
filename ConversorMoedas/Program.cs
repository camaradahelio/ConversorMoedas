using ConversorMoedas.Api;
using ConversorMoedas.Api.Configuration;
using ConversorMoedas.Services.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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

var exchangerateapi = builder.Configuration.GetSection("ExchangeRatesApi");
builder.Services.Configure<ExchangeRatesApiSettings>(exchangerateapi);

builder.Services.AddSingleton<ExchangeRatesApiSettings>();

builder.Services.AddScoped<HttpClient>();

builder.Services.AddMongoConfiguration(builder.Configuration);
builder.Services.AddApiConfiguration(builder.Configuration);

var appSettings = appsettingsSection.Get<AppSettings>();
var chave = Encoding.ASCII.GetBytes(appSettings.Chave);

//builder.Services.AddAuthentication(c => 
//{
//    c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
//}).AddJwtBearer(j => 
//{
//    j.RequireHttpsMetadata = true;
//    j.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(chave),
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidAudience = appSettings.ValidoEm,
//        ValidIssuer = appSettings.Emissor
//    };
//});

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => 
{
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

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Bearer",
        Description = "Autenticação JWT",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string [] { }
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
//app.UseAuthentication();

app.UseRouting();

app.UseEndpoints(endpoints =>
 {
     endpoints.MapControllers();
 });

 app.Run();