using Microsoft.EntityFrameworkCore;
using Simple.Api.Services;
using Simple.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddDbContext<SimpleDbContext>(options =>
//    options.UseNpgsql(@"Server=127.0.0.1;Port=5432;Database=SimpleDb;User Id=postgres;Password=password;"));

builder.Services.AddDbContext<SimpleDbContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IGenericService, GenericService>();

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
