using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// #region Add services
builder.Services.AddSwaggerGen();

// Add Controllers
builder.Services.AddControllers();

// Add DbContext (MySQL)
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySQL"),
        new MySqlServerVersion(new Version(8, 0, 35)),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()
    ));

// Add TaskService (Application)
builder.Services.AddScoped<ITaskService, TaskService>();

// Add TaskRepository (Infrastructure)
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// #endregion

var app = builder.Build();

// #region Configure middleware

// Use Swagger (só no development)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use Controllers
app.MapControllers();

// #endregion
app.Run();