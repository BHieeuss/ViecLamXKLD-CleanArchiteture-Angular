using Microsoft.OpenApi.Models;
using ViecLam.Application.Extensions;
using ViecLam.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAuthorization();

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
;

// Add Services for ImageFile (uncomment if needed)
// builder.Services.AddTransient<IFileService, FileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHsts();
}

app.UseHttpsRedirection();

// Map custom endpoints
//app.MapBlogEndpoints();

app.UseAuthorization();

app.MapControllers();

app.Run();
