using Blazored.LocalStorage;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using NOAM_ASISTENCIA_v3.Client;
using NOAM_ASISTENCIA_v3.Client.Helpers.Services;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}api/") });

builder.Services.AddScoped<IAccountManagement, AccountManagement>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAutenticationStateProvider>();

builder.Services.AddOptions();
builder.Services.AddMudServices();
builder.Services.AddSweetAlert2();
builder.Services.AddApiAuthorization();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddCascadingAuthenticationState();

builder.Logging.AddFilter("Microsoft.AspNetCore.Authorization.*", LogLevel.None);

await builder.Build().RunAsync();
