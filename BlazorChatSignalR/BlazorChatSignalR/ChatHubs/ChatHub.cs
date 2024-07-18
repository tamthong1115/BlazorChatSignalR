using BlazorChatSignalR.Repos;
using ChatModels;
using Microsoft.AspNetCore.SignalR;

namespace BlazorChatSignalR.ChatHubs;

// The ChatHub class is a SignalR hub that clients will connect to in order to send and receive messages.
public class ChatHub(ChatRepo chatRepo) : Hub
{
    public async Task SendMessage(Chat chat)
    {
        await chatRepo.SaveChatAsync(chat);
        await Clients.All.SendAsync("ReceiveMessage", chat);
    }


}