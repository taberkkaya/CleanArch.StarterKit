using System.Threading.RateLimiting;
using CleanArch.StarterKit.Application;
using CleanArch.StarterKit.Infrastructure;
using CleanArch.StarterKit.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// Register CORS policy (open for development, restrict in production)
builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCorsPolicy", policy =>
    {
        policy
            .AllowAnyOrigin() // In production, use .WithOrigins("https://yourdomain.com")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Register infrastructure layer (DbContext, repositories, services, JWT authentication, etc.)
builder.Services.AddInfrastructure(builder.Configuration);

// Register application layer (MediatR, CQRS, etc.)
builder.Services.AddApplication();

// Register controllers
builder.Services.AddControllers();



// Register Swagger/OpenAPI with JWT Auth support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
});

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", options =>
    {
        options.QueueLimit = 100;
        options.PermitLimit = 100;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.Window = TimeSpan.FromSeconds(1);
    });
});

// Register health checks (SQL Server example)
builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

var app = builder.Build();

// Always enable Swagger and SwaggerUI
app.UseSwagger();
app.UseSwaggerUI();

// Enforce HTTPS redirection
app.UseHttpsRedirection();

// Use CORS policy (after HTTPS, before authentication/authorization)
app.UseCors("DefaultCorsPolicy");

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();

// Map controller endpoints
app.MapControllers();

// Map health check endpoint
app.MapHealthChecks("/health");

// Migrate and seed database (optional async extension)
await app.MigrateAndSeedDatabaseAsync<ApplicationDbContext>();

app.Run();
