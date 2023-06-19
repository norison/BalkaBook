using BalkaBook.Identity.Api.Services.Auth;
using BalkaBook.Identity.Api.Services.Password;
using BalkaBook.Identity.Persistence.Data;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IdentityDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddFastEndpoints();
builder.Services.AddCookieAuth(TimeSpan.FromDays(365),
    options =>
    {
        options.Cookie.Name = "BalkaBook.Identity";
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        };
    });

builder.Services.AddScoped<IIdentityDbContext>(provider => provider.GetRequiredService<IdentityDbContext>());
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IPasswordService, PasswordService>();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var context = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
    await context.Database.MigrateAsync();
}

app.UseDefaultExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints(
    config =>
    {
        config.Errors.GeneralErrorsField = "general";
        config.Errors.UseProblemDetails();
    });

app.Run();
