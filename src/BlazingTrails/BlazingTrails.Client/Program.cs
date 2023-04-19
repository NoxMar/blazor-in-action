using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazingTrails.Client;
using BlazingTrails.Client.AppState;
using BlazorBootstrap;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazorBootstrap();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AppState>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddHttpClient("SecureAPIClient",
        client => client.BaseAddress =
            new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddOidcAuthentication(cfg =>
{
    builder.Configuration.Bind("Auth0", cfg.ProviderOptions);
    cfg.ProviderOptions.ResponseType = "code";
});

await builder.Build().RunAsync();
