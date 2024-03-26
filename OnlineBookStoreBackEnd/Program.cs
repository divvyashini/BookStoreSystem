using OnlineBookStoreSystem.Models;
using OnlineBookStoreSystem.Services;
using OnlineBookStoreSystem.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Build configuration
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Configure services
builder.Configuration.Bind("ApiSettings", new ApiSettings());
builder.Services.AddControllers();
// Add HttpClient
builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddTransient<IBookService<Post>, BookServiceManager<Post>>();
builder.Services.AddTransient<BookServiceApiClient>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.UseCors("AllowOrigin");

app.MapControllers();

app.Run();
