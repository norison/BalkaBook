using System.Security.Claims;
using FastEndpoints.Security;

namespace BalkaBook.Identity.Api.Services.Auth;

public class AuthService : IAuthService
{
    public async Task LoginAsync(Guid userId, CancellationToken ct = default)
    {
        await CookieAuth.SignInAsync(
            privileges =>
            {
                privileges.Claims.Add(new Claim("sub", userId.ToString()));
            });
    }

    public async Task LogoutAsync(CancellationToken ct = default)
    {
        await CookieAuth.SignOutAsync();
    }
}
