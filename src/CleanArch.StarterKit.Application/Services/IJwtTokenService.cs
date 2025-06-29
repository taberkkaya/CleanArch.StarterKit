namespace CleanArch.StarterKit.Application.Services;

public interface IJwtTokenService
{
    string GenerateToken(string userId, string userName, string email, IList<string> roles);
}
