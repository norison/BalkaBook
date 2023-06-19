using BalkaBook.Identity.Api.Validation;
using FastEndpoints;

namespace BalkaBook.Identity.Api.Endpoints.Auth.Login;

public class LoginValidator : Validator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.UserName).Username();
        RuleFor(x => x.Password).Password();
    }
}
