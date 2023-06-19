using BalkaBook.Identity.Api.Services.Password;
using BalkaBook.Identity.Persistence.Entities;
using FastEndpoints;

namespace BalkaBook.Identity.Api.Endpoints.Auth.Register;

public class RegisterMapper : Mapper<RegisterRequest, EmptyResponse, User>
{
    private readonly IPasswordService _passwordService;

    public RegisterMapper(IPasswordService passwordService)
    {
        _passwordService = passwordService;
    }
    
    public override User ToEntity(RegisterRequest r)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            UserName = r.UserName,
            Email = r.Email,
            PasswordHash = _passwordService.HashPassword(r.Password),
            FirstName = r.FirstName,
            LastName = r.LastName
        };
    }
}
