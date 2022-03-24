using Api.Autenticacao.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostContext, config) =>
{
    var env = hostContext.HostingEnvironment;

    config.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
        .AddEnvironmentVariables();
});

builder.Services.AddIdentityMongoConfiguration(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();