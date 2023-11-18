using System.Text.Json.Serialization;
using ContactService.Application;
using ContactService.Domain.Repositories;
using ContactService.Infrastructure;
using ContactService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.


builder.Services.AddApplication(configuration);
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (Environment.GetEnvironmentVariable("MIGRATE_DB") == "1")
{
    MigrateDb();
}
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


void MigrateDb()
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var appDbContext = services.GetRequiredService<ContactDbContext>();
    appDbContext.Database.Migrate();
}