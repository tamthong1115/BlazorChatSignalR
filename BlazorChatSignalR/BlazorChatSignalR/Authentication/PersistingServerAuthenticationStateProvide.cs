using System.Security.Claims;
using BlazorChatSignalR.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BlazorChatSignalR.Authentication;

/*
 * This class is used to persist the user's authentication state across Blazor server-side sessions.
 */

/// <summary>
/// Provides an implementation of <see cref="ServerAuthenticationStateProvider"/> that persists authentication state across Blazor server-side sessions.
/// </summary>
public class PersistingServerAuthenticationStateProvide : ServerAuthenticationStateProvider, IDisposable
{
    private readonly PersistentComponentState _persistentComponentState;
    private readonly IdentityOptions _identityOptions;
    private readonly PersistingComponentStateSubscription _subscription;
    private Task<AuthenticationState> _authenticationStateTask;

    /// <summary>
    /// Initializes a new instance of the <see cref="PersistingServerAuthenticationStateProvide"/> class.
    /// </summary>
    /// <param name="persistentComponentState">The <see cref="PersistentComponentState"/> for persisting component state across prerendering.</param>
    /// <param name="optionAccessor">The options accessor for <see cref="IdentityOptions"/>.</param>
    public PersistingServerAuthenticationStateProvide(PersistentComponentState persistentComponentState,
        IOptions<IdentityOptions> optionAccessor)
    {
        _persistentComponentState = persistentComponentState;
        _identityOptions = optionAccessor.Value;

        AuthenticationStateChanged += OnAuthenticationStateChanged;
        _subscription =
            _persistentComponentState.RegisterOnPersisting(OnpersistingAsync, RenderMode.InteractiveWebAssembly);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PersistingServerAuthenticationStateProvide"/> class.
    /// </summary>
    /// <param name="persistentComponentState">The <see cref="PersistentComponentState"/> for persisting component state across prerendering.</param>
    /// <param name="optionAccessor">The options accessor for <see cref="IdentityOptions"/>.</param>
    private void OnAuthenticationStateChanged(Task<AuthenticationState> task)
    {
        // This method is called when the authentication state changes, and it updates the task that is returned by GetAuthenticationStateAsync.
        _authenticationStateTask = task;
    }

    /// <summary>
    /// Persists the current user's authentication state asynchronously during the persisting phase of the component state lifecycle.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
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

    /// <summary>
    /// Releases all resources used by the <see cref="PersistingServerAuthenticationStateProvide"/>.
    /// </summary>
    public void Dispose()
    {
        _subscription.Dispose();
        AuthenticationStateChanged -= OnAuthenticationStateChanged;
    }
}