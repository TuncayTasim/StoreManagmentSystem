using System.Security.Cryptography;

namespace StoreManagmentSystem.Helpers
{
    public static class TokenGenerator
    {
        public static string GenerateToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(32);
            var token = Convert.ToBase64String(tokenBytes);

            return token;
        }
    }
}
