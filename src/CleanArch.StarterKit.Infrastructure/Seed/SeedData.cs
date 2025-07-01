using CleanArch.StarterKit.Domain.Entities;
using CleanArch.StarterKit.Domain.Identity;
using CleanArch.StarterKit.Infrastructure.Persistence;
using CleanArch.StarterKit.Infrastructure.Services;
using k8s.KubeConfigModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.StarterKit.Infrastructure.Seed;

public static class SeedData
{
    public static async Task SeedAsync(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        #region ApplicationUser
        var roles = new List<string>() { "admin", "developer" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new ApplicationRole { Name = role });
        }

        var email = "system@system.com";
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = "system",
                Email = email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, "System123*");

            if (result.Succeeded)
            {
                foreach (var role in roles)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
        #endregion

        #region HangfireUser
        var hasher = new DashboardPasswordHasher();
        if (!dbContext.HangfireDashboardUsers.Any(u => u.UserName == "hangfire-admin"))
        {
            dbContext.HangfireDashboardUsers.Add(new HangfireDashboardUser
            {
                UserName = "hangfire-admin",
                PasswordHash = hasher.HashPassword("Hanfire123*")
            });
            dbContext.SaveChanges();
        }
        #endregion

    }
}
