using Microsoft.Extensions.Configuration;
using WebApplication1.Configurations;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Obtener la configuración del builder
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));
builder.Services.AddHttpClient();
builder.Services.AddScoped<IApiService, ApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
