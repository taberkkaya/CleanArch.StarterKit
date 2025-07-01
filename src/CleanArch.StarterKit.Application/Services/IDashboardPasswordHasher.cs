namespace CleanArch.StarterKit.Application.Services;
public interface IDashboardPasswordHasher
{
    string HashPassword(string password);
    bool VerifyHashedPassword(string hashedPassword, string providedPassword);
}