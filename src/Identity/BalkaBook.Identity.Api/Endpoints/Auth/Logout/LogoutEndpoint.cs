using BalkaBook.Identity.Api.Services.Auth;
using FastEndpoints;

namespace BalkaBook.Identity.Api.Endpoints.Auth.Logout;

public class LogoutEndpoint : EndpointWithoutRequest
{
    private readonly IAuthService _authService;

    public LogoutEndpoint(IAuthService authService)
    {
        _authService = authService;
    }

    public override void Configure()
    {
        Post("/auth/logout");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await _authService.LogoutAsync(ct);
    }
}
