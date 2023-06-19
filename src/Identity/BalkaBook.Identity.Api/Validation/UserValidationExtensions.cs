using FluentValidation;

namespace BalkaBook.Identity.Api.Validation;

public static class UserValidationExtensions
{
    public static IRuleBuilderOptions<T, string> Username<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.NotEmpty().MinimumLength(2).MaximumLength(20);
    }
    
    public static IRuleBuilderOptions<T, string> Email<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.NotEmpty().EmailAddress().MaximumLength(100);
    }
    
    public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.NotEmpty().MinimumLength(2).MaximumLength(20);
    }
    
    public static IRuleBuilderOptions<T, string> FirstName<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.NotEmpty().MinimumLength(2).MaximumLength(50);
    }
    
    public static IRuleBuilderOptions<T, string> LastName<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.NotEmpty().MinimumLength(2).MaximumLength(50);
    }
}
