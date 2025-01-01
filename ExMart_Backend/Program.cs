using System.Reflection;
using ExMart_Backend;
using ExMart_Backend.Data;
using ExMart_Backend.Interface;
using ExMart_Backend.Repository;
using ExMart_Backend.Services.Interface;
using ExMart_Backend.Services.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using YourNamespace.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>
    (options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddControllers();


builder.Services.AddScoped<DBDataInitializer>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddTransient<IMailRepository, MailRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<DBDataInitializer>();
builder.Services.AddScoped<IAddToCartRepository, AddToCartRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IImageUpload, ImageUploadRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

    // Enable Swagger to handle IFormFile
    c.OperationFilter<FileUploadOperation>();
}); builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Allow Angular app URL
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // If you're using cookies for authentication
    });
});
builder.Services.AddAutoMapper(typeof(MappingConfig));

var app = builder.Build();
app.UseCors("AllowAngularApp");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExMart_Backend v1");
    });
}
app.UseHttpsRedirection();
// Apply CORS policy globally
app.UseAuthorization();

app.MapControllers();

app.Run();