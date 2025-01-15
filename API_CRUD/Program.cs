using API_CRUD.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  
            .AllowAnyMethod()  
            .AllowAnyHeader(); 
    });
});

var connectionString = builder.Configuration.GetConnectionString("MyCnn");

builder.Services.AddDbContext<PRN211Context>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Cấu hình CORS
app.UseCors("AllowAll");  

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();