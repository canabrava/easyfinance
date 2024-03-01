namespace RegisterSystem.Application.Common.Interfaces.Services
{
    public interface ICryptographyProvider
    {
        byte[] GenerateSalt();
        byte[] HashPassword(string password, byte[] salt);
        bool ValidatePassword(string providedPassword, byte[] storedHash, byte[] salt);
    }
}
