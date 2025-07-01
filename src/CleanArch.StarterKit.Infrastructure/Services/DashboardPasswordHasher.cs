using CleanArch.StarterKit.Application.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.StarterKit.Infrastructure.Services;
public class DashboardPasswordHasher : IDashboardPasswordHasher
{
    public string HashPassword(string password)
    {
        return new PasswordHasher<object?>().HashPassword(null, password);
    }
    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        return new PasswordHasher<object?>().VerifyHashedPassword(null, hashedPassword, providedPassword) != PasswordVerificationResult.Failed;
    }
}