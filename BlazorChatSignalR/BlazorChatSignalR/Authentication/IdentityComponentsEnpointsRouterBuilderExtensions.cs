using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace BlazorChatSignalR.Authentication
{
	internal static class IdentityComponentsEnpointsRouterBuilderExtensions
	{
		public static IEndpointConventionBuilder MapAdditionalIdentityEnpoints(this IEndpointRouteBuilder endpoints)
		{
			var accountGroup = endpoints.MapGroup("/Account");
			accountGroup.MapPost("/Logout", async (ClaimsPrincipal user, SignInManager<AppUser> signInManager) =>
			{
				await signInManager.SignOutAsync();
				return TypedResults.LocalRedirect("/");
			});
			return accountGroup;
		}
	}
}