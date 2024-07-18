using ChatModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorChatSignalR.Client.ChatServices;

public class ChatService
{
    public Action? InvokeChatDisplay { get; set; }
    public List<Chat> Chats { get; set; } = new List<Chat>();
    private readonly HubConnection _hubConnection;
    public bool IsConnected { get; set; }

    public ChatService(NavigationManager navigationManager)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri("/chathub"))
            .Build();
    }

    public void ReceiveMessage()
    {

        _hubConnection.On<Chat>("ReceiveMessage", chat =>
        {
            Chats.Add(chat);
            InvokeChatDisplay?.Invoke(); // Invoke the action to update the UI
        });
    }

    public async Task StartConnectionAsync()
    {
        await _hubConnection.StartAsync();
        IsConnected = _hubConnection!.State == HubConnectionState.Connected;
    }

    public async Task SendChat(Chat chat)
    {
        // SendMessage is the method name defined in the ChatHub class
        await _hubConnection.SendAsync("SendMessage", chat);
    }
}