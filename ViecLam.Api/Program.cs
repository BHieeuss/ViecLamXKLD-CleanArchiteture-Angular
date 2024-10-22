using Microsoft.OpenApi.Models;
using ViecLam.Application.Extensions;
using ViecLam.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAuthorization();

// Add CORS support
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", builder =>
    {
        builder.WithOrigins("https://localhost:4200", "http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

// Add persistence services and application services
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ViecLam API", Version = "v1" });
    c.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHsts();
}

// Middleware setup

// CORS middleware
app.UseCors("AllowAngular");

// WebSocket extension middleware
app.UseWebSockets(); 
app.UseCustomWebSocket();

// TCP Socket Server extension
app.UseTcpSocketServer();

// HTTPS Redirection middleware
app.UseHttpsRedirection();

// Authorization middleware
app.UseAuthorization();

// Mapping controllers
app.MapControllers();

app.Run();
