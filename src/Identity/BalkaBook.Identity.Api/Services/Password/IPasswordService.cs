namespace BalkaBook.Identity.Api.Services.Password;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}
