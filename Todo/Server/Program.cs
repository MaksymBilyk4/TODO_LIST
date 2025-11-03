using Microsoft.EntityFrameworkCore;
using Server.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<TodolistContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("LocalhostNpgsqlConnectionString")));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();