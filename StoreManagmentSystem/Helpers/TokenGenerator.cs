using System.Security.Cryptography;

namespace StoreManagmentSystem.Helpers
{
    public static class TokenGenerator
    {
        public static string GenerateResetToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(32);
            var token = Convert.ToBase64String(tokenBytes);

            return token;
        }

        public static string GenerateConfirmToken()
        {
            var tokenBytes = RandomNumberGenerator.GetInt32(10000000, 100000000);
            var token = Convert.ToString(tokenBytes);

            return token;
        }

        public static string GenerateBulgarianEan13()
        {
            string countryCode = "380";

            Random random = new Random();
            string randomPart = "";
            for (int i = 0; i < 9; i++)
            {
                randomPart += random.Next(0, 10).ToString();
            }

            string twelveDigits = countryCode + randomPart;

            int checkDigit = CalculateChecksum(twelveDigits);

            return twelveDigits + checkDigit;
        }

        private static int CalculateChecksum(string data)
        {
            int sum = 0;

            for (int i = 0; i < 12; i++)
            {
                int digit = int.Parse(data[i].ToString());

                if (i % 2 == 0)
                    sum += digit * 1;
                else
                    sum += digit * 3;
            }

            int remainder = sum % 10;
            return (remainder == 0) ? 0 : 10 - remainder;
        }
    }
}

