using BlazorChatSignalR.Client.ChatServices;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register the ChatService with the DI container. This will allow the ChatService to be injected into components.
builder.Services.AddScoped<ChatService>();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

await builder.Build().RunAsync();