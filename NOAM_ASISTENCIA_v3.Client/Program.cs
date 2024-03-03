using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NOAM_ASISTENCIA_v3.Client;
using NOAM_ASISTENCIA_v3.Client.Helpers.Services;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}api/") });
builder.Services.AddScoped<IAccountManagement, AccountManagement>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAutenticationStateProvider>();
//builder.Services.AddScoped<CookieHandler>();
//builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
//builder.Services.AddScoped(sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());

builder.Logging.AddFilter("Microsoft.AspNetCore.Authorization.*", LogLevel.None);

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
