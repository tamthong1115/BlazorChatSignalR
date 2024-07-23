using Microsoft.AspNetCore.Identity;

namespace BlazorChatSignalR.Authentication;

public class AppUser : IdentityUser
{
    public string Fullname { get; set; }
}