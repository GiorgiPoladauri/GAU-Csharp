using System.Text;
using MedicalBilling.Application.Interfaces;
using MedicalBilling.Application.Services;
using MedicalBilling.Domain.Interfaces;
using MedicalBilling.Infrastructure.Data;
using MedicalBilling.Infrastructure.Repositories;
using MedicalBilling.Infrastructure.Services;
using MedicalBilling.Presentation.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ── Database ──────────────────────────────────────────────────────────────────
builder.Services.AddDbContext<HealthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ── Repositories ──────────────────────────────────────────────────────────────
builder.Services.AddScoped<IVisitRepository, VisitRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// ── Application Services ──────────────────────────────────────────────────────
builder.Services.AddScoped<IVisitService, VisitService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// ── JWT Authentication ────────────────────────────────────────────────────────
var jwt = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer           = true,
        ValidateAudience         = true,
        ValidateLifetime         = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer              = jwt["Issuer"],
        ValidAudience            = jwt["Audience"],
        IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["SecretKey"]!))
    };
});
builder.Services.AddAuthorization();

// ── Controllers + Swagger ─────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Medical Billing API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name        = "Authorization",
        Type        = SecuritySchemeType.Http,
        Scheme      = "Bearer",
        BearerFormat = "JWT",
        In          = ParameterLocation.Header,
        Description = "Enter your JWT token here."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// ── Middleware pipeline ────────────────────────────────────────────────────────
app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Medical Billing API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseJwtAuth(); // uses JwtAuthMiddleware extension (wraps UseAuthentication + UseAuthorization)
app.MapControllers();

// ── Run migrations and seed default admin ─────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<HealthDbContext>();
    db.Database.Migrate();

    var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();
    if (!await userRepo.UserExistsAsync("admin"))
    {
        await userRepo.AddUserAsync(
            "admin",
            BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            "admin",
            null);
        Console.WriteLine("Default admin seeded — username: admin / password: Admin@123");
    }
}

app.Run();
