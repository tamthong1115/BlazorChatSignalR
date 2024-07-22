using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace BlazorChatSignalR.Authentication;

/// <summary>
/// Provides extension methods for mapping additional identity-related endpoints.
/// </summary>
internal static class IdentityComponentsEndpointsRouteBuilderExtensions
{
    /// <summary>
    /// Maps additional identity-related endpoints to the application.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to add the endpoints to.</param>
    /// <returns>An <see cref="IEndpointConventionBuilder"/> that can be used to further configure the endpoints.</returns>
    /// <remarks>
    /// Currently, this method adds a logout endpoint under "/Account/Logout".
    /// When invoked, it signs the current user out and redirects to the home page.
    /// </remarks>
    public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
    {
        // Group all account-related endpoints under "/Account"
        var accountGroup = endpoints.MapGroup("/Account");

        // Map the logout action
        accountGroup.MapPost("/Logout", async(ClaimsPrincipal user, SignInManager<AppUser> signInManager) =>
        {
            // Sign out the current user
            await signInManager.SignOutAsync();
            // Redirect to the home page after successful sign out
            return TypedResults.LocalRedirect("/");
        });

        return accountGroup;
    }
}