using System.Security.Claims;
using BlazorChatSignalR.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BlazorChatSignalR.Authentication;

public class PersistingServerAuthenticationStateProvide : ServerAuthenticationStateProvider, IDisposable
{
    private readonly PersistentComponentState _persistentComponentState;
    private readonly IdentityOptions _identityOptions;
    private readonly PersistingComponentStateSubscription _subscription;
    private Task<AuthenticationState> _authenticationStateTask;

    public PersistingServerAuthenticationStateProvide(PersistentComponentState persistentComponentState,
        IOptions<IdentityOptions> optionAccessor)
    {
        _persistentComponentState = persistentComponentState;
        _identityOptions = optionAccessor.Value;

        AuthenticationStateChanged += OnAuthenticationStateChanged;
        _subscription =
            _persistentComponentState.RegisterOnPersisting(OnpersistingAsync, RenderMode.InteractiveWebAssembly);
    }

    private void OnAuthenticationStateChanged(Task<AuthenticationState> task)
    {
        _authenticationStateTask = task;
    }

    private async Task OnpersistingAsync()
    {
        var authenticationState = await _authenticationStateTask;
        var principal = authenticationState.User;
        if (principal.Identity?.IsAuthenticated == true)
        {
            var userId = principal.FindFirstValue(_identityOptions.ClaimsIdentity.UserIdClaimType);
            var email = principal.FindFirstValue(_identityOptions.ClaimsIdentity.EmailClaimType);
            var fullName = principal.Claims.Last(fn => fn.Type == ClaimTypes.Name).Value;

            if(userId != null && email != null && fullName != null)
            {
                _persistentComponentState.PersistAsJson(nameof(UserInfo), new UserInfo
                {
                    Id = userId,
                    FullName = fullName,
                    Email = email
                });
            }
        }

    }

    public void Dispose()
    {
        _subscription.Dispose();
        AuthenticationStateChanged -= OnAuthenticationStateChanged;
    }
}