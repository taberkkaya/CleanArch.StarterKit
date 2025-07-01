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
        // Admin rolü varsa geç
        var adminRoleName = "Admin";
        if (!await roleManager.RoleExistsAsync(adminRoleName))
        {
            await roleManager.CreateAsync(new ApplicationRole { Name = adminRoleName });
        }

        // Admin kullanıcı varsa geç
        var adminEmail = "admin@admin.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "Admin123*"); // şifreyi istediğin gibi değiştirebilirsin
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, adminRoleName);
            }
        }

        var hasher = new DashboardPasswordHasher();
        if (!dbContext.HangfireDashboardUsers.Any(u => u.UserName == "admin"))
        {
            dbContext.HangfireDashboardUsers.Add(new HangfireDashboardUser
            {
                UserName = "admin",
                PasswordHash = hasher.HashPassword("admin123")
            });
            dbContext.SaveChanges();
        }

    }
}
