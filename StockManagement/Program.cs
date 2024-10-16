using StockManagement.Common.Data.Repositories;
using StockManagement.Common.Domain.Interfaces;
using StockManagement.Common.Redis;
using StockManagement.Common.Redis.Interfaces;
using StockManagement.Common.Redis.Repositories;
using StockManagement.Ioc.Dependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddScoped<IRedisRepository, RedisRepository>()
    .AddTransient<IProductRepository, ProductRepository>();

builder.Services.RegisterRedis();
builder.Services.RegisterRedisHealthCheck();
builder.Services.RegisterAutoMapper();


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
