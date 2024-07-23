
using BlazorChatSignalR.Client.ChatServices;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorChatSignalR.Client.Authentication;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<ChatService>();
// Register the ChatService with the DI container. This will allow the ChatService to be injected into components.
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingAuthenticationStateProvider>();


builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

await builder.Build().RunAsync();