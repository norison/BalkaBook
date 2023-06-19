namespace BalkaBook.Identity.Api.Endpoints.Auth.Register;

public record RegisterRequest(string UserName, string Email, string Password, string FirstName, string LastName);
