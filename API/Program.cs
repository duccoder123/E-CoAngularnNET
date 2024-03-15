using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Middleware;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider;
try{
    var context = service.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await SeedingData.SeedUsers(context);
}
catch(Exception ex){
    var logger = service.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during seeding data");
}
app.Run();
