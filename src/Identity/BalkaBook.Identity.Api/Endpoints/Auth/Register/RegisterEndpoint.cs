using BalkaBook.Identity.Api.Services.Auth;
using BalkaBook.Identity.Persistence.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BalkaBook.Identity.Api.Endpoints.Auth.Register;

public class RegisterEndpoint : Endpoint<RegisterRequest, EmptyResponse, RegisterMapper>
{
    private readonly IIdentityDbContext _context;
    private readonly IAuthService _authService;

    public RegisterEndpoint(IIdentityDbContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public override void Configure()
    {
        Post("/auth/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        var isUserExists = await _context.Users.AnyAsync(x => x.UserName == req.UserName || x.Email == req.Email, ct);
        
        if (isUserExists)
        {
            ThrowError("User already exists");
            return;
        }

        var user = Map.ToEntity(req);

        await _context.Users.AddAsync(user, ct);
        await _context.SaveChangesAsync(ct);
        await _authService.LoginAsync(user.Id, ct);
        await SendNoContentAsync(ct);
    }
}
