using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Northwind.Application.Common.Interfaces;

namespace Northwind.Infrastructure.Identity;

public class UserManagerService(UserManager<ApplicationUser> userManager) : IUserManager
{
    public async Task<string?> CreateUserAsync(string userName, string password)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = userName,
        };

        var result = await userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            return user.Id;
        }

        return null;
    }
}