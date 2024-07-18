using Microsoft.AspNetCore.Identity;

namespace BlazorChatSignalR.Authentication;

public class AppUser : IdentityUser
{
    public string FullName { get; set; }
}