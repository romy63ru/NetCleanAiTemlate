using Application.Abstractions;
using Application.Services;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// DI wiring
builder.Services.AddSingleton<ISampleRepository, InMemorySampleRepository>();
builder.Services.AddSingleton<ISampleService, SampleService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
