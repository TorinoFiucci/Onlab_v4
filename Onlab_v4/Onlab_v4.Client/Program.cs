using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// Inside builder.Services
builder.Services.AddMudServices();

//builder.Services.AddHttpClient("MyApi", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

//builder.Services.AddApiClientServices();

await builder.Build().RunAsync();
