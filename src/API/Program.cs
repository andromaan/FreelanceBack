using API.Modules;
using API.Services.UserProvider;
using BLL;
using BLL.Common.Interfaces;
using BLL.Middlewares;
using DAL;
using Microsoft.Extensions.FileProviders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDataAccess(builder);
builder.Services.AddBusinessLogic(builder);

builder.Services.AddScoped<IUserProvider, UserProvider>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(options => options
    .WithOrigins("http://localhost:3000", "https://localhost:3000")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
);

app.UseAuthentication();
app.UseAuthorization();

if (!app.Environment.IsEnvironment("Test"))
{
    await app.InitialiseDb();
}

app.MapControllers();

app.UseMiddleware<MiddlewareExceptionsHandling>();

var imagesPath = Path.Combine(builder.Environment.ContentRootPath, Settings.ImagesPathSettings.ImagesPath);

if (!Directory.Exists(imagesPath))
{
    Directory.CreateDirectory(imagesPath);

    foreach (var file in Settings.ImagesPathSettings.ListOfDirectoriesNames)
    {
        var containersPath = Path.Combine(imagesPath, file);
        if (!Directory.Exists(containersPath))
        {
            Directory.CreateDirectory(containersPath);
        }
    }
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesPath),
    RequestPath = $"/{Settings.ImagesPathSettings.StaticFileRequestPath}"
});


app.Run();

namespace API
{
    public class Program;
}