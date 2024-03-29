using API.Extensions;
using API.Middleware;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplicationServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
//Delegate --> function signature that you could assign any function to it using the same signature

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<SiteContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await SiteContextSeed.SeedDataAsync(context);
}
catch (Exception ex)
{
    logger.LogError(ex, "An Error Occured During The Migration !");
}

app.Run();
