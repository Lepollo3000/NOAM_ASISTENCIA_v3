using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NOAM_ASISTENCIA_v3.Client;
using NOAM_ASISTENCIA_v3.Client.Helpers;
using NOAM_ASISTENCIA_v3.Client.Helpers.Services;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HOST ENVIRONMENT FOR CLIENT
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}api") });
// register the cookie handler
builder.Services.AddScoped<CookieHandler>();
// register the custom state provider
builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
// register the account management interface
builder.Services.AddScoped(sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());

// PREVENT AUTHORIZE VIEW COMPONENT TO WRITE ON CONSOLE
builder.Logging.AddFilter("Microsoft.AspNetCore.Authorization.*", LogLevel.None);

// set up authorization
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
