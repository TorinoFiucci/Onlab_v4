using Microsoft.AspNetCore.Identity;
using Onlab.Dal; // Your namespace for ApplicationUser

// A placeholder implementation that doesn't actually send emails
// Good for development or if email confirmation isn't required.
public class DevelopmentEmailSender : IEmailSender<ApplicationUser>
{
    private readonly ILogger<DevelopmentEmailSender> _logger;

    // Inject ILogger to log the emails instead of sending them
    public DevelopmentEmailSender(ILogger<DevelopmentEmailSender> logger)
    {
        _logger = logger;
    }

    // Log the confirmation link
    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        _logger.LogWarning("----- DEVELOPMENT EMAIL SENDER -----");
        _logger.LogInformation("Confirmation Link for user {UserId} to email {Email}: {Link}", user.Id, email, confirmationLink);
        _logger.LogWarning("--------------------------------------");
        return Task.CompletedTask;
    }

    // Log the password reset link
    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        _logger.LogWarning("----- DEVELOPMENT EMAIL SENDER -----");
        _logger.LogInformation("Password Reset Link for user {UserId} to email {Email}: {Link}", user.Id, email, resetLink);
        _logger.LogWarning("--------------------------------------");
        return Task.CompletedTask;
    }

    // Log the password reset code
    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        _logger.LogWarning("----- DEVELOPMENT EMAIL SENDER -----");
        _logger.LogInformation("Password Reset Code for user {UserId} to email {Email}: {Code}", user.Id, email, resetCode);
        _logger.LogWarning("--------------------------------------");
        return Task.CompletedTask;
    }
}