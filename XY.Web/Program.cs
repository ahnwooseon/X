using XY.Web;
using XY.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddOutputCache();

var apiServiceUrl = builder.Configuration["ApiService:Url"] ?? "http://api";
var apiServicePort = builder.Configuration["ApiService:Port"] ?? "8080";

builder.Services.AddHttpClient<WeatherApiClient>(client =>
{
    // Utilisation des valeurs configurées pour l'URL de base du service API
    client.BaseAddress = new Uri($"{apiServiceUrl}:{apiServicePort}");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();
