using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Extensions;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// #region Add services

// Add Controllers
builder.Services.AddControllers();

// Adiciona Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registra o DbContext com MySQL.
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySQL"),
        new MySqlServerVersion(new Version(8, 0, 46)),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()
    ));

// Registra os serviços da aplicação.
builder.Services.AddScoped<ITaskService, TaskService>();

// Registra os repositórios da infraestrutura.
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

var app = builder.Build();

// Ativa Swagger apenas em desenvolvimento.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use Controllers
app.MapControllers();

// Registra o middleware global de exceção.
app.UseGlobalExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();