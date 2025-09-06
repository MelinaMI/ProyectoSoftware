using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Services;
using Application.Validators;
using Infrastructure.Commands;
using Infrastructure.Persistence;
using Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//CUSTOM
//Inyección de dependencias
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));

//Servicio para que me muestre asc y desc 
//Servicio para que me muestre asc y desc 
/*builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });*/

builder.Services.AddSwaggerGen(c =>
{
    // Mapea el enum OrderPrice como string con valores restringidos
    c.MapType<Application.Enum.OrderPrice>(() => new Microsoft.OpenApi.Models.OpenApiSchema
    {
        Type = "string",
        Enum = new List<Microsoft.OpenApi.Any.IOpenApiAny>
        {
            new Microsoft.OpenApi.Any.OpenApiString("asc"),
            new Microsoft.OpenApi.Any.OpenApiString("desc")
        }
    });
});


// Inyeccion de servicios
//-- DISH--

builder.Services.AddScoped<ICreateService, CreateDishService>();
builder.Services.AddScoped<IUpdateService, UpdateDishService>();
builder.Services.AddScoped<ICreateValidation, CreateDishValidator>();
builder.Services.AddScoped<IUpdateValidation, UpdateDishValidator>();
builder.Services.AddScoped<IGetService, GetDishService>();
builder.Services.AddScoped<IDishCommand, DishCommand>();
builder.Services.AddScoped<IDishQuery, DishQuery>();
builder.Services.AddScoped<ICategoryQuery, CategoryQuery>();
builder.Services.AddScoped<Exceptions>();
builder.Services.AddScoped<IGetValidation, GetDishValidator>();

//END CUSTOM 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
