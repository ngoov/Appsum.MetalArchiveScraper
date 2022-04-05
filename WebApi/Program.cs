using Data;

using Microsoft.EntityFrameworkCore;

using NodaTime;
using NodaTime.Serialization.SystemTextJson;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMetalDbContext();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000").Build()));


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();