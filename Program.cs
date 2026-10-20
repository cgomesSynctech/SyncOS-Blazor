using info;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using syncos_Blazor.Components;
using syncos_Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// MudBlazor
builder.Services.AddMudServices();

// Autenticação e Autorização Blazor
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorizationCore();

// Serviços de Negócio
builder.Services.AddSingleton<ParcelaStatusService>();

// Banco de Dados - APPDbContext
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContextFactory<APPDbContext>(options =>
    options.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure()));

// Contexto com escopo para serviços que recebem APPDbContext diretamente
builder.Services.AddScoped(p => 
    p.GetRequiredService<IDbContextFactory<APPDbContext>>().CreateDbContext());

builder.Services.AddSignalR(options =>
{
    options.MaximumReceiveMessageSize = 500 * 1024 * 1024; // 500 MB
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();

// Servir arquivos da pasta de Pacotes para atualizações externas
var pacotesConfig = builder.Configuration["Pastas:Pacotes"];
var pacotesPath = string.IsNullOrWhiteSpace(pacotesConfig)
    ? Path.Combine(builder.Environment.ContentRootPath, "Pacotes")
    : (Path.IsPathRooted(pacotesConfig) ? pacotesConfig : Path.Combine(builder.Environment.ContentRootPath, pacotesConfig));

if (!Directory.Exists(pacotesPath))
{
    Directory.CreateDirectory(pacotesPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(pacotesPath),
    RequestPath = "/Pacotes"
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
