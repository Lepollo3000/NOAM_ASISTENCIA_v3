using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NOAM_ASISTENCIA_v3.Client;
using NOAM_ASISTENCIA_v3.Client.Helpers;
using NOAM_ASISTENCIA_v3.Shared.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HOST ENVIRONMENT FOR CLIENT
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}api/") });

// register the cookie handler
builder.Services.AddScoped<CookieHandler>();

// set up authorization
builder.Services.AddAuthorizationCore();

// register the custom state provider
builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();

// register the account management interface
builder.Services.AddScoped(sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());

// configure client for auth interactions
builder.Services.AddHttpClient("Auth", opt => opt.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<CookieHandler>();

// PREVENT AUTHORIZE VIEW COMPONENT TO WRITE ON CONSOLE
builder.Logging.AddFilter("Microsoft.AspNetCore.Authorization.*", LogLevel.None);

await builder.Build().RunAsync();
