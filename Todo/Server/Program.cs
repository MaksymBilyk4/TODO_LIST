using Microsoft.EntityFrameworkCore;
using Server.Midllewares;
using Server.Models;
using Server.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<TodolistContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("LocalhostNpgsqlConnectionString")));

builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();