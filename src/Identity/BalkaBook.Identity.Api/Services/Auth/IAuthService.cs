namespace BalkaBook.Identity.Api.Services.Auth;

public interface IAuthService
{
    Task LoginAsync(Guid userId, CancellationToken ct = default);
    Task LogoutAsync(CancellationToken ct = default);
}
