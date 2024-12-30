using System.Text.Json.Serialization;
using ExMart_Backend;
using ExMart_Backend.Data;
using ExMart_Backend.Interface;
using ExMart_Backend.Repository;
using ExMart_Backend.Services.Interface;
using ExMart_Backend.Services.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>
    (options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();

   
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<DBDataInitializer>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddTransient<IMailRepository, MailRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IAddToCartRepository, AddToCartRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IConfigRepository, ConfigRepository>();
//builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
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

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
// Apply CORS policy globally
app.UseCors("AllowAngularApp");
app.UseAuthorization();

app.MapControllers();

app.Run();
