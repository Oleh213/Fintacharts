using Fintacharts.Abstractions.AppSettings;
using Fintacharts.Business;
using Fintacharts.DataService;
using Fintacharts.Infrastructure.Database;
using Fintacharts.Infrastructure.Mediatr;
using Fintacharts.Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

SQLitePCL.Batteries.Init();

builder.Services.AddSqLiteInfrastructure();
builder.Services.AddDataServiceInfrastructure();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var appSettings = BindAppSettings(builder.Configuration);

builder.Services.AddSingleton(appSettings);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Fintacharts API",
        Version = "v1"
    });
});

var assemblies = new[]
{
    typeof(Program).Assembly, 
    typeof(BusinessServiceCollectionExtensions).Assembly,
};

builder.Services.AddMediatRInfrastructure(assemblies);
builder.Services.AddBusiness();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Play Tech API");
        c.RoutePrefix = string.Empty;  
    });
}

app.Use(async (context, next) =>
{
    await next();
    var path = context.Request.Path.Value!;
    if (context.Response.StatusCode == 404 &&
        !path.StartsWith("/api/"))
    {
        context.Request.Path = "/index.html";
        await next();
    }
});

app.UseRouting();
app.UseCors(builder =>
    builder.WithOrigins()
        .SetIsOriginAllowed(origin => true)
        .AllowAnyHeader()
        .AllowCredentials()
        .AllowAnyMethod());

var logger = app.Logger;

await ApplyMigrationsAndSeeding(app, logger, appSettings);

app.UseAuthorization();
app.MapControllers();

app.Run();


static async Task ApplyMigrationsAndSeeding(IApplicationBuilder app, ILogger logger, AppSettings appSettings)
{
    var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();

    if (scopeFactory != null)
    {
        using var scope = scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<FintachartsDbContext>();
        logger.LogInformation("Start migration database.");
        try
        {
            await context.Database.MigrateAsync();
            logger.LogInformation("Migration database completed.");
            
            var seeding = new Seeding(new FintachartsService(appSettings));
            await seeding.Seed(context);
        }
        catch (Exception ex)
        {
            logger.LogError("Error during migration/seeding: {Message}", ex.Message);
            throw;
        }
    }
}


static AppSettings BindAppSettings(IConfiguration configuration)
{
    var appSettings = new AppSettings();

    try
    {
        
        new ConfigureFromConfigurationOptions<AppSettings>(configuration).Configure(appSettings);
    }
    catch (Exception exception)
    {
        Console.WriteLine($"Error binding appsettings.json to AppSettings: {exception}");
        throw;
    }

    return appSettings;
}
