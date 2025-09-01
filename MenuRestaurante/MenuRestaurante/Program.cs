using Application.Interfaces.IStatus;
using Application.UseCase.Status;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//agrego swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inyección de dependencias
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));

// Registrar el servicio de Status
//builder.Services.AddScoped<IStatusService, StatusService>();

var app = builder.Build();

// Crear la base de datos / aplicar migraciones al iniciar
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();      
    app.UseSwaggerUI();    
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
