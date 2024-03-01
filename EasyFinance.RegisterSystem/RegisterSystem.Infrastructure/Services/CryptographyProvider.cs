using RegisterSystem.Application.Common.Interfaces.Services;
using System.Security.Cryptography;

namespace RegisterSystem.Infrastructure.Services
{
    public class CryptographyProvider : ICryptographyProvider
    {
        public byte[] GenerateSalt()
        {
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                var salt = new byte[16];
                randomNumberGenerator.GetBytes(salt);
                return salt;
            }
        }

        public byte[] HashPassword(string password, byte[] salt)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                return hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool ValidatePassword(string providedPassword, byte[] storedHash, byte[] salt)
        {
            var hashOfProvidedPassword = HashPassword(providedPassword, salt);
            return hashOfProvidedPassword.SequenceEqual(storedHash);
        }
    }
}
