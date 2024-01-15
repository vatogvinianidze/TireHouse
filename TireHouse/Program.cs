using TireHouse.Configuration;
using TireHouse.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.ConfigureDefendency(configuration);
builder.ConfigureSwagger();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.Configure();
app.Run();
