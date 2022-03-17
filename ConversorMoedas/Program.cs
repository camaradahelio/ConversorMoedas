using ConversorMoedas.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostContext, config) => 
{
    var env = hostContext.HostingEnvironment;

    config.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
        .AddEnvironmentVariables();
});

builder.Services.AddMongoConfiguration(builder.Configuration);
builder.Services.AddApiConfiguration(builder.Configuration);

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

app.UseEndpoints(endpoints =>
 {
     endpoints.MapControllers();
 });

 app.Run();