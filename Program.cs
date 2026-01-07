using Microsoft.EntityFrameworkCore;
using CommentsApi.Services;
using CommentsApi.Data;


var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger clásico (NET 8)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=/data/comments.db"));

builder.Services.AddScoped<ISentimentService, SentimentService>();

var app = builder.Build();

// Swagger - habilitado siempre para facilitar el desarrollo
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Comments API V1");
    c.RoutePrefix = string.Empty; // Swagger en la raíz
});

// Middleware
app.UseAuthorization();
app.MapControllers();

// Aplicar migraciones automáticamente
try
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        
        logger.LogInformation("Aplicando migraciones de base de datos...");
        db.Database.Migrate();
        logger.LogInformation("Migraciones aplicadas correctamente.");
    }
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Error al aplicar migraciones: {Message}", ex.Message);
    throw;
}

app.Run();
