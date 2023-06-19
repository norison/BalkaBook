using BalkaBook.Identity.Api.Validation;
using FastEndpoints;

namespace BalkaBook.Identity.Api.Endpoints.Auth.Register;

public class RegisterValidator : Validator<RegisterRequest>
{
    public RegisterValidator()
    {
        RuleFor(x => x.UserName).Username();
        RuleFor(x => x.Email).Email();
        RuleFor(x => x.Password).Password();
        RuleFor(x => x.FirstName).FirstName();
        RuleFor(x => x.LastName).LastName();
    }
}
