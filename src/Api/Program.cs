using Application.Abstractions;
using Application.Services;
using Infrastructure.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// DI wiring
builder.Services.AddSingleton<ISampleRepository, InMemorySampleRepository>();
builder.Services.AddSingleton<ISampleService, SampleService>();

// Order persistence (EF Core InMemory for local runs)
builder.Services.AddDbContext<OrderDbContext>(options =>
	options.UseInMemoryDatabase("OrdersDb"));
builder.Services.AddScoped<IOrderRepository, OrderEfRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
