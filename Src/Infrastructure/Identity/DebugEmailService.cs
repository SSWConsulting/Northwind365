using Microsoft.AspNetCore.Identity.UI.Services;

namespace Northwind.Infrastructure;

public class DebugEmailService : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        Console.WriteLine($"MOCK EMAIL:{subject}");
        Console.WriteLine(htmlMessage);

        return Task.CompletedTask;
    }
}

// public class DebugEmailService : IEmailSender<ApplicationUser>
// {
//     public Task SendEmailAsync(string email, string subject, string htmlMessage)
//     {
//         Console.WriteLine($"MOCK EMAIL:{subject}");
//         Console.WriteLine(htmlMessage);
//
//         return Task.CompletedTask;
//     }
//
//     public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
//     {
//         Console.WriteLine($"MOCK EMAIL:{email}");
//         Console.WriteLine(confirmationLink);
//
//         return Task.CompletedTask;
//     }
//
//     public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
//     {
//         Console.WriteLine($"MOCK EMAIL:{email}");
//         Console.WriteLine(resetLink);
//
//         return Task.CompletedTask;
//     }
//
//     public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
//     {
//         Console.WriteLine($"MOCK EMAIL:{email}");
//         Console.WriteLine(resetCode);
//
//         return Task.CompletedTask;
//     }
// }