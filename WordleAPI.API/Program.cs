using Microsoft.EntityFrameworkCore;
using WordleAPI.Application.Interfaces;
using WordleAPI.Application.Services;
using WordleAPI.Infrastructure.Data;
using WordleAPI.Infrastructure.Repositories;
using WordleAPI.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// ── Controllers & API Explorer ──────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Wordle API",
        Version = "v1",
        Description = "A RESTful Wordle game API built with Clean Architecture and .NET 8"
    });
});

// ── Database (EF Core / SQL Server) ─────────────────────────────────────────
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.MigrationsAssembly("WordleAPI.Infrastructure")
    ));

// ── Repositories (Infrastructure) ────────────────────────────────────────────
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGuessRepository, GuessRepository>();

// ── Services (Application + Infrastructure) ──────────────────────────────────
builder.Services.AddScoped<IWordService, WordService>();
builder.Services.AddScoped<IGameService, GameService>();

// ── CORS (open for development) ───────────────────────────────────────────────
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

// ── Auto-migrate on startup ───────────────────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// ── Middleware pipeline ───────────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wordle API v1");
        c.RoutePrefix = string.Empty; // Swagger at root
    });
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();
