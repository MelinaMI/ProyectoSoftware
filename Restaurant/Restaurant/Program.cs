using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Services;
using Application.Validators;
using Infrastructure.Commands;
using Infrastructure.Persistence;
using Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddSwaggerGen(c =>
{
    c.UseInlineDefinitionsForEnums();
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
