using BalkaBook.Identity.Api.Services.Auth;
using BalkaBook.Identity.Api.Services.Password;
using BalkaBook.Identity.Persistence.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BalkaBook.Identity.Api.Endpoints.Auth.Login;

public class LoginEndpoint : Endpoint<LoginRequest>
{
    private readonly IIdentityDbContext _context;
    private readonly IAuthService _authService;
    private readonly IPasswordService _passwordService;

    public LoginEndpoint(IIdentityDbContext context, IAuthService authService, IPasswordService passwordService)
    {
        _context = context;
        _authService = authService;
        _passwordService = passwordService;
    }

    public override void Configure()
    {
        Post("/auth/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var user = await _context.Users
            .Where(x => x.UserName == req.UserName)
            .Select(x => new { x.Id, x.PasswordHash })
            .FirstOrDefaultAsync(ct);

        if (user is null || !_passwordService.VerifyPassword(req.Password, user.PasswordHash))
        {
            ThrowError("Invalid username or password");
            return;
        }

        await _authService.LoginAsync(user.Id, ct);
        await SendNoContentAsync(ct);
    }
}
